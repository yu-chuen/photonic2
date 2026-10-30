using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class UserCtrl_OP60_OP70_OP80_SolderStation_Settings_Dlg : UserControl
    {
        //variables
        Data_Common_Stat m_pDataCommonStat;
        SeqMachineControl m_pSeqMachineCtrl;
        SupportFunctions m_pSupportFunctions;

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_OP60_OP70_OP80_SolderStation_Settings_Dlg(Data_Common_Stat pDataCommonStat,  String strLabel)
        {
            InitializeComponent();
            m_pDataCommonStat = pDataCommonStat;
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pSupportFunctions = m_pSeqMachineCtrl.m_SupportFunctions;

            lbl_solder_program.Text = strLabel;
        }

        //Set Data robot package pointer////////////////////////////////////////////////////////////////////////////////////
        public void Set_DataRobot_Ptr(Data_Common_Stat mRobotData) { m_pDataCommonStat = mRobotData; }

        //dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_DataCommonStat_Dlg_Load(object sender, EventArgs e)
        {
            Load_Settings();
        }

        //Load Settings//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Load_Settings()
        {
            //solder program
            lbl_iSetProgClean.Text = m_pDataCommonStat.m_iSolder_Program_Cleaning.ToString();
            lbl_iSolderProg_P1.Text = m_pDataCommonStat.m_iSolder_Program_Pin_1.ToString();
            lbl_iSolderProg_P2.Text = m_pDataCommonStat.m_iSolder_Program_Pin_2.ToString();
            lbl_iSolderProg_P3.Text = m_pDataCommonStat.m_iSolder_Program_Pin_3.ToString();
            lbl_iSolderProg_P4.Text = m_pDataCommonStat.m_iSolder_Program_Pin_4.ToString();
            lbl_iSolderProg_P5.Text = m_pDataCommonStat.m_iSolder_Program_Pin_5.ToString();
            lbl_iSolderProg_P6.Text = m_pDataCommonStat.m_iSolder_Program_Pin_6.ToString();
            lbl_iSolderProg_P7.Text = m_pDataCommonStat.m_iSolder_Program_Pos_7.ToString();
            lbl_iSolderProg_P8.Text = m_pDataCommonStat.m_iSolder_Program_Pos_8.ToString();
            lbl_iSolderProg_P9.Text = m_pDataCommonStat.m_iSolder_Program_Pos_9.ToString();

            //Cleaning setting 
            lbl_CleanUpOffset.Text = m_pDataCommonStat.m_dbCleaningOffset_Up.ToString();
            lbl_CleanDownOffset.Text = m_pDataCommonStat.m_dbCleaningOffset_Dwn.ToString();
            lbl_CleanCycle.Text = m_pDataCommonStat.m_iCleanCycle.ToString();

            //time thresholds
            lbl_Solder_Ready_Timeout.Text = m_pDataCommonStat.m_lg_Solder_Ready_Timeout.ToString();
            lbl_SolderSleepTime.Text = m_pDataCommonStat.m_lg_Solder_Sleep_Timeout.ToString();
            lbl_Solder_HeaterOFF_Timeout.Text = m_pDataCommonStat.m_lg_Solder_HeaterOFF_Timeout.ToString();
            lbl_SolderStartTimeOut.Text = m_pDataCommonStat.m_lg_Soldering_Start_Timeout.ToString();
            lbl_SolderRunTimeout.Text = m_pDataCommonStat.m_lg_Soldering_Run_Timeout.ToString();
            lbl_SolderTipWettingTimeout.Text = m_pDataCommonStat.m_lg_Solder_Tip_Wetting_Timeout.ToString();
            lbl_SolderWaitPalletTimeout.Text = m_pDataCommonStat.m_lg_Solder_Wait_Pallet_Timeout.ToString();
            lbl_SolderNextCycleDelay.Text = m_pDataCommonStat.m_int_Solder_NextCycle_Delay.ToString();
            lbl_FumExtractorOffTime.Text = m_pDataCommonStat.m_lg_FumeOffTime_Max.ToString();

            //solder point selection
            Update_ButtonEnable(btn_SolderPoint1, m_pDataCommonStat.m_bSolder_Pos1);
            Update_ButtonEnable(btn_SolderPoint2, m_pDataCommonStat.m_bSolder_Pos2);
            Update_ButtonEnable(btn_SolderPoint3, m_pDataCommonStat.m_bSolder_Pos3);
            Update_ButtonEnable(btn_SolderPoint4, m_pDataCommonStat.m_bSolder_Pos4);
            Update_ButtonEnable(btn_SolderPoint5, m_pDataCommonStat.m_bSolder_Pos5);
            Update_ButtonEnable(btn_SolderPoint6, m_pDataCommonStat.m_bSolder_Pos6);
            Update_ButtonEnable(btn_SolderPoint7, m_pDataCommonStat.m_bSolder_Pos7);
            Update_ButtonEnable(btn_SolderPoint8, m_pDataCommonStat.m_bSolder_Pos8);
            Update_ButtonEnable(btn_SolderPoint9, m_pDataCommonStat.m_bSolder_Pos9);

            Update_Button_Rotation(btn_SolderPoint1_Rotation, m_pDataCommonStat.m_bSolder_Pos1_Rotation);
            Update_Button_Rotation(btn_SolderPoint2_Rotation, m_pDataCommonStat.m_bSolder_Pos2_Rotation);
            Update_Button_Rotation(btn_SolderPoint3_Rotation, m_pDataCommonStat.m_bSolder_Pos3_Rotation);
            Update_Button_Rotation(btn_SolderPoint4_Rotation, m_pDataCommonStat.m_bSolder_Pos4_Rotation);
            Update_Button_Rotation(btn_SolderPoint5_Rotation, m_pDataCommonStat.m_bSolder_Pos5_Rotation);
            Update_Button_Rotation(btn_SolderPoint6_Rotation, m_pDataCommonStat.m_bSolder_Pos6_Rotation);
            Update_Button_Rotation(btn_SolderPoint7_Rotation, m_pDataCommonStat.m_bSolder_Pos7_Rotation);
            Update_Button_Rotation(btn_SolderPoint8_Rotation, m_pDataCommonStat.m_bSolder_Pos8_Rotation);
            Update_Button_Rotation(btn_SolderPoint9_Rotation, m_pDataCommonStat.m_bSolder_Pos9_Rotation);

            Update_ButtonEnable(btn_SolderPoint1_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos1);
            Update_ButtonEnable(btn_SolderPoint2_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos2);
            Update_ButtonEnable(btn_SolderPoint3_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos3);
            Update_ButtonEnable(btn_SolderPoint4_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos4);
            Update_ButtonEnable(btn_SolderPoint5_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos5);
            Update_ButtonEnable(btn_SolderPoint6_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos6);
            Update_ButtonEnable(btn_SolderPoint7_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos7);
            Update_ButtonEnable(btn_SolderPoint8_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos8);
            Update_ButtonEnable(btn_SolderPoint9_CleanBefore, m_pDataCommonStat.m_bCleanBefore_Pos9);
        }

        //Update Settings////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Settings()
        {
            //solder programs
            m_pDataCommonStat.m_iSolder_Program_Cleaning = int.Parse(lbl_iSetProgClean.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_1 = int.Parse(lbl_iSolderProg_P1.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_2 = int.Parse(lbl_iSolderProg_P2.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_3 = int.Parse(lbl_iSolderProg_P3.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_4 = int.Parse(lbl_iSolderProg_P4.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_5 = int.Parse(lbl_iSolderProg_P5.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_6 = int.Parse(lbl_iSolderProg_P6.Text);
            m_pDataCommonStat.m_iSolder_Program_Pos_7 = int.Parse(lbl_iSolderProg_P7.Text);
            m_pDataCommonStat.m_iSolder_Program_Pos_8 = int.Parse(lbl_iSolderProg_P8.Text);
            m_pDataCommonStat.m_iSolder_Program_Pos_9 = int.Parse(lbl_iSolderProg_P9.Text);

            //cleaning settings
            m_pDataCommonStat.m_dbCleaningOffset_Up = double.Parse(lbl_CleanUpOffset.Text);
            m_pDataCommonStat.m_dbCleaningOffset_Dwn = double.Parse(lbl_CleanDownOffset.Text);
            m_pDataCommonStat.m_iCleanCycle = int.Parse(lbl_CleanCycle.Text);

            //time thresholds
            m_pDataCommonStat.m_lg_Solder_Ready_Timeout = long.Parse(lbl_Solder_Ready_Timeout.Text);
            m_pDataCommonStat.m_lg_Solder_Sleep_Timeout = long.Parse(lbl_SolderSleepTime.Text);
            m_pDataCommonStat.m_lg_Solder_HeaterOFF_Timeout = long.Parse(lbl_Solder_HeaterOFF_Timeout.Text);
            m_pDataCommonStat.m_lg_Soldering_Start_Timeout = long.Parse(lbl_SolderStartTimeOut.Text);
            m_pDataCommonStat.m_lg_Soldering_Run_Timeout = long.Parse(lbl_SolderRunTimeout.Text);
            m_pDataCommonStat.m_lg_Solder_Tip_Wetting_Timeout = long.Parse(lbl_SolderTipWettingTimeout.Text);
            m_pDataCommonStat.m_lg_Solder_Wait_Pallet_Timeout = long.Parse(lbl_SolderWaitPalletTimeout.Text);
            m_pDataCommonStat.m_int_Solder_NextCycle_Delay = int.Parse(lbl_SolderNextCycleDelay.Text);
            m_pDataCommonStat.m_lg_FumeOffTime_Max = long.Parse(lbl_FumExtractorOffTime.Text);

            //enable selection
            m_pDataCommonStat.m_bSolder_Pos1 = Get_Enable(btn_SolderPoint1);
            m_pDataCommonStat.m_bSolder_Pos2 = Get_Enable(btn_SolderPoint2);
            m_pDataCommonStat.m_bSolder_Pos3 = Get_Enable(btn_SolderPoint3);
            m_pDataCommonStat.m_bSolder_Pos4 = Get_Enable(btn_SolderPoint4);
            m_pDataCommonStat.m_bSolder_Pos5 = Get_Enable(btn_SolderPoint5);
            m_pDataCommonStat.m_bSolder_Pos6 = Get_Enable(btn_SolderPoint6);
            m_pDataCommonStat.m_bSolder_Pos7 = Get_Enable(btn_SolderPoint7);
            m_pDataCommonStat.m_bSolder_Pos8 = Get_Enable(btn_SolderPoint8);
            m_pDataCommonStat.m_bSolder_Pos9 = Get_Enable(btn_SolderPoint9);

            m_pDataCommonStat.m_bSolder_Pos1_Rotation = Get_Rotation(btn_SolderPoint1_Rotation);
            m_pDataCommonStat.m_bSolder_Pos2_Rotation = Get_Rotation(btn_SolderPoint2_Rotation);
            m_pDataCommonStat.m_bSolder_Pos3_Rotation = Get_Rotation(btn_SolderPoint3_Rotation);
            m_pDataCommonStat.m_bSolder_Pos4_Rotation = Get_Rotation(btn_SolderPoint4_Rotation);
            m_pDataCommonStat.m_bSolder_Pos5_Rotation = Get_Rotation(btn_SolderPoint5_Rotation);
            m_pDataCommonStat.m_bSolder_Pos6_Rotation = Get_Rotation(btn_SolderPoint6_Rotation);
            m_pDataCommonStat.m_bSolder_Pos7_Rotation = Get_Rotation(btn_SolderPoint7_Rotation);
            m_pDataCommonStat.m_bSolder_Pos8_Rotation = Get_Rotation(btn_SolderPoint8_Rotation);
            m_pDataCommonStat.m_bSolder_Pos9_Rotation = Get_Rotation(btn_SolderPoint9_Rotation);

            m_pDataCommonStat.m_bCleanBefore_Pos1 = Get_Enable(btn_SolderPoint1_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos2 = Get_Enable(btn_SolderPoint2_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos3 = Get_Enable(btn_SolderPoint3_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos4 = Get_Enable(btn_SolderPoint4_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos5 = Get_Enable(btn_SolderPoint5_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos6 = Get_Enable(btn_SolderPoint6_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos7 = Get_Enable(btn_SolderPoint7_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos8 = Get_Enable(btn_SolderPoint8_CleanBefore);
            m_pDataCommonStat.m_bCleanBefore_Pos9 = Get_Enable(btn_SolderPoint9_CleanBefore);

            //save to pkg file
            m_pSeqMachineCtrl.MachineInfo_To_Registry();
            m_pSeqMachineCtrl.Pkg_To_Registry();
        }

        //Class helper
        //Update lable///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Label(Label obj, String strTitle, bool bDoubleSelect = false)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            if (bDoubleSelect == false)
                dlg.Set_Integer();
            else
                dlg.Set_Double();

            dlg.Set_Title(strTitle);
            dlg.Set_Label(strTitle);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
                obj.Text = dlg.Get_DataString();
        }

        public void Update_Label_SolderProg(Label obj, string strTitle, bool bDoubleSelect = false)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            if (bDoubleSelect == false)
                dlg.Set_Integer();
            else
                dlg.Set_Double();

            dlg.Set_Title(strTitle);
            dlg.Set_Label(strTitle);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (int.Parse(dlg.Get_DataString()) >= 1 && int.Parse(dlg.Get_DataString()) <= 7)
                {
                    obj.Text = dlg.Get_DataString();
                }
                else
                {
                    MessageBox.Show("Set Solder Program Failed!\r\n\r\nMin. Value = 1\r\nMax. Value = 7\r\n\r\nPrevious value is applied.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        //Update Button Enable//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_ButtonEnable(Button obj, bool bEnable)
        {
            //remove all enabled and disabled text and trim all leading and ending white spaces
            String strText = obj.Text.Replace("Enabled", "").Replace("Disabled", "").Trim();

            if (bEnable)
            {
                obj.Text = strText + "\r\nEnabled";
                obj.BackColor = Color.LimeGreen;
            }
            else
            {
                obj.Text = strText + "\r\nDisabled";
                obj.BackColor = Color.Red;
            }
        }

        private void Update_Button_Rotation(Button obj, bool bEnable)
        {
            //remove all enabled and disabled text and trim all leading and ending white spaces
            string strText = obj.Text.Replace("Positive", "").Replace("Negative", "").Trim();

            if (bEnable)
            {
                obj.Text = strText + "\r\nPositive";
                obj.BackColor = Color.LimeGreen;
            }
            else
            {
                obj.Text = strText + "\r\nNegative";
                obj.BackColor = Color.Red;
            }
        }

        //Get Enable or Disable//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool Get_EnableInverse(Button obj)
        {
            return (obj.Text.Contains("Enabled") == true) ? false : true;
        }
        private bool Get_Enable(Button obj)
        {
            return (obj.Text.Contains("Enabled") == true) ? true : false;
        }

        private bool Get_RotationInverse(Button obj)
        {
            return (obj.Text.Contains("Positive") == true) ? false : true;
        }
        private bool Get_Rotation(Button obj)
        {
            return (obj.Text.Contains("Positive") == true) ? true : false;
        }

        //button Events///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Update_Click(object sender, EventArgs e)
        {
            Update_Settings();
            MessageBox.Show("Settings Updated.");
        }

        //solder programs
        private void btn_SetProgClean_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSetProgClean, btn_SetProgClean.Text); Update_Settings(); }
        private void btn_SetProg1_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P1, btn_SetProg1.Text); Update_Settings(); }
        private void btn_SetProg2_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P2, btn_SetProg2.Text); Update_Settings(); }
        private void btn_SetProg3_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P3, btn_SetProg3.Text); Update_Settings(); }
        private void btn_SetProg4_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P4, btn_SetProg4.Text); Update_Settings(); }
        private void btn_SetProg5_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P5, btn_SetProg5.Text); Update_Settings(); }
        private void btn_SetProg6_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P6, btn_SetProg6.Text); Update_Settings(); }
        private void btn_SetProg7_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P7, btn_SetProg7.Text); Update_Settings(); }
        private void btn_SetProg8_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P8, btn_SetProg8.Text); Update_Settings(); }
        private void btn_SetProg9_Click(object sender, EventArgs e) { Update_Label_SolderProg(lbl_iSolderProg_P9, btn_SetProg9.Text); Update_Settings(); }

        //cleaning settings
        private void btn_CleanUpOffset_Click(object sender, EventArgs e) { Update_Label(lbl_CleanUpOffset, btn_CleanUpOffset.Text); Update_Settings(); }
        private void btn_CleanDownOffset_Click(object sender, EventArgs e) { Update_Label(lbl_CleanDownOffset, btn_CleanDownOffset.Text); Update_Settings(); }
        private void btn_CleanCycle_Click(object sender, EventArgs e) { Update_Label(lbl_CleanCycle, btn_CleanCycle.Text); Update_Settings(); }

        //timeout thresholds
        private void btn_Solder_HeaterOFF_Timeout_Click(object sender, EventArgs e) { Update_Label(lbl_Solder_HeaterOFF_Timeout, btn_Solder_HeaterOFF_Timeout.Text); Update_Settings(); }
        private void btn_SolderSleepTime_Click(object sender, EventArgs e) { Update_Label(lbl_SolderSleepTime, btn_SolderSleepTimeout.Text); Update_Settings(); }
        private void btn_SolderStartTimeOut_Click(object sender, EventArgs e) { Update_Label(lbl_SolderStartTimeOut, btn_SolderStartTimeOut.Text); Update_Settings(); }
        private void btn_SolderRunTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_SolderRunTimeout, btn_SolderRunTimeout.Text); Update_Settings(); }
        private void btn_Solder_Ready_Timeout_Click(object sender, EventArgs e) { Update_Label(lbl_Solder_Ready_Timeout, btn_Solder_Ready_Timeout.Text); Update_Settings(); }
        private void btn_SolderTipWettingTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_SolderTipWettingTimeout, btn_SolderTipWettingTimeout.Text); Update_Settings(); }
        private void btn_SolderWaitPalletTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_SolderWaitPalletTimeout, btn_SolderWaitPalletTimeout.Text); Update_Settings(); }
        private void btn_SolderNextCycleDelay_Click(object sender, EventArgs e) { Update_Label(lbl_SolderNextCycleDelay, btn_SolderNextCycleDelay.Text); Update_Settings(); }
        private void btn_FumeExtratorOffTime_Click(object sender, EventArgs e) { Update_Label(lbl_FumExtractorOffTime, btn_FumeExtratorOffTime.Text); Update_Settings(); }

        //solder thresholds
        private void btn_New_SolderIronHead_UseCount_Click(object sender, EventArgs e) 
        {
            Update_Label(lbl_New_SolderIronHead_UseCount, btn_New_SolderIronHead_UseCount.Text);
            Update_Settings();
            Load_Settings();
        }

        private void btn_SolderIronHead_Threshold_UseCount_Click(object sender, EventArgs e)
        {
            Update_Label(lbl_SolderIronHead_Threshold_UseCount, btn_SolderIronHead_Threshold_UseCount.Text);
            Update_Settings();
            Load_Settings();
        }

        private void btn_SolderIronHead_Threshold_Percentage_Click(object sender, EventArgs e)
        {
            Update_Label(lbl_SolderIronHead_Threshold_Percentage, btn_SolderIronHead_Threshold_Percentage.Text);
            Update_Settings();
            Load_Settings();
        }   

        private void btn_ResetCurrentIronHeadCounter_Click(object sender, EventArgs e)
        {
            MessageBox_Confirmation dlg = new MessageBox_Confirmation();
            dlg.Set_Title("Are you sure?");
            dlg.Set_Label("Do you want to reset the current solder iron head use counter?\r\nThis action cannot be undone.");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_pSeqMachineCtrl.Pkg_To_Registry();

                Load_Settings();
            }
        }

        private void btn_SolderLead_Length_Program1_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program1, btn_SolderLead_Length_Program1.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program2_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program2, btn_SolderLead_Length_Program2.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program3_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program3, btn_SolderLead_Length_Program3.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program4_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program4, btn_SolderLead_Length_Program4.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program5_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program5, btn_SolderLead_Length_Program5.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program6_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program6, btn_SolderLead_Length_Program6.Text, true); Update_Settings(); }
        private void btn_SolderLead_Length_Program7_Click(object sender, EventArgs e) { Update_Label(lbl_SolderLead_Length_Program7, btn_SolderLead_Length_Program7.Text, true); Update_Settings(); }

        private void btn_New_SolderLead_Total_Length_Click(object sender, EventArgs e) 
        {
            Update_Label(lbl_New_SolderLead_Total_Length, btn_New_SolderLead_Total_Length.Text, true);
            Update_Settings();
            Load_Settings();
        }

        private void btn_SolderLead_Threshold_mm_Click(object sender, EventArgs e) 
        {
            Update_Label(lbl_SolderLead_Threshold_mm, btn_SolderLead_Threshold_mm.Text, true);
            Update_Settings();
            Load_Settings();
        }

        private void btn_SolderLead_Threshold_percentage_Click(object sender, EventArgs e) 
        {
            Update_Label(lbl_SolderLead_Threshold_percentage, btn_SolderLead_Threshold_percentage.Text, true);
            Update_Settings();
            Load_Settings();
        }

        private void btn_Reset_CurrentUsed_SolderLead_Length_Click(object sender, EventArgs e)
        {
            MessageBox_Confirmation dlg = new MessageBox_Confirmation();
            dlg.Set_Title("Are you sure?");
            dlg.Set_Label("Do you want to reset the current used solder lead length?\r\nThis action cannot be undone.");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_pSeqMachineCtrl.Pkg_To_Registry();

                Load_Settings();
            }
        }

        private void btn_Manual_SolderLead_Amount_mm_Click(object sender, EventArgs e)
        {
            Update_Label(lbl_Manual_SolderLead_Amount_mm, btn_Manual_SolderLead_Amount_mm.Text, true);
        }

        private void btn_Add_Manual_SolderLead_Amount_mm_Click(object sender, EventArgs e)
        {
            m_pSeqMachineCtrl.Pkg_To_Registry();
            Load_Settings();
        }

        private void btn_Minus_Manual_SolderLead_Amount_mm_Click(object sender, EventArgs e)
        {
            m_pSeqMachineCtrl.Pkg_To_Registry();
            Load_Settings();
        }

        //solder selection
        private void btn_SolderPoint1_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint1, Get_EnableInverse(btn_SolderPoint1)); Update_Settings(); }
        private void btn_SolderPoint2_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint2, Get_EnableInverse(btn_SolderPoint2)); Update_Settings(); }
        private void btn_SolderPoint3_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint3, Get_EnableInverse(btn_SolderPoint3)); Update_Settings(); }
        private void btn_SolderPoint4_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint4, Get_EnableInverse(btn_SolderPoint4)); Update_Settings(); }
        private void btn_SolderPoint5_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint5, Get_EnableInverse(btn_SolderPoint5)); Update_Settings(); }
        private void btn_SolderPoint6_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint6, Get_EnableInverse(btn_SolderPoint6)); Update_Settings(); }
        private void btn_SolderPoint7_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint7, Get_EnableInverse(btn_SolderPoint7)); Update_Settings(); }
        private void btn_SolderPoint8_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint8, Get_EnableInverse(btn_SolderPoint8)); Update_Settings(); }
        private void btn_SolderPoint9_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint9, Get_EnableInverse(btn_SolderPoint9)); Update_Settings(); }

        private void btn_SolderPoint1_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint1_Rotation, Get_RotationInverse(btn_SolderPoint1_Rotation)); Update_Settings(); }
        private void btn_SolderPoint2_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint2_Rotation, Get_RotationInverse(btn_SolderPoint2_Rotation)); Update_Settings(); }
        private void btn_SolderPoint3_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint3_Rotation, Get_RotationInverse(btn_SolderPoint3_Rotation)); Update_Settings(); }
        private void btn_SolderPoint4_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint4_Rotation, Get_RotationInverse(btn_SolderPoint4_Rotation)); Update_Settings(); }
        private void btn_SolderPoint5_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint5_Rotation, Get_RotationInverse(btn_SolderPoint5_Rotation)); Update_Settings(); }
        private void btn_SolderPoint6_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint6_Rotation, Get_RotationInverse(btn_SolderPoint6_Rotation)); Update_Settings(); }
        private void btn_SolderPoint7_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint7_Rotation, Get_RotationInverse(btn_SolderPoint7_Rotation)); Update_Settings(); }
        private void btn_SolderPoint8_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint8_Rotation, Get_RotationInverse(btn_SolderPoint8_Rotation)); Update_Settings(); }
        private void btn_SolderPoint9_Rotation_Click(object sender, EventArgs e) { Update_Button_Rotation(btn_SolderPoint9_Rotation, Get_RotationInverse(btn_SolderPoint9_Rotation)); Update_Settings(); }

        private void btn_SolderPoint1_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint1_CleanBefore, Get_EnableInverse(btn_SolderPoint1_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint2_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint2_CleanBefore, Get_EnableInverse(btn_SolderPoint2_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint3_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint3_CleanBefore, Get_EnableInverse(btn_SolderPoint3_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint4_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint4_CleanBefore, Get_EnableInverse(btn_SolderPoint4_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint5_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint5_CleanBefore, Get_EnableInverse(btn_SolderPoint5_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint6_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint6_CleanBefore, Get_EnableInverse(btn_SolderPoint6_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint7_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint7_CleanBefore, Get_EnableInverse(btn_SolderPoint7_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint8_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint8_CleanBefore, Get_EnableInverse(btn_SolderPoint8_CleanBefore)); Update_Settings(); }
        private void btn_SolderPoint9_CleanBefore_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint9_CleanBefore, Get_EnableInverse(btn_SolderPoint9_CleanBefore)); Update_Settings(); }
    }
}