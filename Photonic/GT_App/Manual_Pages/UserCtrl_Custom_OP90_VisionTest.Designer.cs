namespace GT_App
{
    partial class UserCtrl_Custom_OP90_VisionTest
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
            this.btn_OP90Test = new System.Windows.Forms.Button();
            this.num_Counts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_Counts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_OP90Test
            // 
            this.btn_OP90Test.Location = new System.Drawing.Point(19, 21);
            this.btn_OP90Test.Name = "btn_OP90Test";
            this.btn_OP90Test.Size = new System.Drawing.Size(210, 44);
            this.btn_OP90Test.TabIndex = 0;
            this.btn_OP90Test.Text = "OP90 Vision Repeat Test";
            this.btn_OP90Test.UseVisualStyleBackColor = true;
            this.btn_OP90Test.Click += new System.EventHandler(this.btn_OP90Test_Click);
            // 
            // num_Counts
            // 
            this.num_Counts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_Counts.Location = new System.Drawing.Point(243, 35);
            this.num_Counts.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.num_Counts.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_Counts.Name = "num_Counts";
            this.num_Counts.Size = new System.Drawing.Size(63, 30);
            this.num_Counts.TabIndex = 1;
            this.num_Counts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(246, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 6;
            this.label1.Text = "Counts";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.num_Counts);
            this.groupBox1.Controls.Add(this.btn_OP90Test);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 81);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OP90 Subsequence";
            // 
            // UserCtrl_Custom_OP90_VisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Custom_OP90_VisionTest";
            this.Size = new System.Drawing.Size(326, 86);
            ((System.ComponentModel.ISupportInitialize)(this.num_Counts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btn_OP90Test;
        private System.Windows.Forms.NumericUpDown num_Counts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
	}
}
