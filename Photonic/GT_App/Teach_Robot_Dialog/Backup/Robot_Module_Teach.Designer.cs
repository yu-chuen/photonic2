namespace GT_App
{
    partial class Robot_Module_Teach
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
			this.tabctrl_MotorPg = new System.Windows.Forms.TabControl();
			this.btn_ESC = new System.Windows.Forms.Button();
			this.m_Timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// tabctrl_MotorPg
			// 
			this.tabctrl_MotorPg.Location = new System.Drawing.Point(12, 12);
			this.tabctrl_MotorPg.Name = "tabctrl_MotorPg";
			this.tabctrl_MotorPg.SelectedIndex = 0;
			this.tabctrl_MotorPg.Size = new System.Drawing.Size(1014, 670);
			this.tabctrl_MotorPg.TabIndex = 0;
			// 
			// btn_ESC
			// 
			this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btn_ESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.btn_ESC.Location = new System.Drawing.Point(1030, 628);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(175, 55);
			this.btn_ESC.TabIndex = 11;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = true;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// m_Timer
			// 
			this.m_Timer.Enabled = true;
			this.m_Timer.Interval = 500;
			this.m_Timer.Tick += new System.EventHandler(this.OnTimer);
			// 
			// Robot_Module_Teach
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btn_ESC;
			this.ClientSize = new System.Drawing.Size(1211, 694);
			this.Controls.Add(this.btn_ESC);
			this.Controls.Add(this.tabctrl_MotorPg);
			this.Name = "Robot_Module_Teach";
			this.Text = "Robot Module Teaching Page";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Robot_Module_Teach_FormClosing);
			this.Load += new System.EventHandler(this.Robot_Module_Teach_Load);
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Robot_Module_Teach_KeyDown);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabctrl_MotorPg;
        private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Timer m_Timer;
    }
}