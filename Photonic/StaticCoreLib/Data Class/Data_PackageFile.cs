/****************************************************************************************************************************************
 * Data Package File class: 
 * Desc:    This class is to keep settings which is dependent on the product, such as, position, depth, color, vision settings and etc.
 * 
 * Method:  This class is used for recipe and product / part spec settings variables storage and registry
 *                 File path of the package file is set at machine info class
 *                 Use Seqmachinecontrol Pkg_From_registry to load the settings into this class. This function uses the file path from machine info
 *                 Use Seqmachinecontrol Pkg_To_Registry to save the settings to xml file 
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace StaticCoreLib
{
    [Serializable]
    public class Data_PackageFile
    {
		//Note:
		//Please Update the settings via the Update_Settings function in this class to reflect the changes.

        //variables

		//robot position
		public Data_Robot m_DataRobot01 = new Data_Robot();
		public Data_Robot m_DataRobot02 = new Data_Robot();
		public Data_Robot m_DataRobot03 = new Data_Robot();
        public Data_Robot m_DataRobot04 = new Data_Robot();

		//Common Data: All non clone settings are placed here
		public Data_Common m_DataCommon = new Data_Common();

		//Common Station / Clone data
		public Data_Common_Stat m_DataCommon_Stat1 = new Data_Common_Stat();
		public Data_Common_Stat m_DataCommon_Stat2 = new Data_Common_Stat();
		public Data_Common_Stat m_DataCommon_Stat3 = new Data_Common_Stat();

        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_PackageFile()
        {
            //solder station gantry position
            m_DataRobot01.AddSettings(0, 0, 0, "P0", "Standby", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P1", "Solder Pos1", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P2", "Solder Pos2", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P3", "Solder Pos3", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P4", "Solder Pos4", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P5", "Solder Pos5", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P6", "Solder Pos6", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P7", "Cleaner", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P8", "Cleaner Up", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P9", "Cleaner Down", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P10", "Datum Point", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P11", "Waste Bin", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P12", "Solder Pos7", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P13", "Solder Pos8", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P14", "Solder Pos9", 1);
            m_DataRobot01.AddSettings(0, 0, 0, "P32", "Safe Point", 1);

            m_DataRobot02.AddSettings(0, 0, 0, "P0", "Standby", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P1", "Solder Pos1", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P2", "Solder Pos2", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P3", "Solder Pos3", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P4", "Solder Pos4", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P5", "Solder Pos5", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P6", "Solder Pos6", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P7", "Cleaner", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P8", "Cleaner Up", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P9", "Cleaner Down", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P10", "Datum Point", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P11", "Waste Bin", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P12", "Solder Pos7", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P13", "Solder Pos8", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P14", "Solder Pos9", 1);
            m_DataRobot02.AddSettings(0, 0, 0, "P32", "Safe Point", 1);

            m_DataRobot03.AddSettings(0, 0, 0, "P0", "Standby", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P1", "Solder Pos1", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P2", "Solder Pos2", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P3", "Solder Pos3", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P4", "Solder Pos4", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P5", "Solder Pos5", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P6", "Solder Pos6", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P7", "Cleaner", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P8", "Cleaner Up", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P9", "Cleaner Down", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P10", "Datum Point", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P11", "Waste Bin", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P12", "Solder Pos7", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P13", "Solder Pos8", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P14", "Solder Pos9", 1);
            m_DataRobot03.AddSettings(0, 0, 0, "P32", "Safe Point", 1);

            m_DataRobot04.AddSettings(0, 0, 0, "P0", "Thermocouple", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P1", "Standby", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P2", "Terminal 1 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P3", "Terminal 1 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P4", "Terminal 2 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P5", "Terminal 2 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P6", "Terminal 3 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P7", "Terminal 3 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P8", "Terminal 4 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P9", "Terminal 4 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P10", "Terminal 5 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P11", "Terminal 5 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P12", "Terminal 6 - Ceramic", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P13", "Terminal 6 - Tip", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P14", "PreHeat 1", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P15", "PreHeat 2", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P16", "PreHeat 3", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P17", "PreHeat 4", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P18", "PreHeat 5", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P19", "PreHeat 6", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P20", "PostHeat 1", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P21", "PostHeat 2", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P22", "PostHeat 3", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P23", "PostHeat 4", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P24", "PostHeat 5", 1);
            m_DataRobot04.AddSettings(0, 0, 0, "P25", "PostHeat 6", 1);
        }

		//Update Settings: Used by package file from registry////////////////////////////////////////////////////////////////////////
        public void Update_Settings(Data_PackageFile mpDataPackage)
        {
            //update common data settings
            m_DataCommon = mpDataPackage.m_DataCommon;

            //update common clone settings
            m_DataCommon_Stat1 = mpDataPackage.m_DataCommon_Stat1;
            m_DataCommon_Stat2 = mpDataPackage.m_DataCommon_Stat2;
            m_DataCommon_Stat3 = mpDataPackage.m_DataCommon_Stat3;

            //update robot position
            m_DataRobot01.Update_Settings(mpDataPackage.m_DataRobot01);
            m_DataRobot02.Update_Settings(mpDataPackage.m_DataRobot02);
            m_DataRobot03.Update_Settings(mpDataPackage.m_DataRobot03);
            m_DataRobot04.Update_Settings(mpDataPackage.m_DataRobot04);
        }
    }

	//Common Data Settings:
	[Serializable]
	public class Data_Common
	{
        //vision delay
        public int m_iDelay_BeforeTriggerVision_OP85 = 0;
        public int m_iDelay_BeforeTriggerVision_OP90 = 0;

        //OP85 vision spec
        public double m_db_Terminal1_Width_Spec = 0;
        public double m_db_Terminal1_Length_Spec = 0;
        public double m_db_Terminal1_Distance_Spec = 0;

        public double m_db_Terminal2_Width_Spec = 0;
        public double m_db_Terminal2_Length_Spec = 0;
        public double m_db_Terminal2_Distance_Spec = 0;

        public double m_db_Terminal3_Width_Spec = 0;
        public double m_db_Terminal3_Length_Spec = 0;
        public double m_db_Terminal3_Distance_Spec = 0;

        public double m_db_Terminal4_Width_Spec = 0;
        public double m_db_Terminal4_Length_Spec = 0;
        public double m_db_Terminal4_Distance_Spec = 0;

        public double m_db_Terminal5_Width_Spec = 0;
        public double m_db_Terminal5_Length_Spec = 0;
        public double m_db_Terminal5_Distance_Spec = 0;

        public double m_db_Terminal6_Width_Spec = 0;
        public double m_db_Terminal6_Length_Spec = 0;
        public double m_db_Terminal6_Distance_Spec = 0;

        public double m_db_Terminal1_Width_Tolerance_Min = 0;
        public double m_db_Terminal1_Length_Tolerance_Min = 0;
        public double m_db_Terminal1_Distance_Tolerance_Min = 0;

        public double m_db_Terminal2_Width_Tolerance_Min = 0;
        public double m_db_Terminal2_Length_Tolerance_Min = 0;
        public double m_db_Terminal2_Distance_Tolerance_Min = 0;

        public double m_db_Terminal3_Width_Tolerance_Min = 0;
        public double m_db_Terminal3_Length_Tolerance_Min = 0;
        public double m_db_Terminal3_Distance_Tolerance_Min = 0;

        public double m_db_Terminal4_Width_Tolerance_Min = 0;
        public double m_db_Terminal4_Length_Tolerance_Min = 0;
        public double m_db_Terminal4_Distance_Tolerance_Min = 0;

        public double m_db_Terminal5_Width_Tolerance_Min = 0;
        public double m_db_Terminal5_Length_Tolerance_Min = 0;
        public double m_db_Terminal5_Distance_Tolerance_Min = 0;

        public double m_db_Terminal6_Width_Tolerance_Min = 0;
        public double m_db_Terminal6_Length_Tolerance_Min = 0;
        public double m_db_Terminal6_Distance_Tolerance_Min = 0;

        public double m_db_Terminal1_Width_Tolerance_Max = 0;
        public double m_db_Terminal1_Length_Tolerance_Max = 0;
        public double m_db_Terminal1_Distance_Tolerance_Max = 0;

        public double m_db_Terminal2_Width_Tolerance_Max = 0;
        public double m_db_Terminal2_Length_Tolerance_Max = 0;
        public double m_db_Terminal2_Distance_Tolerance_Max = 0;

        public double m_db_Terminal3_Width_Tolerance_Max = 0;
        public double m_db_Terminal3_Length_Tolerance_Max = 0;
        public double m_db_Terminal3_Distance_Tolerance_Max = 0;

        public double m_db_Terminal4_Width_Tolerance_Max = 0;
        public double m_db_Terminal4_Length_Tolerance_Max = 0;
        public double m_db_Terminal4_Distance_Tolerance_Max = 0;

        public double m_db_Terminal5_Width_Tolerance_Max = 0;
        public double m_db_Terminal5_Length_Tolerance_Max = 0;
        public double m_db_Terminal5_Distance_Tolerance_Max = 0;

        public double m_db_Terminal6_Width_Tolerance_Max = 0;
        public double m_db_Terminal6_Length_Tolerance_Max = 0;
        public double m_db_Terminal6_Distance_Tolerance_Max = 0;

        //OP90
        //Vision
        public int m_i_OP90_VisionFailCountLimit = 3;

        //HTR
        public double m_db_HTR_A_Min_LV = 40.0;
        public double m_db_HTR_B_Min_LV = 40.0;
        public double m_db_HTR_C_Min_LV = 40.0;
        public double m_db_HTR_D_Min_LV = 40.0;
        public double m_db_HTR_E_Min_LV = 40.0;

        public double m_db_HTR_A_Max_LV = 30.0;
        public double m_db_HTR_B_Max_LV = 30.0;
        public double m_db_HTR_C_Max_LV = 30.0;
        public double m_db_HTR_D_Max_LV = 30.0;
        public double m_db_HTR_E_Max_LV = 30.0;

        public double m_db_HTR_A_Nominal_LV = 40.0;
        public double m_db_HTR_B_Nominal_LV = 40.0;
        public double m_db_HTR_C_Nominal_LV = 40.0;
        public double m_db_HTR_D_Nominal_LV = 40.0;
        public double m_db_HTR_E_Nominal_LV = 40.0;

        public double m_db_HTR_A_Min = 40.0;
        public double m_db_HTR_B_Min = 40.0;
        public double m_db_HTR_C_Min = 40.0;
        public double m_db_HTR_D_Min = 40.0;
        public double m_db_HTR_E_Min = 40.0;

        public double m_db_HTR_A_Max = 30.0;
        public double m_db_HTR_B_Max = 30.0;
        public double m_db_HTR_C_Max = 30.0;
        public double m_db_HTR_D_Max = 30.0;
        public double m_db_HTR_E_Max = 30.0;

        public double m_db_HTR_A_Nominal = 40.0;
        public double m_db_HTR_B_Nominal = 40.0;
        public double m_db_HTR_C_Nominal = 40.0;
        public double m_db_HTR_D_Nominal = 40.0;
        public double m_db_HTR_E_Nominal = 40.0;

        //RTD
        public double m_db_RTD_A_Min_LV = 40.0;
        public double m_db_RTD_B_Min_LV = 40.0;
        public double m_db_RTD_C_Min_LV = 40.0;
        public double m_db_RTD_D_Min_LV = 40.0;
        public double m_db_RTD_E_Min_LV = 40.0;

        public double m_db_RTD_A_Max_LV = 30.0;
        public double m_db_RTD_B_Max_LV = 30.0;
        public double m_db_RTD_C_Max_LV = 30.0;
        public double m_db_RTD_D_Max_LV = 30.0;
        public double m_db_RTD_E_Max_LV = 30.0;

        public double m_db_RTD_A_Nominal_LV = 40.0;
        public double m_db_RTD_B_Nominal_LV = 40.0;
        public double m_db_RTD_C_Nominal_LV = 40.0;
        public double m_db_RTD_D_Nominal_LV = 40.0;
        public double m_db_RTD_E_Nominal_LV = 40.0;

        public double m_db_RTD_A_Min = 40.0;
        public double m_db_RTD_B_Min = 40.0;
        public double m_db_RTD_C_Min = 40.0;
        public double m_db_RTD_D_Min = 40.0;
        public double m_db_RTD_E_Min = 40.0;

        public double m_db_RTD_A_Max = 30.0;
        public double m_db_RTD_B_Max = 30.0;
        public double m_db_RTD_C_Max = 30.0;
        public double m_db_RTD_D_Max = 30.0;
        public double m_db_RTD_E_Max = 30.0;

        public double m_db_RTD_A_Nominal = 40.0;
        public double m_db_RTD_B_Nominal = 40.0;
        public double m_db_RTD_C_Nominal = 40.0;
        public double m_db_RTD_D_Nominal = 40.0;
        public double m_db_RTD_E_Nominal = 40.0;

        //HotBlower
        public double m_dbHotBlower_Temperature = 395;
        public long m_lg_BlowerTimeout_Run = 7000;	//HotBlower Station: Run timeout

        //HotBlower Station: HotBlower Point Selection 
        public bool m_bHotBlower_PreHeat_1 = false;
        public bool m_bHotBlower_PreHeat_2 = false;
        public bool m_bHotBlower_PreHeat_3 = false;
        public bool m_bHotBlower_PreHeat_4 = false;
        public bool m_bHotBlower_PreHeat_5 = false;
        public bool m_bHotBlower_PreHeat_6 = false;
        public bool m_bHotBlower_Terminal1_Ceramic = true;
        public bool m_bHotBlower_Terminal1_Tip = true;
        public bool m_bHotBlower_Terminal2_Ceramic = true;
        public bool m_bHotBlower_Terminal2_Tip = true;
        public bool m_bHotBlower_Terminal3_Ceramic = true;
        public bool m_bHotBlower_Terminal3_Tip = true;
        public bool m_bHotBlower_Terminal4_Ceramic = true;
        public bool m_bHotBlower_Terminal4_Tip = true;
        public bool m_bHotBlower_Terminal5_Ceramic = true;
        public bool m_bHotBlower_Terminal5_Tip = true;
        public bool m_bHotBlower_Terminal6_Ceramic = true;
        public bool m_bHotBlower_Terminal6_Tip = true;
        public bool m_bHotBlower_PostHeat_1 = false;
        public bool m_bHotBlower_PostHeat_2 = false;
        public bool m_bHotBlower_PostHeat_3 = false;
        public bool m_bHotBlower_PostHeat_4 = false;
        public bool m_bHotBlower_PostHeat_5 = false;
        public bool m_bHotBlower_PostHeat_6 = false;
	}

    //Note: This class setting is meant for Solder station which will be clone into 3 objects
	//Common Station / Clone product Settings
	[Serializable]
	public class Data_Common_Stat
	{
        //solder program selection
        public int m_iSolder_Program_Cleaning = 5;
        public int m_iSolder_Program_Pin_1 = 3;
        public int m_iSolder_Program_Pin_2 = 3;
        public int m_iSolder_Program_Pin_3 = 2;
        public int m_iSolder_Program_Pin_4 = 2;
        public int m_iSolder_Program_Pin_5 = 2;
        public int m_iSolder_Program_Pin_6 = 1;
        public int m_iSolder_Program_Pos_7 = 1;
        public int m_iSolder_Program_Pos_8 = 1;
        public int m_iSolder_Program_Pos_9 = 1;

        //soldering station: solder selection 
        public bool m_bSolder_Pos1 = false;
        public bool m_bSolder_Pos2 = false;
        public bool m_bSolder_Pos3 = false;
        public bool m_bSolder_Pos4 = false;
        public bool m_bSolder_Pos5 = false;
        public bool m_bSolder_Pos6 = false;
        public bool m_bSolder_Pos7 = false;
        public bool m_bSolder_Pos8 = false;
        public bool m_bSolder_Pos9 = false;

        //solder point gantry rotation, (default) false = negative, true = positive
        public bool m_bSolder_Pos1_Rotation = false;
        public bool m_bSolder_Pos2_Rotation = false;
        public bool m_bSolder_Pos3_Rotation = false;
        public bool m_bSolder_Pos4_Rotation = false;
        public bool m_bSolder_Pos5_Rotation = false;
        public bool m_bSolder_Pos6_Rotation = false;
        public bool m_bSolder_Pos7_Rotation = false;
        public bool m_bSolder_Pos8_Rotation = false;
        public bool m_bSolder_Pos9_Rotation = false;

        //cleaning selection before soldering
        public bool m_bCleanBefore_Pos1 = false;
        public bool m_bCleanBefore_Pos2 = false;
        public bool m_bCleanBefore_Pos3 = false;
        public bool m_bCleanBefore_Pos4 = false;
        public bool m_bCleanBefore_Pos5 = false;
        public bool m_bCleanBefore_Pos6 = false;
        public bool m_bCleanBefore_Pos7 = false;
        public bool m_bCleanBefore_Pos8 = false;
        public bool m_bCleanBefore_Pos9 = false;

        //cleaning settings
        public double m_dbCleaningOffset_Up = 0;
        public double m_dbCleaningOffset_Dwn = 0;
        public int m_iCleanCycle = 2;

		//timer: time out
        public long m_lg_Solder_Ready_Timeout = 120000;       //solder device ready signal timeout
        public long m_lg_Solder_Sleep_Timeout = 900000;       //solder device timeout to trigger sleep mode
        public long m_lg_Solder_HeaterOFF_Timeout = 1800000;  //solder device timeout to trigger Heater OFF
        public long m_lg_Soldering_Start_Timeout = 2000;      //solder station: start soldering timeout
        public long m_lg_Soldering_Run_Timeout = 7000;		  //solder station: run soldering timeout
        public long m_lg_Solder_Tip_Wetting_Timeout = 5000;   //solder tip wetting timeout, then do wetting
        public long m_lg_Solder_Wait_Pallet_Timeout = 60000;  //solder wait trigger flag timeout, then go back to wait pallet present and do wetting
        public int m_int_Solder_NextCycle_Delay = 1000;       //solder sequence delay before continue to next cycle
        public long m_lg_FumeOffTime_Max = 3000000;

		//Constructor
		public Data_Common_Stat() { }
	}
}