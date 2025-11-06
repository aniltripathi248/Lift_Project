using Lift_Project;
using Lift_Project.concreteClass;
using Lift_Project.Context;
using System.Data;

namespace Lift_Project
{
    public partial class Form1 : Form
    {

        // Removed old boolean flags - now using state pattern
        int doorOpenMaxWidth;
        int doorSpped = 10;
        int liftSpeed = 20;

        DataTable dt = new DataTable();

        DBContext dBContext = new DBContext();

        // Pending travel requests queued while doors are closing
        bool pendingMoveUp = false;
        bool pendingMoveDown = false;

        private Lift lift;
        public Form1()
        {
            InitializeComponent();

            lift = new Lift(btnLift, liftSpeed, this.ClientSize.Height, liftTimer, liftTimerDown);

            doorOpenMaxWidth = btnLift.Width / 2 - 10;

            // Set door properties in lift context
            lift.SetDoorProperties(leftDoor, rightDoor, topleftdoor, toprightdoor,
                doorTimer, doorTimerUp, doorSpped, doorOpenMaxWidth, this);

            dataGridViewLogs.ColumnCount = 2;
            dataGridViewLogs.Columns[0].Name = "Time";
            dataGridViewLogs.Columns[1].Name = "Events";

            dt.Columns.Add("LogTime");
            dt.Columns.Add("EventDescription");

        }

