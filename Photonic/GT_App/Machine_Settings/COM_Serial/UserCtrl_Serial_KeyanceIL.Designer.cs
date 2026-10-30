namespace GT_App
{
    partial class UserCtrl_Serial_KeyanceIL
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
            this.btn_UpdateSettings = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_ReadCode = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_UpdateSettings
            // 
            this.btn_UpdateSettings.Location = new System.Drawing.Point(476, 316);
            this.btn_UpdateSettings.Name = "btn_UpdateSettings";
            this.btn_UpdateSettings.Size = new System.Drawing.Size(152, 48);
            this.btn_UpdateSettings.TabIndex = 25;
            this.btn_UpdateSettings.Text = "Update Settings";
            this.btn_UpdateSettings.UseVisualStyleBackColor = true;
            this.btn_UpdateSettings.Click += new System.EventHandler(this.btn_UpdateSettings_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Disconnect);
            this.groupBox2.Controls.Add(this.btn_ReadCode);
            this.groupBox2.Controls.Add(this.btn_Connect);
            this.groupBox2.Controls.Add(this.lbl_Response);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(292, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 307);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Operations";
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(16, 240);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(152, 48);
            this.btn_Disconnect.TabIndex = 6;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // btn_ReadCode
            // 
            this.btn_ReadCode.Location = new System.Drawing.Point(184, 184);
            this.btn_ReadCode.Name = "btn_ReadCode";
            this.btn_ReadCode.Size = new System.Drawing.Size(152, 48);
            this.btn_ReadCode.TabIndex = 5;
            this.btn_ReadCode.Text = "Read Value";
            this.btn_ReadCode.UseVisualStyleBackColor = true;
            this.btn_ReadCode.Click += new System.EventHandler(this.btn_ReadCode_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(16, 184);
            this.btn_Connect.Name = "btn_Connect";
            this.btn_Connect.Size = new System.Drawing.Size(152, 48);
            this.btn_Connect.TabIndex = 4;
            this.btn_Connect.Text = "Connect";
            this.btn_Connect.UseVisualStyleBackColor = true;
            this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
            // 
            // lbl_Response
            // 
            this.lbl_Response.BackColor = System.Drawing.Color.White;
            this.lbl_Response.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Response.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Response.Location = new System.Drawing.Point(16, 40);
            this.lbl_Response.Name = "lbl_Response";
            this.lbl_Response.Size = new System.Drawing.Size(320, 136);
            this.lbl_Response.TabIndex = 3;
            this.lbl_Response.Text = "...";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(16, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Response";
            // 
            // UserCtrl_Serial_KeyanceIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_UpdateSettings);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Serial_KeyanceIL";
            this.Size = new System.Drawing.Size(648, 404);
            this.Load += new System.EventHandler(this.UserCtrl_Serial_Screwdriver_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_UpdateSettings;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_ReadCode;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label lbl_Response;
        private System.Windows.Forms.Label label4;
    }
}
