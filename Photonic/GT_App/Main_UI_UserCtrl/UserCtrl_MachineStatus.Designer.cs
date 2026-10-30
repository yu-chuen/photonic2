namespace GT_App
{
    partial class UserCtrl_MachineStatus
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
            this.lbl_MachineState = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_ErrorMsg = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.lbl_InstructionMsg = new System.Windows.Forms.Label();
            this.bkg_Worker_Image = new System.ComponentModel.BackgroundWorker();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_Date = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lbl_Time = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_MachineState
            // 
            this.lbl_MachineState.BackColor = System.Drawing.Color.White;
            this.lbl_MachineState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_MachineState.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_MachineState.ForeColor = System.Drawing.Color.DeepSkyBlue;
            this.lbl_MachineState.Location = new System.Drawing.Point(1174, 572);
            this.lbl_MachineState.Name = "lbl_MachineState";
            this.lbl_MachineState.Size = new System.Drawing.Size(246, 64);
            this.lbl_MachineState.TabIndex = 0;
            this.lbl_MachineState.Text = "N/A";
            this.lbl_MachineState.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(1177, 552);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(130, 20);
            this.label2.TabIndex = 1;
            this.label2.Text = "Machine State:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.lbl_ErrorMsg);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.lbl_InstructionMsg);
            this.groupBox1.ForeColor = System.Drawing.Color.Black;
            this.groupBox1.Location = new System.Drawing.Point(3, 554);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1165, 235);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Machine Status";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(4, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(131, 20);
            this.label6.TabIndex = 9;
            this.label6.Text = "Error Message:";
            // 
            // lbl_ErrorMsg
            // 
            this.lbl_ErrorMsg.BackColor = System.Drawing.Color.Black;
            this.lbl_ErrorMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_ErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorMsg.ForeColor = System.Drawing.Color.Red;
            this.lbl_ErrorMsg.Location = new System.Drawing.Point(8, 40);
            this.lbl_ErrorMsg.Name = "lbl_ErrorMsg";
            this.lbl_ErrorMsg.Size = new System.Drawing.Size(1150, 182);
            this.lbl_ErrorMsg.TabIndex = 8;
            this.lbl_ErrorMsg.Text = "N/A";
            this.lbl_ErrorMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(7, 138);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(177, 20);
            this.label5.TabIndex = 7;
            this.label5.Text = "Instruction Message:";
            this.label5.Visible = false;
            // 
            // lbl_InstructionMsg
            // 
            this.lbl_InstructionMsg.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_InstructionMsg.BackColor = System.Drawing.Color.Black;
            this.lbl_InstructionMsg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_InstructionMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_InstructionMsg.ForeColor = System.Drawing.Color.Lime;
            this.lbl_InstructionMsg.Location = new System.Drawing.Point(11, 120);
            this.lbl_InstructionMsg.Name = "lbl_InstructionMsg";
            this.lbl_InstructionMsg.Size = new System.Drawing.Size(1115, 73);
            this.lbl_InstructionMsg.TabIndex = 6;
            this.lbl_InstructionMsg.Text = "N/A";
            this.lbl_InstructionMsg.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lbl_InstructionMsg.Visible = false;
            // 
            // bkg_Worker_Image
            // 
            this.bkg_Worker_Image.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkg_Worker_Image_DoWork);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_Date);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.lbl_Time);
            this.groupBox2.Location = new System.Drawing.Point(1174, 639);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(246, 87);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Time";
            // 
            // lbl_Date
            // 
            this.lbl_Date.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Date.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Date.Location = new System.Drawing.Point(83, 19);
            this.lbl_Date.Name = "lbl_Date";
            this.lbl_Date.Size = new System.Drawing.Size(154, 31);
            this.lbl_Date.TabIndex = 8;
            this.lbl_Date.Text = "30:8:2018";
            this.lbl_Date.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label3
            // 
            this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(10, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 31);
            this.label3.TabIndex = 6;
            this.label3.Text = "Date:";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(10, 50);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(74, 29);
            this.label4.TabIndex = 7;
            this.label4.Text = "Time:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Time
            // 
            this.lbl_Time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Time.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Time.Location = new System.Drawing.Point(83, 50);
            this.lbl_Time.Name = "lbl_Time";
            this.lbl_Time.Size = new System.Drawing.Size(154, 29);
            this.lbl_Time.TabIndex = 9;
            this.lbl_Time.Text = "17:30:56";
            this.lbl_Time.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pictureBox1.ErrorImage = global::GT_App.Properties.Resources.Genetec_MAIN_logo;
            this.pictureBox1.InitialImage = global::GT_App.Properties.Resources.Genetec_MAIN_logo1;
            this.pictureBox1.Location = new System.Drawing.Point(3, 14);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1165, 524);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            // 
            // UserCtrl_MachineStatus
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lbl_MachineState);
            this.Controls.Add(this.label2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.Black;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_MachineStatus";
            this.Size = new System.Drawing.Size(1750, 802);
            this.Load += new System.EventHandler(this.UserCtrl_MachineStatus_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_MachineState;
        private System.Windows.Forms.Label label2;
		private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lbl_InstructionMsg;
        private System.Windows.Forms.Label lbl_ErrorMsg;
		private System.Windows.Forms.Label label6;
		private System.ComponentModel.BackgroundWorker bkg_Worker_Image;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label lbl_Date;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lbl_Time;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
