namespace GT_App
{
    partial class UserCtrl_UI_OutputBit
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
            this.btn_OutputBit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_OutputBit
            // 
            this.btn_OutputBit.BackColor = System.Drawing.SystemColors.Control;
            this.btn_OutputBit.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OutputBit.Location = new System.Drawing.Point(3, 3);
            this.btn_OutputBit.Name = "btn_OutputBit";
            this.btn_OutputBit.Size = new System.Drawing.Size(450, 50);
            this.btn_OutputBit.TabIndex = 0;
            this.btn_OutputBit.Text = "...";
            this.btn_OutputBit.UseVisualStyleBackColor = true;
            this.btn_OutputBit.Click += new System.EventHandler(this.btn_OutputBit_Click);
            // 
            // UserCtrl_UI_OutputBit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btn_OutputBit);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_UI_OutputBit";
            this.Size = new System.Drawing.Size(456, 56);
            this.Load += new System.EventHandler(this.UserCtrl_UI_OutputBit_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_OutputBit;
    }
}
