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
            Update_Buttons  (btn_DryRun, m_SeqMachineCtrl.m_DataUnit.Flag_DryRun, false);
            Update_Buttons  (btn_EndLot, m_SeqMachineCtrl.m_DataUnit.Flag_EndLot, false);

			Update_Buttons ( btn_SolderStationEnable_1,				m_DataUnit.Flag_EnableSolderStation1_b, false);
			Update_Buttons ( btn_SolderStationEnable_2,				m_DataUnit.Flag_EnableSolderStation2_b, false);
			Update_Buttons ( btn_SolderStationEnable_3,				m_DataUnit.Flag_EnableSolderStation3_b, false);
			Update_Buttons ( btn_EnableVisionInspection,			m_DataUnit.Flag_EnableVisionInspection_b, false);
			Update_Buttons ( btn_EnableManualOP100,					m_DataUnit.Flag_EnableManualOP100_b, false);
            Update_Buttons(btn_EnableManualOP120, m_DataUnit.Flag_EnableManualOP120_b, false);
            Update_Buttons(btn_EnableManualOP125, m_DataUnit.Flag_EnableManualOP125_b, false);
			Update_Buttons ( btn_EnableHotBlower,					m_DataUnit.Flag_EnableHotBlower_b, false);

			Update_Buttons ( btn_EnableConvSolderStation,			m_DataUnit.Flag_EnableConvSolder_b, false);
			Update_Buttons ( btn_EnableConvVisionInspection,		m_DataUnit.Flag_EnableConvVisionInspection_b, false);
			Update_Buttons ( btn_EnableConvOP120nOP110,			    m_DataUnit.Flag_EnableConvOP110nOP120_b, false);

            Update_Buttons(btn_SubSeq_ServerTest1, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder1, false);
            Update_Buttons(btn_SubSeq_ServerTest2, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder2, false);
            Update_Buttons(btn_SubSeq_ServerTest3, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder3, false);

            Update_Buttons(btn_EnableReverseSeq, m_DataUnit.Flag_EnablereverseSeq_b, false);
            Update_Buttons(btn_EnableSolder1Rotate, m_DataUnit.Flag_EnableSolder1Rotate, false);
			Update_Buttons(btn_EnableCleanSolderHead, m_DataUnit.Flag_EnableCleanSolderHead, false);
			Update_Buttons(btn_ByPassOP100, m_DataUnit.Flag_ByPassOP100_b, false);
            Update_Buttons(btn_EnableDoubleSoldering, m_DataUnit.Flag_EnableDoubleSoldering_b, false);
            Update_Buttons(btn_EnableSolderSelection, m_DataUnit.Flag_EnableSolderSelection_b, false);
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

		//Dialog Closing///////////////////////////////////////////////////////////////////////////////////////////////////////
		private void ChangeProcessFlag_Dialog_FormClosing( object sender, FormClosingEventArgs e )
		{
			//m_SeqMachineCtrl.MachineInfo_To_Registry();
		}

        //Button Events///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            Main_Dlg dlg = GT_App.Program.m_MainDialog;
            dlg.Update_UserCtrlUI();

            this.Close();
        }
        //dry run
        private void btn_DryRun_Click(object sender, EventArgs e)
        {
            if (m_SeqMachineCtrl.m_DataUnit.Flag_DryRun == false)
                m_SeqMachineCtrl.m_DataUnit.Flag_DryRun = true;
            else
                m_SeqMachineCtrl.m_DataUnit.Flag_DryRun = false;

            Update_Buttons(btn_DryRun, m_SeqMachineCtrl.m_DataUnit.Flag_DryRun);
        }

		private void btn_SolderStationEnable_1_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation1_b = ( m_DataUnit.Flag_EnableSolderStation1_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_1, m_DataUnit.Flag_EnableSolderStation1_b);
		}

		private void btn_SolderStationEnable_2_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation2_b = ( m_DataUnit.Flag_EnableSolderStation2_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_2, m_DataUnit.Flag_EnableSolderStation2_b);
		}

		private void btn_SolderStationEnable_3_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableSolderStation3_b = ( m_DataUnit.Flag_EnableSolderStation3_b == true ) ? false : true;
			Update_Buttons(btn_SolderStationEnable_3, m_DataUnit.Flag_EnableSolderStation3_b);
		}

		private void btn_EnableVisionInspection_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableVisionInspection_b = ( m_DataUnit.Flag_EnableVisionInspection_b == true ) ? false : true;
			Update_Buttons(btn_EnableVisionInspection, m_DataUnit.Flag_EnableVisionInspection_b);
		}

		private void btn_EnableManualOP100_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableManualOP100_b = ( m_DataUnit.Flag_EnableManualOP100_b == true ) ? false : true;
			Update_Buttons(btn_EnableManualOP100, m_DataUnit.Flag_EnableManualOP100_b);
		}

		private void btn_EnableManualOP120_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableManualOP120_b = ( m_DataUnit.Flag_EnableManualOP120_b == true ) ? false : true;
			Update_Buttons(btn_EnableManualOP120, m_DataUnit.Flag_EnableManualOP120_b);
		}

		private void btn_EnableHotBlower_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableHotBlower_b = ( m_DataUnit.Flag_EnableHotBlower_b == true ) ? false : true;
			Update_Buttons(btn_EnableHotBlower, m_DataUnit.Flag_EnableHotBlower_b);
		}

		private void btn_EnableConvSolderStation_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableConvSolder_b = ( m_DataUnit.Flag_EnableConvSolder_b == true ) ? false : true;
			Update_Buttons(btn_EnableConvSolderStation, m_DataUnit.Flag_EnableConvSolder_b);
		}

		private void btn_EnableConvVisionInspection_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableConvVisionInspection_b = ( m_DataUnit.Flag_EnableConvVisionInspection_b == true ) ? false : true;
			Update_Buttons(btn_EnableConvVisionInspection, m_DataUnit.Flag_EnableConvVisionInspection_b);
		}

		private void btn_EnableConvOP120nOP110_Click( object sender, EventArgs e )
		{
			m_DataUnit.Flag_EnableConvOP110nOP120_b = ( m_DataUnit.Flag_EnableConvOP110nOP120_b == true ) ? false : true;
			Update_Buttons(btn_EnableConvOP120nOP110, m_DataUnit.Flag_EnableConvOP110nOP120_b);
		}


		//Display instruction for sub seq test
		private void Displa_SubSeqTestMsgBox()
		{
			String strMsg = "Please make sure of the following items: \r\n" ;
			strMsg += "1. Server ProcessData table has the record for Dummy_Test1 & 2 & 3.\r\n ";
			strMsg += "2. Dry run is Off.";

			Class_Helper.Display_NoticeMessageBox(strMsg);
		}


		private void btn_SubSeq_ServerTest1_Click( object sender, EventArgs e )
		{
			m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder1 = ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder1 == true ) ? false : true;
			Update_Buttons(btn_SubSeq_ServerTest1, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder1);

			if ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder1 ) Displa_SubSeqTestMsgBox();
		}

		private void btn_SubSeq_ServerTest2_Click( object sender, EventArgs e )
		{
			m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder2 = ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder2 == true ) ? false : true;
			Update_Buttons(btn_SubSeq_ServerTest2, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder2);

			if ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder2 ) Displa_SubSeqTestMsgBox();
		}

		private void btn_SubSeq_ServerTest3_Click( object sender, EventArgs e )
		{
			m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder3 = ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder3 == true ) ? false : true;
			Update_Buttons(btn_SubSeq_ServerTest3, m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder3);

			if ( m_pMachineInfo.m_bFlagEnable_SubSeqServerTest_Solder3 ) Displa_SubSeqTestMsgBox();
		}

        private void btn_EndLot_Click(object sender, EventArgs e)
        {
            if (m_SeqMachineCtrl.m_DataUnit.Flag_EndLot == false)
                m_SeqMachineCtrl.m_DataUnit.Flag_EndLot = true;
            else
                m_SeqMachineCtrl.m_DataUnit.Flag_EndLot = false;

            Update_Buttons(btn_EndLot, m_SeqMachineCtrl.m_DataUnit.Flag_EndLot);
        }

        private void btn_EnableReverseSeq_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnablereverseSeq_b = (m_DataUnit.Flag_EnablereverseSeq_b == true) ? false : true;
            Update_Buttons(btn_EnableReverseSeq, m_DataUnit.Flag_EnablereverseSeq_b);
        }

        private void btn_EnableSolder1Rotate_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableSolder1Rotate = (m_DataUnit.Flag_EnableSolder1Rotate == true) ? false : true;
            Update_Buttons(btn_EnableSolder1Rotate, m_DataUnit.Flag_EnableSolder1Rotate);
        }

        private void btn_EnableCleanSolderHead_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableCleanSolderHead = (m_DataUnit.Flag_EnableCleanSolderHead == true) ? false : true;
            Update_Buttons(btn_EnableCleanSolderHead, m_DataUnit.Flag_EnableCleanSolderHead);
        }

		private void btn_ByPassOP100_Click( object sender, EventArgs e )
		{
			if ( m_DataUnit.Flag_ByPassOP100_b == true )
				m_DataUnit.Flag_ByPassOP100_b = false;
			else
				m_DataUnit.Flag_ByPassOP100_b = true;
			Update_Buttons(btn_ByPassOP100, m_DataUnit.Flag_ByPassOP100_b, true);
		}

        private void btn_EnableDoubleSoldering_Click(object sender, EventArgs e)
        {
            if (m_DataUnit.Flag_EnableDoubleSoldering_b == false)
                m_DataUnit.Flag_EnableDoubleSoldering_b = true;
            else
                m_DataUnit.Flag_EnableDoubleSoldering_b = false;
            Update_Buttons(btn_EnableDoubleSoldering, m_DataUnit.Flag_EnableDoubleSoldering_b);
        }

        private void btn_EnableSolderSelection_Click(object sender, EventArgs e)
        {
            if (m_DataUnit.Flag_EnableSolderSelection_b == false)
                m_DataUnit.Flag_EnableSolderSelection_b = true;
            else
                m_DataUnit.Flag_EnableSolderSelection_b = false;
            Update_Buttons(btn_EnableSolderSelection, m_DataUnit.Flag_EnableSolderSelection_b);
        }

        private void btn_ManualStationOP125_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_EnableManualOP125_b = (m_DataUnit.Flag_EnableManualOP125_b == true) ? false : true;
            Update_Buttons(btn_EnableManualOP125, m_DataUnit.Flag_EnableManualOP125_b);
        }






        


        


    }
}
