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
	internal class DoorClosedState : ILiftState
	{
		public void MovingUp(Lift lift)
		{
			// Change to moving up state
			lift.SetState(new MovingUpState());
			lift.LiftTimer.Start();
		}

		public void MovingDown(Lift lift)
		{
			// Change to moving down state
			lift.SetState(new MovingDownState());
			lift.LiftTimerDown.Start();
		}

		public void OpenDoor(Lift lift)
		{
			// Change to opening state
			lift.SetState(new DoorOpeningState());
			// Start door opening animation
			if (lift.IsAtTop() && lift.DoorTimerTop != null)
			{
				lift.DoorTimerTop.Start();
			}
			else if (lift.IsAtBottom() && lift.DoorTimer != null)
			{
				lift.DoorTimer.Start();
			}
		}

		public void CloseDoor(Lift lift)
		{
			// Already closed, do nothing
		}
	}
}
