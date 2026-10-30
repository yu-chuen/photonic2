namespace GT_App
{
    partial class UserCtrl_UI_InputBit_backup
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
            this.lbl_Label = new System.Windows.Forms.Label();
            this.pic_Indicator = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Indicator)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Label
            // 
            this.lbl_Label.Location = new System.Drawing.Point(36, 6);
            this.lbl_Label.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(369, 18);
            this.lbl_Label.TabIndex = 8;
            this.lbl_Label.Text = "...";
            // 
            // pic_Indicator
            // 
            this.pic_Indicator.Image = global::GT_App.Properties.Resources.Red_Ball;
            this.pic_Indicator.InitialImage = null;
            this.pic_Indicator.Location = new System.Drawing.Point(4, 3);
            this.pic_Indicator.Name = "pic_Indicator";
            this.pic_Indicator.Size = new System.Drawing.Size(25, 22);
            this.pic_Indicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Indicator.TabIndex = 9;
            this.pic_Indicator.TabStop = false;
            // 
            // UserCtrl_UI_InputBit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pic_Indicator);
            this.Controls.Add(this.lbl_Label);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_UI_InputBit";
            this.Size = new System.Drawing.Size(421, 30);
            this.Load += new System.EventHandler(this.UserCtrl_UI_InputBit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Indicator)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_Label;
        private System.Windows.Forms.PictureBox pic_Indicator;
    }
}
