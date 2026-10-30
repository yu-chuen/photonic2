using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.IO;
using System.Diagnostics;

namespace GT_App
{
    public partial class Manual_OperationsMainDlg : Form
    {
        //variables
		SeqMachineControl m_pSeqMachineCtrl;
        System.Windows.Forms.Timer m_Timer = new Timer();

		List<UserCtrl_ManualPage_Template> m_list_userctrl_manualPage = new List<UserCtrl_ManualPage_Template>();
		List<TabPage> m_list_TabPage = new List<TabPage>();
        
        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Manual_OperationsMainDlg()
        {
            InitializeComponent();
			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

			//Add tab Pages
			this.Add_UserCtrl();

            //set timer
            m_Timer.Interval = 10;
            m_Timer.Tick += new EventHandler(Update_UI);
            m_Timer.Start();
        }

        //Set the tab color and font/////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tabctrl_ManualOps_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat strFormat = new StringFormat();
            Graphics g = e.Graphics;
            //Brush br = new SolidBrush(Color.Black);
            Brush br;
            Font ft = new System.Drawing.Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);
            
            //Get tab page
            TabPage tbPage = tabctrl_ManualOps.TabPages[e.Index];

            //get tab bounds
            Rectangle rec_tabBounds = tabctrl_ManualOps.GetTabRect(e.Index) ;

