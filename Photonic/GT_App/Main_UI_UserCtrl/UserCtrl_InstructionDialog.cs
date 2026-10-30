using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class UserCtrl_InstructionDialog : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;

        bool m_bLoginOK = false;
        private readonly object m_lockLoginOK = new object();
        public bool Login_OK { set { lock (m_lockLoginOK) { m_bLoginOK = value; } } get { lock (m_lockLoginOK) { return m_bLoginOK; } } }
        
        //Constructor/////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_InstructionDialog()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Machine State////////////////////////////////////////////////////////////////////////////////////////////
        private bool MachineState()
        {
            bool bRet = true;

            if (m_SeqMachineCtrl.Flag_Run == true)
            {
                Display_ErrorMessage("Warning: Machine is running. Please stop the machine before continuing.");
                return false;
            }
            if (m_SeqMachineCtrl.Flag_RunningInit == true)
            {
                Display_ErrorMessage("Warning: Machine is initializing. Please wait until the machine completes its initialization cycle.");
                return false;
            }

            return bRet;
        }

        //Display Error Message/////////////////////////////////////////////////////////////////////////////
        private void Display_ErrorMessage(String strError)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            dlg.Set_BitMapLogo(1);
            dlg.Set_Buttons(1);
            dlg.Set_Messages(strError, "");
            dlg.ShowDialog();
            dlg.Dispose();
        }

        //button control//////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_About_Click(object sender, EventArgs e)
        {
            About_Dialog dlg = new About_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_ShutDown_Click(object sender, EventArgs e)
        {
            if (MachineState() == false) return;

            //User confirm input
            MessageBox_CustomDlg mUserInput = new MessageBox_CustomDlg();
            mUserInput.Set_BitMapLogo(0);
            mUserInput.Set_Buttons(3);
            mUserInput.Set_Title("Notice");
            mUserInput.Set_Messages("Do you want to shut down this software?", "");
            if (mUserInput.ShowDialog() != DialogResult.OK)
            {
                mUserInput.Dispose();
                return;
            }

            //close this app
            Main_Dlg dlg = GT_App.Program.m_MainDialog;
            dlg.Close();
        }

        private void btn_Maintenance_Click(object sender, EventArgs e)
        {
            if (MachineState() == false) return;

            Maintenance_Dialog dlg = new Maintenance_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_ManualMode_Click(object sender, EventArgs e)
        {
            if (MachineState() == false) return;

            Manual_OperationsMainDlg dlg = new Manual_OperationsMainDlg();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_Initialize_Click(object sender, EventArgs e)
        {
			if ( MachineState() == false ) return;

			m_SeqMachineCtrl.Start_Init();
        }

        private void btn_Rework_OP85_Click(object sender, EventArgs e)
        {
            Rework_Dlg dlg = new Rework_Dlg();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_Login_Click(object sender, EventArgs e)
        {
            if (MachineState() == false) return;

            Login_Dialog dlg = new Login_Dialog();
            if (Login_OK == false)
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    btn_Login.Text = "Logout";
                    Login_OK = true;

                    //register user
                    m_SeqMachineCtrl.SetLog("User Login: " + m_SeqMachineCtrl.m_Password.Get_CurrentUser());
                }
            }
            else
            {
                btn_Login.Text = "Login";
                Login_OK = false;
            }

            dlg.Dispose();
            Update_UI();
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI()
        {
            //Main_Dlg Main_Dlg = GT_App.Program.m_MainDialog;

            bool bEnable = false;

            //set var
            if (Login_OK == true) { bEnable = true; }

            //Update related buttons
            btn_Maintenance.Enabled = bEnable;
            btn_ManualMode.Enabled = bEnable;

            //if (Main_Dlg.Get_UserLevel().Contains("admin"))
            //{
            //    //btn_ManualMode.Enabled = false;
            //}
        }

        //Dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_InstructionDialog_Load(object sender, EventArgs e)
        {
            Update_UI();
        }

        private void btn_Setup_Click(object sender, EventArgs e)
        {
            Setup_Dialog dlg = new Setup_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

		private void btn_StationControl_Click( object sender, EventArgs e )
		{
			StationControl_Dialog dlg = new StationControl_Dialog();

            //size adjust
            //user control size - Width = 284, Height = 220
            //multiply the width and height based on how many X and how many Y accordingly otherwise the dialog will not centered correctly
            //85 because of the ESC button
            dlg.Size = new Size(284 * 4, (220 * 4) + 85);

            dlg.ShowDialog();
            dlg.Dispose();
		}

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Io dlg = new Io();
            dlg.ShowDialog();
            dlg.Dispose();
        }
    }
}
