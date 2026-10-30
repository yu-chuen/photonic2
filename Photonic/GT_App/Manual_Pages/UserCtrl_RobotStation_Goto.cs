using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
	public partial class UserCtrl_RobotStation_Goto : UserControl
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;

		int m_iTypeID = 0;
		String m_strPositionName = "";

		String m_strLabel = "Go to ";
		String m_strPosDesc = "";

		Thread m_thProcessThread = null;

		private readonly object m_lock_Access = new object();				//blocker to signal end of process
		bool m_bFlag_Access = false;
		bool Flag_Access_b { set { lock ( m_lock_Access ) { m_bFlag_Access = value; } } get { lock ( m_lock_Access ) { return m_bFlag_Access; } } }


		//Constructor.////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_RobotStation_Goto(String strPosName, String strPosDesc, int iTypeID)
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            m_strPositionName = strPosName;
            m_strPosDesc = strPosDesc;
            m_iTypeID = iTypeID;
        }

		//Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_RobotStation_Goto_Load( object sender, EventArgs e )
		{
			btn_GotoPos_RobotStation.Text = "[" + m_strPositionName + "]: " + m_strLabel + m_strPosDesc;
		}

		//Button Click Event////////////////////////////////////////////////////////////////////////////////////////
		private void btn_GotoPos_RobotStation_Click( object sender, EventArgs e )
		{
			if ( Flag_Access_b == false )
			{
				Flag_Access_b = true;

				m_thProcessThread = new Thread(TheadMovement);
				m_thProcessThread.Start();
			}
		}

		//Movement thread///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void TheadMovement()
		{
			//display moving message
			m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running.....", true);

			if ( m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Goto_RobotStation(m_strPositionName, m_iTypeID) == false )
			{
				m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
				Class_Helper.Display_ErrorMessageBox(m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Get_ErrorMessage());
			}
			else
				m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);

			Flag_Access_b = false;
		}





	}
}
