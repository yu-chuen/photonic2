namespace GT_App
{
    partial class UserCtrl_Report_PerformanceDialog
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_ResetCounter = new System.Windows.Forms.Button();
            this.lbl_TotalUnitFailed = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.lbl_TotalUnitPassed = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.lbl_TotalUnitCount = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lbl_DownTime = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_RunTime = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_StartTime = new System.Windows.Forms.Label();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.bkgrdWorker_PerfReport = new System.ComponentModel.BackgroundWorker();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.LightGray;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(143, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Start Time:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_ResetCounter);
            this.groupBox1.Controls.Add(this.lbl_TotalUnitFailed);
            this.groupBox1.Controls.Add(this.label20);
            this.groupBox1.Controls.Add(this.lbl_TotalUnitPassed);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.lbl_TotalUnitCount);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.lbl_DownTime);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_RunTime);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.lbl_StartTime);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(925, 75);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Performance Data";
            // 
            // btn_ResetCounter
            // 
            this.btn_ResetCounter.BackColor = System.Drawing.SystemColors.Control;
            this.btn_ResetCounter.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
            this.btn_ResetCounter.Location = new System.Drawing.Point(799, 22);
            this.btn_ResetCounter.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ResetCounter.Name = "btn_ResetCounter";
            this.btn_ResetCounter.Size = new System.Drawing.Size(117, 48);
            this.btn_ResetCounter.TabIndex = 47;
            this.btn_ResetCounter.Text = "Reset Counter";
            this.btn_ResetCounter.UseVisualStyleBackColor = false;
            this.btn_ResetCounter.Click += new System.EventHandler(this.btn_ResetCounter_Click);
            // 
            // lbl_TotalUnitFailed
            // 
            this.lbl_TotalUnitFailed.BackColor = System.Drawing.Color.White;
            this.lbl_TotalUnitFailed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_TotalUnitFailed.ForeColor = System.Drawing.Color.Black;
            this.lbl_TotalUnitFailed.Location = new System.Drawing.Point(689, 44);
            this.lbl_TotalUnitFailed.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TotalUnitFailed.Name = "lbl_TotalUnitFailed";
            this.lbl_TotalUnitFailed.Size = new System.Drawing.Size(106, 21);
            this.lbl_TotalUnitFailed.TabIndex = 46;
            this.lbl_TotalUnitFailed.Text = "0";
            this.lbl_TotalUnitFailed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.BackColor = System.Drawing.Color.LightGray;
            this.label20.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label20.ForeColor = System.Drawing.Color.Black;
            this.label20.Location = new System.Drawing.Point(538, 44);
            this.label20.Margin = new System.Windows.Forms.Padding(0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(143, 21);
            this.label20.TabIndex = 45;
            this.label20.Text = "Total Unit Failed:";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TotalUnitPassed
            // 
            this.lbl_TotalUnitPassed.BackColor = System.Drawing.Color.White;
            this.lbl_TotalUnitPassed.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_TotalUnitPassed.ForeColor = System.Drawing.Color.Black;
            this.lbl_TotalUnitPassed.Location = new System.Drawing.Point(425, 44);
            this.lbl_TotalUnitPassed.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TotalUnitPassed.Name = "lbl_TotalUnitPassed";
            this.lbl_TotalUnitPassed.Size = new System.Drawing.Size(106, 21);
            this.lbl_TotalUnitPassed.TabIndex = 10;
            this.lbl_TotalUnitPassed.Text = "0";
            this.lbl_TotalUnitPassed.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.BackColor = System.Drawing.Color.LightGray;
            this.label10.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(274, 44);
            this.label10.Margin = new System.Windows.Forms.Padding(0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(143, 21);
            this.label10.TabIndex = 9;
            this.label10.Text = "Total Unit Passed:";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_TotalUnitCount
            // 
            this.lbl_TotalUnitCount.BackColor = System.Drawing.Color.White;
            this.lbl_TotalUnitCount.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_TotalUnitCount.ForeColor = System.Drawing.Color.Black;
            this.lbl_TotalUnitCount.Location = new System.Drawing.Point(160, 44);
            this.lbl_TotalUnitCount.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_TotalUnitCount.Name = "lbl_TotalUnitCount";
            this.lbl_TotalUnitCount.Size = new System.Drawing.Size(106, 21);
            this.lbl_TotalUnitCount.TabIndex = 8;
            this.lbl_TotalUnitCount.Text = "0";
            this.lbl_TotalUnitCount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.Color.LightGray;
            this.label8.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(9, 44);
            this.label8.Margin = new System.Windows.Forms.Padding(0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(143, 21);
            this.label8.TabIndex = 7;
            this.label8.Text = "Total Unit Count:";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_DownTime
            // 
            this.lbl_DownTime.BackColor = System.Drawing.Color.White;
            this.lbl_DownTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_DownTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_DownTime.Location = new System.Drawing.Point(689, 23);
            this.lbl_DownTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_DownTime.Name = "lbl_DownTime";
            this.lbl_DownTime.Size = new System.Drawing.Size(106, 21);
            this.lbl_DownTime.TabIndex = 6;
            this.lbl_DownTime.Text = "00.1.2.3";
            this.lbl_DownTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.LightGray;
            this.label6.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(538, 23);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(143, 21);
            this.label6.TabIndex = 5;
            this.label6.Text = "Down Time:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_RunTime
            // 
            this.lbl_RunTime.BackColor = System.Drawing.Color.White;
            this.lbl_RunTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_RunTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_RunTime.Location = new System.Drawing.Point(425, 23);
            this.lbl_RunTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_RunTime.Name = "lbl_RunTime";
            this.lbl_RunTime.Size = new System.Drawing.Size(106, 21);
            this.lbl_RunTime.TabIndex = 4;
            this.lbl_RunTime.Text = "00.1.2.3";
            this.lbl_RunTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.LightGray;
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(274, 23);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(143, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "Run Time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_StartTime
            // 
            this.lbl_StartTime.BackColor = System.Drawing.Color.White;
            this.lbl_StartTime.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lbl_StartTime.ForeColor = System.Drawing.Color.Black;
            this.lbl_StartTime.Location = new System.Drawing.Point(160, 23);
            this.lbl_StartTime.Margin = new System.Windows.Forms.Padding(0);
            this.lbl_StartTime.Name = "lbl_StartTime";
            this.lbl_StartTime.Size = new System.Drawing.Size(106, 21);
            this.lbl_StartTime.TabIndex = 2;
            this.lbl_StartTime.Text = "00.1.2.3";
            this.lbl_StartTime.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // bkgrdWorker_PerfReport
            // 
            this.bkgrdWorker_PerfReport.WorkerSupportsCancellation = true;
            this.bkgrdWorker_PerfReport.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgrdWorker_PerfReport_DoWork);
            // 
            // UserCtrl_Report_PerformanceDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Report_PerformanceDialog";
            this.Size = new System.Drawing.Size(939, 81);
            this.Load += new System.EventHandler(this.UserCtrl_Report_PerformanceDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label lbl_StartTime;
        private System.Windows.Forms.Label lbl_TotalUnitPassed;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lbl_TotalUnitCount;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lbl_DownTime;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lbl_RunTime;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Timer m_Timer;
		private System.ComponentModel.BackgroundWorker bkgrdWorker_PerfReport;
        private System.Windows.Forms.Label lbl_TotalUnitFailed;
		private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Button btn_ResetCounter;
    }
}
