using StaticCoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class Io : Form
    {

        SeqMachineControl m_pSeqMachineCtrl;
        System.Windows.Forms.Timer m_Timer = new Timer();

        List<UserCtrl_ManualPage_Template> m_list_userctrl_manualPage = new List<UserCtrl_ManualPage_Template>();
        List<TabPage> m_list_TabPage = new List<TabPage>();
        public Io()
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
        private void tabctrl_IO_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat strFormat = new StringFormat();
            Graphics g = e.Graphics;
            //Brush br = new SolidBrush(Color.Black);
            Brush br;
            Font ft = new System.Drawing.Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

            //Get tab page
            TabPage tbPage = tabctrl_IO.TabPages[e.Index];

            //get tab bounds
            Rectangle rec_tabBounds = tabctrl_IO.GetTabRect(e.Index);

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
            foreach (UserCtrl_ManualPage_Template mUserCtrl in m_list_userctrl_manualPage)
                mUserCtrl.Dispose();

            m_Timer.Stop();
            this.Close();
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI(object sender, EventArgs e)
        {
            m_Timer.Stop();

            String strTabText = tabctrl_IO.SelectedTab.Text;
            int iCount = 0;

            foreach (TabPage mtabpage in m_list_TabPage)
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
            //MessageBox.Show(strLabel);
            this.tabctrl_IO.TabPages.Add(m_list_TabPage[iCount]);
        }



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

            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightBuzzer);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight_VisionStat);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_SolderStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_HotBlowerStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_VisionStation);

            Add_TabPage("Control Panel", iCount);
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

            //if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            //{
            //    m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpButton_Run);
            //    m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpButton_Stop);
            //    m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_inpBridgePlungerLocked);
            //    m_list_userctrl_manualPage[iCount].Add_UserCtrlCylinder(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_cylBridge_LockUnlock);
            //}

            //m_list_userctrl_manualPage[iCount].Add_UserCtrl_OP90();

            Add_TabPage("Manual\r\n(OP100)", iCount);
            #endregion

        }

        private void Io_Load(object sender, EventArgs e)
        {

        }

        private void tabctrl_IO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
