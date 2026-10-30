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
            this.btn_BarcodeEnable = new System.Windows.Forms.Button();
            this.txtb_BCThreshold = new System.Windows.Forms.TextBox();
            this.btn_UpdateSettings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.txtb_EndTrigger = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtb_Trigger = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_DecodeResponse = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_ReadCode = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_CommandSettings = new System.Windows.Forms.GroupBox();
            this.groupBox2.SuspendLayout();
            this.gb_CommandSettings.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_BarcodeEnable
            // 
            this.btn_BarcodeEnable.Location = new System.Drawing.Point(23, 108);
            this.btn_BarcodeEnable.Name = "btn_BarcodeEnable";
            this.btn_BarcodeEnable.Size = new System.Drawing.Size(250, 34);
            this.btn_BarcodeEnable.TabIndex = 8;
            this.btn_BarcodeEnable.Text = "Barcode Enabled";
            this.btn_BarcodeEnable.UseVisualStyleBackColor = true;
            this.btn_BarcodeEnable.Click += new System.EventHandler(this.btn_BarcodeEnable_Click);
            // 
            // txtb_BCThreshold
            // 
            this.txtb_BCThreshold.Location = new System.Drawing.Point(192, 79);
            this.txtb_BCThreshold.Name = "txtb_BCThreshold";
            this.txtb_BCThreshold.Size = new System.Drawing.Size(80, 22);
            this.txtb_BCThreshold.TabIndex = 5;
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
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(149, 16);
            this.label3.TabIndex = 4;
            this.label3.Text = "Barcode Threshold: ";
            // 
            // txtb_EndTrigger
            // 
            this.txtb_EndTrigger.Location = new System.Drawing.Point(120, 51);
            this.txtb_EndTrigger.Name = "txtb_EndTrigger";
            this.txtb_EndTrigger.Size = new System.Drawing.Size(152, 22);
            this.txtb_EndTrigger.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "End Trigger:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trigger:";
            // 
            // txtb_Trigger
            // 
            this.txtb_Trigger.Location = new System.Drawing.Point(120, 25);
            this.txtb_Trigger.Name = "txtb_Trigger";
            this.txtb_Trigger.Size = new System.Drawing.Size(152, 22);
            this.txtb_Trigger.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_DecodeResponse);
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
            // btn_DecodeResponse
            // 
            this.btn_DecodeResponse.Location = new System.Drawing.Point(184, 240);
            this.btn_DecodeResponse.Name = "btn_DecodeResponse";
            this.btn_DecodeResponse.Size = new System.Drawing.Size(152, 48);
            this.btn_DecodeResponse.TabIndex = 7;
            this.btn_DecodeResponse.Text = "Decode Response";
            this.btn_DecodeResponse.UseVisualStyleBackColor = true;
            this.btn_DecodeResponse.Visible = false;
            this.btn_DecodeResponse.Click += new System.EventHandler(this.btn_DecodeResponse_Click);
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
            this.btn_ReadCode.Text = "Read Code";
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
            // gb_CommandSettings
            // 
            this.gb_CommandSettings.Controls.Add(this.btn_BarcodeEnable);
            this.gb_CommandSettings.Controls.Add(this.txtb_BCThreshold);
            this.gb_CommandSettings.Controls.Add(this.label3);
            this.gb_CommandSettings.Controls.Add(this.txtb_EndTrigger);
            this.gb_CommandSettings.Controls.Add(this.label2);
            this.gb_CommandSettings.Controls.Add(this.label1);
            this.gb_CommandSettings.Controls.Add(this.txtb_Trigger);
            this.gb_CommandSettings.Location = new System.Drawing.Point(3, 43);
            this.gb_CommandSettings.Name = "gb_CommandSettings";
            this.gb_CommandSettings.Size = new System.Drawing.Size(320, 151);
            this.gb_CommandSettings.TabIndex = 23;
            this.gb_CommandSettings.TabStop = false;
            this.gb_CommandSettings.Text = "Module Settings";
            this.gb_CommandSettings.Visible = false;
            // 
            // UserCtrl_Serial_KeyanceIL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_UpdateSettings);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_CommandSettings);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Serial_KeyanceIL";
            this.Size = new System.Drawing.Size(648, 404);
            this.Load += new System.EventHandler(this.UserCtrl_Serial_Screwdriver_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.gb_CommandSettings.ResumeLayout(false);
            this.gb_CommandSettings.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_BarcodeEnable;
        private System.Windows.Forms.TextBox txtb_BCThreshold;
        private System.Windows.Forms.Button btn_UpdateSettings;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtb_EndTrigger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtb_Trigger;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_DecodeResponse;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_ReadCode;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label lbl_Response;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.GroupBox gb_CommandSettings;
    }
}
