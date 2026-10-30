using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
	public partial class InitializeStations_Dialog : Form
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;

		int m_iInitNum = 0;			//initialization ID: to identify which button / module is current being initialize

		private readonly object m_lock_Flag_InitRunning = new object();
		bool m_bFlag_InitRunning = false;
		bool Flag_InitRunning_b { set { lock ( m_lock_Flag_InitRunning ) { m_bFlag_InitRunning = value; } } get { lock ( m_lock_Flag_InitRunning ) { return m_bFlag_InitRunning; } } }

		//Constructor////////////////////////////////////////////////////////////////////////////////////////////////
		public InitializeStations_Dialog()
		{
			InitializeComponent();

			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
		}

		//Dialog loaded////////////////////////////////////////////////////////////////////////////////////////////////
		private void InitializeStations_Dialog_Load( object sender, EventArgs e )
		{

		}

		//Class helper: Init Initalization/////////////////////////////////////////////////////////////////////////
		private void Start_Init( int iInitNum )
		{
			if ( Flag_InitRunning_b == true )
			{
				Class_Helper.Display_ErrorMessageBox("Initialization is running. Please wait.");
				return;
			}

			m_iInitNum = iInitNum;
			backGround_Worker.RunWorkerAsync();
		}

		//Form Closing. Block if the init process is still running///////////////////////////////////////////////////////////////////////////////
		private void InitializeStations_Dialog_FormClosing( object sender, FormClosingEventArgs e )
		{
			if ( Flag_InitRunning_b == true ) e.Cancel = true;
		}

		//Button Events////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void btn_MainInitialize_Click( object sender, EventArgs e )				{ Start_Init(1); }
		private void btn_InitSolderStation1_Click( object sender, EventArgs e )		{ Start_Init(5); }
		private void btn_InitSolderStation2_Click( object sender, EventArgs e )		{ Start_Init(6); }
		private void btn_InitSolderStation3_Click( object sender, EventArgs e )		{ Start_Init(7); }
		private void btn_InitVisionInspection_Click( object sender, EventArgs e )		{ Start_Init(8); }
		private void btn_InitManualStatOP100_Click( object sender, EventArgs e ) { Start_Init(3); }
		private void bnt_IntiManualStatOP120_Click( object sender, EventArgs e ) { Start_Init(4); }
		private void btn_Init_StatHotBlower_Click( object sender, EventArgs e )		{ Start_Init(2); }
		private void btn_InitConvVision_Click( object sender, EventArgs e )				{ Start_Init(9);}
		private void btn_InitConvSolderStation_Click( object sender, EventArgs e ) { Start_Init(10); }




		//Back Ground Worker/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//For Init
		private void Init_Thread( object sender, DoWorkEventArgs e )
		{
			String strError = "";

			//set Running flag to block multiple trigger
			Flag_InitRunning_b = true;

			//Check if the related modules are running
			switch ( m_iInitNum )
			{
				//main init: error message is already catered in the main init
				case 1: if ( m_pSeqMachineCtrl.Flag_Run == true) strError = "Main sequence is running. Not allowed to Initialize..."; break;

				//Single module init
				case 2: if ( m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Module_Running == true ) strError = "Module Hot Blower is running. Not allowed to Initialize..."; break;
				//case 3: if ( m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Module_Running == true ) strError = "Module Manual Station OP100 is running. Not allowed to Initialize..."; break;
				//case 4: if ( m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Module_Running == true ) strError = "Module manual Station OP120 is running. Not allowed to Initialize..."; break;
			}

			if ( strError != "" )
			{
				Class_Helper.Display_ErrorMessageBox(strError);
				return;
			}

			//init modules
			switch ( m_iInitNum )
			{
				//main init: error message is already catered in the main init
				case 1: m_pSeqMachineCtrl.Init(); break;

				//Single module init
				case 2: m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Init_Module(); break;
				//case 3: m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Init_Module(); break;
				//case 4: m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Init_Module(); break;
			}

			//Check for error: error message is not catered for the single module
			switch ( m_iInitNum )
			{
				//Single module init
				case 2: 
					if ( m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_InitOK != true )
					{
						strError = m_pSeqMachineCtrl.Get_ErrorMessage(	m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Get_ErrorID(),
																													m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Get_ErrorMsgAdd(),
																													m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Get_ErrorMsg_Front());
					}
					break;

				//case 3:
				//    if ( m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Flag_InitOK != true )
				//    {
				//        strError = m_pSeqMachineCtrl.Get_ErrorMessage(	m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Get_ErrorID(),
				//                                                                                                    m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Get_ErrorMsgAdd(),
				//                                                                                                    m_pSeqMachineCtrl.m_Module_ManualStat_OP100.Get_ErrorMsg_Front());
				//    }
				//    break;

				//case 4:
				//    if ( m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Flag_InitOK != true )
				//    {
				//        strError = m_pSeqMachineCtrl.Get_ErrorMessage(	m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Get_ErrorID(),
				//                                                                                                    m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Get_ErrorMsgAdd(),
				//                                                                                                    m_pSeqMachineCtrl.m_Module_ManualStat_OP120.Get_ErrorMsg_Front());
				//    }
				//    break;

			}

			//Display error messge
			if ( strError != "" ) Class_Helper.Display_ErrorMessageBox(strError);

			Flag_InitRunning_b = false;
		}





	}
}
