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
    public partial class Setup_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        MachineInfo m_MachineInfo = null;
        String m_strRecipeFilename = "";

        //Constructor///////////////////////////////////////////////////////////////////////////
        public Setup_Dialog()
        {
            InitializeComponent();

            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_MachineInfo = m_SeqMachineControl.m_MachineInfo;
        }

        //Loader//////////////////////////////////////////////////////////////////////////////////
        private void Setup_Dialog_Load(object sender, EventArgs e)
        {
			String strError = "";
			if ( m_SeqMachineControl.Get_PackageList(ref strError) > 0 )
			{
				foreach ( String str in m_SeqMachineControl.m_strPackageList )
					listView_PackageFile.Items.Add(str);
			}
			else
				Class_Helper.Display_ErrorMessageBox(strError);

			lbl_PackageFileSelect.Text = m_MachineInfo.m_strPackageFileName;
        }

        //Button Events///////////////////////////////////////////////////////////////////////
        private void btn_UpdateESC_Click(object sender, EventArgs e)
		{
			Main_Dlg dlg = GT_App.Program.m_MainDialog;

			m_MachineInfo.m_strPackageFileName = lbl_PackageFileSelect.Text;

			m_SeqMachineControl.MachineInfo_To_Registry();
			m_SeqMachineControl.Pkg_From_Registry(m_MachineInfo.m_strPackageFileName);

			//Update main UI
			dlg.Update_UserCtrlUI();

            this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		private void listView_PackageFile_SelectedIndexChanged( object sender, EventArgs e )
		{
			//blocker
			if ( listView_PackageFile.SelectedIndices.Count == 0 ) return;

			//get string
			String strRecipeName = listView_PackageFile.SelectedItems[0].Text;
			lbl_PackageFileSelect.Text = strRecipeName.Replace(m_MachineInfo.m_strPackageFileExt, "");
		}

    }
}
