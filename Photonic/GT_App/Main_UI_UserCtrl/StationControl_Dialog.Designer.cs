namespace GT_App
{
	partial class StationControl_Dialog
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
            this.bkgWorker_UpdateUI = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(665, 409);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(147, 48);
            this.btn_ESC.TabIndex = 2;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // bkgWorker_UpdateUI
            // 
            this.bkgWorker_UpdateUI.WorkerSupportsCancellation = true;
            this.bkgWorker_UpdateUI.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bkgWorker_UpdateUI_DoWork);
            // 
            // StationControl_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(820, 464);
            this.ControlBox = false;
            this.Controls.Add(this.btn_ESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "StationControl_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Station Control";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.StationControl_Dialog_FormClosing);
            this.Load += new System.EventHandler(this.StationControl_Dialog_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_ESC;
		private System.ComponentModel.BackgroundWorker bkgWorker_UpdateUI;
	}
}