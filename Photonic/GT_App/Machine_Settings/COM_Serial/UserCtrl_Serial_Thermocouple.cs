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
    public partial class UserCtrl_Serial_Thermocouple : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings= new UserCtrl_SerialPortSettings();
        Comm_Serial_Thermocouple m_Thermocouple;

        

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Serial_Thermocouple()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_Thermocouple = m_SeqMachineCtrl.m_CommSerialThermocouple;
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_Serial_Screwdriver_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(0, 0);
        }

        ////Set Code Scanner pointer//////////////////////////////////////////////////////////////////
        public void Set_CodeScannerPtr(Comm_Serial_Thermocouple mThermocouple) { m_Thermocouple = mThermocouple; }

        //Buton Events/////////////////////////////////////////////////////////////////////////////////////////////////
        private void Error_Message(String strError)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            dlg.Set_Buttons(1);
            dlg.Set_Messages(strError, "");
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_Thermocouple.Disconnect();
            //lbl_Response.Text = "Disconnected from Reader.";
        }
        
        private void btn_ReadCode_Click(object sender, EventArgs e)
        {
            String strData = "";

            //Read Code
            if (m_Thermocouple.Read_Temperature(ref strData) == false)
            {
                Error_Message(m_Thermocouple.Get_ErrorMessage());
            }
        }

        private void btn_UpdateSettings_Click(object sender, EventArgs e)
        {
            m_Thermocouple.To_Registry();
        }
    }
}
