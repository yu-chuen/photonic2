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
    public partial class UserCtrl_ProductionControlDialog : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
                
        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_ProductionControlDialog()
        {
            InitializeComponent();
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

            m_SeqMachineControl = pApp.m_SeqMachineCtrl;
        }

        //Clean Up////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            bkgWorker_UpdateUI.CancelAsync();
        }

		//Dialog loaded/////////////////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_ProductionControlDialog_Load( object sender, EventArgs e )
		{
			//Start back ground worker for ui
			bkgWorker_UpdateUI.WorkerSupportsCancellation = true;
			bkgWorker_UpdateUI.RunWorkerAsync();
		}

		//Background worker update UI////////////////////////////////////////////////////////////////////////
		private void bkgWorker_UpdateUI_DoWork( object sender, DoWorkEventArgs e )
		{
			BackgroundWorker worker = ( BackgroundWorker ) sender;
			while ( worker.CancellationPending == false )
			{
				this.Invoke(( MethodInvoker ) delegate { Update_UI(); });
				Thread.Sleep(10);
			}
		}

		//Update UI///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Update_UI()
		{
			//Disable start button
			if ( m_SeqMachineControl.Flag_Run == true ||
				m_SeqMachineControl.Flag_RunningInit == true ||
				m_SeqMachineControl.Flag_InitOK == false )
			{
				if ( btn_Start.Enabled == true )
				{
					btn_Start.Enabled = false;
					btn_Start.BackColor = Color.FromKnownColor(KnownColor.Control);//Color.FromArgb(224, 224, 224);
				}
			}
			else
			{
				if ( btn_Start.Enabled == false )
				{
					btn_Start.Enabled = true;
					btn_Start.BackColor = Color.LawnGreen;
					btn_Start.ForeColor = Color.Black;
				}
			}
		}


		//Class helper: Update Button Color////////////////////////////////////////////////////////////////
		private void Update_Button( Button obj, bool bEnable, bool bActivate )
		{
			//remove "Start" and "Stop" words
			obj.Text = obj.Text.Replace("Start", "").Replace("Stop", "");

			//enable or disable button
			if ( bEnable != obj.Enabled ) obj.Enabled = bEnable;
			if ( bEnable == false )
			{
				obj.BackColor = Color.FromKnownColor(KnownColor.Control);
				return;
			}

			//change button color
			obj.BackColor = ( ( bActivate == true ) ? Color.Green : Color.Red );

			//Set button Text
			obj.Text += ( ( bActivate == true ) ? "Stop" : "Start" );
		}




        //button//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Start_Click(object sender, EventArgs e)
        {
            m_SeqMachineControl.Start_Run();
        }

        private void btn_Stop_Click(object sender, EventArgs e)
        {
            //stop machine
            m_SeqMachineControl.Stop_Run();
            m_SeqMachineControl.SetLog("Stop Button Pressed.");

            MessageBox.Show("Stop button pressed. Please wait until the current running process is completed.");

            //wait for seq thread to stop
            while (m_SeqMachineControl.Flag_Run == true)
            {
                Application.DoEvents();
                Thread.Sleep(100);
            }
        }

    }
}
