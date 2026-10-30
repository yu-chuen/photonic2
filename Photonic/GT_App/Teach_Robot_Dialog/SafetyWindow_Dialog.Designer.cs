namespace GT_App
{
    partial class SafetyWindow_Dialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
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
			this.lbl_PositionName = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lbl_PositionDesc = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btn_BLY = new System.Windows.Forms.Button();
			this.btn_BLX = new System.Windows.Forms.Button();
			this.btn_TRY = new System.Windows.Forms.Button();
			this.btn_TRX = new System.Windows.Forms.Button();
			this.lbl_BLY = new System.Windows.Forms.Label();
			this.lbl_BLX = new System.Windows.Forms.Label();
			this.lbl_TRY = new System.Windows.Forms.Label();
			this.lbl_TRX = new System.Windows.Forms.Label();
			this.btn_UpdateEsc = new System.Windows.Forms.Button();
			this.btn_ESC = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// lbl_PositionName
			// 
			this.lbl_PositionName.BackColor = System.Drawing.Color.White;
			this.lbl_PositionName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_PositionName.Location = new System.Drawing.Point(136, 16);
			this.lbl_PositionName.Name = "lbl_PositionName";
			this.lbl_PositionName.Size = new System.Drawing.Size(88, 24);
			this.lbl_PositionName.TabIndex = 0;
			this.lbl_PositionName.Text = "...";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.label2.Location = new System.Drawing.Point(16, 16);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(113, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Position Name:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.label3.Location = new System.Drawing.Point(16, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(108, 16);
			this.label3.TabIndex = 3;
			this.label3.Text = "Position Desc:";
			// 
			// lbl_PositionDesc
			// 
			this.lbl_PositionDesc.BackColor = System.Drawing.Color.White;
			this.lbl_PositionDesc.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_PositionDesc.Location = new System.Drawing.Point(136, 48);
			this.lbl_PositionDesc.Name = "lbl_PositionDesc";
			this.lbl_PositionDesc.Size = new System.Drawing.Size(184, 24);
			this.lbl_PositionDesc.TabIndex = 2;
			this.lbl_PositionDesc.Text = "...";
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_BLY);
			this.groupBox1.Controls.Add(this.btn_BLX);
			this.groupBox1.Controls.Add(this.btn_TRY);
			this.groupBox1.Controls.Add(this.btn_TRX);
			this.groupBox1.Controls.Add(this.lbl_BLY);
			this.groupBox1.Controls.Add(this.lbl_BLX);
			this.groupBox1.Controls.Add(this.lbl_TRY);
			this.groupBox1.Controls.Add(this.lbl_TRX);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.groupBox1.Location = new System.Drawing.Point(8, 80);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(312, 240);
			this.groupBox1.TabIndex = 15;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Settings";
			// 
			// btn_BLY
			// 
			this.btn_BLY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_BLY.Location = new System.Drawing.Point(24, 180);
			this.btn_BLY.Name = "btn_BLY";
			this.btn_BLY.Size = new System.Drawing.Size(160, 40);
			this.btn_BLY.TabIndex = 22;
			this.btn_BLY.Text = "Min (Bottom Left) Y";
			this.btn_BLY.UseVisualStyleBackColor = true;
			this.btn_BLY.Click += new System.EventHandler(this.btn_BLY_Click);
			// 
			// btn_BLX
			// 
			this.btn_BLX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_BLX.Location = new System.Drawing.Point(24, 132);
			this.btn_BLX.Name = "btn_BLX";
			this.btn_BLX.Size = new System.Drawing.Size(160, 40);
			this.btn_BLX.TabIndex = 21;
			this.btn_BLX.Text = "Min (Bottom Left) X";
			this.btn_BLX.UseVisualStyleBackColor = true;
			this.btn_BLX.Click += new System.EventHandler(this.btn_BLX_Click);
			// 
			// btn_TRY
			// 
			this.btn_TRY.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_TRY.Location = new System.Drawing.Point(24, 84);
			this.btn_TRY.Name = "btn_TRY";
			this.btn_TRY.Size = new System.Drawing.Size(160, 40);
			this.btn_TRY.TabIndex = 20;
			this.btn_TRY.Text = "Max (Top Right) Y";
			this.btn_TRY.UseVisualStyleBackColor = true;
			this.btn_TRY.Click += new System.EventHandler(this.btn_TRY_Click);
			// 
			// btn_TRX
			// 
			this.btn_TRX.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_TRX.Location = new System.Drawing.Point(24, 36);
			this.btn_TRX.Name = "btn_TRX";
			this.btn_TRX.Size = new System.Drawing.Size(160, 40);
			this.btn_TRX.TabIndex = 19;
			this.btn_TRX.Text = "Max (Top Right) X";
			this.btn_TRX.UseVisualStyleBackColor = true;
			this.btn_TRX.Click += new System.EventHandler(this.btn_TRX_Click);
			// 
			// lbl_BLY
			// 
			this.lbl_BLY.BackColor = System.Drawing.Color.White;
			this.lbl_BLY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_BLY.Location = new System.Drawing.Point(192, 188);
			this.lbl_BLY.Name = "lbl_BLY";
			this.lbl_BLY.Size = new System.Drawing.Size(88, 24);
			this.lbl_BLY.TabIndex = 18;
			this.lbl_BLY.Text = "...";
			// 
			// lbl_BLX
			// 
			this.lbl_BLX.BackColor = System.Drawing.Color.White;
			this.lbl_BLX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_BLX.Location = new System.Drawing.Point(192, 140);
			this.lbl_BLX.Name = "lbl_BLX";
			this.lbl_BLX.Size = new System.Drawing.Size(88, 24);
			this.lbl_BLX.TabIndex = 17;
			this.lbl_BLX.Text = "...";
			// 
			// lbl_TRY
			// 
			this.lbl_TRY.BackColor = System.Drawing.Color.White;
			this.lbl_TRY.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_TRY.Location = new System.Drawing.Point(192, 92);
			this.lbl_TRY.Name = "lbl_TRY";
			this.lbl_TRY.Size = new System.Drawing.Size(88, 24);
			this.lbl_TRY.TabIndex = 16;
			this.lbl_TRY.Text = "...";
			// 
			// lbl_TRX
			// 
			this.lbl_TRX.BackColor = System.Drawing.Color.White;
			this.lbl_TRX.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lbl_TRX.Location = new System.Drawing.Point(192, 44);
			this.lbl_TRX.Name = "lbl_TRX";
			this.lbl_TRX.Size = new System.Drawing.Size(88, 24);
			this.lbl_TRX.TabIndex = 15;
			this.lbl_TRX.Text = "...";
			// 
			// btn_UpdateEsc
			// 
			this.btn_UpdateEsc.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btn_UpdateEsc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_UpdateEsc.Location = new System.Drawing.Point(17, 405);
			this.btn_UpdateEsc.Name = "btn_UpdateEsc";
			this.btn_UpdateEsc.Size = new System.Drawing.Size(144, 40);
			this.btn_UpdateEsc.TabIndex = 16;
			this.btn_UpdateEsc.Text = "Update and Esc";
			this.btn_UpdateEsc.UseVisualStyleBackColor = true;
			this.btn_UpdateEsc.Click += new System.EventHandler(this.btn_UpdateEsc_Click);
			// 
			// btn_ESC
			// 
			this.btn_ESC.Anchor = ( ( System.Windows.Forms.AnchorStyles ) ( ( System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right ) ) );
			this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_ESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_ESC.Location = new System.Drawing.Point(169, 405);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(144, 40);
			this.btn_ESC.TabIndex = 17;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = true;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// label1
			// 
			this.label1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label1.Location = new System.Drawing.Point(8, 331);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(312, 62);
			this.label1.TabIndex = 18;
			this.label1.Text = "Note: Max (Top Right) < Motor Value > Min (Bottom Left).   Set all to 0 to disabl" +
				"e safety Window";
			// 
			// SafetyWindow_Dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_ESC;
			this.ClientSize = new System.Drawing.Size(336, 457);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btn_ESC);
			this.Controls.Add(this.btn_UpdateEsc);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.lbl_PositionDesc);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.lbl_PositionName);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "SafetyWindow_Dialog";
			this.Text = "Safety Window";
			this.Load += new System.EventHandler(this.SafetyWindow_Dialog_Load);
			this.groupBox1.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_PositionName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lbl_PositionDesc;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_BLY;
        private System.Windows.Forms.Button btn_BLX;
        private System.Windows.Forms.Button btn_TRY;
        private System.Windows.Forms.Button btn_TRX;
        private System.Windows.Forms.Label lbl_BLY;
        private System.Windows.Forms.Label lbl_BLX;
        private System.Windows.Forms.Label lbl_TRY;
        private System.Windows.Forms.Label lbl_TRX;
        private System.Windows.Forms.Button btn_UpdateEsc;
        private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Label label1;
    }
}