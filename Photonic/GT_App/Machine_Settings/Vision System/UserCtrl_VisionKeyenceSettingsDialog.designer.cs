namespace GT_App
{
    partial class UserCtrl_VisionKeyenceSettingsDialog
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
            this.txtb_IPAddress4 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress1 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress3 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress2 = new System.Windows.Forms.TextBox();
            this.txtb_PortNumber = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_UpdateSettings = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_SendCommand = new System.Windows.Forms.Button();
            this.listView_CommandList = new System.Windows.Forms.ListView();
            this.col_Num = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbl_Command = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.txtb_Command = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.col_Command = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.col_Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtb_IPAddress4
            // 
            this.txtb_IPAddress4.Location = new System.Drawing.Point(253, 34);
            this.txtb_IPAddress4.Name = "txtb_IPAddress4";
            this.txtb_IPAddress4.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress4.TabIndex = 41;
            this.txtb_IPAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress4.TextChanged += new System.EventHandler(this.txtb_IPAddress4_TextChanged);
            // 
            // txtb_IPAddress1
            // 
            this.txtb_IPAddress1.Location = new System.Drawing.Point(118, 34);
            this.txtb_IPAddress1.Name = "txtb_IPAddress1";
            this.txtb_IPAddress1.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress1.TabIndex = 38;
            this.txtb_IPAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress1.TextChanged += new System.EventHandler(this.txtb_IPAddress1_TextChanged);
            // 
            // txtb_IPAddress3
            // 
            this.txtb_IPAddress3.Location = new System.Drawing.Point(208, 34);
            this.txtb_IPAddress3.Name = "txtb_IPAddress3";
            this.txtb_IPAddress3.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress3.TabIndex = 40;
            this.txtb_IPAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress3.TextChanged += new System.EventHandler(this.txtb_IPAddress3_TextChanged);
            // 
            // txtb_IPAddress2
            // 
            this.txtb_IPAddress2.Location = new System.Drawing.Point(163, 34);
            this.txtb_IPAddress2.Name = "txtb_IPAddress2";
            this.txtb_IPAddress2.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress2.TabIndex = 39;
            this.txtb_IPAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress2.TextChanged += new System.EventHandler(this.txtb_IPAddress2_TextChanged);
            // 
            // txtb_PortNumber
            // 
            this.txtb_PortNumber.Location = new System.Drawing.Point(118, 62);
            this.txtb_PortNumber.Name = "txtb_PortNumber";
            this.txtb_PortNumber.Size = new System.Drawing.Size(84, 22);
            this.txtb_PortNumber.TabIndex = 33;
            this.txtb_PortNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(14, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(98, 16);
            this.label3.TabIndex = 31;
            this.label3.Text = "Port Number:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 37);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 16);
            this.label2.TabIndex = 30;
            this.label2.Text = "IP Address:";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_UpdateSettings);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtb_PortNumber);
            this.groupBox1.Controls.Add(this.txtb_IPAddress2);
            this.groupBox1.Controls.Add(this.txtb_IPAddress3);
            this.groupBox1.Controls.Add(this.txtb_IPAddress1);
            this.groupBox1.Controls.Add(this.txtb_IPAddress4);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(527, 101);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Vision Settings";
            // 
            // btn_UpdateSettings
            // 
            this.btn_UpdateSettings.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateSettings.Location = new System.Drawing.Point(348, 40);
            this.btn_UpdateSettings.Name = "btn_UpdateSettings";
            this.btn_UpdateSettings.Size = new System.Drawing.Size(168, 44);
            this.btn_UpdateSettings.TabIndex = 43;
            this.btn_UpdateSettings.Text = "Update Settings";
            this.btn_UpdateSettings.UseVisualStyleBackColor = true;
            this.btn_UpdateSettings.Click += new System.EventHandler(this.btn_UpdateSettings_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Connect.Location = new System.Drawing.Point(15, 40);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(120, 44);
            this.btn_Connect.TabIndex = 41;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Disconnect.Location = new System.Drawing.Point(15, 90);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(120, 44);
            this.btn_Disconnect.TabIndex = 42;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_SendCommand);
            this.groupBox2.Controls.Add(this.btn_Disconnect);
            this.groupBox2.Controls.Add(this.listView_CommandList);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_Connect);
            this.groupBox2.Location = new System.Drawing.Point(536, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(476, 393);
            this.groupBox2.TabIndex = 44;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Operations";
            // 
            // btn_SendCommand
            // 
            this.btn_SendCommand.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_SendCommand.Location = new System.Drawing.Point(15, 306);
            this.btn_SendCommand.Name = "btn_SendCommand";
            this.btn_SendCommand.Size = new System.Drawing.Size(120, 75);
            this.btn_SendCommand.TabIndex = 46;
            this.btn_SendCommand.Text = "Send Command";
            this.btn_SendCommand.UseVisualStyleBackColor = true;
            this.btn_SendCommand.Click += new System.EventHandler(this.btn_SendCommand_Click);
            // 
            // listView_CommandList
            // 
            this.listView_CommandList.AutoArrange = false;
            this.listView_CommandList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.col_Num,
            this.col_Command,
            this.col_Description});
            this.listView_CommandList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listView_CommandList.FullRowSelect = true;
            this.listView_CommandList.GridLines = true;
            this.listView_CommandList.HideSelection = false;
            this.listView_CommandList.Location = new System.Drawing.Point(141, 42);
            this.listView_CommandList.MultiSelect = false;
            this.listView_CommandList.Name = "listView_CommandList";
            this.listView_CommandList.Size = new System.Drawing.Size(320, 339);
            this.listView_CommandList.TabIndex = 45;
            this.listView_CommandList.UseCompatibleStateImageBehavior = false;
            this.listView_CommandList.View = System.Windows.Forms.View.Details;
            this.listView_CommandList.SelectedIndexChanged += new System.EventHandler(this.listView_CommandList_SelectedIndexChanged);
            // 
            // col_Num
            // 
            this.col_Num.DisplayIndex = 0;
            this.col_Num.Text = "Num";
            this.col_Num.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_Num.Width = 40;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(138, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(109, 16);
            this.label5.TabIndex = 49;
            this.label5.Text = "Command List:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(3, 213);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(116, 16);
            this.label7.TabIndex = 51;
            this.label7.Text = "Sent Command:";
            // 
            // lbl_Command
            // 
            this.lbl_Command.BackColor = System.Drawing.Color.White;
            this.lbl_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Command.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Command.Location = new System.Drawing.Point(6, 234);
            this.lbl_Command.Name = "lbl_Command";
            this.lbl_Command.Size = new System.Drawing.Size(513, 66);
            this.lbl_Command.TabIndex = 50;
            this.lbl_Command.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(3, 116);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(83, 16);
            this.label6.TabIndex = 48;
            this.label6.Text = "Response:";
            // 
            // lbl_Response
            // 
            this.lbl_Response.BackColor = System.Drawing.Color.White;
            this.lbl_Response.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Response.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Response.Location = new System.Drawing.Point(6, 137);
            this.lbl_Response.Name = "lbl_Response";
            this.lbl_Response.Size = new System.Drawing.Size(513, 66);
            this.lbl_Response.TabIndex = 47;
            this.lbl_Response.Text = "...";
            // 
            // txtb_Command
            // 
            this.txtb_Command.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtb_Command.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_Command.Location = new System.Drawing.Point(6, 330);
            this.txtb_Command.Multiline = true;
            this.txtb_Command.Name = "txtb_Command";
            this.txtb_Command.Size = new System.Drawing.Size(513, 66);
            this.txtb_Command.TabIndex = 52;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 309);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 16);
            this.label1.TabIndex = 53;
            this.label1.Text = "Command:";
            // 
            // col_Command
            // 
            this.col_Command.DisplayIndex = 1;
            this.col_Command.Text = "Command";
            this.col_Command.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.col_Command.Width = 70;
            // 
            // col_Description
            // 
            this.col_Description.DisplayIndex = 2;
            this.col_Description.Text = "Description";
            this.col_Description.Width = 190;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DisplayIndex = 3;
            this.columnHeader1.Width = 0;
            // 
            // UserCtrl_VisionKeyenceSettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtb_Command);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lbl_Command);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.lbl_Response);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_VisionKeyenceSettingsDialog";
            this.Size = new System.Drawing.Size(1015, 403);
            this.Load += new System.EventHandler(this.UserCtrl_VisionSettingsDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_IPAddress4;
        private System.Windows.Forms.TextBox txtb_IPAddress1;
        private System.Windows.Forms.TextBox txtb_IPAddress3;
        private System.Windows.Forms.TextBox txtb_IPAddress2;
        private System.Windows.Forms.TextBox txtb_PortNumber;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_UpdateSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ListView listView_CommandList;
        private System.Windows.Forms.Button btn_SendCommand;
        private System.Windows.Forms.Label lbl_Response;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ColumnHeader col_Num;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lbl_Command;
        private System.Windows.Forms.TextBox txtb_Command;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader col_Command;
        private System.Windows.Forms.ColumnHeader col_Description;
        private System.Windows.Forms.ColumnHeader columnHeader1;
    }
}
