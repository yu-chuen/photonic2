namespace GT_App
{
	partial class UserCtrl_StationDisplay
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.lbl_StationDisplay = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_StationDisplay
            // 
            this.lbl_StationDisplay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.lbl_StationDisplay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_StationDisplay.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lbl_StationDisplay.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_StationDisplay.Location = new System.Drawing.Point(0, 0);
            this.lbl_StationDisplay.Margin = new System.Windows.Forms.Padding(3);
            this.lbl_StationDisplay.Name = "lbl_StationDisplay";
            this.lbl_StationDisplay.Size = new System.Drawing.Size(406, 206);
            this.lbl_StationDisplay.TabIndex = 0;
            this.lbl_StationDisplay.Text = "label1";
            this.lbl_StationDisplay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserCtrl_StationDisplay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Gray;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_StationDisplay);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_StationDisplay";
            this.Size = new System.Drawing.Size(406, 206);
            this.Load += new System.EventHandler(this.UserCtrl_StationDisplay_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_StationDisplay;
	}
}
