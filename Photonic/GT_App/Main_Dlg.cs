using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Reflection;
using System.Threading;

namespace GT_App
{
	public partial class Main_Dlg : Form
	{
		//variables
		SeqMachineControl m_pSeqMachineControl = null;
		Data_UnitTempStorage m_pDataUnit = null;
		Class_Helper_DisplayMsgBoxes m_ClassHelperMsgBox = null;

		public UserCtrl_Banner									m_UserCtrl_Banner;
		public UserCtrl_Report_ProductInfo						m_UserCtrl_ProductInfo;
		public UserCtrl_Report_PerformanceDialog				m_UserCtrl_ReportPerformaceDlg;
		public UserCtrl_MachineStatus							m_UserCtrl_MachineStatus;
		public UserCtrl_ProductionControlDialog					m_UserCtrl_ProductionControlDlg;
		public UserCtrl_InstructionDialog						m_UserCtrl_InstructrionDlg;

		//Constructor///////////////////////////////////////////////////////////////////////////////////////////////
		public Main_Dlg()
		{
			InitializeComponent();

			//Pointer to library core class and data temp storage
			m_pSeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_pDataUnit = m_pSeqMachineControl.m_DataUnit;

			m_ClassHelperMsgBox                 = new Class_Helper_DisplayMsgBoxes();

			m_UserCtrl_Banner					= new UserCtrl_Banner();
			m_UserCtrl_ProductInfo				= new UserCtrl_Report_ProductInfo();
			m_UserCtrl_ReportPerformaceDlg      = new UserCtrl_Report_PerformanceDialog();
			m_UserCtrl_MachineStatus			= new UserCtrl_MachineStatus();
			m_UserCtrl_ProductionControlDlg 	= new UserCtrl_ProductionControlDialog();
			m_UserCtrl_InstructrionDlg			= new UserCtrl_InstructionDialog();

		}

		//Dialog loaded///////////////////////////////////////////////////////////////////////////////////////////
		private void Main_Dlg_Load(object sender, EventArgs e)
		{
			Point pPoint = new Point(0, 0);
			int iBannerHeigth = 0;

            //Set App Version display
            //Note: To display the correct build date, require to comment out [assembly: AssemblyVersion("1.0.0.0")] and uncomment [assembly: AssemblyVersion("1.0.*")] @ AssembyInfo.cs 
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var builddate = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + TimeSpan.TicksPerSecond * 2 * version.Revision));
            //this.Text = m_pSeqMachineControl.m_MachineInfo.m_strMachineName + " - Version " + m_pSeqMachineControl.m_MachineInfo.Get_FullVersionName(/*builddate.ToString("dd-MMM-yyyy")*/);
            this.Text = m_pSeqMachineControl.m_MachineInfo.m_strMachineName + " - " + m_pSeqMachineControl.m_VersionInfo.GetVersion();

			//add main ui usercontrol
			//Banner
			this.Controls.Add(m_UserCtrl_Banner);
			iBannerHeigth = m_UserCtrl_Banner.Size.Height;

			//production control user control
			pPoint.X = 0;
			pPoint.Y = iBannerHeigth;
			m_UserCtrl_ProductionControlDlg.Location = pPoint;
			this.Controls.Add(m_UserCtrl_ProductionControlDlg);

			//Instruction User control
			pPoint.X = 0;
			pPoint.Y = iBannerHeigth + m_UserCtrl_ProductionControlDlg.Size.Height;
			m_UserCtrl_InstructrionDlg.Location = pPoint;
			this.Controls.Add(m_UserCtrl_InstructrionDlg);

			//Product Info user control
			pPoint.X = m_UserCtrl_InstructrionDlg.Size.Width;
			pPoint.Y = iBannerHeigth;
			m_UserCtrl_ProductInfo.Location = pPoint;
			this.Controls.Add(m_UserCtrl_ProductInfo);

            //Report Performance
            pPoint.X = m_UserCtrl_ProductInfo.Location.X + m_UserCtrl_ProductInfo.Size.Width;
            pPoint.Y = m_UserCtrl_ProductInfo.Location.Y;
            m_UserCtrl_ReportPerformaceDlg.Location = pPoint;
            this.Controls.Add(m_UserCtrl_ReportPerformaceDlg);

