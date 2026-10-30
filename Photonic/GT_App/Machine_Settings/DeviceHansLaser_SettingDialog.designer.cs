namespace GT_App
{
    partial class DeviceHansLaser_SettingDialog
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.txtb_AppFileName = new System.Windows.Forms.TextBox();
			this.lbl_AppFileName_Static = new System.Windows.Forms.Label();
			this.btn_AppFilePath = new System.Windows.Forms.Button();
			this.lbl_AppFilePath_Static = new System.Windows.Forms.Label();
			this.lbl_AppFilePath = new System.Windows.Forms.Label();
			this.btn_CodeEnable_002 = new System.Windows.Forms.Button();
			this.btn_CodeEnable_001 = new System.Windows.Forms.Button();
			this.lbl_TimeOutSetting = new System.Windows.Forms.Label();
			this.btn_TimeOutSetting = new System.Windows.Forms.Button();
			this.lbl_EnableDesc = new System.Windows.Forms.Label();
			this.btn_Enable = new System.Windows.Forms.Button();
			this.btn_DataFileName_InputDlg = new System.Windows.Forms.Button();
			this.btn_DataFilePath_FolderDlg = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.lbl_DataFileName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lbl_DataFilePath = new System.Windows.Forms.Label();
			this.btn_ESC = new System.Windows.Forms.Button();
			this.btn_UpdateNEsc = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.btn_OpenApp = new System.Windows.Forms.Button();
			this.btn_TriggerLaserMarker = new System.Windows.Forms.Button();
			this.btn_CheckFileExist = new System.Windows.Forms.Button();
			this.txtb_DataCode2 = new System.Windows.Forms.TextBox();
			this.txtb_DataCode1 = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.btn_DeleteFile = new System.Windows.Forms.Button();
			this.btn_WriteIDToFile = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.txtb_AppFileName);
			this.groupBox1.Controls.Add(this.lbl_AppFileName_Static);
			this.groupBox1.Controls.Add(this.btn_AppFilePath);
			this.groupBox1.Controls.Add(this.lbl_AppFilePath_Static);
			this.groupBox1.Controls.Add(this.lbl_AppFilePath);
			this.groupBox1.Controls.Add(this.btn_CodeEnable_002);
			this.groupBox1.Controls.Add(this.btn_CodeEnable_001);
			this.groupBox1.Controls.Add(this.lbl_TimeOutSetting);
			this.groupBox1.Controls.Add(this.btn_TimeOutSetting);
			this.groupBox1.Controls.Add(this.lbl_EnableDesc);
			this.groupBox1.Controls.Add(this.btn_Enable);
			this.groupBox1.Controls.Add(this.btn_DataFileName_InputDlg);
			this.groupBox1.Controls.Add(this.btn_DataFilePath_FolderDlg);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lbl_DataFileName);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.lbl_DataFilePath);
			this.groupBox1.Location = new System.Drawing.Point(6, 4);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(510, 409);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// txtb_AppFileName
			// 
			this.txtb_AppFileName.Location = new System.Drawing.Point(198, 213);
			this.txtb_AppFileName.Name = "txtb_AppFileName";
			this.txtb_AppFileName.Size = new System.Drawing.Size(217, 22);
			this.txtb_AppFileName.TabIndex = 19;
			// 
			// lbl_AppFileName_Static
			// 
			this.lbl_AppFileName_Static.AutoSize = true;
			this.lbl_AppFileName_Static.Location = new System.Drawing.Point(15, 215);
			this.lbl_AppFileName_Static.Name = "lbl_AppFileName_Static";
			this.lbl_AppFileName_Static.Size = new System.Drawing.Size(182, 16);
			this.lbl_AppFileName_Static.TabIndex = 18;
			this.lbl_AppFileName_Static.Text = "HANS LASER File Name:";
			// 
			// btn_AppFilePath
			// 
			this.btn_AppFilePath.Location = new System.Drawing.Point(449, 150);
			this.btn_AppFilePath.Name = "btn_AppFilePath";
			this.btn_AppFilePath.Size = new System.Drawing.Size(40, 23);
			this.btn_AppFilePath.TabIndex = 16;
			this.btn_AppFilePath.Text = "...";
			this.btn_AppFilePath.UseVisualStyleBackColor = true;
			this.btn_AppFilePath.Click += new System.EventHandler(this.btn_AppFilePath_Click);
			// 
			// lbl_AppFilePath_Static
			// 
			this.lbl_AppFilePath_Static.AutoSize = true;
			this.lbl_AppFilePath_Static.Location = new System.Drawing.Point(14, 128);
			this.lbl_AppFilePath_Static.Name = "lbl_AppFilePath_Static";
			this.lbl_AppFilePath_Static.Size = new System.Drawing.Size(204, 16);
			this.lbl_AppFilePath_Static.TabIndex = 15;
			this.lbl_AppFilePath_Static.Text = "HANS LASER App File Path:";
			// 
			// lbl_AppFilePath
			// 
			this.lbl_AppFilePath.BackColor = System.Drawing.Color.White;
			this.lbl_AppFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_AppFilePath.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
			this.lbl_AppFilePath.Location = new System.Drawing.Point(13, 149);
			this.lbl_AppFilePath.Name = "lbl_AppFilePath";
			this.lbl_AppFilePath.Size = new System.Drawing.Size(433, 53);
			this.lbl_AppFilePath.TabIndex = 14;
			this.lbl_AppFilePath.Text = "...";
			// 
			// btn_CodeEnable_002
			// 
			this.btn_CodeEnable_002.Location = new System.Drawing.Point(168, 298);
			this.btn_CodeEnable_002.Name = "btn_CodeEnable_002";
			this.btn_CodeEnable_002.Size = new System.Drawing.Size(147, 48);
			this.btn_CodeEnable_002.TabIndex = 13;
			this.btn_CodeEnable_002.Text = "Code 2 Enabled";
			this.btn_CodeEnable_002.UseVisualStyleBackColor = true;
			this.btn_CodeEnable_002.Click += new System.EventHandler(this.btn_CodeEnable_002_Click);
			// 
			// btn_CodeEnable_001
			// 
			this.btn_CodeEnable_001.Location = new System.Drawing.Point(13, 298);
			this.btn_CodeEnable_001.Name = "btn_CodeEnable_001";
			this.btn_CodeEnable_001.Size = new System.Drawing.Size(147, 48);
			this.btn_CodeEnable_001.TabIndex = 12;
			this.btn_CodeEnable_001.Text = "Code 1 Enabled";
			this.btn_CodeEnable_001.UseVisualStyleBackColor = true;
			this.btn_CodeEnable_001.Click += new System.EventHandler(this.btn_CodeEnable_001_Click);
			// 
			// lbl_TimeOutSetting
			// 
			this.lbl_TimeOutSetting.BackColor = System.Drawing.Color.White;
			this.lbl_TimeOutSetting.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_TimeOutSetting.Location = new System.Drawing.Point(174, 365);
			this.lbl_TimeOutSetting.Name = "lbl_TimeOutSetting";
			this.lbl_TimeOutSetting.Size = new System.Drawing.Size(86, 26);
			this.lbl_TimeOutSetting.TabIndex = 9;
			this.lbl_TimeOutSetting.Text = "...";
			// 
			// btn_TimeOutSetting
			// 
			this.btn_TimeOutSetting.Location = new System.Drawing.Point(13, 353);
			this.btn_TimeOutSetting.Name = "btn_TimeOutSetting";
			this.btn_TimeOutSetting.Size = new System.Drawing.Size(147, 48);
			this.btn_TimeOutSetting.TabIndex = 8;
			this.btn_TimeOutSetting.Text = "Time Out (ms)";
			this.btn_TimeOutSetting.UseVisualStyleBackColor = true;
			this.btn_TimeOutSetting.Click += new System.EventHandler(this.btn_TimeOutSetting_Click);
			// 
			// lbl_EnableDesc
			// 
			this.lbl_EnableDesc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_EnableDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.lbl_EnableDesc.Location = new System.Drawing.Point(168, 246);
			this.lbl_EnableDesc.Name = "lbl_EnableDesc";
			this.lbl_EnableDesc.Size = new System.Drawing.Size(300, 42);
			this.lbl_EnableDesc.TabIndex = 7;
			this.lbl_EnableDesc.Text = "label1";
			// 
			// btn_Enable
			// 
			this.btn_Enable.Location = new System.Drawing.Point(13, 242);
			this.btn_Enable.Name = "btn_Enable";
			this.btn_Enable.Size = new System.Drawing.Size(147, 48);
			this.btn_Enable.TabIndex = 6;
			this.btn_Enable.Text = "Enabled";
			this.btn_Enable.UseVisualStyleBackColor = true;
			this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
			// 
			// btn_DataFileName_InputDlg
			// 
			this.btn_DataFileName_InputDlg.Location = new System.Drawing.Point(450, 99);
			this.btn_DataFileName_InputDlg.Name = "btn_DataFileName_InputDlg";
			this.btn_DataFileName_InputDlg.Size = new System.Drawing.Size(40, 23);
			this.btn_DataFileName_InputDlg.TabIndex = 5;
			this.btn_DataFileName_InputDlg.Text = "...";
			this.btn_DataFileName_InputDlg.UseVisualStyleBackColor = true;
			this.btn_DataFileName_InputDlg.Click += new System.EventHandler(this.btn_DataFileName_InputDlg_Click);
			// 
			// btn_DataFilePath_FolderDlg
			// 
			this.btn_DataFilePath_FolderDlg.Location = new System.Drawing.Point(449, 48);
			this.btn_DataFilePath_FolderDlg.Name = "btn_DataFilePath_FolderDlg";
			this.btn_DataFilePath_FolderDlg.Size = new System.Drawing.Size(40, 23);
			this.btn_DataFilePath_FolderDlg.TabIndex = 4;
			this.btn_DataFilePath_FolderDlg.Text = "...";
			this.btn_DataFilePath_FolderDlg.UseVisualStyleBackColor = true;
			this.btn_DataFilePath_FolderDlg.Click += new System.EventHandler(this.btn_DataFilePath_FolderDlg_Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(14, 76);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(120, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Data File Name:";
			// 
			// lbl_DataFileName
			// 
			this.lbl_DataFileName.BackColor = System.Drawing.Color.White;
			this.lbl_DataFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_DataFileName.Location = new System.Drawing.Point(13, 97);
			this.lbl_DataFileName.Name = "lbl_DataFileName";
			this.lbl_DataFileName.Size = new System.Drawing.Size(433, 26);
			this.lbl_DataFileName.TabIndex = 2;
			this.lbl_DataFileName.Text = "...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(14, 26);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(129, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "ID Data File Path:";
			// 
			// lbl_DataFilePath
			// 
			this.lbl_DataFilePath.BackColor = System.Drawing.Color.White;
			this.lbl_DataFilePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lbl_DataFilePath.Location = new System.Drawing.Point(13, 47);
			this.lbl_DataFilePath.Name = "lbl_DataFilePath";
			this.lbl_DataFilePath.Size = new System.Drawing.Size(433, 26);
			this.lbl_DataFilePath.TabIndex = 0;
			this.lbl_DataFilePath.Text = "...";
			// 
			// btn_ESC
			// 
			this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_ESC.Location = new System.Drawing.Point(364, 624);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(147, 48);
			this.btn_ESC.TabIndex = 1;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = true;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// btn_UpdateNEsc
			// 
			this.btn_UpdateNEsc.Location = new System.Drawing.Point(212, 624);
			this.btn_UpdateNEsc.Name = "btn_UpdateNEsc";
			this.btn_UpdateNEsc.Size = new System.Drawing.Size(147, 48);
			this.btn_UpdateNEsc.TabIndex = 2;
			this.btn_UpdateNEsc.Text = "Update and ESC";
			this.btn_UpdateNEsc.UseVisualStyleBackColor = true;
			this.btn_UpdateNEsc.Click += new System.EventHandler(this.btn_UpdateNEsc_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.btn_OpenApp);
			this.groupBox2.Controls.Add(this.btn_TriggerLaserMarker);
			this.groupBox2.Controls.Add(this.btn_CheckFileExist);
			this.groupBox2.Controls.Add(this.txtb_DataCode2);
			this.groupBox2.Controls.Add(this.txtb_DataCode1);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.btn_DeleteFile);
			this.groupBox2.Controls.Add(this.btn_WriteIDToFile);
			this.groupBox2.Location = new System.Drawing.Point(6, 420);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(512, 191);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Operations";
			// 
			// btn_OpenApp
			// 
			this.btn_OpenApp.Location = new System.Drawing.Point(355, 77);
			this.btn_OpenApp.Name = "btn_OpenApp";
			this.btn_OpenApp.Size = new System.Drawing.Size(147, 48);
			this.btn_OpenApp.TabIndex = 13;
			this.btn_OpenApp.Text = "Open Hans Laser Application";
			this.btn_OpenApp.UseVisualStyleBackColor = true;
			this.btn_OpenApp.Click += new System.EventHandler(this.btn_OpenApp_Click);
			// 
			// btn_TriggerLaserMarker
			// 
			this.btn_TriggerLaserMarker.Location = new System.Drawing.Point(355, 23);
			this.btn_TriggerLaserMarker.Name = "btn_TriggerLaserMarker";
			this.btn_TriggerLaserMarker.Size = new System.Drawing.Size(147, 48);
			this.btn_TriggerLaserMarker.TabIndex = 12;
			this.btn_TriggerLaserMarker.Text = "Trigger Laser Marker";
			this.btn_TriggerLaserMarker.UseVisualStyleBackColor = true;
			this.btn_TriggerLaserMarker.Click += new System.EventHandler(this.btn_TriggerLaserMarker_Click);
			// 
			// btn_CheckFileExist
			// 
			this.btn_CheckFileExist.Location = new System.Drawing.Point(188, 130);
			this.btn_CheckFileExist.Name = "btn_CheckFileExist";
			this.btn_CheckFileExist.Size = new System.Drawing.Size(147, 48);
			this.btn_CheckFileExist.TabIndex = 11;
			this.btn_CheckFileExist.Text = "Check File Exist";
			this.btn_CheckFileExist.UseVisualStyleBackColor = true;
			this.btn_CheckFileExist.Click += new System.EventHandler(this.btn_CheckFileExist_Click);
			// 
			// txtb_DataCode2
			// 
			this.txtb_DataCode2.Location = new System.Drawing.Point(16, 90);
			this.txtb_DataCode2.Name = "txtb_DataCode2";
			this.txtb_DataCode2.Size = new System.Drawing.Size(312, 22);
			this.txtb_DataCode2.TabIndex = 10;
			this.txtb_DataCode2.Text = "QWERTY1234567890";
			// 
			// txtb_DataCode1
			// 
			this.txtb_DataCode1.Location = new System.Drawing.Point(16, 42);
			this.txtb_DataCode1.Name = "txtb_DataCode1";
			this.txtb_DataCode1.Size = new System.Drawing.Size(312, 22);
			this.txtb_DataCode1.TabIndex = 9;
			this.txtb_DataCode1.Text = "1234567890ABCDEF";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(16, 72);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 16);
			this.label5.TabIndex = 8;
			this.label5.Text = "Data Code 2:";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(16, 23);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(98, 16);
			this.label4.TabIndex = 6;
			this.label4.Text = "Data Code 1:";
			// 
			// btn_DeleteFile
			// 
			this.btn_DeleteFile.Location = new System.Drawing.Point(355, 130);
			this.btn_DeleteFile.Name = "btn_DeleteFile";
			this.btn_DeleteFile.Size = new System.Drawing.Size(147, 48);
			this.btn_DeleteFile.TabIndex = 4;
			this.btn_DeleteFile.Text = "Delete File";
			this.btn_DeleteFile.UseVisualStyleBackColor = true;
			this.btn_DeleteFile.Click += new System.EventHandler(this.btn_DeleteFile_Click);
			// 
			// btn_WriteIDToFile
			// 
			this.btn_WriteIDToFile.Location = new System.Drawing.Point(22, 130);
			this.btn_WriteIDToFile.Name = "btn_WriteIDToFile";
			this.btn_WriteIDToFile.Size = new System.Drawing.Size(147, 48);
			this.btn_WriteIDToFile.TabIndex = 3;
			this.btn_WriteIDToFile.Text = "Write ID to File";
			this.btn_WriteIDToFile.UseVisualStyleBackColor = true;
			this.btn_WriteIDToFile.Click += new System.EventHandler(this.btn_WriteIDToFile_Click);
			// 
			// DeviceHansLaser_SettingDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.SystemColors.Control;
			this.CancelButton = this.btn_ESC;
			this.ClientSize = new System.Drawing.Size(526, 678);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.btn_UpdateNEsc);
			this.Controls.Add(this.btn_ESC);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.ForeColor = System.Drawing.Color.Black;
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "DeviceHansLaser_SettingDialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "HansLaser Setting";
			this.Load += new System.EventHandler(this.DeviceHansLaser_SettingDialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_DataFileName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_DataFilePath;
        private System.Windows.Forms.Button btn_DataFileName_InputDlg;
        private System.Windows.Forms.Button btn_DataFilePath_FolderDlg;
        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_UpdateNEsc;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox txtb_DataCode2;
        private System.Windows.Forms.TextBox txtb_DataCode1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_DeleteFile;
        private System.Windows.Forms.Button btn_WriteIDToFile;
        private System.Windows.Forms.Button btn_CheckFileExist;
        private System.Windows.Forms.Button btn_TriggerLaserMarker;
        private System.Windows.Forms.Button btn_Enable;
        private System.Windows.Forms.Label lbl_EnableDesc;
        private System.Windows.Forms.Label lbl_TimeOutSetting;
        private System.Windows.Forms.Button btn_TimeOutSetting;
        private System.Windows.Forms.Button btn_CodeEnable_002;
        private System.Windows.Forms.Button btn_CodeEnable_001;
        private System.Windows.Forms.Label lbl_AppFileName_Static;
        private System.Windows.Forms.Button btn_AppFilePath;
        private System.Windows.Forms.Label lbl_AppFilePath_Static;
        private System.Windows.Forms.Label lbl_AppFilePath;
        private System.Windows.Forms.Button btn_OpenApp;
        private System.Windows.Forms.TextBox txtb_AppFileName;
    }
}