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
    public partial class UserCtrl_CommonData : UserControl
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl;
        Data_Common m_pDataCommon;

        //constructor/////////////////////////////////////////////////////////////////////
        public UserCtrl_CommonData()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pDataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////
        private void UserCtrl_CommonData_Load(object sender, EventArgs e)
        {
            lbl_HotBlowerTemperature.Text = m_pDataCommon.m_dbHotBlower_Temperature.ToString();
            lbl_BlowerRunTimeout.Text = m_pDataCommon.m_lg_BlowerTimeout_Run.ToString();

            //Blower point selection
            Update_ButtonEnable(btn_BlowerPoint1, m_pDataCommon.m_bBlower_Point_1);
            Update_ButtonEnable(btn_BlowerPoint2, m_pDataCommon.m_bBlower_Point_2);
            Update_ButtonEnable(btn_BlowerPoint3, m_pDataCommon.m_bBlower_Point_3);
            Update_ButtonEnable(btn_BlowerPoint4, m_pDataCommon.m_bBlower_Point_4);
            Update_ButtonEnable(btn_BlowerPoint5, m_pDataCommon.m_bBlower_Point_5);
            Update_ButtonEnable(btn_BlowerPoint6, m_pDataCommon.m_bBlower_Point_6);
            Update_ButtonEnable(btn_BlowerPoint7, m_pDataCommon.m_bBlower_Point_7);
            Update_ButtonEnable(btn_BlowerPoint8, m_pDataCommon.m_bBlower_Point_8);
            Update_ButtonEnable(btn_BlowerPoint9, m_pDataCommon.m_bBlower_Point_9);
            Update_ButtonEnable(btn_BlowerPoint10, m_pDataCommon.m_bBlower_Point_10);
            Update_ButtonEnable(btn_BlowerPoint11, m_pDataCommon.m_bBlower_Point_11);
            Update_ButtonEnable(btn_BlowerPoint12, m_pDataCommon.m_bBlower_Point_12);

        }

        //Get Settings///////////////////////////////////////////////////////////////////
        public void Get_Settings()
        {
            m_pDataCommon.m_dbHotBlower_Temperature = int.Parse(lbl_HotBlowerTemperature.Text);
            m_pDataCommon.m_lg_BlowerTimeout_Run = long.Parse(lbl_BlowerRunTimeout.Text);

            //enable selection
            m_pDataCommon.m_bBlower_Point_1 = Get_Enable(btn_BlowerPoint1);
            m_pDataCommon.m_bBlower_Point_2 = Get_Enable(btn_BlowerPoint2);
            m_pDataCommon.m_bBlower_Point_3 = Get_Enable(btn_BlowerPoint3);
            m_pDataCommon.m_bBlower_Point_4 = Get_Enable(btn_BlowerPoint4);
            m_pDataCommon.m_bBlower_Point_5 = Get_Enable(btn_BlowerPoint5);
            m_pDataCommon.m_bBlower_Point_6 = Get_Enable(btn_BlowerPoint6);
            m_pDataCommon.m_bBlower_Point_7 = Get_Enable(btn_BlowerPoint7);
            m_pDataCommon.m_bBlower_Point_8 = Get_Enable(btn_BlowerPoint8);
            m_pDataCommon.m_bBlower_Point_9 = Get_Enable(btn_BlowerPoint9);
            m_pDataCommon.m_bBlower_Point_10 = Get_Enable(btn_BlowerPoint10);
            m_pDataCommon.m_bBlower_Point_11 = Get_Enable(btn_BlowerPoint11);
            m_pDataCommon.m_bBlower_Point_12 = Get_Enable(btn_BlowerPoint12);

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
        private void btn_HotBlowerTemperature_Click(object sender, EventArgs e) { Update_Label(lbl_HotBlowerTemperature, btn_HotBlowerTemperature.Text); }

        //Blower selection
        private void btn_BlowerPoint1_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint1, Get_EnableInverse(btn_BlowerPoint1)); }
        private void btn_BlowerPoint2_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint2, Get_EnableInverse(btn_BlowerPoint2)); }
        private void btn_BlowerPoint3_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint3, Get_EnableInverse(btn_BlowerPoint3)); }
        private void btn_BlowerPoint4_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint4, Get_EnableInverse(btn_BlowerPoint4)); }
        private void btn_BlowerPoint5_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint5, Get_EnableInverse(btn_BlowerPoint5)); }
        private void btn_BlowerPoint6_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint6, Get_EnableInverse(btn_BlowerPoint6)); }
        private void btn_BlowerPoint7_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint7, Get_EnableInverse(btn_BlowerPoint7)); }
        private void btn_BlowerPoint8_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint8, Get_EnableInverse(btn_BlowerPoint8)); }
        private void btn_BlowerPoint9_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint9, Get_EnableInverse(btn_BlowerPoint9)); }
        private void btn_BlowerPoint10_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint10, Get_EnableInverse(btn_BlowerPoint10)); }
        private void btn_BlowerPoint11_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint11, Get_EnableInverse(btn_BlowerPoint11)); }
        private void btn_BlowerPoint12_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_BlowerPoint12, Get_EnableInverse(btn_BlowerPoint12)); }
        private void btn_BlowerRunTimeout_Click(object sender, EventArgs e) { Update_Label(lbl_BlowerRunTimeout, btn_BlowerRunTimeout.Text); }


        private void btn_Update_Click(object sender, EventArgs e)
        {
            Get_Settings();
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
