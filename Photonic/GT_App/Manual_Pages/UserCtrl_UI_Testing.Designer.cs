namespace GT_App
{
    partial class UserCtrl_UI_Testing
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
            this.pic_Indicator = new System.Windows.Forms.PictureBox();
            this.btn_OutputBit = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pic_Indicator)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_Indicator
            // 
            this.pic_Indicator.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pic_Indicator.Image = global::GT_App.Properties.Resources.Red_Ball;
            this.pic_Indicator.Location = new System.Drawing.Point(3, 3);
            this.pic_Indicator.Name = "pic_Indicator";
            this.pic_Indicator.Size = new System.Drawing.Size(30, 30);
            this.pic_Indicator.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_Indicator.TabIndex = 1;
            this.pic_Indicator.TabStop = false;
            this.pic_Indicator.Click += new System.EventHandler(this.pic_Indicator_Click);
            // 
            // btn_OutputBit
            // 
            this.btn_OutputBit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_OutputBit.FlatAppearance.BorderSize = 0;
            this.btn_OutputBit.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_OutputBit.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.ControlLight;
            this.btn_OutputBit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_OutputBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OutputBit.Location = new System.Drawing.Point(40, 3);
            this.btn_OutputBit.Name = "btn_OutputBit";
            this.btn_OutputBit.Size = new System.Drawing.Size(486, 30);
            this.btn_OutputBit.TabIndex = 0;
            this.btn_OutputBit.Text = "...";
            this.btn_OutputBit.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btn_OutputBit.UseVisualStyleBackColor = false;
            this.btn_OutputBit.Click += new System.EventHandler(this.btn_OutputBit_Click);
            // 
            // UserCtrl_UI_Testing
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pic_Indicator);
            this.Controls.Add(this.btn_OutputBit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_UI_Testing";
            this.Size = new System.Drawing.Size(528, 36);
            this.Load += new System.EventHandler(this.UserCtrl_UI_OutputBit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_Indicator)).EndInit();
            this.ResumeLayout(false);

        }
        #endregion

        private System.Windows.Forms.PictureBox pic_Indicator;
        private System.Windows.Forms.Button btn_OutputBit;
    }
}