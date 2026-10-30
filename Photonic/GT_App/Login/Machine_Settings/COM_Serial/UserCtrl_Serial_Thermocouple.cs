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

            //Get Command Setting group box position
            //pPoint.Y = gb_CommandSettings.Height + 10;
            //pPoint.X = gb_CommandSettings.Location.X;
;

            //Add Serial Port Settings user ctrl
            //m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
            //m_userctrl_SerialPortSettings.Set_CommSettingsPtr(m_Thermocouple.Get_Settings().m_Settings);
            //m_userctrl_SerialPortSettings.Location = pPoint;
            //this.Controls.Add(m_userctrl_SerialPortSettings);

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

        private void btn_Connect_Click(object sender, EventArgs e)
        {
            //if (!m_Thermocouple.Connect())
            //{
            //    Error_Message(m_Thermocouple.Get_ErrorMessage());
            //    lbl_Response.Text = "Failed to Connect to reader";
            //}
            //else
            //    lbl_Response.Text = "Connected to Reader.";
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_Thermocouple.Comm_Disconnect();
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
            //else
                //lbl_Response.Text = strData;
        }

        private void btn_DecodeResponse_Click(object sender, EventArgs e)
        {
            //String strError = "";
            //if (m_Thermocouple.Decode_Response(lbl_Response.Text) == false)
            //{
            //    strError = m_Thermocouple.Get_ErrorMessage();
            //    Error_Message(strError);
            //    lbl_Response.Text = strError;
            //    return;
            //}
            //
            ////Return decode response
            // lbl_Response.Text += "\r\nCode: " + m_Thermocouple.m_CodeData.m_strBarcode +
            //    "\r\nMatching Value: " + m_Thermocouple.m_CodeData.MatchingValue.ToString() +
            //    "\r\nRead Rate: " + m_Thermocouple.m_CodeData.m_strReadRate_UEC;
        }

        private void btn_UpdateSettings_Click(object sender, EventArgs e)
        {
            //get serial com settings
            //m_Thermocouple.Get_Settings().m_Settings = m_userctrl_SerialPortSettings.Get_Settings();

            ////Update to registry
            //m_ScrewdriverTorque.Trigger = txtb_Trigger.Text;
            //m_ScrewdriverTorque.TriggerEnd = txtb_EndTrigger.Text;
            //m_ScrewdriverTorque.BarcodeMatchingValueThreshold = int.Parse(txtb_BCThreshold.Text);

            //if (btn_BarcodeEnable.Text.Contains("Enabled")) m_ScrewdriverTorque.Enable = true;
            //else m_ScrewdriverTorque.Enable = false;

            m_Thermocouple.To_Registry();
        }
    }
}
