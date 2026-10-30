namespace GT_App
{
    partial class SetFilePathDialog
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
            this.txtb_PackageFilePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_FileDialog_PackageFilePath = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.btn_UpdateEsc = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtb_PackageFilePath
            // 
            this.txtb_PackageFilePath.BackColor = System.Drawing.Color.White;
            this.txtb_PackageFilePath.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtb_PackageFilePath.Location = new System.Drawing.Point(27, 39);
            this.txtb_PackageFilePath.Multiline = true;
            this.txtb_PackageFilePath.Name = "txtb_PackageFilePath";
            this.txtb_PackageFilePath.ReadOnly = true;
            this.txtb_PackageFilePath.Size = new System.Drawing.Size(342, 45);
            this.txtb_PackageFilePath.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(139, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Package File Path:";
            // 
            // btn_FileDialog_PackageFilePath
            // 
            this.btn_FileDialog_PackageFilePath.Location = new System.Drawing.Point(377, 39);
            this.btn_FileDialog_PackageFilePath.Name = "btn_FileDialog_PackageFilePath";
            this.btn_FileDialog_PackageFilePath.Size = new System.Drawing.Size(32, 22);
            this.btn_FileDialog_PackageFilePath.TabIndex = 8;
            this.btn_FileDialog_PackageFilePath.Text = "...";
            this.btn_FileDialog_PackageFilePath.UseVisualStyleBackColor = true;
            this.btn_FileDialog_PackageFilePath.Click += new System.EventHandler(this.btn_FileDialog_PackageFilePath_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(269, 425);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(149, 53);
            this.btn_ESC.TabIndex = 11;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_UpdateEsc
            // 
            this.btn_UpdateEsc.Location = new System.Drawing.Point(114, 425);
            this.btn_UpdateEsc.Name = "btn_UpdateEsc";
            this.btn_UpdateEsc.Size = new System.Drawing.Size(149, 53);
            this.btn_UpdateEsc.TabIndex = 12;
            this.btn_UpdateEsc.Text = "Update and ESC";
            this.btn_UpdateEsc.UseVisualStyleBackColor = true;
            this.btn_UpdateEsc.Click += new System.EventHandler(this.btn_UpdateEsc_Click);
            // 
            // SetFilePathDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(441, 488);
            this.Controls.Add(this.btn_UpdateEsc);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.btn_FileDialog_PackageFilePath);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtb_PackageFilePath);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "SetFilePathDialog";
            this.Text = "Set File Path";
            this.Load += new System.EventHandler(this.SetFilePathDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_PackageFilePath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_FileDialog_PackageFilePath;
        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_UpdateEsc;
    }
}