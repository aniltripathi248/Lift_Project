using Lift_Project.concreteClass;
using Lift_Project.stateInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lift_Project.Context
{
    internal class Lift
    {
        //Context Maintains current state and delegates actions(Lift)
        // Current state of the lift (follows State Design Pattern)
        public ILiftState _CurrentState;

        // Represents the main elevator button control on the form
        public Button MainElevator;
        public int LiftSpeed; 
        public int FormSize; // Total size of form (used for top/bottom boundary check)
        public System.Windows.Forms.Timer LiftTimer; 
        public System.Windows.Forms.Timer LiftTimerDown; 

        // Door properties (buttons representing door panels)
        public Button? LeftDoorBottom;
        public Button? RightDoorBottom;
        public Button? LeftDoorTop;
        public Button? RightDoorTop;
        public System.Windows.Forms.Timer? DoorTimer; // Timer for bottom floor door
        public System.Windows.Forms.Timer? DoorTimerTop; // Timer for top floor door
        public int DoorSpeed; 
        public int DoorOpenMaxWidth; 
        public Form? Form; // Reference to parent form

        // Constructor initializes main elevator and sets default state
        public Lift(Button mainElevator, int liftSpeed, int formSize, System.Windows.Forms.Timer liftTimer, System.Windows.Forms.Timer liftTimerDown)
        {
            MainElevator = mainElevator;
            LiftSpeed = liftSpeed;
            FormSize = formSize;
            LiftTimer = liftTimer;
            _CurrentState = new DoorClosedState(); // Start with doors closed (ready state)
            LiftTimerDown = liftTimerDown;
        }

        // Sets door controls and related properties for both floors
        public void SetDoorProperties(Button leftDoorBottom, Button rightDoorBottom, Button leftDoorTop, Button rightDoorTop,
            System.Windows.Forms.Timer doorTimer, System.Windows.Forms.Timer doorTimerTop, int doorSpeed, int doorOpenMaxWidth, Form form)
        {
            LeftDoorBottom = leftDoorBottom;
            RightDoorBottom = rightDoorBottom;
            LeftDoorTop = leftDoorTop;
            RightDoorTop = rightDoorTop;
            DoorTimer = doorTimer;
            DoorTimerTop = doorTimerTop;
            DoorSpeed = doorSpeed;
            DoorOpenMaxWidth = doorOpenMaxWidth;
            Form = form;
        }

        // Changes the current lift state
        public void SetState(ILiftState state)
        {
            _CurrentState = state;
        }

        // Delegates the "move up" action to the current state
        public void MovingUp()
        {
            _CurrentState.MovingUp(this);
        }

        public void MovingDown()
        {
            _CurrentState.MovingDown(this);
        }

        public void OpenDoor()
        {
            _CurrentState.OpenDoor(this);
        }

        public void CloseDoor()
        {
            _CurrentState.CloseDoor(this);
        }

        // Checks if the elevator is near the top of the form
        public bool IsAtTop()
        {
            return MainElevator.Top <= 50; 
        }

        // Checks if the elevator is near the bottom of the form
        public bool IsAtBottom()
        {
            return MainElevator.Bottom >= FormSize - 50; 
        }
    }
}
