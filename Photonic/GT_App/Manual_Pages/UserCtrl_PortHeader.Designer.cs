namespace GT_App
{
    partial class UserCtrl_PortHeader
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code
        private void InitializeComponent()
        {
            this.lbl_DI = new System.Windows.Forms.Label();
            this.lbl_DO = new System.Windows.Forms.Label();
            this.lbl_Line = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lbl_DI
            // 
            this.lbl_DI.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(111)))), ((int)(((byte)(178)))));
            this.lbl_DI.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DI.ForeColor = System.Drawing.Color.White;
            this.lbl_DI.Location = new System.Drawing.Point(0, 2);
            this.lbl_DI.Name = "lbl_DI";
            this.lbl_DI.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_DI.Size = new System.Drawing.Size(528, 30);
            this.lbl_DI.TabIndex = 0;
            this.lbl_DI.Text = "INPUTS  (DI)   -   read only";
            this.lbl_DI.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_DO
            // 
            this.lbl_DO.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(119)))), ((int)(((byte)(43)))));
            this.lbl_DO.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_DO.ForeColor = System.Drawing.Color.White;
            this.lbl_DO.Location = new System.Drawing.Point(578, 2);
            this.lbl_DO.Name = "lbl_DO";
            this.lbl_DO.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.lbl_DO.Size = new System.Drawing.Size(456, 30);
            this.lbl_DO.TabIndex = 1;
            this.lbl_DO.Text = "OUTPUTS  (DO)   -   click to switch";
            this.lbl_DO.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbl_Line
            // 
            this.lbl_Line.BackColor = System.Drawing.Color.Silver;
            this.lbl_Line.Location = new System.Drawing.Point(552, 2);
            this.lbl_Line.Name = "lbl_Line";
            this.lbl_Line.Size = new System.Drawing.Size(2, 76);
            this.lbl_Line.TabIndex = 2;
            // 
            // UserCtrl_PortHeader
            // 
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.lbl_Line);
            this.Controls.Add(this.lbl_DO);
            this.Controls.Add(this.lbl_DI);
            this.Name = "UserCtrl_PortHeader";
            this.Size = new System.Drawing.Size(1034, 78);
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.Label lbl_DI;
        private System.Windows.Forms.Label lbl_DO;
        private System.Windows.Forms.Label lbl_Line;
    }
}