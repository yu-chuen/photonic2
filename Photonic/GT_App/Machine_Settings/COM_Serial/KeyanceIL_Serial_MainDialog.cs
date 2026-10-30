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
        UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
        TabPage m_tabpage_userctrl_KeyanceIL = new TabPage();
        Comm_Serial_KeyenceIL m_CommDevice_KeyanceIL_1;
        UserCtrl_Serial_KeyanceIL m_userctrl_KeyanceIL = new UserCtrl_Serial_KeyanceIL();
        
        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public KeyanceIL_Serial_MainDialog()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_CommDevice_KeyanceIL_1 = m_SeqMachineCtrl.m_CommSerial_KeyenceIL;

        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void KeyanceIL_MainDialog_Load(object sender, EventArgs e)
        {
            //add user ctrl 
            m_userctrl_KeyanceIL.Set_CodeScannerPtr(m_SeqMachineCtrl.m_CommSerial_KeyenceIL);
            m_tabpage_userctrl_KeyanceIL.Controls.Add(m_userctrl_KeyanceIL);
            m_tabpage_userctrl_KeyanceIL.Text = "Keyance IL";
            this.tabCtrl_CodeReaderSetting.TabPages.Add(m_tabpage_userctrl_KeyanceIL);
        }
    }
}
