using Lift_Project.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lift_Project.stateInterface
{
    // Interface Defines the common methods for all states (ILiftState)
    internal interface ILiftState
	{
		void MovingUp(Lift lift);
		void MovingDown(Lift lift);
		void OpenDoor(Lift lift);
		void CloseDoor(Lift lift);
	}
}
