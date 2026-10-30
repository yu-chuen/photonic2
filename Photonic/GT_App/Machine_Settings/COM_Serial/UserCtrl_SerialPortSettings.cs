using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;
using StaticCoreLib;

namespace GT_App
{
    public partial class UserCtrl_SerialPortSettings : UserControl
    {
        //variables
        Comm_Serial_Settings m_CommSettings;
        MessageBox_InputString m_Dlg = new MessageBox_InputString();

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_SerialPortSettings()
        {
            InitializeComponent();
        }

        //Set Comm Settings pointer//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_CommSettingsPtr(Comm_Serial_Settings mCommSettings) { m_CommSettings = mCommSettings; }

        //Dialog Load////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_SerialPortSettings_Load(object sender, EventArgs e)
        {
            comboB_PortNumber.Text = m_CommSettings.m_strPortName.Replace("COM", "");
            comboB_Baudrate.Text = m_CommSettings.m_iBaudRate.ToString();
            comboB_DataBits.Text = m_CommSettings.m_iDataBits.ToString();
            comboB_Parity.Text = m_CommSettings.m_parParity.ToString();

            switch (m_CommSettings.m_sbStopBits.ToString().ToLower())
            {
                case "one": comboB_StopBits.Text = "1"; break;
                case "two": comboB_StopBits.Text = "2"; break;
                case "onepointfive": comboB_StopBits.Text = "1.5"; break;
            }
        }
        
        //Get Settings//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Comm_Serial_Settings Get_Settings()
        {
            m_CommSettings.m_strPortName = "COM" + comboB_PortNumber.Text;
            m_CommSettings.m_iBaudRate = int.Parse(comboB_Baudrate.Text);
            m_CommSettings.m_iDataBits = int.Parse(comboB_DataBits.Text);

            switch (comboB_StopBits.Text.ToLower())
            {
                case "1": m_CommSettings.m_sbStopBits = System.IO.Ports.StopBits.One; break;
                case "1.5": m_CommSettings.m_sbStopBits = System.IO.Ports.StopBits.OnePointFive; break;
                case "2": m_CommSettings.m_sbStopBits = System.IO.Ports.StopBits.Two; break;
            }

            switch (comboB_Parity.Text.ToLower())
            {
                case "even": m_CommSettings.m_parParity = System.IO.Ports.Parity.Even; break;
                case "odd": m_CommSettings.m_parParity = System.IO.Ports.Parity.Odd; break;
                case "mark": m_CommSettings.m_parParity = System.IO.Ports.Parity.Mark; break;
                case "space": m_CommSettings.m_parParity = System.IO.Ports.Parity.Space; break;
                case "none": m_CommSettings.m_parParity = System.IO.Ports.Parity.None; break;

                default: m_CommSettings.m_parParity = System.IO.Ports.Parity.Even; break;
            }

            return m_CommSettings;
        }
    }
}
