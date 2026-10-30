namespace GT_App
{
    partial class MessageBox_CustomDlg_3Inputs
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
            this.picb_Icon = new System.Windows.Forms.PictureBox();
            this.lbl_ErrorMsg = new System.Windows.Forms.Label();
            this.btn_Fail = new System.Windows.Forms.Button();
            this.btn_Redo = new System.Windows.Forms.Button();
            this.btn_Pass = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picb_Icon)).BeginInit();
            this.SuspendLayout();
            // 
            // picb_Icon
            // 
            this.picb_Icon.BackColor = System.Drawing.Color.Transparent;
            this.picb_Icon.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picb_Icon.Image = global::GT_App.Properties.Resources.iconInfo;
            this.picb_Icon.InitialImage = null;
            this.picb_Icon.Location = new System.Drawing.Point(11, 23);
            this.picb_Icon.Name = "picb_Icon";
            this.picb_Icon.Size = new System.Drawing.Size(49, 42);
            this.picb_Icon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picb_Icon.TabIndex = 5;
            this.picb_Icon.TabStop = false;
            // 
            // lbl_ErrorMsg
            // 
            this.lbl_ErrorMsg.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_ErrorMsg.Location = new System.Drawing.Point(66, 23);
            this.lbl_ErrorMsg.Name = "lbl_ErrorMsg";
            this.lbl_ErrorMsg.Size = new System.Drawing.Size(544, 101);
            this.lbl_ErrorMsg.TabIndex = 4;
            this.lbl_ErrorMsg.Text = "Message 1 / Error Message";
            // 
            // btn_Fail
            // 
            this.btn_Fail.Location = new System.Drawing.Point(438, 201);
            this.btn_Fail.Name = "btn_Fail";
            this.btn_Fail.Size = new System.Drawing.Size(177, 51);
            this.btn_Fail.TabIndex = 3;
            this.btn_Fail.Text = "Fail";
            this.btn_Fail.UseVisualStyleBackColor = true;
            this.btn_Fail.Click += new System.EventHandler(this.btn_Fail_Click);
            // 
            // btn_Redo
            // 
            this.btn_Redo.Location = new System.Drawing.Point(259, 201);
            this.btn_Redo.Name = "btn_Redo";
            this.btn_Redo.Size = new System.Drawing.Size(177, 51);
            this.btn_Redo.TabIndex = 2;
            this.btn_Redo.Text = "Redo";
            this.btn_Redo.UseVisualStyleBackColor = true;
            this.btn_Redo.Click += new System.EventHandler(this.btn_Redo_Click);
            // 
            // btn_Pass
            // 
            this.btn_Pass.Location = new System.Drawing.Point(80, 201);
            this.btn_Pass.Name = "btn_Pass";
            this.btn_Pass.Size = new System.Drawing.Size(177, 51);
            this.btn_Pass.TabIndex = 1;
            this.btn_Pass.Text = "Pass";
            this.btn_Pass.UseVisualStyleBackColor = true;
            this.btn_Pass.Click += new System.EventHandler(this.btn_Pass_Click);
            // 
            // MessageBox_CustomDlg_3Inputs
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(633, 264);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Pass);
            this.Controls.Add(this.btn_Redo);
            this.Controls.Add(this.btn_Fail);
            this.Controls.Add(this.picb_Icon);
            this.Controls.Add(this.lbl_ErrorMsg);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MessageBox_CustomDlg_3Inputs";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Selection";
            ((System.ComponentModel.ISupportInitialize)(this.picb_Icon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picb_Icon;
        private System.Windows.Forms.Label lbl_ErrorMsg;
        private System.Windows.Forms.Button btn_Fail;
        private System.Windows.Forms.Button btn_Redo;
        private System.Windows.Forms.Button btn_Pass;

    }
}