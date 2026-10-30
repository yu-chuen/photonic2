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
    public partial class Robot_Module_Teach_HotBlower : Form
    {
		//variables
		SeqMachineControl m_pSeqMachineCtrl = null;

		List<UserCtrl_4Axis_APSLib_ECAT> m_list_userctrl_RobotTeach = new List<UserCtrl_4Axis_APSLib_ECAT>();

        List<UserCtrl_RobotXY_APSLib_PanasonicECAT> m_list_userctrl_RobotTeach_HotBlower = new List<UserCtrl_RobotXY_APSLib_PanasonicECAT>();

		List<UserCtrl_ManualPage_Template> m_list_userctrl_manualPage = new List<UserCtrl_ManualPage_Template>();
		List<TabPage> m_list_TabPage = new List<TabPage>();

        List<TabPage> m_ll_TabPage = new List<TabPage>();

		private readonly object m_lock_SelectTab = new object();
		int m_iSelectedTab = 0;
		int SelectedTab_i
		{
			set { lock ( m_lock_SelectTab ) { m_iSelectedTab = value; } }
			get { lock ( m_lock_SelectTab ) { return m_iSelectedTab; } }
		}

		private readonly object m_lock_FlagEnableKeyPress = new object();
		bool m_bFlagEnableKeyPress = false;
		bool FlagEnableKeyPress_b { set { lock ( m_lock_FlagEnableKeyPress ) { m_bFlagEnableKeyPress = value; } } get { lock ( m_lock_FlagEnableKeyPress ) { return m_bFlagEnableKeyPress; } } }

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //add tabs here
        public Robot_Module_Teach_HotBlower()
        {
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

		//Destructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Robot_Module_Teach_HotBlower()
		{
			m_Timer.Stop();
		}

		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Robot_Module_Teach_Load(object sender, EventArgs e)
        {
            Add_UserCtrl_HotBlower(m_pSeqMachineCtrl.m_robotStationHotBlower, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot04);

            //Add manual Page IO
            Add_UserCtrl();

            //display button
            if (FlagEnableKeyPress_b)
            {
                btn_KeyPressEnable.BackColor = Color.Green;
                btn_KeyPressEnable.Text = "Key Press Enabled";
            }
            else
            {
                btn_KeyPressEnable.BackColor = Color.Red;
                btn_KeyPressEnable.Text = "Key Press Disabled";
            }

            //Start timer
            m_Timer.Start();
        }

        //Add user ctrl//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Add_UserCtrl_HotBlower( RobotXY_APSlib_PanasonicECAT mRobotStation_HB, Data_Robot mDataRobot )
		{
			//add user ctrl
			UserCtrl_RobotXY_APSLib_PanasonicECAT mUserCtrl_HotBlower = new UserCtrl_RobotXY_APSLib_PanasonicECAT(mRobotStation_HB);
            mUserCtrl_HotBlower.Set_DataRobot_Ptr(mDataRobot);
            m_list_userctrl_RobotTeach_HotBlower.Add(mUserCtrl_HotBlower);

			//Add Tab page
			TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_list_userctrl_RobotTeach_HotBlower[m_list_userctrl_RobotTeach_HotBlower.Count - 1]);
            mTabPage.Text = mRobotStation_HB.m_strLabel;

			//set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabctrl_MotorPg.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
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
            if (m_list_userctrl_RobotTeach_HotBlower.Count > 0)
            {
                m_list_userctrl_RobotTeach_HotBlower[SelectedTab_i].Update_PositionBoxes();
                m_list_userctrl_manualPage[SelectedTab_i].Update_UI();
            }
			m_Timer.Start();
		}

		//Dialog closing//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Robot_Module_Teach_FormClosing( object sender, FormClosingEventArgs e )
		{
			m_Timer.Stop();

            for (int iCount = 0; iCount < m_list_userctrl_RobotTeach_HotBlower.Count; iCount++)
                m_list_userctrl_RobotTeach_HotBlower[iCount].Dispose();
		}

		//Selected tab control page///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void tabctrl_MotorPg_SelectedIndexChanged( object sender, EventArgs e )
		{
			SelectedTab_i = tabctrl_MotorPg.SelectedIndex;
		}

		private void tabctrl_MotorPg_KeyDown( object sender, KeyEventArgs e )
		{
			//blocker
			if ( FlagEnableKeyPress_b == false ) return;

			//Key press selection
			if ( e.KeyCode == Keys.Left )
			{
                m_list_userctrl_RobotTeach_HotBlower[SelectedTab_i].btn_JogPosX_Click(null, EventArgs.Empty);
				e.Handled = true;
			}
			else if ( e.KeyCode == Keys.Right )
			{
                m_list_userctrl_RobotTeach_HotBlower[SelectedTab_i].btn_JogNegX_Click(null, EventArgs.Empty);
				e.Handled = true;
			}
			else if ( e.KeyCode == Keys.Up )
			{
                m_list_userctrl_RobotTeach_HotBlower[SelectedTab_i].btn_JogPosY_Click(null, EventArgs.Empty);
				e.Handled = true;
			}
			else if ( e.KeyCode == Keys.Down )
			{
                m_list_userctrl_RobotTeach_HotBlower[SelectedTab_i].btn_JogNegY_Click(null, EventArgs.Empty);
				e.Handled = true;
			}


		}

		private void btn_KeyPressEnable_Click( object sender, EventArgs e )
		{
			//selection
			FlagEnableKeyPress_b = ( FlagEnableKeyPress_b == true ) ? false : true;

			//display button
			if ( FlagEnableKeyPress_b )
			{
				btn_KeyPressEnable.BackColor = Color.Green;
				btn_KeyPressEnable.Text = "Key Press Enabled";
			}
			else
			{
				btn_KeyPressEnable.BackColor = Color.Red;
				btn_KeyPressEnable.Text = "Key Press Disabled";
			}
		}


		//Add user control into tab page///////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrl()
        {
            Module_OP110_HotBlower m_pModuleHotBlower = null;
            Module_FwdConv m_pModuleFwdConv_HotBlower = null;
            Point pPoint = new Point();
            String strStationName = "";

            for (int iCount = 0; iCount < 1; iCount++)
            {
                switch (iCount)
                {
                    case 0:
                        m_pModuleHotBlower = m_pSeqMachineCtrl.m_Module_OP110_HotBlower;
                        m_pModuleFwdConv_HotBlower = m_pSeqMachineCtrl.m_Module_FwdConv_OP110;
                        strStationName = "OP110";
                        break;
                }

                m_list_TabPage.Add(new TabPage());
                m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

                m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pModuleFwdConv_HotBlower.m_outConv_MoveFWD);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pModuleFwdConv_HotBlower.m_outConv_MoveREV);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pModuleHotBlower.m_outBlowerOn);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pModuleHotBlower.m_outBlowerOff);

                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylHotBlower_DnUp);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylBiasor_DnUp);
                //m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylBiasorPusher_ExtRet);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylReflector_ExtRet);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylReflector_UpDown);

                if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                {
                    m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet);
                    m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp);
                    m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pModuleHotBlower.m_cylCombGripper_GripUngrip);
                }

                if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 2)
                {
                }

                //set location
                pPoint.X = m_list_userctrl_RobotTeach_HotBlower[iCount].Size.Width;
                m_list_userctrl_manualPage[iCount].Location = pPoint;

                //set to tabcontrol
                m_ll_TabPage[iCount].Controls.Add(m_list_userctrl_manualPage[iCount]);
            }
        }
    }
}
