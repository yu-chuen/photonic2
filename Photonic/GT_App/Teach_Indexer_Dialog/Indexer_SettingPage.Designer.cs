namespace GT_App
{
    partial class Indexer_SettingPage
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
            this.btn_UpdateEsc = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_UpdateEsc
            // 
            this.btn_UpdateEsc.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_UpdateEsc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_UpdateEsc.Location = new System.Drawing.Point(17, 480);
            this.btn_UpdateEsc.Name = "btn_UpdateEsc";
            this.btn_UpdateEsc.Size = new System.Drawing.Size(135, 49);
            this.btn_UpdateEsc.TabIndex = 19;
            this.btn_UpdateEsc.Text = "UPDATE AND ESC";
            this.btn_UpdateEsc.UseVisualStyleBackColor = true;
            this.btn_UpdateEsc.Click += new System.EventHandler(this.btn_UpdateEsc_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ESC.Location = new System.Drawing.Point(158, 480);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(135, 49);
            this.btn_ESC.TabIndex = 20;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // Indexer_SettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(311, 534);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.btn_UpdateEsc);
            this.Name = "Indexer_SettingPage";
            this.Text = "Indexer Setting Page";
            this.Load += new System.EventHandler(this.Indexer_SettingPage_Load);
            this.ResumeLayout(false);

        }

        #endregion

		private System.Windows.Forms.Button btn_UpdateEsc;
		private System.Windows.Forms.Button btn_ESC;

    }
}