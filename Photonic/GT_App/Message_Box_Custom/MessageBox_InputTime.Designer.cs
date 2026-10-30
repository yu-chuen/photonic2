namespace GT_App
{
    partial class MessageBox_InputTime
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
            this.lbl_Title = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.numericbox_Hour = new System.Windows.Forms.NumericUpDown();
            this.numericbox_minute = new System.Windows.Forms.NumericUpDown();
            this.label3 = new System.Windows.Forms.Label();
            this.btn_OK = new System.Windows.Forms.Button();
            this.btn_Cancel = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numericbox_Hour)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericbox_minute)).BeginInit();
            this.SuspendLayout();
            // 
            // lbl_Title
            // 
            this.lbl_Title.AutoSize = true;
            this.lbl_Title.Location = new System.Drawing.Point(32, 30);
            this.lbl_Title.Name = "lbl_Title";
            this.lbl_Title.Size = new System.Drawing.Size(57, 20);
            this.lbl_Title.TabIndex = 0;
            this.lbl_Title.Text = "label1";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 71);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 20);
            this.label2.TabIndex = 2;
            this.label2.Text = "Hour";
            // 
            // numericbox_Hour
            // 
            this.numericbox_Hour.Location = new System.Drawing.Point(86, 69);
            this.numericbox_Hour.Maximum = new decimal(new int[] {
            24,
            0,
            0,
            0});
            this.numericbox_Hour.Name = "numericbox_Hour";
            this.numericbox_Hour.Size = new System.Drawing.Size(63, 26);
            this.numericbox_Hour.TabIndex = 3;
            this.numericbox_Hour.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // numericbox_minute
            // 
            this.numericbox_minute.Location = new System.Drawing.Point(225, 71);
            this.numericbox_minute.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericbox_minute.Name = "numericbox_minute";
            this.numericbox_minute.Size = new System.Drawing.Size(63, 26);
            this.numericbox_minute.TabIndex = 5;
            this.numericbox_minute.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(156, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 20);
            this.label3.TabIndex = 4;
            this.label3.Text = "Minute";
            // 
            // btn_OK
            // 
            this.btn_OK.Location = new System.Drawing.Point(262, 136);
            this.btn_OK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_OK.Name = "btn_OK";
            this.btn_OK.Size = new System.Drawing.Size(197, 64);
            this.btn_OK.TabIndex = 7;
            this.btn_OK.Text = "OK";
            this.btn_OK.UseVisualStyleBackColor = true;
            this.btn_OK.Click += new System.EventHandler(this.btn_OK_Click);
            // 
            // btn_Cancel
            // 
            this.btn_Cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_Cancel.Location = new System.Drawing.Point(59, 136);
            this.btn_Cancel.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btn_Cancel.Name = "btn_Cancel";
            this.btn_Cancel.Size = new System.Drawing.Size(197, 64);
            this.btn_Cancel.TabIndex = 6;
            this.btn_Cancel.Text = "Cancel";
            this.btn_Cancel.UseVisualStyleBackColor = true;
            this.btn_Cancel.Click += new System.EventHandler(this.btn_Cancel_Click);
            // 
            // MessageBox_InputTime
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(10F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CancelButton = this.btn_Cancel;
            this.ClientSize = new System.Drawing.Size(471, 213);
            this.ControlBox = false;
            this.Controls.Add(this.btn_OK);
            this.Controls.Add(this.btn_Cancel);
            this.Controls.Add(this.numericbox_minute);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.numericbox_Hour);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lbl_Title);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(5);
            this.Name = "MessageBox_InputTime";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MessageBox Input Time";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.MessageBox_InputTime_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericbox_Hour)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericbox_minute)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_Title;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.NumericUpDown numericbox_Hour;
        private System.Windows.Forms.NumericUpDown numericbox_minute;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btn_OK;
        private System.Windows.Forms.Button btn_Cancel;
    }
}