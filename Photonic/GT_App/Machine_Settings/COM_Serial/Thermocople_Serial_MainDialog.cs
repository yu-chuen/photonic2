using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
    public partial class Thermocouple_Serial_MainDialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
        Comm_Serial_Thermocouple m_CommDevice;
        
        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Thermocouple_Serial_MainDialog()
        {
            InitializeComponent();

            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_CommDevice = m_SeqMachineCtrl.m_CommSerialThermocouple;
        }

        private void Thermocouple_Serial_MainDialog_Load(object sender, EventArgs e)
        {
            //load user control
            Point pPoint = new Point();
            pPoint.X = groupBox_Operations.Size.Width + 10;
            pPoint.Y = 0;

            //add user ctrl 
            m_userctrl_SerialPortSettings.Set_CommSettingsPtr(m_CommDevice.m_Settings.m_Settings);
            m_userctrl_SerialPortSettings.Location = pPoint;
            this.Controls.Add(m_userctrl_SerialPortSettings);
        }

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
            if (!m_CommDevice.Connect())
            {
                Error_Message(m_CommDevice.Get_ErrorMessage());
                lbl_Response.Text = "Failed to Connect to reader";
            }
            else
                lbl_Response.Text = "Connected to Reader.";
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                backgroundWorker1.CancelAsync();
            }

            m_CommDevice.Disconnect();
            lbl_Response.Text = "Disconnected from Reader.";
        }

        private void btn_UpdateSettings_Click(object sender, EventArgs e)
        {
            //get serial com settings
            m_CommDevice.m_Settings.m_Settings = m_userctrl_SerialPortSettings.Get_Settings();
            m_CommDevice.To_Registry();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == true)
            {
                backgroundWorker1.CancelAsync();
            }

            this.Close();
        }

        private void btn_ReadTemperature_Click(object sender, EventArgs e)
        {
            if (backgroundWorker1.IsBusy == false)
            {
                //Start background worker
                backgroundWorker1.RunWorkerAsync();
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (worker.CancellationPending == false)
            {
                this.Invoke((MethodInvoker)delegate { if (Update_UI() == false) { backgroundWorker1.CancelAsync(); } });
                Thread.Sleep(100);
            }
        }

        private bool Update_UI()
        {
            String strData = "";

            //Read Code
            if (m_CommDevice.Read_Temperature(ref strData) == false)
            {
                Error_Message(m_CommDevice.Get_ErrorMessage());
                return false;
            }
            else
            {
                strData = double.Parse(strData).ToString();
                lbl_Response.Text = strData + " °C";
                return true;
            }
        }
    }
}
