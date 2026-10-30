namespace GT_App
{
    partial class UserCtrl_TCP_SettingPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb_SendTimeout = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtb_ReceiveTimeout = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress4 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress1 = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtb_IPAddress3 = new System.Windows.Forms.TextBox();
            this.txtb_IPAddress2 = new System.Windows.Forms.TextBox();
            this.txtb_CommandDelay = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txtb_PortNum = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtb_SendTimeout);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtb_ReceiveTimeout);
            this.groupBox1.Controls.Add(this.txtb_IPAddress4);
            this.groupBox1.Controls.Add(this.txtb_IPAddress1);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtb_IPAddress3);
            this.groupBox1.Controls.Add(this.txtb_IPAddress2);
            this.groupBox1.Controls.Add(this.txtb_CommandDelay);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtb_PortNum);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 192);
            this.groupBox1.TabIndex = 20;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "TCP Settings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(8, 144);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(108, 16);
            this.label2.TabIndex = 27;
            this.label2.Text = "Send Timeout:";
            // 
            // txtb_SendTimeout
            // 
            this.txtb_SendTimeout.Location = new System.Drawing.Point(144, 144);
            this.txtb_SendTimeout.Name = "txtb_SendTimeout";
            this.txtb_SendTimeout.Size = new System.Drawing.Size(113, 22);
            this.txtb_SendTimeout.TabIndex = 26;
            this.txtb_SendTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_SendTimeout.TextChanged += new System.EventHandler(this.txtb_SendTimeout_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(8, 120);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(130, 16);
            this.label1.TabIndex = 25;
            this.label1.Text = "Receive Timeout:";
            // 
            // txtb_ReceiveTimeout
            // 
            this.txtb_ReceiveTimeout.Location = new System.Drawing.Point(144, 120);
            this.txtb_ReceiveTimeout.Name = "txtb_ReceiveTimeout";
            this.txtb_ReceiveTimeout.Size = new System.Drawing.Size(113, 22);
            this.txtb_ReceiveTimeout.TabIndex = 24;
            this.txtb_ReceiveTimeout.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_ReceiveTimeout.TextChanged += new System.EventHandler(this.txtb_ReceiveTimeout_TextChanged);
            // 
            // txtb_IPAddress4
            // 
            this.txtb_IPAddress4.Location = new System.Drawing.Point(262, 31);
            this.txtb_IPAddress4.Name = "txtb_IPAddress4";
            this.txtb_IPAddress4.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress4.TabIndex = 23;
            this.txtb_IPAddress4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress4.TextChanged += new System.EventHandler(this.txtb_IPAddress4_TextChanged);
            // 
            // txtb_IPAddress1
            // 
            this.txtb_IPAddress1.Location = new System.Drawing.Point(127, 31);
            this.txtb_IPAddress1.Name = "txtb_IPAddress1";
            this.txtb_IPAddress1.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress1.TabIndex = 22;
            this.txtb_IPAddress1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress1.TextChanged += new System.EventHandler(this.txtb_IPAddress1_TextChanged);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(29, 90);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 16);
            this.label7.TabIndex = 18;
            this.label7.Text = "Send Delay:";
            // 
            // txtb_IPAddress3
            // 
            this.txtb_IPAddress3.Location = new System.Drawing.Point(217, 31);
            this.txtb_IPAddress3.Name = "txtb_IPAddress3";
            this.txtb_IPAddress3.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress3.TabIndex = 21;
            this.txtb_IPAddress3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress3.TextChanged += new System.EventHandler(this.txtb_IPAddress3_TextChanged);
            // 
            // txtb_IPAddress2
            // 
            this.txtb_IPAddress2.Location = new System.Drawing.Point(172, 31);
            this.txtb_IPAddress2.Name = "txtb_IPAddress2";
            this.txtb_IPAddress2.Size = new System.Drawing.Size(39, 22);
            this.txtb_IPAddress2.TabIndex = 20;
            this.txtb_IPAddress2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_IPAddress2.TextChanged += new System.EventHandler(this.txtb_IPAddress2_TextChanged);
            // 
            // txtb_CommandDelay
            // 
            this.txtb_CommandDelay.Location = new System.Drawing.Point(128, 87);
            this.txtb_CommandDelay.Name = "txtb_CommandDelay";
            this.txtb_CommandDelay.Size = new System.Drawing.Size(55, 22);
            this.txtb_CommandDelay.TabIndex = 17;
            this.txtb_CommandDelay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_CommandDelay.TextChanged += new System.EventHandler(this.txtb_CommandDelay_TextChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(24, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(98, 16);
            this.label6.TabIndex = 16;
            this.label6.Text = "Port Number:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(34, 34);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(88, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "IP Address:";
            // 
            // txtb_PortNum
            // 
            this.txtb_PortNum.Location = new System.Drawing.Point(128, 59);
            this.txtb_PortNum.Name = "txtb_PortNum";
            this.txtb_PortNum.Size = new System.Drawing.Size(55, 22);
            this.txtb_PortNum.TabIndex = 14;
            this.txtb_PortNum.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtb_PortNum.TextChanged += new System.EventHandler(this.txtb_PortNum_TextChanged);
            // 
            // UserCtrl_TCP_SettingPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "UserCtrl_TCP_SettingPage";
            this.Size = new System.Drawing.Size(328, 196);
            this.Load += new System.EventHandler(this.UserCtrl_TCP_SettingPage_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox txtb_IPAddress4;
        private System.Windows.Forms.TextBox txtb_IPAddress1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtb_IPAddress3;
        private System.Windows.Forms.TextBox txtb_IPAddress2;
        private System.Windows.Forms.TextBox txtb_CommandDelay;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtb_PortNum;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtb_ReceiveTimeout;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_SendTimeout;
    }
}
