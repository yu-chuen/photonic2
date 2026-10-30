namespace GT_App
{
	partial class Server_COMTCP_GT_CentralPC_Dlg
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.btn_ESC = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_Enable = new System.Windows.Forms.Button();
            this.groupBox_Operations = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtb_Password = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.btn_GetUserAccess = new System.Windows.Forms.Button();
            this.txtb_Username = new System.Windows.Forms.TextBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btn_UpdateResults = new System.Windows.Forms.Button();
            this.cmbBox_Results = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_GetDataFromServer = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.cmbBox_CommandStrings = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_CmdPalletStationDone = new System.Windows.Forms.Button();
            this.btn_SkipCheck = new System.Windows.Forms.Button();
            this.cmbBox_StationID = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btn_GetCurrentPkg = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.txtb_SN = new System.Windows.Forms.TextBox();
            this.btn_InserNewPart = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Command = new System.Windows.Forms.Label();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.btn_GetPartCount = new System.Windows.Forms.Button();
            this.groupBox_Operations.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(763, 769);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(141, 46);
            this.btn_ESC.TabIndex = 0;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Update.Location = new System.Drawing.Point(618, 769);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(141, 46);
            this.btn_Update.TabIndex = 1;
            this.btn_Update.Text = "Update Settings";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_Enable
            // 
            this.btn_Enable.Location = new System.Drawing.Point(8, 8);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(160, 40);
            this.btn_Enable.TabIndex = 2;
            this.btn_Enable.Text = "Server";
            this.btn_Enable.UseVisualStyleBackColor = true;
            this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
            // 
            // groupBox_Operations
            // 
            this.groupBox_Operations.Controls.Add(this.btn_GetPartCount);
            this.groupBox_Operations.Controls.Add(this.groupBox4);
            this.groupBox_Operations.Controls.Add(this.groupBox3);
            this.groupBox_Operations.Controls.Add(this.groupBox2);
            this.groupBox_Operations.Controls.Add(this.groupBox1);
            this.groupBox_Operations.Controls.Add(this.btn_GetCurrentPkg);
            this.groupBox_Operations.Controls.Add(this.label1);
            this.groupBox_Operations.Controls.Add(this.txtb_SN);
            this.groupBox_Operations.Controls.Add(this.btn_InserNewPart);
            this.groupBox_Operations.Controls.Add(this.label3);
            this.groupBox_Operations.Controls.Add(this.label2);
            this.groupBox_Operations.Controls.Add(this.lbl_Command);
            this.groupBox_Operations.Controls.Add(this.lbl_Response);
            this.groupBox_Operations.Location = new System.Drawing.Point(315, 14);
            this.groupBox_Operations.Name = "groupBox_Operations";
            this.groupBox_Operations.Size = new System.Drawing.Size(555, 746);
            this.groupBox_Operations.TabIndex = 3;
            this.groupBox_Operations.TabStop = false;
            this.groupBox_Operations.Text = "Operations / Testing";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label8);
            this.groupBox4.Controls.Add(this.txtb_Password);
            this.groupBox4.Controls.Add(this.label7);
            this.groupBox4.Controls.Add(this.btn_GetUserAccess);
            this.groupBox4.Controls.Add(this.txtb_Username);
            this.groupBox4.Location = new System.Drawing.Point(6, 642);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(543, 95);
            this.groupBox4.TabIndex = 23;
            this.groupBox4.TabStop = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(291, 19);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(80, 16);
            this.label8.TabIndex = 19;
            this.label8.Text = "Password:";
            // 
            // txtb_Password
            // 
            this.txtb_Password.Location = new System.Drawing.Point(377, 16);
            this.txtb_Password.Name = "txtb_Password";
            this.txtb_Password.Size = new System.Drawing.Size(158, 22);
            this.txtb_Password.TabIndex = 18;
            this.txtb_Password.UseSystemPasswordChar = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(10, 19);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(83, 16);
            this.label7.TabIndex = 17;
            this.label7.Text = "Username:";
            // 
            // btn_GetUserAccess
            // 
            this.btn_GetUserAccess.Location = new System.Drawing.Point(6, 44);
            this.btn_GetUserAccess.Name = "btn_GetUserAccess";
            this.btn_GetUserAccess.Size = new System.Drawing.Size(262, 36);
            this.btn_GetUserAccess.TabIndex = 10;
            this.btn_GetUserAccess.Text = "Get User Access";
            this.btn_GetUserAccess.UseVisualStyleBackColor = true;
            this.btn_GetUserAccess.Click += new System.EventHandler(this.btn_GetUserAccess_Click);
            // 
            // txtb_Username
            // 
            this.txtb_Username.Location = new System.Drawing.Point(106, 16);
            this.txtb_Username.Name = "txtb_Username";
            this.txtb_Username.Size = new System.Drawing.Size(158, 22);
            this.txtb_Username.TabIndex = 11;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btn_UpdateResults);
            this.groupBox3.Controls.Add(this.cmbBox_Results);
            this.groupBox3.Controls.Add(this.label5);
            this.groupBox3.Location = new System.Drawing.Point(6, 541);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(543, 95);
            this.groupBox3.TabIndex = 22;
            this.groupBox3.TabStop = false;
            // 
            // btn_UpdateResults
            // 
            this.btn_UpdateResults.Location = new System.Drawing.Point(6, 45);
            this.btn_UpdateResults.Name = "btn_UpdateResults";
            this.btn_UpdateResults.Size = new System.Drawing.Size(262, 36);
            this.btn_UpdateResults.TabIndex = 5;
            this.btn_UpdateResults.Text = "Update Results";
            this.btn_UpdateResults.UseVisualStyleBackColor = true;
            this.btn_UpdateResults.Click += new System.EventHandler(this.btn_UpdateResults_Click);
            // 
            // cmbBox_Results
            // 
            this.cmbBox_Results.FormattingEnabled = true;
            this.cmbBox_Results.Location = new System.Drawing.Point(152, 15);
            this.cmbBox_Results.Name = "cmbBox_Results";
            this.cmbBox_Results.Size = new System.Drawing.Size(383, 24);
            this.cmbBox_Results.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(10, 19);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(130, 16);
            this.label5.TabIndex = 16;
            this.label5.Text = "Results (Testing):";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_GetDataFromServer);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.cmbBox_CommandStrings);
            this.groupBox2.Location = new System.Drawing.Point(6, 440);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(543, 95);
            this.groupBox2.TabIndex = 21;
            this.groupBox2.TabStop = false;
            // 
            // btn_GetDataFromServer
            // 
            this.btn_GetDataFromServer.Location = new System.Drawing.Point(6, 45);
            this.btn_GetDataFromServer.Name = "btn_GetDataFromServer";
            this.btn_GetDataFromServer.Size = new System.Drawing.Size(262, 36);
            this.btn_GetDataFromServer.TabIndex = 17;
            this.btn_GetDataFromServer.Text = "Get Data From Server";
            this.btn_GetDataFromServer.UseVisualStyleBackColor = true;
            this.btn_GetDataFromServer.Click += new System.EventHandler(this.btn_GetDataFromServer_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(10, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(133, 16);
            this.label6.TabIndex = 19;
            this.label6.Text = "Command Strings:";
            // 
            // cmbBox_CommandStrings
            // 
            this.cmbBox_CommandStrings.FormattingEnabled = true;
            this.cmbBox_CommandStrings.Location = new System.Drawing.Point(152, 15);
            this.cmbBox_CommandStrings.Name = "cmbBox_CommandStrings";
            this.cmbBox_CommandStrings.Size = new System.Drawing.Size(383, 24);
            this.cmbBox_CommandStrings.TabIndex = 18;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_CmdPalletStationDone);
            this.groupBox1.Controls.Add(this.btn_SkipCheck);
            this.groupBox1.Controls.Add(this.cmbBox_StationID);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Location = new System.Drawing.Point(6, 339);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(543, 95);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            // 
            // btn_CmdPalletStationDone
            // 
            this.btn_CmdPalletStationDone.Location = new System.Drawing.Point(275, 45);
            this.btn_CmdPalletStationDone.Name = "btn_CmdPalletStationDone";
            this.btn_CmdPalletStationDone.Size = new System.Drawing.Size(262, 36);
            this.btn_CmdPalletStationDone.TabIndex = 4;
            this.btn_CmdPalletStationDone.Text = "Pallet Station Done";
            this.btn_CmdPalletStationDone.UseVisualStyleBackColor = true;
            this.btn_CmdPalletStationDone.Click += new System.EventHandler(this.btn_CmdPalletStationDone_Click);
            // 
            // btn_SkipCheck
            // 
            this.btn_SkipCheck.Location = new System.Drawing.Point(6, 45);
            this.btn_SkipCheck.Name = "btn_SkipCheck";
            this.btn_SkipCheck.Size = new System.Drawing.Size(262, 36);
            this.btn_SkipCheck.TabIndex = 8;
            this.btn_SkipCheck.Text = "Skip Check";
            this.btn_SkipCheck.UseVisualStyleBackColor = true;
            this.btn_SkipCheck.Click += new System.EventHandler(this.btn_SkipCheck_Click);
            // 
            // cmbBox_StationID
            // 
            this.cmbBox_StationID.FormattingEnabled = true;
            this.cmbBox_StationID.Location = new System.Drawing.Point(152, 15);
            this.cmbBox_StationID.Name = "cmbBox_StationID";
            this.cmbBox_StationID.Size = new System.Drawing.Size(383, 24);
            this.cmbBox_StationID.TabIndex = 13;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 19);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 14;
            this.label4.Text = "Station ID:";
            // 
            // btn_GetCurrentPkg
            // 
            this.btn_GetCurrentPkg.Location = new System.Drawing.Point(281, 255);
            this.btn_GetCurrentPkg.Name = "btn_GetCurrentPkg";
            this.btn_GetCurrentPkg.Size = new System.Drawing.Size(262, 36);
            this.btn_GetCurrentPkg.TabIndex = 7;
            this.btn_GetCurrentPkg.Text = "Get Current Package";
            this.btn_GetCurrentPkg.UseVisualStyleBackColor = true;
            this.btn_GetCurrentPkg.Click += new System.EventHandler(this.btn_GetCurrentPkg_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 223);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(115, 16);
            this.label1.TabIndex = 12;
            this.label1.Text = "Serial Number: ";
            // 
            // txtb_SN
            // 
            this.txtb_SN.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtb_SN.Location = new System.Drawing.Point(135, 220);
            this.txtb_SN.Name = "txtb_SN";
            this.txtb_SN.Size = new System.Drawing.Size(406, 22);
            this.txtb_SN.TabIndex = 11;
            // 
            // btn_InserNewPart
            // 
            this.btn_InserNewPart.Location = new System.Drawing.Point(13, 255);
            this.btn_InserNewPart.Name = "btn_InserNewPart";
            this.btn_InserNewPart.Size = new System.Drawing.Size(262, 36);
            this.btn_InserNewPart.TabIndex = 9;
            this.btn_InserNewPart.Text = "Insert New Part";
            this.btn_InserNewPart.UseVisualStyleBackColor = true;
            this.btn_InserNewPart.Click += new System.EventHandler(this.btn_InserNewPart_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(112, 16);
            this.label3.TabIndex = 3;
            this.label3.Text = "Command Sent";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Response";
            // 
            // lbl_Command
            // 
            this.lbl_Command.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Command.BackColor = System.Drawing.Color.White;
            this.lbl_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Command.Location = new System.Drawing.Point(13, 165);
            this.lbl_Command.Name = "lbl_Command";
            this.lbl_Command.Size = new System.Drawing.Size(529, 42);
            this.lbl_Command.TabIndex = 1;
            this.lbl_Command.Text = "...";
            // 
            // lbl_Response
            // 
            this.lbl_Response.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Response.BackColor = System.Drawing.Color.White;
            this.lbl_Response.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Response.Location = new System.Drawing.Point(13, 47);
            this.lbl_Response.Name = "lbl_Response";
            this.lbl_Response.Size = new System.Drawing.Size(529, 87);
            this.lbl_Response.TabIndex = 0;
            this.lbl_Response.Text = "...";
            // 
            // btn_GetPartCount
            // 
            this.btn_GetPartCount.Location = new System.Drawing.Point(13, 297);
            this.btn_GetPartCount.Name = "btn_GetPartCount";
            this.btn_GetPartCount.Size = new System.Drawing.Size(262, 36);
            this.btn_GetPartCount.TabIndex = 20;
            this.btn_GetPartCount.Text = "Get Part Count";
            this.btn_GetPartCount.UseVisualStyleBackColor = true;
            this.btn_GetPartCount.Click += new System.EventHandler(this.btn_GetPartCount_Click);
            // 
            // Server_COMTCP_GT_CentralPC_Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(911, 823);
            this.Controls.Add(this.groupBox_Operations);
            this.Controls.Add(this.btn_Enable);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_ESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Server_COMTCP_GT_CentralPC_Dlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Server Client";
            this.Load += new System.EventHandler(this.Server_COMTCP_GT_CentralPC_Dlg_Load);
            this.groupBox_Operations.ResumeLayout(false);
            this.groupBox_Operations.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Button btn_Update;
		private System.Windows.Forms.Button btn_Enable;
		private System.Windows.Forms.GroupBox groupBox_Operations;
		private System.Windows.Forms.Label lbl_Response;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label lbl_Command;
		private System.Windows.Forms.Button btn_CmdPalletStationDone;
		private System.Windows.Forms.Button btn_UpdateResults;
		private System.Windows.Forms.Button btn_GetUserAccess;
		private System.Windows.Forms.Button btn_InserNewPart;
		private System.Windows.Forms.Button btn_SkipCheck;
		private System.Windows.Forms.Button btn_GetCurrentPkg;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtb_SN;
		private System.Windows.Forms.ComboBox cmbBox_StationID;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ComboBox cmbBox_Results;
        private System.Windows.Forms.Button btn_GetDataFromServer;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbBox_CommandStrings;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtb_Password;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtb_Username;
        private System.Windows.Forms.Button btn_GetPartCount;
	}
}