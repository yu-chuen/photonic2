namespace GT_App
{
    partial class Rework_Dlg
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
            this.components = new System.ComponentModel.Container();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.lbl_Instruction = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_Insert = new System.Windows.Forms.Button();
            this.btn_Done = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            this.cb_StationOP85 = new System.Windows.Forms.CheckBox();
            this.cb_StationOP90 = new System.Windows.Forms.CheckBox();
            this.cb_StationOP100 = new System.Windows.Forms.CheckBox();
            this.cb_StationOP115 = new System.Windows.Forms.CheckBox();
            this.cb_StationOP117 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // m_Timer
            // 
            this.m_Timer.Interval = 10;
            this.m_Timer.Tick += new System.EventHandler(this.m_Timer_Tick);
            // 
            // lbl_Instruction
            // 
            this.lbl_Instruction.BackColor = System.Drawing.Color.White;
            this.lbl_Instruction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Instruction.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Instruction.Location = new System.Drawing.Point(18, 164);
            this.lbl_Instruction.Name = "lbl_Instruction";
            this.lbl_Instruction.Size = new System.Drawing.Size(520, 83);
            this.lbl_Instruction.TabIndex = 11;
            this.lbl_Instruction.Text = "...";
            this.lbl_Instruction.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(12, 133);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 31);
            this.label2.TabIndex = 10;
            this.label2.Text = "Instruction";
            // 
            // lbl_Status
            // 
            this.lbl_Status.BackColor = System.Drawing.Color.White;
            this.lbl_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Status.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_Status.Location = new System.Drawing.Point(18, 40);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(520, 83);
            this.lbl_Status.TabIndex = 9;
            this.lbl_Status.Text = "...";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 9);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 31);
            this.label6.TabIndex = 8;
            this.label6.Text = "Status";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(12, 267);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(242, 31);
            this.label1.TabIndex = 18;
            this.label1.Text = "Rework Selection";
            // 
            // btn_Insert
            // 
            this.btn_Insert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Insert.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Insert.Location = new System.Drawing.Point(12, 567);
            this.btn_Insert.Name = "btn_Insert";
            this.btn_Insert.Size = new System.Drawing.Size(142, 45);
            this.btn_Insert.TabIndex = 21;
            this.btn_Insert.Text = "Insert";
            this.btn_Insert.UseVisualStyleBackColor = true;
            this.btn_Insert.Click += new System.EventHandler(this.btn_Insert_Click);
            // 
            // btn_Done
            // 
            this.btn_Done.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Done.Enabled = false;
            this.btn_Done.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Done.Location = new System.Drawing.Point(403, 567);
            this.btn_Done.Name = "btn_Done";
            this.btn_Done.Size = new System.Drawing.Size(142, 45);
            this.btn_Done.TabIndex = 20;
            this.btn_Done.Text = "Done";
            this.btn_Done.UseVisualStyleBackColor = true;
            this.btn_Done.Click += new System.EventHandler(this.btn_Done_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(160, 567);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(142, 45);
            this.btn_Cancel.TabIndex = 19;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // cb_StationOP85
            // 
            this.cb_StationOP85.AutoSize = true;
            this.cb_StationOP85.BackColor = System.Drawing.SystemColors.Control;
            this.cb_StationOP85.Enabled = false;
            this.cb_StationOP85.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_StationOP85.Location = new System.Drawing.Point(52, 321);
            this.cb_StationOP85.Name = "cb_StationOP85";
            this.cb_StationOP85.Size = new System.Drawing.Size(149, 28);
            this.cb_StationOP85.TabIndex = 22;
            this.cb_StationOP85.Text = "Station OP85";
            this.cb_StationOP85.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_StationOP85.UseVisualStyleBackColor = false;
            this.cb_StationOP85.Click += new System.EventHandler(this.cb_StationOP85_Click);
            // 
            // cb_StationOP90
            // 
            this.cb_StationOP90.AutoSize = true;
            this.cb_StationOP90.Enabled = false;
            this.cb_StationOP90.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_StationOP90.Location = new System.Drawing.Point(52, 365);
            this.cb_StationOP90.Name = "cb_StationOP90";
            this.cb_StationOP90.Size = new System.Drawing.Size(149, 28);
            this.cb_StationOP90.TabIndex = 23;
            this.cb_StationOP90.Text = "Station OP90";
            this.cb_StationOP90.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_StationOP90.UseVisualStyleBackColor = true;
            this.cb_StationOP90.Click += new System.EventHandler(this.cb_StationOP90_Click);
            // 
            // cb_StationOP100
            // 
            this.cb_StationOP100.AutoSize = true;
            this.cb_StationOP100.Enabled = false;
            this.cb_StationOP100.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_StationOP100.Location = new System.Drawing.Point(52, 409);
            this.cb_StationOP100.Name = "cb_StationOP100";
            this.cb_StationOP100.Size = new System.Drawing.Size(160, 28);
            this.cb_StationOP100.TabIndex = 24;
            this.cb_StationOP100.Text = "Station OP100";
            this.cb_StationOP100.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_StationOP100.UseVisualStyleBackColor = true;
            this.cb_StationOP100.Click += new System.EventHandler(this.cb_StationOP100_Click);
            // 
            // cb_StationOP115
            // 
            this.cb_StationOP115.AutoSize = true;
            this.cb_StationOP115.Enabled = false;
            this.cb_StationOP115.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_StationOP115.Location = new System.Drawing.Point(52, 453);
            this.cb_StationOP115.Name = "cb_StationOP115";
            this.cb_StationOP115.Size = new System.Drawing.Size(160, 28);
            this.cb_StationOP115.TabIndex = 25;
            this.cb_StationOP115.Text = "Station OP115";
            this.cb_StationOP115.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_StationOP115.UseVisualStyleBackColor = true;
            this.cb_StationOP115.CheckedChanged += new System.EventHandler(this.cb_StationOP115_CheckedChanged);
            // 
            // cb_StationOP117
            // 
            this.cb_StationOP117.AutoSize = true;
            this.cb_StationOP117.Enabled = false;
            this.cb_StationOP117.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cb_StationOP117.Location = new System.Drawing.Point(52, 497);
            this.cb_StationOP117.Name = "cb_StationOP117";
            this.cb_StationOP117.Size = new System.Drawing.Size(160, 28);
            this.cb_StationOP117.TabIndex = 26;
            this.cb_StationOP117.Text = "Station OP117";
            this.cb_StationOP117.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cb_StationOP117.UseVisualStyleBackColor = true;
            this.cb_StationOP117.CheckedChanged += new System.EventHandler(this.cb_StationOP117_CheckedChanged);
            // 
            // Rework_Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(557, 624);
            this.ControlBox = false;
            this.Controls.Add(this.cb_StationOP117);
            this.Controls.Add(this.cb_StationOP115);
            this.Controls.Add(this.cb_StationOP100);
            this.Controls.Add(this.cb_StationOP85);
            this.Controls.Add(this.cb_StationOP90);
            this.Controls.Add(this.btn_Insert);
            this.Controls.Add(this.btn_Done);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_Instruction);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.label6);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Rework_Dlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Rework";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Rework_Dlg_FormClosing);
            this.Load += new System.EventHandler(this.Rework_Dlg_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.Label lbl_Instruction;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lbl_Status;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_Insert;
        private System.Windows.Forms.Button btn_Done;
        private System.Windows.Forms.Button btn_Cancel;
        private System.Windows.Forms.CheckBox cb_StationOP85;
        private System.Windows.Forms.CheckBox cb_StationOP90;
        private System.Windows.Forms.CheckBox cb_StationOP100;
        private System.Windows.Forms.CheckBox cb_StationOP115;
        private System.Windows.Forms.CheckBox cb_StationOP117;
    }
}