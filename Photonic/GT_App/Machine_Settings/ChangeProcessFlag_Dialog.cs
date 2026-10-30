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
    public partial class ChangeProcessFlag_Dialog : Form
    {
        //Variables
        SeqMachineControl m_SeqMachineCtrl;
		Data_UnitTempStorage m_DataUnit;
		MachineInfo m_pMachineInfo;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public ChangeProcessFlag_Dialog()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
			m_pMachineInfo = m_SeqMachineCtrl.m_MachineInfo;
        }

        //Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void ChangeProcessFlag_Dialog_Load(object sender, EventArgs e)
        {
            Update_UI();
        }

        private void Update_UI()
        {
            Update_Buttons(btn_DryRun, m_SeqMachineCtrl.m_DataUnit.Flag_DryRun, false);
            Update_Buttons(btn_EndLot, m_SeqMachineCtrl.m_DataUnit.Flag_EndLot, false);
            Update_Buttons(btn_TestMode, m_DataUnit.Flag_Enable_TestMode_b, false);

            Update_Buttons(btn_SolderStationEnable_1, m_DataUnit.Flag_EnableSolderStation1_b, false);
            Update_Buttons(btn_SolderStationEnable_2, m_DataUnit.Flag_EnableSolderStation2_b, false);
            Update_Buttons(btn_SolderStationEnable_3, m_DataUnit.Flag_EnableSolderStation3_b, false);
            Update_Buttons(btn_EnableConvSolderStation, m_DataUnit.Flag_EnableConvSolder_b, false);

            Update_Buttons(btn_EnableModule_OP85, m_DataUnit.Flag_Enable_Module_OP85_b, false);
            Update_Buttons(btn_EnableModuleConv_OP85, m_DataUnit.Flag_Enable_Module_Conv_OP85_b, false);
            Update_Buttons(btn_EnableModule_OP90, m_DataUnit.Flag_Enable_Module_OP90_b, false);
            Update_Buttons(btn_EnableModuleConv_OP90, m_DataUnit.Flag_Enable_Module_Conv_OP90_b, false);
            Update_Buttons(btn_EnableModuleFwdConv_OP85_OP90, m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b, false);

            Update_Buttons(btn_EnableManualOP100, m_DataUnit.Flag_EnableManualOP100_b, false);

            Update_Buttons(btn_EnableModule_OP110, m_DataUnit.Flag_Enable_Module_OP110_b, false);
            Update_Buttons(btn_EnableModuleConv_OP110, m_DataUnit.Flag_Enable_Module_Conv_OP110_b, false);
            Update_Buttons(btn_EnableModuleFwdConv_OP110, m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b, false);

            Update_Buttons(btn_EnableReverseSeq, m_DataUnit.Flag_EnableReverseSeq_b, false);
            Update_Buttons(btn_EnableCleanSolderHead, m_DataUnit.Flag_EnableCleanSolderHead, false);
            Update_Buttons(btn_ByPassOP100, m_DataUnit.Flag_Bypass_OP100_b, false);
            Update_Buttons(btn_Bypass_OP110_AutoPNPSpringComb, m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b, false);

            m_SeqMachineCtrl.MachineInfo_To_Registry();
        }

        //Button Event///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_Buttons(Button mObj, bool bActivated, bool bLog=true)
        {
            String strActivate = " Activated", strDeactivated = " Deactivated";

            //Remove back text - make it original
            mObj.Text = mObj.Text.Replace(strActivate, "");
            mObj.Text = mObj.Text.Replace(strDeactivated, "");

            //Set button
            if (bActivated == true)
            {
                mObj.Text = mObj.Text + strActivate;
                mObj.BackColor = Color.LightGreen;
            }
            else
            {
                mObj.Text = mObj.Text + strDeactivated;
                mObj.BackColor = Color.LightGray;
            }

            //log activity
            if (bLog) m_SeqMachineCtrl.SetLog("ChangeProcessFlag activity Logger: " + mObj.Text);
        }

        //Button Events///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            Main_Dlg dlg = GT_App.Program.m_MainDialog;
            dlg.Update_UserCtrlUI();

            this.Close();
        }

        private void btn_DryRun_Click(object sender, EventArgs e)
        {
            if (m_SeqMachineCtrl.m_DataUnit.Flag_DryRun == false)
                m_SeqMachineCtrl.m_DataUnit.Flag_DryRun = true;
            else
                m_SeqMachineCtrl.m_DataUnit.Flag_DryRun = false;

            Update_Buttons(btn_DryRun, m_SeqMachineCtrl.m_DataUnit.Flag_DryRun);

            Update_UI();
        }

        private void btn_EndLot_Click(object sender, EventArgs e)
        {
            if (m_SeqMachineCtrl.m_DataUnit.Flag_EndLot == false)
                m_SeqMachineCtrl.m_DataUnit.Flag_EndLot = true;
            else
                m_SeqMachineCtrl.m_DataUnit.Flag_EndLot = false;

            Update_Buttons(btn_EndLot, m_SeqMachineCtrl.m_DataUnit.Flag_EndLot);

            Update_UI();
        }

		private void btn_SolderStationEnable_1_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation1_b = ( m_DataUnit.Flag_EnableSolderStation1_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_1, m_DataUnit.Flag_EnableSolderStation1_b);

            Update_UI();
		}

		private void btn_SolderStationEnable_2_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation2_b = ( m_DataUnit.Flag_EnableSolderStation2_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_2, m_DataUnit.Flag_EnableSolderStation2_b);

            Update_UI();
		}

		private void btn_SolderStationEnable_3_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation3_b = ( m_DataUnit.Flag_EnableSolderStation3_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_3, m_DataUnit.Flag_EnableSolderStation3_b);

            Update_UI();
		}

        private void btn_EnableConvSolderStation_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableConvSolder_b = (m_DataUnit.Flag_EnableConvSolder_b == true) ? false : true;
            Update_Buttons(btn_EnableConvSolderStation, m_DataUnit.Flag_EnableConvSolder_b);

            Update_UI();
        }

        private void btn_EnableModule_OP85_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_OP85_b = (m_DataUnit.Flag_Enable_Module_OP85_b == true) ? false : true;
            Update_Buttons(btn_EnableModule_OP85, m_DataUnit.Flag_Enable_Module_OP85_b);

            Update_UI();
        }

        private void btn_EnableModuleConv_OP85_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_Conv_OP85_b = (m_DataUnit.Flag_Enable_Module_Conv_OP85_b == true) ? false : true;
            Update_Buttons(btn_EnableModuleConv_OP85, m_DataUnit.Flag_Enable_Module_Conv_OP85_b);

            Update_UI();
        }

        private void btn_EnableModule_OP90_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_OP90_b = (m_DataUnit.Flag_Enable_Module_OP90_b == true) ? false : true;
            Update_Buttons(btn_EnableModule_OP90, m_DataUnit.Flag_Enable_Module_OP90_b);

            Update_UI();
        }

        private void btn_EnableModuleConv_OP90_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_Conv_OP90_b = (m_DataUnit.Flag_Enable_Module_Conv_OP90_b == true) ? false : true;
            Update_Buttons(btn_EnableModuleConv_OP90, m_DataUnit.Flag_Enable_Module_Conv_OP90_b);

            Update_UI();
        }

        private void btn_EnableModuleFwdConv_OP85_OP90_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b = (m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b == true) ? false : true;
            Update_Buttons(btn_EnableModuleFwdConv_OP85_OP90, m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b);

            Update_UI();
        }

		private void btn_EnableManualOP100_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableManualOP100_b = ( m_DataUnit.Flag_EnableManualOP100_b == true ) ? false : true;
			Update_Buttons(btn_EnableManualOP100, m_DataUnit.Flag_EnableManualOP100_b);

            Update_UI();
		}

        private void btn_EnableHotBlower_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_OP110_b = (m_DataUnit.Flag_Enable_Module_OP110_b == true) ? false : true;
            Update_Buttons(btn_EnableModule_OP110, m_DataUnit.Flag_Enable_Module_OP110_b);

            Update_UI();
        }

        private void btn_EnableModuleConv_OP110_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_Conv_OP110_b = (m_DataUnit.Flag_Enable_Module_Conv_OP110_b == true) ? false : true;
            Update_Buttons(btn_EnableModuleConv_OP110, m_DataUnit.Flag_Enable_Module_Conv_OP110_b);

            Update_UI();
        }

        private void btn_EnableModuleFwdConv_OP110_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b = (m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b == true) ? false : true;
            Update_Buttons(btn_EnableModuleFwdConv_OP110, m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b);

            Update_UI();
        }

		//Display instruction for sub seq test
		private void Displa_SubSeqTestMsgBox()
		{
			String strMsg = "Please make sure of the following items: \r\n" ;
			strMsg += "1. Server ProcessData table has the record for Dummy_Test1 & 2 & 3.\r\n ";
			strMsg += "2. Dry run is Off.";

			Class_Helper.Display_NoticeMessageBox(strMsg);
		}

        private void btn_EnableReverseSeq_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableReverseSeq_b = (m_DataUnit.Flag_EnableReverseSeq_b == true) ? false : true;
            Update_Buttons(btn_EnableReverseSeq, m_DataUnit.Flag_EnableReverseSeq_b);

            Update_UI();
        }

        private void btn_EnableCleanSolderHead_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableCleanSolderHead = (m_DataUnit.Flag_EnableCleanSolderHead == true) ? false : true;
            Update_Buttons(btn_EnableCleanSolderHead, m_DataUnit.Flag_EnableCleanSolderHead);

            Update_UI();
        }

        private void btn_ByPassOP100_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Bypass_OP100_b = (m_DataUnit.Flag_Bypass_OP100_b == true) ? false : true;
            Update_Buttons(btn_ByPassOP100, m_DataUnit.Flag_Bypass_OP100_b, true);

            Update_UI();
        }

        private void btn_TestMode_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_TestMode_b = (m_DataUnit.Flag_Enable_TestMode_b == true) ? false : true;
            Update_Buttons(btn_TestMode, m_DataUnit.Flag_Enable_TestMode_b);

            Update_UI();
        }

        private void btn_Bypass_OP110_AutoPNPSpringComb_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b = (m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b == true) ? false : true;
            Update_Buttons(btn_Bypass_OP110_AutoPNPSpringComb, m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b);

            Update_UI();
        }
    }
}