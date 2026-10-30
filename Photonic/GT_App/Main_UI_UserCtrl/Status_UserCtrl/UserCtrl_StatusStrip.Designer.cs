namespace GT_App
{
    partial class UserCtrl_StatusStrip
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
            this.lbl_Description = new System.Windows.Forms.Label();
            this.pic_Status = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Status)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Description
            // 
            this.lbl_Description.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lbl_Description.Location = new System.Drawing.Point(44, 5);
            this.lbl_Description.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Description.Name = "lbl_Description";
            this.lbl_Description.Size = new System.Drawing.Size(225, 30);
            this.lbl_Description.TabIndex = 13;
            this.lbl_Description.Text = "label";
            this.lbl_Description.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pic_Status
            // 
            this.pic_Status.Image = global::GT_App.Properties.Resources.Red_Ball;
            this.pic_Status.InitialImage = null;
            this.pic_Status.Location = new System.Drawing.Point(5, 5);
            this.pic_Status.Margin = new System.Windows.Forms.Padding(5);
            this.pic_Status.Name = "pic_Status";
            this.pic_Status.Size = new System.Drawing.Size(30, 30);
            this.pic_Status.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Status.TabIndex = 14;
            this.pic_Status.TabStop = false;
            // 
            // UserCtrl_StatusStrip
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.pic_Status);
            this.Controls.Add(this.lbl_Description);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "UserCtrl_StatusStrip";
            this.Size = new System.Drawing.Size(314, 40);
            this.Load += new System.EventHandler(this.UserCtrl_StatusStrip_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Status)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_Status;
        private System.Windows.Forms.Label lbl_Description;
    }
}
