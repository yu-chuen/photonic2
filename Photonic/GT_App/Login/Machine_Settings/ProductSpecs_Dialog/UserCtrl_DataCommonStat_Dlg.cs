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
	public partial class UserCtrl_DataCommonStat_Dlg : UserControl
	{
		//variables
		Data_Common_Stat m_pDataCommonStat;
        SeqMachineControl m_pSeqMachineCtrl;

		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_DataCommonStat_Dlg(Data_Common_Stat pDataCommonStat, String strLabel)
		{
			InitializeComponent();
			m_pDataCommonStat = pDataCommonStat;
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            lbl_solder_program.Text = strLabel;
		}

        //Set Data robot package pointer////////////////////////////////////////////////////////////////////////////////////
        public void Set_DataRobot_Ptr(Data_Common_Stat mRobotData) { m_pDataCommonStat = mRobotData; }


		//dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_DataCommonStat_Dlg_Load( object sender, EventArgs e )
		{
			lbl_iSolderProg_P1.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_1.ToString();
			lbl_iSolderProg_P2.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_2.ToString();
			lbl_iSolderProg_P3.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_3.ToString();
			lbl_iSolderProg_P4.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_4.ToString();
			lbl_iSolderProg_P5.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_5.ToString();
			lbl_iSolderProg_P6.Text   = m_pDataCommonStat.m_iSolder_Program_Pin_6.ToString();

            lbl_iSolderProg_P1_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_1_A.ToString();         //added 20200914
            lbl_iSolderProg_P2_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_2_A.ToString();
            lbl_iSolderProg_P3_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_3_A.ToString();
            lbl_iSolderProg_P4_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_4_A.ToString();
            lbl_iSolderProg_P5_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_5_A.ToString();
            lbl_iSolderProg_P6_A.Text = m_pDataCommonStat.m_iSolder_Program_Pin_6_A.ToString();

			lbl_SolderSleepTime.Text	    = m_pDataCommonStat.m_lg_SolderStopTime_Max.ToString();
			lbl_FumExtractorOffTime.Text	= m_pDataCommonStat.m_lg_FumeOffTime_Max.ToString();
			lbl_SolderStartTimeOut.Text		= m_pDataCommonStat.m_lg_SolderTimeout_Start.ToString();
			lbl_SolderRunTimeout.Text		= m_pDataCommonStat.m_lg_SolderTimeout_Run.ToString();
            lbl_Solder_Ready_Timeout.Text   = m_pDataCommonStat.m_lg_Solder_Ready_Timeout.ToString();
			lbl_SolderCleanTime.Text		= m_pDataCommonStat.m_lgCleaning_Time.ToString();

			lbl_IronHeadMaxUseCount.Text	        = m_pDataCommonStat.m_iIronHead_UseCount.ToString();
            lbl_CurrentSolderIronHeadUseCount.Text  = m_pDataCommonStat.m_iCurrentIronHead_UseCount.ToString();
			lbl_CleanCounterThreshold.Text          = m_pDataCommonStat.m_iCleanCount.ToString();
			lbl_CleanUpOffset.Text			        = m_pDataCommonStat.m_dbCleaningOffset_Up.ToString();
			lbl_CleanDownOffset.Text		        = m_pDataCommonStat.m_dbCleaningOffset_Dwn.ToString();

            //solder point selection
            Update_ButtonEnable(btn_SolderPoint1, m_pDataCommonStat.m_bSolder_Point_1);
            Update_ButtonEnable(btn_SolderPoint2, m_pDataCommonStat.m_bSolder_Point_2);
            Update_ButtonEnable(btn_SolderPoint3, m_pDataCommonStat.m_bSolder_Point_3);
            Update_ButtonEnable(btn_SolderPoint4, m_pDataCommonStat.m_bSolder_Point_4);
            Update_ButtonEnable(btn_SolderPoint5, m_pDataCommonStat.m_bSolder_Point_5);
            Update_ButtonEnable(btn_SolderPoint6, m_pDataCommonStat.m_bSolder_Point_6);

            //offset setting for X
            lbl_SolderPoint1_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint1.ToString();
            lbl_SolderPoint1_X_NoRotate.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint1_NoRotation.ToString();
            lbl_SolderPoint2_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint2.ToString();
            lbl_SolderPoint3_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint3.ToString();
            lbl_SolderPoint4_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint4.ToString();
            lbl_SolderPoint5_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint5.ToString();
            lbl_SolderPoint6_X.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint6.ToString();

            //offset setting for X_A          //added 20200914
            lbl_SolderPoint1_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint1_A.ToString();
            lbl_SolderPoint1_X_A_NoRotate.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint1_NoRotation_A.ToString();
            lbl_SolderPoint2_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint2_A.ToString();
            lbl_SolderPoint3_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint3_A.ToString();
            lbl_SolderPoint4_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint4_A.ToString();
            lbl_SolderPoint5_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint5_A.ToString();
            lbl_SolderPoint6_X_A.Text = m_pDataCommonStat.m_dbOffset_X_SolderPoint6_A.ToString();

            //offset setting for Y
            lbl_SolderPoint1_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint1.ToString();
            lbl_SolderPoint1_Y_NoRotate.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_NoRotation.ToString();
            lbl_SolderPoint2_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint2.ToString();
            lbl_SolderPoint3_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint3.ToString();
            lbl_SolderPoint4_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint4.ToString();
            lbl_SolderPoint5_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint5.ToString();
            lbl_SolderPoint6_Y.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint6.ToString();

            //offset setting for Y_A            //added 20200914
            lbl_SolderPoint1_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_A.ToString();
            lbl_SolderPoint1_Y_A_NoRotate.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_NoRotation_A.ToString();
            lbl_SolderPoint2_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint2_A.ToString();
            lbl_SolderPoint3_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint3_A.ToString();
            lbl_SolderPoint4_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint4_A.ToString();
            lbl_SolderPoint5_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint5_A.ToString();
            lbl_SolderPoint6_Y_A.Text = m_pDataCommonStat.m_dbOffset_Y_SolderPoint6_A.ToString();

            //offset setting for Z
            lbl_SolderPoint1_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint1.ToString();
            lbl_SolderPoint1_Z_NoRotate.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_NoRotation.ToString();
            lbl_SolderPoint2_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint2.ToString();
            lbl_SolderPoint3_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint3.ToString();
            lbl_SolderPoint4_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint4.ToString();
            lbl_SolderPoint5_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint5.ToString();
            lbl_SolderPoint6_Z.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint6.ToString();

            //offset setting for Z_A
            lbl_SolderPoint1_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_A.ToString();
            lbl_SolderPoint1_Z_A_NoRotate.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_NoRotation_A.ToString();
            lbl_SolderPoint2_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint2_A.ToString();
            lbl_SolderPoint3_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint3_A.ToString();
            lbl_SolderPoint4_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint4_A.ToString();
            lbl_SolderPoint5_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint5_A.ToString();
            lbl_SolderPoint6_Z_A.Text = m_pDataCommonStat.m_dbOffset_Z_SolderPoint6_A.ToString();

            //Cleaning setting 
            lbl_iSetProgClean.Text = m_pDataCommonStat.m_iSolder_Program_Cleaning.ToString();

            //Spec file point to point difference
            lbl_SpecOffset_X_1.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint1.ToString();
            lbl_SpecOffset_X_2.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint2.ToString();
            lbl_SpecOffset_X_3.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint3.ToString();
            lbl_SpecOffset_X_4.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint4.ToString();
            lbl_SpecOffset_X_5.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint5.ToString();
            lbl_SpecOffset_X_6.Text = m_pDataCommonStat.m_dbSpec_X_PinPoint6.ToString();

            lbl_SpecOffset_Y_1.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint1.ToString();
            lbl_SpecOffset_Y_2.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint2.ToString();
            lbl_SpecOffset_Y_3.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint3.ToString();
            lbl_SpecOffset_Y_4.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint4.ToString();
            lbl_SpecOffset_Y_5.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint5.ToString();
            lbl_SpecOffset_Y_6.Text = m_pDataCommonStat.m_dbSpec_Y_PinPoint6.ToString();

            lbl_SpecOffset_Z_1.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint1.ToString(); //david
            lbl_SpecOffset_Z_2.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint2.ToString(); //david
            lbl_SpecOffset_Z_3.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint3.ToString(); //david
            lbl_SpecOffset_Z_4.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint4.ToString(); //david
            lbl_SpecOffset_Z_5.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint5.ToString(); //david
            lbl_SpecOffset_Z_6.Text = m_pDataCommonStat.m_dbSpec_Z_PinPoint6.ToString(); //david

            //Datum Offset
            lbl_DatumOffset_X.Text = m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_X.ToString();
            lbl_DatumOffset_Y.Text = m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_Y.ToString();
            lbl_DatumOffset_Z.Text = m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_Z.ToString(); //david
		}

		//Get Settings////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Get_Settings()
        {
            m_pDataCommonStat.m_iSolder_Program_Pin_1 = int.Parse(lbl_iSolderProg_P1.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_2 = int.Parse(lbl_iSolderProg_P2.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_3 = int.Parse(lbl_iSolderProg_P3.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_4 = int.Parse(lbl_iSolderProg_P4.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_5 = int.Parse(lbl_iSolderProg_P5.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_6 = int.Parse(lbl_iSolderProg_P6.Text);
            
            //Double Soldering
            m_pDataCommonStat.m_iSolder_Program_Pin_1_A = int.Parse(lbl_iSolderProg_P1_A.Text);         //added 20200914
            m_pDataCommonStat.m_iSolder_Program_Pin_2_A = int.Parse(lbl_iSolderProg_P2_A.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_3_A = int.Parse(lbl_iSolderProg_P3_A.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_4_A = int.Parse(lbl_iSolderProg_P4_A.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_5_A = int.Parse(lbl_iSolderProg_P5_A.Text);
            m_pDataCommonStat.m_iSolder_Program_Pin_6_A = int.Parse(lbl_iSolderProg_P6_A.Text);

            m_pDataCommonStat.m_lg_SolderStopTime_Max    = long.Parse(lbl_SolderSleepTime.Text);
            m_pDataCommonStat.m_lg_FumeOffTime_Max       = long.Parse(lbl_FumExtractorOffTime.Text);
            m_pDataCommonStat.m_lg_SolderTimeout_Start   = long.Parse(lbl_SolderStartTimeOut.Text);
            m_pDataCommonStat.m_lg_SolderTimeout_Run     = long.Parse(lbl_SolderRunTimeout.Text);
            m_pDataCommonStat.m_lg_Solder_Ready_Timeout  = long.Parse(lbl_Solder_Ready_Timeout.Text);
            m_pDataCommonStat.m_lgCleaning_Time          = long.Parse(lbl_SolderCleanTime.Text);

            m_pDataCommonStat.m_iIronHead_UseCount      = int.Parse(lbl_IronHeadMaxUseCount.Text);
            m_pDataCommonStat.m_iCleanCount             = int.Parse(lbl_CleanCounterThreshold.Text);
            m_pDataCommonStat.m_dbCleaningOffset_Up     = double.Parse(lbl_CleanUpOffset.Text);
            m_pDataCommonStat.m_dbCleaningOffset_Dwn    = double.Parse(lbl_CleanDownOffset.Text);

            //enable selection
            m_pDataCommonStat.m_bSolder_Point_1 = Get_Enable(btn_SolderPoint1);
            m_pDataCommonStat.m_bSolder_Point_2 = Get_Enable(btn_SolderPoint2);
            m_pDataCommonStat.m_bSolder_Point_3 = Get_Enable(btn_SolderPoint3);
            m_pDataCommonStat.m_bSolder_Point_4 = Get_Enable(btn_SolderPoint4);
            m_pDataCommonStat.m_bSolder_Point_5 = Get_Enable(btn_SolderPoint5);
            m_pDataCommonStat.m_bSolder_Point_6 = Get_Enable(btn_SolderPoint6);

            m_pDataCommonStat.m_dbOffset_X_SolderPoint1_NoRotation = double.Parse(lbl_SolderPoint1_X_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint1 = double.Parse(lbl_SolderPoint1_X.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint2 = double.Parse(lbl_SolderPoint2_X.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint3 = double.Parse(lbl_SolderPoint3_X.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint4 = double.Parse(lbl_SolderPoint4_X.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint5 = double.Parse(lbl_SolderPoint5_X.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint6 = double.Parse(lbl_SolderPoint6_X.Text);

            m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_NoRotation = double.Parse(lbl_SolderPoint1_Y_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint1 = double.Parse(lbl_SolderPoint1_Y.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint2 = double.Parse(lbl_SolderPoint2_Y.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint3 = double.Parse(lbl_SolderPoint3_Y.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint4 = double.Parse(lbl_SolderPoint4_Y.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint5 = double.Parse(lbl_SolderPoint5_Y.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint6 = double.Parse(lbl_SolderPoint6_Y.Text);

            m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_NoRotation = double.Parse(lbl_SolderPoint1_Z_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint1 = double.Parse(lbl_SolderPoint1_Z.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint2 = double.Parse(lbl_SolderPoint2_Z.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint3 = double.Parse(lbl_SolderPoint3_Z.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint4 = double.Parse(lbl_SolderPoint4_Z.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint5 = double.Parse(lbl_SolderPoint5_Z.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint6 = double.Parse(lbl_SolderPoint6_Z.Text);

            //Double Soldering     //added 20200914
            m_pDataCommonStat.m_dbOffset_X_SolderPoint1_NoRotation_A = double.Parse(lbl_SolderPoint1_X_A_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint1_A = double.Parse(lbl_SolderPoint1_X_A.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint2_A = double.Parse(lbl_SolderPoint2_X_A.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint3_A = double.Parse(lbl_SolderPoint3_X_A.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint4_A = double.Parse(lbl_SolderPoint4_X_A.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint5_A = double.Parse(lbl_SolderPoint5_X_A.Text);
            m_pDataCommonStat.m_dbOffset_X_SolderPoint6_A = double.Parse(lbl_SolderPoint6_X_A.Text);

            m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_NoRotation_A = double.Parse(lbl_SolderPoint1_Y_A_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint1_A = double.Parse(lbl_SolderPoint1_Y_A.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint2_A = double.Parse(lbl_SolderPoint2_Y_A.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint3_A = double.Parse(lbl_SolderPoint3_Y_A.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint4_A = double.Parse(lbl_SolderPoint4_Y_A.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint5_A = double.Parse(lbl_SolderPoint5_Y_A.Text);
            m_pDataCommonStat.m_dbOffset_Y_SolderPoint6_A = double.Parse(lbl_SolderPoint6_Y_A.Text);
                                                                                           
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_NoRotation_A = double.Parse(lbl_SolderPoint1_Z_A_NoRotate.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint1_A = double.Parse(lbl_SolderPoint1_Z_A.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint2_A = double.Parse(lbl_SolderPoint2_Z_A.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint3_A = double.Parse(lbl_SolderPoint3_Z_A.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint4_A = double.Parse(lbl_SolderPoint4_Z_A.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint5_A = double.Parse(lbl_SolderPoint5_Z_A.Text);
            m_pDataCommonStat.m_dbOffset_Z_SolderPoint6_A = double.Parse(lbl_SolderPoint6_Z_A.Text);

            //Cleaning setting 
            m_pDataCommonStat.m_iSolder_Program_Cleaning = int.Parse(lbl_iSetProgClean.Text);

            //Spec file point to point difference
            m_pDataCommonStat.m_dbSpec_X_PinPoint1 = double.Parse(lbl_SpecOffset_X_1.Text);
            m_pDataCommonStat.m_dbSpec_X_PinPoint2 = double.Parse(lbl_SpecOffset_X_2.Text);
            m_pDataCommonStat.m_dbSpec_X_PinPoint3 = double.Parse(lbl_SpecOffset_X_3.Text);
            m_pDataCommonStat.m_dbSpec_X_PinPoint4 = double.Parse(lbl_SpecOffset_X_4.Text);
            m_pDataCommonStat.m_dbSpec_X_PinPoint5 = double.Parse(lbl_SpecOffset_X_5.Text);
            m_pDataCommonStat.m_dbSpec_X_PinPoint6 = double.Parse(lbl_SpecOffset_X_6.Text);

            m_pDataCommonStat.m_dbSpec_Y_PinPoint1 = double.Parse(lbl_SpecOffset_Y_1.Text);
            m_pDataCommonStat.m_dbSpec_Y_PinPoint2 = double.Parse(lbl_SpecOffset_Y_2.Text);
            m_pDataCommonStat.m_dbSpec_Y_PinPoint3 = double.Parse(lbl_SpecOffset_Y_3.Text);
            m_pDataCommonStat.m_dbSpec_Y_PinPoint4 = double.Parse(lbl_SpecOffset_Y_4.Text);
            m_pDataCommonStat.m_dbSpec_Y_PinPoint5 = double.Parse(lbl_SpecOffset_Y_5.Text);
            m_pDataCommonStat.m_dbSpec_Y_PinPoint6 = double.Parse(lbl_SpecOffset_Y_6.Text);

            m_pDataCommonStat.m_dbSpec_Z_PinPoint1 = double.Parse(lbl_SpecOffset_Z_1.Text);  //david
            m_pDataCommonStat.m_dbSpec_Z_PinPoint2 = double.Parse(lbl_SpecOffset_Z_2.Text);  //david
            m_pDataCommonStat.m_dbSpec_Z_PinPoint3 = double.Parse(lbl_SpecOffset_Z_3.Text);  //david
            m_pDataCommonStat.m_dbSpec_Z_PinPoint4 = double.Parse(lbl_SpecOffset_Z_4.Text);  //david
            m_pDataCommonStat.m_dbSpec_Z_PinPoint5 = double.Parse(lbl_SpecOffset_Z_5.Text);  //david
            m_pDataCommonStat.m_dbSpec_Z_PinPoint6 = double.Parse(lbl_SpecOffset_Z_6.Text);  //david

            //Datum Offset
            m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_X = double.Parse(lbl_DatumOffset_X.Text);
            m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_Y = double.Parse(lbl_DatumOffset_Y.Text);
            m_pDataCommonStat.m_dbOffset_DatumTo1stPoint_Z = double.Parse(lbl_DatumOffset_Z.Text); //david

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


		//button Events
        private void btn_SetProg1_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P1, btn_SetProg1.Text); }
        private void btn_SetProg2_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P2, btn_SetProg2.Text); }
        private void btn_SetProg3_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P3, btn_SetProg3.Text); }
        private void btn_SetProg4_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P4, btn_SetProg4.Text); }
        private void btn_SetProg5_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P5, btn_SetProg5.Text); }
        private void btn_SetProg6_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P6, btn_SetProg6.Text); }

        //solder selection
        private void btn_SolderPoint1_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint1, Get_EnableInverse(btn_SolderPoint1)); }
        private void btn_SolderPoint2_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint2, Get_EnableInverse(btn_SolderPoint2)); }
        private void btn_SolderPoint3_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint3, Get_EnableInverse(btn_SolderPoint3)); }
        private void btn_SolderPoint4_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint4, Get_EnableInverse(btn_SolderPoint4)); }
        private void btn_SolderPoint5_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint5, Get_EnableInverse(btn_SolderPoint5)); }
        private void btn_SolderPoint6_Click(object sender, EventArgs e) { Update_ButtonEnable(btn_SolderPoint6, Get_EnableInverse(btn_SolderPoint6)); }

        //Double Soldering          //added 20200914
        private void btn_SetProg1_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P1_A, btn_SetProg1_A.Text); }
        private void btn_SetProg2_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P2_A, btn_SetProg2_A.Text); }
        private void btn_SetProg3_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P3_A, btn_SetProg3_A.Text); }
        private void btn_SetProg4_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P4_A, btn_SetProg4_A.Text); }
        private void btn_SetProg5_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P5_A, btn_SetProg5_A.Text); }
        private void btn_SetProg6_A_Click(object sender, EventArgs e) { Update_Label(lbl_iSolderProg_P6_A, btn_SetProg6_A.Text); }

        private void btn_SolderSleepTime_Click              (object sender, EventArgs e) { Update_Label(lbl_SolderSleepTime, btn_SolderSleepTime.Text); }
        private void btn_SolderStartTimeOut_Click           (object sender, EventArgs e) { Update_Label(lbl_SolderStartTimeOut, btn_SolderStartTimeOut.Text); }
        private void btn_SolderRunTimeout_Click             (object sender, EventArgs e) { Update_Label(lbl_SolderRunTimeout, btn_SolderRunTimeout.Text); }
        private void btn_Solder_Ready_Timeout_Click         (object sender, EventArgs e) { Update_Label(lbl_Solder_Ready_Timeout, btn_Solder_Ready_Timeout.Text); }
        private void btn_SolderCleanTime_Click              (object sender, EventArgs e) { Update_Label(lbl_SolderCleanTime, btn_SolderCleanTime.Text); }
        private void btn_FumeExtratorOffTime_Click          (object sender, EventArgs e) { Update_Label(lbl_FumExtractorOffTime, btn_FumeExtratorOffTime.Text); }
        private void btn_CleanUpOffset_Click                (object sender, EventArgs e) { Update_Label(lbl_CleanUpOffset, btn_CleanUpOffset.Text); }
        private void btn_CleanDownOffset_Click              (object sender, EventArgs e) { Update_Label(lbl_CleanDownOffset, btn_CleanDownOffset.Text); }
        private void btn_IronHeadMaxUseCount_Click          (object sender, EventArgs e) { Update_Label(lbl_IronHeadMaxUseCount, btn_IronHeadMaxUseCount.Text); }
        private void btn_CleanCounterThreshold_Click        (object sender, EventArgs e) { Update_Label(lbl_CleanCounterThreshold, btn_CleanCounterThreshold.Text); }

        private void btn_SetProgClean_Click(object sender, EventArgs e) { Update_Label(lbl_iSetProgClean, btn_SetProgClean.Text); }

        //manual offset after each point
        private void btn_SolderPoint1_X_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_X_NoRotate, btn_SolderPoint1_X_NoRotate.Text, true); }
        private void btn_SolderPoint1_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_X, btn_SolderPoint1_X.Text, true); }
        private void btn_SolderPoint2_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_X, btn_SolderPoint2_X.Text, true); }
        private void btn_SolderPoint3_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_X, btn_SolderPoint3_X.Text, true); }
        private void btn_SolderPoint4_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_X, btn_SolderPoint4_X.Text, true); }
        private void btn_SolderPoint5_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_X, btn_SolderPoint5_X.Text, true); }
        private void btn_SolderPoint6_X_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_X, btn_SolderPoint6_X.Text, true); }

        private void btn_SolderPoint1_Y_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Y_NoRotate, btn_SolderPoint1_Y_NoRotate.Text, true); }
        private void btn_SolderPoint1_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Y, btn_SolderPoint1_Y.Text, true); }
        private void btn_SolderPoint2_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_Y, btn_SolderPoint2_Y.Text, true); }
        private void btn_SolderPoint3_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_Y, btn_SolderPoint3_Y.Text, true); }
        private void btn_SolderPoint4_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_Y, btn_SolderPoint4_Y.Text, true); }
        private void btn_SolderPoint5_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_Y, btn_SolderPoint5_Y.Text, true); }
        private void btn_SolderPoint6_Y_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_Y, btn_SolderPoint6_Y.Text, true); }

        private void btn_SolderPoint1_Z_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Z_NoRotate, btn_SolderPoint1_Z_NoRotate.Text, true); }
        private void btn_SolderPoint1_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Z, btn_SolderPoint1_Z.Text, true); }
        private void btn_SolderPoint2_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_Z, btn_SolderPoint2_Z.Text, true); }
        private void btn_SolderPoint3_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_Z, btn_SolderPoint3_Z.Text, true); }
        private void btn_SolderPoint4_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_Z, btn_SolderPoint4_Z.Text, true); }
        private void btn_SolderPoint5_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_Z, btn_SolderPoint5_Z.Text, true); }
        private void btn_SolderPoint6_Z_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_Z, btn_SolderPoint6_Z.Text, true); }

        //manual offset after each point (Double Soldering)         //added 20200914
        private void btn_SolderPoint1_X_A_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_X_A_NoRotate, btn_SolderPoint1_X_A_NoRotate.Text, true); }
        private void btn_SolderPoint1_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_X_A, btn_SolderPoint1_X_A.Text, true); }
        private void btn_SolderPoint2_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_X_A, btn_SolderPoint2_X_A.Text, true); }
        private void btn_SolderPoint3_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_X_A, btn_SolderPoint3_X_A.Text, true); }
        private void btn_SolderPoint4_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_X_A, btn_SolderPoint4_X_A.Text, true); }
        private void btn_SolderPoint5_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_X_A, btn_SolderPoint5_X_A.Text, true); }
        private void btn_SolderPoint6_X_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_X_A, btn_SolderPoint6_X_A.Text, true); }

        private void btn_SolderPoint1_Y_A_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Y_A_NoRotate, btn_SolderPoint1_Y_A_NoRotate.Text, true); }
        private void btn_SolderPoint1_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Y_A, btn_SolderPoint1_Y_A.Text, true); }
        private void btn_SolderPoint2_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_Y_A, btn_SolderPoint2_Y_A.Text, true); }
        private void btn_SolderPoint3_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_Y_A, btn_SolderPoint3_Y_A.Text, true); }
        private void btn_SolderPoint4_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_Y_A, btn_SolderPoint4_Y_A.Text, true); }
        private void btn_SolderPoint5_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_Y_A, btn_SolderPoint5_Y_A.Text, true); }
        private void btn_SolderPoint6_Y_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_Y_A, btn_SolderPoint6_Y_A.Text, true); }

        private void btn_SolderPoint1_Z_A_NoRotate_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Z_A_NoRotate, btn_SolderPoint1_Z_A_NoRotate.Text, true); }
        private void btn_SolderPoint1_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint1_Z_A, btn_SolderPoint1_Z_A.Text, true); }
        private void btn_SolderPoint2_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint2_Z_A, btn_SolderPoint2_Z_A.Text, true); }
        private void btn_SolderPoint3_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint3_Z_A, btn_SolderPoint3_Z_A.Text, true); }
        private void btn_SolderPoint4_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint4_Z_A, btn_SolderPoint4_Z_A.Text, true); }
        private void btn_SolderPoint5_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint5_Z_A, btn_SolderPoint5_Z_A.Text, true); }
        private void btn_SolderPoint6_Z_A_Click(object sender, EventArgs e) { Update_Label(lbl_SolderPoint6_Z_A, btn_SolderPoint6_Z_A.Text, true); }

        //spec (point to point) offset
        private void btn_SpecOffset_X_1_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_1, btn_SpecOffset_X_1.Text, true); }
        private void btn_SpecOffset_X_2_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_2, btn_SpecOffset_X_2.Text, true); }
        private void btn_SpecOffset_X_3_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_3, btn_SpecOffset_X_3.Text, true); }
        private void btn_SpecOffset_X_4_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_4, btn_SpecOffset_X_4.Text, true); }
        private void btn_SpecOffset_X_5_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_5, btn_SpecOffset_X_5.Text, true); }
        private void btn_SpecOffset_X_6_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_X_6, btn_SpecOffset_X_6.Text, true); }

        private void btn_SpecOffset_Y_1_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_1, btn_SpecOffset_Y_1.Text, true); }
        private void btn_SpecOffset_Y_2_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_2, btn_SpecOffset_Y_2.Text, true); }
        private void btn_SpecOffset_Y_3_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_3, btn_SpecOffset_Y_3.Text, true); }
        private void btn_SpecOffset_Y_4_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_4, btn_SpecOffset_Y_4.Text, true); }
        private void btn_SpecOffset_Y_5_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_5, btn_SpecOffset_Y_5.Text, true); }
        private void btn_SpecOffset_Y_6_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Y_6, btn_SpecOffset_Y_6.Text, true); }

        private void btn_SpecOffset_Z_1_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_1, btn_SpecOffset_Z_1.Text, true); }  //david
        private void btn_SpecOffset_Z_2_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_2, btn_SpecOffset_Z_2.Text, true); }  //david
        private void btn_SpecOffset_Z_3_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_3, btn_SpecOffset_Z_3.Text, true); }  //david
        private void btn_SpecOffset_Z_4_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_4, btn_SpecOffset_Z_4.Text, true); }  //david
        private void btn_SpecOffset_Z_5_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_5, btn_SpecOffset_Z_5.Text, true); }  //david
        private void btn_SpecOffset_Z_6_Click(object sender, EventArgs e) { Update_Label(lbl_SpecOffset_Z_6, btn_SpecOffset_Z_6.Text, true); }  //david

        //datum to 1st point offset
        private void btn_DatumOffset_X_Click(object sender, EventArgs e) { Update_Label(lbl_DatumOffset_X, btn_DatumOffset_X.Text, true); }
        private void btn_DatumOffset_Y_Click(object sender, EventArgs e) { Update_Label(lbl_DatumOffset_Y, btn_DatumOffset_Y.Text, true); }
        private void btn_DatumOffset_Z_Click(object sender, EventArgs e) { Update_Label(lbl_DatumOffset_Z, btn_DatumOffset_Z.Text, true); }

        private void btn_ResetCurrentIronHeadCounter_Click(object sender, EventArgs e) 
        {
            MessageBox_Confirmation dlg = new MessageBox_Confirmation();
            dlg.Set_Title("Are you sure?");
            dlg.Set_Label("Do you want to reset the current solder iron head use counter?\r\nThis action cannot be undone.");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_pDataCommonStat.m_iCurrentIronHead_UseCount = 0;
                m_pSeqMachineCtrl.Pkg_To_Registry();

                lbl_CurrentSolderIronHeadUseCount.Text = m_pDataCommonStat.m_iCurrentIronHead_UseCount.ToString();
            }
        }

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
