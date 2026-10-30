namespace GT_App
{
	partial class UserCtrl_Custom_GotoSolderPoint
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
            this.btn_GotoSolderPoint = new System.Windows.Forms.Button();
            this.num_SolderPoint = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_SolderAllPoints = new System.Windows.Forms.Button();
            this.btn_CleanSolderIron = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.num_SolderPoint)).BeginInit();
            this.SuspendLayout();
            // 
            // btn_GotoSolderPoint
            // 
            this.btn_GotoSolderPoint.Location = new System.Drawing.Point(8, 12);
            this.btn_GotoSolderPoint.Name = "btn_GotoSolderPoint";
            this.btn_GotoSolderPoint.Size = new System.Drawing.Size(291, 46);
            this.btn_GotoSolderPoint.TabIndex = 0;
            this.btn_GotoSolderPoint.Text = "Go to Solder Point";
            this.btn_GotoSolderPoint.UseVisualStyleBackColor = true;
            this.btn_GotoSolderPoint.Click += new System.EventHandler(this.btn_GotoSolderPoint_Click);
            // 
            // num_SolderPoint
            // 
            this.num_SolderPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.num_SolderPoint.Location = new System.Drawing.Point(316, 23);
            this.num_SolderPoint.Maximum = new decimal(new int[] {
            6,
            0,
            0,
            0});
            this.num_SolderPoint.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.num_SolderPoint.Name = "num_SolderPoint";
            this.num_SolderPoint.Size = new System.Drawing.Size(72, 35);
            this.num_SolderPoint.TabIndex = 1;
            this.num_SolderPoint.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.num_SolderPoint.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(306, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 16);
            this.label1.TabIndex = 2;
            this.label1.Text = "Solder Point";
            // 
            // btn_SolderAllPoints
            // 
            this.btn_SolderAllPoints.Location = new System.Drawing.Point(8, 74);
            this.btn_SolderAllPoints.Name = "btn_SolderAllPoints";
            this.btn_SolderAllPoints.Size = new System.Drawing.Size(192, 46);
            this.btn_SolderAllPoints.TabIndex = 3;
            this.btn_SolderAllPoints.Text = "Solder All Points";
            this.btn_SolderAllPoints.UseVisualStyleBackColor = true;
            this.btn_SolderAllPoints.Visible = false;
            this.btn_SolderAllPoints.Click += new System.EventHandler(this.btn_SolderAllPoints_Click);
            // 
            // btn_CleanSolderIron
            // 
            this.btn_CleanSolderIron.Location = new System.Drawing.Point(207, 74);
            this.btn_CleanSolderIron.Name = "btn_CleanSolderIron";
            this.btn_CleanSolderIron.Size = new System.Drawing.Size(192, 46);
            this.btn_CleanSolderIron.TabIndex = 4;
            this.btn_CleanSolderIron.Text = "Clean Solder Iron";
            this.btn_CleanSolderIron.UseVisualStyleBackColor = true;
            this.btn_CleanSolderIron.Click += new System.EventHandler(this.btn_CleanSolderIron_Click);
            // 
            // UserCtrl_Custom_GotoSolderPoint
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btn_CleanSolderIron);
            this.Controls.Add(this.btn_SolderAllPoints);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.num_SolderPoint);
            this.Controls.Add(this.btn_GotoSolderPoint);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_Custom_GotoSolderPoint";
            this.Size = new System.Drawing.Size(406, 131);
            ((System.ComponentModel.ISupportInitialize)(this.num_SolderPoint)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btn_GotoSolderPoint;
		private System.Windows.Forms.NumericUpDown num_SolderPoint;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button btn_SolderAllPoints;
        private System.Windows.Forms.Button btn_CleanSolderIron;
	}
}
