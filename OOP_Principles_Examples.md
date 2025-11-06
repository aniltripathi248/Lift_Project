# OOP Principles in Lift Project

This document demonstrates the five core OOP principles found in the Lift Project codebase.

---

## 1. **ENCAPSULATION** 
**Definition**: Binding data and methods together, hiding internal implementation details while providing controlled access through public interfaces.

### Example 1: Lift Class (Context/Lift.cs)
```csharp
internal class Lift
{
    // Private/Internal fields - data is encapsulated
    public ILiftState _CurrentState;  // State is protected
    public Button MainElevator;
    public int LiftSpeed; 
    public int FormSize;
    
    // Public methods provide controlled access to internal state
    public void SetState(ILiftState state)
    {
        _CurrentState = state;  // Controlled state change
    }
    
    // Encapsulated behaviors - internal logic hidden
    public void MovingUp()
    {
        _CurrentState.MovingUp(this);  // Delegates to state
    }
    
    public bool IsAtTop()
    {
        return MainElevator.Top <= 50;  // Internal logic hidden
    }
}
```
**Explanation**: The `Lift` class encapsulates its internal state and behavior. Users interact through public methods (`MovingUp()`, `MovingDown()`, `OpenDoor()`, `CloseDoor()`) without knowing the internal state management details.

### Example 2: DBContext Class (DBContext.cs)
```csharp
public class DBContext
{
    // Private field - connection string is encapsulated
    string _connectionString = @"Server=AJAY; Database= liftDemo; ...";
    
    // Public methods provide access to database operations
    public void InsertLogsIntoDB(DataTable dt)
    {
        // Internal implementation hidden - users don't need to know
        // about SqlConnection, SqlCommand details
    }
}
```
**Explanation**: Database connection details are hidden. Users only need to call `InsertLogsIntoDB()` without knowing connection string or SQL implementation.

---

## 2. **INHERITANCE**
**Definition**: Creating a new class (child) from an existing class (parent), inheriting its properties and methods.

### Example 1: State Classes Inheriting from ILiftState Interface
```csharp
// Parent Interface (Abstraction)
internal interface ILiftState
{
    void MovingUp(Lift lift);
    void MovingDown(Lift lift);
    void OpenDoor(Lift lift);
    void CloseDoor(Lift lift);
}

// Child Classes (Inheritance)
internal class MovingUpState : ILiftState  // Inherits interface
{
    public void MovingUp(Lift lift) { /* Implementation */ }
    public void MovingDown(Lift lift) { /* Implementation */ }
    public void OpenDoor(Lift lift) { /* Implementation */ }
    public void CloseDoor(Lift lift) { /* Implementation */ }
}

internal class MovingDownState : ILiftState  // Inherits interface
{
    public void MovingUp(Lift lift) { /* Implementation */ }
    public void MovingDown(Lift lift) { /* Implementation */ }
    // ... different implementation
}

internal class DoorOpenState : ILiftState  // Inherits interface
{
    // ... different implementation
}
```

**All State Classes Inherit from ILiftState:**
- `IdleState : ILiftState`
- `MovingUpState : ILiftState`
- `MovingDownState : ILiftState`
- `DoorOpenState : ILiftState`
- `DoorClosedState : ILiftState`
- `DoorOpeningState : ILiftState`
- `DoorClosingState : ILiftState`

### Example 2: Form1 Inheriting from Form Class
```csharp
public partial class Form1 : Form  // Inherits from Windows Forms Form class
{
    // Inherits all Form properties and methods (Size, Location, Show(), etc.)
}
```
**Explanation**: `Form1` inherits from the base `Form` class, getting all Windows Forms functionality (windowing, events, etc.).

---

## 3. **ABSTRACTION**
**Definition**: Showing only essential details while hiding implementation complexity.

### Example 1: ILiftState Interface (Abstraction)
```csharp
// Abstraction - defines WHAT can be done, not HOW
internal interface ILiftState
{
    void MovingUp(Lift lift);      // What: can move up
    void MovingDown(Lift lift);    // What: can move down
    void OpenDoor(Lift lift);      // What: can open door
    void CloseDoor(Lift lift);     // What: can close door
}
```
**Explanation**: The interface abstracts the lift behavior. It defines what operations are possible without specifying how each state implements them.

### Example 2: Lift Class Methods (Abstraction)
```csharp
public class Lift
{
    // Abstract interface - users don't need to know about state management
    public void MovingUp()
    {
        _CurrentState.MovingUp(this);  // Complexity hidden
    }
    
    public void OpenDoor()
    {
        _CurrentState.OpenDoor(this);  // State transitions hidden
    }
}
```
**Explanation**: The `Lift` class provides a simple interface. Users call `MovingUp()` without knowing about state transitions, timers, or animation logic.

---

## 4. **POLYMORPHISM**
**Definition**: Same interface, different implementations. The ability to process objects differently based on their data type or class.

