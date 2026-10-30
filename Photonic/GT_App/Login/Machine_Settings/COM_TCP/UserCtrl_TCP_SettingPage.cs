using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Text.RegularExpressions;

/*****************************************************************************************************************************
 * Note1: Calling this user control requires to set the comm settings pointer
 * Note2: Closing the parent application will require to call the "Update_Settings()" function
 *****************************************************************************************************************************/

namespace GT_App
{
    public partial class UserCtrl_TCP_SettingPage : UserControl
    {
        //Variables
        Comm_TCP_Settings m_Settings;

        //Constructor//////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_TCP_SettingPage()
        {
            InitializeComponent();
        }

        //Set Settings ptr///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Settings(Comm_TCP_Settings mSettings) { m_Settings = mSettings; }
        public Comm_TCP_Settings Get_Settings() { return m_Settings; }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_TCP_SettingPage_Load(object sender, EventArgs e)
        {
            Set_IPAddress(m_Settings.m_strIPAddress);
            txtb_PortNum.Text = m_Settings.m_iPortNum.ToString();
            txtb_CommandDelay.Text = m_Settings.m_lgReadTimeout.ToString();
            txtb_ReceiveTimeout.Text = m_Settings.m_iReceiveTimeout.ToString();
            txtb_SendTimeout.Text = m_Settings.m_iSendTimeout.ToString();
        }

        //Test Change at text boxes//////////////////////////////////////////////////////////////////////////
        private void txtb_IPAddress1_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress1); }
        private void txtb_IPAddress2_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress2); }
        private void txtb_IPAddress3_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress3); }
        private void txtb_IPAddress4_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress4); }

        private void txtb_PortNum_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_PortNum.Text) == false)
            {
                txtb_PortNum.Text = Regex.Replace(txtb_PortNum.Text, "[^0-9]", "");
                if (txtb_PortNum.Text == "")
                    txtb_PortNum.Text = m_Settings.m_iPortNum.ToString();
            }
        }

        private void txtb_CommandDelay_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_CommandDelay.Text) == false)
            {
                txtb_CommandDelay.Text = Regex.Replace(txtb_CommandDelay.Text, "[^0-9]", "");
                if (txtb_CommandDelay.Text == "") 
                    txtb_CommandDelay.Text = m_Settings.m_lgReadTimeout.ToString();
            }
        }

        private void txtb_ReceiveTimeout_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_ReceiveTimeout.Text) == false)
            {
                txtb_ReceiveTimeout.Text = Regex.Replace(txtb_ReceiveTimeout.Text, "[^0-9]", "");
                if (txtb_ReceiveTimeout.Text == "")
                    txtb_ReceiveTimeout.Text = m_Settings.m_iReceiveTimeout.ToString();
            }
        }

        private void txtb_SendTimeout_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_SendTimeout.Text) == false)
            {
                txtb_SendTimeout.Text = Regex.Replace(txtb_SendTimeout.Text, "[^0-9]", "");
                if (txtb_SendTimeout.Text == "")
                    txtb_SendTimeout.Text = m_Settings.m_iSendTimeout.ToString();
            }
        }

        //Set IP Address////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_IPAddress(String strIPAddress)
        {
            String[] strIPArr = strIPAddress.Split('.');

            //check ip address string
            if (strIPArr.Length != 4)
            {
                MessageBox.Show("IP Address string is not enough.");
                return;
            }

            //Populate text box
            txtb_IPAddress1.Text = strIPArr[0];
            txtb_IPAddress2.Text = strIPArr[1];
            txtb_IPAddress3.Text = strIPArr[2];
            txtb_IPAddress4.Text = strIPArr[3];
        }

        //Update TCP Settings/////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Settings()
        {
            m_Settings.m_strIPAddress = txtb_IPAddress1.Text + "." + txtb_IPAddress2.Text + "." + txtb_IPAddress3.Text + "." + txtb_IPAddress4.Text;
            m_Settings.m_iPortNum = int.Parse(txtb_PortNum.Text);
            m_Settings.m_iReceiveTimeout = int.Parse(txtb_ReceiveTimeout.Text);
            m_Settings.m_iSendTimeout = int.Parse(txtb_SendTimeout.Text);
            m_Settings.m_lgReadTimeout = long.Parse(txtb_CommandDelay.Text);
        }

        //Set Title / Label//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Lable(String strLabel)
        {
            groupBox1.Text = strLabel;
        }
    }
}
