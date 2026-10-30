namespace GT_App
{
    partial class Keyence_GT2
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
            this.btn_UpdateESC = new System.Windows.Forms.Button();
            this.gb_CommandSettings = new System.Windows.Forms.GroupBox();
            this.btn_BarcodeEnable_002 = new System.Windows.Forms.Button();
            this.btn_BarcodeEnable_001 = new System.Windows.Forms.Button();
            this.txtb_EndTrigger = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtb_Trigger = new System.Windows.Forms.TextBox();
            this.txtb_BC2Threshold = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtb_BC1Threshold = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_IsConnected = new System.Windows.Forms.Button();
            this.btn_DecodeResponse = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_ReadValue = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.gb_CommandSettings.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(528, 422);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(168, 45);
            this.btn_ESC.TabIndex = 14;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_UpdateESC
            // 
            this.btn_UpdateESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_UpdateESC.Location = new System.Drawing.Point(354, 422);
            this.btn_UpdateESC.Name = "btn_UpdateESC";
            this.btn_UpdateESC.Size = new System.Drawing.Size(168, 45);
            this.btn_UpdateESC.TabIndex = 15;
            this.btn_UpdateESC.Text = "Update";
            this.btn_UpdateESC.UseVisualStyleBackColor = true;
            this.btn_UpdateESC.Click += new System.EventHandler(this.btn_UpdateESC_Click);
            // 
            // gb_CommandSettings
            // 
            this.gb_CommandSettings.Controls.Add(this.btn_BarcodeEnable_002);
            this.gb_CommandSettings.Controls.Add(this.btn_BarcodeEnable_001);
            this.gb_CommandSettings.Controls.Add(this.txtb_EndTrigger);
            this.gb_CommandSettings.Controls.Add(this.label2);
            this.gb_CommandSettings.Controls.Add(this.label1);
            this.gb_CommandSettings.Controls.Add(this.txtb_Trigger);
            this.gb_CommandSettings.Location = new System.Drawing.Point(8, 8);
            this.gb_CommandSettings.Name = "gb_CommandSettings";
            this.gb_CommandSettings.Size = new System.Drawing.Size(320, 247);
            this.gb_CommandSettings.TabIndex = 16;
            this.gb_CommandSettings.TabStop = false;
            this.gb_CommandSettings.Text = "Module Settings";
            // 
            // btn_BarcodeEnable_002
            // 
            this.btn_BarcodeEnable_002.Location = new System.Drawing.Point(23, 201);
            this.btn_BarcodeEnable_002.Name = "btn_BarcodeEnable_002";
            this.btn_BarcodeEnable_002.Size = new System.Drawing.Size(250, 34);
            this.btn_BarcodeEnable_002.TabIndex = 9;
            this.btn_BarcodeEnable_002.Text = "Barcode 2 Enabled";
            this.btn_BarcodeEnable_002.UseVisualStyleBackColor = true;
            this.btn_BarcodeEnable_002.Visible = false;
            this.btn_BarcodeEnable_002.Click += new System.EventHandler(this.btn_BarcodeEnable_002_Click);
            // 
            // btn_BarcodeEnable_001
            // 
            this.btn_BarcodeEnable_001.Location = new System.Drawing.Point(23, 162);
            this.btn_BarcodeEnable_001.Name = "btn_BarcodeEnable_001";
            this.btn_BarcodeEnable_001.Size = new System.Drawing.Size(250, 34);
            this.btn_BarcodeEnable_001.TabIndex = 8;
            this.btn_BarcodeEnable_001.Text = "Barcode 1 Enabled";
            this.btn_BarcodeEnable_001.UseVisualStyleBackColor = true;
            this.btn_BarcodeEnable_001.Visible = false;
            this.btn_BarcodeEnable_001.Click += new System.EventHandler(this.btn_BarcodeEnable_001_Click);
            // 
            // txtb_EndTrigger
            // 
            this.txtb_EndTrigger.Location = new System.Drawing.Point(120, 64);
            this.txtb_EndTrigger.Name = "txtb_EndTrigger";
            this.txtb_EndTrigger.Size = new System.Drawing.Size(152, 22);
            this.txtb_EndTrigger.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(24, 64);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "End Trigger:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Trigger:";
            // 
            // txtb_Trigger
            // 
            this.txtb_Trigger.Location = new System.Drawing.Point(120, 32);
            this.txtb_Trigger.Name = "txtb_Trigger";
            this.txtb_Trigger.Size = new System.Drawing.Size(152, 22);
            this.txtb_Trigger.TabIndex = 0;
            // 
            // txtb_BC2Threshold
            // 
            this.txtb_BC2Threshold.Location = new System.Drawing.Point(0, 0);
            this.txtb_BC2Threshold.Name = "txtb_BC2Threshold";
            this.txtb_BC2Threshold.Size = new System.Drawing.Size(100, 20);
            this.txtb_BC2Threshold.TabIndex = 0;
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 23);
            this.label5.TabIndex = 0;
            // 
            // txtb_BC1Threshold
            // 
            this.txtb_BC1Threshold.Location = new System.Drawing.Point(0, 0);
            this.txtb_BC1Threshold.Name = "txtb_BC1Threshold";
            this.txtb_BC1Threshold.Size = new System.Drawing.Size(100, 20);
            this.txtb_BC1Threshold.TabIndex = 0;
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(0, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 23);
            this.label3.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_IsConnected);
            this.groupBox2.Controls.Add(this.btn_DecodeResponse);
            this.groupBox2.Controls.Add(this.btn_Disconnect);
            this.groupBox2.Controls.Add(this.btn_ReadValue);
            this.groupBox2.Controls.Add(this.btn_Connect);
            this.groupBox2.Controls.Add(this.lbl_Response);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Location = new System.Drawing.Point(344, 8);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(349, 348);
            this.groupBox2.TabIndex = 17;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Test Operations";
            // 
            // btn_IsConnected
            // 
            this.btn_IsConnected.Location = new System.Drawing.Point(17, 293);
            this.btn_IsConnected.Name = "btn_IsConnected";
            this.btn_IsConnected.Size = new System.Drawing.Size(152, 48);
            this.btn_IsConnected.TabIndex = 8;
            this.btn_IsConnected.Text = "Check Connection";
            this.btn_IsConnected.UseVisualStyleBackColor = true;
            this.btn_IsConnected.Click += new System.EventHandler(this.btn_IsConnected_Click);
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
            // btn_ReadValue
            // 
            this.btn_ReadValue.Location = new System.Drawing.Point(184, 184);
            this.btn_ReadValue.Name = "btn_ReadValue";
            this.btn_ReadValue.Size = new System.Drawing.Size(152, 48);
            this.btn_ReadValue.TabIndex = 5;
            this.btn_ReadValue.Text = "Read Value";
            this.btn_ReadValue.UseVisualStyleBackColor = true;
            this.btn_ReadValue.Click += new System.EventHandler(this.btn_ReadCode_Click);
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
            // Keyence_GT2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(709, 476);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.gb_CommandSettings);
            this.Controls.Add(this.btn_UpdateESC);
            this.Controls.Add(this.btn_ESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Keyence_GT2";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Keyence IL";
            this.Load += new System.EventHandler(this.Keyence_SR1000Reader_Dialog_Load);
            this.gb_CommandSettings.ResumeLayout(false);
            this.gb_CommandSettings.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_UpdateESC;
        private System.Windows.Forms.GroupBox gb_CommandSettings;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtb_Trigger;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_EndTrigger;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_Response;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btn_ReadValue;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.TextBox txtb_BC2Threshold;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtb_BC1Threshold;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_DecodeResponse;
        private System.Windows.Forms.Button btn_IsConnected;
        private System.Windows.Forms.Button btn_BarcodeEnable_002;
        private System.Windows.Forms.Button btn_BarcodeEnable_001;
    }
}