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
    public partial class SetFilePathDialog : Form
    {
        //variables
        SeqMachineControl m_seqMachineControl = null;
        MachineInfo m_MachineInfo = null;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SetFilePathDialog()
        {
            InitializeComponent();
            m_seqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_MachineInfo = m_seqMachineControl.m_MachineInfo;
        }

        //Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void SetFilePathDialog_Load(object sender, EventArgs e)
        {
            //Update textbox
            txtb_PackageFilePath.Text = m_seqMachineControl.m_MachineInfo.m_strPackageFilePath;
        }

		//Class Helper: Browse Folder Dialog//////////////////////////////////////////////////////////////////////////////////////////////////
        private void Browse_Folder(TextBox obj)
        {
            FolderBrowserDialog dlg = new FolderBrowserDialog();

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                obj.Text = dlg.SelectedPath;

            dlg.Dispose();
        }

		//Button events///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_FileDialog_PackageFilePath_Click(object sender, EventArgs e) { Browse_Folder(txtb_PackageFilePath); }
        
        private void btn_UpdateEsc_Click(object sender, EventArgs e)
        {
            //Get data
            m_seqMachineControl.m_MachineInfo.m_strPackageFilePath = txtb_PackageFilePath.Text;

            //write to registry file
            m_seqMachineControl.MachineInfo_To_Registry();
            //Set data log file paths
            m_seqMachineControl.Setup_LogFiles();

            //Close the dialog
            this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
