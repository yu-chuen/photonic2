using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Text.RegularExpressions;
using GT_App;

namespace GT_App
{
    public partial class Keyence_GT2 : Form
    {
        //variables
        UserCtrl_TCP_SettingPage m_userCtrl_TCPSettingPage;
        SeqMachineControl m_SeqMachineCtrl;
		Comm_TCP_KeyenceGT2 m_CommGT2;

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Keyence_GT2()
        {
            InitializeComponent();

			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_CommGT2 = m_SeqMachineCtrl.m_CommTCP_GT2;
        }

		

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Keyence_SR1000Reader_Dialog_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(0, 0);

            //Get Command Setting group box position
            pPoint.Y = gb_CommandSettings.Height + 10;
            pPoint.X = gb_CommandSettings.Location.X;

            //Add TCP Settings
            m_userCtrl_TCPSettingPage = new UserCtrl_TCP_SettingPage();
            m_userCtrl_TCPSettingPage.Set_Settings(m_CommGT2.Get_CommSettings());
            m_userCtrl_TCPSettingPage.Location = pPoint;
            this.Controls.Add(m_userCtrl_TCPSettingPage);

            //Command and reader Settings
            txtb_Trigger.Text = m_CommGT2.Trigger;
            txtb_EndTrigger.Text = m_CommGT2.TriggerEnd;

           }

        //button events/////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            //disconnect
            m_CommGT2.Disconnect();

            this.Close();
        }

        private void btn_UpdateESC_Click(object sender, EventArgs e)
        {
            //Update TCP Comm Settings first
            m_userCtrl_TCPSettingPage.Update_Settings();

            //Update to registry
            m_CommGT2.Trigger = txtb_Trigger.Text;
            m_CommGT2.TriggerEnd = txtb_EndTrigger.Text;

            m_CommGT2.To_Registry();

            //disconnect
            m_CommGT2.Disconnect();

            //Close page (not close)
            //this.Close();
        }
        
        //Error Message/////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
            if (!m_CommGT2.Connect())
            {
                Error_Message(m_CommGT2.Get_ErrorMessage());
                lbl_Response.Text = "Failed to Connect";
            }
            else
                lbl_Response.Text = "Connected";
        }

        private void btn_ReadCode_Click(object sender, EventArgs e)
        {
            Double dTemp = 0.0;

            if (m_CommGT2.ReadValue(ref dTemp) == false)
            {
                Error_Message(m_CommGT2.Get_ErrorMessage());
                return;
            }
            lbl_Response.Text = dTemp.ToString();
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_CommGT2.Disconnect();
            lbl_Response.Text = "Disconnected";
        }

        private void txtb_BC1Threshold_TextChanged(object sender, EventArgs e)
        {
			
        }

        private void txtb_BC2Threshold_TextChanged(object sender, EventArgs e)
        {
			
        }

		private void btn_DecodeResponse_Click(object sender, EventArgs e)
		{
		    
		}

        private void btn_IsConnected_Click(object sender, EventArgs e)
        {
            if (!m_CommGT2.Is_Connected())
            {
                Error_Message(m_CommGT2.Get_ErrorMessage());
            }
            else
            {
                lbl_Response.Text = "Module is still connected.";
            }
        }

        private void btn_BarcodeEnable_001_Click(object sender, EventArgs e)
        {
            
        }

        private void btn_BarcodeEnable_002_Click(object sender, EventArgs e)
        {
            
        }
    }
}
