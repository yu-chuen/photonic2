namespace GT_App
{
    partial class UserCtrl_Custom_HotBlower
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
            this.btn_OP110Test = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OP110Test
            // 
            this.btn_OP110Test.Location = new System.Drawing.Point(20, 25);
            this.btn_OP110Test.Name = "btn_OP110Test";
            this.btn_OP110Test.Size = new System.Drawing.Size(210, 44);
            this.btn_OP110Test.TabIndex = 0;
            this.btn_OP110Test.Text = "Test Hot Blower";
            this.btn_OP110Test.UseVisualStyleBackColor = true;
            this.btn_OP110Test.Click += new System.EventHandler(this.btn_OP110Test_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_OP110Test);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(250, 80);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Hot Blower Subsequence";
            // 
            // UserCtrl_Custom_HotBlower
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Custom_HotBlower";
            this.Size = new System.Drawing.Size(256, 86);
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btn_OP110Test;
        private System.Windows.Forms.GroupBox groupBox1;
	}
}
