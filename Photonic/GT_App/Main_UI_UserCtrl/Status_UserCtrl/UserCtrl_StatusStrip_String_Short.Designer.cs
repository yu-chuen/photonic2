namespace GT_App
{
    partial class UserCtrl_StatusStrip_String_Short
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
            this.lbl_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_Label
            // 
            this.lbl_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Label.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Label.Location = new System.Drawing.Point(5, 6);
            this.lbl_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(147, 29);
            this.lbl_Label.TabIndex = 14;
            this.lbl_Label.Text = "...";
            this.lbl_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // UserCtrl_StatusStrip_String_Short
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.lbl_Label);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UserCtrl_StatusStrip_String_Short";
            this.Size = new System.Drawing.Size(156, 41);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_Label;
	}
}
