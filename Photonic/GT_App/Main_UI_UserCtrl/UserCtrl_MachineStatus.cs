using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using StaticCoreLib;
using Microsoft.Win32;

namespace GT_App
{
    public partial class UserCtrl_MachineStatus : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        private readonly object m_lock = new object();

        List<UserCtrl_UI_FlagBox> m_list_UserCtrFlagBox;				//display device flags

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_MachineStatus()
        {
            InitializeComponent();
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            m_list_UserCtrFlagBox = new List<UserCtrl_UI_FlagBox>();
        }

        //loading UI - auto generate///////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_MachineStatus_Load(object sender, EventArgs e)
        {
            //update date and time
            lbl_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbl_Time.Text = DateTime.Now.ToString("HH:mm:ss");

            int iCount = 1;
            Point pPoint = new Point(0, 0);

            //Set flag user control initial position
            pPoint.X = lbl_MachineState.Location.X + lbl_MachineState.Width + 5;
            pPoint.Y = lbl_MachineState.Location.Y;

            //Add user control
            while (Add_List_UserCtrl_FlagBox(iCount) == true) iCount++;

            //Add controls
            iCount = 0;
            foreach (UserCtrl_UI_FlagBox mFlag in m_list_UserCtrFlagBox)
            {
                mFlag.Location = pPoint;
                this.Controls.Add(mFlag);

                pPoint.Y += mFlag.Height;

                //Set to the other side
                if (pPoint.X == 0 && iCount == 3)
                {
                    pPoint.X = mFlag.Width;
                    pPoint.Y = groupBox1.Height + 5;
                }

                //increment counter (for setting of other usercontrol to the other side)
                iCount++;
            }

            //Update User Ctrl
            this.Update_UserCtrl();

            Bitmap mBt; Image img;
            using (img = GT_App.Properties.Resources.Genetec_MAIN_logo_1)
            {
                mBt = new Bitmap(img);
                pictureBox1.Image = mBt;
                pictureBox1.Update();
                pictureBox1.InitialImage = null;
            }

            bkg_Worker_Image.WorkerSupportsCancellation = true;
            bkg_Worker_Image.RunWorkerAsync();

            //add system event so that software can respond to system date time changes
            SystemEvents.TimeChanged += SystemEvents_TimeChanged;
        }

        //make sure date time is correctly displayed
        void SystemEvents_TimeChanged(object sender, EventArgs e)
        {
            TimeZoneInfo.ClearCachedData();
        }

        //Update UI/////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_UI()
        {
            String strMacStatus = "";

            //update date and time
            lbl_Date.Text = DateTime.Now.ToString("dd-MMM-yyyy");
            lbl_Time.Text = DateTime.Now.ToString("HH:mm:ss");
            
            //Update Mac status
            strMacStatus = m_SeqMachineControl.m_DataUnit.MachineState_Str;
            if (strMacStatus.Contains("Error"))
            {
                lbl_MachineState.ForeColor = Color.Black;
                lbl_MachineState.BackColor = Color.Red;
            }
            else if (strMacStatus.Contains("Running"))
            {
                lbl_MachineState.ForeColor = Color.Green;
                lbl_MachineState.BackColor = Color.White;
            }
            else
            {
                lbl_MachineState.ForeColor = Color.DeepSkyBlue;
                lbl_MachineState.BackColor = Color.MistyRose;
            }
            lbl_MachineState.Text = strMacStatus;

            //Update Data
            lbl_ErrorMsg.Text = m_SeqMachineControl.m_DataUnit.DispErrorMessage_str;
            lbl_InstructionMsg.Text = m_SeqMachineControl.m_DataUnit.RequestStateMessage_str;

            Update_UserCtrl();
        }


        //Close all Threads//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            //stop all background trhead
            bkg_Worker_Image.CancelAsync();
            bkg_Worker_Image.Dispose();
        }

        //Update User Control////////////////////////////////////////////////////////////////////////////
        //Note: Please sync with Add_List_UserCtrl_FlagBox(...)
        public void Update_UserCtrl()
        {
            int iCount = -1;

            iCount++; m_list_UserCtrFlagBox[iCount].Update_UI(m_SeqMachineControl.m_DataUnit.Flag_DryRun);
        }

        //Add UserControl Flag/////////////////////////////////////////////////////////////////////////
        //Add box here to be displayed. Require to sync with Update_UserCtrl()
        private bool Add_List_UserCtrl_FlagBox(int iCount)
        {
            //Set usercontrol box and add to list
            switch (iCount)
            {
                case 1: m_list_UserCtrFlagBox.Add(new UserCtrl_UI_FlagBox("Dry Run")); break;

                default: return false;
            }

            return true;
        }

        private void bkg_Worker_Image_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (worker.CancellationPending == false)
            {
                this.Invoke((MethodInvoker)delegate { this.Update_UI(); });
                Thread.Sleep(100);
            }
        }

    }
}