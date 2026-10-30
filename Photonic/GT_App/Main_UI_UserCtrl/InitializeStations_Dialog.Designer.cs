namespace GT_App
{
	partial class InitializeStations_Dialog
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
			this.btn_ESC = new System.Windows.Forms.Button();
			this.btn_MainInitialize = new System.Windows.Forms.Button();
			this.btn_InitSolderStation1 = new System.Windows.Forms.Button();
			this.btn_InitSolderStation2 = new System.Windows.Forms.Button();
			this.btn_InitSolderStation3 = new System.Windows.Forms.Button();
			this.btn_InitVisionInspection = new System.Windows.Forms.Button();
			this.btn_InitManualStatOP100 = new System.Windows.Forms.Button();
			this.bnt_IntiManualStatOP120 = new System.Windows.Forms.Button();
			this.backGround_Worker = new System.ComponentModel.BackgroundWorker();
			this.btn_Init_StatHotBlower = new System.Windows.Forms.Button();
			this.btn_InitConvVision = new System.Windows.Forms.Button();
			this.btn_InitConvSolderStation = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btn_ESC
			// 
			this.btn_ESC.Location = new System.Drawing.Point(51, 577);
			this.btn_ESC.Name = "btn_ESC";
			this.btn_ESC.Size = new System.Drawing.Size(145, 45);
			this.btn_ESC.TabIndex = 0;
			this.btn_ESC.Text = "ESC";
			this.btn_ESC.UseVisualStyleBackColor = true;
			this.btn_ESC.Click += new System.EventHandler(this.btn_ESC_Click);
			// 
			// btn_MainInitialize
			// 
			this.btn_MainInitialize.Location = new System.Drawing.Point(10, 16);
			this.btn_MainInitialize.Name = "btn_MainInitialize";
			this.btn_MainInitialize.Size = new System.Drawing.Size(234, 45);
			this.btn_MainInitialize.TabIndex = 1;
			this.btn_MainInitialize.Text = "Main Initialize";
			this.btn_MainInitialize.UseVisualStyleBackColor = true;
			this.btn_MainInitialize.Click += new System.EventHandler(this.btn_MainInitialize_Click);
			// 
			// btn_InitSolderStation1
			// 
			this.btn_InitSolderStation1.Location = new System.Drawing.Point(10, 87);
			this.btn_InitSolderStation1.Name = "btn_InitSolderStation1";
			this.btn_InitSolderStation1.Size = new System.Drawing.Size(234, 45);
			this.btn_InitSolderStation1.TabIndex = 2;
			this.btn_InitSolderStation1.Text = "Init: Solder Station 1";
			this.btn_InitSolderStation1.UseVisualStyleBackColor = true;
			this.btn_InitSolderStation1.Click += new System.EventHandler(this.btn_InitSolderStation1_Click);
			// 
			// btn_InitSolderStation2
			// 
			this.btn_InitSolderStation2.Location = new System.Drawing.Point(10, 139);
			this.btn_InitSolderStation2.Name = "btn_InitSolderStation2";
			this.btn_InitSolderStation2.Size = new System.Drawing.Size(234, 45);
			this.btn_InitSolderStation2.TabIndex = 3;
			this.btn_InitSolderStation2.Text = "Init: Solder Station 2";
			this.btn_InitSolderStation2.UseVisualStyleBackColor = true;
			this.btn_InitSolderStation2.Click += new System.EventHandler(this.btn_InitSolderStation2_Click);
			// 
			// btn_InitSolderStation3
			// 
			this.btn_InitSolderStation3.Location = new System.Drawing.Point(10, 191);
			this.btn_InitSolderStation3.Name = "btn_InitSolderStation3";
			this.btn_InitSolderStation3.Size = new System.Drawing.Size(234, 45);
			this.btn_InitSolderStation3.TabIndex = 4;
			this.btn_InitSolderStation3.Text = "Init: Solder Station 3";
			this.btn_InitSolderStation3.UseVisualStyleBackColor = true;
			this.btn_InitSolderStation3.Click += new System.EventHandler(this.btn_InitSolderStation3_Click);
			// 
			// btn_InitVisionInspection
			// 
			this.btn_InitVisionInspection.Location = new System.Drawing.Point(10, 243);
			this.btn_InitVisionInspection.Name = "btn_InitVisionInspection";
			this.btn_InitVisionInspection.Size = new System.Drawing.Size(234, 45);
			this.btn_InitVisionInspection.TabIndex = 5;
			this.btn_InitVisionInspection.Text = "Init: Vision Inspection Station";
			this.btn_InitVisionInspection.UseVisualStyleBackColor = true;
			this.btn_InitVisionInspection.Click += new System.EventHandler(this.btn_InitVisionInspection_Click);
			// 
			// btn_InitManualStatOP100
			// 
			this.btn_InitManualStatOP100.Location = new System.Drawing.Point(10, 295);
			this.btn_InitManualStatOP100.Name = "btn_InitManualStatOP100";
			this.btn_InitManualStatOP100.Size = new System.Drawing.Size(234, 45);
			this.btn_InitManualStatOP100.TabIndex = 6;
			this.btn_InitManualStatOP100.Text = "Init: Manual Station OP100";
			this.btn_InitManualStatOP100.UseVisualStyleBackColor = true;
			this.btn_InitManualStatOP100.Click += new System.EventHandler(this.btn_InitManualStatOP100_Click);
			// 
			// bnt_IntiManualStatOP120
			// 
			this.bnt_IntiManualStatOP120.Location = new System.Drawing.Point(10, 347);
			this.bnt_IntiManualStatOP120.Name = "bnt_IntiManualStatOP120";
			this.bnt_IntiManualStatOP120.Size = new System.Drawing.Size(234, 45);
			this.bnt_IntiManualStatOP120.TabIndex = 7;
			this.bnt_IntiManualStatOP120.Text = "Init: Manual Station OP 120";
			this.bnt_IntiManualStatOP120.UseVisualStyleBackColor = true;
			this.bnt_IntiManualStatOP120.Click += new System.EventHandler(this.bnt_IntiManualStatOP120_Click);
			// 
			// backGround_Worker
			// 
			this.backGround_Worker.WorkerSupportsCancellation = true;
			this.backGround_Worker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.Init_Thread);
			// 
			// btn_Init_StatHotBlower
			// 
			this.btn_Init_StatHotBlower.Location = new System.Drawing.Point(10, 400);
			this.btn_Init_StatHotBlower.Name = "btn_Init_StatHotBlower";
			this.btn_Init_StatHotBlower.Size = new System.Drawing.Size(234, 45);
			this.btn_Init_StatHotBlower.TabIndex = 8;
			this.btn_Init_StatHotBlower.Text = "Init: Hot Blower Station";
			this.btn_Init_StatHotBlower.UseVisualStyleBackColor = true;
			this.btn_Init_StatHotBlower.Click += new System.EventHandler(this.btn_Init_StatHotBlower_Click);
			// 
			// btn_InitConvVision
			// 
			this.btn_InitConvVision.Location = new System.Drawing.Point(10, 453);
			this.btn_InitConvVision.Name = "btn_InitConvVision";
			this.btn_InitConvVision.Size = new System.Drawing.Size(234, 45);
			this.btn_InitConvVision.TabIndex = 9;
			this.btn_InitConvVision.Text = "Init: Conveyor Vision";
			this.btn_InitConvVision.UseVisualStyleBackColor = true;
			this.btn_InitConvVision.Click += new System.EventHandler(this.btn_InitConvVision_Click);
			// 
			// btn_InitConvSolderStation
			// 
			this.btn_InitConvSolderStation.Location = new System.Drawing.Point(10, 506);
			this.btn_InitConvSolderStation.Name = "btn_InitConvSolderStation";
			this.btn_InitConvSolderStation.Size = new System.Drawing.Size(234, 45);
			this.btn_InitConvSolderStation.TabIndex = 10;
			this.btn_InitConvSolderStation.Text = "Init: Conveyor Solder Station";
			this.btn_InitConvSolderStation.UseVisualStyleBackColor = true;
			this.btn_InitConvSolderStation.Click += new System.EventHandler(this.btn_InitConvSolderStation_Click);
			// 
			// InitializeStations_Dialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 16F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(254, 636);
			this.Controls.Add(this.btn_InitConvSolderStation);
			this.Controls.Add(this.btn_InitConvVision);
			this.Controls.Add(this.btn_Init_StatHotBlower);
			this.Controls.Add(this.bnt_IntiManualStatOP120);
			this.Controls.Add(this.btn_InitManualStatOP100);
			this.Controls.Add(this.btn_InitVisionInspection);
			this.Controls.Add(this.btn_InitSolderStation3);
			this.Controls.Add(this.btn_InitSolderStation2);
			this.Controls.Add(this.btn_InitSolderStation1);
			this.Controls.Add(this.btn_MainInitialize);
			this.Controls.Add(this.btn_ESC);
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ( ( byte ) ( 0 ) ));
			this.Margin = new System.Windows.Forms.Padding(4);
			this.Name = "InitializeStations_Dialog";
			this.Text = "Statio Initialization";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.InitializeStations_Dialog_FormClosing);
			this.Load += new System.EventHandler(this.InitializeStations_Dialog_Load);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Button btn_ESC;
		private System.Windows.Forms.Button btn_MainInitialize;
		private System.Windows.Forms.Button btn_InitSolderStation1;
		private System.Windows.Forms.Button btn_InitSolderStation2;
		private System.Windows.Forms.Button btn_InitSolderStation3;
		private System.Windows.Forms.Button btn_InitVisionInspection;
		private System.Windows.Forms.Button btn_InitManualStatOP100;
		private System.Windows.Forms.Button bnt_IntiManualStatOP120;
		private System.ComponentModel.BackgroundWorker backGround_Worker;
		private System.Windows.Forms.Button btn_Init_StatHotBlower;
		private System.Windows.Forms.Button btn_InitConvVision;
		private System.Windows.Forms.Button btn_InitConvSolderStation;
	}
}