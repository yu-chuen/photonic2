namespace GT_App
{
    partial class Thermocouple_Serial_MainDialog
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
            this.btn_UpdateSettings = new System.Windows.Forms.Button();
            this.groupBox_Operations = new System.Windows.Forms.GroupBox();
            this.btn_DecodeResponse = new System.Windows.Forms.Button();
            this.btn_Disconnect = new System.Windows.Forms.Button();
            this.btn_ReadTemperature = new System.Windows.Forms.Button();
            this.btn_Connect = new System.Windows.Forms.Button();
            this.lbl_Response = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.groupBox_Operations.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(527, 271);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(152, 48);
            this.btn_ESC.TabIndex = 25;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_UpdateSettings
            // 
            this.btn_UpdateSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_UpdateSettings.Location = new System.Drawing.Point(369, 271);
            this.btn_UpdateSettings.Name = "btn_UpdateSettings";
            this.btn_UpdateSettings.Size = new System.Drawing.Size(152, 48);
            this.btn_UpdateSettings.TabIndex = 27;
            this.btn_UpdateSettings.Text = "Update Settings";
            this.btn_UpdateSettings.UseVisualStyleBackColor = true;
            this.btn_UpdateSettings.Click += new System.EventHandler(this.btn_UpdateSettings_Click);
            // 
            // groupBox_Operations
            // 
            this.groupBox_Operations.Controls.Add(this.btn_DecodeResponse);
            this.groupBox_Operations.Controls.Add(this.btn_Disconnect);
            this.groupBox_Operations.Controls.Add(this.btn_ReadTemperature);
            this.groupBox_Operations.Controls.Add(this.btn_Connect);
            this.groupBox_Operations.Controls.Add(this.lbl_Response);
            this.groupBox_Operations.Controls.Add(this.label4);
            this.groupBox_Operations.Location = new System.Drawing.Point(12, 12);
            this.groupBox_Operations.Name = "groupBox_Operations";
            this.groupBox_Operations.Size = new System.Drawing.Size(349, 306);
            this.groupBox_Operations.TabIndex = 26;
            this.groupBox_Operations.TabStop = false;
            this.groupBox_Operations.Text = "Operations";
            // 
            // btn_DecodeResponse
            // 
            this.btn_DecodeResponse.Location = new System.Drawing.Point(177, 245);
            this.btn_DecodeResponse.Name = "btn_DecodeResponse";
            this.btn_DecodeResponse.Size = new System.Drawing.Size(152, 48);
            this.btn_DecodeResponse.TabIndex = 7;
            this.btn_DecodeResponse.Text = "Decode Response";
            this.btn_DecodeResponse.UseVisualStyleBackColor = true;
            this.btn_DecodeResponse.Visible = false;
            // 
            // btn_Disconnect
            // 
            this.btn_Disconnect.Location = new System.Drawing.Point(19, 245);
            this.btn_Disconnect.Name = "btn_Disconnect";
            this.btn_Disconnect.Size = new System.Drawing.Size(152, 48);
            this.btn_Disconnect.TabIndex = 6;
            this.btn_Disconnect.Text = "Disconnect";
            this.btn_Disconnect.UseVisualStyleBackColor = true;
            this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
            // 
            // btn_ReadTemperature
            // 
            this.btn_ReadTemperature.Location = new System.Drawing.Point(177, 191);
            this.btn_ReadTemperature.Name = "btn_ReadTemperature";
            this.btn_ReadTemperature.Size = new System.Drawing.Size(152, 48);
            this.btn_ReadTemperature.TabIndex = 5;
            this.btn_ReadTemperature.Text = "Read Temperature";
            this.btn_ReadTemperature.UseVisualStyleBackColor = true;
            this.btn_ReadTemperature.Click += new System.EventHandler(this.btn_ReadTemperature_Click);
            // 
            // btn_Connect
            // 
            this.btn_Connect.Location = new System.Drawing.Point(19, 191);
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
            this.lbl_Response.Font = new System.Drawing.Font("Times New Roman", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Response.Location = new System.Drawing.Point(14, 47);
            this.lbl_Response.Name = "lbl_Response";
            this.lbl_Response.Size = new System.Drawing.Size(320, 136);
            this.lbl_Response.TabIndex = 3;
            this.lbl_Response.Text = "...";
            this.lbl_Response.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(14, 27);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(79, 16);
            this.label4.TabIndex = 2;
            this.label4.Text = "Response";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            // 
            // Thermocouple_Serial_MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(691, 325);
            this.ControlBox = false;
            this.Controls.Add(this.btn_UpdateSettings);
            this.Controls.Add(this.groupBox_Operations);
            this.Controls.Add(this.btn_ESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Thermocouple_Serial_MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thermocouple Settings";
            this.Load += new System.EventHandler(this.Thermocouple_Serial_MainDialog_Load);
            this.groupBox_Operations.ResumeLayout(false);
            this.groupBox_Operations.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_UpdateSettings;
        private System.Windows.Forms.GroupBox groupBox_Operations;
        private System.Windows.Forms.Button btn_DecodeResponse;
        private System.Windows.Forms.Button btn_Disconnect;
        private System.Windows.Forms.Button btn_ReadTemperature;
        private System.Windows.Forms.Button btn_Connect;
        private System.Windows.Forms.Label lbl_Response;
        private System.Windows.Forms.Label label4;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;

    }
}