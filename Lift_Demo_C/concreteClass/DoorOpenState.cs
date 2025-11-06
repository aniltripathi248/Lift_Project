using Lift_Project.stateInterface;
using Lift_Project.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//Liskov Substitution Principle If class B inherits from class A, you should be able to use B anywhere A is expected — without breaking behavior.

namespace Lift_Project.concreteClass
{
	internal class DoorOpenState : ILiftState
	{
		public void MovingUp(Lift lift)
		{
			// Cannot move while doors are open - close doors first
			lift.SetState(new DoorClosingState());
		}

		public void MovingDown(Lift lift)
		{
			// Cannot move while doors are open - close doors first
			lift.SetState(new DoorClosingState());
		}

		public void OpenDoor(Lift lift)
		{
			// Already open, do nothing
		}

		public void CloseDoor(Lift lift)
		{
			// Change to closing state
			lift.SetState(new DoorClosingState());
		}
	}
}

