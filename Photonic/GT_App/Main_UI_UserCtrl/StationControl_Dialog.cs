using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
    public partial class StationControl_Dialog : Form
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl;
        Data_UnitTempStorage m_DataUnit;

        List<UserCtrl_StationControlDlg> m_ll_userctrl_StatCtrlDlg = new List<UserCtrl_StationControlDlg>();

        const string m_strOP60 = "Solder OP60";
        const string m_strConv_OP60 = "Conv OP60";
        const string m_strOP70 = "Solder OP70";
        const string m_strConv_OP70 = "Conv OP70";
        const string m_strOP80 = "Solder OP80";
        const string m_strConv_OP80 = "Conv OP80";
        const string m_strOP85 = "Vision OP85";
        const string m_strConv_OP85 = "Conv OP85";
        const string m_strOP90 = "Vision OP90";
        const string m_strConv_OP90 = "Conv OP90";
        const string m_strOP100_Buffer = "OP100 Buffer";
        const string m_strOP100_Station = "OP100 Station";
        const string m_strOP110 = "Hot Blower OP110";
        const string m_strConv_OP110 = "Conv OP110";

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////
        public StationControl_Dialog()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////
        private void StationControl_Dialog_Load(object sender, EventArgs e)
        {
            int iCount = 0;
            Point pPoint = new Point();

            //add user control
            while (true)
            {
                //add user control
                if (Add_UserCtrl(iCount) == false) break;

                //set position
                m_ll_userctrl_StatCtrlDlg[iCount].Location = pPoint;

                //add to main dlg
                this.Controls.Add(m_ll_userctrl_StatCtrlDlg[iCount]);

                //increment count
                iCount++;

                //calculate position
                //user control arrangement based on vertically
                //pPoint.X = m_ll_userctrl_StatCtrlDlg[0].Size.Width * (iCount / 3);
                //pPoint.Y += m_ll_userctrl_StatCtrlDlg[0].Size.Height;
                //if ((iCount % 3) == 0) pPoint.Y = 0;

                //user control arrangement based on horizontally
                pPoint.X += m_ll_userctrl_StatCtrlDlg[0].Size.Width;
                pPoint.Y = m_ll_userctrl_StatCtrlDlg[0].Size.Height * (iCount / 4);
                if ((iCount % 4) == 0) pPoint.X = 0;
            }

            //size adjust
            //user control size - Width = 284, Height = 220
            //multiply the width and height based on how many X and how many Y accordingly otherwise the dialog will not centered correctly
            //pls change the value in UserCtrl_InstructionDialog.cs Station Control button
            //this.Size = new Size(284 * 5, 220 * 3);

            //start back ground worker
            bkgWorker_UpdateUI.RunWorkerAsync();
        }

        //Class Helper//////////////////////////////////////////////////////////////////////////////////////
        //Add User Control Station control dialog////////////////////////////////////////
        public bool Add_UserCtrl(int iCount)
        {
            String strLabel = "";

            switch (iCount)
            {
                case 0: { strLabel = m_strOP60; } break;
                case 1: { strLabel = m_strConv_OP60; } break;
                case 2: { strLabel = m_strOP70; } break;
                case 3: { strLabel = m_strConv_OP70; } break;
                case 4: { strLabel = m_strOP80; } break;
                case 5: { strLabel = m_strConv_OP80; } break;
                case 6: { strLabel = m_strOP85; } break;
                case 7: { strLabel = m_strConv_OP85; } break;
                case 8: { strLabel = m_strOP90; } break;
                case 9: { strLabel = m_strConv_OP90; } break;
                case 10: { strLabel = m_strOP100_Buffer; } break;
                case 11: { strLabel = m_strOP100_Station; } break;
                case 12: { strLabel = m_strOP110; } break;
                case 13: { strLabel = m_strConv_OP110; } break;

                default: { return false; } break;
            }

            //add new user ctrl
            m_ll_userctrl_StatCtrlDlg.Add(new UserCtrl_StationControlDlg(strLabel));
            m_ll_userctrl_StatCtrlDlg[iCount].PropertyChanged += new PropertyChangedEventHandler(StationControl_Dialog_PropertyChanged);

            return true;
        }

        //Property change/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        void StationControl_Dialog_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UserCtrl_StationControlDlg mUserCtrl = (UserCtrl_StationControlDlg)sender;

            switch (mUserCtrl.StationSelectCondition_str.ToLower())
            {
                case "run": StartRun(mUserCtrl.m_strLabel); break;
                case "stop": StopRun(mUserCtrl.m_strLabel); break;
                case "init": StartInit(mUserCtrl.m_strLabel); break;
                case "trigger": m_pSeqMachineCtrl.SingleStationTrigger(mUserCtrl.m_strLabel); break;
            }

            mUserCtrl.StationSelectCondition_str = "";
        }

        //Update User Ctrl UI//////////////////////////////////////////////////////////////////////////
        public void Update_UI()
        {
            int iStatus = -1; //iStatus: -1 => Unknown, 0 => Stop, 1 => Running, 2 => Initializing, 3 => Idle

            //bool bStatus = false;

            foreach (UserCtrl_StationControlDlg mUserCtrl in m_ll_userctrl_StatCtrlDlg)
            {
                //list selection
                switch (mUserCtrl.m_strLabel)
                {

                    case m_strOP100_Buffer:
                        {
                            if (m_pSeqMachineCtrl.m_Module_OP100_Buffer.Flag_Init == true) { iStatus = 2; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Buffer.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP100_Buffer.Module_Running == true) { iStatus = 1; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Buffer.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP100_Buffer.Module_Running == false) { iStatus = 0; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Buffer.Flag_Init == false && m_pSeqMachineCtrl.m_Module_OP100_Buffer.Flag_InitOK == false && m_pSeqMachineCtrl.m_Module_OP100_Buffer.Module_Running == false) { iStatus = 3; }
                            else { iStatus = -1; }
                            //bStatus = m_pSeqMachineCtrl.m_Module_OP100_Buffer.Module_Running;
                        }
                        break;

                    case m_strOP100_Station:
                        {
                            if (m_pSeqMachineCtrl.m_Module_OP100_Station.Flag_Init == true) { iStatus = 2; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Station.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP100_Station.Module_Running == true) { iStatus = 1; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Station.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP100_Station.Module_Running == false) { iStatus = 0; }
                            else if (m_pSeqMachineCtrl.m_Module_OP100_Station.Flag_Init == false && m_pSeqMachineCtrl.m_Module_OP100_Station.Flag_InitOK == false && m_pSeqMachineCtrl.m_Module_OP100_Station.Module_Running == false) { iStatus = 3; }
                            else { iStatus = -1; }
                            //bStatus = m_pSeqMachineCtrl.m_Module_OP100_Station.Module_Running;
                        }
                        break;

                    case m_strOP110:
                        {
                            if (m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_Init == true) { iStatus = 2; }
                            else if (m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Module_Running == true) { iStatus = 1; }
                            else if (m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_InitOK == true && m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Module_Running == false) { iStatus = 0; }
                            else if (m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_Init == false && m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Flag_InitOK == false && m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Module_Running == false) { iStatus = 3; }
                            else { iStatus = -1; }
                            //bStatus = m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Module_Running;
                        }
                        break;

                }

                //update user control
                //mUserCtrl.Update_UI(bStatus);
                mUserCtrl.Update_UI(iStatus);
            }
        }

        //Station Condition: Start Run////////////////////////////////////////////////////////////
        private void StartRun(String strlabel)
        {
            switch (strlabel)
            {
                case m_strOP100_Buffer: { m_pSeqMachineCtrl.m_Module_OP100_Buffer.Start_Run(); } break;
                case m_strOP100_Station: { m_pSeqMachineCtrl.m_Module_OP100_Station.Start_Run(); } break;

            }
        }

        //Station Condition: Stop Run////////////////////////////////////////////////////////////
        private void StopRun(String strlabel)
        {
            switch (strlabel)
            {
                case m_strOP100_Buffer: { m_pSeqMachineCtrl.m_Module_OP100_Buffer.Module_StopRun = true; } break;
                case m_strOP100_Station: { m_pSeqMachineCtrl.m_Module_OP100_Station.Module_StopRun = true; } break;

            }
        }

        //Station Condition: Init///////////////////////////////////////////////////////////////////////
        private void StartInit(String strlabel)
        {
            switch (strlabel)
            {
                case m_strOP100_Buffer: { m_pSeqMachineCtrl.m_Module_OP100_Buffer.Start_Init(); } break;
                case m_strOP100_Station: { m_pSeqMachineCtrl.m_Module_OP100_Station.Start_Init(); } break;

            }
        }


        //background Worker//////////////////////////////////////////////////////////////////////////
        private void bkgWorker_UpdateUI_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (worker.CancellationPending == false)
            {
                this.Invoke((MethodInvoker)delegate { this.Update_UI(); });
                Thread.Sleep(100);
            }
        }

        //Dialog closing//////////////////////////////////////////////////////////////////////////////////////////
        private void StationControl_Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            bkgWorker_UpdateUI.CancelAsync();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}