            if (e.State == DrawItemState.Selected)
            {
                br = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                br = new System.Drawing.SolidBrush(e.ForeColor);
                //e.DrawBackground();       //this will paint tab background color white
            }

            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(tbPage.Text, ft, br, rec_tabBounds, strFormat);
        }

        //buttons control//////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
			foreach ( UserCtrl_ManualPage_Template mUserCtrl in m_list_userctrl_manualPage )
				mUserCtrl.Dispose();

            m_Timer.Stop();
            this.Close();
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI(object sender, EventArgs e)
        {
			m_Timer.Stop();

			String strTabText = tabctrl_ManualOps.SelectedTab.Text;
			int iCount = 0;

			foreach ( TabPage mtabpage in m_list_TabPage )
			{
                if (mtabpage.Text == strTabText) { m_list_userctrl_manualPage[iCount].Update_UI(); break; }
				iCount++;
			}

			m_Timer.Start();
        }

		//Add tabpage////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Add_TabPage(String strLabel, int iCount)
        {
            m_list_TabPage[iCount].Controls.Add(m_list_userctrl_manualPage[iCount]);
            m_list_TabPage[iCount].Text = strLabel;
            m_list_TabPage[iCount].AutoScroll = true;
            this.tabctrl_ManualOps.TabPages.Add(m_list_TabPage[iCount]);
        }


		//Add user control into tab page///////////////////////////////////////////////////////////////////////////////
		//Note: Add the Input Bits, output bits, Cyclinders and robotstation goto here
		//Note: Do not need to bother to modify UserCtrl_ManualPage_Template unless there is a new template
        public void Add_UserCtrl()
        {
            int iCount = 0;

            //add Control Panel------------------------------------------------------------------------------
            #region ControlPanel
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInAirPressure_UL);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInAirPressure_LL);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInSafetyRelay);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInEStopButton);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInEStopButton_OP110);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation1_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation2_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation3_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation1_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation2_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation3_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_VisionStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_HotBlower_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_HotBlower_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpKeySwitch);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpKeySwitch1);

            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Static);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Static);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Static);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Blink);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Blink);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Blink);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightBuzzer);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight_VisionStat);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_SolderStation);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_HotBlowerStation);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_VisionStation);

            Add_TabPage("Control Panel", iCount);
            #endregion

            //add Solder station 1---------------------------------------------------------------------------
            iCount++;
            #region SolderStation1
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlCustom(1, "OP60");

            //add robot station manual goto
            foreach (Robot_Pos_Settings rbPos in m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot01.m_llRobotSettings)				//add robot position
                m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(rbPos.m_strPosName, rbPos.m_strPosDesc, 1);

            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder1, "Solder Point 1", 1);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder2, "Solder Point 2", 1);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder3, "Solder Point 3", 1);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder4, "Solder Point 4", 1);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder5, "Solder Point 5", 1);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_1_OP60.Pos_Solder6, "Solder Point 6", 1);

            Add_TabPage("SolderStation1\r\n(OP60)", iCount);
            #endregion

            //add Solder station 2---------------------------------------------------------------------------
            iCount++;
            #region SolderStation2
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlCustom(2, "OP70");

            foreach (Robot_Pos_Settings rbPos in m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot02.m_llRobotSettings)				//add robot position
                m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(rbPos.m_strPosName, rbPos.m_strPosDesc, 2);

            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder1, "Solder Point 1", 2);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder2, "Solder Point 2", 2);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder3, "Solder Point 3", 2);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder4, "Solder Point 4", 2);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder5, "Solder Point 5", 2);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_2_OP70.Pos_Solder6, "Solder Point 6", 2);

            Add_TabPage("SolderStation2\r\n(OP70)", iCount);
            #endregion

            //add Solder station 3---------------------------------------------------------------------------
            iCount++;
            #region SolderStation3
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);

            m_list_userctrl_manualPage[iCount].Add_UserCtrlCustom(3, "OP80");

            foreach (Robot_Pos_Settings rbPos in m_pSeqMachineCtrl.m_DataPackageFile.m_DataRobot03.m_llRobotSettings)				//add robot position
                m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(rbPos.m_strPosName, rbPos.m_strPosDesc, 3);

            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder1, "Solder Point 1", 3);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder2, "Solder Point 2", 3);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder3, "Solder Point 3", 3);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder4, "Solder Point 4", 3);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder5, "Solder Point 5", 3);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlRobotGoto(m_pSeqMachineCtrl.m_Module_SolderStation_3_OP80.Pos_Solder6, "Solder Point 6", 3);

            Add_TabPage("SolderStation3\r\n(OP80)", iCount);
            #endregion

            //add Solder station Conv---------------------------------------------------------------------------
            iCount++;
            #region SolderStationConv
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);

            m_list_userctrl_manualPage[iCount].Add_UserCtrlCustom(3, "OP90");

            Add_TabPage("SolderStation\r\nConveyor", iCount);
            #endregion

            //Add OP100------------------------------------------------------------------------------
            iCount++;
            #region OP100
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPresent_2);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPresent_3);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPassThru);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_inpButton_Start);

            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP100.m_outConv_MoveFWD);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP100.m_outConv_MoveREV);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outStopper_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_outStopper_2);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outIndicatorLight_Red);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outIndicatorLight_Green);
            
            if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpButton_Run);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpButton_Stop);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpBridgePlungerLocked);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_cylBridge_LockUnlock);
            }

            m_list_userctrl_manualPage[iCount].Add_UserCtrl_OP90();

            Add_TabPage("Manual\r\n(OP100)", iCount);
            #endregion

            //Add Hotblower------------------------------------------------------------------------------------
            iCount++;
            #region Hot Blower
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP110.m_outConv_MoveFWD);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP110.m_outConv_MoveREV);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_outBlowerOn);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_outBlowerOff);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlDualOutput(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_outBlowerOn, m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_outBlowerOff, m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_list_HotBlowerState, delegate(bool bState) { m_pSeqMachineCtrl.m_Module_OP110_HotBlower.Trigger_HotBlower_OnOff(bState); });

            m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylHotBlower_DnUp);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylBiasor_DnUp);
            //m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylBiasorPusher_ExtRet);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylReflector_ExtRet);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylReflector_UpDown);

            if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1) //leg A
            {
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylCombPNP_AxisX_ExtRet);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylCombPNP_AxisZ_DownUp);
                m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP110_HotBlower.m_cylCombGripper_GripUngrip);
            }

            if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 2) //leg B
            {
            }

            m_list_userctrl_manualPage[iCount].Add_UserCtrlHotBlower();

            Add_TabPage("Hot Blower\r\n(OP110)", iCount);

            #endregion
        }

        private void tabctrl_ManualOps_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
