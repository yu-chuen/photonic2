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
    public partial class UserCtrl_Common_Settings : UserControl
    {
        //variables
        Data_Common m_DataCommon;
        Data_UnitTempStorage m_DataUnit;
        SeqMachineControl m_pSeqMachineCtrl;
        MachineInfo m_pMachineInfo;

        //constructor
        public UserCtrl_Common_Settings()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
            m_pMachineInfo = m_pSeqMachineCtrl.m_MachineInfo;
        }

        //dialog loaded
        private void UserCtrl_WireTesterSpecs_Dlg_Load(object sender, EventArgs e)
        {
            //delay settings
            lbl_DelayBeforeTriggerVisionOP85.Text = m_DataCommon.m_iDelay_BeforeTriggerVision_OP85.ToString();
            lbl_DelayBeforeTriggerVisionOP90.Text = m_DataCommon.m_iDelay_BeforeTriggerVision_OP90.ToString();
        }

        //update setting
        public void Update_Settings()
        {
            //delay settings
            m_DataCommon.m_iDelay_BeforeTriggerVision_OP85 = int.Parse(lbl_DelayBeforeTriggerVisionOP85.Text);
            m_DataCommon.m_iDelay_BeforeTriggerVision_OP90 = int.Parse(lbl_DelayBeforeTriggerVisionOP90.Text);

            //save to pkg file
            m_pSeqMachineCtrl.MachineInfo_To_Registry();
            m_pSeqMachineCtrl.Pkg_To_Registry();
        }

        //Class helper/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Display_InputBox(Label obj, String strLabel, int iSelection = 1)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();

            switch (iSelection)
            {
                default: dlg.Set_Integer(); break;
                case 2: dlg.Set_Double(); break;
            }

            dlg.Set_Label(strLabel);
            dlg.Set_Title(strLabel);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) obj.Text = dlg.Get_DataString();
            dlg.Dispose();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Update_Settings();
        }

        //delay settings
        private void btn_DelayBeforeTriggerVisionOP85_Click(object sender, EventArgs e) { Display_InputBox(lbl_DelayBeforeTriggerVisionOP85, btn_DelayBeforeTriggerVisionOP85.Text); Update_Settings(); }
        private void btn_DelayBeforeTriggerVisionOP90_Click(object sender, EventArgs e) { Display_InputBox(lbl_DelayBeforeTriggerVisionOP90, btn_DelayBeforeTriggerVisionOP90.Text); Update_Settings(); }
    }
}