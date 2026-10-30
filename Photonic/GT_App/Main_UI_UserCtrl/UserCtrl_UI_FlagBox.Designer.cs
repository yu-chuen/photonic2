namespace GT_App
{
    partial class UserCtrl_UI_FlagBox
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
            this.pictureBox_Flag = new System.Windows.Forms.PictureBox();
            this.lbl_Text = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Flag)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_Flag
            // 
            this.pictureBox_Flag.Image = global::GT_App.Properties.Resources.Red_Ball;
            this.pictureBox_Flag.Location = new System.Drawing.Point(4, 4);
            this.pictureBox_Flag.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox_Flag.Name = "pictureBox_Flag";
            this.pictureBox_Flag.Size = new System.Drawing.Size(26, 23);
            this.pictureBox_Flag.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox_Flag.TabIndex = 0;
            this.pictureBox_Flag.TabStop = false;
            // 
            // lbl_Text
            // 
            this.lbl_Text.ForeColor = System.Drawing.Color.Black;
            this.lbl_Text.Location = new System.Drawing.Point(38, 8);
            this.lbl_Text.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Text.Name = "lbl_Text";
            this.lbl_Text.Size = new System.Drawing.Size(225, 19);
            this.lbl_Text.TabIndex = 1;
            this.lbl_Text.Text = "label1";
            // 
            // UserCtrl_UI_FlagBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Text);
            this.Controls.Add(this.pictureBox_Flag);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_UI_FlagBox";
            this.Size = new System.Drawing.Size(250, 32);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_Flag)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_Flag;
        private System.Windows.Forms.Label lbl_Text;
    }
}
