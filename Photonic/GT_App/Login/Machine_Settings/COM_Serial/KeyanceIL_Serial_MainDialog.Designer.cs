namespace GT_App
{
    partial class KeyanceIL_Serial_MainDialog
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
            this.tabCtrl_CodeReaderSetting = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(522, 445);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(152, 48);
            this.btn_ESC.TabIndex = 25;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // tabCtrl_CodeReaderSetting
            // 
            this.tabCtrl_CodeReaderSetting.Location = new System.Drawing.Point(6, 6);
            this.tabCtrl_CodeReaderSetting.Name = "tabCtrl_CodeReaderSetting";
            this.tabCtrl_CodeReaderSetting.SelectedIndex = 0;
            this.tabCtrl_CodeReaderSetting.Size = new System.Drawing.Size(670, 430);
            this.tabCtrl_CodeReaderSetting.TabIndex = 24;
            // 
            // Thermocouple_Serial_MainDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(681, 499);
            this.Controls.Add(this.tabCtrl_CodeReaderSetting);
            this.Controls.Add(this.btn_ESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Thermocouple_Serial_MainDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Thermocouple Setting";
            this.Load += new System.EventHandler(this.Screwdriver_Serial_MainDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.TabControl tabCtrl_CodeReaderSetting;

    }
}