namespace GT_App
{
	partial class RobotSetting_APSLib_PanasonicECAT_Dlg
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
            this.btn_UpdateEsc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(379, 474);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(135, 49);
            this.btn_ESC.TabIndex = 22;
            this.btn_ESC.Text = "Esc";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_UpdateEsc
            // 
            this.btn_UpdateEsc.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_UpdateEsc.Location = new System.Drawing.Point(241, 474);
            this.btn_UpdateEsc.Name = "btn_UpdateEsc";
            this.btn_UpdateEsc.Size = new System.Drawing.Size(135, 49);
            this.btn_UpdateEsc.TabIndex = 21;
            this.btn_UpdateEsc.Text = "Update and Esc";
            this.btn_UpdateEsc.UseVisualStyleBackColor = true;
            this.btn_UpdateEsc.Click += new System.EventHandler(this.btn_UpdateEsc_Click);
            // 
            // RobotSetting_APSLib_PanasonicECAT_Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(522, 530);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.btn_UpdateEsc);
            this.Name = "RobotSetting_APSLib_PanasonicECAT_Dlg";
            this.Text = "Panasonic Motor Settings";
            this.Load += new System.EventHandler(this.RobotSetting_APSLib_PanasonicECAT_Dlg_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Button btn_UpdateEsc;
	}
}