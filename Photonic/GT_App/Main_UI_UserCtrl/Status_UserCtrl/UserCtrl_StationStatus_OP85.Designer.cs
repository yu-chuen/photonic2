namespace GT_App
{
    partial class UserCtrl_StationStatus_OP85
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
            this.lbl_Title = new System.Windows.Forms.Label();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.btn_OP85_VisionResult = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_Title.BackColor = System.Drawing.Color.White;
            this.lbl_Title.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Title.Location = new System.Drawing.Point(5, 5);
            this.lbl_Title.Margin = new System.Windows.Forms.Padding(5);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(312, 25);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "...";
            this.lbl_Title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            // 
            // btn_OP85_VisionResult
            // 
            this.btn_OP85_VisionResult.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_OP85_VisionResult.BackColor = System.Drawing.SystemColors.Control;
            this.btn_OP85_VisionResult.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OP85_VisionResult.Location = new System.Drawing.Point(81, 407);
            this.btn_OP85_VisionResult.Margin = new System.Windows.Forms.Padding(4);
            this.btn_OP85_VisionResult.Name = "btn_OP85_VisionResult";
            this.btn_OP85_VisionResult.Size = new System.Drawing.Size(162, 53);
            this.btn_OP85_VisionResult.TabIndex = 11;
            this.btn_OP85_VisionResult.Text = "OP85 Vision Result";
            this.btn_OP85_VisionResult.UseVisualStyleBackColor = false;
            this.btn_OP85_VisionResult.Visible = false;
            this.btn_OP85_VisionResult.Click += new System.EventHandler(this.btn_OP85_VisionResult_Click);
            // 
            // UserCtrl_StationStatus_OP85
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.btn_OP85_VisionResult);
            this.Controls.Add(this.lbl_Title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UserCtrl_StationStatus_OP85";
            this.Size = new System.Drawing.Size(324, 464);
            this.Load += new System.EventHandler(this.UserCtrl_StationStatus_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label lbl_Title;
		private System.ComponentModel.BackgroundWorker backgroundWorker;
        private System.Windows.Forms.Button btn_OP85_VisionResult;
	}
}
