namespace GT_App
{
	partial class UserCtrl_Indexer_SettingPage
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_trackbarMainSpeed = new System.Windows.Forms.Label();
            this.trackBar_PrimarySpeed = new System.Windows.Forms.TrackBar();
            this.label18 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.txt_Velocity = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_HomingVelocity = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txt_HomingAcceleration = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb_Acc = new System.Windows.Forms.TextBox();
            this.txtb_Dec = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_TimeOut = new System.Windows.Forms.TextBox();
            this.txt_mmPerRev = new System.Windows.Forms.TextBox();
            this.txt_HomingTimeOut = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.txt_HomeDir = new System.Windows.Forms.TextBox();
            this.txt_PulsePerRev = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txt_HomingMaxVelocity = new System.Windows.Forms.TextBox();
            this.lbl_Unit001 = new System.Windows.Forms.Label();
            this.lbl_Label = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PrimarySpeed)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_trackbarMainSpeed);
            this.groupBox2.Controls.Add(this.trackBar_PrimarySpeed);
            this.groupBox2.Controls.Add(this.label18);
            this.groupBox2.Controls.Add(this.label17);
            this.groupBox2.Location = new System.Drawing.Point(7, 378);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(294, 85);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Speed Settings";
            // 
            // lbl_trackbarMainSpeed
            // 
            this.lbl_trackbarMainSpeed.AutoSize = true;
            this.lbl_trackbarMainSpeed.Location = new System.Drawing.Point(97, 59);
            this.lbl_trackbarMainSpeed.Name = "lbl_trackbarMainSpeed";
            this.lbl_trackbarMainSpeed.Size = new System.Drawing.Size(111, 13);
            this.lbl_trackbarMainSpeed.TabIndex = 22;
            this.lbl_trackbarMainSpeed.Text = "Global Speed Modifier";
            // 
            // trackBar_PrimarySpeed
            // 
            this.trackBar_PrimarySpeed.LargeChange = 1;
            this.trackBar_PrimarySpeed.Location = new System.Drawing.Point(25, 27);
            this.trackBar_PrimarySpeed.Maximum = 100;
            this.trackBar_PrimarySpeed.Name = "trackBar_PrimarySpeed";
            this.trackBar_PrimarySpeed.Size = new System.Drawing.Size(233, 45);
            this.trackBar_PrimarySpeed.TabIndex = 21;
            this.trackBar_PrimarySpeed.TickFrequency = 10;
            this.trackBar_PrimarySpeed.Value = 100;
            this.trackBar_PrimarySpeed.Scroll += new System.EventHandler(this.trackBar_PrimarySpeed_Scroll);
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(256, 30);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(25, 13);
            this.label18.TabIndex = 20;
            this.label18.Text = "100";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(14, 30);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(13, 13);
            this.label17.TabIndex = 19;
            this.label17.Text = "0";
            // 
            // txt_Velocity
            // 
            this.txt_Velocity.Location = new System.Drawing.Point(33, 57);
            this.txt_Velocity.Name = "txt_Velocity";
            this.txt_Velocity.Size = new System.Drawing.Size(100, 20);
            this.txt_Velocity.TabIndex = 19;
            this.txt_Velocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txt_HomingVelocity);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txt_HomingAcceleration);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtb_Acc);
            this.groupBox1.Controls.Add(this.txtb_Dec);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txt_TimeOut);
            this.groupBox1.Controls.Add(this.txt_mmPerRev);
            this.groupBox1.Controls.Add(this.txt_HomingTimeOut);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.label14);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txt_HomeDir);
            this.groupBox1.Controls.Add(this.txt_PulsePerRev);
            this.groupBox1.Controls.Add(this.label13);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.txt_HomingMaxVelocity);
            this.groupBox1.Controls.Add(this.lbl_Unit001);
            this.groupBox1.Location = new System.Drawing.Point(8, 26);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(293, 346);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(135, 113);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(107, 13);
            this.label4.TabIndex = 28;
            this.label4.Text = "Homing Velocity (VO)";
            // 
            // txt_HomingVelocity
            // 
            this.txt_HomingVelocity.Location = new System.Drawing.Point(25, 109);
            this.txt_HomingVelocity.Name = "txt_HomingVelocity";
            this.txt_HomingVelocity.Size = new System.Drawing.Size(100, 20);
            this.txt_HomingVelocity.TabIndex = 27;
            this.txt_HomingVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(136, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(105, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Homing Acceleration";
            // 
            // txt_HomingAcceleration
            // 
            this.txt_HomingAcceleration.Location = new System.Drawing.Point(25, 57);
            this.txt_HomingAcceleration.Name = "txt_HomingAcceleration";
            this.txt_HomingAcceleration.Size = new System.Drawing.Size(100, 20);
            this.txt_HomingAcceleration.TabIndex = 25;
            this.txt_HomingAcceleration.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(135, 190);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Deceleration";
            // 
            // txtb_Acc
            // 
            this.txtb_Acc.Location = new System.Drawing.Point(25, 161);
            this.txtb_Acc.Name = "txtb_Acc";
            this.txtb_Acc.Size = new System.Drawing.Size(100, 20);
            this.txtb_Acc.TabIndex = 21;
            this.txtb_Acc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtb_Dec
            // 
            this.txtb_Dec.Location = new System.Drawing.Point(26, 187);
            this.txtb_Dec.Name = "txtb_Dec";
            this.txtb_Dec.Size = new System.Drawing.Size(100, 20);
            this.txtb_Dec.TabIndex = 23;
            this.txtb_Dec.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(135, 164);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 22;
            this.label1.Text = "Acceleration";
            // 
            // txt_TimeOut
            // 
            this.txt_TimeOut.Location = new System.Drawing.Point(26, 213);
            this.txt_TimeOut.Name = "txt_TimeOut";
            this.txt_TimeOut.Size = new System.Drawing.Size(100, 20);
            this.txt_TimeOut.TabIndex = 4;
            this.txt_TimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_mmPerRev
            // 
            this.txt_mmPerRev.Location = new System.Drawing.Point(26, 291);
            this.txt_mmPerRev.Name = "txt_mmPerRev";
            this.txt_mmPerRev.Size = new System.Drawing.Size(100, 20);
            this.txt_mmPerRev.TabIndex = 1;
            this.txt_mmPerRev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_HomingTimeOut
            // 
            this.txt_HomingTimeOut.Location = new System.Drawing.Point(26, 239);
            this.txt_HomingTimeOut.Name = "txt_HomingTimeOut";
            this.txt_HomingTimeOut.Size = new System.Drawing.Size(100, 20);
            this.txt_HomingTimeOut.TabIndex = 5;
            this.txt_HomingTimeOut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(136, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(44, 13);
            this.label8.TabIndex = 7;
            this.label8.Text = "Velocity";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(135, 138);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(80, 13);
            this.label10.TabIndex = 9;
            this.label10.Text = "Home Direction";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(135, 242);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(89, 13);
            this.label14.TabIndex = 13;
            this.label14.Text = "Homing Time Out";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(136, 87);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(131, 13);
            this.label9.TabIndex = 8;
            this.label9.Text = "Homing Max Velocity (VM)";
            // 
            // txt_HomeDir
            // 
            this.txt_HomeDir.Location = new System.Drawing.Point(25, 135);
            this.txt_HomeDir.Name = "txt_HomeDir";
            this.txt_HomeDir.Size = new System.Drawing.Size(100, 20);
            this.txt_HomeDir.TabIndex = 3;
            this.txt_HomeDir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txt_PulsePerRev
            // 
            this.txt_PulsePerRev.Location = new System.Drawing.Point(26, 265);
            this.txt_PulsePerRev.Name = "txt_PulsePerRev";
            this.txt_PulsePerRev.Size = new System.Drawing.Size(100, 20);
            this.txt_PulsePerRev.TabIndex = 6;
            this.txt_PulsePerRev.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(135, 216);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(50, 13);
            this.label13.TabIndex = 12;
            this.label13.Text = "Time Out";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(135, 268);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(106, 13);
            this.label11.TabIndex = 10;
            this.label11.Text = "Pulse Per Revolution";
            // 
            // txt_HomingMaxVelocity
            // 
            this.txt_HomingMaxVelocity.Location = new System.Drawing.Point(25, 83);
            this.txt_HomingMaxVelocity.Name = "txt_HomingMaxVelocity";
            this.txt_HomingMaxVelocity.Size = new System.Drawing.Size(100, 20);
            this.txt_HomingMaxVelocity.TabIndex = 2;
            this.txt_HomingMaxVelocity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // lbl_Unit001
            // 
            this.lbl_Unit001.AutoSize = true;
            this.lbl_Unit001.Location = new System.Drawing.Point(135, 294);
            this.lbl_Unit001.Name = "lbl_Unit001";
            this.lbl_Unit001.Size = new System.Drawing.Size(96, 13);
            this.lbl_Unit001.TabIndex = 11;
            this.lbl_Unit001.Text = "mm Per Revolution";
            // 
            // lbl_Label
            // 
            this.lbl_Label.BackColor = System.Drawing.SystemColors.Control;
            this.lbl_Label.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Label.Location = new System.Drawing.Point(12, 6);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(170, 18);
            this.lbl_Label.TabIndex = 22;
            // 
            // UserCtrl_Indexer_SettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Label);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txt_Velocity);
            this.Controls.Add(this.groupBox1);
            this.Name = "UserCtrl_Indexer_SettingPage";
            this.Size = new System.Drawing.Size(308, 472);
            this.Load += new System.EventHandler(this.UserCtrl_Indexer_SettingPage_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_PrimarySpeed)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lbl_trackbarMainSpeed;
		private System.Windows.Forms.TrackBar trackBar_PrimarySpeed;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.TextBox txt_Velocity;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtb_Acc;
		private System.Windows.Forms.TextBox txtb_Dec;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_TimeOut;
		private System.Windows.Forms.TextBox txt_mmPerRev;
		private System.Windows.Forms.TextBox txt_HomingTimeOut;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txt_HomeDir;
		private System.Windows.Forms.TextBox txt_PulsePerRev;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox txt_HomingMaxVelocity;
		private System.Windows.Forms.Label lbl_Unit001;
		private System.Windows.Forms.Label lbl_Label;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_HomingVelocity;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txt_HomingAcceleration;
	}
}
