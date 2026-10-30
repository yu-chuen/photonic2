namespace GT_App
{
	partial class UserCtrl_ProductionNInstruction_MainDlg
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
			this.tabcontrol_Main = new System.Windows.Forms.TabControl();
			this.SuspendLayout();
			// 
			// tabcontrol_Main
			// 
			this.tabcontrol_Main.Alignment = System.Windows.Forms.TabAlignment.Left;
			this.tabcontrol_Main.HotTrack = true;
			this.tabcontrol_Main.ItemSize = new System.Drawing.Size(150, 40);
			this.tabcontrol_Main.Location = new System.Drawing.Point(7, 7);
			this.tabcontrol_Main.Multiline = true;
			this.tabcontrol_Main.Name = "tabcontrol_Main";
			this.tabcontrol_Main.SelectedIndex = 0;
			this.tabcontrol_Main.Size = new System.Drawing.Size(255, 686);
			this.tabcontrol_Main.TabIndex = 0;
			this.tabcontrol_Main.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabcontrol_Main_DrawItem);
			// 
			// UserCtrl_ProductionNInstruction_MainDlg
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.tabcontrol_Main);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
			this.Name = "UserCtrl_ProductionNInstruction_MainDlg";
			this.Size = new System.Drawing.Size(271, 700);
			this.Load += new System.EventHandler(this.UserCtrl_ProductionNInstruction_MainDlg_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TabControl tabcontrol_Main;
	}
}
