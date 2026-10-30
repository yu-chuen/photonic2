namespace GT_App
{
    partial class Comm_TCP_IFMRFID_Dialog
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
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_ReadTag = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.txtb_ReadData = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtb_Command = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb_ReadCmd = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.txtb_PortNum = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtb_CommandDelay = new System.Windows.Forms.TextBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label8 = new System.Windows.Forms.Label();
            this.cb_ByteLength = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cb_RFIDReader = new System.Windows.Forms.ComboBox();
            this.txtb_Serial = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtb_WriteCmd = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.btn_Enable = new System.Windows.Forms.Button();
            this.txtb_IPAddress4 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress1 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress3 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress2 = new System.Windows.Forms.TextBox();
            this.btn_WriteTag = new System.Windows.Forms.Button();
            this.btn_ReaderEnable1 = new System.Windows.Forms.Button();
            this.btn_ReaderEnable2 = new System.Windows.Forms.Button();
            this.btn_ReaderEnable3 = new System.Windows.Forms.Button();
            this.btn_ReaderEnable4 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_Connect
            // 
            this.btn_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Connect.Location = new System.Drawing.Point(12, 160);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(144, 49);
            this.btn_Connect.TabIndex = 0;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_ReadTag
            // 
            this.btn_ReadTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ReadTag.Location = new System.Drawing.Point(157, 160);
            this.btn_ReadTag.Name = "btn_ReadTag";
            this.btn_ReadTag.Size = new System.Drawing.Size(144, 49);
            this.btn_ReadTag.TabIndex = 1;
            this.btn_ReadTag.Text = "Read From tag";
            this.btn_ReadTag.UseVisualStyleBackColor = true;
            this.btn_ReadTag.Click += new System.EventHandler(this.btn_ReadTag_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Disconnect.Location = new System.Drawing.Point(447, 160);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(144, 49);
            this.btn_Disconnect.TabIndex = 2;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // txtb_ReadData
            // 
            this.txtb_ReadData.Location = new System.Drawing.Point(12, 35);
            this.txtb_ReadData.Multiline = true;
            this.txtb_ReadData.Name = "txtb_ReadData";
            this.txtb_ReadData.Size = new System.Drawing.Size(579, 59);
            this.txtb_ReadData.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 16);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 16);
            this.label1.TabIndex = 4;
            this.label1.Text = "Response";
            // 
            // txtb_Command
            // 
            this.txtb_Command.Location = new System.Drawing.Point(12, 125);
            this.txtb_Command.Multiline = true;
            this.txtb_Command.Name = "txtb_Command";
            this.txtb_Command.Size = new System.Drawing.Size(579, 29);
            this.txtb_Command.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(77, 16);
            this.label2.TabIndex = 6;
            this.label2.Text = "Command";
            // 
            // txtb_ReadCmd
            // 
            this.txtb_ReadCmd.Location = new System.Drawing.Point(12, 212);
            this.txtb_ReadCmd.Name = "txtb_ReadCmd";
            this.txtb_ReadCmd.Size = new System.Drawing.Size(259, 22);
            this.txtb_ReadCmd.TabIndex = 8;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(277, 215);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(119, 16);
            this.label3.TabIndex = 9;
            this.label3.Text = "Read Command";
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ESC.Location = new System.Drawing.Point(454, 411);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(144, 49);
            this.btn_ESC.TabIndex = 11;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Update.Location = new System.Drawing.Point(454, 356);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(144, 49);
            this.btn_Update.TabIndex = 12;
            this.btn_Update.Text = "Update and ESC";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // txtb_PortNum
            // 
            this.txtb_PortNum.Location = new System.Drawing.Point(110, 59);
            this.txtb_PortNum.Name = "txtb_PortNum";
            this.txtb_PortNum.Size = new System.Drawing.Size(55, 22);
            this.txtb_PortNum.TabIndex = 14;
            this.txtb_PortNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_PortNum.TextChanged += new System.EventHandler(this.txtb_PortNum_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(16, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "IP Address:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(6, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Port Number:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(11, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Send Delay:";
            // 
            // txtb_CommandDelay
            // 
            this.txtb_CommandDelay.Location = new System.Drawing.Point(110, 87);
            this.txtb_CommandDelay.Name = "txtb_CommandDelay";
            this.txtb_CommandDelay.Size = new System.Drawing.Size(55, 22);
            this.txtb_CommandDelay.TabIndex = 17;
            this.txtb_CommandDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_CommandDelay.TextChanged += new System.EventHandler(this.txtb_CommandDelay_TextChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.cb_ByteLength);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.cb_RFIDReader);
            this.groupBox1.Controls.Add(this.txtb_Serial);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtb_WriteCmd);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btn_Enable);
            this.groupBox1.Controls.Add(this.txtb_IPAddress4);
            this.groupBox1.Controls.Add(this.txtb_IPAddress1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtb_IPAddress3);
            this.groupBox1.Controls.Add(this.txtb_ReadCmd);
            this.groupBox1.Controls.Add(this.txtb_IPAddress2);
            this.groupBox1.Controls.Add(this.txtb_CommandDelay);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtb_PortNum);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(12, 217);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(435, 243);
            this.groupBox1.TabIndex = 19;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Settings";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(247, 102);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(89, 16);
            this.label8.TabIndex = 34;
            this.label8.Text = "Byte Length";
            // 
            // cb_ByteLength
            // 
            this.cb_ByteLength.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_ByteLength.FormattingEnabled = true;
            this.cb_ByteLength.Location = new System.Drawing.Point(351, 99);
            this.cb_ByteLength.Name = "cb_ByteLength";
            this.cb_ByteLength.Size = new System.Drawing.Size(71, 24);
            this.cb_ByteLength.TabIndex = 33;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(247, 70);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 16);
            this.label4.TabIndex = 32;
            this.label4.Text = "RFID Reader";
            // 
            // cb_RFIDReader
            // 
            this.cb_RFIDReader.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cb_RFIDReader.FormattingEnabled = true;
            this.cb_RFIDReader.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4"});
            this.cb_RFIDReader.Location = new System.Drawing.Point(351, 67);
            this.cb_RFIDReader.Name = "cb_RFIDReader";
            this.cb_RFIDReader.Size = new System.Drawing.Size(71, 24);
            this.cb_RFIDReader.TabIndex = 31;
            // 
            // txtb_Serial
            // 
            this.txtb_Serial.Location = new System.Drawing.Point(12, 155);
            this.txtb_Serial.Name = "txtb_Serial";
            this.txtb_Serial.Size = new System.Drawing.Size(259, 22);
            this.txtb_Serial.TabIndex = 30;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(277, 158);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(77, 16);
            this.label9.TabIndex = 29;
            this.label9.Text = "Serial No.";
            // 
            // txtb_WriteCmd
            // 
            this.txtb_WriteCmd.Location = new System.Drawing.Point(12, 184);
            this.txtb_WriteCmd.Name = "txtb_WriteCmd";
            this.txtb_WriteCmd.Size = new System.Drawing.Size(259, 22);
            this.txtb_WriteCmd.TabIndex = 24;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(277, 187);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(117, 16);
            this.label11.TabIndex = 25;
            this.label11.Text = "Write Command";
            // 
            // btn_Enable
            // 
            this.btn_Enable.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Enable.Location = new System.Drawing.Point(12, 117);
            this.btn_Enable.Name = "btn_Enable";
            this.btn_Enable.Size = new System.Drawing.Size(156, 31);
            this.btn_Enable.TabIndex = 20;
            this.btn_Enable.Text = "Enable";
            this.btn_Enable.UseVisualStyleBackColor = true;
            this.btn_Enable.Click += new System.EventHandler(this.btn_Enable_Click);
            // 
            // txtb_IPAddress4
            // 
            this.txtb_IPAddress4.Location = new System.Drawing.Point(244, 31);
            this.txtb_IPAddress4.Name = "txtb_IPAddress4";
            this.txtb_IPAddress4.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress4.TabIndex = 23;
            this.txtb_IPAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress4.TextChanged += new System.EventHandler(this.txtb_IPAddress4_TextChanged);
            // 
            // txtb_IPAddress1
            // 
            this.txtb_IPAddress1.Location = new System.Drawing.Point(109, 31);
            this.txtb_IPAddress1.Name = "txtb_IPAddress1";
            this.txtb_IPAddress1.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress1.TabIndex = 22;
            this.txtb_IPAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress1.TextChanged += new System.EventHandler(this.txtb_IPAddress1_TextChanged);
            // 
            // txtb_IPAddress3
            // 
            this.txtb_IPAddress3.Location = new System.Drawing.Point(199, 31);
            this.txtb_IPAddress3.Name = "txtb_IPAddress3";
            this.txtb_IPAddress3.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress3.TabIndex = 21;
            this.txtb_IPAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress3.TextChanged += new System.EventHandler(this.txtb_IPAddress3_TextChanged);
            // 
            // txtb_IPAddress2
            // 
            this.txtb_IPAddress2.Location = new System.Drawing.Point(154, 31);
            this.txtb_IPAddress2.Name = "txtb_IPAddress2";
            this.txtb_IPAddress2.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress2.TabIndex = 20;
            this.txtb_IPAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress2.TextChanged += new System.EventHandler(this.txtb_IPAddress2_TextChanged);
            // 
            // btn_WriteTag
            // 
            this.btn_WriteTag.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_WriteTag.Location = new System.Drawing.Point(302, 160);
            this.btn_WriteTag.Name = "btn_WriteTag";
            this.btn_WriteTag.Size = new System.Drawing.Size(144, 49);
            this.btn_WriteTag.TabIndex = 20;
            this.btn_WriteTag.Text = "Write to tag";
            this.btn_WriteTag.UseVisualStyleBackColor = true;
            this.btn_WriteTag.Click += new System.EventHandler(this.btn_WriteTag_Click);
            // 
            // btn_ReaderEnable1
            // 
            this.btn_ReaderEnable1.Location = new System.Drawing.Point(460, 227);
            this.btn_ReaderEnable1.Name = "btn_ReaderEnable1";
            this.btn_ReaderEnable1.Size = new System.Drawing.Size(128, 29);
            this.btn_ReaderEnable1.TabIndex = 21;
            this.btn_ReaderEnable1.Text = "Reader 1 Enabled";
            this.btn_ReaderEnable1.UseVisualStyleBackColor = true;
            this.btn_ReaderEnable1.Click += new System.EventHandler(this.btn_ReaderEnable1_Click);
            // 
            // btn_ReaderEnable2
            // 
            this.btn_ReaderEnable2.Location = new System.Drawing.Point(460, 258);
            this.btn_ReaderEnable2.Name = "btn_ReaderEnable2";
            this.btn_ReaderEnable2.Size = new System.Drawing.Size(128, 29);
            this.btn_ReaderEnable2.TabIndex = 22;
            this.btn_ReaderEnable2.Text = "Reader 2 Enabled";
            this.btn_ReaderEnable2.UseVisualStyleBackColor = true;
            this.btn_ReaderEnable2.Click += new System.EventHandler(this.btn_ReaderEnable2_Click);
            // 
            // btn_ReaderEnable3
            // 
            this.btn_ReaderEnable3.Location = new System.Drawing.Point(460, 289);
            this.btn_ReaderEnable3.Name = "btn_ReaderEnable3";
            this.btn_ReaderEnable3.Size = new System.Drawing.Size(128, 29);
            this.btn_ReaderEnable3.TabIndex = 23;
            this.btn_ReaderEnable3.Text = "Reader 3 Enabled";
            this.btn_ReaderEnable3.UseVisualStyleBackColor = true;
            this.btn_ReaderEnable3.Click += new System.EventHandler(this.btn_ReaderEnable3_Click);
            // 
            // btn_ReaderEnable4
            // 
            this.btn_ReaderEnable4.Location = new System.Drawing.Point(460, 320);
            this.btn_ReaderEnable4.Name = "btn_ReaderEnable4";
            this.btn_ReaderEnable4.Size = new System.Drawing.Size(128, 29);
            this.btn_ReaderEnable4.TabIndex = 24;
            this.btn_ReaderEnable4.Text = "Reader 4 Enabled";
            this.btn_ReaderEnable4.UseVisualStyleBackColor = true;
            this.btn_ReaderEnable4.Click += new System.EventHandler(this.btn_ReaderEnable4_Click);
            // 
            // Comm_TCP_IFMRFID_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(606, 470);
            this.Controls.Add(this.btn_ReaderEnable4);
            this.Controls.Add(this.btn_ReaderEnable3);
            this.Controls.Add(this.btn_ReaderEnable2);
            this.Controls.Add(this.btn_ReaderEnable1);
            this.Controls.Add(this.btn_WriteTag);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtb_Command);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtb_ReadData);
            this.Controls.Add(this.btn_Disconnect);
            this.Controls.Add(this.btn_ReadTag);
            this.Controls.Add(this.btn_Connect);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Comm_TCP_IFMRFID_Dialog";
            this.Text = "IFM RFID Reader";
            this.Load += new System.EventHandler(this.Comm_TCP_IFMRFID_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_ReadTag;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.TextBox txtb_ReadData;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtb_Command;
		private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_ReadCmd;
		private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_Update;
        private System.Windows.Forms.TextBox txtb_PortNum;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtb_CommandDelay;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtb_IPAddress4;
        private System.Windows.Forms.TextBox txtb_IPAddress1;
        private System.Windows.Forms.TextBox txtb_IPAddress3;
        private System.Windows.Forms.TextBox txtb_IPAddress2;
        private System.Windows.Forms.Button btn_Enable;
		private System.Windows.Forms.Button btn_WriteTag;
		private System.Windows.Forms.TextBox txtb_Serial;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.TextBox txtb_WriteCmd;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.ComboBox cb_ByteLength;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox cb_RFIDReader;
		private System.Windows.Forms.Button btn_ReaderEnable1;
		private System.Windows.Forms.Button btn_ReaderEnable2;
		private System.Windows.Forms.Button btn_ReaderEnable3;
        private System.Windows.Forms.Button btn_ReaderEnable4;
    }
}