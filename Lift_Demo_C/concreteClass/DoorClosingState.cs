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
	internal class DoorClosingState : ILiftState
	{
		public void MovingUp(Lift lift)
		{
			// Cannot move while doors are closing - wait for doors to close
			// State will transition to DoorClosedState when done, then can move
		}

		public void MovingDown(Lift lift)
		{
			// Cannot move while doors are closing - wait for doors to close
			// State will transition to DoorClosedState when done, then can move
		}

		public void OpenDoor(Lift lift)
		{
			// Change to opening state (reverse direction)
			lift.SetState(new DoorOpeningState());
		}

		public void CloseDoor(Lift lift)
		{
			// Already closing, do nothing or continue closing
			// Continue the closing animation via timer
		}

		// Method to handle door closing animation - called from Form timer
		public static void HandleDoorClosing(Lift lift)
		{
			bool isTopDoor = lift.IsAtTop();
			Button? leftDoor = isTopDoor ? lift.LeftDoorTop : lift.LeftDoorBottom;
			Button? rightDoor = isTopDoor ? lift.RightDoorTop : lift.RightDoorBottom;
			System.Windows.Forms.Timer? doorTimer = isTopDoor ? lift.DoorTimerTop : lift.DoorTimer;

			if (leftDoor != null && rightDoor != null && doorTimer != null)
			{
				int targetRight = lift.MainElevator.Left + lift.MainElevator.Width / 2;
				if (rightDoor.Left > targetRight)
				{
					rightDoor.Left -= lift.DoorSpeed;
					leftDoor.Left += lift.DoorSpeed;
				}
				else
				{
					// Doors fully closed - ensure exact position and transition to DoorClosedState
					rightDoor.Left = targetRight;
					leftDoor.Left = lift.MainElevator.Left;
					doorTimer.Stop();
					lift.SetState(new DoorClosedState());
				}
			}
		}
	}
}

