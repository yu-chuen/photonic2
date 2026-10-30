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

namespace GT_App
{
    public partial class UserCtrl_TCP_IFMRFID_Dlg : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        Comm_TCP_RFID m_RFIDReader = null;

        bool m_bEnable = true;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_TCP_IFMRFID_Dlg()
        {
            InitializeComponent();

            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Set Comm_TCP_RFID ptr//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_RFIDPtr(Comm_TCP_RFID mComm) { this.m_RFIDReader = mComm; }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_TCP_IFMRFID_Dlg_Load(object sender, EventArgs e)
        {
            //add string into combo box
            for (int iCount = 16; iCount <= 100; iCount++)
            {
                cb_ByteLength.Items.Add(iCount.ToString());
            }

            //update UI
            txtb_CommandDelay.Text = m_RFIDReader.CmdSendDelay.ToString();
            txtb_PortNum.Text = m_RFIDReader.PortNumber.ToString();

            cb_ByteLength.Text = m_RFIDReader.Data_ByteLength.ToString();
            cb_RFIDReader.Text = "1";

            Button_Helper(btn_ReaderEnable1, m_RFIDReader.Channel1_Enable_b);
            Button_Helper(btn_ReaderEnable2, m_RFIDReader.Channel2_Enable_b);
            Button_Helper(btn_ReaderEnable3, m_RFIDReader.Channel3_Enable_b);
            Button_Helper(btn_ReaderEnable4, m_RFIDReader.Channel4_Enable_b);

            if (m_RFIDReader.Enable) { btn_Enable.Text = "RFID Enabled"; btn_Enable.BackColor = Color.Green; }
            else { btn_Enable.Text = "RFID Disabled"; btn_Enable.BackColor = Color.Red; }

            Display_IPAddress(m_RFIDReader.IPAddress);
        }


        //update display///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_UI(String strCommand)
        {
            txtb_WriteCmd.Text = m_RFIDReader.m_strWriteData;
            txtb_ReadCmd.Text = m_RFIDReader.m_strReadCmd;
            txtb_Command.Text = strCommand;
            txtb_ReadData.Text = "Response: " + m_RFIDReader.m_strReceiveData + "\r\nSerial ID: " + m_RFIDReader.m_strRFID_ID;
        }

        //Display IP Address////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Display_IPAddress(String strIP)
        {
            String[] strIPArr = strIP.Split('.');

            txtb_IPAddress1.Text = strIPArr[0];
            txtb_IPAddress2.Text = strIPArr[1];
            txtb_IPAddress3.Text = strIPArr[2];
            txtb_IPAddress4.Text = strIPArr[3];
        }

        //Display Error Message Box/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Display_ErrorMessageBox()
        {
            int iErrorID = 0;
            String strErrorAdd = "";
            m_RFIDReader.Get_Error(ref iErrorID, ref strErrorAdd);
            Class_Helper.Display_ErrorMessageBox(m_SeqMachineControl.Get_ErrorMessage(iErrorID, strErrorAdd));
        }

        //Button helper/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Button_Helper(Button obj, bool bItem)
        {
            String strLabel = obj.Text;
            strLabel = strLabel.Replace("Disabled", "").Replace("Enabled", "");

            if (bItem == true)
            {
                obj.Text = strLabel + "Enabled";
                obj.BackColor = Color.Green;
            }
            else
            {
                obj.Text = strLabel + "Disabled";
                obj.BackColor = Color.Red;
            }
        }

        //save settings
        private void Update_Settings()
        {
            //get all data
            m_RFIDReader.Enable = m_bEnable;
            m_RFIDReader.IPAddress = txtb_IPAddress1.Text + "." + txtb_IPAddress2.Text + "." + txtb_IPAddress3.Text + "." + txtb_IPAddress4.Text;
            m_RFIDReader.PortNumber = int.Parse(txtb_PortNum.Text);
            m_RFIDReader.CmdSendDelay = int.Parse(txtb_CommandDelay.Text);
            m_RFIDReader.Data_ByteLength = int.Parse(cb_ByteLength.Text);

            //update to registry
            m_RFIDReader.To_Registry();
        }


        //IP Address text box text change/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtb_IPAddress1_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress1); }
        private void txtb_IPAddress2_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress2); }
        private void txtb_IPAddress3_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress3); }
        private void txtb_IPAddress4_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress4); }

        //Port number text box text changed////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtb_PortNum_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_PortNum.Text) == false)
            {
                txtb_PortNum.Text = Regex.Replace(txtb_PortNum.Text, "[^0-9]", "");
            }
        }

        //Delay command text box text changed///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtb_CommandDelay_TextChanged(object sender, EventArgs e)
        {
            if (Class_Helper.Detect_IntegerOnly(txtb_CommandDelay.Text) == false)
            {
                txtb_CommandDelay.Text = Regex.Replace(txtb_CommandDelay.Text, "[^0-9]", "");
            }
        }


        //Button Clicked////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (m_RFIDReader.Connect() == false)
            {
                Display_ErrorMessageBox();
            }
            else
                txtb_ReadData.Text = "Connected";

            //Update_UI("Connect");
        }

        private void btn_ReadTag_Click(object sender, EventArgs e)
        {
            String strRFID = "";
            if (m_RFIDReader.Send_Command(int.Parse(cb_RFIDReader.Text), ref strRFID) == false) Display_ErrorMessageBox();
            //update receive data text box
            Update_UI(m_RFIDReader.m_strReadCmd);
        }

        private void btn_WriteTag_Click(object sender, EventArgs e)
        {
            if (txtb_Serial.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number is empty."); return; }
            if (m_RFIDReader.Write_Command(int.Parse(cb_RFIDReader.Text), txtb_Serial.Text) == false) Display_ErrorMessageBox();

            Update_UI(m_RFIDReader.m_strWriteData);
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_RFIDReader.Disconnect();
            txtb_ReadData.Text = "Disconnected";
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            ////get all data
            //m_RFIDReader.IPAddress = txtb_IPAddress1.Text + "." + txtb_IPAddress2.Text + "." + txtb_IPAddress3.Text + "." + txtb_IPAddress4.Text;
            //m_RFIDReader.CmdSendDelay = int.Parse(txtb_CommandDelay.Text);
            //m_RFIDReader.PortNumber = int.Parse(txtb_PortNum.Text);
            //m_RFIDReader.Enable = m_bEnable;
            //m_RFIDReader.Data_ByteLength = int.Parse(cb_ByteLength.Text);

            ////update to registry
            //m_RFIDReader.To_Registry();

            Update_Settings();

            MessageBox.Show("Settings Updated.");
        }

        private void btn_ReaderEnable1_Click(object sender, EventArgs e)
        {
            m_RFIDReader.Channel1_Enable_b = (m_RFIDReader.Channel1_Enable_b == true) ? false : true;
            Button_Helper(btn_ReaderEnable1, m_RFIDReader.Channel1_Enable_b);
            Update_Settings();
        }

        private void btn_ReaderEnable2_Click(object sender, EventArgs e)
        {
            m_RFIDReader.Channel2_Enable_b = (m_RFIDReader.Channel2_Enable_b == true) ? false : true;
            Button_Helper(btn_ReaderEnable2, m_RFIDReader.Channel2_Enable_b);
            Update_Settings();
        }

        private void btn_ReaderEnable3_Click(object sender, EventArgs e)
        {
            m_RFIDReader.Channel3_Enable_b = (m_RFIDReader.Channel3_Enable_b == true) ? false : true;
            Button_Helper(btn_ReaderEnable3, m_RFIDReader.Channel3_Enable_b);
            Update_Settings();
        }

        private void btn_ReaderEnable4_Click(object sender, EventArgs e)
        {
            m_RFIDReader.Channel4_Enable_b = (m_RFIDReader.Channel4_Enable_b == true) ? false : true;
            Button_Helper(btn_ReaderEnable4, m_RFIDReader.Channel4_Enable_b);
            Update_Settings();
        }

        private void btn_Enable_Click(object sender, EventArgs e)
        {
            if (m_RFIDReader.Enable)
            {
                m_RFIDReader.Enable = m_bEnable = false;
                btn_Enable.Text = "RFID Disabled";
                btn_Enable.BackColor = Color.Red;
            }
            else
            {
                m_RFIDReader.Enable = m_bEnable = true;
                btn_Enable.Text = "RFID Enabled";
                btn_Enable.BackColor = Color.Green;
            }

            Update_Settings();
        }
    }
}