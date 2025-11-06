using Lift_Project.stateInterface;
using Lift_Project.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift_Project.concreteClass
{
	internal class MovingUpState : ILiftState //Abstraction Showing only essential details, hiding complexity.
    {
		public void MovingDown(Lift lift)
		{
			/* Do Nothing - Cannot change direction while moving */
		}

		public void MovingUp(Lift lift)
		{
            // Encapsulation  Hiding internal data; access through public methods/properties.
            if (lift.MainElevator.Top > 0)
			{
				lift.MainElevator.Top -= lift.LiftSpeed;
			}
			else
			{
				// Reached top - stop moving and automatically open top doors
				lift.LiftTimer.Stop();
				lift.MainElevator.Top = 0; // Ensure we're exactly at top
				
				// Transition to door opening state
				lift.SetState(new DoorOpeningState());
				
				// Start door opening animation for top doors
				if (lift.DoorTimerTop != null && lift.LeftDoorTop != null && lift.RightDoorTop != null)
				{
					lift.DoorTimerTop.Start();
				}
			}
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
