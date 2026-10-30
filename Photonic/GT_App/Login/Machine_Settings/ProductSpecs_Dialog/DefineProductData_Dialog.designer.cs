namespace GT_App
{
    partial class DefineProductData_Dialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtb_DataFileName = new System.Windows.Forms.TextBox();
            this.txtb_DataFilePath = new System.Windows.Forms.TextBox();
            this.txtb_DataFileExtension = new System.Windows.Forms.TextBox();
            this.listb_ProductPackageList = new System.Windows.Forms.ListBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_IndexerSettings = new System.Windows.Forms.Button();
            this.btn_RobotSettings_Dlg = new System.Windows.Forms.Button();
            this.btn_ProductSpecs = new System.Windows.Forms.Button();
            this.btn_SetFilePathDlg = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_DeletePackageFile = new System.Windows.Forms.Button();
            this.btn_CopyPackageFile = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_AddPackageFile = new System.Windows.Forms.Button();
            this.btn_RobotSettings_HotBlower_Dlg = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(23, 17);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Data File Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(23, 45);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "Local Data File Path:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(495, 17);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 16);
            this.label3.TabIndex = 2;
            this.label3.Text = "Data File Extension:";
            // 
            // txtb_DataFileName
            // 
            this.txtb_DataFileName.BackColor = System.Drawing.Color.White;
            this.txtb_DataFileName.Location = new System.Drawing.Point(183, 14);
            this.txtb_DataFileName.Name = "txtb_DataFileName";
            this.txtb_DataFileName.ReadOnly = true;
            this.txtb_DataFileName.Size = new System.Drawing.Size(305, 22);
            this.txtb_DataFileName.TabIndex = 3;
            // 
            // txtb_DataFilePath
            // 
            this.txtb_DataFilePath.BackColor = System.Drawing.Color.White;
            this.txtb_DataFilePath.Location = new System.Drawing.Point(183, 42);
            this.txtb_DataFilePath.Multiline = true;
            this.txtb_DataFilePath.Name = "txtb_DataFilePath";
            this.txtb_DataFilePath.ReadOnly = true;
            this.txtb_DataFilePath.Size = new System.Drawing.Size(543, 57);
            this.txtb_DataFilePath.TabIndex = 4;
            // 
            // txtb_DataFileExtension
            // 
            this.txtb_DataFileExtension.BackColor = System.Drawing.Color.White;
            this.txtb_DataFileExtension.Location = new System.Drawing.Point(648, 14);
            this.txtb_DataFileExtension.Name = "txtb_DataFileExtension";
            this.txtb_DataFileExtension.ReadOnly = true;
            this.txtb_DataFileExtension.Size = new System.Drawing.Size(78, 22);
            this.txtb_DataFileExtension.TabIndex = 5;
            // 
            // listb_ProductPackageList
            // 
            this.listb_ProductPackageList.FormattingEnabled = true;
            this.listb_ProductPackageList.ItemHeight = 16;
            this.listb_ProductPackageList.Location = new System.Drawing.Point(25, 187);
            this.listb_ProductPackageList.Name = "listb_ProductPackageList";
            this.listb_ProductPackageList.Size = new System.Drawing.Size(375, 228);
            this.listb_ProductPackageList.TabIndex = 6;
            this.listb_ProductPackageList.SelectedIndexChanged += new System.EventHandler(this.listb_ProductPackageList_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_RobotSettings_HotBlower_Dlg);
            this.groupBox1.Controls.Add(this.btn_IndexerSettings);
            this.groupBox1.Controls.Add(this.btn_RobotSettings_Dlg);
            this.groupBox1.Controls.Add(this.btn_ProductSpecs);
            this.groupBox1.Controls.Add(this.btn_SetFilePathDlg);
            this.groupBox1.Location = new System.Drawing.Point(426, 117);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(300, 257);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Instructions";
            // 
            // btn_IndexerSettings
            // 
            this.btn_IndexerSettings.Location = new System.Drawing.Point(158, 142);
            this.btn_IndexerSettings.Name = "btn_IndexerSettings";
            this.btn_IndexerSettings.Size = new System.Drawing.Size(129, 50);
            this.btn_IndexerSettings.TabIndex = 14;
            this.btn_IndexerSettings.Text = "Indexer Settings";
            this.btn_IndexerSettings.UseVisualStyleBackColor = true;
            this.btn_IndexerSettings.Visible = false;
            this.btn_IndexerSettings.Click += new System.EventHandler(this.btn_IndexerSettings_Click);
            // 
            // btn_RobotSettings_Dlg
            // 
            this.btn_RobotSettings_Dlg.Location = new System.Drawing.Point(159, 86);
            this.btn_RobotSettings_Dlg.Name = "btn_RobotSettings_Dlg";
            this.btn_RobotSettings_Dlg.Size = new System.Drawing.Size(129, 50);
            this.btn_RobotSettings_Dlg.TabIndex = 13;
            this.btn_RobotSettings_Dlg.Text = "Robot Settings";
            this.btn_RobotSettings_Dlg.UseVisualStyleBackColor = true;
            this.btn_RobotSettings_Dlg.Click += new System.EventHandler(this.btn_RobotSettings_Dlg_Click);
            // 
            // btn_ProductSpecs
            // 
            this.btn_ProductSpecs.Location = new System.Drawing.Point(158, 31);
            this.btn_ProductSpecs.Name = "btn_ProductSpecs";
            this.btn_ProductSpecs.Size = new System.Drawing.Size(129, 50);
            this.btn_ProductSpecs.TabIndex = 10;
            this.btn_ProductSpecs.Text = "Product Specs";
            this.btn_ProductSpecs.UseVisualStyleBackColor = true;
            this.btn_ProductSpecs.Click += new System.EventHandler(this.btn_ProductSpecs_Click);
            // 
            // btn_SetFilePathDlg
            // 
            this.btn_SetFilePathDlg.Location = new System.Drawing.Point(23, 31);
            this.btn_SetFilePathDlg.Name = "btn_SetFilePathDlg";
            this.btn_SetFilePathDlg.Size = new System.Drawing.Size(129, 50);
            this.btn_SetFilePathDlg.TabIndex = 9;
            this.btn_SetFilePathDlg.Text = "Set File Paths";
            this.btn_SetFilePathDlg.UseVisualStyleBackColor = true;
            this.btn_SetFilePathDlg.Click += new System.EventHandler(this.btn_SetFilePathDlg_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(600, 380);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(129, 50);
            this.btn_ESC.TabIndex = 12;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_DeletePackageFile);
            this.groupBox2.Controls.Add(this.btn_CopyPackageFile);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btn_AddPackageFile);
            this.groupBox2.Location = new System.Drawing.Point(12, 117);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(408, 313);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Product Package File List:";
            // 
            // btn_DeletePackageFile
            // 
            this.btn_DeletePackageFile.Location = new System.Drawing.Point(270, 30);
            this.btn_DeletePackageFile.Name = "btn_DeletePackageFile";
            this.btn_DeletePackageFile.Size = new System.Drawing.Size(117, 34);
            this.btn_DeletePackageFile.TabIndex = 15;
            this.btn_DeletePackageFile.Text = "Delete";
            this.btn_DeletePackageFile.UseVisualStyleBackColor = true;
            this.btn_DeletePackageFile.Click += new System.EventHandler(this.btn_DeletePackageFile_Click);
            // 
            // btn_CopyPackageFile
            // 
            this.btn_CopyPackageFile.Location = new System.Drawing.Point(142, 30);
            this.btn_CopyPackageFile.Name = "btn_CopyPackageFile";
            this.btn_CopyPackageFile.Size = new System.Drawing.Size(117, 34);
            this.btn_CopyPackageFile.TabIndex = 15;
            this.btn_CopyPackageFile.Text = "Copy";
            this.btn_CopyPackageFile.UseVisualStyleBackColor = true;
            this.btn_CopyPackageFile.Click += new System.EventHandler(this.btn_CopyPackageFile_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(146, 139);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(117, 34);
            this.button2.TabIndex = 15;
            this.button2.Text = "ESC";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // btn_AddPackageFile
            // 
            this.btn_AddPackageFile.Location = new System.Drawing.Point(14, 30);
            this.btn_AddPackageFile.Name = "btn_AddPackageFile";
            this.btn_AddPackageFile.Size = new System.Drawing.Size(117, 34);
            this.btn_AddPackageFile.TabIndex = 14;
            this.btn_AddPackageFile.Text = "Add";
            this.btn_AddPackageFile.UseVisualStyleBackColor = true;
            this.btn_AddPackageFile.Click += new System.EventHandler(this.btn_AddPackageFile_Click);
            // 
            // btn_RobotSettings_HotBlower_Dlg
            // 
            this.btn_RobotSettings_HotBlower_Dlg.Location = new System.Drawing.Point(23, 87);
            this.btn_RobotSettings_HotBlower_Dlg.Name = "btn_RobotSettings_HotBlower_Dlg";
            this.btn_RobotSettings_HotBlower_Dlg.Size = new System.Drawing.Size(129, 50);
            this.btn_RobotSettings_HotBlower_Dlg.TabIndex = 15;
            this.btn_RobotSettings_HotBlower_Dlg.Text = "Robot Settings Hot Blower";
            this.btn_RobotSettings_HotBlower_Dlg.UseVisualStyleBackColor = true;
            this.btn_RobotSettings_HotBlower_Dlg.Click += new System.EventHandler(this.btn_RobotSettings_HotBlower_Dlg_Click);
            // 
            // DefineProductData_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(737, 476);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.listb_ProductPackageList);
            this.Controls.Add(this.txtb_DataFileExtension);
            this.Controls.Add(this.txtb_DataFilePath);
            this.Controls.Add(this.txtb_DataFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "DefineProductData_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Define Product Data";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DefineProductData_Dialog_FormClosing);
            this.Load += new System.EventHandler(this.DefineProductData_Dialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtb_DataFileName;
        private System.Windows.Forms.TextBox txtb_DataFilePath;
        private System.Windows.Forms.TextBox txtb_DataFileExtension;
        private System.Windows.Forms.ListBox listb_ProductPackageList;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_SetFilePathDlg;
        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btn_DeletePackageFile;
        private System.Windows.Forms.Button btn_CopyPackageFile;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_AddPackageFile;
        private System.Windows.Forms.Button btn_ProductSpecs;
		private System.Windows.Forms.Button btn_RobotSettings_Dlg;
        private System.Windows.Forms.Button btn_IndexerSettings;
        private System.Windows.Forms.Button btn_RobotSettings_HotBlower_Dlg;
    }
}