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
	public partial class UserCtrl_Custom_HotBlower: UserControl
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;
        ViewModel_HotBlower m_pViewModel_HotBlower;

		private readonly object m_lock_Access = new object();				//blocker to signal end of process
		bool m_bFlag_Access = false;
		bool Flag_Access_b { set { lock ( m_lock_Access ) { m_bFlag_Access = value; } } get { lock ( m_lock_Access ) { return m_bFlag_Access; } } }

		//int m_iType = 0;

		Thread m_thProcessThread = null;


		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Custom_HotBlower()
		{
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pViewModel_HotBlower = m_pSeqMachineCtrl.m_ViewModel_HotBlower;

			//m_iType = iType;
		}

		//Goto Position////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_OP110Test_Click( object sender, EventArgs e )
		{
            Start_Thread_OP110Test();
		}

        public void Start_Thread_OP110Test()
        {

            if (Flag_Access_b == false)
            {
                Flag_Access_b = true;

                m_thProcessThread = new Thread(ProcessThread);
                m_thProcessThread.Start();
            }
            else
                Class_Helper.Display_NoticeMessageBox("Wire Tester is in progress. Please standby.");
        }

		//Movement thread/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void ProcessThread()
		{
            m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running...", true);

            //set flag
            Flag_Access_b = false;
		}



	}
}
