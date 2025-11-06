## OOP, Design Patterns, DBMS (Connection + CRUD), Validation, Business Logic/Middleware/API, Data Flow

This guide demonstrates each topic with concise, runnable-style Python snippets using FastAPI + SQLAlchemy + Pydantic. You can copy the snippets into a project, or use them as a study reference.

---

### OOPS (Object-Oriented Programming)

Key ideas: encapsulation, abstraction, inheritance, polymorphism.

```python
from abc import ABC, abstractmethod

# Abstraction via an interface-like base class
class Notifier(ABC):
    @abstractmethod
    def send(self, message: str) -> str:
        ...

# Inheritance + Polymorphism
class EmailNotifier(Notifier):
    def __init__(self, address: str) -> None:
        self.address = address

    def send(self, message: str) -> str:  # polymorphic behavior
        return f"Email to {self.address}: {message}"

class SmsNotifier(Notifier):
    def __init__(self, phone: str) -> None:
        self.phone = phone

    def send(self, message: str) -> str:
        return f"SMS to {self.phone}: {message}"

# Encapsulation: service hides internal details from callers
class NotificationService:
    def __init__(self, notifier: Notifier) -> None:
        self._notifier = notifier

    def notify_user(self, message: str) -> str:
        return self._notifier.send(message)

# Usage
service = NotificationService(EmailNotifier("user@example.com"))
print(service.notify_user("Welcome!"))
```

---

### Design Patterns (Components)

Shown: Factory, Singleton, Strategy.

```python
# Factory: create appropriate notifier without exposing construction logic
class NotifierFactory:
    @staticmethod
    def create(channel: str, target: str) -> Notifier:
        if channel == "email":
            return EmailNotifier(target)
        if channel == "sms":
            return SmsNotifier(target)
        raise ValueError("Unknown channel")

# Singleton: one app-level config instance
class SingletonMeta(type):
    _instances: dict[type, object] = {}
    def __call__(cls, *args, **kwargs):  # type: ignore[override]
        if cls not in cls._instances:
            cls._instances[cls] = super().__call__(*args, **kwargs)
        return cls._instances[cls]

class AppConfig(metaclass=SingletonMeta):
    def __init__(self) -> None:
        self.app_name = "Lift Project"
        self.debug = True

# Strategy: interchangeable password hashing strategies
from abc import ABC, abstractmethod
from passlib.hash import bcrypt

class PasswordHasher(ABC):
    @abstractmethod
    def hash(self, plain: str) -> str: ...
    @abstractmethod
    def verify(self, plain: str, hashed: str) -> bool: ...

class BCryptHasher(PasswordHasher):
    def hash(self, plain: str) -> str:
        return bcrypt.hash(plain)
    def verify(self, plain: str, hashed: str) -> bool:
        return bcrypt.verify(plain, hashed)

class NoopHasher(PasswordHasher):
    def hash(self, plain: str) -> str:
        return plain
    def verify(self, plain: str, hashed: str) -> bool:
        return plain == hashed
```

---

### DBMS - Connection

Using SQLAlchemy 2.x style: engine + session factory.

```python
from sqlalchemy import create_engine
from sqlalchemy.orm import sessionmaker, Session

class DatabaseSessionFactory:
    _engine = None
    _session_factory = None

    @classmethod
    def configure(cls, url: str = "sqlite:///./app.db", echo: bool = False) -> None:
        if cls._engine is None:
            cls._engine = create_engine(url, echo=echo, future=True)
            cls._session_factory = sessionmaker(bind=cls._engine, autoflush=False, autocommit=False, future=True)

    @classmethod
    def get_session(cls) -> Session:
        if cls._session_factory is None:
            cls.configure()
        return cls._session_factory()
```

---

### DBMS - CRUD Operations

Model, repository, and service examples.

```python
from sqlalchemy.orm import DeclarativeBase, Mapped, mapped_column
from sqlalchemy import String, select

class Base(DeclarativeBase):
    pass

class User(Base):
    __tablename__ = "user"
    id: Mapped[int] = mapped_column(primary_key=True)
    email: Mapped[str] = mapped_column(String(255), unique=True, index=True)
    full_name: Mapped[str] = mapped_column(String(255))
    password_hash: Mapped[str] = mapped_column(String(255))

class UserRepository:
    def __init__(self, session):
        self.session = session

    def create(self, user: User) -> User:
        self.session.add(user)
        self.session.commit()
        self.session.refresh(user)
        return user

    def get_by_id(self, user_id: int) -> User | None:
        return self.session.get(User, user_id)

    def get_by_email(self, email: str) -> User | None:
        stmt = select(User).where(User.email == email)
        return self.session.scalar(stmt)

    def list(self, limit: int = 100, offset: int = 0) -> list[User]:
        stmt = select(User).limit(limit).offset(offset)
        return list(self.session.scalars(stmt).all())

    def update(self, user: User) -> User:
        self.session.add(user)
        self.session.commit()
        self.session.refresh(user)
        return user

    def delete(self, user: User) -> None:
        self.session.delete(user)
        self.session.commit()
```

