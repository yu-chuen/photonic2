namespace GT_App
{
	partial class Comm_TCP_IFMRFID_MainDlg
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
            this.tabCtrl_ComIFMScannerDlg = new System.Windows.Forms.TabControl();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabCtrl_ComIFMScannerDlg
            // 
            this.tabCtrl_ComIFMScannerDlg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabCtrl_ComIFMScannerDlg.Location = new System.Drawing.Point(12, 11);
            this.tabCtrl_ComIFMScannerDlg.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabCtrl_ComIFMScannerDlg.Name = "tabCtrl_ComIFMScannerDlg";
            this.tabCtrl_ComIFMScannerDlg.SelectedIndex = 0;
            this.tabCtrl_ComIFMScannerDlg.Size = new System.Drawing.Size(651, 482);
            this.tabCtrl_ComIFMScannerDlg.TabIndex = 0;
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(513, 497);
            this.btn_ESC.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(150, 49);
            this.btn_ESC.TabIndex = 1;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // Comm_TCP_IFMRFID_MainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(675, 551);
            this.ControlBox = false;
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.tabCtrl_ComIFMScannerDlg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Comm_TCP_IFMRFID_MainDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "IFM RFID Scanner";
            this.Load += new System.EventHandler(this.Comm_TCP_IFMRFID_MainDlg_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabCtrl_ComIFMScannerDlg;
		private System.Windows.Forms.Button btn_ESC;
	}
}