using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

/****************************************************************************************************************************************************************************************
 * NOTES: 
 *              1.  Commands are obtain via SeqMachineControl : Vision_GetCommands(...)
 *              2. Add vision user control dialog is done using the Add_UserCtrl function.
****************************************************************************************************************************************************************************************/

namespace GT_App
{
    public partial class Vision_ControlDialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineControl;

		List<UserCtrl_VisionSettingsDialog> m_list_userCtrl_VisionSettingDlg = new List<UserCtrl_VisionSettingsDialog>();
		List<TabPage> m_list_tabpages = new List<TabPage>();

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vision_ControlDialog()
        {
            InitializeComponent();
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            
        }

        //Dialog Load///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Vision_ControlDialog_Load(object sender, EventArgs e)
        {
			Add_UserCtrl(m_SeqMachineControl.m_Camera1);
			Add_UserCtrl(m_SeqMachineControl.m_Camera2);
        }

		//Add UserCtrl/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Add_UserCtrl(Vision_Cognex mVision)
		{
			int iCount = m_list_userCtrl_VisionSettingDlg.Count;		//adding current number base 0

			m_list_userCtrl_VisionSettingDlg.Add(new UserCtrl_VisionSettingsDialog());
			m_list_userCtrl_VisionSettingDlg[iCount].Set_VisionCognexPtr(mVision);
			m_list_userCtrl_VisionSettingDlg[iCount].VisionCam_Number = iCount + 1;

			m_list_tabpages.Add(new TabPage());
			m_list_tabpages[iCount].Controls.Add(m_list_userCtrl_VisionSettingDlg[iCount]);
			m_list_tabpages[iCount].Text = mVision.Label;
			this.TabCtrl_VisionDlg.TabPages.Add(m_list_tabpages[iCount]);
		}

		//button events//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }


    }
}