---

### Validation

Use Pydantic models for request/response validation.

```python
from pydantic import BaseModel, EmailStr, Field

class UserCreate(BaseModel):
    email: EmailStr
    full_name: str = Field(min_length=1)
    password: str = Field(min_length=8)

class UserUpdate(BaseModel):
    full_name: str | None = Field(default=None, min_length=1)
    password: str | None = Field(default=None, min_length=8)

class UserOut(BaseModel):
    id: int
    email: EmailStr
    full_name: str
    class Config:
        from_attributes = True  # allow ORM -> DTO
```

---

### Business Logic / Middleware / API

Service layer enforces rules; API layer handles HTTP; middleware decorates requests.

```python
# Service layer with Strategy for hashing
class UserService:
    def __init__(self, session=None, hasher: PasswordHasher | None = None) -> None:
        self.session = session or DatabaseSessionFactory.get_session()
        self.repo = UserRepository(self.session)
        self.hasher = hasher or BCryptHasher()

    def create_user(self, payload: UserCreate) -> User:
        if self.repo.get_by_email(payload.email):
            raise ValueError("Email already registered")
        user = User(
            email=payload.email,
            full_name=payload.full_name,
            password_hash=self.hasher.hash(payload.password),
        )
        return self.repo.create(user)

    def update_user(self, user_id: int, payload: UserUpdate) -> User:
        user = self.repo.get_by_id(user_id)
        if not user:
            raise LookupError("User not found")
        if payload.full_name is not None:
            user.full_name = payload.full_name
        if payload.password is not None:
            user.password_hash = self.hasher.hash(payload.password)
        return self.repo.update(user)
```

```python
# FastAPI API and middleware
from fastapi import FastAPI, APIRouter, HTTPException, Query, Request, Response
import time

app = FastAPI(title="Lift Project API")
router = APIRouter(prefix="/api/users", tags=["users"])

@app.middleware("http")
async def timing_middleware(request: Request, call_next):
    start = time.perf_counter()
    response: Response = await call_next(request)
    response.headers["X-Process-Time-ms"] = f"{(time.perf_counter() - start) * 1000:.2f}"
    return response

@router.post("/", response_model=UserOut, status_code=201)
def create_user(payload: UserCreate) -> UserOut:
    try:
        user = UserService().create_user(payload)
        return UserOut.model_validate(user)
    except ValueError as e:
        raise HTTPException(status_code=400, detail=str(e))

@router.get("/{user_id}", response_model=UserOut)
def get_user(user_id: int) -> UserOut:
    user = UserService().repo.get_by_id(user_id)
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    return UserOut.model_validate(user)

@router.get("/", response_model=list[UserOut])
def list_users(limit: int = Query(100, ge=1, le=500), offset: int = Query(0, ge=0)) -> list[UserOut]:
    users = UserService().repo.list(limit=limit, offset=offset)
    return [UserOut.model_validate(u) for u in users]

@router.put("/{user_id}", response_model=UserOut)
def update_user(user_id: int, payload: UserUpdate) -> UserOut:
    try:
        user = UserService().update_user(user_id, payload)
        return UserOut.model_validate(user)
    except LookupError:
        raise HTTPException(status_code=404, detail="User not found")

@router.delete("/{user_id}", status_code=204)
def delete_user(user_id: int) -> None:
    svc = UserService()
    user = svc.repo.get_by_id(user_id)
    if not user:
        raise HTTPException(status_code=404, detail="User not found")
    svc.repo.delete(user)
    return None

app.include_router(router)
```

---

### Data Flow

- Client sends HTTP request to API endpoint
- FastAPI parses JSON → validates using Pydantic schema
- Middleware runs (e.g., timing, logging, auth)
- Controller calls Service with validated data
- Service applies business rules (uniqueness, hashing via Strategy)
- Service uses Repository to query/update DB via SQLAlchemy Session
- Repository performs CRUD on ORM models
- Service returns domain object(s)
- Controller converts to response schema (DTO) and returns JSON

Simple sequence diagram (textual):

```
Client -> API (FastAPI) -> Middleware -> Controller/Route
Controller -> Service -> Repository -> DB
Repository -> Service -> Controller -> Response JSON -> Client
```

---

### Minimal Dependencies (if you want to run snippets)

```text
fastapi
uvicorn
SQLAlchemy
pydantic
passlib[bcrypt]
```



