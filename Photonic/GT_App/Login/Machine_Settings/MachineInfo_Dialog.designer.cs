namespace GT_App
{
    partial class MachineInfo_Dialog
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
			this.btn_ESC = new System.Windows.Forms.Button();
			this.txtb_MachineID = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtb_MachineName = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.lbl_MachineNumber = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.btn_UpdateEsc = new System.Windows.Forms.Button();
			this.lbl_LogLevelSet = new System.Windows.Forms.Label();
			this.btn_LogLevelSet = new System.Windows.Forms.Button();
			this.btn_MachineType = new System.Windows.Forms.Button();
			this.lbl_MachineType = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// btn_ESC
			// 
			this.btn_ESC.BackColor = System.Drawing.SystemColors.Control;
			this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_ESC.ForeColor = System.Drawing.Color.Black;
			this.btn_ESC.Location = new System.Drawing.Point(348, 246);
			this.btn_ESC.Margin = new System.Windows.Forms.Padding(4);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(165, 53);
			this.btn_ESC.TabIndex = 1;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = false;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// txtb_MachineID
			// 
			this.txtb_MachineID.Location = new System.Drawing.Point(144, 30);
			this.txtb_MachineID.Name = "txtb_MachineID";
			this.txtb_MachineID.Size = new System.Drawing.Size(184, 22);
			this.txtb_MachineID.TabIndex = 2;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(14, 33);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(89, 16);
			this.label1.TabIndex = 3;
			this.label1.Text = "Machine ID:";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 61);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(115, 16);
			this.label2.TabIndex = 5;
			this.label2.Text = "Machine Name:";
			// 
			// txtb_MachineName
			// 
			this.txtb_MachineName.Location = new System.Drawing.Point(144, 58);
			this.txtb_MachineName.Name = "txtb_MachineName";
			this.txtb_MachineName.Size = new System.Drawing.Size(307, 22);
			this.txtb_MachineName.TabIndex = 4;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Transparent;
			this.groupBox1.Controls.Add(this.lbl_MachineType);
			this.groupBox1.Controls.Add(this.btn_MachineType);
			this.groupBox1.Controls.Add(this.lbl_MachineNumber);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtb_MachineName);
			this.groupBox1.Controls.Add(this.txtb_MachineID);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.groupBox1.ForeColor = System.Drawing.Color.Black;
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(498, 176);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Machine Settings";
			// 
			// lbl_MachineNumber
			// 
			this.lbl_MachineNumber.BackColor = System.Drawing.Color.White;
			this.lbl_MachineNumber.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_MachineNumber.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lbl_MachineNumber.Location = new System.Drawing.Point(144, 88);
			this.lbl_MachineNumber.Name = "lbl_MachineNumber";
			this.lbl_MachineNumber.Size = new System.Drawing.Size(53, 26);
			this.lbl_MachineNumber.TabIndex = 8;
			this.lbl_MachineNumber.Text = "1";
			this.lbl_MachineNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 87);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(128, 16);
			this.label3.TabIndex = 7;
			this.label3.Text = "Machine Number:";
			// 
			// btn_UpdateEsc
			// 
			this.btn_UpdateEsc.BackColor = System.Drawing.SystemColors.Control;
			this.btn_UpdateEsc.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_UpdateEsc.ForeColor = System.Drawing.Color.Black;
			this.btn_UpdateEsc.Location = new System.Drawing.Point(175, 246);
			this.btn_UpdateEsc.Margin = new System.Windows.Forms.Padding(4);
			this.btn_UpdateEsc.Name = "btn_UpdateEsc";
			this.btn_UpdateEsc.Size = new System.Drawing.Size(165, 53);
			this.btn_UpdateEsc.TabIndex = 7;
			this.btn_UpdateEsc.Text = "Update and ESC";
			this.btn_UpdateEsc.UseVisualStyleBackColor = false;
			this.btn_UpdateEsc.Click += new System.EventHandler(this.btn_UpdateEsc_Click);
			// 
			// lbl_LogLevelSet
			// 
			this.lbl_LogLevelSet.BackColor = System.Drawing.Color.White;
			this.lbl_LogLevelSet.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_LogLevelSet.Location = new System.Drawing.Point(459, 200);
			this.lbl_LogLevelSet.Name = "lbl_LogLevelSet";
			this.lbl_LogLevelSet.Size = new System.Drawing.Size(48, 34);
			this.lbl_LogLevelSet.TabIndex = 8;
			this.lbl_LogLevelSet.Text = "2";
			this.lbl_LogLevelSet.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// btn_LogLevelSet
			// 
			this.btn_LogLevelSet.BackColor = System.Drawing.SystemColors.Control;
			this.btn_LogLevelSet.ForeColor = System.Drawing.Color.Black;
			this.btn_LogLevelSet.Location = new System.Drawing.Point(244, 199);
			this.btn_LogLevelSet.Name = "btn_LogLevelSet";
			this.btn_LogLevelSet.Size = new System.Drawing.Size(210, 37);
			this.btn_LogLevelSet.TabIndex = 9;
			this.btn_LogLevelSet.Text = "Log Level Set";
			this.btn_LogLevelSet.UseVisualStyleBackColor = false;
			this.btn_LogLevelSet.Click += new System.EventHandler(this.btn_LogLevelSet_Click);
			// 
			// btn_MachineType
			// 
			this.btn_MachineType.Location = new System.Drawing.Point(230, 88);
			this.btn_MachineType.Name = "btn_MachineType";
			this.btn_MachineType.Size = new System.Drawing.Size(192, 36);
			this.btn_MachineType.TabIndex = 9;
			this.btn_MachineType.Text = "Machine Type Num";
			this.btn_MachineType.UseVisualStyleBackColor = true;
			this.btn_MachineType.Click += new System.EventHandler(this.btn_MachineType_Click);
			// 
			// lbl_MachineType
			// 
			this.lbl_MachineType.BackColor = System.Drawing.Color.White;
			this.lbl_MachineType.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_MachineType.ForeColor = System.Drawing.SystemColors.WindowText;
			this.lbl_MachineType.Location = new System.Drawing.Point(431, 93);
			this.lbl_MachineType.Name = "lbl_MachineType";
			this.lbl_MachineType.Size = new System.Drawing.Size(53, 26);
			this.lbl_MachineType.TabIndex = 10;
			this.lbl_MachineType.Text = "1";
			this.lbl_MachineType.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// MachineInfo_Dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btn_ESC;
			this.ClientSize = new System.Drawing.Size(521, 307);
			this.ControlBox = false;
			this.Controls.Add(this.btn_LogLevelSet);
			this.Controls.Add(this.lbl_LogLevelSet);
			this.Controls.Add(this.btn_UpdateEsc);
			this.Controls.Add(this.btn_ESC);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "MachineInfo_Dialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Machine Info";
			this.Load += new System.EventHandler(this.MachineInfo_Dialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.TextBox txtb_MachineID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_MachineName;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_UpdateEsc;
        private System.Windows.Forms.Label lbl_LogLevelSet;
        private System.Windows.Forms.Button btn_LogLevelSet;
        private System.Windows.Forms.Label lbl_MachineNumber;
        private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label lbl_MachineType;
		private System.Windows.Forms.Button btn_MachineType;
    }
}