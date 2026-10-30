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
    public partial class UserCtrl_Serial_KeyanceIL : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings= new UserCtrl_SerialPortSettings();
        Comm_Serial_KeyenceIL m_KeyanceIL;

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Serial_KeyanceIL()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            //m_KeyanceIL = m_SeqMachineCtrl.m_CommSerial_KeyenceIL;
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_Serial_Screwdriver_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(0, 0);

            //Add Serial Port Settings user ctrl
            m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
            m_userctrl_SerialPortSettings.Set_CommSettingsPtr(m_KeyanceIL.Get_Settings().m_Settings);
            m_userctrl_SerialPortSettings.Location = pPoint;
            this.Controls.Add(m_userctrl_SerialPortSettings);

        }

        ////Set Code Scanner pointer//////////////////////////////////////////////////////////////////
        public void Set_CodeScannerPtr(Comm_Serial_KeyenceIL mKeyanceIL) { m_KeyanceIL = mKeyanceIL; }

        //Buton Events/////////////////////////////////////////////////////////////////////////////////////////////////
        private void Error_Message(String strError)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            dlg.Set_Buttons(1);
            dlg.Set_Messages(strError, "");
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (!m_KeyanceIL.Connect())
            {
                Error_Message(m_KeyanceIL.Get_ErrorMessage());
                lbl_Response.Text = "Failed to Connect to reader";
            }
            else
                lbl_Response.Text = "Connected to Reader.";
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_KeyanceIL.Disconnect();
            lbl_Response.Text = "Disconnected from Reader.";
        }
        
        private void btn_ReadCode_Click(object sender, EventArgs e)
        {
            String strData = "";
            double dbData_1 = 0;
            double dbData_2 = 0;

            //Read Code
            if (m_KeyanceIL.Read_TorqueValue(ref strData) == false)
            {
                Error_Message(m_KeyanceIL.Get_ErrorMessage());
            }

            dbData_1 = double.Parse(m_KeyanceIL.m_CodeData.m_strHeightCheck1);
            dbData_2 = double.Parse(m_KeyanceIL.m_CodeData.m_strHeightCheck2); 

            //Return decode response
            lbl_Response.Text += "\r\nHeight Check 1: " + dbData_1 +
              "\r\nHeight Check 2: " + dbData_2;
        }


        private void btn_UpdateSettings_Click(object sender, EventArgs e)
        {
            //get serial com settings
            m_KeyanceIL.Get_Settings().m_Settings = m_userctrl_SerialPortSettings.Get_Settings();

            m_KeyanceIL.To_Registry();
        }





    }
}
