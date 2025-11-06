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
	internal class IdleState : ILiftState
	{
		public void MovingDown(Lift lift)
		{
			// IdleState is similar to DoorClosedState - doors are closed, can move
			lift.SetState(new MovingDownState());
			lift.LiftTimerDown.Start();
		

		public void MovingUp(Lift lift)
		{
			// IdleState is similar to DoorClosedState - doors are closed, can move
			lift.SetState(new MovingUpState());
			lift.LiftTimer.Start();
		}

		public void OpenDoor(Lift lift)
		{
			// Can open doors when idle
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
			// Already idle (doors closed), do nothing
		}
	}
}