            //Machine status
            pPoint.X = m_UserCtrl_ProductInfo.Location.X;
            pPoint.Y = m_UserCtrl_ProductInfo.Location.Y + m_UserCtrl_ProductInfo.Size.Height;
            m_UserCtrl_MachineStatus.Location = pPoint;
            this.Controls.Add(m_UserCtrl_MachineStatus);

        }

		//Form closing in progress/////////////////////////////////////////////////////////////////////
		private void Main_Dlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			m_pSeqMachineControl.Clean_Up();
			m_UserCtrl_Banner.Dispose();
			m_UserCtrl_ProductInfo.Dispose();
			m_UserCtrl_ReportPerformaceDlg.Dispose();
			m_UserCtrl_MachineStatus.Dispose();
			m_UserCtrl_ProductionControlDlg.Dispose();
			m_UserCtrl_InstructrionDlg.Dispose();

			m_Timer.Stop();
		}

		//Timer Event////////////////////////////////////////////////////////////////////////////////////////
		//Note: Object set at main ui dlg
		private void On_Timer(object sender, EventArgs e)
		{
			m_Timer.Stop();

			//Display message Box
			m_ClassHelperMsgBox.Display_ErrorMsgBox();
			m_ClassHelperMsgBox.Display_WarningMsgBox();
			m_ClassHelperMsgBox.Display_RequestMsgBox();
			m_ClassHelperMsgBox.Display_RequestMsgBox3Input();
			m_ClassHelperMsgBox.Display_ModelessMsgBox();

            //Machine status
            Point pPoint = new Point(0, 0);
            pPoint.X = m_UserCtrl_ProductInfo.Location.X;
            pPoint.Y = m_UserCtrl_ProductInfo.Location.Y + m_UserCtrl_ProductInfo.Size.Height;
            m_UserCtrl_MachineStatus.Location = pPoint;

			m_Timer.Start();
		}

		//Update User Control UI//////////////////////////////////////////////////////////////////////////
		public void Update_UserCtrlUI()
		{
			m_UserCtrl_MachineStatus.Update_UserCtrl();
			m_UserCtrl_ProductInfo.Update_UI();
		}

		//Dummy Button///////////////////////////////////////////////////////////////////////////////////////////
		private void btn_Dummy001_Click(object sender, EventArgs e)
		{
            //bool bRet = true;
            //int iExtraLength = 3;												//additional data which is stored in the RFID tag
            //String strData = "";

            //if (m_pSeqMachineControl.m_Module_OP90_Vision.Trigger_CameraDual(Vision_Command.INSPECT_PART_FRONT, Vision_Command.INSPECT_PART_REAR) == false)
            //{
            //    Class_Helper.Display_ErrorMessageBox(m_pSeqMachineControl.Get_ErrorMessage( m_pSeqMachineControl.m_Module_OP90_Vision.Get_ErrorID(),
            //                                                                                m_pSeqMachineControl.m_Module_OP90_Vision.Get_ErrorMsgAdd(),
            //                                                                                m_pSeqMachineControl.m_Module_OP90_Vision.Get_ErrorMsg_Front()));
            //}


			//Read from RFID Tag
            //if ( m_pSeqMachineControl.m_Module_SolderingConv.RFIDScanner_Read(1) == false )
            //{
            //    Class_Helper.Display_ErrorMessageBox(m_pSeqMachineControl.Get_ErrorMessage(m_pSeqMachineControl.m_Module_SolderingConv.Get_ErrorID(),
            //                                                                                                                                                                m_pSeqMachineControl.m_Module_SolderingConv.Get_ErrorMsgAdd(),
            //                                                                                                                                                                ""));
            //}
			
			//Class_Helper.Display_NoticeMessageBox( Vision_Command.LOAD_RECIPE);

			//String str = "";
			//str = Enum.GetNames(typeof(ErrorTable.en_ErrorCode))[( int ) ErrorTable.en_ErrorCode.AIR_PRESSURE_LL_ERR];
			//MessageBox.Show(str);
			
			//m_pSeqMachineControl.m_MessageBoxTrigger.Trigger_MessageBox_Error(m_pSeqMachineControl.Get_ErrorMessage((int)ErrorTable.en_ErrorCode.AIR_PRESSURE_ERR));
			
			//m_pSeqMachineControl.m_MessageBoxTrigger.Trigger_MessageBox_Error("Test Test");

            //test solder iron head use count reset button
            //m_pSeqMachineControl.m_DataPackageFile.m_DataCommon_Stat3.m_iCurrentIronHead_UseCount++;
            //m_pSeqMachineControl.m_DataPackageFile.m_DataCommon_Stat2.m_iCurrentIronHead_UseCount++;
            //m_pSeqMachineControl.m_DataPackageFile.m_DataCommon_Stat1.m_iCurrentIronHead_UseCount++;
            //m_pSeqMachineControl.Pkg_To_Registry();


            #region test station down time

            //m_pSeqMachineControl.m_Module_OP110_HotBlowerConv.Set_Error((int)ErrorTable.en_ErrorCode.SENSOR_TRIGGERED_ERR, "", m_pSeqMachineControl.m_Module_OP110_HotBlowerConv.m_senFinProtectorPresent.Get_Label());

            //m_pSeqMachineControl.m_Module_OP110_HotBlowerConv.Set_Station_DownTime(true);

            //m_pSeqMachineControl.m_Module_OP110_HotBlowerConv.Set_Station_DownTime(false);
            //m_pSeqMachineControl.m_Data_PerStation_DownTime_OP110.Save_Settings();

            #endregion
		}
	}
}