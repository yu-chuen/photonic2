namespace GT_App
{
    partial class Manual_OperationsMainDlg
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
            this.tabctrl_ManualOps = new System.Windows.Forms.TabControl();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabctrl_ManualOps
            // 
            this.tabctrl_ManualOps.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabctrl_ManualOps.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrl_ManualOps.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabctrl_ManualOps.ItemSize = new System.Drawing.Size(60, 120);
            this.tabctrl_ManualOps.Location = new System.Drawing.Point(0, 0);
            this.tabctrl_ManualOps.Margin = new System.Windows.Forms.Padding(4);
            this.tabctrl_ManualOps.Multiline = true;
            this.tabctrl_ManualOps.Name = "tabctrl_ManualOps";
            this.tabctrl_ManualOps.SelectedIndex = 0;
            this.tabctrl_ManualOps.Size = new System.Drawing.Size(1196, 630);
            this.tabctrl_ManualOps.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabctrl_ManualOps.TabIndex = 0;
            this.tabctrl_ManualOps.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabctrl_ManualOps_DrawItem);
            this.tabctrl_ManualOps.SelectedIndexChanged += new System.EventHandler(this.tabctrl_ManualOps_SelectedIndexChanged);
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(-1, 565);
            this.btn_ESC.Margin = new System.Windows.Forms.Padding(4);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(124, 66);
            this.btn_ESC.TabIndex = 1;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // Manual_OperationsMainDlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(1196, 630);
            this.ControlBox = false;
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.tabctrl_ManualOps);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Manual_OperationsMainDlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Manual Operations";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabctrl_ManualOps;
        private System.Windows.Forms.Button btn_ESC;
    }
}