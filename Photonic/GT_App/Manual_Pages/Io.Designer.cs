namespace GT_App
{
    partial class Io
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
            this.tabctrl_IO = new System.Windows.Forms.TabControl();
            this.SuspendLayout();
            // 
            // tabctrl_IO
            // 
            this.tabctrl_IO.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabctrl_IO.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabctrl_IO.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabctrl_IO.ItemSize = new System.Drawing.Size(60, 120);
            this.tabctrl_IO.Location = new System.Drawing.Point(0, 0);
            this.tabctrl_IO.Margin = new System.Windows.Forms.Padding(6, 6, 6, 6);
            this.tabctrl_IO.Multiline = true;
            this.tabctrl_IO.Name = "tabctrl_IO";
            this.tabctrl_IO.SelectedIndex = 0;
            this.tabctrl_IO.Size = new System.Drawing.Size(1042, 629);
            this.tabctrl_IO.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabctrl_IO.TabIndex = 0;
            this.tabctrl_IO.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabctrl_IO_DrawItem);
            this.tabctrl_IO.SelectedIndexChanged += new System.EventHandler(this.tabctrl_IO_SelectedIndexChanged);
            // 
            // Io
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1042, 629);
            this.Controls.Add(this.tabctrl_IO);
            this.Name = "Io";
            this.Text = "Io";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Io_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabctrl_IO;
    }
}