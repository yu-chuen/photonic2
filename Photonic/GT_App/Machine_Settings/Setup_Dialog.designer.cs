namespace GT_App
{
    partial class Setup_Dialog
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
            this.btn_UpdateESC = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.lbl_PackageFileSelect = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.listView_PackageFile = new System.Windows.Forms.ListView();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // btn_UpdateESC
            // 
            this.btn_UpdateESC.Location = new System.Drawing.Point(12, 395);
            this.btn_UpdateESC.Name = "btn_UpdateESC";
            this.btn_UpdateESC.Size = new System.Drawing.Size(136, 56);
            this.btn_UpdateESC.TabIndex = 1;
            this.btn_UpdateESC.Text = "Update and ESC";
            this.btn_UpdateESC.UseVisualStyleBackColor = true;
            this.btn_UpdateESC.Click += new System.EventHandler(this.btn_UpdateESC_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(154, 395);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(136, 56);
            this.btn_ESC.TabIndex = 2;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.lbl_PackageFileSelect);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.listView_PackageFile);
            this.groupBox2.Location = new System.Drawing.Point(12, 12);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(278, 377);
            this.groupBox2.TabIndex = 9;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Package File";
            // 
            // lbl_PackageFileSelect
            // 
            this.lbl_PackageFileSelect.BackColor = System.Drawing.Color.White;
            this.lbl_PackageFileSelect.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lbl_PackageFileSelect.Location = new System.Drawing.Point(19, 329);
            this.lbl_PackageFileSelect.Name = "lbl_PackageFileSelect";
            this.lbl_PackageFileSelect.Size = new System.Drawing.Size(236, 25);
            this.lbl_PackageFileSelect.TabIndex = 14;
            this.lbl_PackageFileSelect.Text = "...";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(16, 309);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(166, 16);
            this.label6.TabIndex = 13;
            this.label6.Text = "Selected Package File";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 22);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(149, 16);
            this.label4.TabIndex = 11;
            this.label4.Text = "Package File Listing";
            // 
            // listView_PackageFile
            // 
            this.listView_PackageFile.Location = new System.Drawing.Point(19, 42);
            this.listView_PackageFile.MultiSelect = false;
            this.listView_PackageFile.Name = "listView_PackageFile";
            this.listView_PackageFile.Size = new System.Drawing.Size(236, 256);
            this.listView_PackageFile.TabIndex = 10;
            this.listView_PackageFile.UseCompatibleStateImageBehavior = false;
            this.listView_PackageFile.View = System.Windows.Forms.View.List;
            this.listView_PackageFile.SelectedIndexChanged += new System.EventHandler(this.listView_PackageFile_SelectedIndexChanged);
            // 
            // Setup_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(302, 461);
            this.ControlBox = false;
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.btn_UpdateESC);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Setup_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Setup";
            this.Load += new System.EventHandler(this.Setup_Dialog_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_UpdateESC;
		private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label lbl_PackageFileSelect;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ListView listView_PackageFile;
    }
}