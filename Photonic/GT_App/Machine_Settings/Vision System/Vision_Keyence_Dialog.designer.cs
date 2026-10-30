namespace GT_App
{
    partial class Vision_Keyence_Dialog
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
            this.TabCtrl_VisionDlg = new System.Windows.Forms.TabControl();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // TabCtrl_VisionDlg
            // 
            this.TabCtrl_VisionDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabCtrl_VisionDlg.Location = new System.Drawing.Point(12, 12);
            this.TabCtrl_VisionDlg.Name = "TabCtrl_VisionDlg";
            this.TabCtrl_VisionDlg.SelectedIndex = 0;
            this.TabCtrl_VisionDlg.Size = new System.Drawing.Size(1073, 630);
            this.TabCtrl_VisionDlg.TabIndex = 0;
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(938, 647);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(147, 49);
            this.btn_ESC.TabIndex = 1;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // Vision_Keyence_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(1097, 708);
            this.ControlBox = false;
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.TabCtrl_VisionDlg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Vision_Keyence_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vision Keyence Settings";
            this.Load += new System.EventHandler(this.Vision_ControlDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl TabCtrl_VisionDlg;
		private System.Windows.Forms.Button btn_ESC;

    }
}