### Example 1: Runtime Polymorphism via State Pattern
```csharp
// Same interface call, different behavior based on current state
public class Lift
{
    public ILiftState _CurrentState;  // Can hold any state implementation
    
    public void MovingUp()
    {
        _CurrentState.MovingUp(this);  // Polymorphic call
    }
}

// Different implementations:
// When _CurrentState = MovingUpState:
_CurrentState.MovingUp(this);  // Actually moves the lift up

// When _CurrentState = DoorOpenState:
_CurrentState.MovingUp(this);  // Changes to DoorClosingState instead

// When _CurrentState = MovingDownState:
_CurrentState.MovingUp(this);  // Does nothing (can't change direction)
```

### Example 2: Different State Implementations
```csharp
// MovingUpState implementation
internal class MovingUpState : ILiftState
{
    public void MovingUp(Lift lift)
    {
        // Actually moves lift up
        if (lift.MainElevator.Top > 0)
        {
            lift.MainElevator.Top -= lift.LiftSpeed;
        }
    }
}

// DoorOpenState implementation
internal class DoorOpenState : ILiftState
{
    public void MovingUp(Lift lift)
    {
        // Different behavior - closes doors first
        lift.SetState(new DoorClosingState());
    }
}

// MovingDownState implementation
internal class MovingDownState : ILiftState
{
    public void MovingUp(Lift lift)
    {
        // Different behavior - does nothing
        /* Do Nothing - Cannot change direction while moving */
    }
}
```
**Explanation**: All states implement the same `MovingUp()` method, but each behaves differently based on the current state. This is polymorphism in action.

### Example 3: Form1 Usage (Polymorphism)
```csharp
// In Form1.cs - same method call, different state behaviors
private void btnUp_Click(object sender, EventArgs e)
{
    lift.MovingUp();  // Polymorphic - behavior depends on current state
    
    // Check state type to determine what happened
    if (lift._CurrentState is MovingUpState)
    {
        logEvents("Lift Mathi Jadai Xa!!!");
    }
    else
    {
        logEvents("Lift cannot move - doors must be closed first!");
    }
}
```

---

## 5. **COMPOSITION**
**Definition**: Building complex objects by combining simpler objects. "Has-a" relationship.

### Example 1: Lift Class Composition
```csharp
public class Lift
{
    // Composition - Lift "has-a" Button (MainElevator)
    public Button MainElevator;
    
    // Composition - Lift "has-a" Timer objects
    public System.Windows.Forms.Timer LiftTimer;
    public System.Windows.Forms.Timer LiftTimerDown;
    
    // Composition - Lift "has-a" Door Buttons
    public Button? LeftDoorBottom;
    public Button? RightDoorBottom;
    public Button? LeftDoorTop;
    public Button? RightDoorTop;
    
    // Composition - Lift "has-a" Door Timers
    public System.Windows.Forms.Timer? DoorTimer;
    public System.Windows.Forms.Timer? DoorTimerTop;
    
    // Composition - Lift "has-a" State (ILiftState)
    public ILiftState _CurrentState;
}
```
**Explanation**: The `Lift` class is composed of multiple objects (Buttons, Timers, State) that work together to create the lift functionality.

### Example 2: Form1 Class Composition
```csharp
public partial class Form1 : Form
{
    // Composition - Form1 "has-a" Lift object
    private Lift lift;
    
    // Composition - Form1 "has-a" DBContext object
    DBContext dBContext = new DBContext();
    
    // Composition - Form1 "has-a" DataTable
    DataTable dt = new DataTable();
    
    public Form1()
    {
        InitializeComponent();
        // Creating Lift object - composition
        lift = new Lift(btnLift, liftSpeed, this.ClientSize.Height, 
                       liftTimer, liftTimerDown);
    }
}
```
**Explanation**: `Form1` is composed of a `Lift` object, `DBContext` object, and `DataTable`. These are separate objects that Form1 uses to build its functionality.

### Example 3: State Objects Creating Other States (Composition)
```csharp
internal class DoorClosedState : ILiftState
{
    public void MovingUp(Lift lift)
    {
        // Creates new state object - composition
        lift.SetState(new MovingUpState());  // "has-a" MovingUpState
        lift.LiftTimer.Start();
    }
}

internal class MovingUpState : ILiftState
{
    public void MovingUp(Lift lift)
    {
        // Creates new state object - composition
        lift.SetState(new DoorOpeningState());  // "has-a" DoorOpeningState
    }
}
```
**Explanation**: States create and transition to other state objects, demonstrating composition in the state pattern.

---

## Summary Table

| OOP Principle | Location | Example |
|--------------|----------|---------|
| **Encapsulation** | `Lift.cs`, `DBContext.cs` | Private fields accessed through public methods |
| **Inheritance** | All state classes | `MovingUpState : ILiftState`, `Form1 : Form` |
| **Abstraction** | `ILiftState` interface | Interface defines what, not how |
| **Polymorphism** | State classes | Same method, different implementations |
| **Composition** | `Lift.cs`, `Form1.cs` | Lift has Button, Timer, State objects |

---

## Design Pattern Used
This project uses the **State Design Pattern**, which perfectly demonstrates all OOP principles:
- **Encapsulation**: State transitions are encapsulated in the Lift class
- **Inheritance**: All states inherit from ILiftState interface
- **Abstraction**: ILiftState abstracts lift behavior
- **Polymorphism**: Different states provide different implementations
- **Composition**: Lift is composed of state objects and UI components

