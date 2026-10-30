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
	public partial class UserCtrl_Custom_OP90_VisionTest: UserControl
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;
        ViewModel_Vision m_pViewModel_Vision;

		private readonly object m_lock_Access = new object();				//blocker to signal end of process
		bool m_bFlag_Access = false;
		bool Flag_Access_b { set { lock ( m_lock_Access ) { m_bFlag_Access = value; } } get { lock ( m_lock_Access ) { return m_bFlag_Access; } } }

		//int m_iType = 0;

		Thread m_thProcessThread = null;


		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Custom_OP90_VisionTest()
		{
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pViewModel_Vision = m_pSeqMachineCtrl.m_ViewModel_Vision;

			//m_iType = iType;
		}

		//Goto Position////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_OP90Test_Click( object sender, EventArgs e )
		{
            Start_Thread_OP90Test();
		}

        public void Start_Thread_OP90Test()
        {

            if (Flag_Access_b == false)
            {
                Flag_Access_b = true;

                m_thProcessThread = new Thread(() => ProcessThread((int)num_Counts.Value));
                m_thProcessThread.Start();
            }
            else
                Class_Helper.Display_NoticeMessageBox("Wire Tester is in progress. Please standby.");
        }

		//Movement thread/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void ProcessThread(int iCount)
		{
            m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("Process running...", true);

            if (m_pViewModel_Vision.VisionTest(iCount) == false)
            {
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                Class_Helper.Display_ErrorMessageBox(m_pSeqMachineCtrl.m_ViewModel_Vision.Get_ErrorMessage());
            }
            else
            {
                //update message box
                m_pSeqMachineCtrl.m_MessageBoxTrigger.Trigger_MessageBox_Modeless("", false);
                Class_Helper.Display_NoticeMessageBox("Process Done.");
            }

            //set flag
            Flag_Access_b = false;
		}



	}
}
