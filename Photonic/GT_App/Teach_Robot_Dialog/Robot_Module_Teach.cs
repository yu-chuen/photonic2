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
		SeqMachineControl m_pSeqMachineCtrl = null;

		List<UserCtrl_4Axis_APSLib_ECAT> m_list_userctrl_RobotTeach = new List<UserCtrl_4Axis_APSLib_ECAT>();
        List<TabPage> m_list_TabPage = new List<TabPage>();

        List<UserCtrl_ManualPage_Template> m_list_userctrl_manualPage = new List<UserCtrl_ManualPage_Template>();
        List<TabPage> m_list_ManualPage = new List<TabPage>();

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
        public Robot_Module_Teach()
        {
			InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

		//Destructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		~Robot_Module_Teach()
		{
			m_Timer.Stop();
		}

		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Robot_Module_Teach_Load(object sender, EventArgs e)
        {
            //Add user control panels here ***************************************************************
            if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder03, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot03);
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder02, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot02);
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder01, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot01);
            }
            else
            {
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder01, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot01);
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder02, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot02);
                Add_UserCtrl(m_pSeqMachineCtrl.m_robotStationSolder03, m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot03);
            }

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
        public void Add_UserCtrl(RobotXYZ_APSlib_PanasonicECAT mRobotStation, Data_Robot mDataRobot)
        {
            //add user ctrl
            UserCtrl_4Axis_APSLib_ECAT mUserCtrl = new UserCtrl_4Axis_APSLib_ECAT(mRobotStation);
            mUserCtrl.Set_DataRobot_Ptr(mDataRobot);
            m_list_userctrl_RobotTeach.Add(mUserCtrl);

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_list_userctrl_RobotTeach[m_list_userctrl_RobotTeach.Count - 1]);
            mTabPage.Text = mRobotStation.m_strLabel;
            mTabPage.AutoScroll = true;

            //set to main ui
            m_list_ManualPage.Add(mTabPage);
            this.tabctrl_MotorPg.TabPages.Add(m_list_ManualPage[m_list_ManualPage.Count - 1]);
        }

		//ESC////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Robot_Module_Teach_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		//Timer Event////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void OnTimer(object sender, EventArgs e)
        {
            m_Timer.Stop();

            //Update the related in view user control
            if (m_list_userctrl_RobotTeach.Count > 0)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].Update_PositionBoxes();
                m_list_userctrl_manualPage[SelectedTab_i].Update_UI();
            }

            m_Timer.Start();
        }

		//Dialog closing//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Robot_Module_Teach_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Timer.Stop();

            //dispose the user control
            for (int iCount = 0; iCount < m_list_userctrl_RobotTeach.Count; iCount++)
                m_list_userctrl_RobotTeach[iCount].Dispose();
        }

		//Selected tab control page///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void tabctrl_MotorPg_SelectedIndexChanged( object sender, EventArgs e )
		{
			SelectedTab_i = tabctrl_MotorPg.SelectedIndex;
        }

        private void tabctrl_MotorPg_KeyDown(object sender, KeyEventArgs e)
        {
            //blocker
            if (FlagEnableKeyPress_b == false) return;

            //Key press selection
            if (e.KeyCode == Keys.Left)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogPosX_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogNegX_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Up)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogPosY_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Down)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogNegY_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageUp)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogPosZ_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.PageDown)
            {
                m_list_userctrl_RobotTeach[SelectedTab_i].btn_JogNegZ_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void btn_KeyPressEnable_Click(object sender, EventArgs e)
        {
            //selection
            FlagEnableKeyPress_b = (FlagEnableKeyPress_b == true) ? false : true;

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
        }


		//Add user control into tab page///////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrl()
        {
            Point pPoint = new Point();
            int iCountDisplay = 0;
            String strStationName = "";

            //add SolderStation1--------------------------------------------------------------------------
            #region SolderStation1
            for (int iCount = 0; iCount < 3; iCount++)
            {
                if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                {
                    switch (iCount)
                    {
                    }
                }
                else
                {
                    switch (iCount)
                    {
                    }
                }

                m_list_TabPage.Add(new TabPage());
                m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

                m_list_userctrl_manualPage[iCount].Add_UserCtrlCustom(iCountDisplay, strStationName);

                //set location
                pPoint.X = m_list_userctrl_RobotTeach[iCount].Size.Width + 5;
                m_list_userctrl_manualPage[iCount].Location = pPoint;

                //set to tabcontrol
                m_list_ManualPage[iCount].Controls.Add(m_list_userctrl_manualPage[iCount]);
               
            }
            #endregion

        }
    }
}