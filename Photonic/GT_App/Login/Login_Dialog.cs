using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class Login_Dialog : Form
    {
        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Login_Dialog()
        {
            InitializeComponent();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

            String strResponse = "";

            //Checker
            if (txtb_Username.Text == "") { Class_Helper.Display_ErrorMessageBox("Username box is empty. Please insert a username."); return; }
            if (txtb_Password.Text == "") { Class_Helper.Display_ErrorMessageBox("Password box is empty. Please insert a password."); return; }

            //if (pApp.m_SeqMachineCtrl.m_Password.IsValid(txtb_Username.Text, txtb_Password.Text) == true)
            //if ( pApp.m_SeqMachineCtrl.m_Server.Cmd_GetUserAccess(txtb_Username.Text, txtb_Password.Text)  == true)
            int iRet = pApp.m_SeqMachineCtrl.m_Server.Cmd_GetUserAccess(txtb_Username.Text, txtb_Password.Text, ref strResponse);

            //Central PC comm error
            if (iRet == -1)
            {
                //display notice 
                Class_Helper.Display_NoticeMessageBox("Central PC communication error. Revert to local user access.");

                //comm error: revert to local user access (default)
                if (pApp.m_SeqMachineCtrl.m_Password.IsValid(txtb_Username.Text, txtb_Password.Text) == false)
                {
                    Class_Helper.Display_ErrorMessageBox("Username or Password is incorrect. \r\nPlease key in the correct username and password.");
                    //this.DialogResult = System.Windows.Forms.DialogResult.No;
                }
                else
                    this.DialogResult = System.Windows.Forms.DialogResult.OK;
            }
            //user access is denied via central pc
            else if (iRet == 0)
            {
                Class_Helper.Display_ErrorMessageBox("Username or Password is incorrect. \r\nPlease key in the correct username and password.");
                //this.DialogResult = System.Windows.Forms.DialogResult.No;
            }
            //user name access valid via Central PC
            else
            {
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.Close();
            }
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
            this.Close();
        }

        private void Login_Dialog_Load(object sender, EventArgs e)
        {
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

            //txtb_Username.Text = "admin"; //temp
            //txtb_Password.Text = "1234"; //temp

            //txtb_Username.Text = pApp.m_SeqMachineCtrl.m_Password.Get_CurrentUser();
        }
    }
}
