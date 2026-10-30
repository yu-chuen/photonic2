namespace GT_App
{
	partial class UserCtrl_StationControlDlg
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_SinglStationTrig = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_StationInitialize = new System.Windows.Forms.Button();
            this.btn_StopStationRun = new System.Windows.Forms.Button();
            this.btn_StartStationRun = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_SinglStationTrig);
            this.groupBox1.Controls.Add(this.lbl_Status);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.btn_StationInitialize);
            this.groupBox1.Controls.Add(this.btn_StopStationRun);
            this.groupBox1.Controls.Add(this.btn_StartStationRun);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(278, 213);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Label";
            // 
            // btn_SinglStationTrig
            // 
            this.btn_SinglStationTrig.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_SinglStationTrig.Location = new System.Drawing.Point(186, 23);
            this.btn_SinglStationTrig.Name = "btn_SinglStationTrig";
            this.btn_SinglStationTrig.Size = new System.Drawing.Size(82, 94);
            this.btn_SinglStationTrig.TabIndex = 5;
            this.btn_SinglStationTrig.Text = "Single Station Trigger";
            this.btn_SinglStationTrig.UseVisualStyleBackColor = false;
            this.btn_SinglStationTrig.Visible = false;
            this.btn_SinglStationTrig.Click += new System.EventHandler(this.btn_SinglStationTrig_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.BackColor = System.Drawing.Color.White;
            this.lbl_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Status.Location = new System.Drawing.Point(71, 175);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(197, 27);
            this.lbl_Status.TabIndex = 4;
            this.lbl_Status.Text = "Stop";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 180);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(51, 16);
            this.label1.TabIndex = 3;
            this.label1.Text = "Status";
            // 
            // btn_StationInitialize
            // 
            this.btn_StationInitialize.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_StationInitialize.Location = new System.Drawing.Point(10, 123);
            this.btn_StationInitialize.Name = "btn_StationInitialize";
            this.btn_StationInitialize.Size = new System.Drawing.Size(258, 45);
            this.btn_StationInitialize.TabIndex = 2;
            this.btn_StationInitialize.Text = "Start Initialize";
            this.btn_StationInitialize.UseVisualStyleBackColor = false;
            this.btn_StationInitialize.Click += new System.EventHandler(this.btn_StationInitialize_Click);
            // 
            // btn_StopStationRun
            // 
            this.btn_StopStationRun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_StopStationRun.Location = new System.Drawing.Point(98, 23);
            this.btn_StopStationRun.Name = "btn_StopStationRun";
            this.btn_StopStationRun.Size = new System.Drawing.Size(82, 94);
            this.btn_StopStationRun.TabIndex = 1;
            this.btn_StopStationRun.Text = "Stop Station Run";
            this.btn_StopStationRun.UseVisualStyleBackColor = false;
            this.btn_StopStationRun.Click += new System.EventHandler(this.btn_StopStationRun_Click);
            // 
            // btn_StartStationRun
            // 
            this.btn_StartStationRun.BackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_StartStationRun.Location = new System.Drawing.Point(10, 23);
            this.btn_StartStationRun.Name = "btn_StartStationRun";
            this.btn_StartStationRun.Size = new System.Drawing.Size(82, 94);
            this.btn_StartStationRun.TabIndex = 0;
            this.btn_StartStationRun.Text = "Start Station Run";
            this.btn_StartStationRun.UseVisualStyleBackColor = false;
            this.btn_StartStationRun.Click += new System.EventHandler(this.btn_StartStationRun_Click);
            // 
            // UserCtrl_StationControlDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_StationControlDlg";
            this.Size = new System.Drawing.Size(284, 220);
            this.Load += new System.EventHandler(this.UserCtrl_StationControlDlg_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btn_StationInitialize;
		private System.Windows.Forms.Button btn_StopStationRun;
		private System.Windows.Forms.Button btn_StartStationRun;
		private System.Windows.Forms.Label lbl_Status;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_SinglStationTrig;
	}
}
