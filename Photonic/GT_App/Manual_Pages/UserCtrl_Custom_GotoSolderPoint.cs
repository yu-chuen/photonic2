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
	public partial class UserCtrl_Custom_GotoSolderPoint : UserControl
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;

		private readonly object m_lock_Access = new object();				//blocker to signal end of process
		bool m_bFlag_Access = false;
		bool Flag_Access_b { set { lock ( m_lock_Access ) { m_bFlag_Access = value; } } get { lock ( m_lock_Access ) { return m_bFlag_Access; } } }

		int m_iType = 0;

		Thread m_thProcessThread = null;


		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////
		public UserCtrl_Custom_GotoSolderPoint(int iType)
		{
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

			m_iType = iType;
		}

		//Goto Position////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_GotoSolderPoint_Click( object sender, EventArgs e )
		{
			if ( Flag_Access_b == false )
			{
				Flag_Access_b = true;

				m_thProcessThread = new Thread(ProcessThread);
				m_thProcessThread.Start();
			}
			else
				Class_Helper.Display_NoticeMessageBox("Soldering process is in progress. Please standby.");
		}

		//Movement thread/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void ProcessThread()
        {
            m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running.....", true);

            //trigger sub sequence
            if (m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Goto_SolderPoint((int)num_SolderPoint.Value, m_iType) == false)
            {
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                Class_Helper.Display_ErrorMessageBox(m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Get_ErrorMessage());
            }
            else
            {
                //update message box
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Warning("Process Done.");
            }

            Flag_Access_b = false;
        }

		//Solder all positions/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_SolderAllPoints_Click( object sender, EventArgs e )
		{
			if ( Flag_Access_b == false )
			{
				Flag_Access_b = true;

				m_thProcessThread = new Thread(SolderAllPointsThread);
				m_thProcessThread.Start();
			}
			else
				Class_Helper.Display_NoticeMessageBox("Soldering process is in progress. Please standby.");
		}
		//Movement thread//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void SolderAllPointsThread()
		{
			m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running.....", true);

			//trigger sub sequence
			if ( m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.SolderAllPoints(m_iType) == false )
			{
				m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
				Class_Helper.Display_ErrorMessageBox(m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Get_ErrorMessage());
			}
			else
			{
				//update message box
				m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
				m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Warning("Process Done.");
			}

			Flag_Access_b = false;
		}

        private void btn_CleanSolderIron_Click(object sender, EventArgs e)
        {
            if (Flag_Access_b == false)
            {
                Flag_Access_b = true;

                m_thProcessThread = new Thread(CleanSolderIronThread);
                m_thProcessThread.Start();
            }
            else
                Class_Helper.Display_NoticeMessageBox("Cleaning Soldering iron is in progress. Please standby.");
        }
        //Movement thread//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void CleanSolderIronThread()
        {
            m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running.....", true);

            //trigger sub sequence
            if (m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.CleanSolderIron(m_iType) == false)
            {
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                Class_Helper.Display_ErrorMessageBox(m_pSeqMachineCtrl.m_ViewModel_GotoFunctions.Get_ErrorMessage());
            }
            else
            {
                //update message box
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Warning("Process Done.");
            }

            Flag_Access_b = false;
        }
	}
}