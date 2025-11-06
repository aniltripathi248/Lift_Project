namespace Lift_Project
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnLift = new Button();
            btnUp = new Button();
            btnDown = new Button();
            btnExit = new Button();
            liftTimer = new System.Windows.Forms.Timer(components);
            leftDoor = new Button();
            rightDoor = new Button();
            btnOpen = new Button();
            btnClose = new Button();
            doorTimer = new System.Windows.Forms.Timer(components);
            dataGridViewLogs = new DataGridView();
            liftTimerDown = new System.Windows.Forms.Timer(components);
            topleftdoor = new Button();
            toprightdoor = new Button();
            Close_Up = new Button();
            Open_Up = new Button();
            doorTimerUp = new System.Windows.Forms.Timer(components);
            top_lift_call = new Button();
            bottom_lift_call = new Button();
            button1 = new Button();
            button2 = new Button();
            button3 = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).BeginInit();
            SuspendLayout();
            // 
            // btnLift
            // 
            btnLift.BackgroundImage = (Image)resources.GetObject("btnLift.BackgroundImage");
            btnLift.BackgroundImageLayout = ImageLayout.Stretch;
            btnLift.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLift.Location = new Point(117, 371);
            btnLift.Name = "btnLift";
            btnLift.Size = new Size(223, 220);
            btnLift.TabIndex = 0;
            btnLift.UseVisualStyleBackColor = true;
            // 
            // btnUp
            // 
            btnUp.BackgroundImage = Properties.Resources.firstfloorbutton;
            btnUp.BackgroundImageLayout = ImageLayout.Stretch;
            btnUp.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnUp.Location = new Point(460, 160);
            btnUp.Name = "btnUp";
            btnUp.Size = new Size(69, 62);
            btnUp.TabIndex = 1;
            btnUp.UseVisualStyleBackColor = true;
            btnUp.Click += btnUp_Click;
            // 
            // btnDown
            // 
            btnDown.BackgroundImage = Properties.Resources.Groundfloorbutton;
            btnDown.BackgroundImageLayout = ImageLayout.Stretch;
            btnDown.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnDown.Location = new Point(460, 245);
            btnDown.Name = "btnDown";
            btnDown.Size = new Size(69, 68);
            btnDown.TabIndex = 2;
            btnDown.UseVisualStyleBackColor = true;
            btnDown.Click += btnDown_Click;
            // 
            // btnExit
            // 
            btnExit.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnExit.Location = new Point(460, 470);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(69, 46);
            btnExit.TabIndex = 3;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // liftTimer
            // 
            liftTimer.Tick += liftTimer_Tick;
            // 
            // leftDoor
            // 
            leftDoor.BackgroundImage = (Image)resources.GetObject("leftDoor.BackgroundImage");
            leftDoor.BackgroundImageLayout = ImageLayout.Stretch;
            leftDoor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            leftDoor.Location = new Point(117, 371);
            leftDoor.Name = "leftDoor";
            leftDoor.Size = new Size(112, 218);
            leftDoor.TabIndex = 4;
            leftDoor.UseVisualStyleBackColor = true;
            // 
            // rightDoor
            // 
            rightDoor.BackgroundImage = (Image)resources.GetObject("rightDoor.BackgroundImage");
            rightDoor.BackgroundImageLayout = ImageLayout.Stretch;
            rightDoor.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            rightDoor.Location = new Point(229, 371);
            rightDoor.Name = "rightDoor";
            rightDoor.Size = new Size(112, 220);
            rightDoor.TabIndex = 5;
            rightDoor.UseVisualStyleBackColor = true;
            // 
            // btnOpen
            // 
            btnOpen.BackgroundImage = Properties.Resources.opendoorbutton;
            btnOpen.BackgroundImageLayout = ImageLayout.Stretch;
            btnOpen.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnOpen.Location = new Point(460, 331);
            btnOpen.Name = "btnOpen";
            btnOpen.Size = new Size(69, 58);
            btnOpen.TabIndex = 6;
            btnOpen.UseVisualStyleBackColor = true;
            btnOpen.Click += btnOpen_Click;
            // 
            // btnClose
            // 
            btnClose.BackgroundImage = Properties.Resources.closedoorsbutton;
            btnClose.BackgroundImageLayout = ImageLayout.Stretch;
            btnClose.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnClose.Location = new Point(460, 395);
            btnClose.Name = "btnClose";
            btnClose.Size = new Size(69, 58);
            btnClose.TabIndex = 7;
            btnClose.UseVisualStyleBackColor = true;
            btnClose.Click += btnClose_Click;
            // 
            // doorTimer
            // 
            doorTimer.Tick += doorTimer_Tick;
            // 
            // dataGridViewLogs
            // 
            dataGridViewLogs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewLogs.Dock = DockStyle.Right;
            dataGridViewLogs.Location = new Point(700, 0);
            dataGridViewLogs.Name = "dataGridViewLogs";
            dataGridViewLogs.RowHeadersWidth = 51;
            dataGridViewLogs.Size = new Size(308, 591);
            dataGridViewLogs.TabIndex = 8;
            // 
            // liftTimerDown
            // 
            liftTimerDown.Tick += liftTimerDown_Tick;
            // 
            // topleftdoor
            // 
            topleftdoor.BackgroundImage = Properties.Resources.lift_door_left;
            topleftdoor.BackgroundImageLayout = ImageLayout.Stretch;
            topleftdoor.Location = new Point(117, -1);
            topleftdoor.Name = "topleftdoor";
            topleftdoor.Size = new Size(112, 222);
            topleftdoor.TabIndex = 9;
            topleftdoor.UseVisualStyleBackColor = true;
            topleftdoor.Click += topleftdoor_Click;
            // 
            // toprightdoor
            // 
            toprightdoor.BackgroundImage = Properties.Resources.lift_door_right;
            toprightdoor.BackgroundImageLayout = ImageLayout.Stretch;
            toprightdoor.Location = new Point(229, 0);
            toprightdoor.Name = "toprightdoor";
            toprightdoor.Size = new Size(110, 222);
            toprightdoor.TabIndex = 10;
            toprightdoor.UseVisualStyleBackColor = true;
            // 
            // Close_Up
            // 
            Close_Up.BackgroundImage = Properties.Resources.closedoorsbutton;
            Close_Up.BackgroundImageLayout = ImageLayout.Stretch;
            Close_Up.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Close_Up.Location = new Point(913, 434);
            Close_Up.Name = "Close_Up";
            Close_Up.Size = new Size(69, 58);
            Close_Up.TabIndex = 12;
            Close_Up.UseVisualStyleBackColor = true;
            Close_Up.Click += Close_Up_Click;
            // 
            // Open_Up
            // 
            Open_Up.BackgroundImage = Properties.Resources.opendoorbutton;
            Open_Up.BackgroundImageLayout = ImageLayout.Stretch;
            Open_Up.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            Open_Up.Location = new Point(838, 434);
            Open_Up.Name = "Open_Up";
            Open_Up.Size = new Size(69, 58);
            Open_Up.TabIndex = 11;
            Open_Up.UseVisualStyleBackColor = true;
            Open_Up.Click += Open_Up_Click;
            // 
            // doorTimerUp
            // 
            doorTimerUp.Tick += doorTimerUp_Tick;
            // 
            // top_lift_call
            // 
            top_lift_call.BackgroundImage = Properties.Resources.icon;
            top_lift_call.BackgroundImageLayout = ImageLayout.Stretch;
            top_lift_call.Location = new Point(27, 107);
            top_lift_call.Name = "top_lift_call";
            top_lift_call.Size = new Size(59, 45);
            top_lift_call.TabIndex = 13;
            top_lift_call.Text = "\r\n";
            top_lift_call.UseVisualStyleBackColor = true;
            top_lift_call.Click += top_lift_call_Click;
            // 
            // bottom_lift_call
            // 
            bottom_lift_call.BackgroundImage = Properties.Resources.icon;
            bottom_lift_call.BackgroundImageLayout = ImageLayout.Stretch;
            bottom_lift_call.Location = new Point(27, 495);
            bottom_lift_call.Name = "bottom_lift_call";
            bottom_lift_call.Size = new Size(59, 50);
            bottom_lift_call.TabIndex = 14;
            bottom_lift_call.UseVisualStyleBackColor = true;
            bottom_lift_call.Click += bottom_lift_call_Click;
            // 
            // button1
            // 
            button1.Location = new Point(12, 434);
            button1.Name = "button1";
            button1.Size = new Size(90, 55);
            button1.TabIndex = 15;
            button1.Text = "Ground Floor";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Location = new Point(13, 61);
            button2.Name = "button2";
            button2.Size = new Size(94, 40);
            button2.TabIndex = 16;
            button2.Text = "First Floor";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.BackgroundImage = (Image)resources.GetObject("button3.BackgroundImage");
            button3.Location = new Point(-2, 0);
            button3.Name = "button3";
            button3.Size = new Size(696, 633);
            button3.TabIndex = 17;
            button3.UseVisualStyleBackColor = true;
            button3.Click += button3_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 591);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(bottom_lift_call);
            Controls.Add(top_lift_call);
            Controls.Add(toprightdoor);
            Controls.Add(topleftdoor);
            Controls.Add(dataGridViewLogs);
            Controls.Add(btnClose);
            Controls.Add(btnOpen);
            Controls.Add(rightDoor);
            Controls.Add(leftDoor);
            Controls.Add(btnExit);
            Controls.Add(btnDown);
            Controls.Add(btnUp);
            Controls.Add(btnLift);
            Controls.Add(Close_Up);
            Controls.Add(Open_Up);
            Controls.Add(button3);
            Name = "Form1";
            Text = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewLogs).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private Button btnLift;
		private Button btnUp;
		private Button btnDown;
		private Button btnExit;
		private System.Windows.Forms.Timer liftTimer;
		private Button leftDoor;
		private Button rightDoor;
		private Button btnOpen;
		private Button btnClose;
		private System.Windows.Forms.Timer doorTimer;
		private DataGridView dataGridViewLogs;
		private System.Windows.Forms.Timer liftTimerDown;
        private Button topleftdoor;
        private Button toprightdoor;
        private Button Close_Up;
        private Button Open_Up;
        private System.Windows.Forms.Timer doorTimerUp;
        private Button top_lift_call;
        private Button bottom_lift_call;
        private Button button1;
        private Button button2;
        private Button button3;
    }
}
