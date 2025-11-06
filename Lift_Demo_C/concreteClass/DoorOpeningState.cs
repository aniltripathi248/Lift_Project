using Lift_Project.concreteClass;
using Lift_Project.stateInterface;
using Lift_Project.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Lift_Project.concreteClass
{
	internal class DoorOpeningState : ILiftState
	{
		public void MovingUp(Lift lift)
		{
			// Cannot move while doors are opening - close doors first
		}

		public void MovingDown(Lift lift)
		{
			// Cannot move while doors are opening - close doors first
		}

		public void OpenDoor(Lift lift)
		{
			// Already opening, do nothing or continue opening
			// Continue the opening animation via timer
		}

		public void CloseDoor(Lift lift)
		{
			// Change to closing state (reverse direction)
			lift.SetState(new DoorClosingState());
		}

		// Method to handle door opening animation - called from Form timer
		public static void HandleDoorOpening(Lift lift)
		{
			bool isTopDoor = lift.IsAtTop();
			Button? leftDoor = isTopDoor ? lift.LeftDoorTop : lift.LeftDoorBottom;
			Button? rightDoor = isTopDoor ? lift.RightDoorTop : lift.RightDoorBottom;
			System.Windows.Forms.Timer? doorTimer = isTopDoor ? lift.DoorTimerTop : lift.DoorTimer;

			if (leftDoor != null && rightDoor != null && doorTimer != null)
			{
				int targetLeft = lift.MainElevator.Left - lift.DoorOpenMaxWidth;
				if (leftDoor.Left > targetLeft)
				{
					leftDoor.Left -= lift.DoorSpeed;
					rightDoor.Left += lift.DoorSpeed;
				}
				else
				{
					// Doors fully open - ensure exact position and transition to DoorOpenState
					leftDoor.Left = targetLeft;
					// Right door should be at the right edge of the elevator
					rightDoor.Left = lift.MainElevator.Left + lift.MainElevator.Width / 2 + lift.DoorOpenMaxWidth;
					doorTimer.Stop();
					lift.SetState(new DoorOpenState());
				}
			}
		}
	}
}

