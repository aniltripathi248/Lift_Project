using Lift_Project.stateInterface;
using Lift_Project.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift_Project.concreteClass
{
	internal class MovingDownState : ILiftState // Inheritance Reusing code by creating a subclass from a parent class.
    {
		public void MovingDown(Lift lift)
		{
            if (lift.MainElevator.Bottom < lift.FormSize)
			{
				lift.MainElevator.Top += lift.LiftSpeed;
			}
			else
			{

				// Reached bottom - stop moving and automatically open bottom doors
				lift.LiftTimerDown.Stop();
				// Position lift exactly at bottom
				lift.MainElevator.Top = lift.FormSize - lift.MainElevator.Height;
				
				// Transition to door opening state 
				lift.SetState(new DoorOpeningState()); //Object  Instance of a class, representing a specific entity.

                // Start door opening animation for bottom doors
                if (lift.DoorTimer != null && lift.LeftDoorBottom != null && lift.RightDoorBottom != null)
				{
					lift.DoorTimer.Start();
				}
			}
		}
        //Dependency Inversion Principle Depend on abstractions, not concrete classes.
        public void MovingUp(Lift lift)
		{
			/* Do Nothing - Cannot change direction while moving */
		}

		public void OpenDoor(Lift lift)
		{
			// Cannot open doors while moving - doors will open automatically when lift stops
		}

		public void CloseDoor(Lift lift)
		{
			// Cannot close doors while moving
		}
	}
}
