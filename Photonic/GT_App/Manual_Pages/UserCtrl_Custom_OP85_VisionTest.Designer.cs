namespace GT_App
{
    partial class UserCtrl_Custom_OP85_VisionTest
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
            this.btn_VisionTest = new System.Windows.Forms.Button();
            this.num_Counts = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            ((System.ComponentModel.ISupportInitialize)(this.num_Counts)).BeginInit();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_VisionTest
            // 
            this.btn_VisionTest.Location = new System.Drawing.Point(14, 31);
            this.btn_VisionTest.Name = "btn_VisionTest";
            this.btn_VisionTest.Size = new System.Drawing.Size(291, 46);
            this.btn_VisionTest.TabIndex = 0;
            this.btn_VisionTest.Text = "Vision Test (Docking Once)";
            this.btn_VisionTest.UseVisualStyleBackColor = true;
            this.btn_VisionTest.Click += new System.EventHandler(this.btn_VisionTest_Click);
            // 
            // num_Counts
            // 
            this.num_Counts.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_Counts.Location = new System.Drawing.Point(326, 46);
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
            this.num_Counts.Size = new System.Drawing.Size(63, 31);
            this.num_Counts.TabIndex = 3;
            this.num_Counts.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(330, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 16);
            this.label1.TabIndex = 5;
            this.label1.Text = "Counts";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_VisionTest);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.num_Counts);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 93);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "OP85 Vision Sub Sequence";
            // 
            // UserCtrl_Custom_OP85_VisionTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Custom_OP85_VisionTest";
            this.Size = new System.Drawing.Size(408, 99);
            this.Load += new System.EventHandler(this.UserCtrl_Custom_VisionTest_Load);
            ((System.ComponentModel.ISupportInitialize)(this.num_Counts)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

        private System.Windows.Forms.Button btn_VisionTest;
        private System.Windows.Forms.NumericUpDown num_Counts;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
	}
}
