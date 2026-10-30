namespace GT_App
{
    partial class Login_AddUser_Dialog
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
            this.txtb_Username = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtb_Password = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtb_ConfirmPassword = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.comboBox_Position = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btn_Register = new System.Windows.Forms.Button();
            this.btn_ESC = new System.Windows.Forms.Button();
            this.btn_DeleteUser = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtb_UserDelete = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // txtb_Username
            // 
            this.txtb_Username.Location = new System.Drawing.Point(161, 41);
            this.txtb_Username.Name = "txtb_Username";
            this.txtb_Username.Size = new System.Drawing.Size(259, 22);
            this.txtb_Username.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "User Name:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(18, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 16);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password:";
            // 
            // txtb_Password
            // 
            this.txtb_Password.Location = new System.Drawing.Point(161, 69);
            this.txtb_Password.Name = "txtb_Password";
            this.txtb_Password.PasswordChar = '*';
            this.txtb_Password.Size = new System.Drawing.Size(259, 22);
            this.txtb_Password.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(18, 100);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 16);
            this.label3.TabIndex = 5;
            this.label3.Text = "Confirm Password:";
            // 
            // txtb_ConfirmPassword
            // 
            this.txtb_ConfirmPassword.Location = new System.Drawing.Point(161, 97);
            this.txtb_ConfirmPassword.Name = "txtb_ConfirmPassword";
            this.txtb_ConfirmPassword.PasswordChar = '*';
            this.txtb_ConfirmPassword.Size = new System.Drawing.Size(259, 22);
            this.txtb_ConfirmPassword.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(18, 128);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 16);
            this.label4.TabIndex = 7;
            this.label4.Text = "Position:";
            // 
            // comboBox_Position
            // 
            this.comboBox_Position.FormattingEnabled = true;
            this.comboBox_Position.Location = new System.Drawing.Point(149, 113);
            this.comboBox_Position.Name = "comboBox_Position";
            this.comboBox_Position.Size = new System.Drawing.Size(174, 24);
            this.comboBox_Position.TabIndex = 8;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btn_Register);
            this.groupBox1.Controls.Add(this.comboBox_Position);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(447, 215);
            this.groupBox1.TabIndex = 9;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Add User";
            // 
            // btn_Register
            // 
            this.btn_Register.Location = new System.Drawing.Point(283, 155);
            this.btn_Register.Name = "btn_Register";
            this.btn_Register.Size = new System.Drawing.Size(158, 52);
            this.btn_Register.TabIndex = 10;
            this.btn_Register.Text = "Register";
            this.btn_Register.UseVisualStyleBackColor = true;
            this.btn_Register.Click += new System.EventHandler(this.btn_Register_Click);
            // 
            // btn_ESC
            // 
            this.btn_ESC.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btn_ESC.Location = new System.Drawing.Point(295, 373);
            this.btn_ESC.Name = "btn_ESC";
            this.btn_ESC.Size = new System.Drawing.Size(158, 52);
            this.btn_ESC.TabIndex = 11;
            this.btn_ESC.Text = "ESC";
            this.btn_ESC.UseVisualStyleBackColor = true;
            this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
            // 
            // btn_DeleteUser
            // 
            this.btn_DeleteUser.Location = new System.Drawing.Point(276, 76);
            this.btn_DeleteUser.Name = "btn_DeleteUser";
            this.btn_DeleteUser.Size = new System.Drawing.Size(158, 52);
            this.btn_DeleteUser.TabIndex = 11;
            this.btn_DeleteUser.Text = "Delete User";
            this.btn_DeleteUser.UseVisualStyleBackColor = true;
            this.btn_DeleteUser.Click += new System.EventHandler(this.btn_DeleteUser_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.btn_DeleteUser);
            this.groupBox2.Controls.Add(this.txtb_UserDelete);
            this.groupBox2.Location = new System.Drawing.Point(19, 233);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(447, 134);
            this.groupBox2.TabIndex = 13;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Delete User";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 36);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 16);
            this.label5.TabIndex = 15;
            this.label5.Text = "User Name:";
            // 
            // txtb_UserDelete
            // 
            this.txtb_UserDelete.Location = new System.Drawing.Point(149, 33);
            this.txtb_UserDelete.Name = "txtb_UserDelete";
            this.txtb_UserDelete.Size = new System.Drawing.Size(259, 22);
            this.txtb_UserDelete.TabIndex = 14;
            // 
            // Login_AddUser_Dialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btn_ESC;
            this.ClientSize = new System.Drawing.Size(478, 431);
            this.Controls.Add(this.btn_ESC);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtb_ConfirmPassword);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtb_Password);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtb_Username);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Login_AddUser_Dialog";
            this.Text = "Add User";
            this.Load += new System.EventHandler(this.Login_AddUser_Dialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtb_Username;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtb_Password;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtb_ConfirmPassword;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox comboBox_Position;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_Register;
        private System.Windows.Forms.Button btn_ESC;
        private System.Windows.Forms.Button btn_DeleteUser;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtb_UserDelete;
    }
}