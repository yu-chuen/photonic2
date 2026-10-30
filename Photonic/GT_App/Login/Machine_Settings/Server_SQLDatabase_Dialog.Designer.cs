namespace GT_App
{
	partial class Server_SQLDatabase_Dialog
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
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
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label10 = new System.Windows.Forms.Label();
			this.txtb_SerialNumber = new System.Windows.Forms.TextBox();
			this.txtb_StationID = new System.Windows.Forms.TextBox();
			this.label9 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.txtb_inputString = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txtb_response = new System.Windows.Forms.TextBox();
			this.btn_UpdateDatabase = new System.Windows.Forms.Button();
			this.btn_ReadDatabase = new System.Windows.Forms.Button();
			this.btn_Disconnect = new System.Windows.Forms.Button();
			this.btn_DatabaseAdd = new System.Windows.Forms.Button();
			this.btn_IsExist = new System.Windows.Forms.Button();
			this.btn_Connect = new System.Windows.Forms.Button();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label8 = new System.Windows.Forms.Label();
			this.txtb_TableName = new System.Windows.Forms.TextBox();
			this.label7 = new System.Windows.Forms.Label();
			this.txtb_InitCatalog = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txtb_DBFilename = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txtb_DataSource = new System.Windows.Forms.TextBox();
			this.label4 = new System.Windows.Forms.Label();
			this.txtb_Password = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txtb_Username = new System.Windows.Forms.TextBox();
			this.btn_ESC = new System.Windows.Forms.Button();
			this.btn_Update = new System.Windows.Forms.Button();
			this.btn_GetProductStatus = new System.Windows.Forms.Button();
			this.btn_GetGoPermission = new System.Windows.Forms.Button();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btn_GetGoPermission);
			this.groupBox1.Controls.Add(this.btn_GetProductStatus);
			this.groupBox1.Controls.Add(this.label10);
			this.groupBox1.Controls.Add(this.txtb_SerialNumber);
			this.groupBox1.Controls.Add(this.txtb_StationID);
			this.groupBox1.Controls.Add(this.label9);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txtb_inputString);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txtb_response);
			this.groupBox1.Controls.Add(this.btn_UpdateDatabase);
			this.groupBox1.Controls.Add(this.btn_ReadDatabase);
			this.groupBox1.Controls.Add(this.btn_Disconnect);
			this.groupBox1.Controls.Add(this.btn_DatabaseAdd);
			this.groupBox1.Controls.Add(this.btn_IsExist);
			this.groupBox1.Controls.Add(this.btn_Connect);
			this.groupBox1.Location = new System.Drawing.Point(5, 6);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(608, 306);
			this.groupBox1.TabIndex = 0;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Operations";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Location = new System.Drawing.Point(17, 82);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(153, 16);
			this.label10.TabIndex = 13;
			this.label10.Text = "Input String or Debug";
			// 
			// txtb_SerialNumber
			// 
			this.txtb_SerialNumber.Location = new System.Drawing.Point(16, 161);
			this.txtb_SerialNumber.Multiline = true;
			this.txtb_SerialNumber.Name = "txtb_SerialNumber";
			this.txtb_SerialNumber.Size = new System.Drawing.Size(419, 26);
			this.txtb_SerialNumber.TabIndex = 12;
			// 
			// txtb_StationID
			// 
			this.txtb_StationID.Location = new System.Drawing.Point(442, 161);
			this.txtb_StationID.Multiline = true;
			this.txtb_StationID.Name = "txtb_StationID";
			this.txtb_StationID.Size = new System.Drawing.Size(75, 26);
			this.txtb_StationID.TabIndex = 11;
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Location = new System.Drawing.Point(443, 143);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(75, 16);
			this.label9.TabIndex = 10;
			this.label9.Text = "Station ID";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(17, 143);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(107, 16);
			this.label2.TabIndex = 9;
			this.label2.Text = "Serial Number";
			// 
			// txtb_inputString
			// 
			this.txtb_inputString.Location = new System.Drawing.Point(16, 101);
			this.txtb_inputString.Multiline = true;
			this.txtb_inputString.Name = "txtb_inputString";
			this.txtb_inputString.Size = new System.Drawing.Size(577, 33);
			this.txtb_inputString.TabIndex = 8;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(17, 21);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Response";
			// 
			// txtb_response
			// 
			this.txtb_response.Location = new System.Drawing.Point(16, 42);
			this.txtb_response.Multiline = true;
			this.txtb_response.Name = "txtb_response";
			this.txtb_response.Size = new System.Drawing.Size(575, 33);
			this.txtb_response.TabIndex = 6;
			// 
			// btn_UpdateDatabase
			// 
			this.btn_UpdateDatabase.Location = new System.Drawing.Point(306, 257);
			this.btn_UpdateDatabase.Name = "btn_UpdateDatabase";
			this.btn_UpdateDatabase.Size = new System.Drawing.Size(139, 41);
			this.btn_UpdateDatabase.TabIndex = 5;
			this.btn_UpdateDatabase.Text = "Update Database";
			this.btn_UpdateDatabase.UseVisualStyleBackColor = true;
			this.btn_UpdateDatabase.Click += new System.EventHandler(this.btn_UpdateDatabase_Click);
			// 
			// btn_ReadDatabase
			// 
			this.btn_ReadDatabase.Location = new System.Drawing.Point(159, 257);
			this.btn_ReadDatabase.Name = "btn_ReadDatabase";
			this.btn_ReadDatabase.Size = new System.Drawing.Size(139, 41);
			this.btn_ReadDatabase.TabIndex = 4;
			this.btn_ReadDatabase.Text = "Read From Database";
			this.btn_ReadDatabase.UseVisualStyleBackColor = true;
			this.btn_ReadDatabase.Click += new System.EventHandler(this.btn_ReadDatabase_Click);
			// 
			// btn_Disconnect
			// 
			this.btn_Disconnect.Location = new System.Drawing.Point(12, 257);
			this.btn_Disconnect.Name = "btn_Disconnect";
			this.btn_Disconnect.Size = new System.Drawing.Size(139, 41);
			this.btn_Disconnect.TabIndex = 3;
			this.btn_Disconnect.Text = "Disconnect";
			this.btn_Disconnect.UseVisualStyleBackColor = true;
			this.btn_Disconnect.Click += new System.EventHandler(this.btn_Disconnect_Click);
			// 
			// btn_DatabaseAdd
			// 
			this.btn_DatabaseAdd.Location = new System.Drawing.Point(306, 209);
			this.btn_DatabaseAdd.Name = "btn_DatabaseAdd";
			this.btn_DatabaseAdd.Size = new System.Drawing.Size(139, 41);
			this.btn_DatabaseAdd.TabIndex = 2;
			this.btn_DatabaseAdd.Text = "Database Add";
			this.btn_DatabaseAdd.UseVisualStyleBackColor = true;
			this.btn_DatabaseAdd.Click += new System.EventHandler(this.btn_DatabaseAdd_Click);
			// 
			// btn_IsExist
			// 
			this.btn_IsExist.Location = new System.Drawing.Point(159, 209);
			this.btn_IsExist.Name = "btn_IsExist";
			this.btn_IsExist.Size = new System.Drawing.Size(139, 41);
			this.btn_IsExist.TabIndex = 1;
			this.btn_IsExist.Text = "Is Record Exist";
			this.btn_IsExist.UseVisualStyleBackColor = true;
			this.btn_IsExist.Click += new System.EventHandler(this.btn_IsExist_Click);
			// 
			// btn_Connect
			// 
			this.btn_Connect.Location = new System.Drawing.Point(12, 209);
			this.btn_Connect.Name = "btn_Connect";
			this.btn_Connect.Size = new System.Drawing.Size(139, 41);
			this.btn_Connect.TabIndex = 0;
			this.btn_Connect.Text = "Connect";
			this.btn_Connect.UseVisualStyleBackColor = true;
			this.btn_Connect.Click += new System.EventHandler(this.btn_Connect_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.txtb_TableName);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.txtb_InitCatalog);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Controls.Add(this.txtb_DBFilename);
			this.groupBox2.Controls.Add(this.label5);
			this.groupBox2.Controls.Add(this.txtb_DataSource);
			this.groupBox2.Controls.Add(this.label4);
			this.groupBox2.Controls.Add(this.txtb_Password);
			this.groupBox2.Controls.Add(this.label3);
			this.groupBox2.Controls.Add(this.txtb_Username);
			this.groupBox2.Location = new System.Drawing.Point(7, 316);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(608, 223);
			this.groupBox2.TabIndex = 1;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Settings";
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Location = new System.Drawing.Point(19, 185);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(98, 16);
			this.label8.TabIndex = 21;
			this.label8.Text = "Table Name:";
			// 
			// txtb_TableName
			// 
			this.txtb_TableName.Location = new System.Drawing.Point(127, 182);
			this.txtb_TableName.Multiline = true;
			this.txtb_TableName.Name = "txtb_TableName";
			this.txtb_TableName.Size = new System.Drawing.Size(226, 23);
			this.txtb_TableName.TabIndex = 20;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(19, 154);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(107, 16);
			this.label7.TabIndex = 19;
			this.label7.Text = "Initial Catalog:";
			// 
			// txtb_InitCatalog
			// 
			this.txtb_InitCatalog.Location = new System.Drawing.Point(127, 151);
			this.txtb_InitCatalog.Multiline = true;
			this.txtb_InitCatalog.Name = "txtb_InitCatalog";
			this.txtb_InitCatalog.Size = new System.Drawing.Size(470, 23);
			this.txtb_InitCatalog.TabIndex = 18;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(19, 123);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(101, 16);
			this.label6.TabIndex = 17;
			this.label6.Text = "DB Filename:";
			// 
			// txtb_DBFilename
			// 
			this.txtb_DBFilename.Location = new System.Drawing.Point(127, 120);
			this.txtb_DBFilename.Multiline = true;
			this.txtb_DBFilename.Name = "txtb_DBFilename";
			this.txtb_DBFilename.Size = new System.Drawing.Size(469, 23);
			this.txtb_DBFilename.TabIndex = 16;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(19, 92);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(98, 16);
			this.label5.TabIndex = 15;
			this.label5.Text = "Data Source:";
			// 
			// txtb_DataSource
			// 
			this.txtb_DataSource.Location = new System.Drawing.Point(127, 89);
			this.txtb_DataSource.Multiline = true;
			this.txtb_DataSource.Name = "txtb_DataSource";
			this.txtb_DataSource.Size = new System.Drawing.Size(468, 23);
			this.txtb_DataSource.TabIndex = 14;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(19, 61);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(80, 16);
			this.label4.TabIndex = 13;
			this.label4.Text = "Password:";
			// 
			// txtb_Password
			// 
			this.txtb_Password.Location = new System.Drawing.Point(127, 58);
			this.txtb_Password.Multiline = true;
			this.txtb_Password.Name = "txtb_Password";
			this.txtb_Password.Size = new System.Drawing.Size(226, 23);
			this.txtb_Password.TabIndex = 12;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(19, 30);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(86, 16);
			this.label3.TabIndex = 11;
			this.label3.Text = "UserName:";
			// 
			// txtb_Username
			// 
			this.txtb_Username.Location = new System.Drawing.Point(127, 27);
			this.txtb_Username.Multiline = true;
			this.txtb_Username.Name = "txtb_Username";
			this.txtb_Username.Size = new System.Drawing.Size(226, 23);
			this.txtb_Username.TabIndex = 10;
			// 
			// btn_ESC
			// 
			this.btn_ESC.Location = new System.Drawing.Point(408, 550);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(139, 41);
			this.btn_ESC.TabIndex = 6;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = true;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// btn_Update
			// 
			this.btn_Update.Location = new System.Drawing.Point(265, 550);
			this.btn_Update.Name = "btn_Update";
			this.btn_Update.Size = new System.Drawing.Size(139, 41);
			this.btn_Update.TabIndex = 7;
			this.btn_Update.Text = "Update";
			this.btn_Update.UseVisualStyleBackColor = true;
			this.btn_Update.Click += new System.EventHandler(this.btn_Update_Click);
			// 
			// btn_GetProductStatus
			// 
			this.btn_GetProductStatus.Location = new System.Drawing.Point(453, 209);
			this.btn_GetProductStatus.Name = "btn_GetProductStatus";
			this.btn_GetProductStatus.Size = new System.Drawing.Size(139, 41);
			this.btn_GetProductStatus.TabIndex = 14;
			this.btn_GetProductStatus.Text = "Get Product Status";
			this.btn_GetProductStatus.UseVisualStyleBackColor = true;
			this.btn_GetProductStatus.Click += new System.EventHandler(this.btn_GetProductStatus_Click);
			// 
			// btn_GetGoPermission
			// 
			this.btn_GetGoPermission.Location = new System.Drawing.Point(453, 257);
			this.btn_GetGoPermission.Name = "btn_GetGoPermission";
			this.btn_GetGoPermission.Size = new System.Drawing.Size(139, 41);
			this.btn_GetGoPermission.TabIndex = 15;
			this.btn_GetGoPermission.Text = "Get Go Permission";
			this.btn_GetGoPermission.UseVisualStyleBackColor = true;
			this.btn_GetGoPermission.Click += new System.EventHandler(this.btn_GetGoPermission_Click);
			// 
			// Server_SQLDatabase_Dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(624, 598);
			this.Controls.Add(this.btn_Update);
			this.Controls.Add(this.btn_ESC);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "Server_SQLDatabase_Dialog";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Server ";
			this.Load += new System.EventHandler(this.Server_SQLDatabase_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Button btn_Connect;
		private System.Windows.Forms.Button btn_UpdateDatabase;
		private System.Windows.Forms.Button btn_ReadDatabase;
		private System.Windows.Forms.Button btn_Disconnect;
		private System.Windows.Forms.Button btn_DatabaseAdd;
		private System.Windows.Forms.Button btn_IsExist;
		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Button btn_Update;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txtb_inputString;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txtb_response;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox txtb_Password;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txtb_Username;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txtb_DBFilename;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txtb_DataSource;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox txtb_InitCatalog;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox txtb_TableName;
		private System.Windows.Forms.TextBox txtb_StationID;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.TextBox txtb_SerialNumber;
		private System.Windows.Forms.Button btn_GetGoPermission;
		private System.Windows.Forms.Button btn_GetProductStatus;
	}
}