        private void logEvents(string message)
        {
            string currentTime = DateTime.Now.ToString("hh:mm:ss");

            dt.Rows.Add(currentTime, message);

            dataGridViewLogs.Rows.Add(currentTime, message);

            dBContext.InsertLogsIntoDB(dt);
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnUp_Click(object sender, EventArgs e)
        {
            // Use state pattern - state will handle door closing if needed
            lift.MovingUp();

            // If state allows movement, it will start the timer
            if (lift._CurrentState is MovingUpState)
            {
                logEvents("Lift Mathi Jadai Xa!!!");
            }
            else
            {
                logEvents("Lift cannot move - doors must be closed first!");
            }
        }

        private void btnDown_Click(object sender, EventArgs e)
        {
            // Use state pattern - state will handle door closing if needed
            lift.MovingDown();

            // If state allows movement, it will start the timer
            if (lift._CurrentState is MovingDownState)
            {
                logEvents("Lift Tala Jadai Xa!!!");
            }
            else
            {
                logEvents("Lift cannot move - doors must be closed first!");
            }
        }

        private void liftTimer_Tick(object sender, EventArgs e)
        {
            lift.MovingUp();

            //if (isMovingUp)
            //{
            //	if (btnLift.Top > 0)
            //	{
            //		btnLift.Top -= liftSpeed;
            //	}
            //	else
            //	{
            //		liftTimer.Stop();
            //	}
            //}

            //if (isMovingDown)
            //{
            //	if (btnLift.Bottom < this.ClientSize.Height)
            //	{
            //		btnLift.Top += liftSpeed;
            //	}
            //	else
            //	{
            //		liftTimer.Stop();
            //	}
            //}
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            // Smart door open button - works at both top and bottom
            if (!lift.IsAtTop() && !lift.IsAtBottom())
            {
                logEvents("Cannot open doors - lift is not at any floor!");
                return;
            }

            // Use state pattern for door opening
            lift.OpenDoor();

            // Check if we're in a state that allows opening
            if (lift._CurrentState is DoorOpeningState || lift._CurrentState is DoorOpenState)
            {
                if (lift.IsAtTop())
                {
                    // Open top doors
                    if (doorTimerUp != null)
                    {
                        doorTimerUp.Start();
                        logEvents("UP Dhoka Khuldai Xa!!!");
                    }
                }
                else if (lift.IsAtBottom())
                {
                    // Open bottom doors
                    if (doorTimer != null)
                    {
                        doorTimer.Start();
                        logEvents("Dhoka Khuldai Xa!!!");
                    }
                }
            }
            else
            {
                logEvents("Cannot open doors - lift is moving!");
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            // Smart door close button - works at both top and bottom
            if (!lift.IsAtTop() && !lift.IsAtBottom())
            {
                logEvents("Cannot close doors - lift is not at any floor!");
                return;
            }

            // Use state pattern for door closing
            lift.CloseDoor();

            // Check if we're in a state that allows closing
            if (lift._CurrentState is DoorClosingState)
            {
                if (lift.IsAtTop())
                {
                    // Close top doors
                    if (doorTimerUp != null)
                    {
                        doorTimerUp.Start();
                        logEvents("UP Dhoka Banda Hudai Xa!!!");
                    }
                }
                else if (lift.IsAtBottom())
                {
                    // Close bottom doors
                    if (doorTimer != null)
                    {
                        doorTimer.Start();
                        logEvents("Dhoka Banda Hudai Xa!!!");
                    }
                }
            }
            else
            {
                logEvents("Cannot close doors - lift is moving or doors are already closed!");
            }
        }

        private void doorTimer_Tick(Object sender, EventArgs e)
        {
            // Use state pattern to handle door animation
            if (lift._CurrentState is DoorOpeningState)
            {
                DoorOpeningState.HandleDoorOpening(lift);
            }
            else if (lift._CurrentState is DoorClosingState)
            {
                DoorClosingState.HandleDoorClosing(lift);

                // After closing, kick off any pending movement requests from bottom
                if (lift._CurrentState is DoorClosedState)
                {
                    if (pendingMoveUp)
                    {
                        pendingMoveUp = false;
                        lift.MovingUp();
                        if (lift._CurrentState is MovingUpState)
                        {
                            logEvents("Lift Mathi Jadai Xa!!!");
                        }
                    }
                    else if (pendingMoveDown)
                    {
                        pendingMoveDown = false;
                        lift.MovingDown();
                        if (lift._CurrentState is MovingDownState)
                        {
                            logEvents("Lift Tala Jadai Xa!!!");
                        }
                    }
                }
            }
            else
            {
                doorTimer.Stop();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dBContext.loadLogsFromDB(dt, dataGridViewLogs);
        }

        private void liftTimerDown_Tick(object sender, EventArgs e)
        {
            lift.MovingDown();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Open_Up_Click(object sender, EventArgs e)
        {
            // Top door open button - only works when lift is at top
            if (!lift.IsAtTop())
            {
                logEvents("Cannot open top doors - lift is not at top floor!");
                return;
            }

            // Use state pattern for door opening
            lift.OpenDoor();

            // Check if we're in a state that allows opening
            if (lift._CurrentState is DoorOpeningState || lift._CurrentState is DoorOpenState)
            {
                if (doorTimerUp != null)
                {
                    doorTimerUp.Start();
                    logEvents("UP Dhoka Khuldai Xa!!!");
                }
            }
            else
            {
                logEvents("Cannot open doors - lift is moving!");
            }
        }

        private void Close_Up_Click(object sender, EventArgs e)
        {
            // Top door close button - only works when lift is at top
            if (!lift.IsAtTop())
            {
                logEvents("Cannot close top doors - lift is not at top floor!");
                return;
            }

            // Use state pattern for door closing
            lift.CloseDoor();

            // Check if we're in a state that allows closing
            if (lift._CurrentState is DoorClosingState)
            {
                if (doorTimerUp != null)
                {
                    doorTimerUp.Start();
                    logEvents("UP Dhoka Banda Hudai Xa!!!");
                }
            }
            else
            {
                logEvents("Cannot close doors - lift is moving or doors are already closed!");
            }
        }

        private void doorTimerUp_Tick(object sender, EventArgs e)
        {
            // Use state pattern to handle door animation for top doors
            if (lift._CurrentState is DoorOpeningState)
            {
                DoorOpeningState.HandleDoorOpening(lift);
            }
            else if (lift._CurrentState is DoorClosingState)
            {
                DoorClosingState.HandleDoorClosing(lift);

                // After closing, kick off any pending movement requests from top
                if (lift._CurrentState is DoorClosedState)
                {
                    if (pendingMoveDown)
                    {
                        pendingMoveDown = false;
                        lift.MovingDown();
                        if (lift._CurrentState is MovingDownState)
                        {
                            logEvents("Lift Tala Jadai Xa!!!");
                        }
                    }
                    else if (pendingMoveUp)
                    {
                        pendingMoveUp = false;
                        lift.MovingUp();
                        if (lift._CurrentState is MovingUpState)
                        {
                            logEvents("Lift Mathi Jadai Xa!!!");
                        }
                    }
                }
            }
            else
            {
                doorTimerUp.Stop();
            }
        }

        private void topleftdoor_Click(object sender, EventArgs e)
        {

        }

        // Added to satisfy designer reference if present
        private void button2_Click(object sender, EventArgs e)
        {
            // No-op: legacy event handler placeholder
        }

        private void top_lift_call_Click(object sender, EventArgs e)
        {
            // If already at top, just open doors
            if (lift.IsAtTop())
            {
                lift.OpenDoor();
                if (lift._CurrentState is DoorOpeningState || lift._CurrentState is DoorOpenState)
                {
                    if (doorTimerUp != null) doorTimerUp.Start();
                    logEvents("UP Dhoka Khuldai Xa!!!");
                }
                return;
            }

            // We are at bottom (or in-between) → request move up
            if (lift._CurrentState is DoorOpenState || lift._CurrentState is DoorOpeningState)
            {
                pendingMoveUp = true;
                lift.CloseDoor();
                if (doorTimer != null) doorTimer.Start();
                logEvents("Dhoka Banda Hudai Xa!!!");
            }
            else if (lift._CurrentState is DoorClosedState)
            {
                lift.MovingUp();
                if (lift._CurrentState is MovingUpState)
                {
                    logEvents("Lift Mathi Jadai Xa!!!");
                }
            }
            else if (lift._CurrentState is DoorClosingState)
            {
                pendingMoveUp = true; // will start after close completes
            }
        }

        private void bottom_lift_call_Click(object sender, EventArgs e)
        {
            // If already at bottom, just open doors
            if (lift.IsAtBottom())
            {
                lift.OpenDoor();
                if (lift._CurrentState is DoorOpeningState || lift._CurrentState is DoorOpenState)
                {
                    if (doorTimer != null) doorTimer.Start();
                    logEvents("Dhoka Khuldai Xa!!!");
                }
                return;
            }

            // We are at top (or in-between) → request move down
            if (lift._CurrentState is DoorOpenState || lift._CurrentState is DoorOpeningState)
            {
                pendingMoveDown = true;
                lift.CloseDoor();
                if (doorTimerUp != null) doorTimerUp.Start();
                logEvents("UP Dhoka Banda Hudai Xa!!!");
            }
            else if (lift._CurrentState is DoorClosedState)
            {
                lift.MovingDown();
                if (lift._CurrentState is MovingDownState)
                {
                    logEvents("Lift Tala Jadai Xa!!!");
                }
            }
            else if (lift._CurrentState is DoorClosingState)
            {
                pendingMoveDown = true; // will start after close completes
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
