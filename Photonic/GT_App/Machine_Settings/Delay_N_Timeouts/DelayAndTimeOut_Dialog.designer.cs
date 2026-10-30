namespace GT_App
{
    partial class DelayAndTimeOut_Dialog
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
            this.tabCtrl_DelayNTimeouts = new System.Windows.Forms.TabControl();
            this.btn_Close = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabCtrl_DelayNTimeouts
            // 
            this.tabCtrl_DelayNTimeouts.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabCtrl_DelayNTimeouts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabCtrl_DelayNTimeouts.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabCtrl_DelayNTimeouts.ItemSize = new System.Drawing.Size(60, 120);
            this.tabCtrl_DelayNTimeouts.Location = new System.Drawing.Point(0, 0);
            this.tabCtrl_DelayNTimeouts.Multiline = true;
            this.tabCtrl_DelayNTimeouts.Name = "tabCtrl_DelayNTimeouts";
            this.tabCtrl_DelayNTimeouts.SelectedIndex = 0;
            this.tabCtrl_DelayNTimeouts.Size = new System.Drawing.Size(1284, 670);
            this.tabCtrl_DelayNTimeouts.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabCtrl_DelayNTimeouts.TabIndex = 0;
            this.tabCtrl_DelayNTimeouts.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabCtrl_DelayNTimeouts_DrawItem);
            // 
            // btn_Close
            // 
            this.btn_Close.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btn_Close.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Close.Location = new System.Drawing.Point(-1, 605);
            this.btn_Close.Name = "btn_Close";
            this.btn_Close.Size = new System.Drawing.Size(124, 66);
            this.btn_Close.TabIndex = 1;
            this.btn_Close.Text = "Close";
            this.btn_Close.UseVisualStyleBackColor = true;
            this.btn_Close.Click += new System.EventHandler(this.btn_Close_Click);
            // 
            // DelayAndTimeOut_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_Close;
            this.ClientSize = new System.Drawing.Size(1284, 670);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Close);
            this.Controls.Add(this.tabCtrl_DelayNTimeouts);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "DelayAndTimeOut_Dialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Delay and Time Out";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.DelayAndTimeOut_Dialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrl_DelayNTimeouts;
        private System.Windows.Forms.Button btn_Close;
    }
}