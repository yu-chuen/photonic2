namespace GT_App
{
	partial class WireTester_Hioki_Dialog
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
            this.groupBox_Operations = new System.Windows.Forms.GroupBox();
            this.btn_ReadTemperature = new System.Windows.Forms.Button();
            this.radbtn_WT_R0R1 = new System.Windows.Forms.RadioButton();
            this.radbtn_WT_LR0 = new System.Windows.Forms.RadioButton();
            this.radbtn_WT_LN3 = new System.Windows.Forms.RadioButton();
            this.radbtn_WT_LN2 = new System.Windows.Forms.RadioButton();
            this.radbtn_WT_LN1 = new System.Windows.Forms.RadioButton();
            this.btn_ZeroAdjustment = new System.Windows.Forms.Button();
            this.lbl_OperationResponse = new System.Windows.Forms.Label();
            this.btn_ReadResistance = new System.Windows.Forms.Button();
            this.btn_Update = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.btn_EnableWireTester = new System.Windows.Forms.Button();
            this.btn_WTClamp = new System.Windows.Forms.Button();
            this.groupBox_Operations.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox_Operations
            // 
            this.groupBox_Operations.Controls.Add(this.btn_ReadTemperature);
            this.groupBox_Operations.Controls.Add(this.radbtn_WT_R0R1);
            this.groupBox_Operations.Controls.Add(this.radbtn_WT_LR0);
            this.groupBox_Operations.Controls.Add(this.radbtn_WT_LN3);
            this.groupBox_Operations.Controls.Add(this.radbtn_WT_LN2);
            this.groupBox_Operations.Controls.Add(this.radbtn_WT_LN1);
            this.groupBox_Operations.Controls.Add(this.btn_ZeroAdjustment);
            this.groupBox_Operations.Controls.Add(this.lbl_OperationResponse);
            this.groupBox_Operations.Controls.Add(this.btn_ReadResistance);
            this.groupBox_Operations.Location = new System.Drawing.Point(7, 7);
            this.groupBox_Operations.Name = "groupBox_Operations";
            this.groupBox_Operations.Size = new System.Drawing.Size(355, 348);
            this.groupBox_Operations.TabIndex = 2;
            this.groupBox_Operations.TabStop = false;
            this.groupBox_Operations.Text = "Operations";
            // 
            // btn_ReadTemperature
            // 
            this.btn_ReadTemperature.Location = new System.Drawing.Point(15, 205);
            this.btn_ReadTemperature.Name = "btn_ReadTemperature";
            this.btn_ReadTemperature.Size = new System.Drawing.Size(168, 64);
            this.btn_ReadTemperature.TabIndex = 10;
            this.btn_ReadTemperature.Text = "Read Temperature";
            this.btn_ReadTemperature.UseVisualStyleBackColor = true;
            this.btn_ReadTemperature.Click += new System.EventHandler(this.btn_ReadTemperature_Click);
            // 
            // radbtn_WT_R0R1
            // 
            this.radbtn_WT_R0R1.AutoSize = true;
            this.radbtn_WT_R0R1.Location = new System.Drawing.Point(198, 250);
            this.radbtn_WT_R0R1.Name = "radbtn_WT_R0R1";
            this.radbtn_WT_R0R1.Size = new System.Drawing.Size(149, 20);
            this.radbtn_WT_R0R1.TabIndex = 9;
            this.radbtn_WT_R0R1.TabStop = true;
            this.radbtn_WT_R0R1.Text = "Wire Test - R0-R1";
            this.radbtn_WT_R0R1.UseVisualStyleBackColor = true;
            // 
            // radbtn_WT_LR0
            // 
            this.radbtn_WT_LR0.AutoSize = true;
            this.radbtn_WT_LR0.Location = new System.Drawing.Point(198, 223);
            this.radbtn_WT_LR0.Name = "radbtn_WT_LR0";
            this.radbtn_WT_LR0.Size = new System.Drawing.Size(138, 20);
            this.radbtn_WT_LR0.TabIndex = 8;
            this.radbtn_WT_LR0.TabStop = true;
            this.radbtn_WT_LR0.Text = "Wire Test - L-R0";
            this.radbtn_WT_LR0.UseVisualStyleBackColor = true;
            // 
            // radbtn_WT_LN3
            // 
            this.radbtn_WT_LN3.AutoSize = true;
            this.radbtn_WT_LN3.Location = new System.Drawing.Point(198, 196);
            this.radbtn_WT_LN3.Name = "radbtn_WT_LN3";
            this.radbtn_WT_LN3.Size = new System.Drawing.Size(138, 20);
            this.radbtn_WT_LN3.TabIndex = 7;
            this.radbtn_WT_LN3.TabStop = true;
            this.radbtn_WT_LN3.Text = "Wire Test - L-N3";
            this.radbtn_WT_LN3.UseVisualStyleBackColor = true;
            // 
            // radbtn_WT_LN2
            // 
            this.radbtn_WT_LN2.AutoSize = true;
            this.radbtn_WT_LN2.Location = new System.Drawing.Point(198, 169);
            this.radbtn_WT_LN2.Name = "radbtn_WT_LN2";
            this.radbtn_WT_LN2.Size = new System.Drawing.Size(138, 20);
            this.radbtn_WT_LN2.TabIndex = 6;
            this.radbtn_WT_LN2.TabStop = true;
            this.radbtn_WT_LN2.Text = "Wire Test - L-N2";
            this.radbtn_WT_LN2.UseVisualStyleBackColor = true;
            // 
            // radbtn_WT_LN1
            // 
            this.radbtn_WT_LN1.AutoSize = true;
            this.radbtn_WT_LN1.Checked = true;
            this.radbtn_WT_LN1.Location = new System.Drawing.Point(198, 142);
            this.radbtn_WT_LN1.Name = "radbtn_WT_LN1";
            this.radbtn_WT_LN1.Size = new System.Drawing.Size(138, 20);
            this.radbtn_WT_LN1.TabIndex = 5;
            this.radbtn_WT_LN1.TabStop = true;
            this.radbtn_WT_LN1.Text = "Wire Test - L-N1";
            this.radbtn_WT_LN1.UseVisualStyleBackColor = true;
            // 
            // btn_ZeroAdjustment
            // 
            this.btn_ZeroAdjustment.Location = new System.Drawing.Point(16, 276);
            this.btn_ZeroAdjustment.Name = "btn_ZeroAdjustment";
            this.btn_ZeroAdjustment.Size = new System.Drawing.Size(168, 64);
            this.btn_ZeroAdjustment.TabIndex = 4;
            this.btn_ZeroAdjustment.Text = "Zero Adjustment";
            this.btn_ZeroAdjustment.UseVisualStyleBackColor = true;
            this.btn_ZeroAdjustment.Click += new System.EventHandler(this.btn_ZeroAdjustment_Click);
            // 
            // lbl_OperationResponse
            // 
            this.lbl_OperationResponse.BackColor = System.Drawing.Color.White;
            this.lbl_OperationResponse.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_OperationResponse.Location = new System.Drawing.Point(9, 24);
            this.lbl_OperationResponse.Name = "lbl_OperationResponse";
            this.lbl_OperationResponse.Size = new System.Drawing.Size(317, 102);
            this.lbl_OperationResponse.TabIndex = 3;
            this.lbl_OperationResponse.Text = "...";
            // 
            // btn_ReadResistance
            // 
            this.btn_ReadResistance.Location = new System.Drawing.Point(15, 134);
            this.btn_ReadResistance.Name = "btn_ReadResistance";
            this.btn_ReadResistance.Size = new System.Drawing.Size(168, 64);
            this.btn_ReadResistance.TabIndex = 2;
            this.btn_ReadResistance.Text = "Read From Wire Tester";
            this.btn_ReadResistance.UseVisualStyleBackColor = true;
            this.btn_ReadResistance.Click += new System.EventHandler(this.btn_ReadResistance_Click);
            // 
            // btn_Update
            // 
            this.btn_Update.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_Update.Location = new System.Drawing.Point(507, 245);
            this.btn_Update.Name = "btn_Update";
            this.btn_Update.Size = new System.Drawing.Size(144, 52);
            this.btn_Update.TabIndex = 3;
            this.btn_Update.Text = "Update Settings";
            this.btn_Update.UseVisualStyleBackColor = true;
            this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(507, 303);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(144, 52);
            this.btn_ESC.TabIndex = 4;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_EnableWireTester
            // 
            this.btn_EnableWireTester.BackColor = System.Drawing.Color.LimeGreen;
            this.btn_EnableWireTester.Location = new System.Drawing.Point(368, 245);
            this.btn_EnableWireTester.Name = "btn_EnableWireTester";
            this.btn_EnableWireTester.Size = new System.Drawing.Size(134, 52);
            this.btn_EnableWireTester.TabIndex = 11;
            this.btn_EnableWireTester.Text = " Wire Tester Enabled";
            this.btn_EnableWireTester.UseVisualStyleBackColor = false;
            this.btn_EnableWireTester.Click += new System.EventHandler(this.btn_EnableWireTester_Click);
            // 
            // btn_WTClamp
            // 
            this.btn_WTClamp.Location = new System.Drawing.Point(368, 303);
            this.btn_WTClamp.Name = "btn_WTClamp";
            this.btn_WTClamp.Size = new System.Drawing.Size(134, 52);
            this.btn_WTClamp.TabIndex = 12;
            this.btn_WTClamp.Text = "WT Clamp";
            this.btn_WTClamp.UseVisualStyleBackColor = true;
            this.btn_WTClamp.Click += new System.EventHandler(this.btn_WTClamp_Click);
            // 
            // WireTester_Hioki_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(663, 364);
            this.ControlBox = false;
            this.Controls.Add(this.btn_WTClamp);
            this.Controls.Add(this.btn_EnableWireTester);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.btn_Update);
            this.Controls.Add(this.groupBox_Operations);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "WireTester_Hioki_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hioki Wire Tester";
            this.Load += new System.EventHandler(this.WireTester_Hioki_Dialog_Load);
            this.groupBox_Operations.ResumeLayout(false);
            this.groupBox_Operations.PerformLayout();
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox_Operations;
		private System.Windows.Forms.Label lbl_OperationResponse;
		private System.Windows.Forms.Button btn_ReadResistance;
		private System.Windows.Forms.Button btn_Update;
		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Button btn_ZeroAdjustment;
		private System.Windows.Forms.RadioButton radbtn_WT_R0R1;
		private System.Windows.Forms.RadioButton radbtn_WT_LR0;
		private System.Windows.Forms.RadioButton radbtn_WT_LN3;
		private System.Windows.Forms.RadioButton radbtn_WT_LN2;
		private System.Windows.Forms.RadioButton radbtn_WT_LN1;
		private System.Windows.Forms.Button btn_ReadTemperature;
        private System.Windows.Forms.Button btn_EnableWireTester;
		private System.Windows.Forms.Button btn_WTClamp;

	}
}