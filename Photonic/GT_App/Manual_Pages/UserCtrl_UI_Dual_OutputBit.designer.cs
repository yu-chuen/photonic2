namespace GT_App
{
    partial class UserCtrl_UI_Dual_OutputBit
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btn_Boolean = new System.Windows.Forms.Button();
            this.lbl_Status = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_Boolean
            // 
            this.btn_Boolean.BackColor = System.Drawing.SystemColors.Control;
            this.btn_Boolean.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Boolean.Location = new System.Drawing.Point(3, 3);
            this.btn_Boolean.Name = "btn_Boolean";
            this.btn_Boolean.Size = new System.Drawing.Size(394, 50);
            this.btn_Boolean.TabIndex = 0;
            this.btn_Boolean.Text = "...";
            this.btn_Boolean.UseVisualStyleBackColor = true;
            this.btn_Boolean.Click += new System.EventHandler(this.btn_Boolean_Click);
            // 
            // lbl_Status
            // 
            this.lbl_Status.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Status.Location = new System.Drawing.Point(403, 3);
            this.lbl_Status.Name = "lbl_Status";
            this.lbl_Status.Size = new System.Drawing.Size(50, 50);
            this.lbl_Status.TabIndex = 1;
            this.lbl_Status.Text = "OFF";
            this.lbl_Status.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // UserCtrl_UI_Dual_OutputBit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Status);
            this.Controls.Add(this.btn_Boolean);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_UI_Dual_OutputBit";
            this.Size = new System.Drawing.Size(456, 56);
            this.Load += new System.EventHandler(this.UserCtrl_UI_Dual_OutputBit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_Boolean;
        private System.Windows.Forms.Label lbl_Status;
    }
}
