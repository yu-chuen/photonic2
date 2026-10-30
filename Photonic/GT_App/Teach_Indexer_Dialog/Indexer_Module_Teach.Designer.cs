namespace GT_App
{
    partial class Indexer_Module_Teach
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
            this.tabctrl_IndexerPg = new System.Windows.Forms.TabControl();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.m_Timer = new System.Windows.Forms.Timer(this.components);
            this.btn_KeyPressEnable = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // tabctrl_IndexerPg
            // 
            this.tabctrl_IndexerPg.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabctrl_IndexerPg.Location = new System.Drawing.Point(12, 12);
            this.tabctrl_IndexerPg.Name = "tabctrl_IndexerPg";
            this.tabctrl_IndexerPg.SelectedIndex = 0;
            this.tabctrl_IndexerPg.Size = new System.Drawing.Size(847, 507);
            this.tabctrl_IndexerPg.TabIndex = 0;
            this.tabctrl_IndexerPg.SelectedIndexChanged += new System.EventHandler(this.tabctrl_IndexerPg_SelectedIndexChanged);
            this.tabctrl_IndexerPg.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Indexer_Module_Teach_KeyDown);
            // 
            // btn_ESC
            // 
            this.btn_ESC.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_ESC.Location = new System.Drawing.Point(700, 531);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(159, 56);
            this.btn_ESC.TabIndex = 1;
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
            // btn_KeyPressEnable
            // 
            this.btn_KeyPressEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_KeyPressEnable.Location = new System.Drawing.Point(543, 554);
            this.btn_KeyPressEnable.Name = "btn_KeyPressEnable";
            this.btn_KeyPressEnable.Size = new System.Drawing.Size(143, 33);
            this.btn_KeyPressEnable.TabIndex = 2;
            this.btn_KeyPressEnable.Text = "Key Press Disabled";
            this.btn_KeyPressEnable.UseVisualStyleBackColor = true;
            this.btn_KeyPressEnable.Click += new System.EventHandler(this.btn_KeyPressEnable_Click);
            // 
            // Indexer_Module_Teach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(871, 599);
            this.Controls.Add(this.btn_KeyPressEnable);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.tabctrl_IndexerPg);
            this.Name = "Indexer_Module_Teach";
            this.Text = "Indexer Module Teaching Page";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Indexer_Module_Teach_FormClosing);
            this.Load += new System.EventHandler(this.Indexer_Module_Teach_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Indexer_Module_Teach_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabctrl_IndexerPg;
		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Timer m_Timer;
        private System.Windows.Forms.Button btn_KeyPressEnable;
    }
}