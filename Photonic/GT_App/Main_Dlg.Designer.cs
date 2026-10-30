namespace GT_App
{
	partial class Main_Dlg
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main_Dlg));
            this.btn_Dummy001 = new System.Windows.Forms.Button();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // btn_Dummy001
            // 
            this.btn_Dummy001.Location = new System.Drawing.Point(679, 14);
            this.btn_Dummy001.Name = "btn_Dummy001";
            this.btn_Dummy001.Size = new System.Drawing.Size(127, 42);
            this.btn_Dummy001.TabIndex = 0;
            this.btn_Dummy001.Text = "Dummy001";
            this.btn_Dummy001.UseVisualStyleBackColor = true;
            this.btn_Dummy001.Visible = false;
            this.btn_Dummy001.Click += new System.EventHandler(this.btn_Dummy001_Click);
            // 
            // m_Timer
            // 
            this.m_Timer.Enabled = true;
            this.m_Timer.Tick += new System.EventHandler(this.On_Timer);
            // 
            // Main_Dlg
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(826, 485);
            this.ControlBox = false;
            this.Controls.Add(this.btn_Dummy001);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Main_Dlg";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Main Dialog";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_Dlg_FormClosing);
            this.Load += new System.EventHandler(this.Main_Dlg_Load);
            this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_Dummy001;
        private System.Windows.Forms.Timer m_Timer;
	}
}

