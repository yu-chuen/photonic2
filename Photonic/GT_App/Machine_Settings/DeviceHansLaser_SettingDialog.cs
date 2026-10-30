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
    public partial class DeviceHansLaser_SettingDialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Device_HansLaserMarker m_DeviceModule;
        Device_HansLaserMarker_Settings m_Settings;

        bool m_bEnable = false;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DeviceHansLaser_SettingDialog()
        {
            InitializeComponent();

            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DeviceModule = m_SeqMachineCtrl.m_DeviceHansLaser;
            m_Settings = m_DeviceModule.Get_Settings();
        }

        //Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DeviceHansLaser_SettingDialog_Load(object sender, EventArgs e)
        {
            lbl_DataFileName.Text = m_DeviceModule.DataFileName;
            lbl_DataFilePath.Text = m_DeviceModule.DataFilePath;
            txtb_AppFileName.Text = m_DeviceModule.AppFilename;
            lbl_AppFilePath.Text = m_DeviceModule.AppFilePath;
            m_bEnable = m_DeviceModule.DeviceEnable;
			//lbl_TimeOutSetting.Text = m_SeqMachineCtrl.m_MachineInfo.m_lg_TimeOutLaserMarker.ToString();

            //update enable button
			if (m_bEnable == false) btn_Enable.Text = "Disabled";
			//if (m_DeviceModule.CodeEnable_b_001 == false) btn_CodeEnable_001.Text = "Code 1 Disabled";
			//if (m_DeviceModule.CodeEnable_b_002 == false) btn_CodeEnable_002.Text = "Code 2 Disabled";

            //update enable feature desc
            lbl_EnableDesc.Text = "Enabled = Device is set and ready for laser marking.\r\n"+
                                                    "Disabled = Device is blocked from performing laser marking.";
        }

        //Helper///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Display_MessageBox (String strError, int iErrorMsgBoxSelect = 1)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            dlg.Set_BitMapLogo(iErrorMsgBoxSelect);
            dlg.Set_Buttons(1);
            dlg.Set_Messages(strError, "");
            dlg.ShowDialog();
            dlg.Dispose();
        }


        //Button Events////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_DataFilePath_FolderDlg_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = lbl_DataFilePath.Text;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                lbl_DataFilePath.Text = dlg.SelectedPath;

            dlg.Dispose();
        }

        private void btn_DataFileName_InputDlg_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_String();
            dlg.Set_Label("");
            dlg.Set_Title("");

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbl_DataFileName.Text = dlg.Get_DataString();
            }

            dlg.Dispose();
        }

        private void btn_UpdateNEsc_Click(object sender, EventArgs e)
        {
            //Get data
            m_DeviceModule.DataFileName = lbl_DataFileName.Text;
            m_DeviceModule.DataFilePath = lbl_DataFilePath.Text;
            m_DeviceModule.AppFilename = txtb_AppFileName.Text;
            m_DeviceModule.AppFilePath = lbl_AppFilePath.Text;
            m_DeviceModule.DeviceEnable = m_bEnable;
            
			//m_SeqMachineCtrl.m_MachineInfo.m_lg_TimeOutLaserMarker = int.Parse(lbl_TimeOutSetting.Text);

            //Update registry file
            m_DeviceModule.To_Registry();
            m_SeqMachineCtrl.MachineInfo_To_Registry();
            this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_WriteIDToFile_Click(object sender, EventArgs e)
        {
            if (m_DeviceModule.Write_ToFile(txtb_DataCode1.Text, txtb_DataCode2.Text) == false)
                Display_MessageBox("Error: " + m_DeviceModule.Get_ErrorMessage(), 1);
            else
                Display_MessageBox("Write to File success", 0);
        }

        private void btn_TriggerLaserMarker_Click(object sender, EventArgs e)
        {
			//if (m_SeqMachineCtrl.m_pModule_LaserMarker.LaserMark_Trigger(txtb_DataCode1.Text, txtb_DataCode2.Text) == false)
			//    Display_MessageBox("Error: " + m_SeqMachineCtrl.m_pModule_LaserMarker.Get_ErrorMessage());
			//else
			//    Display_MessageBox("Trigger Complete. Please check the part.", 0);
        }

        private void btn_CheckFileExist_Click(object sender, EventArgs e)
        {
            if (m_DeviceModule.Is_DataFileExist() == false)
                Display_MessageBox("Data File does not exist.\r\nFile path=" + m_Settings.m_strFilePath, 1);
            else
                Display_MessageBox("Data File Exist", 0);
        }

        private void btn_DeleteFile_Click(object sender, EventArgs e)
        {
            m_DeviceModule.Delete_DataFile();
            Display_MessageBox("Delete File Complete", 0);
        }

        private void btn_Enable_Click(object sender, EventArgs e)
        {
            if (m_bEnable)
            {
                btn_Enable.Text = "Disabled";
                m_bEnable = false;
            }
            else
            {
                btn_Enable.Text = "Enabled";
                m_bEnable = true;
            }
        }

        private void btn_TimeOutSetting_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Label("Laser Marker Time Out (ms)");
            dlg.Set_Title("Laser Marker Time Out Setting");

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbl_TimeOutSetting.Text = dlg.Get_DataString();
            }

            dlg.Dispose();
        }

        private void btn_CodeEnable_001_Click(object sender, EventArgs e)
        {
            //if (btn_CodeEnable_001.Text.Contains("Enabled"))
            //    btn_CodeEnable_001.Text = "Code 1 Disabled";
            //else
            //    btn_CodeEnable_001.Text = "Code 1 Enabled";
            Display_MessageBox("Please set this at the Change process flag window", 0);
        }

        private void btn_CodeEnable_002_Click(object sender, EventArgs e)
        {

        //    if (btn_CodeEnable_002.Text.Contains("Enabled"))
        //        btn_CodeEnable_002.Text = "Code 2 Disabled";
        //    else
        //        btn_CodeEnable_002.Text = "Code 2 Enabled";
            Display_MessageBox("Please set this at the Change process flag window", 0);
        }

        private void btn_AppFilePath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();
            dlg.SelectedPath = lbl_DataFilePath.Text;

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                lbl_AppFilePath.Text = dlg.SelectedPath;
            }

            dlg.Dispose();
        }

        private void btn_OpenApp_Click(object sender, EventArgs e)
        {
            if (!m_DeviceModule.Open_HLMApplication())
            {
                Display_MessageBox(m_DeviceModule.Get_ErrorMessage());
            }
        }





    }
}
