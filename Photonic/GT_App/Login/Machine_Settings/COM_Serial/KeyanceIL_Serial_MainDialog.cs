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
    public partial class KeyanceIL_Serial_MainDialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;

        Comm_Serial_Thermocouple m_CommDevice;
        UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
        Comm_Serial_Settings m_CommSettings;
        TabPage m_tabpage_userctrl_KeyanceIL = new TabPage();
        Comm_Serial_KeyanceIL m_CommDevice_KeyanceIL_1;
        Comm_Serial_KeyanceIL m_CommDevice_KeyanceIL_2;
        UserCtrl_Serial_KeyanceIL m_userctrl_KeyanceIL = new UserCtrl_Serial_KeyanceIL();
        
        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public KeyanceIL_Serial_MainDialog()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_CommDevice_KeyanceIL_1 = m_SeqMachineCtrl.m_CommSerial_KeyanceIL;

        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Screwdriver_Serial_MainDialog_Load(object sender, EventArgs e)
        {
            //add user ctrl 
            //m_userctrl_SerialPortSettings.Set_CommSettingsPtr(m_CommDevice.m_Settings.m_Settings);
            m_userctrl_KeyanceIL.Set_CodeScannerPtr(m_SeqMachineCtrl.m_CommSerial_KeyanceIL);
            m_tabpage_userctrl_KeyanceIL.Controls.Add(m_userctrl_KeyanceIL);
            m_tabpage_userctrl_KeyanceIL.Text = "Keyance IL";
            this.tabCtrl_CodeReaderSetting.TabPages.Add(m_tabpage_userctrl_KeyanceIL);

        }
    }
}
