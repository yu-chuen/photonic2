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
    public partial class Robot_Module_Teach : Form
    {
        //variables
        TabPage m_tab1 = new TabPage();
		TabPage m_tab2 = new TabPage();
		TabPage m_tab3 = new TabPage();

		UserCtrl_RobotXYZ_APSLib_PanasonicECAT m_userctrl_RobotStation01 = null;
		UserCtrl_RobotXYZ_APSLib_PanasonicECAT m_userctrl_RobotStation02 = null;
		UserCtrl_RobotXYZ_APSLib_PanasonicECAT m_userctrl_RobotStation03 = null;

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //add tabs here
        public Robot_Module_Teach()
        {
            InitializeComponent();
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

			m_userctrl_RobotStation01 = new UserCtrl_RobotXYZ_APSLib_PanasonicECAT(pApp.m_SeqMachineCtrl.m_robotStationSolder01);
			m_userctrl_RobotStation01.Set_DataRobot_Ptr(pApp.m_SeqMachineCtrl.m_DataPackageFile.m_DataRobot01);
			m_tab1.Controls.Add(m_userctrl_RobotStation01);
			m_tab1.Text = pApp.m_SeqMachineCtrl.m_robotStationSolder01.m_strLabel;
			this.tabctrl_MotorPg.TabPages.Add(m_tab1);

			m_userctrl_RobotStation02 = new UserCtrl_RobotXYZ_APSLib_PanasonicECAT(pApp.m_SeqMachineCtrl.m_robotStationSolder02);
			m_userctrl_RobotStation02.Set_DataRobot_Ptr(pApp.m_SeqMachineCtrl.m_DataPackageFile.m_DataRobot02);
			m_tab2.Controls.Add(m_userctrl_RobotStation02);
			m_tab2.Text = pApp.m_SeqMachineCtrl.m_robotStationSolder02.m_strLabel;
			this.tabctrl_MotorPg.TabPages.Add(m_tab2);

			m_userctrl_RobotStation03 = new UserCtrl_RobotXYZ_APSLib_PanasonicECAT(pApp.m_SeqMachineCtrl.m_robotStationSolder03);
			m_userctrl_RobotStation03.Set_DataRobot_Ptr(pApp.m_SeqMachineCtrl.m_DataPackageFile.m_DataRobot03);
			m_tab3.Controls.Add(m_userctrl_RobotStation03);
			m_tab3.Text = pApp.m_SeqMachineCtrl.m_robotStationSolder03.m_strLabel;
			this.tabctrl_MotorPg.TabPages.Add(m_tab3);
        }

		//Destructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		~Robot_Module_Teach()
		{
			m_Timer.Stop();
		}

		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Robot_Module_Teach_Load( object sender, EventArgs e )
		{

		}

		//ESC////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Robot_Module_Teach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.Close();
		}


		//Timer Event////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTimer( object sender, EventArgs e )
		{
			m_Timer.Stop();

			int iSelectedTab = this.tabctrl_MotorPg.SelectedIndex;
			if ( Global_Var.m_bOfflineTest == true )
			{
				switch ( iSelectedTab )
				{
					case 1: m_userctrl_RobotStation01.Update_PositionBoxes(); break;
					case 2: m_userctrl_RobotStation02.Update_PositionBoxes(); break;
					case 3: m_userctrl_RobotStation03.Update_PositionBoxes(); break;
				}
			}

			m_Timer.Start();
		}

		//Dialog closing//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Robot_Module_Teach_FormClosing( object sender, FormClosingEventArgs e )
		{
			m_Timer.Stop();
		}

    }
}
