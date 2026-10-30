namespace GT_App
{
    partial class MessageBox_CustomDlg
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
            this.lbl_ErrorMsg = new System.Windows.Forms.Label();
            this.lbl_ErrorRecovery = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.picb_Icon = new System.Windows.Forms.PictureBox();
            this.btn_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picb_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_ErrorMsg
            // 
            this.lbl_ErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorMsg.Location = new System.Drawing.Point(67, 22);
            this.lbl_ErrorMsg.Name = "lbl_ErrorMsg";
            this.lbl_ErrorMsg.Size = new System.Drawing.Size(544, 169);
            this.lbl_ErrorMsg.TabIndex = 0;
            this.lbl_ErrorMsg.Text = "Message 1 / Error Message";
            // 
            // lbl_ErrorRecovery
            // 
            this.lbl_ErrorRecovery.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorRecovery.Location = new System.Drawing.Point(12, 243);
            this.lbl_ErrorRecovery.Name = "lbl_ErrorRecovery";
            this.lbl_ErrorRecovery.Size = new System.Drawing.Size(163, 19);
            this.lbl_ErrorRecovery.TabIndex = 1;
            this.lbl_ErrorRecovery.Text = "Message 2 / Recovery Message";
            this.lbl_ErrorRecovery.Visible = false;
            // 
            // btn_OK
            // 
            this.btn_OK.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_OK.Location = new System.Drawing.Point(454, 211);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(177, 51);
            this.btn_OK.TabIndex = 2;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // picb_Icon
            // 
            this.picb_Icon.BackColor = System.Drawing.Color.Transparent;
            this.picb_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picb_Icon.Image = global::GT_App.Properties.Resources.icon_err;
            this.picb_Icon.InitialImage = null;
            this.picb_Icon.Location = new System.Drawing.Point(12, 22);
            this.picb_Icon.Name = "picb_Icon";
            this.picb_Icon.Size = new System.Drawing.Size(49, 42);
            this.picb_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb_Icon.TabIndex = 3;
            this.picb_Icon.TabStop = false;
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Cancel.Location = new System.Drawing.Point(271, 211);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(177, 51);
            this.btn_Cancel.TabIndex = 4;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Visible = false;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // MessageBox_CustomDlg
            // 
            this.AcceptButton = this.btn_OK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(643, 274);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.picb_Icon);
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.lbl_ErrorRecovery);
            this.Controls.Add(this.lbl_ErrorMsg);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "MessageBox_CustomDlg";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Error";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MessageBox_CustomDlg_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picb_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lbl_ErrorMsg;
        private System.Windows.Forms.Label lbl_ErrorRecovery;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.PictureBox picb_Icon;
        private System.Windows.Forms.Button btn_Cancel;
    }
}