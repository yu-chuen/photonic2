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
using System.IO;

namespace GT_App
{
    public partial class UserCtrl_Report_PerformanceDialog : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        Data_UnitTempStorage m_DataStore = null;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Report_PerformanceDialog()
        {
            InitializeComponent();
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

            m_SeqMachineControl = pApp.m_SeqMachineCtrl;
            m_DataStore = m_SeqMachineControl.m_DataUnit;
        }

        //Clean Up////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            bkgrdWorker_PerfReport.CancelAsync();
            bkgrdWorker_PerfReport.Dispose();
        }

        //destructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~UserCtrl_Report_PerformanceDialog()
        {
            m_Timer.Stop();
            m_Timer.Dispose();
        }

        private void UserCtrl_Report_PerformanceDialog_Load(object sender, EventArgs e)
        {
            bkgrdWorker_PerfReport.WorkerSupportsCancellation = true;
            bkgrdWorker_PerfReport.RunWorkerAsync();
        }

        private void bkgrdWorker_PerfReport_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (worker.CancellationPending == false)
            {
                this.Invoke((MethodInvoker)delegate { Update_UI(); });
                Thread.Sleep(100);
            }
        }

        public void Update_UI()
        {
            //Update run time display---------------------------------------------------------------------------
            lbl_RunTime.Text = m_DataStore.Total_RunTime;
            lbl_DownTime.Text = m_DataStore.Total_StopTime;
            lbl_StartTime.Text = m_DataStore.ProcessRun_Start;
            //------------------------------------------------------------------------------------------------------------

            //process data
            lbl_TotalUnitCount.Text = m_DataStore.PartCount_int.ToString();
            lbl_TotalUnitFailed.Text = m_DataStore.PartCountFail_int.ToString();
            lbl_TotalUnitPassed.Text = m_DataStore.PartCountPass_int.ToString();

        }

        private void btn_ResetCounter_Click(object sender, EventArgs e)
        {
            //confirm user wishes to reset counter
            MessageBox_CustomDlg mUserInput = new MessageBox_CustomDlg();
            mUserInput.Set_BitMapLogo(0);
            mUserInput.Set_Buttons(3);
            mUserInput.Set_Messages("Confirm: Do you wish to Reset the counter?", "");
            if (mUserInput.ShowDialog() != DialogResult.OK)
            {
                mUserInput.Dispose();
                return;
            }

            //reset relevant counter
            m_SeqMachineControl.m_DataUnit.Reset_Data();
            m_SeqMachineControl.m_DataUnit.Reset_Counter();
            m_SeqMachineControl.m_WatchTimer.Reset_Time();
        }
    }
}
