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
	public partial class RobotSetting_APSLib_PanasonicECAT_Dlg : Form
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;
		RobotXYZ_APSlib_PanasonicECAT m_RobotStation = null;

		UserCtrl_Indexer_SettingPage m_userctrl_settingdlg_AxisX;
		UserCtrl_Indexer_SettingPage m_userctrl_settingdlg_AxisY;
		UserCtrl_Indexer_SettingPage m_userctrl_settingdlg_AxisZ;

		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////
		public RobotSetting_APSLib_PanasonicECAT_Dlg()
		{
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
		}

		//Set Robot Station//////////////////////////////////////////////////////////////////////////////////////
		public void Set_RobotStation_Ptr( RobotXYZ_APSlib_PanasonicECAT mRobotStation ) { m_RobotStation = mRobotStation; }

		//Dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////
		private void RobotSetting_APSLib_PanasonicECAT_Dlg_Load( object sender, EventArgs e )
		{
			Point pPoint = new Point(0,0);

			//Add robot station setting user control
			m_userctrl_settingdlg_AxisX = new UserCtrl_Indexer_SettingPage();
			m_userctrl_settingdlg_AxisY = new UserCtrl_Indexer_SettingPage();
			m_userctrl_settingdlg_AxisZ = new UserCtrl_Indexer_SettingPage();

			m_userctrl_settingdlg_AxisX.Set_SettingsPtr(m_RobotStation.m_indexerAxisX.m_Settings);
			m_userctrl_settingdlg_AxisX.Location = pPoint;
			m_userctrl_settingdlg_AxisX.Set_Label(m_RobotStation.m_indexerAxisX.m_strLabel);
			this.Controls.Add(m_userctrl_settingdlg_AxisX);

			pPoint.X = m_userctrl_settingdlg_AxisX.Size.Width + 5;
			m_userctrl_settingdlg_AxisY.Set_SettingsPtr(m_RobotStation.m_indexerAxisY1.m_Settings);
			m_userctrl_settingdlg_AxisY.Location = pPoint;
			m_userctrl_settingdlg_AxisY.Set_Label(m_RobotStation.m_indexerAxisY1.m_strLabel);
			this.Controls.Add(m_userctrl_settingdlg_AxisY);

			pPoint.X = (m_userctrl_settingdlg_AxisX.Size.Width * 2) + 5;
			m_userctrl_settingdlg_AxisZ.Set_SettingsPtr(m_RobotStation.m_indexerAxisZ.m_Settings);
			m_userctrl_settingdlg_AxisZ.Location = pPoint;
			m_userctrl_settingdlg_AxisZ.Set_Label(m_RobotStation.m_indexerAxisZ.m_strLabel);
			this.Controls.Add(m_userctrl_settingdlg_AxisZ);


		}

		//Button Events//////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_UpdateEsc_Click( object sender, EventArgs e )
		{
			m_RobotStation.m_indexerAxisX.m_Settings = m_userctrl_settingdlg_AxisX.Get_Settings();
			m_RobotStation.m_indexerAxisY1.m_Settings = m_userctrl_settingdlg_AxisY.Get_Settings();
			m_RobotStation.m_indexerAxisZ.m_Settings = m_userctrl_settingdlg_AxisZ.Get_Settings();

			//return OK
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.Close();
		}
	}
}
