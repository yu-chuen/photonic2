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
    public partial class UserCtrl_OP110_HotBlower_Settings : UserControl
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl;
        MachineInfo m_pMachineInfo;
        Data_Common m_pDataCommon;

        //constructor/////////////////////////////////////////////////////////////////////
        public UserCtrl_OP110_HotBlower_Settings()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pMachineInfo = m_pSeqMachineCtrl.m_MachineInfo;
            m_pDataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////
        private void UserCtrl_CommonData_Load(object sender, EventArgs e)
        {
            lbl_HotBlowerTemperature.Text = m_pDataCommon.m_dbHotBlower_Temperature.ToString();
            lbl_BlowerRunTimeout.Text = m_pDataCommon.m_lg_BlowerTimeout_Run.ToString();
            lbl_NextPalletTimeout.Text = m_pMachineInfo.m_int_HotBlower_NextPalletTimeout.ToString();
            lbl_CheckTempTimeout.Text = m_pMachineInfo.m_int_HotBlower_CheckTempTimeout.ToString();

            //HotBlower Point Selection
            Update_ButtonEnable(btn_HotBlower_Terminal1_Ceramic, m_pDataCommon.m_bHotBlower_Terminal1_Ceramic);
            Update_ButtonEnable(btn_HotBlower_Terminal2_Ceramic, m_pDataCommon.m_bHotBlower_Terminal2_Ceramic);
            Update_ButtonEnable(btn_HotBlower_Terminal3_Ceramic, m_pDataCommon.m_bHotBlower_Terminal3_Ceramic);
            Update_ButtonEnable(btn_HotBlower_Terminal4_Ceramic, m_pDataCommon.m_bHotBlower_Terminal4_Ceramic);
            Update_ButtonEnable(btn_HotBlower_Terminal5_Ceramic, m_pDataCommon.m_bHotBlower_Terminal5_Ceramic);
            Update_ButtonEnable(btn_HotBlower_Terminal6_Ceramic, m_pDataCommon.m_bHotBlower_Terminal6_Ceramic);

            Update_ButtonEnable(btn_HotBlower_Terminal1_Tip, m_pDataCommon.m_bHotBlower_Terminal1_Tip);
            Update_ButtonEnable(btn_HotBlower_Terminal2_Tip, m_pDataCommon.m_bHotBlower_Terminal2_Tip);
            Update_ButtonEnable(btn_HotBlower_Terminal3_Tip, m_pDataCommon.m_bHotBlower_Terminal3_Tip);
            Update_ButtonEnable(btn_HotBlower_Terminal4_Tip, m_pDataCommon.m_bHotBlower_Terminal4_Tip);
            Update_ButtonEnable(btn_HotBlower_Terminal5_Tip, m_pDataCommon.m_bHotBlower_Terminal5_Tip);
            Update_ButtonEnable(btn_HotBlower_Terminal6_Tip, m_pDataCommon.m_bHotBlower_Terminal6_Tip);

            Update_ButtonEnable(btn_HotBlower_PreHeat_1, m_pDataCommon.m_bHotBlower_PreHeat_1);
            Update_ButtonEnable(btn_HotBlower_PreHeat_2, m_pDataCommon.m_bHotBlower_PreHeat_2);
            Update_ButtonEnable(btn_HotBlower_PreHeat_3, m_pDataCommon.m_bHotBlower_PreHeat_3);
            Update_ButtonEnable(btn_HotBlower_PreHeat_4, m_pDataCommon.m_bHotBlower_PreHeat_4);
            Update_ButtonEnable(btn_HotBlower_PreHeat_5, m_pDataCommon.m_bHotBlower_PreHeat_5);
            Update_ButtonEnable(btn_HotBlower_PreHeat_6, m_pDataCommon.m_bHotBlower_PreHeat_6);

            Update_ButtonEnable(btn_HotBlower_PostHeat_1, m_pDataCommon.m_bHotBlower_PostHeat_1);
            Update_ButtonEnable(btn_HotBlower_PostHeat_2, m_pDataCommon.m_bHotBlower_PostHeat_2);
            Update_ButtonEnable(btn_HotBlower_PostHeat_3, m_pDataCommon.m_bHotBlower_PostHeat_3);
            Update_ButtonEnable(btn_HotBlower_PostHeat_4, m_pDataCommon.m_bHotBlower_PostHeat_4);
            Update_ButtonEnable(btn_HotBlower_PostHeat_5, m_pDataCommon.m_bHotBlower_PostHeat_5);
            Update_ButtonEnable(btn_HotBlower_PostHeat_6, m_pDataCommon.m_bHotBlower_PostHeat_6);
        }

        //Save Settings///////////////////////////////////////////////////////////////////
        public void Save_Settings()
        {
            m_pDataCommon.m_dbHotBlower_Temperature = int.Parse(lbl_HotBlowerTemperature.Text);
            m_pDataCommon.m_lg_BlowerTimeout_Run = long.Parse(lbl_BlowerRunTimeout.Text);
            m_pMachineInfo.m_int_HotBlower_NextPalletTimeout = int.Parse(lbl_NextPalletTimeout.Text);
            m_pMachineInfo.m_int_HotBlower_CheckTempTimeout = int.Parse(lbl_CheckTempTimeout.Text);

            //enable selection
            m_pDataCommon.m_bHotBlower_Terminal1_Ceramic = Get_Enable(btn_HotBlower_Terminal1_Ceramic);
            m_pDataCommon.m_bHotBlower_Terminal2_Ceramic = Get_Enable(btn_HotBlower_Terminal2_Ceramic);
            m_pDataCommon.m_bHotBlower_Terminal3_Ceramic = Get_Enable(btn_HotBlower_Terminal3_Ceramic);
            m_pDataCommon.m_bHotBlower_Terminal4_Ceramic = Get_Enable(btn_HotBlower_Terminal4_Ceramic);
            m_pDataCommon.m_bHotBlower_Terminal5_Ceramic = Get_Enable(btn_HotBlower_Terminal5_Ceramic);
            m_pDataCommon.m_bHotBlower_Terminal6_Ceramic = Get_Enable(btn_HotBlower_Terminal6_Ceramic);

            m_pDataCommon.m_bHotBlower_Terminal1_Tip = Get_Enable(btn_HotBlower_Terminal1_Tip);
            m_pDataCommon.m_bHotBlower_Terminal2_Tip = Get_Enable(btn_HotBlower_Terminal2_Tip);
            m_pDataCommon.m_bHotBlower_Terminal3_Tip = Get_Enable(btn_HotBlower_Terminal3_Tip);
            m_pDataCommon.m_bHotBlower_Terminal4_Tip = Get_Enable(btn_HotBlower_Terminal4_Tip);
            m_pDataCommon.m_bHotBlower_Terminal5_Tip = Get_Enable(btn_HotBlower_Terminal5_Tip);
            m_pDataCommon.m_bHotBlower_Terminal6_Tip = Get_Enable(btn_HotBlower_Terminal6_Tip);

            m_pDataCommon.m_bHotBlower_PreHeat_1 = Get_Enable(btn_HotBlower_PreHeat_1);
            m_pDataCommon.m_bHotBlower_PreHeat_2 = Get_Enable(btn_HotBlower_PreHeat_2);
            m_pDataCommon.m_bHotBlower_PreHeat_3 = Get_Enable(btn_HotBlower_PreHeat_3);
            m_pDataCommon.m_bHotBlower_PreHeat_4 = Get_Enable(btn_HotBlower_PreHeat_4);
            m_pDataCommon.m_bHotBlower_PreHeat_5 = Get_Enable(btn_HotBlower_PreHeat_5);
            m_pDataCommon.m_bHotBlower_PreHeat_6 = Get_Enable(btn_HotBlower_PreHeat_6);

            m_pDataCommon.m_bHotBlower_PostHeat_1 = Get_Enable(btn_HotBlower_PostHeat_1);
            m_pDataCommon.m_bHotBlower_PostHeat_2 = Get_Enable(btn_HotBlower_PostHeat_2);
            m_pDataCommon.m_bHotBlower_PostHeat_3 = Get_Enable(btn_HotBlower_PostHeat_3);
            m_pDataCommon.m_bHotBlower_PostHeat_4 = Get_Enable(btn_HotBlower_PostHeat_4);
            m_pDataCommon.m_bHotBlower_PostHeat_5 = Get_Enable(btn_HotBlower_PostHeat_5);
            m_pDataCommon.m_bHotBlower_PostHeat_6 = Get_Enable(btn_HotBlower_PostHeat_6);

            //save to pkg file
            m_pSeqMachineCtrl.MachineInfo_To_Registry();
            m_pSeqMachineCtrl.Pkg_To_Registry();
        }


        //Class Helper////////////////////////////////////////////////////////////////////
        //Update lable///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Label(Label obj, String strTitle)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Title(strTitle);
            dlg.Set_Label(strTitle);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
                obj.Text = dlg.Get_DataString();
        }


        //Button Events///////////////////////////////////////////////////////////////////
        private void btn_HotBlowerTemperature_Click(object sender, EventArgs e) { Update_Label(lbl_HotBlowerTemperature, btn_HotBlowerTemperature.Text); Save_Settings(); }
        private void btn_BlowerRunTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_BlowerRunTimeout, btn_BlowerRunTimeout.Text); Save_Settings(); }
        private void btn_NextPalletTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_NextPalletTimeout, btn_NextPalletTimeout.Text); Save_Settings(); }
        private void btn_CheckTempTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_CheckTempTimeout, btn_CheckTempTimeout.Text); Save_Settings(); }

        //HotBlower Point Selection
        private void btn_HotBlower_Terminal1_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal1_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal1_Ceramic)); Save_Settings(); }
        private void btn_HotBlower_Terminal2_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal2_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal2_Ceramic)); Save_Settings(); }
        private void btn_HotBlower_Terminal3_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal3_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal3_Ceramic)); Save_Settings(); }
        private void btn_HotBlower_Terminal4_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal4_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal4_Ceramic)); Save_Settings(); }
        private void btn_HotBlower_Terminal5_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal5_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal5_Ceramic)); Save_Settings(); }
        private void btn_HotBlower_Terminal6_Ceramic_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal6_Ceramic, Get_EnableInverse(btn_HotBlower_Terminal6_Ceramic)); Save_Settings(); }

        private void btn_HotBlower_Terminal1_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal1_Tip, Get_EnableInverse(btn_HotBlower_Terminal1_Tip)); Save_Settings(); }
        private void btn_HotBlower_Terminal2_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal2_Tip, Get_EnableInverse(btn_HotBlower_Terminal2_Tip)); Save_Settings(); }
        private void btn_HotBlower_Terminal3_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal3_Tip, Get_EnableInverse(btn_HotBlower_Terminal3_Tip)); Save_Settings(); }
        private void btn_HotBlower_Terminal4_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal4_Tip, Get_EnableInverse(btn_HotBlower_Terminal4_Tip)); Save_Settings(); }
        private void btn_HotBlower_Terminal5_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal5_Tip, Get_EnableInverse(btn_HotBlower_Terminal5_Tip)); Save_Settings(); }
        private void btn_HotBlower_Terminal6_Tip_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_Terminal6_Tip, Get_EnableInverse(btn_HotBlower_Terminal6_Tip)); Save_Settings(); }

        private void btn_HotBlower_PreHeat_1_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_1, Get_EnableInverse(btn_HotBlower_PreHeat_1)); Save_Settings(); }
        private void btn_HotBlower_PreHeat_2_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_2, Get_EnableInverse(btn_HotBlower_PreHeat_2)); Save_Settings(); }
        private void btn_HotBlower_PreHeat_3_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_3, Get_EnableInverse(btn_HotBlower_PreHeat_3)); Save_Settings(); }
        private void btn_HotBlower_PreHeat_4_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_4, Get_EnableInverse(btn_HotBlower_PreHeat_4)); Save_Settings(); }
        private void btn_HotBlower_PreHeat_5_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_5, Get_EnableInverse(btn_HotBlower_PreHeat_5)); Save_Settings(); }
        private void btn_HotBlower_PreHeat_6_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PreHeat_6, Get_EnableInverse(btn_HotBlower_PreHeat_6)); Save_Settings(); }

        private void btn_HotBlower_PostHeat_1_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_1, Get_EnableInverse(btn_HotBlower_PostHeat_1)); Save_Settings(); }
        private void btn_HotBlower_PostHeat_2_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_2, Get_EnableInverse(btn_HotBlower_PostHeat_2)); Save_Settings(); }
        private void btn_HotBlower_PostHeat_3_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_3, Get_EnableInverse(btn_HotBlower_PostHeat_3)); Save_Settings(); }
        private void btn_HotBlower_PostHeat_4_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_4, Get_EnableInverse(btn_HotBlower_PostHeat_4)); Save_Settings(); }
        private void btn_HotBlower_PostHeat_5_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_5, Get_EnableInverse(btn_HotBlower_PostHeat_5)); Save_Settings(); }
        private void btn_HotBlower_PostHeat_6_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_HotBlower_PostHeat_6, Get_EnableInverse(btn_HotBlower_PostHeat_6)); Save_Settings(); }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Save_Settings();
        }

        //Update Button Enable//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_ButtonEnable(Button obj, bool bEnable)
        {
            //remove all enabled and disabled text and trim all leading and ending white spaces
            String strText = obj.Text.Replace("Enabled", "").Replace("Disabled", "").Trim();

            if (bEnable)
            {
                obj.Text = strText + " Enabled";
                obj.BackColor = Color.LimeGreen;
            }
            else
            {
                obj.Text = strText + " Disabled";
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
    }
}