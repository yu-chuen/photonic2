namespace GT_App
{
	partial class UserCtrl_RobotStation_Goto
	{
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btn_GotoPos_RobotStation = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_GotoPos_RobotStation
            // 
            this.btn_GotoPos_RobotStation.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_GotoPos_RobotStation.Location = new System.Drawing.Point(3, 3);
            this.btn_GotoPos_RobotStation.Name = "btn_GotoPos_RobotStation";
            this.btn_GotoPos_RobotStation.Size = new System.Drawing.Size(350, 50);
            this.btn_GotoPos_RobotStation.TabIndex = 2;
            this.btn_GotoPos_RobotStation.Text = "...";
            this.btn_GotoPos_RobotStation.UseVisualStyleBackColor = true;
            this.btn_GotoPos_RobotStation.Click += new System.EventHandler(this.btn_GotoPos_RobotStation_Click);
            // 
            // UserCtrl_RobotStation_Goto
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_GotoPos_RobotStation);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_RobotStation_Goto";
            this.Size = new System.Drawing.Size(356, 56);
            this.Load += new System.EventHandler(this.UserCtrl_RobotStation_Goto_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_GotoPos_RobotStation;
	}
}
