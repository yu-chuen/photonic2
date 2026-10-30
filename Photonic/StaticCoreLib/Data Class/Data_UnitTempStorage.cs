/****************************************************************************************************************************************
 * Data Class Temporary Storage
 * Desc:    This class stores temporary data from the camera which will be used by the Robot for positioning and or 
 *                data collection
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class Data_UnitTempStorage
    {
        //Same flag type
        public Data_SolderStationFlag m_DataFlag_SolderStation1 = new Data_SolderStationFlag();
        public Data_SolderStationFlag m_DataFlag_SolderStation2 = new Data_SolderStationFlag();
        public Data_SolderStationFlag m_DataFlag_SolderStation3 = new Data_SolderStationFlag();

        //Variables

        //MISC Flag
        private readonly object m_lock_RequestStateMessage = new object();
        private readonly object m_lock_MachineState = new object();
        private readonly object m_lock_LogLevel = new object();
        private readonly object m_lock_DispErrorMessage = new object();
		private readonly object m_lock_FlagDryRun = new object();
        private readonly object m_lock_FlagEndLot = new object();

        
        String m_strRequestStateMessage = "N/A";
        String m_strMachineState = "N/A";
        String m_strDispErrorMessage = "N/A";
		public int m_iLogLevel = 1;																	
		bool m_bFlag_DryRun = false;
        bool m_bFlagEndLot = false;
       
        public String RequestStateMessage_str
        {
            set { lock (m_lock_RequestStateMessage) { m_strRequestStateMessage = value; } }
            get { lock (m_lock_RequestStateMessage) { return m_strRequestStateMessage; } }
        }                                        //Display Message at main UI Machine Status
        public String DispErrorMessage_str
        {
            set { lock (m_lock_DispErrorMessage) { m_strDispErrorMessage = value; } }
            get { lock (m_lock_DispErrorMessage) { return m_strDispErrorMessage; } }
        }                                               //Display: Error Message
        public String MachineState_Str
        {
            set { lock (m_lock_MachineState) { m_strMachineState = value; } }
            get { lock (m_lock_MachineState) { return m_strMachineState; } }
        }                                                      //for GUI: display machine state (ex: initializing, stop error and running) 
		public int LogLevel_int
		{
			set { lock (m_lock_LogLevel) { m_iLogLevel = value; } }
			get { lock (m_lock_LogLevel) { return m_iLogLevel; } }
		}																	   //Set Log level
		public bool Flag_DryRun
		{
			set { lock (m_lock_FlagDryRun) { m_bFlag_DryRun = value; } }
			get { lock (m_lock_FlagDryRun) { return m_bFlag_DryRun; } }
		}																   //Flag Dry Run
        public bool Flag_EndLot
        {
            set { lock (m_lock_FlagEndLot) { m_bFlagEndLot = value; } }
            get { lock (m_lock_FlagEndLot) { return m_bFlagEndLot; } }
        }

        //bypass flags to force Station Run Complete to send to Server
        public bool m_Flag_ServerOn_BypassOP60 = false;
        public bool m_Flag_ServerOn_BypassOP70 = false;
        public bool m_Flag_ServerOn_BypassOP80 = false;
        public bool m_Flag_ServerOn_BypassOP85 = false;
        public bool m_Flag_ServerOn_BypassOP90 = false;
        public bool m_Flag_ServerOn_BypassOP100 = false;
        public bool m_Flag_ServerOn_BypassOP110 = false;

        //lock access to prevent 2 or more modules (threads) accessing the same function/object simultaneously, otherwise will crash
        public readonly object m_lock_Set_Station_DownTime_OP60 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP70 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP80 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP85 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP90 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP100 = new object();
        public readonly object m_lock_Set_Station_DownTime_OP110 = new object();

        //Runtime Logging 
		#region Runtime_Log
        private readonly object m_lock_TotalRunTime = new object();
        private readonly object m_lock_TotalStopTime = new object();
        private readonly object m_lock_StartProcessRun = new object();
        private readonly object m_lock_StopProcessRun = new object();
        private readonly object m_lock_CycleTime = new object();
        private readonly object m_lock_intPartCount = new object();
        private readonly object m_lock_intPartCount_Pass = new object();
        private readonly object m_lock_intPartCount_Fail = new object();

		String m_strRunTime = "0:0:0";
		String m_strStopTime = "0:0:0";
		String m_strStartProcessRun = "0:0:0";
		String m_strStopProcessRun = "0:0:0";
		String m_strCycleTime = "";
		int m_iPartCount = 0;
		int m_iPartCount_Pass = 0;
		int m_iPartCount_Fail = 0;

        public String Total_RunTime
        {
            set { lock (m_lock_TotalRunTime) { m_strRunTime = value; } }
            get { lock (m_lock_TotalRunTime) { return m_strRunTime; } }
        }
        public String Total_StopTime
        {
            set { lock (m_lock_TotalStopTime) { m_strStopTime = value; } }
            get { lock (m_lock_TotalStopTime) { return m_strStopTime; } }
        }
        public String ProcessRun_Start
        {
            set { lock (m_lock_StartProcessRun) { m_strStartProcessRun = value; } }
            get { lock (m_lock_StartProcessRun) { return m_strStartProcessRun; } }
        }
        public String ProcessRun_Stop
        {
            set { lock (m_lock_StopProcessRun) { m_strStopProcessRun = value; } }
            get { lock (m_lock_StopProcessRun) { return m_strStopProcessRun; } }
        }
        public String Cycle_Time
        {
            set { lock (m_lock_CycleTime) { m_strCycleTime = value; } }
            get { lock (m_lock_CycleTime) { return m_strCycleTime; } }
        }
        public int PartCount_int
        {
            set { lock (m_lock_intPartCount) { m_iPartCount = value; } }
            get { lock (m_lock_intPartCount) { return m_iPartCount; } }
        }
        public int PartCountPass_int
        {
            set { lock (m_lock_intPartCount_Pass) { m_iPartCount_Pass = value; } }
            get { lock (m_lock_intPartCount_Pass) { return m_iPartCount_Pass; } }
        }
        public int PartCountFail_int
        {
            set { lock (m_lock_intPartCount_Fail) { m_iPartCount_Fail = value; } }
            get { lock (m_lock_intPartCount_Fail) { return m_iPartCount_Fail; } }
        }
		#endregion

        //RFID
        private readonly object m_lock_RFID_OP85_SN = new object();
        String m_strRFID_OP85_SN = "";
        public String RFID_OP85_SN_str { set { lock (m_lock_RFID_OP85_SN) { m_strRFID_OP85_SN = value; } } get { lock (m_lock_RFID_OP85_SN) { return m_strRFID_OP85_SN; } } }

        private readonly object m_lock_RFID_OP90_SN = new object();
        String m_strRFID_OP90_SN = "";
        public String RFID_OP90_SN_str { set { lock (m_lock_RFID_OP90_SN) { m_strRFID_OP90_SN = value; } } get { lock (m_lock_RFID_OP90_SN) { return m_strRFID_OP90_SN; } } }

        private readonly object m_lock_RFID_OP100_Buffer_SN = new object();
        String m_strRFID_OP100_Buffer_SN = "";
        public String RFID_OP100_Buffer_SN_str { set { lock (m_lock_RFID_OP100_Buffer_SN) { m_strRFID_OP100_Buffer_SN = value; } } get { lock (m_lock_RFID_OP100_Buffer_SN) { return m_strRFID_OP100_Buffer_SN; } } }

        private readonly object m_lock_RFID_OP100_Station_SN = new object();
        String m_strRFID_OP100_Station_SN = "";
        public String RFID_OP100_Station_SN_str { set { lock (m_lock_RFID_OP100_Station_SN) { m_strRFID_OP100_Station_SN = value; } } get { lock (m_lock_RFID_OP100_Station_SN) { return m_strRFID_OP100_Station_SN; } } }

        private readonly object m_lock_RFID_OP110_SN = new object();
        String m_strRFID_OP110_SN = "";
        public String RFID_OP110_SN_str { set { lock (m_lock_RFID_OP110_SN) { m_strRFID_OP110_SN = value; } } get { lock (m_lock_RFID_OP110_SN) { return m_strRFID_OP110_SN; } } }
      
		//Cycle Time
        private readonly object m_lock_Cycle_Time_OP60 = new object();
        String m_strCycle_Time_OP60 = "";
        public String Cycle_Time_OP60_str { set { lock (m_lock_Cycle_Time_OP60) { m_strCycle_Time_OP60 = value; } } get { lock (m_lock_Cycle_Time_OP60) { return m_strCycle_Time_OP60; } } }

        private readonly object m_lock_Cycle_Time_OP70 = new object();
        String m_strCycle_Time_OP70 = "";
        public String Cycle_Time_OP70_str { set { lock (m_lock_Cycle_Time_OP70) { m_strCycle_Time_OP70 = value; } } get { lock (m_lock_Cycle_Time_OP70) { return m_strCycle_Time_OP70; } } }

        private readonly object m_lock_Cycle_Time_OP80 = new object();
        String m_strCycle_Time_OP80 = "";
        public String Cycle_Time_OP80_str { set { lock (m_lock_Cycle_Time_OP80) { m_strCycle_Time_OP80 = value; } } get { lock (m_lock_Cycle_Time_OP80) { return m_strCycle_Time_OP80; } } }

        private readonly object m_lock_Cycle_Time_OP85 = new object();
        String m_strCycle_Time_OP85 = "";
        public String Cycle_Time_OP85_str { set { lock (m_lock_Cycle_Time_OP85) { m_strCycle_Time_OP85 = value; } } get { lock (m_lock_Cycle_Time_OP85) { return m_strCycle_Time_OP85; } } }

        private readonly object m_lock_Cycle_Time_OP90 = new object();
        String m_strCycle_Time_OP90 = "";
        public String Cycle_Time_OP90_str { set { lock (m_lock_Cycle_Time_OP90) { m_strCycle_Time_OP90 = value; } } get { lock (m_lock_Cycle_Time_OP90) { return m_strCycle_Time_OP90; } } }

        private readonly object m_lock_Cycle_Time_OP100 = new object();
        String m_strCycle_Time_OP100 = "";
        public String Cycle_Time_OP100_str { set { lock (m_lock_Cycle_Time_OP100) { m_strCycle_Time_OP100 = value; } } get { lock (m_lock_Cycle_Time_OP100) { return m_strCycle_Time_OP100; } } }

        private readonly object m_lock_Cycle_Time_OP110 = new object();
        String m_strCycle_Time_OP110 = "";
        public String Cycle_Time_OP110_str { set { lock (m_lock_Cycle_Time_OP110) { m_strCycle_Time_OP110 = value; } } get { lock (m_lock_Cycle_Time_OP110) { return m_strCycle_Time_OP110; } } }

		//Flagging - Operations
		//Soldering Station CONVEYOR flagging
        private readonly object m_lock_Flag_Conv_OP60_Ready = new object();				//Flag: Signal that the conveyor is ready for pallet
        private readonly object m_lock_Flag_Conv_OP70_Ready = new object();
        private readonly object m_lock_Flag_Conv_OP80_Ready = new object();
        private readonly object m_lock_Flag_Conv_OP60_Sending = new object();			//Flag: Trigger conveyor to run. Pallet moving from station to station
        private readonly object m_lock_Flag_Conv_OP70_Sending = new object();
        private readonly object m_lock_Flag_Conv_OP80_Sending = new object();
        private readonly object m_lock_Flag_Conv_OP60_Trigger = new object();			//Flag: Trigger Station to do process
        private readonly object m_lock_Flag_Conv_OP70_Trigger = new object();
        private readonly object m_lock_Flag_Conv_OP80_Trigger = new object();
        private readonly object m_lock_Flag_Station_OP60_Done = new object();			//Flag: Signal the Conveyor that the process is complete
        private readonly object m_lock_Flag_Station_OP70_Done = new object();
        private readonly object m_lock_Flag_Station_OP80_Done = new object();

        bool m_bFlag_Conv_OP60_Ready = false;
        bool m_bFlag_Conv_OP70_Ready = false;
        bool m_bFlag_Conv_OP80_Ready = false;
        bool m_bFlag_Conv_OP60_Sending = false;
        bool m_bFlag_Conv_OP70_Sending = false;
        bool m_bFlag_Conv_OP80_Sending = false;
        bool m_bFlag_Conv_OP60_Trigger = false;
        bool m_bFlag_Conv_OP70_Trigger = false;
        bool m_bFlag_Conv_OP80_Trigger = false;
        bool m_bFlag_Station_OP60_Done = false;
        bool m_bFlag_Station_OP70_Done = false;
        bool m_bFlag_Station_OP80_Done = false;

        public bool Flag_Conv_OP60_Ready_b { set { lock (m_lock_Flag_Conv_OP60_Ready) { m_bFlag_Conv_OP60_Ready = value; } } get { lock (m_lock_Flag_Conv_OP60_Ready) { return m_bFlag_Conv_OP60_Ready; } } }
        public bool Flag_Conv_OP70_Ready_b { set { lock (m_lock_Flag_Conv_OP70_Ready) { m_bFlag_Conv_OP70_Ready = value; } } get { lock (m_lock_Flag_Conv_OP70_Ready) { return m_bFlag_Conv_OP70_Ready; } } }
        public bool Flag_Conv_OP80_Ready_b { set { lock (m_lock_Flag_Conv_OP80_Ready) { m_bFlag_Conv_OP80_Ready = value; } } get { lock (m_lock_Flag_Conv_OP80_Ready) { return m_bFlag_Conv_OP80_Ready; } } }
        public bool Flag_Conv_OP60_Sending_b { set { lock (m_lock_Flag_Conv_OP60_Sending) { m_bFlag_Conv_OP60_Sending = value; } } get { lock (m_lock_Flag_Conv_OP60_Sending) { return m_bFlag_Conv_OP60_Sending; } } }
        public bool Flag_Conv_OP70_Sending_b { set { lock (m_lock_Flag_Conv_OP70_Sending) { m_bFlag_Conv_OP70_Sending = value; } } get { lock (m_lock_Flag_Conv_OP70_Sending) { return m_bFlag_Conv_OP70_Sending; } } }
        public bool Flag_Conv_OP80_Sending_b { set { lock (m_lock_Flag_Conv_OP80_Sending) { m_bFlag_Conv_OP80_Sending = value; } } get { lock (m_lock_Flag_Conv_OP80_Sending) { return m_bFlag_Conv_OP80_Sending; } } }
        public bool Flag_Conv_OP60_Trigger_b { set { lock (m_lock_Flag_Conv_OP60_Trigger) { m_bFlag_Conv_OP60_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP60_Trigger) { return m_bFlag_Conv_OP60_Trigger; } } }
        public bool Flag_Conv_OP70_Trigger_b { set { lock (m_lock_Flag_Conv_OP70_Trigger) { m_bFlag_Conv_OP70_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP70_Trigger) { return m_bFlag_Conv_OP70_Trigger; } } }
        public bool Flag_Conv_OP80_Trigger_b { set { lock (m_lock_Flag_Conv_OP80_Trigger) { m_bFlag_Conv_OP80_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP80_Trigger) { return m_bFlag_Conv_OP80_Trigger; } } }
        public bool Flag_Station_OP60_Done_b { set { lock (m_lock_Flag_Station_OP60_Done) { m_bFlag_Station_OP60_Done = value; } } get { lock (m_lock_Flag_Station_OP60_Done) { return m_bFlag_Station_OP60_Done; } } }
        public bool Flag_Station_OP70_Done_b { set { lock (m_lock_Flag_Station_OP70_Done) { m_bFlag_Station_OP70_Done = value; } } get { lock (m_lock_Flag_Station_OP70_Done) { return m_bFlag_Station_OP70_Done; } } }
        public bool Flag_Station_OP80_Done_b { set { lock (m_lock_Flag_Station_OP80_Done) { m_bFlag_Station_OP80_Done = value; } } get { lock (m_lock_Flag_Station_OP80_Done) { return m_bFlag_Station_OP80_Done; } } }

        //OP90 Wire Tester Results
        private readonly object m_lock_OP90_WT_Result_LN1 = new object();
        String m_str_OP90_WT_Result_LN1 = "";
        public String OP90_WT_Result_LN1_str { set { lock (m_lock_OP90_WT_Result_LN1) { m_str_OP90_WT_Result_LN1 = value; } } get { lock (m_lock_OP90_WT_Result_LN1) { return m_str_OP90_WT_Result_LN1; } } }

        private readonly object m_lock_OP90_WT_Result_LN2 = new object();
        String m_str_OP90_WT_Result_LN2 = "";
        public String OP90_WT_Result_LN2_str { set { lock (m_lock_OP90_WT_Result_LN2) { m_str_OP90_WT_Result_LN2 = value; } } get { lock (m_lock_OP90_WT_Result_LN2) { return m_str_OP90_WT_Result_LN2; } } }

        private readonly object m_lock_OP90_WT_Result_LN3 = new object();
        String m_str_OP90_WT_Result_LN3 = "";
        public String OP90_WT_Result_LN3_str { set { lock (m_lock_OP90_WT_Result_LN3) { m_str_OP90_WT_Result_LN3 = value; } } get { lock (m_lock_OP90_WT_Result_LN3) { return m_str_OP90_WT_Result_LN3; } } }

        private readonly object m_lock_OP90_WT_Result_LR0 = new object();
        String m_str_OP90_WT_Result_LR0 = "";
        public String OP90_WT_Result_LR0_str { set { lock (m_lock_OP90_WT_Result_LR0) { m_str_OP90_WT_Result_LR0 = value; } } get { lock (m_lock_OP90_WT_Result_LR0) { return m_str_OP90_WT_Result_LR0; } } }

        private readonly object m_lock_OP90_WT_Result_R0R1 = new object();
        String m_str_OP90_WT_Result_R0R1 = "";
        public String OP90_WT_Result_R0R1_str { set { lock (m_lock_OP90_WT_Result_R0R1) { m_str_OP90_WT_Result_R0R1 = value; } } get { lock (m_lock_OP90_WT_Result_R0R1) { return m_str_OP90_WT_Result_R0R1; } } }

        private readonly object m_lock_OP90_WT_Result_Temperature = new object();
        double m_db_OP90_WT_Result_Temperature = 0;
        public double OP90_WT_Result_Temperature_db { set { lock (m_lock_OP90_WT_Result_Temperature) { m_db_OP90_WT_Result_Temperature = value; } } get { lock (m_lock_OP90_WT_Result_Temperature) { return m_db_OP90_WT_Result_Temperature; } } }

        private readonly object m_lock_OP90_WT_Result_LN_Average = new object();
        String m_str_OP90_WT_Result_LN_Average = "";
        public String OP90_WT_Result_LN_Average_str { set { lock (m_lock_OP90_WT_Result_LN_Average) { m_str_OP90_WT_Result_LN_Average = value; } } get { lock (m_lock_OP90_WT_Result_LN_Average) { return m_str_OP90_WT_Result_LN_Average; } } }

        private readonly object m_lock_OP90_WT_Result_HTR_Min = new object();
        String m_str_OP90_WT_Result_HTR_Min = "";
        public String OP90_WT_Result_HTR_Min_str { set { lock (m_lock_OP90_WT_Result_HTR_Min) { m_str_OP90_WT_Result_HTR_Min = value; } } get { lock (m_lock_OP90_WT_Result_HTR_Min) { return m_str_OP90_WT_Result_HTR_Min; } } }

        private readonly object m_lock_OP90_WT_Result_HTR_Avg = new object();
        String m_str_OP90_WT_Result_HTR_Avg = "";
        public String OP90_WT_Result_HTR_Avg_str { set { lock (m_lock_OP90_WT_Result_HTR_Avg) { m_str_OP90_WT_Result_HTR_Avg = value; } } get { lock (m_lock_OP90_WT_Result_HTR_Avg) { return m_str_OP90_WT_Result_HTR_Avg; } } }

        private readonly object m_lock_OP90_WT_Result_HTR_Max = new object();
        String m_str_OP90_WT_Result_HTR_Max = "";
        public String OP90_WT_Result_HTR_Max_str { set { lock (m_lock_OP90_WT_Result_HTR_Max) { m_str_OP90_WT_Result_HTR_Max = value; } } get { lock (m_lock_OP90_WT_Result_HTR_Max) { return m_str_OP90_WT_Result_HTR_Max; } } }

        private readonly object m_lock_OP90_WT_Result_HTR_Result = new object();
        String m_str_OP90_WT_Result_HTR_Result = "";
        public String OP90_WT_Result_HTR_Result_str { set { lock (m_lock_OP90_WT_Result_HTR_Result) { m_str_OP90_WT_Result_HTR_Result = value; } } get { lock (m_lock_OP90_WT_Result_HTR_Result) { return m_str_OP90_WT_Result_HTR_Result; } } }

        private readonly object m_lock_OP90_WT_Result_HTR_Grade = new object();
        String m_str_OP90_WT_Result_HTR_Grade = "";
        public String OP90_WT_Result_HTR_Grade_str { set { lock (m_lock_OP90_WT_Result_HTR_Grade) { m_str_OP90_WT_Result_HTR_Grade = value; } } get { lock (m_lock_OP90_WT_Result_HTR_Grade) { return m_str_OP90_WT_Result_HTR_Grade; } } }

        private readonly object m_lock_OP90_WT_Result_RTD_Min = new object();
        String m_str_OP90_WT_Result_RTD_Min = "";
        public String OP90_WT_Result_RTD_Min_str { set { lock (m_lock_OP90_WT_Result_RTD_Min) { m_str_OP90_WT_Result_RTD_Min = value; } } get { lock (m_lock_OP90_WT_Result_RTD_Min) { return m_str_OP90_WT_Result_RTD_Min; } } }

        private readonly object m_lock_OP90_WT_Result_RTD_Avg = new object();
        String m_str_OP90_WT_Result_RTD_Avg = "";
        public String OP90_WT_Result_RTD_Avg_str { set { lock (m_lock_OP90_WT_Result_RTD_Avg) { m_str_OP90_WT_Result_RTD_Avg = value; } } get { lock (m_lock_OP90_WT_Result_RTD_Avg) { return m_str_OP90_WT_Result_RTD_Avg; } } }

        private readonly object m_lock_OP90_WT_Result_RTD_Max = new object();
        String m_str_OP90_WT_Result_RTD_Max = "";
        public String OP90_WT_Result_RTD_Max_str { set { lock (m_lock_OP90_WT_Result_RTD_Max) { m_str_OP90_WT_Result_RTD_Max = value; } } get { lock (m_lock_OP90_WT_Result_RTD_Max) { return m_str_OP90_WT_Result_RTD_Max; } } }

        private readonly object m_lock_OP90_WT_Result_RTD_Result = new object();
        String m_str_OP90_WT_Result_RTD_Result = "";
        public String OP90_WT_Result_RTD_Result_str { set { lock (m_lock_OP90_WT_Result_RTD_Result) { m_str_OP90_WT_Result_RTD_Result = value; } } get { lock (m_lock_OP90_WT_Result_RTD_Result) { return m_str_OP90_WT_Result_RTD_Result; } } }

        private readonly object m_lock_OP90_WT_Result_RTD_Grade = new object();
        String m_str_OP90_WT_Result_RTD_Grade = "";
        public String OP90_WT_Result_RTD_Grade_str { set { lock (m_lock_OP90_WT_Result_RTD_Grade) { m_str_OP90_WT_Result_RTD_Grade = value; } } get { lock (m_lock_OP90_WT_Result_RTD_Grade) { return m_str_OP90_WT_Result_RTD_Grade; } } }

        private readonly object m_lock_OP90_WT_Result_Overall_Result = new object();
        String m_str_OP90_WT_Result_Overall_Result = "";
        public String OP90_WT_Result_Overall_Result_str { set { lock (m_lock_OP90_WT_Result_Overall_Result) { m_str_OP90_WT_Result_Overall_Result = value; } } get { lock (m_lock_OP90_WT_Result_Overall_Result) { return m_str_OP90_WT_Result_Overall_Result; } } }

        private readonly object m_lock_OP90_WT_Result_Overall_Grade = new object();
        String m_str_OP90_WT_Result_Overall_Grade = "";
        public String OP90_WT_Result_Overall_Grade_str { set { lock (m_lock_OP90_WT_Result_Overall_Grade) { m_str_OP90_WT_Result_Overall_Grade = value; } } get { lock (m_lock_OP90_WT_Result_Overall_Grade) { return m_str_OP90_WT_Result_Overall_Grade; } } }

        //OP100 Wire Tester Results
        private readonly object m_lock_OP100_WT_Result_OP90_LN1 = new object();
        String m_str_OP100_WT_Result_OP90_LN1 = "";
        public String OP100_WT_Result_OP90_LN1_str { set { lock (m_lock_OP100_WT_Result_OP90_LN1) { m_str_OP100_WT_Result_OP90_LN1 = value; } } get { lock (m_lock_OP100_WT_Result_OP90_LN1) { return m_str_OP100_WT_Result_OP90_LN1; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_LN2 = new object();
        String m_str_OP100_WT_Result_OP90_LN2 = "";
        public String OP100_WT_Result_OP90_LN2_str { set { lock (m_lock_OP100_WT_Result_OP90_LN2) { m_str_OP100_WT_Result_OP90_LN2 = value; } } get { lock (m_lock_OP100_WT_Result_OP90_LN2) { return m_str_OP100_WT_Result_OP90_LN2; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_LN3 = new object();
        String m_str_OP100_WT_Result_OP90_LN3 = "";
        public String OP100_WT_Result_OP90_LN3_str { set { lock (m_lock_OP100_WT_Result_OP90_LN3) { m_str_OP100_WT_Result_OP90_LN3 = value; } } get { lock (m_lock_OP100_WT_Result_OP90_LN3) { return m_str_OP100_WT_Result_OP90_LN3; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_LR0 = new object();
        String m_str_OP100_WT_Result_OP90_LR0 = "";
        public String OP100_WT_Result_OP90_LR0_str { set { lock (m_lock_OP100_WT_Result_OP90_LR0) { m_str_OP100_WT_Result_OP90_LR0 = value; } } get { lock (m_lock_OP100_WT_Result_OP90_LR0) { return m_str_OP100_WT_Result_OP90_LR0; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_R0R1 = new object();
        String m_str_OP100_WT_Result_OP90_R0R1 = "";
        public String OP100_WT_Result_OP90_R0R1_str { set { lock (m_lock_OP100_WT_Result_OP90_R0R1) { m_str_OP100_WT_Result_OP90_R0R1 = value; } } get { lock (m_lock_OP100_WT_Result_OP90_R0R1) { return m_str_OP100_WT_Result_OP90_R0R1; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_Temperature = new object();
        double m_db_OP100_WT_Result_OP90_Temperature = 0;
        public double OP100_WT_Result_OP90_Temperature_db { set { lock (m_lock_OP100_WT_Result_OP90_Temperature) { m_db_OP100_WT_Result_OP90_Temperature = value; } } get { lock (m_lock_OP100_WT_Result_OP90_Temperature) { return m_db_OP100_WT_Result_OP90_Temperature; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_LN_Average = new object();
        String m_str_OP100_WT_Result_OP90_LN_Average = "";
        public String OP100_WT_Result_OP90_LN_Average_str { set { lock (m_lock_OP100_WT_Result_OP90_LN_Average) { m_str_OP100_WT_Result_OP90_LN_Average = value; } } get { lock (m_lock_OP100_WT_Result_OP90_LN_Average) { return m_str_OP100_WT_Result_OP90_LN_Average; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_HTR_Min = new object();
        String m_str_OP100_WT_Result_OP90_HTR_Min = "";
        public String OP100_WT_Result_OP90_HTR_Min_str { set { lock (m_lock_OP100_WT_Result_OP90_HTR_Min) { m_str_OP100_WT_Result_OP90_HTR_Min = value; } } get { lock (m_lock_OP100_WT_Result_OP90_HTR_Min) { return m_str_OP100_WT_Result_OP90_HTR_Min; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_HTR_Avg = new object();
        String m_str_OP100_WT_Result_OP90_HTR_Avg = "";
        public String OP100_WT_Result_OP90_HTR_Avg_str { set { lock (m_lock_OP100_WT_Result_OP90_HTR_Avg) { m_str_OP100_WT_Result_OP90_HTR_Avg = value; } } get { lock (m_lock_OP100_WT_Result_OP90_HTR_Avg) { return m_str_OP100_WT_Result_OP90_HTR_Avg; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_HTR_Max = new object();
        String m_str_OP100_WT_Result_OP90_HTR_Max = "";
        public String OP100_WT_Result_OP90_HTR_Max_str { set { lock (m_lock_OP100_WT_Result_OP90_HTR_Max) { m_str_OP100_WT_Result_OP90_HTR_Max = value; } } get { lock (m_lock_OP100_WT_Result_OP90_HTR_Max) { return m_str_OP100_WT_Result_OP90_HTR_Max; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_HTR_Result = new object();
        String m_str_OP100_WT_Result_OP90_HTR_Result = "";
        public String OP100_WT_Result_OP90_HTR_Result_str { set { lock (m_lock_OP100_WT_Result_OP90_HTR_Result) { m_str_OP100_WT_Result_OP90_HTR_Result = value; } } get { lock (m_lock_OP100_WT_Result_OP90_HTR_Result) { return m_str_OP100_WT_Result_OP90_HTR_Result; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_HTR_Grade = new object();
        String m_str_OP100_WT_Result_OP90_HTR_Grade = "";
        public String OP100_WT_Result_OP90_HTR_Grade_str { set { lock (m_lock_OP100_WT_Result_OP90_HTR_Grade) { m_str_OP100_WT_Result_OP90_HTR_Grade = value; } } get { lock (m_lock_OP100_WT_Result_OP90_HTR_Grade) { return m_str_OP100_WT_Result_OP90_HTR_Grade; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_RTD_Min = new object();
        String m_str_OP100_WT_Result_OP90_RTD_Min = "";
        public String OP100_WT_Result_OP90_RTD_Min_str { set { lock (m_lock_OP100_WT_Result_OP90_RTD_Min) { m_str_OP100_WT_Result_OP90_RTD_Min = value; } } get { lock (m_lock_OP100_WT_Result_OP90_RTD_Min) { return m_str_OP100_WT_Result_OP90_RTD_Min; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_RTD_Avg = new object();
        String m_str_OP100_WT_Result_OP90_RTD_Avg = "";
        public String OP100_WT_Result_OP90_RTD_Avg_str { set { lock (m_lock_OP100_WT_Result_OP90_RTD_Avg) { m_str_OP100_WT_Result_OP90_RTD_Avg = value; } } get { lock (m_lock_OP100_WT_Result_OP90_RTD_Avg) { return m_str_OP100_WT_Result_OP90_RTD_Avg; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_RTD_Max = new object();
        String m_str_OP100_WT_Result_OP90_RTD_Max = "";
        public String OP100_WT_Result_OP90_RTD_Max_str { set { lock (m_lock_OP100_WT_Result_OP90_RTD_Max) { m_str_OP100_WT_Result_OP90_RTD_Max = value; } } get { lock (m_lock_OP100_WT_Result_OP90_RTD_Max) { return m_str_OP100_WT_Result_OP90_RTD_Max; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_RTD_Result = new object();
        String m_str_OP100_WT_Result_OP90_RTD_Result = "";
        public String OP100_WT_Result_OP90_RTD_Result_str { set { lock (m_lock_OP100_WT_Result_OP90_RTD_Result) { m_str_OP100_WT_Result_OP90_RTD_Result = value; } } get { lock (m_lock_OP100_WT_Result_OP90_RTD_Result) { return m_str_OP100_WT_Result_OP90_RTD_Result; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_RTD_Grade = new object();
        String m_str_OP100_WT_Result_OP90_RTD_Grade = "";
        public String OP100_WT_Result_OP90_RTD_Grade_str { set { lock (m_lock_OP100_WT_Result_OP90_RTD_Grade) { m_str_OP100_WT_Result_OP90_RTD_Grade = value; } } get { lock (m_lock_OP100_WT_Result_OP90_RTD_Grade) { return m_str_OP100_WT_Result_OP90_RTD_Grade; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_Overall_Result = new object();
        String m_str_OP100_WT_Result_OP90_Overall_Result = "";
        public String OP100_WT_Result_OP90_Overall_Result_str { set { lock (m_lock_OP100_WT_Result_OP90_Overall_Result) { m_str_OP100_WT_Result_OP90_Overall_Result = value; } } get { lock (m_lock_OP100_WT_Result_OP90_Overall_Result) { return m_str_OP100_WT_Result_OP90_Overall_Result; } } }

        private readonly object m_lock_OP100_WT_Result_OP90_Overall_Grade = new object();
        String m_str_OP100_WT_Result_OP90_Overall_Grade = "";
        public String OP100_WT_Result_OP90_Overall_Grade_str { set { lock (m_lock_OP100_WT_Result_OP90_Overall_Grade) { m_str_OP100_WT_Result_OP90_Overall_Grade = value; } } get { lock (m_lock_OP100_WT_Result_OP90_Overall_Grade) { return m_str_OP100_WT_Result_OP90_Overall_Grade; } } }

        //OP90 Vision Result
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal1 = new object();
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal2 = new object();
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal3 = new object();
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal4 = new object();
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal5 = new object();
        private readonly object m_lock_OP90_VisionResult_OP85_Terminal6 = new object();
        String m_strOP90_VisionResult_OP85_Terminal1 = "";
        String m_strOP90_VisionResult_OP85_Terminal2 = "";
        String m_strOP90_VisionResult_OP85_Terminal3 = "";
        String m_strOP90_VisionResult_OP85_Terminal4 = "";
        String m_strOP90_VisionResult_OP85_Terminal5 = "";
        String m_strOP90_VisionResult_OP85_Terminal6 = "";
        public String OP90_VisionResult_OP85_Terminal1_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal1) { m_strOP90_VisionResult_OP85_Terminal1 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal1) { return m_strOP90_VisionResult_OP85_Terminal1; } } }
        public String OP90_VisionResult_OP85_Terminal2_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal2) { m_strOP90_VisionResult_OP85_Terminal2 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal2) { return m_strOP90_VisionResult_OP85_Terminal2; } } }
        public String OP90_VisionResult_OP85_Terminal3_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal3) { m_strOP90_VisionResult_OP85_Terminal3 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal3) { return m_strOP90_VisionResult_OP85_Terminal3; } } }
        public String OP90_VisionResult_OP85_Terminal4_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal4) { m_strOP90_VisionResult_OP85_Terminal4 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal4) { return m_strOP90_VisionResult_OP85_Terminal4; } } }
        public String OP90_VisionResult_OP85_Terminal5_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal5) { m_strOP90_VisionResult_OP85_Terminal5 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal5) { return m_strOP90_VisionResult_OP85_Terminal5; } } }
        public String OP90_VisionResult_OP85_Terminal6_str { set { lock (m_lock_OP90_VisionResult_OP85_Terminal6) { m_strOP90_VisionResult_OP85_Terminal6 = value; } } get { lock (m_lock_OP90_VisionResult_OP85_Terminal6) { return m_strOP90_VisionResult_OP85_Terminal6; } } }

        private readonly object m_lock_OP90_VisionResult_WireFront1 = new object();
        private readonly object m_lock_OP90_VisionResult_WireFront2 = new object();
        private readonly object m_lock_OP90_VisionResult_WireFront3 = new object();
        private readonly object m_lock_OP90_VisionResult_WireFront4 = new object();
        private readonly object m_lock_OP90_VisionResult_WireFront5 = new object();
        private readonly object m_lock_OP90_VisionResult_WireFront6 = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack1  = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack2  = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack3  = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack4  = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack5  = new object();
        private readonly object m_lock_OP90_VisionResult_WireBack6  = new object();
        private readonly object m_lock_OP90_VisionResult_Wire1      = new object();
        private readonly object m_lock_OP90_VisionResult_Wire2      = new object();
        private readonly object m_lock_OP90_VisionResult_Wire3      = new object();
        private readonly object m_lock_OP90_VisionResult_Wire4      = new object();
        private readonly object m_lock_OP90_VisionResult_Wire5      = new object();
        private readonly object m_lock_OP90_VisionResult_Wire6      = new object();

        String m_strOP90_VisionResult_WireFront1 = "";
        String m_strOP90_VisionResult_WireFront2 = "";
        String m_strOP90_VisionResult_WireFront3 = "";
        String m_strOP90_VisionResult_WireFront4 = "";
        String m_strOP90_VisionResult_WireFront5 = "";
        String m_strOP90_VisionResult_WireFront6 = "";
        String m_strOP90_VisionResult_WireBack1  = "";
        String m_strOP90_VisionResult_WireBack2  = "";
        String m_strOP90_VisionResult_WireBack3  = "";
        String m_strOP90_VisionResult_WireBack4  = "";
        String m_strOP90_VisionResult_WireBack5  = "";
        String m_strOP90_VisionResult_WireBack6  = "";
        String m_strOP90_VisionResult_Wire1      = "";
        String m_strOP90_VisionResult_Wire2      = "";
        String m_strOP90_VisionResult_Wire3      = "";
        String m_strOP90_VisionResult_Wire4      = "";
        String m_strOP90_VisionResult_Wire5      = "";
        String m_strOP90_VisionResult_Wire6      = "";

		public String OP90_VisionResult_WireFront1_str { set { lock (m_lock_OP90_VisionResult_WireFront1) { m_strOP90_VisionResult_WireFront1 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront1) { return m_strOP90_VisionResult_WireFront1; } } }
		public String OP90_VisionResult_WireFront2_str { set { lock (m_lock_OP90_VisionResult_WireFront2) { m_strOP90_VisionResult_WireFront2 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront2) { return m_strOP90_VisionResult_WireFront2; } } }
		public String OP90_VisionResult_WireFront3_str { set { lock (m_lock_OP90_VisionResult_WireFront3) { m_strOP90_VisionResult_WireFront3 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront3) { return m_strOP90_VisionResult_WireFront3; } } }
		public String OP90_VisionResult_WireFront4_str { set { lock (m_lock_OP90_VisionResult_WireFront4) { m_strOP90_VisionResult_WireFront4 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront4) { return m_strOP90_VisionResult_WireFront4; } } }
		public String OP90_VisionResult_WireFront5_str { set { lock (m_lock_OP90_VisionResult_WireFront5) { m_strOP90_VisionResult_WireFront5 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront5) { return m_strOP90_VisionResult_WireFront5; } } }
		public String OP90_VisionResult_WireFront6_str { set { lock (m_lock_OP90_VisionResult_WireFront6) { m_strOP90_VisionResult_WireFront6 = value; } } get { lock (m_lock_OP90_VisionResult_WireFront6) { return m_strOP90_VisionResult_WireFront6; } } }
		public String OP90_VisionResult_WireBack1_str  { set { lock (m_lock_OP90_VisionResult_WireBack1 ) { m_strOP90_VisionResult_WireBack1  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack1 ) { return m_strOP90_VisionResult_WireBack1 ; } } }
		public String OP90_VisionResult_WireBack2_str  { set { lock (m_lock_OP90_VisionResult_WireBack2 ) { m_strOP90_VisionResult_WireBack2  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack2 ) { return m_strOP90_VisionResult_WireBack2 ; } } }
		public String OP90_VisionResult_WireBack3_str  { set { lock (m_lock_OP90_VisionResult_WireBack3 ) { m_strOP90_VisionResult_WireBack3  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack3 ) { return m_strOP90_VisionResult_WireBack3 ; } } }
		public String OP90_VisionResult_WireBack4_str  { set { lock (m_lock_OP90_VisionResult_WireBack4 ) { m_strOP90_VisionResult_WireBack4  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack4 ) { return m_strOP90_VisionResult_WireBack4 ; } } }
		public String OP90_VisionResult_WireBack5_str  { set { lock (m_lock_OP90_VisionResult_WireBack5 ) { m_strOP90_VisionResult_WireBack5  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack5 ) { return m_strOP90_VisionResult_WireBack5 ; } } }
		public String OP90_VisionResult_WireBack6_str  { set { lock (m_lock_OP90_VisionResult_WireBack6 ) { m_strOP90_VisionResult_WireBack6  = value; } } get { lock (m_lock_OP90_VisionResult_WireBack6 ) { return m_strOP90_VisionResult_WireBack6 ; } } }
        public String OP90_VisionResult_Wire1_str      { set { lock (m_lock_OP90_VisionResult_Wire1     ) { m_strOP90_VisionResult_Wire1      = value; } } get { lock (m_lock_OP90_VisionResult_Wire1     ) { return m_strOP90_VisionResult_Wire1     ; } } }
        public String OP90_VisionResult_Wire2_str      { set { lock (m_lock_OP90_VisionResult_Wire2     ) { m_strOP90_VisionResult_Wire2      = value; } } get { lock (m_lock_OP90_VisionResult_Wire2     ) { return m_strOP90_VisionResult_Wire2     ; } } }
        public String OP90_VisionResult_Wire3_str      { set { lock (m_lock_OP90_VisionResult_Wire3     ) { m_strOP90_VisionResult_Wire3      = value; } } get { lock (m_lock_OP90_VisionResult_Wire3     ) { return m_strOP90_VisionResult_Wire3     ; } } }
        public String OP90_VisionResult_Wire4_str      { set { lock (m_lock_OP90_VisionResult_Wire4     ) { m_strOP90_VisionResult_Wire4      = value; } } get { lock (m_lock_OP90_VisionResult_Wire4     ) { return m_strOP90_VisionResult_Wire4     ; } } }
        public String OP90_VisionResult_Wire5_str      { set { lock (m_lock_OP90_VisionResult_Wire5     ) { m_strOP90_VisionResult_Wire5      = value; } } get { lock (m_lock_OP90_VisionResult_Wire5     ) { return m_strOP90_VisionResult_Wire5     ; } } }
        public String OP90_VisionResult_Wire6_str      { set { lock (m_lock_OP90_VisionResult_Wire6     ) { m_strOP90_VisionResult_Wire6      = value; } } get { lock (m_lock_OP90_VisionResult_Wire6     ) { return m_strOP90_VisionResult_Wire6     ; } } }

        //OP100 Vision Result
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal1 = new object();
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal2 = new object();
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal3 = new object();
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal4 = new object();
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal5 = new object();
        private readonly object m_lock_OP100_VisionResult_OP85_Terminal6 = new object();
        String m_strOP100_VisionResult_OP85_Terminal1 = "";
        String m_strOP100_VisionResult_OP85_Terminal2 = "";
        String m_strOP100_VisionResult_OP85_Terminal3 = "";
        String m_strOP100_VisionResult_OP85_Terminal4 = "";
        String m_strOP100_VisionResult_OP85_Terminal5 = "";
        String m_strOP100_VisionResult_OP85_Terminal6 = "";
        public String OP100_VisionResult_OP85_Terminal1_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal1) { m_strOP100_VisionResult_OP85_Terminal1 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal1) { return m_strOP100_VisionResult_OP85_Terminal1; } } }
        public String OP100_VisionResult_OP85_Terminal2_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal2) { m_strOP100_VisionResult_OP85_Terminal2 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal2) { return m_strOP100_VisionResult_OP85_Terminal2; } } }
        public String OP100_VisionResult_OP85_Terminal3_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal3) { m_strOP100_VisionResult_OP85_Terminal3 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal3) { return m_strOP100_VisionResult_OP85_Terminal3; } } }
        public String OP100_VisionResult_OP85_Terminal4_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal4) { m_strOP100_VisionResult_OP85_Terminal4 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal4) { return m_strOP100_VisionResult_OP85_Terminal4; } } }
        public String OP100_VisionResult_OP85_Terminal5_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal5) { m_strOP100_VisionResult_OP85_Terminal5 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal5) { return m_strOP100_VisionResult_OP85_Terminal5; } } }
        public String OP100_VisionResult_OP85_Terminal6_str { set { lock (m_lock_OP100_VisionResult_OP85_Terminal6) { m_strOP100_VisionResult_OP85_Terminal6 = value; } } get { lock (m_lock_OP100_VisionResult_OP85_Terminal6) { return m_strOP100_VisionResult_OP85_Terminal6; } } }
        
        private readonly object m_lock_OP100_VisionResult_OP90_Wire1 = new object();
        private readonly object m_lock_OP100_VisionResult_OP90_Wire2 = new object();
        private readonly object m_lock_OP100_VisionResult_OP90_Wire3 = new object();
        private readonly object m_lock_OP100_VisionResult_OP90_Wire4 = new object();
        private readonly object m_lock_OP100_VisionResult_OP90_Wire5 = new object();
        private readonly object m_lock_OP100_VisionResult_OP90_Wire6 = new object();
        String m_strOP100_VisionResult_OP90_Wire1 = "";
        String m_strOP100_VisionResult_OP90_Wire2 = "";
        String m_strOP100_VisionResult_OP90_Wire3 = "";
        String m_strOP100_VisionResult_OP90_Wire4 = "";
        String m_strOP100_VisionResult_OP90_Wire5 = "";
        String m_strOP100_VisionResult_OP90_Wire6 = "";
        public String OP100_VisionResult_OP90_Wire1_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire1) { m_strOP100_VisionResult_OP90_Wire1 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire1) { return m_strOP100_VisionResult_OP90_Wire1; } } }
        public String OP100_VisionResult_OP90_Wire2_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire2) { m_strOP100_VisionResult_OP90_Wire2 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire2) { return m_strOP100_VisionResult_OP90_Wire2; } } }
        public String OP100_VisionResult_OP90_Wire3_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire3) { m_strOP100_VisionResult_OP90_Wire3 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire3) { return m_strOP100_VisionResult_OP90_Wire3; } } }
        public String OP100_VisionResult_OP90_Wire4_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire4) { m_strOP100_VisionResult_OP90_Wire4 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire4) { return m_strOP100_VisionResult_OP90_Wire4; } } }
        public String OP100_VisionResult_OP90_Wire5_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire5) { m_strOP100_VisionResult_OP90_Wire5 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire5) { return m_strOP100_VisionResult_OP90_Wire5; } } }
        public String OP100_VisionResult_OP90_Wire6_str { set { lock (m_lock_OP100_VisionResult_OP90_Wire6) { m_strOP100_VisionResult_OP90_Wire6 = value; } } get { lock (m_lock_OP100_VisionResult_OP90_Wire6) { return m_strOP100_VisionResult_OP90_Wire6; } } }

        //rework flags
        private readonly object m_lock_Flag_Rework_Insert = new object();
        bool m_bFlag_Rework_Insert = false;
        public bool Flag_Rework_Insert_b { set { lock (m_lock_Flag_Rework_Insert) { m_bFlag_Rework_Insert = value; } } get { lock (m_lock_Flag_Rework_Insert) { return m_bFlag_Rework_Insert; } } }

        private readonly object m_lock_Flag_Rework_AwaitPallet = new object();
        bool m_bFlag_Rework_AwaitPallet = false;
        public bool Flag_Rework_AwaitPallet_b { set { lock (m_lock_Flag_Rework_AwaitPallet) { m_bFlag_Rework_AwaitPallet = value; } } get { lock (m_lock_Flag_Rework_AwaitPallet) { return m_bFlag_Rework_AwaitPallet; } } }

        private readonly object m_lock_Flag_Rework_AwaitSelection = new object();
        bool m_bFlag_Rework_AwaitSelection = false;
        public bool Flag_Rework_AwaitSelection_b { set { lock (m_lock_Flag_Rework_AwaitSelection) { m_bFlag_Rework_AwaitSelection = value; } } get { lock (m_lock_Flag_Rework_AwaitSelection) { return m_bFlag_Rework_AwaitSelection; } } }

        private readonly object m_lock_Flag_Rework_Done = new object();
        bool m_bFlag_Rework_Done = false;
        public bool Flag_Rework_Done_b { set { lock (m_lock_Flag_Rework_Done) { m_bFlag_Rework_Done = value; } } get { lock (m_lock_Flag_Rework_Done) { return m_bFlag_Rework_Done; } } }

        private readonly object m_lock_Flag_Rework_Station_OP85 = new object();
        bool m_bFlag_Rework_Station_OP85 = false;
        public bool Flag_Rework_Station_OP85_b { set { lock (m_lock_Flag_Rework_Station_OP85) { m_bFlag_Rework_Station_OP85 = value; } } get { lock (m_lock_Flag_Rework_Station_OP85) { return m_bFlag_Rework_Station_OP85; } } }

        private readonly object m_lock_Flag_Rework_Station_OP90 = new object();
        bool m_bFlag_Rework_Station_OP90 = false;
        public bool Flag_Rework_Station_OP90_b { set { lock (m_lock_Flag_Rework_Station_OP90) { m_bFlag_Rework_Station_OP90 = value; } } get { lock (m_lock_Flag_Rework_Station_OP90) { return m_bFlag_Rework_Station_OP90; } } }

        private readonly object m_lock_Flag_Rework_Station_OP100 = new object();
        bool m_bFlag_Rework_Station_OP100 = false;
        public bool Flag_Rework_Station_OP100_b { set { lock (m_lock_Flag_Rework_Station_OP100) { m_bFlag_Rework_Station_OP100 = value; } } get { lock (m_lock_Flag_Rework_Station_OP100) { return m_bFlag_Rework_Station_OP100; } } }

        private readonly object m_lock_Flag_Rework_Station_OP115 = new object();
        bool m_bFlag_Rework_Station_OP115 = false;
        public bool Flag_Rework_Station_OP115_b { set { lock (m_lock_Flag_Rework_Station_OP115) { m_bFlag_Rework_Station_OP115 = value; } } get { lock (m_lock_Flag_Rework_Station_OP115) { return m_bFlag_Rework_Station_OP115; } } }

        private readonly object m_lock_Flag_Rework_Station_OP117 = new object();
        bool m_bFlag_Rework_Station_OP117 = false;
        public bool Flag_Rework_Station_OP117_b { set { lock (m_lock_Flag_Rework_Station_OP117) { m_bFlag_Rework_Station_OP117 = value; } } get { lock (m_lock_Flag_Rework_Station_OP117) { return m_bFlag_Rework_Station_OP117; } } }

        private readonly object m_lock_Flag_Rework_SeqDone = new object();
        bool m_bFlag_Rework_SeqDone = false;
        public bool Flag_Rework_SeqDone_b { set { lock (m_lock_Flag_Rework_SeqDone) { m_bFlag_Rework_SeqDone = value; } } get { lock (m_lock_Flag_Rework_SeqDone) { return m_bFlag_Rework_SeqDone; } } }

        private readonly object m_lock_Flag_Rework_SeqCounter = new object();
        int m_bFlag_Rework_SeqCounter = 1;
        public int Flag_Rework_SeqCounter_i { set { lock (m_lock_Flag_Rework_SeqCounter) { m_bFlag_Rework_SeqCounter = value; } } get { lock (m_lock_Flag_Rework_SeqCounter) { return m_bFlag_Rework_SeqCounter; } } }

        //OP85
        private readonly object m_lock_Flag_Conv_OP85_Ready = new object();
        bool m_bFlag_Conv_OP85_Ready = false;
        public bool Flag_Conv_OP85_Ready_b { set { lock (m_lock_Flag_Conv_OP85_Ready) { m_bFlag_Conv_OP85_Ready = value; } } get { lock (m_lock_Flag_Conv_OP85_Ready) { return m_bFlag_Conv_OP85_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP85_Sending = new object();
        bool m_bFlag_Conv_OP85_Sending = false;
        public bool Flag_Conv_OP85_Sending_b { set { lock (m_lock_Flag_Conv_OP85_Sending) { m_bFlag_Conv_OP85_Sending = value; } } get { lock (m_lock_Flag_Conv_OP85_Sending) { return m_bFlag_Conv_OP85_Sending; } } }

        private readonly object m_lock_Flag_Conv_OP85_Trigger = new object();
        bool m_bFlag_Conv_OP85_Trigger = false;
        public bool Flag_Conv_OP85_Trigger_b { set { lock (m_lock_Flag_Conv_OP85_Trigger) { m_bFlag_Conv_OP85_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP85_Trigger) { return m_bFlag_Conv_OP85_Trigger; } } }

        private readonly object m_lock_Flag_Station_OP85_Done = new object();
        bool m_bFlag_Station_OP85_Done = false;
        public bool Flag_Station_OP85_Done_b { set { lock (m_lock_Flag_Station_OP85_Done) { m_bFlag_Station_OP85_Done = value; } } get { lock (m_lock_Flag_Station_OP85_Done) { return m_bFlag_Station_OP85_Done; } } }

        private readonly object m_lock_OP85_Vision_Overall_Result = new object();
        string m_strOP85_Vision_Overall_Result = "";
        public string OP85_Vision_Overall_Result_str { set { lock (m_lock_OP85_Vision_Overall_Result) { m_strOP85_Vision_Overall_Result = value; } } get { lock (m_lock_OP85_Vision_Overall_Result) { return m_strOP85_Vision_Overall_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Width = new object();
        string m_strOP85_Vision_Terminal1_Width = "";
        public string OP85_Vision_Terminal1_Width_str { set { lock (m_lock_OP85_Vision_Terminal1_Width) { m_strOP85_Vision_Terminal1_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Width) { return m_strOP85_Vision_Terminal1_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Width = new object();
        string m_strOP85_Vision_Terminal2_Width = "";
        public string OP85_Vision_Terminal2_Width_str { set { lock (m_lock_OP85_Vision_Terminal2_Width) { m_strOP85_Vision_Terminal2_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Width) { return m_strOP85_Vision_Terminal2_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Width = new object();
        string m_strOP85_Vision_Terminal3_Width = "";
        public string OP85_Vision_Terminal3_Width_str { set { lock (m_lock_OP85_Vision_Terminal3_Width) { m_strOP85_Vision_Terminal3_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Width) { return m_strOP85_Vision_Terminal3_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Width = new object();
        string m_strOP85_Vision_Terminal4_Width = "";
        public string OP85_Vision_Terminal4_Width_str { set { lock (m_lock_OP85_Vision_Terminal4_Width) { m_strOP85_Vision_Terminal4_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Width) { return m_strOP85_Vision_Terminal4_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Width = new object();
        string m_strOP85_Vision_Terminal5_Width = "";
        public string OP85_Vision_Terminal5_Width_str { set { lock (m_lock_OP85_Vision_Terminal5_Width) { m_strOP85_Vision_Terminal5_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Width) { return m_strOP85_Vision_Terminal5_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Width = new object();
        string m_strOP85_Vision_Terminal6_Width = "";
        public string OP85_Vision_Terminal6_Width_str { set { lock (m_lock_OP85_Vision_Terminal6_Width) { m_strOP85_Vision_Terminal6_Width = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Width) { return m_strOP85_Vision_Terminal6_Width; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Length = new object();
        string m_strOP85_Vision_Terminal1_Length = "";
        public string OP85_Vision_Terminal1_Length_str { set { lock (m_lock_OP85_Vision_Terminal1_Length) { m_strOP85_Vision_Terminal1_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Length) { return m_strOP85_Vision_Terminal1_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Length = new object();
        string m_strOP85_Vision_Terminal2_Length = "";
        public string OP85_Vision_Terminal2_Length_str { set { lock (m_lock_OP85_Vision_Terminal2_Length) { m_strOP85_Vision_Terminal2_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Length) { return m_strOP85_Vision_Terminal2_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Length = new object();
        string m_strOP85_Vision_Terminal3_Length = "";
        public string OP85_Vision_Terminal3_Length_str { set { lock (m_lock_OP85_Vision_Terminal3_Length) { m_strOP85_Vision_Terminal3_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Length) { return m_strOP85_Vision_Terminal3_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Length = new object();
        string m_strOP85_Vision_Terminal4_Length = "";
        public string OP85_Vision_Terminal4_Length_str { set { lock (m_lock_OP85_Vision_Terminal4_Length) { m_strOP85_Vision_Terminal4_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Length) { return m_strOP85_Vision_Terminal4_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Length = new object();
        string m_strOP85_Vision_Terminal5_Length = "";
        public string OP85_Vision_Terminal5_Length_str { set { lock (m_lock_OP85_Vision_Terminal5_Length) { m_strOP85_Vision_Terminal5_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Length) { return m_strOP85_Vision_Terminal5_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Length = new object();
        string m_strOP85_Vision_Terminal6_Length = "";
        public string OP85_Vision_Terminal6_Length_str { set { lock (m_lock_OP85_Vision_Terminal6_Length) { m_strOP85_Vision_Terminal6_Length = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Length) { return m_strOP85_Vision_Terminal6_Length; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Distance = new object();
        string m_strOP85_Vision_Terminal1_Distance = "";
        public string OP85_Vision_Terminal1_Distance_str { set { lock (m_lock_OP85_Vision_Terminal1_Distance) { m_strOP85_Vision_Terminal1_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Distance) { return m_strOP85_Vision_Terminal1_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Distance = new object();
        string m_strOP85_Vision_Terminal2_Distance = "";
        public string OP85_Vision_Terminal2_Distance_str { set { lock (m_lock_OP85_Vision_Terminal2_Distance) { m_strOP85_Vision_Terminal2_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Distance) { return m_strOP85_Vision_Terminal2_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Distance = new object();
        string m_strOP85_Vision_Terminal3_Distance = "";
        public string OP85_Vision_Terminal3_Distance_str { set { lock (m_lock_OP85_Vision_Terminal3_Distance) { m_strOP85_Vision_Terminal3_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Distance) { return m_strOP85_Vision_Terminal3_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Distance = new object();
        string m_strOP85_Vision_Terminal4_Distance = "";
        public string OP85_Vision_Terminal4_Distance_str { set { lock (m_lock_OP85_Vision_Terminal4_Distance) { m_strOP85_Vision_Terminal4_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Distance) { return m_strOP85_Vision_Terminal4_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Distance = new object();
        string m_strOP85_Vision_Terminal5_Distance = "";
        public string OP85_Vision_Terminal5_Distance_str { set { lock (m_lock_OP85_Vision_Terminal5_Distance) { m_strOP85_Vision_Terminal5_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Distance) { return m_strOP85_Vision_Terminal5_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Distance = new object();
        string m_strOP85_Vision_Terminal6_Distance = "";
        public string OP85_Vision_Terminal6_Distance_str { set { lock (m_lock_OP85_Vision_Terminal6_Distance) { m_strOP85_Vision_Terminal6_Distance = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Distance) { return m_strOP85_Vision_Terminal6_Distance; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Width_Min = new object();
        string m_strOP85_Vision_Terminal1_Width_Min = "";
        public string OP85_Vision_Terminal1_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal1_Width_Min) { m_strOP85_Vision_Terminal1_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Width_Min) { return m_strOP85_Vision_Terminal1_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Width_Max = new object();
        string m_strOP85_Vision_Terminal1_Width_Max = "";
        public string OP85_Vision_Terminal1_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal1_Width_Max) { m_strOP85_Vision_Terminal1_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Width_Max) { return m_strOP85_Vision_Terminal1_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Width_Min = new object();
        string m_strOP85_Vision_Terminal2_Width_Min = "";
        public string OP85_Vision_Terminal2_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal2_Width_Min) { m_strOP85_Vision_Terminal2_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Width_Min) { return m_strOP85_Vision_Terminal2_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Width_Max = new object();
        string m_strOP85_Vision_Terminal2_Width_Max = "";
        public string OP85_Vision_Terminal2_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal2_Width_Max) { m_strOP85_Vision_Terminal2_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Width_Max) { return m_strOP85_Vision_Terminal2_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Width_Min = new object();
        string m_strOP85_Vision_Terminal3_Width_Min = "";
        public string OP85_Vision_Terminal3_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal3_Width_Min) { m_strOP85_Vision_Terminal3_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Width_Min) { return m_strOP85_Vision_Terminal3_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Width_Max = new object();
        string m_strOP85_Vision_Terminal3_Width_Max = "";
        public string OP85_Vision_Terminal3_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal3_Width_Max) { m_strOP85_Vision_Terminal3_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Width_Max) { return m_strOP85_Vision_Terminal3_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Width_Min = new object();
        string m_strOP85_Vision_Terminal4_Width_Min = "";
        public string OP85_Vision_Terminal4_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal4_Width_Min) { m_strOP85_Vision_Terminal4_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Width_Min) { return m_strOP85_Vision_Terminal4_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Width_Max = new object();
        string m_strOP85_Vision_Terminal4_Width_Max = "";
        public string OP85_Vision_Terminal4_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal4_Width_Max) { m_strOP85_Vision_Terminal4_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Width_Max) { return m_strOP85_Vision_Terminal4_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Width_Min = new object();
        string m_strOP85_Vision_Terminal5_Width_Min = "";
        public string OP85_Vision_Terminal5_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal5_Width_Min) { m_strOP85_Vision_Terminal5_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Width_Min) { return m_strOP85_Vision_Terminal5_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Width_Max = new object();
        string m_strOP85_Vision_Terminal5_Width_Max = "";
        public string OP85_Vision_Terminal5_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal5_Width_Max) { m_strOP85_Vision_Terminal5_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Width_Max) { return m_strOP85_Vision_Terminal5_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Width_Min = new object();
        string m_strOP85_Vision_Terminal6_Width_Min = "";
        public string OP85_Vision_Terminal6_Width_Min_str { set { lock (m_lock_OP85_Vision_Terminal6_Width_Min) { m_strOP85_Vision_Terminal6_Width_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Width_Min) { return m_strOP85_Vision_Terminal6_Width_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Width_Max = new object();
        string m_strOP85_Vision_Terminal6_Width_Max = "";
        public string OP85_Vision_Terminal6_Width_Max_str { set { lock (m_lock_OP85_Vision_Terminal6_Width_Max) { m_strOP85_Vision_Terminal6_Width_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Width_Max) { return m_strOP85_Vision_Terminal6_Width_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Length_Min = new object();
        string m_strOP85_Vision_Terminal1_Length_Min = "";
        public string OP85_Vision_Terminal1_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal1_Length_Min) { m_strOP85_Vision_Terminal1_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Length_Min) { return m_strOP85_Vision_Terminal1_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Length_Max = new object();
        string m_strOP85_Vision_Terminal1_Length_Max = "";
        public string OP85_Vision_Terminal1_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal1_Length_Max) { m_strOP85_Vision_Terminal1_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Length_Max) { return m_strOP85_Vision_Terminal1_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Length_Min = new object();
        string m_strOP85_Vision_Terminal2_Length_Min = "";
        public string OP85_Vision_Terminal2_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal2_Length_Min) { m_strOP85_Vision_Terminal2_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Length_Min) { return m_strOP85_Vision_Terminal2_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Length_Max = new object();
        string m_strOP85_Vision_Terminal2_Length_Max = "";
        public string OP85_Vision_Terminal2_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal2_Length_Max) { m_strOP85_Vision_Terminal2_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Length_Max) { return m_strOP85_Vision_Terminal2_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Length_Min = new object();
        string m_strOP85_Vision_Terminal3_Length_Min = "";
        public string OP85_Vision_Terminal3_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal3_Length_Min) { m_strOP85_Vision_Terminal3_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Length_Min) { return m_strOP85_Vision_Terminal3_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Length_Max = new object();
        string m_strOP85_Vision_Terminal3_Length_Max = "";
        public string OP85_Vision_Terminal3_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal3_Length_Max) { m_strOP85_Vision_Terminal3_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Length_Max) { return m_strOP85_Vision_Terminal3_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Length_Min = new object();
        string m_strOP85_Vision_Terminal4_Length_Min = "";
        public string OP85_Vision_Terminal4_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal4_Length_Min) { m_strOP85_Vision_Terminal4_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Length_Min) { return m_strOP85_Vision_Terminal4_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Length_Max = new object();
        string m_strOP85_Vision_Terminal4_Length_Max = "";
        public string OP85_Vision_Terminal4_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal4_Length_Max) { m_strOP85_Vision_Terminal4_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Length_Max) { return m_strOP85_Vision_Terminal4_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Length_Min = new object();
        string m_strOP85_Vision_Terminal5_Length_Min = "";
        public string OP85_Vision_Terminal5_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal5_Length_Min) { m_strOP85_Vision_Terminal5_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Length_Min) { return m_strOP85_Vision_Terminal5_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Length_Max = new object();
        string m_strOP85_Vision_Terminal5_Length_Max = "";
        public string OP85_Vision_Terminal5_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal5_Length_Max) { m_strOP85_Vision_Terminal5_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Length_Max) { return m_strOP85_Vision_Terminal5_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Length_Min = new object();
        string m_strOP85_Vision_Terminal6_Length_Min = "";
        public string OP85_Vision_Terminal6_Length_Min_str { set { lock (m_lock_OP85_Vision_Terminal6_Length_Min) { m_strOP85_Vision_Terminal6_Length_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Length_Min) { return m_strOP85_Vision_Terminal6_Length_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Length_Max = new object();
        string m_strOP85_Vision_Terminal6_Length_Max = "";
        public string OP85_Vision_Terminal6_Length_Max_str { set { lock (m_lock_OP85_Vision_Terminal6_Length_Max) { m_strOP85_Vision_Terminal6_Length_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Length_Max) { return m_strOP85_Vision_Terminal6_Length_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Distance_Min = new object();
        string m_strOP85_Vision_Terminal1_Distance_Min = "";
        public string OP85_Vision_Terminal1_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal1_Distance_Min) { m_strOP85_Vision_Terminal1_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Distance_Min) { return m_strOP85_Vision_Terminal1_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Distance_Max = new object();
        string m_strOP85_Vision_Terminal1_Distance_Max = "";
        public string OP85_Vision_Terminal1_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal1_Distance_Max) { m_strOP85_Vision_Terminal1_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Distance_Max) { return m_strOP85_Vision_Terminal1_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Distance_Min = new object();
        string m_strOP85_Vision_Terminal2_Distance_Min = "";
        public string OP85_Vision_Terminal2_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal2_Distance_Min) { m_strOP85_Vision_Terminal2_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Distance_Min) { return m_strOP85_Vision_Terminal2_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Distance_Max = new object();
        string m_strOP85_Vision_Terminal2_Distance_Max = "";
        public string OP85_Vision_Terminal2_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal2_Distance_Max) { m_strOP85_Vision_Terminal2_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Distance_Max) { return m_strOP85_Vision_Terminal2_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Distance_Min = new object();
        string m_strOP85_Vision_Terminal3_Distance_Min = "";
        public string OP85_Vision_Terminal3_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal3_Distance_Min) { m_strOP85_Vision_Terminal3_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Distance_Min) { return m_strOP85_Vision_Terminal3_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Distance_Max = new object();
        string m_strOP85_Vision_Terminal3_Distance_Max = "";
        public string OP85_Vision_Terminal3_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal3_Distance_Max) { m_strOP85_Vision_Terminal3_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Distance_Max) { return m_strOP85_Vision_Terminal3_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Distance_Min = new object();
        string m_strOP85_Vision_Terminal4_Distance_Min = "";
        public string OP85_Vision_Terminal4_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal4_Distance_Min) { m_strOP85_Vision_Terminal4_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Distance_Min) { return m_strOP85_Vision_Terminal4_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Distance_Max = new object();
        string m_strOP85_Vision_Terminal4_Distance_Max = "";
        public string OP85_Vision_Terminal4_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal4_Distance_Max) { m_strOP85_Vision_Terminal4_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Distance_Max) { return m_strOP85_Vision_Terminal4_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Distance_Min = new object();
        string m_strOP85_Vision_Terminal5_Distance_Min = "";
        public string OP85_Vision_Terminal5_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal5_Distance_Min) { m_strOP85_Vision_Terminal5_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Distance_Min) { return m_strOP85_Vision_Terminal5_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Distance_Max = new object();
        string m_strOP85_Vision_Terminal5_Distance_Max = "";
        public string OP85_Vision_Terminal5_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal5_Distance_Max) { m_strOP85_Vision_Terminal5_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Distance_Max) { return m_strOP85_Vision_Terminal5_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Distance_Min = new object();
        string m_strOP85_Vision_Terminal6_Distance_Min = "";
        public string OP85_Vision_Terminal6_Distance_Min_str { set { lock (m_lock_OP85_Vision_Terminal6_Distance_Min) { m_strOP85_Vision_Terminal6_Distance_Min = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Distance_Min) { return m_strOP85_Vision_Terminal6_Distance_Min; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Distance_Max = new object();
        string m_strOP85_Vision_Terminal6_Distance_Max = "";
        public string OP85_Vision_Terminal6_Distance_Max_str { set { lock (m_lock_OP85_Vision_Terminal6_Distance_Max) { m_strOP85_Vision_Terminal6_Distance_Max = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Distance_Max) { return m_strOP85_Vision_Terminal6_Distance_Max; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Width_Result = new object();
        string m_strOP85_Vision_Terminal1_Width_Result = "";
        public string OP85_Vision_Terminal1_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal1_Width_Result) { m_strOP85_Vision_Terminal1_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Width_Result) { return m_strOP85_Vision_Terminal1_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Width_Result = new object();
        string m_strOP85_Vision_Terminal2_Width_Result = "";
        public string OP85_Vision_Terminal2_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal2_Width_Result) { m_strOP85_Vision_Terminal2_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Width_Result) { return m_strOP85_Vision_Terminal2_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Width_Result = new object();
        string m_strOP85_Vision_Terminal3_Width_Result = "";
        public string OP85_Vision_Terminal3_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal3_Width_Result) { m_strOP85_Vision_Terminal3_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Width_Result) { return m_strOP85_Vision_Terminal3_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Width_Result = new object();
        string m_strOP85_Vision_Terminal4_Width_Result = "";
        public string OP85_Vision_Terminal4_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal4_Width_Result) { m_strOP85_Vision_Terminal4_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Width_Result) { return m_strOP85_Vision_Terminal4_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Width_Result = new object();
        string m_strOP85_Vision_Terminal5_Width_Result = "";
        public string OP85_Vision_Terminal5_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal5_Width_Result) { m_strOP85_Vision_Terminal5_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Width_Result) { return m_strOP85_Vision_Terminal5_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Width_Result = new object();
        string m_strOP85_Vision_Terminal6_Width_Result = "";
        public string OP85_Vision_Terminal6_Width_Result_str { set { lock (m_lock_OP85_Vision_Terminal6_Width_Result) { m_strOP85_Vision_Terminal6_Width_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Width_Result) { return m_strOP85_Vision_Terminal6_Width_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Length_Result = new object();
        string m_strOP85_Vision_Terminal1_Length_Result = "";
        public string OP85_Vision_Terminal1_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal1_Length_Result) { m_strOP85_Vision_Terminal1_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Length_Result) { return m_strOP85_Vision_Terminal1_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Length_Result = new object();
        string m_strOP85_Vision_Terminal2_Length_Result = "";
        public string OP85_Vision_Terminal2_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal2_Length_Result) { m_strOP85_Vision_Terminal2_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Length_Result) { return m_strOP85_Vision_Terminal2_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Length_Result = new object();
        string m_strOP85_Vision_Terminal3_Length_Result = "";
        public string OP85_Vision_Terminal3_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal3_Length_Result) { m_strOP85_Vision_Terminal3_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Length_Result) { return m_strOP85_Vision_Terminal3_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Length_Result = new object();
        string m_strOP85_Vision_Terminal4_Length_Result = "";
        public string OP85_Vision_Terminal4_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal4_Length_Result) { m_strOP85_Vision_Terminal4_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Length_Result) { return m_strOP85_Vision_Terminal4_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Length_Result = new object();
        string m_strOP85_Vision_Terminal5_Length_Result = "";
        public string OP85_Vision_Terminal5_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal5_Length_Result) { m_strOP85_Vision_Terminal5_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Length_Result) { return m_strOP85_Vision_Terminal5_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Length_Result = new object();
        string m_strOP85_Vision_Terminal6_Length_Result = "";
        public string OP85_Vision_Terminal6_Length_Result_str { set { lock (m_lock_OP85_Vision_Terminal6_Length_Result) { m_strOP85_Vision_Terminal6_Length_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Length_Result) { return m_strOP85_Vision_Terminal6_Length_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal1_Distance_Result = new object();
        string m_strOP85_Vision_Terminal1_Distance_Result = "";
        public string OP85_Vision_Terminal1_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal1_Distance_Result) { m_strOP85_Vision_Terminal1_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal1_Distance_Result) { return m_strOP85_Vision_Terminal1_Distance_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal2_Distance_Result = new object();
        string m_strOP85_Vision_Terminal2_Distance_Result = "";
        public string OP85_Vision_Terminal2_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal2_Distance_Result) { m_strOP85_Vision_Terminal2_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal2_Distance_Result) { return m_strOP85_Vision_Terminal2_Distance_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal3_Distance_Result = new object();
        string m_strOP85_Vision_Terminal3_Distance_Result = "";
        public string OP85_Vision_Terminal3_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal3_Distance_Result) { m_strOP85_Vision_Terminal3_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal3_Distance_Result) { return m_strOP85_Vision_Terminal3_Distance_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal4_Distance_Result = new object();
        string m_strOP85_Vision_Terminal4_Distance_Result = "";
        public string OP85_Vision_Terminal4_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal4_Distance_Result) { m_strOP85_Vision_Terminal4_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal4_Distance_Result) { return m_strOP85_Vision_Terminal4_Distance_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal5_Distance_Result = new object();
        string m_strOP85_Vision_Terminal5_Distance_Result = "";
        public string OP85_Vision_Terminal5_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal5_Distance_Result) { m_strOP85_Vision_Terminal5_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal5_Distance_Result) { return m_strOP85_Vision_Terminal5_Distance_Result; } } }

        private readonly object m_lock_OP85_Vision_Terminal6_Distance_Result = new object();
        string m_strOP85_Vision_Terminal6_Distance_Result = "";
        public string OP85_Vision_Terminal6_Distance_Result_str { set { lock (m_lock_OP85_Vision_Terminal6_Distance_Result) { m_strOP85_Vision_Terminal6_Distance_Result = value; } } get { lock (m_lock_OP85_Vision_Terminal6_Distance_Result) { return m_strOP85_Vision_Terminal6_Distance_Result; } } }

        //OP85 vision final result
        private readonly object m_lock_OP85_VisionResult_Terminal1 = new object();
        private readonly object m_lock_OP85_VisionResult_Terminal2 = new object();
        private readonly object m_lock_OP85_VisionResult_Terminal3 = new object();
        private readonly object m_lock_OP85_VisionResult_Terminal4 = new object();
        private readonly object m_lock_OP85_VisionResult_Terminal5 = new object();
        private readonly object m_lock_OP85_VisionResult_Terminal6 = new object();
        String m_strOP85_VisionResult_Terminal1 = "";
        String m_strOP85_VisionResult_Terminal2 = "";
        String m_strOP85_VisionResult_Terminal3 = "";
        String m_strOP85_VisionResult_Terminal4 = "";
        String m_strOP85_VisionResult_Terminal5 = "";
        String m_strOP85_VisionResult_Terminal6 = "";
        public String OP85_VisionResult_Terminal1_str { set { lock (m_lock_OP85_VisionResult_Terminal1) { m_strOP85_VisionResult_Terminal1 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal1) { return m_strOP85_VisionResult_Terminal1; } } }
        public String OP85_VisionResult_Terminal2_str { set { lock (m_lock_OP85_VisionResult_Terminal2) { m_strOP85_VisionResult_Terminal2 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal2) { return m_strOP85_VisionResult_Terminal2; } } }
        public String OP85_VisionResult_Terminal3_str { set { lock (m_lock_OP85_VisionResult_Terminal3) { m_strOP85_VisionResult_Terminal3 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal3) { return m_strOP85_VisionResult_Terminal3; } } }
        public String OP85_VisionResult_Terminal4_str { set { lock (m_lock_OP85_VisionResult_Terminal4) { m_strOP85_VisionResult_Terminal4 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal4) { return m_strOP85_VisionResult_Terminal4; } } }
        public String OP85_VisionResult_Terminal5_str { set { lock (m_lock_OP85_VisionResult_Terminal5) { m_strOP85_VisionResult_Terminal5 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal5) { return m_strOP85_VisionResult_Terminal5; } } }
        public String OP85_VisionResult_Terminal6_str { set { lock (m_lock_OP85_VisionResult_Terminal6) { m_strOP85_VisionResult_Terminal6 = value; } } get { lock (m_lock_OP85_VisionResult_Terminal6) { return m_strOP85_VisionResult_Terminal6; } } }

        //OP90
        private readonly object m_lock_Flag_Conv_OP90_Ready = new object();
        bool m_bFlag_Conv_OP90_Ready = false;
        public bool Flag_Conv_OP90_Ready_b { set { lock (m_lock_Flag_Conv_OP90_Ready) { m_bFlag_Conv_OP90_Ready = value; } } get { lock (m_lock_Flag_Conv_OP90_Ready) { return m_bFlag_Conv_OP90_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP90_Sending = new object();
        bool m_bFlag_Conv_OP90_Sending = false;
        public bool Flag_Conv_OP90_Sending_b { set { lock (m_lock_Flag_Conv_OP90_Sending) { m_bFlag_Conv_OP90_Sending = value; } } get { lock (m_lock_Flag_Conv_OP90_Sending) { return m_bFlag_Conv_OP90_Sending; } } }

        private readonly object m_lock_Flag_Conv_OP90_Trigger = new object();
        bool m_bFlag_Conv_OP90_Trigger = false;
        public bool Flag_Conv_OP90_Trigger_b { set { lock (m_lock_Flag_Conv_OP90_Trigger) { m_bFlag_Conv_OP90_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP90_Trigger) { return m_bFlag_Conv_OP90_Trigger; } } }

        private readonly object m_lock_Flag_Station_OP90_Done = new object();
        bool m_bFlag_Station_OP90_Done = false;
        public bool Flag_Station_OP90_Done_b { set { lock (m_lock_Flag_Station_OP90_Done) { m_bFlag_Station_OP90_Done = value; } } get { lock (m_lock_Flag_Station_OP90_Done) { return m_bFlag_Station_OP90_Done; } } }

		//OP100
        private readonly object m_lock_Flag_Conv_OP100_Buffer_Ready = new object();
        bool m_bFlag_Conv_OP100_Buffer_Ready = false;
        public bool Flag_Conv_OP100_Buffer_Ready_b { set { lock (m_lock_Flag_Conv_OP100_Buffer_Ready) { m_bFlag_Conv_OP100_Buffer_Ready = value; } } get { lock (m_lock_Flag_Conv_OP100_Buffer_Ready) { return m_bFlag_Conv_OP100_Buffer_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP100_Buffer_Sending = new object();
        bool m_bFlag_Conv_OP100_Buffer_Sending = false;
        public bool Flag_Conv_OP100_Buffer_Sending_b { set { lock (m_lock_Flag_Conv_OP100_Buffer_Sending) { m_bFlag_Conv_OP100_Buffer_Sending = value; } } get { lock (m_lock_Flag_Conv_OP100_Buffer_Sending) { return m_bFlag_Conv_OP100_Buffer_Sending; } } }

        private readonly object m_lock_Flag_Conv_OP100_Ready = new object();
        bool m_bFlag_Conv_OP100_Ready = false;
        public bool Flag_Conv_OP100_Ready_b { set { lock (m_lock_Flag_Conv_OP100_Ready) { m_bFlag_Conv_OP100_Ready = value; } } get { lock (m_lock_Flag_Conv_OP100_Ready) { return m_bFlag_Conv_OP100_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP100_Sending = new object();
        bool m_bFlag_Conv_OP100_Sending = false;
        public bool Flag_Conv_OP100_Sending_b { set { lock (m_lock_Flag_Conv_OP100_Sending) { m_bFlag_Conv_OP100_Sending = value; } } get { lock (m_lock_Flag_Conv_OP100_Sending) { return m_bFlag_Conv_OP100_Sending; } } }

        private readonly object m_lock_Flag_Conv_OP100_Trigger = new object();
        bool m_bFlag_Conv_OP100_Trigger = false;
        public bool Flag_Conv_OP100_Trigger_b { set { lock (m_lock_Flag_Conv_OP100_Trigger) { m_bFlag_Conv_OP100_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP100_Trigger) { return m_bFlag_Conv_OP100_Trigger; } } }

        private readonly object m_lock_Flag_Conv_OP100_Outgoing_Ready = new object();
        bool m_bFlag_Conv_OP100_Outgoing_Ready = false;
        public bool Flag_Conv_OP100_Outgoing_Ready_b { set { lock (m_lock_Flag_Conv_OP100_Outgoing_Ready) { m_bFlag_Conv_OP100_Outgoing_Ready = value; } } get { lock (m_lock_Flag_Conv_OP100_Outgoing_Ready) { return m_bFlag_Conv_OP100_Outgoing_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP100_Outgoing_Sending = new object();
        bool m_bFlag_Conv_OP100_Outgoing_Sending = false;
        public bool Flag_Conv_OP100_Outgoing_Sending_b { set { lock (m_lock_Flag_Conv_OP100_Outgoing_Sending) { m_bFlag_Conv_OP100_Outgoing_Sending = value; } } get { lock (m_lock_Flag_Conv_OP100_Outgoing_Sending) { return m_bFlag_Conv_OP100_Outgoing_Sending; } } }

		private readonly object m_lock_MachineTypeNum = new object();
		int m_iMachineTypeNum = 0;
		public int MachineTypeNum_i { set { lock ( m_lock_MachineTypeNum ) { m_iMachineTypeNum = value; } } get { lock ( m_lock_MachineTypeNum ) { return m_iMachineTypeNum; } } }

        private readonly object m_lock_Flag_Bypass_OP100 = new object();
		bool m_bFlag_Bypass_OP100 = false;
        public bool Flag_Bypass_OP100_b { set { lock (m_lock_Flag_Bypass_OP100) { m_bFlag_Bypass_OP100 = value; } } get { lock (m_lock_Flag_Bypass_OP100) { return m_bFlag_Bypass_OP100; } } }

        private readonly object m_lock_Flag_OP100_OpenBridge = new object();
        bool m_bFlag_OP100_OpenBridge = false;
        public bool Flag_OP100_OpenBridge_b { set { lock (m_lock_Flag_OP100_OpenBridge) { m_bFlag_OP100_OpenBridge = value; } } get { lock (m_lock_Flag_OP100_OpenBridge) { return m_bFlag_OP100_OpenBridge; } } }

		//OP110: Hot Blower
        private readonly object m_lock_Flag_Conv_OP110_Ready = new object();
        bool m_bFlag_Conv_OP110_Ready = false;
        public bool Flag_Conv_OP110_Ready_b { set { lock (m_lock_Flag_Conv_OP110_Ready) { m_bFlag_Conv_OP110_Ready = value; } } get { lock (m_lock_Flag_Conv_OP110_Ready) { return m_bFlag_Conv_OP110_Ready; } } }

        private readonly object m_lock_Flag_Conv_OP110_Sending = new object();
        bool m_bFlag_Conv_OP110_Sending = false;
        public bool Flag_Conv_OP110_Sending_b { set { lock (m_lock_Flag_Conv_OP110_Sending) { m_bFlag_Conv_OP110_Sending = value; } } get { lock (m_lock_Flag_Conv_OP110_Sending) { return m_bFlag_Conv_OP110_Sending; } } }

        private readonly object m_lock_Flag_Conv_OP110_Trigger = new object();
        bool m_bFlag_Conv_OP110_Trigger = false;
        public bool Flag_Conv_OP110_Trigger_b { set { lock (m_lock_Flag_Conv_OP110_Trigger) { m_bFlag_Conv_OP110_Trigger = value; } } get { lock (m_lock_Flag_Conv_OP110_Trigger) { return m_bFlag_Conv_OP110_Trigger; } } }

        private readonly object m_lock_Flag_Station_OP110_Done = new object();
        bool m_bFlag_Station_OP110_Done = false;
        public bool Flag_Station_OP110_Done_b { set { lock (m_lock_Flag_Station_OP110_Done) { m_bFlag_Station_OP110_Done = value; } } get { lock (m_lock_Flag_Station_OP110_Done) { return m_bFlag_Station_OP110_Done; } } }

		private readonly object m_lock_Flag_LastStation_OP110 = new object();
		int m_iFlag_LastStation_OP110 = 0;
		public int Flag_LastStation_OP110_i { set { lock ( m_lock_Flag_LastStation_OP110 ) { m_iFlag_LastStation_OP110 = value; } } get { lock ( m_lock_Flag_LastStation_OP110 ) { return m_iFlag_LastStation_OP110; } } }

        private readonly object m_lock_Flag_HotBlower_Temp = new object();			//Flag: Indicate station completed its process
        double m_db_HotBlower_Temp = 0.0;
        public double m_HotBlower_Temp_db { set { lock (m_lock_Flag_HotBlower_Temp) { m_db_HotBlower_Temp = value; } } get { lock (m_lock_Flag_HotBlower_Temp) { return m_db_HotBlower_Temp; } } }

        private readonly object m_lock_Flag_Bypass_OP110_AutoPNPSpringComb = new object();
        bool m_bFlag_Bypass_OP110_AutoPNPSpringComb = false;
        public bool Flag_Bypass_OP110_AutoPNPSpringComb_b { set { lock (m_lock_Flag_Bypass_OP110_AutoPNPSpringComb) { m_bFlag_Bypass_OP110_AutoPNPSpringComb = value; } } get { lock (m_lock_Flag_Bypass_OP110_AutoPNPSpringComb) { return m_bFlag_Bypass_OP110_AutoPNPSpringComb; } } }

		//Module Enable or Disable
        private readonly object m_lock_Flag_EnableSolderStation1 = new object();
        bool m_bFlag_EnableSolderStation1 = true;
        public bool Flag_EnableSolderStation1_b { set { lock (m_lock_Flag_EnableSolderStation1) { m_bFlag_EnableSolderStation1 = value; } } get { lock (m_lock_Flag_EnableSolderStation1) { return m_bFlag_EnableSolderStation1; } } }

        private readonly object m_lock_Flag_EnableSolderStation2 = new object();
        bool m_bFlag_EnableSolderStation2 = true;
        public bool Flag_EnableSolderStation2_b { set { lock (m_lock_Flag_EnableSolderStation2) { m_bFlag_EnableSolderStation2 = value; } } get { lock (m_lock_Flag_EnableSolderStation2) { return m_bFlag_EnableSolderStation2; } } }

        private readonly object m_lock_Flag_EnableSolderStation3 = new object();
        bool m_bFlag_EnableSolderStation3 = true;
        public bool Flag_EnableSolderStation3_b { set { lock (m_lock_Flag_EnableSolderStation3) { m_bFlag_EnableSolderStation3 = value; } } get { lock (m_lock_Flag_EnableSolderStation3) { return m_bFlag_EnableSolderStation3; } } }

        private readonly object m_lock_Flag_EnableConvSolder = new object();
        bool m_bFlag_EnableConvSolder = true;
        public bool Flag_EnableConvSolder_b { set { lock (m_lock_Flag_EnableConvSolder) { m_bFlag_EnableConvSolder = value; } } get { lock (m_lock_Flag_EnableConvSolder) { return m_bFlag_EnableConvSolder; } } }

        private readonly object m_lock_Flag_Enable_Module_OP85 = new object();
        bool m_bFlag_Enable_Module_OP85 = true;
        public bool Flag_Enable_Module_OP85_b { set { lock (m_lock_Flag_Enable_Module_OP85) { m_bFlag_Enable_Module_OP85 = value; } } get { lock (m_lock_Flag_Enable_Module_OP85) { return m_bFlag_Enable_Module_OP85; } } }

        private readonly object m_lock_Flag_Enable_Module_Conv_OP85 = new object();
        bool m_bFlag_Enable_Module_Conv_OP85 = true;
        public bool Flag_Enable_Module_Conv_OP85_b { set { lock (m_lock_Flag_Enable_Module_Conv_OP85) { m_bFlag_Enable_Module_Conv_OP85 = value; } } get { lock (m_lock_Flag_Enable_Module_Conv_OP85) { return m_bFlag_Enable_Module_Conv_OP85; } } }

        private readonly object m_lock_Flag_Enable_Module_OP90 = new object();
        bool m_bFlag_Enable_Module_OP90 = true;
        public bool Flag_Enable_Module_OP90_b { set { lock (m_lock_Flag_Enable_Module_OP90) { m_bFlag_Enable_Module_OP90 = value; } } get { lock (m_lock_Flag_Enable_Module_OP90) { return m_bFlag_Enable_Module_OP90; } } }

        private readonly object m_lock_Flag_Enable_Module_Conv_OP90 = new object();
        bool m_bFlag_Enable_Module_Conv_OP90 = true;
        public bool Flag_Enable_Module_Conv_OP90_b { set { lock (m_lock_Flag_Enable_Module_Conv_OP90) { m_bFlag_Enable_Module_Conv_OP90 = value; } } get { lock (m_lock_Flag_Enable_Module_Conv_OP90) { return m_bFlag_Enable_Module_Conv_OP90; } } }

        private readonly object m_lock_Flag_Enable_Module_FwdConv_OP85_OP90 = new object();
        bool m_bFlag_Enable_Module_FwdConv_OP85_OP90 = true;
        public bool Flag_Enable_Module_FwdConv_OP85_OP90_b { set { lock (m_lock_Flag_Enable_Module_FwdConv_OP85_OP90) { m_bFlag_Enable_Module_FwdConv_OP85_OP90 = value; } } get { lock (m_lock_Flag_Enable_Module_FwdConv_OP85_OP90) { return m_bFlag_Enable_Module_FwdConv_OP85_OP90; } } }

		private readonly object m_lock_Flag_EnableManualOP100 = new object();
        bool m_bFlag_EnableManualOP100 = true;
        public bool Flag_EnableManualOP100_b { set { lock (m_lock_Flag_EnableManualOP100) { m_bFlag_EnableManualOP100 = value; } } get { lock (m_lock_Flag_EnableManualOP100) { return m_bFlag_EnableManualOP100; } } }

        private readonly object m_lock_Flag_Enable_Module_OP110 = new object();
        bool m_bFlag_Enable_Module_OP110 = true;
        public bool Flag_Enable_Module_OP110_b { set { lock (m_lock_Flag_Enable_Module_OP110) { m_bFlag_Enable_Module_OP110 = value; } } get { lock (m_lock_Flag_Enable_Module_OP110) { return m_bFlag_Enable_Module_OP110; } } }

        private readonly object m_lock_Flag_Enable_Module_Conv_OP110 = new object();
        bool m_bFlag_Enable_Module_Conv_OP110 = true;
        public bool Flag_Enable_Module_Conv_OP110_b { set { lock (m_lock_Flag_Enable_Module_Conv_OP110) { m_bFlag_Enable_Module_Conv_OP110 = value; } } get { lock (m_lock_Flag_Enable_Module_Conv_OP110) { return m_bFlag_Enable_Module_Conv_OP110; } } }

        private readonly object m_lock_Flag_Enable_Module_FwdConv_OP110 = new object();
        bool m_bFlag_Enable_Module_FwdConv_OP110 = true;
        public bool Flag_Enable_Module_FwdConv_OP110_b { set { lock (m_lock_Flag_Enable_Module_FwdConv_OP110) { m_bFlag_Enable_Module_FwdConv_OP110 = value; } } get { lock (m_lock_Flag_Enable_Module_FwdConv_OP110) { return m_bFlag_Enable_Module_FwdConv_OP110; } } }

        private readonly object m_lock_Flag_Enable_TestMode = new object();
        bool m_bFlag_Enable_TestMode = false;
        public bool Flag_Enable_TestMode_b { set { lock (m_lock_Flag_Enable_TestMode) { m_bFlag_Enable_TestMode = value; } } get { lock (m_lock_Flag_Enable_TestMode) { return m_bFlag_Enable_TestMode; } } }

        private readonly object m_lock_Flag_EnableReverseSeq = new object();
        private readonly object m_lock_Flag_EnableCleanSolderHead = new object();

        bool m_bFlag_EnableReverseSeq = true;
        bool m_bFlag_EnableCleanSolderHead = true;

        public bool Flag_EnableReverseSeq_b { set { lock (m_lock_Flag_EnableReverseSeq) { m_bFlag_EnableReverseSeq = value; } } get { lock (m_lock_Flag_EnableReverseSeq) { return m_bFlag_EnableReverseSeq; } } }
        public bool Flag_EnableCleanSolderHead { set { lock (m_lock_Flag_EnableCleanSolderHead) { m_bFlag_EnableCleanSolderHead = value; } } get { lock (m_lock_Flag_EnableCleanSolderHead) { return m_bFlag_EnableCleanSolderHead; } } }
        

		//Data - Operations

        //Enable or Disable - Solder Point
        private readonly object m_lock_Flag_Enable_SolderPoint1 = new object();
        private readonly object m_lock_Flag_Enable_SolderPoint2 = new object();
        private readonly object m_lock_Flag_Enable_SolderPoint3 = new object();
        private readonly object m_lock_Flag_Enable_SolderPoint4 = new object();
        private readonly object m_lock_Flag_Enable_SolderPoint5 = new object();
        private readonly object m_lock_Flag_Enable_SolderPoint6 = new object();

        bool m_bFlag_Enable_SolderPoint1 = true;
        bool m_bFlag_Enable_SolderPoint2 = true;
        bool m_bFlag_Enable_SolderPoint3 = true;
        bool m_bFlag_Enable_SolderPoint4 = true;
        bool m_bFlag_Enable_SolderPoint5 = true;
        bool m_bFlag_Enable_SolderPoint6 = true;

        public bool Flag_Enable_SolderPoint1 { set { lock (m_lock_Flag_Enable_SolderPoint1) { m_bFlag_Enable_SolderPoint1 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint1) { return m_bFlag_Enable_SolderPoint1; } } }
        public bool Flag_Enable_SolderPoint2 { set { lock (m_lock_Flag_Enable_SolderPoint2) { m_bFlag_Enable_SolderPoint2 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint2) { return m_bFlag_Enable_SolderPoint2; } } }
        public bool Flag_Enable_SolderPoint3 { set { lock (m_lock_Flag_Enable_SolderPoint3) { m_bFlag_Enable_SolderPoint3 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint3) { return m_bFlag_Enable_SolderPoint3; } } }
        public bool Flag_Enable_SolderPoint4 { set { lock (m_lock_Flag_Enable_SolderPoint4) { m_bFlag_Enable_SolderPoint4 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint4) { return m_bFlag_Enable_SolderPoint4; } } }
        public bool Flag_Enable_SolderPoint5 { set { lock (m_lock_Flag_Enable_SolderPoint5) { m_bFlag_Enable_SolderPoint5 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint5) { return m_bFlag_Enable_SolderPoint5; } } }
        public bool Flag_Enable_SolderPoint6 { set { lock (m_lock_Flag_Enable_SolderPoint6) { m_bFlag_Enable_SolderPoint6 = value; } } get { lock (m_lock_Flag_Enable_SolderPoint6) { return m_bFlag_Enable_SolderPoint6; } } }

        //Enable or Disable - LV or HV Grades
        private readonly object m_lock_Flag_Enable_HTR_Grade_A_LV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_B_LV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_C_LV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_D_LV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_E_LV = new object();

        private readonly object m_lock_Flag_Enable_RTD_Grade_A_LV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_B_LV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_C_LV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_D_LV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_E_LV = new object();

        private readonly object m_lock_Flag_Enable_HTR_Grade_A_HV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_B_HV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_C_HV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_D_HV = new object();
        private readonly object m_lock_Flag_Enable_HTR_Grade_E_HV = new object();

        private readonly object m_lock_Flag_Enable_RTD_Grade_A_HV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_B_HV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_C_HV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_D_HV = new object();
        private readonly object m_lock_Flag_Enable_RTD_Grade_E_HV = new object();

        bool m_bFlag_Enable_HTR_Grade_A_LV = true;
        bool m_bFlag_Enable_HTR_Grade_B_LV = true;
        bool m_bFlag_Enable_HTR_Grade_C_LV = true;
        bool m_bFlag_Enable_HTR_Grade_D_LV = true;
        bool m_bFlag_Enable_HTR_Grade_E_LV = true;

        bool m_bFlag_Enable_RTD_Grade_A_LV = true;
        bool m_bFlag_Enable_RTD_Grade_B_LV = true;
        bool m_bFlag_Enable_RTD_Grade_C_LV = true;
        bool m_bFlag_Enable_RTD_Grade_D_LV = true;
        bool m_bFlag_Enable_RTD_Grade_E_LV = true;

        bool m_bFlag_Enable_HTR_Grade_A_HV = true;
        bool m_bFlag_Enable_HTR_Grade_B_HV = true;
        bool m_bFlag_Enable_HTR_Grade_C_HV = true;
        bool m_bFlag_Enable_HTR_Grade_D_HV = true;
        bool m_bFlag_Enable_HTR_Grade_E_HV = true;

        bool m_bFlag_Enable_RTD_Grade_A_HV = true;
        bool m_bFlag_Enable_RTD_Grade_B_HV = true;
        bool m_bFlag_Enable_RTD_Grade_C_HV = true;
        bool m_bFlag_Enable_RTD_Grade_D_HV = true;
        bool m_bFlag_Enable_RTD_Grade_E_HV = true;

        public bool Flag_Enable_HTR_Grade_A_LV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_A_LV) { m_bFlag_Enable_HTR_Grade_A_LV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_A_LV) { return m_bFlag_Enable_HTR_Grade_A_LV; } } }
        public bool Flag_Enable_HTR_Grade_B_LV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_B_LV) { m_bFlag_Enable_HTR_Grade_B_LV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_B_LV) { return m_bFlag_Enable_HTR_Grade_B_LV; } } }
        public bool Flag_Enable_HTR_Grade_C_LV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_C_LV) { m_bFlag_Enable_HTR_Grade_C_LV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_C_LV) { return m_bFlag_Enable_HTR_Grade_C_LV; } } }
        public bool Flag_Enable_HTR_Grade_D_LV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_D_LV) { m_bFlag_Enable_HTR_Grade_D_LV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_D_LV) { return m_bFlag_Enable_HTR_Grade_D_LV; } } }
        public bool Flag_Enable_HTR_Grade_E_LV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_E_LV) { m_bFlag_Enable_HTR_Grade_E_LV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_E_LV) { return m_bFlag_Enable_HTR_Grade_E_LV; } } }

        public bool Flag_Enable_RTD_Grade_A_LV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_A_LV) { m_bFlag_Enable_RTD_Grade_A_LV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_A_LV) { return m_bFlag_Enable_RTD_Grade_A_LV; } } }
        public bool Flag_Enable_RTD_Grade_B_LV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_B_LV) { m_bFlag_Enable_RTD_Grade_B_LV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_B_LV) { return m_bFlag_Enable_RTD_Grade_B_LV; } } }
        public bool Flag_Enable_RTD_Grade_C_LV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_C_LV) { m_bFlag_Enable_RTD_Grade_C_LV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_C_LV) { return m_bFlag_Enable_RTD_Grade_C_LV; } } }
        public bool Flag_Enable_RTD_Grade_D_LV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_D_LV) { m_bFlag_Enable_RTD_Grade_D_LV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_D_LV) { return m_bFlag_Enable_RTD_Grade_D_LV; } } }
        public bool Flag_Enable_RTD_Grade_E_LV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_E_LV) { m_bFlag_Enable_RTD_Grade_E_LV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_E_LV) { return m_bFlag_Enable_RTD_Grade_E_LV; } } }

        public bool Flag_Enable_HTR_Grade_A_HV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_A_HV) { m_bFlag_Enable_HTR_Grade_A_HV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_A_HV) { return m_bFlag_Enable_HTR_Grade_A_HV; } } }
        public bool Flag_Enable_HTR_Grade_B_HV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_B_HV) { m_bFlag_Enable_HTR_Grade_B_HV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_B_HV) { return m_bFlag_Enable_HTR_Grade_B_HV; } } }
        public bool Flag_Enable_HTR_Grade_C_HV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_C_HV) { m_bFlag_Enable_HTR_Grade_C_HV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_C_HV) { return m_bFlag_Enable_HTR_Grade_C_HV; } } }
        public bool Flag_Enable_HTR_Grade_D_HV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_D_HV) { m_bFlag_Enable_HTR_Grade_D_HV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_D_HV) { return m_bFlag_Enable_HTR_Grade_D_HV; } } }
        public bool Flag_Enable_HTR_Grade_E_HV_b { set { lock (m_lock_Flag_Enable_HTR_Grade_E_HV) { m_bFlag_Enable_HTR_Grade_E_HV = value; } } get { lock (m_lock_Flag_Enable_HTR_Grade_E_HV) { return m_bFlag_Enable_HTR_Grade_E_HV; } } }

        public bool Flag_Enable_RTD_Grade_A_HV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_A_HV) { m_bFlag_Enable_RTD_Grade_A_HV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_A_HV) { return m_bFlag_Enable_RTD_Grade_A_HV; } } }
        public bool Flag_Enable_RTD_Grade_B_HV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_B_HV) { m_bFlag_Enable_RTD_Grade_B_HV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_B_HV) { return m_bFlag_Enable_RTD_Grade_B_HV; } } }
        public bool Flag_Enable_RTD_Grade_C_HV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_C_HV) { m_bFlag_Enable_RTD_Grade_C_HV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_C_HV) { return m_bFlag_Enable_RTD_Grade_C_HV; } } }
        public bool Flag_Enable_RTD_Grade_D_HV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_D_HV) { m_bFlag_Enable_RTD_Grade_D_HV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_D_HV) { return m_bFlag_Enable_RTD_Grade_D_HV; } } }
        public bool Flag_Enable_RTD_Grade_E_HV_b { set { lock (m_lock_Flag_Enable_RTD_Grade_E_HV) { m_bFlag_Enable_RTD_Grade_E_HV = value; } } get { lock (m_lock_Flag_Enable_RTD_Grade_E_HV) { return m_bFlag_Enable_RTD_Grade_E_HV; } } }

        #region HeightCheck
        public double Value_1 = 0.0;
        public double Value_2 = 0.0;
        public double Value_3 = 0.0;
        public double Value_4 = 0.0;
        public bool Value1_OK = false;
        public bool Value2_OK = false;
        public bool Value3_OK = false;
        public bool Value4_OK = false;
        public bool OverallResult_OK = false;
        #endregion

        #region WorkOrderNumber
        public string OP60_WorkOrderNumber_str = "";
        public string OP70_WorkOrderNumber_str = "";
        public string OP80_WorkOrderNumber_str = "";
        public string OP85_WorkOrderNumber_str = "";
        public string OP90_WorkOrderNumber_str = "";
        public string OP100_WorkOrderNumber_str = "";
        public string OP110_WorkOrderNumber_str = "";
        #endregion

        //construction//////////////////////////////////////////////////////////////////////////////////////////////
        public Data_UnitTempStorage()
        {
            Reset_Data();
        }

        //Reset Data/////////////////////////////////////////////////////////////////////////////////////////////
        public void Reset_Data()
        {
            
        }

        //Reset Process Flags////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Reset_Flag()
        {
            DispErrorMessage_str = "N/A";
            RequestStateMessage_str = "N/A";

            RFID_OP85_SN_str = "";
            RFID_OP90_SN_str = "";
            RFID_OP100_Buffer_SN_str = "";
            RFID_OP100_Station_SN_str = "";
            RFID_OP110_SN_str = "";

            m_DataFlag_SolderStation1.Reset();
            m_DataFlag_SolderStation2.Reset();
            m_DataFlag_SolderStation3.Reset();

            //OP60
            Flag_Conv_OP60_Ready_b = false;
            Flag_Conv_OP60_Sending_b = false;
            Flag_Conv_OP60_Trigger_b = false;
            Flag_Station_OP60_Done_b = false;

            //OP70
            Flag_Conv_OP70_Ready_b = false;
            Flag_Conv_OP70_Sending_b = false;
            Flag_Conv_OP70_Trigger_b = false;
            Flag_Station_OP70_Done_b = false;

            //OP80
            Flag_Conv_OP80_Ready_b = false;
            Flag_Conv_OP80_Sending_b = false;
            Flag_Conv_OP80_Trigger_b = false;
            Flag_Station_OP80_Done_b = false;

            //OP85
            Flag_Conv_OP85_Ready_b = false;
            Flag_Conv_OP85_Sending_b = false;
            Flag_Conv_OP85_Trigger_b = false;
            Flag_Station_OP85_Done_b = false;

            //OP90
            Flag_Conv_OP90_Ready_b = false;
            Flag_Conv_OP90_Sending_b = false;
            Flag_Conv_OP90_Trigger_b = false;
            Flag_Station_OP90_Done_b = false;

            //OP100
            Flag_Conv_OP100_Buffer_Ready_b = false;
            Flag_Conv_OP100_Buffer_Sending_b = false;
            Flag_Conv_OP100_Ready_b = false;
            Flag_Conv_OP100_Sending_b = false;
            Flag_Conv_OP100_Outgoing_Ready_b = false;
            Flag_Conv_OP100_Outgoing_Sending_b = false;
            Flag_Conv_OP100_Trigger_b = false;

            //OP110
            Flag_Conv_OP110_Ready_b = false;
            Flag_Conv_OP110_Sending_b = false;
            Flag_Conv_OP110_Trigger_b = false;
            Flag_Station_OP110_Done_b = false;
        }

        //reset OP85 Vision Data/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Reset_OP85_Vision_Results()
        {
            OP85_Vision_Terminal1_Width_str       = "";
            OP85_Vision_Terminal2_Width_str       = "";
            OP85_Vision_Terminal3_Width_str       = "";
            OP85_Vision_Terminal4_Width_str       = "";
            OP85_Vision_Terminal5_Width_str       = "";
            OP85_Vision_Terminal6_Width_str       = "";

            OP85_Vision_Terminal1_Length_str      = "";
            OP85_Vision_Terminal2_Length_str      = "";
            OP85_Vision_Terminal3_Length_str      = "";
            OP85_Vision_Terminal4_Length_str      = "";
            OP85_Vision_Terminal5_Length_str      = "";
            OP85_Vision_Terminal6_Length_str      = "";

            OP85_Vision_Terminal1_Distance_str    = "";
            OP85_Vision_Terminal2_Distance_str    = "";
            OP85_Vision_Terminal3_Distance_str    = "";
            OP85_Vision_Terminal4_Distance_str    = "";
            OP85_Vision_Terminal5_Distance_str    = "";
            OP85_Vision_Terminal6_Distance_str    = "";

            OP85_Vision_Overall_Result_str = "";

            OP85_Vision_Terminal1_Width_Min_str = "";
            OP85_Vision_Terminal1_Width_Max_str = "";
            OP85_Vision_Terminal1_Length_Min_str = "";
            OP85_Vision_Terminal1_Length_Max_str = "";
            OP85_Vision_Terminal1_Distance_Min_str = "";
            OP85_Vision_Terminal1_Distance_Max_str = "";

            OP85_Vision_Terminal2_Width_Min_str = "";
            OP85_Vision_Terminal2_Width_Max_str = "";
            OP85_Vision_Terminal2_Length_Min_str = "";
            OP85_Vision_Terminal2_Length_Max_str = "";
            OP85_Vision_Terminal2_Distance_Min_str = "";
            OP85_Vision_Terminal2_Distance_Max_str = "";

            OP85_Vision_Terminal3_Width_Min_str = "";
            OP85_Vision_Terminal3_Width_Max_str = "";
            OP85_Vision_Terminal3_Length_Min_str = "";
            OP85_Vision_Terminal3_Length_Max_str = "";
            OP85_Vision_Terminal3_Distance_Min_str = "";
            OP85_Vision_Terminal3_Distance_Max_str = "";

            OP85_Vision_Terminal4_Width_Min_str = "";
            OP85_Vision_Terminal4_Width_Max_str = "";
            OP85_Vision_Terminal4_Length_Min_str = "";
            OP85_Vision_Terminal4_Length_Max_str = "";
            OP85_Vision_Terminal4_Distance_Min_str = "";
            OP85_Vision_Terminal4_Distance_Max_str = "";

            OP85_Vision_Terminal5_Width_Min_str = "";
            OP85_Vision_Terminal5_Width_Max_str = "";
            OP85_Vision_Terminal5_Length_Min_str = "";
            OP85_Vision_Terminal5_Length_Max_str = "";
            OP85_Vision_Terminal5_Distance_Min_str = "";
            OP85_Vision_Terminal5_Distance_Max_str = "";

            OP85_Vision_Terminal6_Width_Min_str = "";
            OP85_Vision_Terminal6_Width_Max_str = "";
            OP85_Vision_Terminal6_Length_Min_str = "";
            OP85_Vision_Terminal6_Length_Max_str = "";
            OP85_Vision_Terminal6_Distance_Min_str = "";
            OP85_Vision_Terminal6_Distance_Max_str = "";

            OP85_Vision_Terminal1_Width_Result_str = "";
            OP85_Vision_Terminal2_Width_Result_str = "";
            OP85_Vision_Terminal3_Width_Result_str = "";
            OP85_Vision_Terminal4_Width_Result_str = "";
            OP85_Vision_Terminal5_Width_Result_str = "";
            OP85_Vision_Terminal6_Width_Result_str = "";

            OP85_Vision_Terminal1_Length_Result_str = "";
            OP85_Vision_Terminal2_Length_Result_str = "";
            OP85_Vision_Terminal3_Length_Result_str = "";
            OP85_Vision_Terminal4_Length_Result_str = "";
            OP85_Vision_Terminal5_Length_Result_str = "";
            OP85_Vision_Terminal6_Length_Result_str = "";

            OP85_Vision_Terminal1_Distance_Result_str = "";
            OP85_Vision_Terminal2_Distance_Result_str = "";
            OP85_Vision_Terminal3_Distance_Result_str = "";
            OP85_Vision_Terminal4_Distance_Result_str = "";
            OP85_Vision_Terminal5_Distance_Result_str = "";
            OP85_Vision_Terminal6_Distance_Result_str = "";

            OP85_VisionResult_Terminal1_str = "";
            OP85_VisionResult_Terminal2_str = "";
            OP85_VisionResult_Terminal3_str = "";
            OP85_VisionResult_Terminal4_str = "";
            OP85_VisionResult_Terminal5_str = "";
            OP85_VisionResult_Terminal6_str = "";
        }

		//reset WT Data///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Reset_OP90_WT_Vision_Results()
		{
            OP90_WT_Result_LN1_str            = "";
            OP90_WT_Result_LN2_str            = "";
            OP90_WT_Result_LN3_str            = "";
            OP90_WT_Result_LR0_str            = "";
            OP90_WT_Result_R0R1_str           = "";
            OP90_WT_Result_Temperature_db     = 0;
            OP90_WT_Result_LN_Average_str     = "";

            OP90_WT_Result_HTR_Min_str        = "";
            OP90_WT_Result_HTR_Avg_str        = "";
            OP90_WT_Result_HTR_Max_str        = "";
            OP90_WT_Result_HTR_Result_str     = "";
            OP90_WT_Result_HTR_Grade_str      = "";

            OP90_WT_Result_RTD_Min_str        = "";
            OP90_WT_Result_RTD_Avg_str        = "";
            OP90_WT_Result_RTD_Max_str        = "";
            OP90_WT_Result_RTD_Result_str     = "";
            OP90_WT_Result_RTD_Grade_str      = "";

            OP90_WT_Result_Overall_Result_str = "";
            OP90_WT_Result_Overall_Grade_str  = "";

            OP90_VisionResult_WireFront1_str = "";
            OP90_VisionResult_WireFront2_str = "";
            OP90_VisionResult_WireFront3_str = "";
            OP90_VisionResult_WireFront4_str = "";
            OP90_VisionResult_WireFront5_str = "";
            OP90_VisionResult_WireFront6_str = "";

            OP90_VisionResult_WireBack1_str = "";
            OP90_VisionResult_WireBack2_str = "";
            OP90_VisionResult_WireBack3_str = "";
            OP90_VisionResult_WireBack4_str = "";
            OP90_VisionResult_WireBack5_str = "";
            OP90_VisionResult_WireBack6_str = "";

            OP90_VisionResult_Wire1_str = "";
            OP90_VisionResult_Wire2_str = "";
            OP90_VisionResult_Wire3_str = "";
            OP90_VisionResult_Wire4_str = "";
            OP90_VisionResult_Wire5_str = "";
            OP90_VisionResult_Wire6_str = "";
		}

        public void Reset_OP100_WT_Vision_Results()
        {
            OP100_VisionResult_OP85_Terminal1_str = "";
            OP100_VisionResult_OP85_Terminal2_str = "";
            OP100_VisionResult_OP85_Terminal3_str = "";
            OP100_VisionResult_OP85_Terminal4_str = "";
            OP100_VisionResult_OP85_Terminal5_str = "";
            OP100_VisionResult_OP85_Terminal6_str = "";

            OP100_WT_Result_OP90_LN1_str            = "";
            OP100_WT_Result_OP90_LN2_str            = "";
            OP100_WT_Result_OP90_LN3_str            = "";
            OP100_WT_Result_OP90_LR0_str            = "";
            OP100_WT_Result_OP90_R0R1_str           = "";
            OP100_WT_Result_OP90_Temperature_db     = 0;
            OP100_WT_Result_OP90_LN_Average_str     = "";

            OP100_WT_Result_OP90_HTR_Min_str        = "";
            OP100_WT_Result_OP90_HTR_Avg_str        = "";
            OP100_WT_Result_OP90_HTR_Max_str        = "";
            OP100_WT_Result_OP90_HTR_Result_str     = "";
            OP100_WT_Result_OP90_HTR_Grade_str      = "";

            OP100_WT_Result_OP90_RTD_Min_str        = "";
            OP100_WT_Result_OP90_RTD_Avg_str        = "";
            OP100_WT_Result_OP90_RTD_Max_str        = "";
            OP100_WT_Result_OP90_RTD_Result_str     = "";
            OP100_WT_Result_OP90_RTD_Grade_str      = "";

            OP100_WT_Result_OP90_Overall_Result_str = "";
            OP100_WT_Result_OP90_Overall_Grade_str  = "";

            OP100_VisionResult_OP90_Wire1_str = "";
            OP100_VisionResult_OP90_Wire2_str = "";
            OP100_VisionResult_OP90_Wire3_str = "";
            OP100_VisionResult_OP90_Wire4_str = "";
            OP100_VisionResult_OP90_Wire5_str = "";
            OP100_VisionResult_OP90_Wire6_str = "";
        }

        //reset counter/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Reset_Counter()
        {
            PartCount_int = 0;
            PartCountFail_int = 0;
            PartCountPass_int = 0;
            Total_RunTime = "0:0:0";
            Total_StopTime = "0:0:0";
            ProcessRun_Start = "0:0:0";
            ProcessRun_Stop = "0:0:0";
            Cycle_Time = "0";
        }
    }

	//Solder Station Common Flag
	public class Data_SolderStationFlag
	{
		private readonly object m_lock_Flag_SolderPoint1 = new object();			//lock: Soldering points
		private readonly object m_lock_Flag_SolderPoint2 = new object();
		private readonly object m_lock_Flag_SolderPoint3 = new object();
		private readonly object m_lock_Flag_SolderPoint4 = new object();
		private readonly object m_lock_Flag_SolderPoint5 = new object();
		private readonly object m_lock_Flag_SolderPoint6 = new object();
		private readonly object m_lock_RFID_SN = new object();

		bool m_bFlag_SolderPoint1 = false;																//signal Soldering points
		bool m_bFlag_SolderPoint2 = false;
		bool m_bFlag_SolderPoint3 = false;
		bool m_bFlag_SolderPoint4 = false;
		bool m_bFlag_SolderPoint5 = false;
		bool m_bFlag_SolderPoint6 = false;
		string m_strRFID_SN = "";																				//RFID Serial Number

		public bool Flag_SolderPoint1_b { set { lock ( m_lock_Flag_SolderPoint1 ) { m_bFlag_SolderPoint1 = value; } } get { lock ( m_lock_Flag_SolderPoint1 ) { return m_bFlag_SolderPoint1; } } }
		public bool Flag_SolderPoint2_b { set { lock ( m_lock_Flag_SolderPoint2 ) { m_bFlag_SolderPoint2 = value; } } get { lock ( m_lock_Flag_SolderPoint2 ) { return m_bFlag_SolderPoint2; } } }
		public bool Flag_SolderPoint3_b { set { lock ( m_lock_Flag_SolderPoint3 ) { m_bFlag_SolderPoint3 = value; } } get { lock ( m_lock_Flag_SolderPoint3 ) { return m_bFlag_SolderPoint3; } } }
		public bool Flag_SolderPoint4_b { set { lock ( m_lock_Flag_SolderPoint4 ) { m_bFlag_SolderPoint4 = value; } } get { lock ( m_lock_Flag_SolderPoint4 ) { return m_bFlag_SolderPoint4; } } }
		public bool Flag_SolderPoint5_b { set { lock ( m_lock_Flag_SolderPoint5 ) { m_bFlag_SolderPoint5 = value; } } get { lock ( m_lock_Flag_SolderPoint5 ) { return m_bFlag_SolderPoint5; } } }
		public bool Flag_SolderPoint6_b { set { lock ( m_lock_Flag_SolderPoint6 ) { m_bFlag_SolderPoint6 = value; } } get { lock ( m_lock_Flag_SolderPoint6 ) { return m_bFlag_SolderPoint6; } } }
		public string RFID_SN_str { set { lock ( m_lock_RFID_SN ) { m_strRFID_SN = value; } } get { lock ( m_lock_RFID_SN ) { return m_strRFID_SN; } } }

		//Reset Flags
		public void Reset()
		{
			Flag_SolderPoint1_b	= false;
			Flag_SolderPoint2_b	= false;
			Flag_SolderPoint3_b	= false;
			Flag_SolderPoint4_b	= false;
			Flag_SolderPoint5_b	= false;
			Flag_SolderPoint6_b	= false;
		}
	}
}