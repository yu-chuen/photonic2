using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class Login_AddUser_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        Password m_Password = null;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Login_AddUser_Dialog()
        {
            InitializeComponent();

            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_Password = m_SeqMachineControl.m_Password;
        }

        //Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Login_AddUser_Dialog_Load(object sender, EventArgs e)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            String strLevel = m_Password.Get_UserLevel(m_Password.Get_CurrentUser());

            //Check level
            if (strLevel.ToLower() == "user" || strLevel.ToLower() == "supervisor")
            {
                dlg.Set_Messages("Error. User level is not high enough to register this personal", "");
                dlg.Set_BitMapLogo(1);
                dlg.Set_Buttons(1);
                dlg.ShowDialog();
                dlg.Dispose();

                this.Close();
            }

            //add one more level
            int iCount = 0;
            while (true)
            {
                //Get user level name
                String str = m_Password.Get_Level(iCount);

                //Exit if end of list
                if (str.ToLower().Contains("error") == true) break;

                //Update Combo box
                comboBox_Position.Items.Add(str);

                //increment counter
                iCount++;
            }
        }

        //Button events///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Register_Click(object sender, EventArgs e)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            bool bRes = true;
            String strLevel = m_Password.Get_UserLevel(m_Password.Get_CurrentUser()).ToLower();
            String strComboLevel = comboBox_Position.Text.ToLower();

            //Check level
            switch (strLevel)
            {
                case "admin": break;
                case "engineer": if (strComboLevel == "admin") bRes = false; break;
                case "Supervisor": if (strComboLevel == "admin" || strComboLevel == "engineer") bRes = false; break;
            }

            if (bRes == false)
            {
                dlg.Set_Messages("Error. User level is not high enough to register this personal", "");
                dlg.Set_BitMapLogo(1);
                dlg.Set_Buttons(1);
                dlg.ShowDialog();
                dlg.Dispose();

                return;
            }

            //Check text box
            dlg.Set_BitMapLogo(1);
            dlg.Set_Buttons(1);

            if (txtb_Username.Text == "") { dlg.Set_Messages("Error. Please key in username", ""); bRes = false; }
            else if (txtb_Password.Text == "") { dlg.Set_Messages("Error. Please key in password", ""); bRes = false; }
            else if (txtb_ConfirmPassword.Text == "") { dlg.Set_Messages("Error. Please confirm the password", ""); bRes = false; }
            else if (comboBox_Position.Text == "") { dlg.Set_Messages("Error. Please select level", ""); bRes = false; }

            if (bRes == false)
            {
                dlg.ShowDialog();
                dlg.Dispose();
                return;
            }

            //Check password with confirm password
            if (txtb_Password.Text != txtb_ConfirmPassword.Text) {
                dlg.Set_Messages("Error. Password does not match with the confirm password text.", "");
                dlg.ShowDialog();
                dlg.Dispose();
                return;
            }
            
            //Registeration complete
            m_Password.Add_User(txtb_Username.Text, txtb_Password.Text, comboBox_Position.Text);

            dlg.Set_BitMapLogo(2);
            dlg.Set_Messages("Register User complete.", ""); 
            dlg.ShowDialog();
            dlg.Dispose();
            return;
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_DeleteUser_Click(object sender, EventArgs e)
        {
            MessageBox_CustomDlg dlg_Error = new MessageBox_CustomDlg();
            int iPos = 0;
            bool bRes = true;
            String strLevel = m_Password.Get_UserLevel(m_Password.Get_CurrentUser()).ToLower();
            String strComboLevel = comboBox_Position.Text.ToLower();

            //Check level
            switch (strLevel)
            {
                case "admin": break;
                case "engineer": if (strComboLevel == "admin") bRes = false; break;
                case "Supervisor": if (strComboLevel == "admin" || strComboLevel == "engineer") bRes = false; break;
            }

            if (bRes == false)
            {
                dlg_Error.Set_Messages("Error. User level is not high enough to register this personal", "");
                dlg_Error.Set_BitMapLogo(1);
                dlg_Error.Set_Buttons(1);
                dlg_Error.ShowDialog();
                dlg_Error.Dispose();

                return;
            }

            //Check if list is empty or 
            if (m_Password.User_Exist(txtb_UserDelete.Text, ref iPos) == false)
            {
                MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
                dlg.Set_Messages("User does not exist: " + txtb_UserDelete.Text, "");
                dlg.ShowDialog();
                dlg.Dispose();
            }

            //delete user
            m_Password.Delete_User(txtb_UserDelete.Text);
        }
    }
}
