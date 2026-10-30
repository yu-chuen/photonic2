namespace GT_App
{
    partial class UserCtrl_DelayNTimeoutsMisc
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
            this.btn_DelayExtend = new System.Windows.Forms.Button();
            this.lbl_Delay = new System.Windows.Forms.Label();
            this.lbl_Identifier = new System.Windows.Forms.Label();
            this.lbl_Label = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_DelayExtend
            // 
            this.btn_DelayExtend.Location = new System.Drawing.Point(258, 7);
            this.btn_DelayExtend.Name = "btn_DelayExtend";
            this.btn_DelayExtend.Size = new System.Drawing.Size(104, 31);
            this.btn_DelayExtend.TabIndex = 1;
            this.btn_DelayExtend.Text = "Delay";
            this.btn_DelayExtend.UseVisualStyleBackColor = true;
            this.btn_DelayExtend.Click += new System.EventHandler(this.btn_DelayExtend_Click);
            // 
            // lbl_Delay
            // 
            this.lbl_Delay.BackColor = System.Drawing.Color.White;
            this.lbl_Delay.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_Delay.Location = new System.Drawing.Point(368, 8);
            this.lbl_Delay.Name = "lbl_Delay";
            this.lbl_Delay.Size = new System.Drawing.Size(112, 28);
            this.lbl_Delay.TabIndex = 6;
            this.lbl_Delay.Text = "0";
            this.lbl_Delay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_Identifier
            // 
            this.lbl_Identifier.Location = new System.Drawing.Point(486, 14);
            this.lbl_Identifier.Name = "lbl_Identifier";
            this.lbl_Identifier.Size = new System.Drawing.Size(44, 18);
            this.lbl_Identifier.TabIndex = 13;
            this.lbl_Identifier.Text = "ms";
            // 
            // lbl_Label
            // 
            this.lbl_Label.Location = new System.Drawing.Point(17, 14);
            this.lbl_Label.Name = "lbl_Label";
            this.lbl_Label.Size = new System.Drawing.Size(235, 18);
            this.lbl_Label.TabIndex = 14;
            this.lbl_Label.Text = "LABEL";
            // 
            // UserCtrl_DelayNTimeoutsMisc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lbl_Label);
            this.Controls.Add(this.lbl_Identifier);
            this.Controls.Add(this.lbl_Delay);
            this.Controls.Add(this.btn_DelayExtend);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_DelayNTimeoutsMisc";
            this.Size = new System.Drawing.Size(542, 49);
            this.Load += new System.EventHandler(this.UserCtrl_DelayNTimeoutsMisc_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_DelayExtend;
        private System.Windows.Forms.Label lbl_Delay;
        private System.Windows.Forms.Label lbl_Identifier;
        private System.Windows.Forms.Label lbl_Label;
    }
}
