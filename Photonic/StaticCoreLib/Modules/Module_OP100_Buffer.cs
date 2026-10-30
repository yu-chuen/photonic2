using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_OP100_Buffer
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl      = null;
		MonitorInput m_pMonitorInput            = null;
		MachineInfo m_MachineInfo               = null;
		Data_UnitTempStorage m_DataUnit         = null;

		Log_Data m_LogFile                      = null;
		Comm_TCP_RFID m_RFIDScanner             = null;
		Server_COMTCP_GT_CentralPC m_Server     = null;
        Data_PerStation_DownTime m_Data_DownTime = null;
        SupportFunctions m_SupportFunctions = null;


		//IO
        public InputBit m_inpButton_Run = null;
        public InputBit m_inpButton_Stop = null;
        public InputBit m_inpBridgePlungerLocked = null;

		public Sensor m_senPalletPresent_1 = null;
        
        public OutputBit m_outStopper_1 = null;
		public OutputBit m_outIndicatorLight_Red = null;
		public OutputBit m_outIndicatorLight_Green = null;

        public Cylinder m_cylBridge_LockUnlock = null;

		//Flags
		#region Flags
		private readonly object m_lockRun = new object();                                       //Lock for thread sequence
		private readonly object m_lockRunEnd = new object();                                //Lock for thread sequence End
		private readonly object m_lockProcessRun = new object();                        //Flag to indicate that the process is running
		private readonly object m_lockInit = new object();                                          //Lock for Init Sequence
		private readonly object m_lockInit_OK = new object();                                   //Lock for Init Sequence
		private readonly object m_lock_Flag_Error = new object();                           //Lock for Error occur at auto sequence

		bool m_bFlag_Init = false;
		bool m_bFlag_InitOK = false;																	//indicate init is done and good
		bool m_bFlag_Run = false;																		//indicate thread is running						
		bool m_bFlag_RunEnd = false;																//signal thread to stop
		bool m_bFlag_Error = false;																	//signal: Error occur
		bool m_bFlag_ProcessRun = false;														//signal process has began

		public bool Module_Running
		{
			set { lock ( m_lockRun ) { m_bFlag_Run = value; } }
			get { lock ( m_lockRun ) { return m_bFlag_Run; } }
		}                                                                //InDicate if the module is running
		public bool Module_StopRun
		{
			set { lock ( m_lockRunEnd ) { m_bFlag_RunEnd = value; } }
			get { lock ( m_lockRunEnd ) { return m_bFlag_RunEnd; } }
		}                                                             //Stop running thread
		public bool Process_Running
		{
			set { lock ( m_lockRun ) { m_bFlag_ProcessRun = value; } }
			get { lock ( m_lockRun ) { return m_bFlag_ProcessRun; } }
		}                                                            //indicate: Process is running
		public bool Flag_Init                                                                               //Init
		{
			set { lock ( m_lockInit ) { m_bFlag_Init = value; } }
			get { lock ( m_lockInit ) { return m_bFlag_Init; } }
		}
		public bool Flag_InitOK
		{
			set { lock ( m_lockInit_OK ) { m_bFlag_InitOK = value; } }
			get { lock ( m_lockInit_OK ) { return m_bFlag_InitOK; } }
		}                                                                      //Indicate: Init passed
		public bool Flag_Error
		{
			set { lock ( m_lock_Flag_Error ) { m_bFlag_Error = value; } }
			get { lock ( m_lock_Flag_Error ) { return m_bFlag_Error; } }
		}
		
		private readonly object m_lock_Flag_RunAgain = new object();
		bool m_bFlag_RunAgain = false;															//signal: Continue even when stop signal is high. Not to be used at wait section
		public bool Flag_RunAgain_b { set { lock ( m_lock_Flag_RunAgain ) { m_bFlag_RunAgain = value; } } get { lock ( m_lock_Flag_RunAgain ) { return m_bFlag_RunAgain; } } }

        private readonly object m_lock_Flag_Sequence_NoGo = new object();
        bool m_bFlag_Sequence_NoGo = false;
        public bool Flag_Sequence_NoGo_b { set { lock (m_lock_Flag_Sequence_NoGo) { m_bFlag_Sequence_NoGo = value; } } get { lock (m_lock_Flag_Sequence_NoGo) { return m_bFlag_Sequence_NoGo; } } }

		#endregion

		public int m_iFlag_SequencePos    = 0;
        int m_iFlag_LogSeqSkip = 0;

        /// <summary>
        /// Indicator Light State (-1 => OFF, 0 => Static Red, 1 => Blink Red, 2 => Static Green, 3 => Blink Green, 4 => Static Red + Green, 5 => Blink Red + Green)
        /// </summary>
        int m_iFlag_IndicatorLight_State = -1;

		int m_iErrorID = 0;
		String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		//thread
		Thread m_thRun                          = null;

        private readonly object m_lock_RFIDAccess = new object();				//lock: RFID Read and Write Function access. One thread can access at a time.

		//constructor//////////////////////////////////////////////////////////////////////////////////
		public Module_OP100_Buffer(SeqMachineControl mSeqMachineCtrl)
		{
			m_SeqMachineCtrl	= mSeqMachineCtrl;
			m_MachineInfo		= m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit			= m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput		= m_SeqMachineCtrl.m_MonitorInput;
			m_RFIDScanner		= m_SeqMachineCtrl.m_RFIDReader_OP90_OP110;
            m_SupportFunctions  = m_SeqMachineCtrl.m_SupportFunctions;

            //create downtime object
            m_Data_DownTime = m_SeqMachineCtrl.m_Data_PerStation_DownTime_OP100;

            //create server object and set server settings
            m_Server = new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_Module_OP100_Buffer", m_MachineInfo.Get_MachineFilePath());
            m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);
            
			//setup log file
            m_LogFile = new Log_Data();
            m_LogFile.SetFolderName("Machine_Sequence_Folder\\OP100 - Buffer");
            m_LogFile.SetFileName("LogFile");
            m_LogFile.SeqLog_StartThread();
		}

		//Clean up////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Clean_Up()
		{
            m_LogFile.Clean_Up();
		}
				
        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_InputsOutputs(InterfaceCards mInterface)
		{
            int iBitID = 0, iBitID_Ret = 0, iExtID = 0, iRetID=0;
			String strFilePath = m_MachineInfo.Get_MachineFilePath();

            //add sensor
			iBitID = 2;		m_senPalletPresent_1	 = new Sensor(mInterface.Get_InputPort(9), iBitID, "OP100 - Pallet Present 1", strFilePath);

            //add output
            iBitID = 0;     m_outIndicatorLight_Green = new OutputBit(mInterface.Get_OutputPort(9), iBitID, "OP100 - Indicator Green");
            iBitID = 1;     m_outIndicatorLight_Red   = new OutputBit(mInterface.Get_OutputPort(9), iBitID, "OP100 - Indicator Red");
			iBitID = 4;		m_outStopper_1		      = new OutputBit(mInterface.Get_OutputPort(9), iBitID, "OP100 - Stopper 1");

            //add cylinder

            //specific machine type IO
            if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                //add sensor
                iBitID = 2; m_inpButton_Stop = new InputBit(mInterface.Get_InputPort(10), iBitID, "OP100 - Stop Button");
                iBitID = 3; m_inpButton_Run = new InputBit(mInterface.Get_InputPort(10), iBitID, "OP100 - Run Button");
                iBitID = 7; m_inpBridgePlungerLocked = new InputBit(mInterface.Get_InputPort(9), iBitID, "OP100 - Bridge Plunger Locked");

                //add output

                //add cylinder
                iBitID = 5; iBitID_Ret = 4; iExtID = 14; iRetID = 15; m_cylBridge_LockUnlock = new Cylinder(mInterface.Get_OutputPort(10), iBitID,
                                                                                                    mInterface.Get_OutputPort(10), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(9), iExtID,
                                                                                                    mInterface.Get_InputPort(9), iRetID,
                                                                                                    "OP100 - Bridge", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "Lock", "Unlock");
            }

			return true;
		}

		//Delay//////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: Found that Thread.Sleep(ms) is very inaccurate and sometimes sleep more than the delay count
        public void Delay_ms(int iDelay_ms)
        {
            ManualResetEvent mEvent = new ManualResetEvent(false);
            mEvent.WaitOne(iDelay_ms);
            mEvent.Dispose();
        }

		//Logging///////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetLog(String strLog, int iLevel = 1)
        {
            //disable log if level clerance failed
            if (iLevel < m_DataUnit.LogLevel_int) return;

            //m_SeqMachineCtrl.SetLog("=====> Module_OP100_Buffer: " + strLog);
            m_LogFile.Write_ToSeqLog("=====> Module_OP100_Buffer: " + strLog);
        }

        public void SetLog_Sequence(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlag_LogSeqSkip == iSequencePos) { return; }
            else { m_iFlag_LogSeqSkip = iSequencePos; }

            //write to log
            SetLog(strLog);
        }

		//Get Error Message////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID;
		}

		public String Get_ErrorMsgAdd()
		{
            String strError = "Module_OP100_Buffer: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}

		public String Get_ErrorMsg_Front()
		{
            String strError = "Module_OP100_Buffer: " + m_strErrorMsg_Front + ": ";
			m_strErrorMsg_Front = "";
			return strError;
		}

		//Set Error Message///////////////////////////////////////////////////////////////////////////////
		public void Set_Error( int iErrorID, String strErrorMsgAdd, String strErrorMsgFront = "" )
		{
			this.m_iErrorID = iErrorID;
			this.m_strErrorMsgAdd = strErrorMsgAdd;
			this.m_strErrorMsg_Front = strErrorMsgFront;

            #region Get Error structure to update server
            String strErrorDesc = "";
            short iRet = 1;

            ErrorMessage_Info error_info = new ErrorMessage_Info();
            error_info = m_SeqMachineCtrl.m_DataErrorMessage.Get_ErrorInfos(iErrorID, ref iRet);

            if (iRet != 0)
            {
                error_info.m_strErrorMsg += ", " + strErrorMsgAdd;
            }
            else
            {
                error_info.m_strErrorMsg += "unknown error...";
                error_info.m_strRecoveryMsg += "...";
            }

            if (strErrorMsgFront != "")
            {
                strErrorDesc = strErrorMsgFront + ", " + error_info.m_strErrorMsg;
            }
            else
            {
                strErrorDesc = error_info.m_strErrorMsg;
            }

            if (strErrorDesc.Contains("\r\n"))
            {
                strErrorDesc = strErrorDesc.Replace("\r\n", ", ");
            }
            #endregion
            Server_InsertErrorLog(iErrorID, strErrorDesc);

			//Logger
			String strLog = "";
            strLog = "ErrorID=" + iErrorID + " : Device: " + strErrorMsgFront + " : ErrorMsg=" + strErrorMsgAdd + ": ErrorName=" + Enum.GetName(typeof(ErrorTable.en_ErrorCode), m_iErrorID);
			SetLog(strLog, 999);
		}

        public void Set_CylError(Cylinder mCyl)
        {
            Set_Error(mCyl.Get_ErrorID(), mCyl.Get_ErrorMessage(), mCyl.Get_Label());
        }

		/************************************************************************************************************************
		 * Others / Supporting functions
		 ************************************************************************************************************************/
		//Read from RFID///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Read_RFIDScanner(int iChannel)
		{
            lock (m_lock_RFIDAccess)
            {
                String strRFID = "";

                //dry run, end lot
                if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

                //scan rfid
                if (m_RFIDScanner.Send_Command(iChannel, ref strRFID) == false)
                {
                    Set_Error(m_RFIDScanner.Get_ErrorID(), m_RFIDScanner.Get_ErrorMessage(), m_RFIDScanner.m_strLabel);
                    return false;
                }

                //Set to rfid var
                m_DataUnit.RFID_OP100_Buffer_SN_str = strRFID.Split(',')[0];

                return true;
            }
		}

        /// <summary>
        /// Static and Blinking Indicator Light function to control the indicator behavior based on the interval specified.
        /// </summary>
        /// <param name="iState">color (-1 => OFF, 0 => Static Red, 1 => Blink Red, 2 => Static Green, 3 => Blink Green, 4 => Static Red + Green, 5 => Blink Red + Green, 6 => Blink Red then Blink Green)</param>
        /// <param name="iInterval">millisecond</param>
        public void IndicatorLight_State(int iState, int iInterval)
        {
            if (iState == -1)
            {
                m_outIndicatorLight_Red.Write(0);
                m_outIndicatorLight_Green.Write(0);
            }
            else if (iState == 0)
            {
                m_outIndicatorLight_Red.Write(1);
            }
            else if (iState == 1)
            {
                m_outIndicatorLight_Red.Write(1);
                Delay_ms(iInterval);
                m_outIndicatorLight_Red.Write(0);
                Delay_ms(iInterval);
            }
            else if (iState == 2)
            {
                m_outIndicatorLight_Green.Write(1);
            }
            else if (iState == 3)
            {
                m_outIndicatorLight_Green.Write(1);
                Delay_ms(iInterval);
                m_outIndicatorLight_Green.Write(0);
                Delay_ms(iInterval);
            }
            else if (iState == 4)
            {
                m_outIndicatorLight_Red.Write(1);
                m_outIndicatorLight_Green.Write(1);
            }
            else if (iState == 5)
            {
                m_outIndicatorLight_Red.Write(1);
                m_outIndicatorLight_Green.Write(1);
                Delay_ms(iInterval);
                m_outIndicatorLight_Red.Write(0);
                m_outIndicatorLight_Green.Write(0);
                Delay_ms(iInterval);
            }
            else if (iState == 6)
            {
                m_outIndicatorLight_Red.Write(1);
                Delay_ms(iInterval);
                m_outIndicatorLight_Red.Write(0);
                m_outIndicatorLight_Green.Write(1);
                Delay_ms(iInterval);
                m_outIndicatorLight_Green.Write(0);
            }
        }

		//Server: Get Go or no GO/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Get Server Go or NoGo signal. Skip check: -1=>error, 0=>no go, 1=>go
		/// </summary>
		/// <returns></returns>
        public int Server_SkipCheck()
        {
            bool bSkipCheck = false;

            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return 1; }

            //get Server GO Permission
            if (!m_Server.Cmd_SkipCheck(m_DataUnit.RFID_OP100_Buffer_SN_str, m_MachineInfo.m_strOP100, ref bSkipCheck))
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return -1;
            }

            //if no go
            if (bSkipCheck == false) { SetLog("SN: " + m_DataUnit.RFID_OP100_Buffer_SN_str + ", Server_SkipCheck_OP100_Buffer: No Go", 2); }

            return ((bSkipCheck == true) ? 1 : 0);
        }

        //get work order number from server
        public bool Server_Get_WorkOrderNumber()
        {
            //check flags
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true || m_DataUnit.Flag_Bypass_OP100_b == true) { return true; }
            if (m_Server.m_Settings.m_bEnable == false) { m_DataUnit.OP100_WorkOrderNumber_str = "H123456789"; return true; }

            string strResponse = "";
            string[] strArData;

            //get data
            if (m_Server.Cmd_GetProcessDataFromServer(m_DataUnit.RFID_OP100_Buffer_SN_str, "workordernumber", ref strResponse) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            strArData = strResponse.Split(',');

            //check data count received
            if (strArData.Count() < 2)
            {
                Set_Error((int)ErrorTable.en_ErrorCode.SERVER_RESULT_NOT_ENOUGH_DATA, "Required Data Count = 2\r\nReceived Data count = " + strArData.Count().ToString());
                return false;
            }

            m_DataUnit.OP100_WorkOrderNumber_str = strArData[1];

            return true;
        }

        //Server: Write Result to Server/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Server_SendStationRunComplete()
        {
            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

            //Send Result to Server
            if (!m_Server.Cmd_StationPalletDone(m_MachineInfo.m_strOP100, m_DataUnit.RFID_OP100_Buffer_SN_str))
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            return true;
        }

        //Server: UpdateInput////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server: UpdateInput
        /// </summary>
        /// <param name="strDateTime">Current DateTime</param>
        /// <returns></returns>
        public bool Server_UpdateInput(string strDateTime)
        {
            string strStationName = "";

            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

            //check MachineID is Leg A, Leg B or Leg C
            if (m_MachineInfo.m_strMachineID == "002")
            {
                strStationName = m_MachineInfo.m_strOP100 + "A";
            }
            else if (m_MachineInfo.m_strMachineID == "003")
            {
                strStationName = m_MachineInfo.m_strOP100 + "B";
            }

            //Send result to server
            if (m_Server.Cmd_UpdateInput(m_DataUnit.RFID_OP100_Buffer_SN_str, strStationName, strDateTime) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            return true;
        }

        //Server: UpdateOutput////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server: UpdateOutput
        /// </summary>
        /// <param name="iPassReject">Pass = 1, Reject = 0</param>
        /// <param name="strDateTime">Current DateTime</param>
        /// <returns></returns>
        public bool Server_UpdateOutput(int iPassReject, string strDateTime)
        {
            string strStationName = "";

            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

            //check MachineID is Leg A, Leg B or Leg C
            if (m_MachineInfo.m_strMachineID == "002")
            {
                strStationName = m_MachineInfo.m_strOP100 + "A";
            }
            else if (m_MachineInfo.m_strMachineID == "003")
            {
                strStationName = m_MachineInfo.m_strOP100 + "B";
            }

            //Send result to server
            if (m_Server.Cmd_UpdateOutput(m_DataUnit.RFID_OP100_Buffer_SN_str, strStationName, iPassReject, strDateTime) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            return true;
        }

        //Server: UpdateProcessTime////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server: UpdateProcessTime
        /// </summary>
        /// <param name="dbProcessTime">Station ProcessTime</param>
        /// <returns></returns>
        public bool Server_UpdateProcessTime(double dbProcessTime)
        {
            string strStationName = "";

            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

            //check MachineID is Leg A, Leg B or Leg C
            if (m_MachineInfo.m_strMachineID == "002")
            {
                strStationName = m_MachineInfo.m_strOP100 + "A";
            }
            else if (m_MachineInfo.m_strMachineID == "003")
            {
                strStationName = m_MachineInfo.m_strOP100 + "B";
            }

            //Send result to server
            if (m_Server.Cmd_UpdateProcessTime(m_DataUnit.RFID_OP100_Buffer_SN_str, strStationName, dbProcessTime) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            return true;
        }

        //Server: UpdateCycleTime////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Server: UpdateCycleTime
        /// </summary>
        /// <param name="dbCycleTime">Station CycleTime</param>
        /// <param name="strDateTime">Current DateTime</param>
        /// <returns></returns>
        public bool Server_UpdateCycleTime(double dbCycleTime, string strDateTime)
        {
            string strStationName = "";

            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true) { return true; }

            //check MachineID is Leg A, Leg B or Leg C
            if (m_MachineInfo.m_strMachineID == "002")
            {
                strStationName = m_MachineInfo.m_strOP100 + "A";
            }
            else if (m_MachineInfo.m_strMachineID == "003")
            {
                strStationName = m_MachineInfo.m_strOP100 + "B";
            }

            //Send result to server
            if (m_Server.Cmd_UpdateCycleTime(m_DataUnit.RFID_OP100_Buffer_SN_str, strStationName, dbCycleTime, strDateTime) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            return true;
        }

        //get data from server/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Server_GetData()
        {
            String strResponse = "";
            String[] strArData;

            //dry run
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true || m_Server.m_Settings.m_bEnable == false) { return true; }

            //get data
            if (m_Server.Cmd_GetProcessDataFromServer(m_DataUnit.RFID_OP100_Buffer_SN_str, "op100_data", ref strResponse) == false)
            {
                Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);
                return false;
            }

            strArData = strResponse.Split(',');

            //check data count received
            if (strArData.Count() < 25)
            {
                Set_Error((int)ErrorTable.en_ErrorCode.SERVER_RESULT_NOT_ENOUGH_DATA, "Required Data Count = 25\r\nReceived Data count = " + strArData.Count().ToString());
                return false;
            }

            m_DataUnit.OP100_VisionResult_OP85_Terminal1_str   = ((strArData[1]  == "Pass" && strArData[2]  == "Pass" && strArData[3]  == "Pass") ? "Pass" : "Fail");
            m_DataUnit.OP100_VisionResult_OP85_Terminal2_str   = ((strArData[4]  == "Pass" && strArData[5]  == "Pass" && strArData[6]  == "Pass") ? "Pass" : "Fail");
            m_DataUnit.OP100_VisionResult_OP85_Terminal3_str   = ((strArData[7]  == "Pass" && strArData[8]  == "Pass" && strArData[9]  == "Pass") ? "Pass" : "Fail");
            m_DataUnit.OP100_VisionResult_OP85_Terminal4_str   = ((strArData[10] == "Pass" && strArData[11] == "Pass" && strArData[12] == "Pass") ? "Pass" : "Fail");
            m_DataUnit.OP100_VisionResult_OP85_Terminal5_str   = ((strArData[13] == "Pass" && strArData[14] == "Pass" && strArData[15] == "Pass") ? "Pass" : "Fail");
            m_DataUnit.OP100_VisionResult_OP85_Terminal6_str   = ((strArData[16] == "Pass" && strArData[17] == "Pass" && strArData[18] == "Pass") ? "Pass" : "Fail");

            //m_DataUnit.OP100_WT_Result_OP90_LN1_str            = strArData[19]; 
            //m_DataUnit.OP100_WT_Result_OP90_LN2_str            = strArData[20]; 
            //m_DataUnit.OP100_WT_Result_OP90_LN3_str            = strArData[21]; 
            //m_DataUnit.OP100_WT_Result_OP90_LR0_str            = strArData[22];
            //m_DataUnit.OP100_WT_Result_OP90_R0R1_str           = strArData[23];
            //m_DataUnit.OP100_WT_Result_OP90_Temperature_db     = double.Parse(strArData[24]);
            //m_DataUnit.OP100_WT_Result_OP90_LN_Average_str     = strArData[25];

            //m_DataUnit.OP100_WT_Result_OP90_HTR_Min_str        = strArData[26];
            //m_DataUnit.OP100_WT_Result_OP90_HTR_Avg_str        = strArData[27];
            //m_DataUnit.OP100_WT_Result_OP90_HTR_Max_str        = strArData[28];
            //m_DataUnit.OP100_WT_Result_OP90_HTR_Result_str     = strArData[29];
            //m_DataUnit.OP100_WT_Result_OP90_HTR_Grade_str      = strArData[30];

            //m_DataUnit.OP100_WT_Result_OP90_RTD_Min_str        = strArData[31];
            //m_DataUnit.OP100_WT_Result_OP90_RTD_Avg_str        = strArData[32];
            //m_DataUnit.OP100_WT_Result_OP90_RTD_Max_str        = strArData[33];
            //m_DataUnit.OP100_WT_Result_OP90_RTD_Result_str     = strArData[34];
            //m_DataUnit.OP100_WT_Result_OP90_RTD_Grade_str      = strArData[35];

            //m_DataUnit.OP100_WT_Result_OP90_Overall_Result_str = strArData[36];
            //m_DataUnit.OP100_WT_Result_OP90_Overall_Grade_str  = strArData[37];

            m_DataUnit.OP100_VisionResult_OP90_Wire1_str       = strArData[19];
            m_DataUnit.OP100_VisionResult_OP90_Wire2_str       = strArData[20];
            m_DataUnit.OP100_VisionResult_OP90_Wire3_str       = strArData[21];
            m_DataUnit.OP100_VisionResult_OP90_Wire4_str       = strArData[22];
            m_DataUnit.OP100_VisionResult_OP90_Wire5_str       = strArData[23];
            m_DataUnit.OP100_VisionResult_OP90_Wire6_str       = strArData[24];

            return true;
        }

        //Server: InsertErrorLog///////////////////////////////////////////////////////
        public void Server_InsertErrorLog(int iErrorCode, String strErrorDesc)
        {
            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true || m_Server.m_Settings.m_bEnable == false) { return; }

            int iResult;

            if (int.TryParse(m_MachineInfo.m_strMachineID, out iResult) == false)
            {
                //prompt error but dont stop machine
                m_SeqMachineCtrl.Display_ErrorMessageBox("Error. Unable to insert ErrorLog into server.\r\nMachine ID is in the wrong format.\r\nOnly numeric values is allowed."); //blocking call until message box is closed
                m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Run(); //reset the tower light back to green

                return;
            }

            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            //Send result to server
            if (m_Server.Cmd_InsertErrorLog(m_DataUnit.RFID_OP100_Buffer_SN_str, strDateTime, int.Parse(m_MachineInfo.m_strMachineID), iErrorCode, Enum.GetName(typeof(ErrorTable.en_ErrorCode), iErrorCode), strErrorDesc) == false)
            {
                //Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);

                //prompt error but dont stop machine
                m_SeqMachineCtrl.Display_ErrorMessageBox("Error. Unable to insert ErrorLog into server.\r\nErrorCode: " + m_Server.Get_ErrorID() + "\r\nErrorDesc: " + m_Server.m_strLabel + ", " + m_Server.Get_ErrorMessage()); //blocking call until message box is closed
                m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Run(); //reset the tower light back to green

                return;
            }
        }

        //Server: Insert Station DownTime//////////////////////////////////////////////////////////////////////
        public void Server_InsertStationDownTime()
        {
            //dry run, end lot
            if (m_DataUnit.Flag_DryRun == true || m_DataUnit.Flag_EndLot == true || m_Server.m_Settings.m_bEnable == false) { return; }

            //set local variable
            string strStation = m_Data_DownTime.m_Data.m_str_StationName;
            string strDown_DateTime = m_Data_DownTime.m_Data.m_str_Down_DateTime;
            string strStart_DateTime = m_Data_DownTime.m_Data.m_str_Start_DateTime;
            double dbTotal_DownTime_seconds = m_Data_DownTime.m_Data.m_db_Total_DownTime;
            string strDescription = m_Data_DownTime.m_Data.m_str_Description;

            if (m_Server.Cmd_InsertStationDownTime(strStation, strDown_DateTime, strStart_DateTime, dbTotal_DownTime_seconds, strDescription) == false)
            {
                //prompt error
                m_SeqMachineCtrl.Display_ErrorMessageBox("Error. Unable to insert Station DownTime into server.\r\nErrorCode: " + m_Server.Get_ErrorID() + "\r\nErrorDesc: " + m_Server.m_strLabel + ", " + m_Server.Get_ErrorMessage()); //blocking call until message box is closed
                m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Run(); //reset the tower light back to standby mode
            }

            return;
        }

        //set variables for Station DownTime to be updated to Server
        public void Set_Station_DownTime(bool bDown, int iThreadIdentifier)
        {
            lock (m_DataUnit.m_lock_Set_Station_DownTime_OP100)
            {
                if (bDown == true)
                {
                    if (m_Data_DownTime.m_Data.m_str_Down_DateTime == "")
                    {
                        //set variables
                        //check MachineID is Leg A, Leg B or Leg C
                        if (m_MachineInfo.m_strMachineID == "002")
                        {
                            m_Data_DownTime.m_Data.m_str_StationName = m_MachineInfo.m_strOP100 + "A";
                        }
                        else if (m_MachineInfo.m_strMachineID == "003")
                        {
                            m_Data_DownTime.m_Data.m_str_StationName = m_MachineInfo.m_strOP100 + "B";
                        }
                        m_Data_DownTime.m_Data.m_i_ThreadIdentifier = iThreadIdentifier;
                        m_Data_DownTime.m_Data.m_str_Down_DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

                        if (Module_StopRun == true)
                        {
                            m_Data_DownTime.m_Data.m_str_Description = "Stop button pressed.";
                        }
                        else if (Flag_Error == true)
                        {
                            #region Get Error structure to update server
                            String strErrorDesc = "";
                            short iRet = 1;

                            ErrorMessage_Info error_info = new ErrorMessage_Info();
                            error_info = m_SeqMachineCtrl.m_DataErrorMessage.Get_ErrorInfos(m_iErrorID, ref iRet);

                            if (iRet != 0)
                            {
                                error_info.m_strErrorMsg += ", " + m_strErrorMsgAdd;
                            }
                            else
                            {
                                error_info.m_strErrorMsg += "unknown error...";
                                error_info.m_strRecoveryMsg += "...";
                            }

                            if (m_strErrorMsg_Front != "")
                            {
                                strErrorDesc = m_strErrorMsg_Front + ", " + error_info.m_strErrorMsg;
                            }
                            else
                            {
                                strErrorDesc = error_info.m_strErrorMsg;
                            }

                            if (strErrorDesc.Contains("\r\n"))
                            {
                                strErrorDesc = strErrorDesc.Replace("\r\n", ", ");
                            }
                            #endregion
                            m_Data_DownTime.m_Data.m_str_Description = "ErrorCode: " + m_iErrorID + ", ErrorName: " + Enum.GetName(typeof(ErrorTable.en_ErrorCode), m_iErrorID) + ", ErrorDesc: " + strErrorDesc;
                        }
                        else
                        {
                            m_Data_DownTime.m_Data.m_str_Description = "Unknown.";
                        }

                        //save to xml file
                        m_Data_DownTime.Save_Settings();
                    }
                }
                else
                {
                    if (m_Data_DownTime.m_Data.m_i_ThreadIdentifier == iThreadIdentifier)
                    {
                        if (m_Data_DownTime.m_Data.m_str_Down_DateTime != "")
                        {
                            //set variables
                            m_Data_DownTime.m_Data.m_str_Start_DateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                            m_Data_DownTime.m_Data.m_db_Total_DownTime = m_SupportFunctions.Get_DecimalPlaces((((DateTime.Parse(m_Data_DownTime.m_Data.m_str_Start_DateTime) - DateTime.Parse(m_Data_DownTime.m_Data.m_str_Down_DateTime)).TotalMilliseconds) / 1000), 3);

                            //update server
                            Server_InsertStationDownTime();

                            //reset all variables
                            m_Data_DownTime.Reset_Data();

                            //save to xml file
                            m_Data_DownTime.Save_Settings();
                        }
                    }
                }
            }
        }

		/************************************************************************************************************************
		 * Initialization Sequence
		 ************************************************************************************************************************/
		//Start Initialization Thread//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Start_Init()
        {
            if (Flag_Init == false && Module_Running == false)
            {
                Thread mTh = new Thread(Init_Module);
                mTh.Start();
            }
        }

		//Initialize module/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Init_Module()
        {
            Flag_Init = true;
            Flag_InitOK = false;

            //reset flag
            m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = false;
            m_DataUnit.Flag_Conv_OP100_Buffer_Sending_b = false;
            m_DataUnit.Flag_Conv_OP100_Trigger_b = false;
            Flag_Sequence_NoGo_b = false;

            //reset sequence
            m_iFlag_SequencePos = 0;
            m_iFlag_LogSeqSkip = 0;

            //reset data
            m_iFlag_IndicatorLight_State = -1;
            m_DataUnit.Reset_OP100_WT_Vision_Results();
            m_DataUnit.RFID_OP100_Buffer_SN_str = "";

            //reset IO
            m_outStopper_1.Write(0);
            m_outIndicatorLight_Red.Write(0);
            m_outIndicatorLight_Green.Write(0);
            
            //make sure bridge is closed and locked
            if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                if (m_cylBridge_LockUnlock.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylBridge_LockUnlock); goto End; }
                if (m_inpBridgePlungerLocked.Read() != 1) { Set_Error((int)ErrorTable.en_ErrorCode.SENSOR_NOT_TRIGGERED_ERR, m_inpBridgePlungerLocked.Label); goto End; }
            }

            //end
            Flag_InitOK = true;
        End:
            Flag_Init = false;
        }

		/************************************************************************************************************************
		 * Auto Sequence
		 ************************************************************************************************************************/
		//Start thread///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Start_Run()
        {
            //Start Run Manual Station thread
            if (Module_Running == false)
            {
                //thread run
                m_thRun = new Thread(Run);
                m_thRun.Start();
            }

            return true;
        }

		public void Stop_Run()
		{
			Module_StopRun = true;
		}

        public bool Is_Runing()
        {
            if (Module_Running == false)
            {
                return false;
            }

            return true;
        }

        public bool Is_Stop()
        {
            if (Module_Running == true)
            {
                return true;
            }

            return false;
        }

		//Auto Sequence Looping function////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            //Set variables
            Module_StopRun = false;
            Flag_Error = false;
            Module_Running = true;

            //set thread identifier, get start date time, upload to server, reset data
            int iThreadIdentifier = 1;
            Set_Station_DownTime(false, iThreadIdentifier);

            //Auto sequence loop
            while (true)
            {
                //check monitor input 
                if (m_pMonitorInput.Flag_ErrorMonitor_b == true) break;

                //stop module run
                if (Module_StopRun == true && Flag_RunAgain_b == false)
                {
                    Set_Station_DownTime(true, iThreadIdentifier); //get down date time, get description, save to xml file
                    SetLog("Sequence Stopped");
                    Module_StopRun = false;
                    break;
                }

                //run again flag reset. it will be set again at the sequence if required
                Flag_RunAgain_b = false;

                //Auto sequence
                if (this.Auto_Sequence() == false)
                {
                    Flag_Error = true;
                    Set_Station_DownTime(true, iThreadIdentifier); //get down date time, get description, save to xml file
                    break;
                }
            }

            //end
            Module_Running = false;
        }

		//Auto Sequence: Manual Station//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence()
        {
            //only for machine type 1 (Leg A)
            if (m_DataUnit.MachineTypeNum_i == 1)
            {
                //always check Run and Stop button to stop previous station from feeding more pallets
                if (m_inpButton_Run.Read() == 1)
                {
                    m_DataUnit.Flag_OP100_OpenBridge_b = false;
                }
                else if (m_inpButton_Stop.Read() == 1)
                {
                    m_DataUnit.Flag_OP100_OpenBridge_b = true;
                }
            }

            switch (m_iFlag_SequencePos)
            {
                case (int)m_Sequence.enStart:
                    {
                        //dont let sequence continue, forever loop here
                        if (m_DataUnit.Flag_Enable_TestMode_b == true) { Delay_ms(1000); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //Place to set the variables to default state. 
                case (int)m_Sequence.enStandby:
                    {
                        //reset data
                        m_iFlag_IndicatorLight_State = -1;
                        m_DataUnit.Reset_OP100_WT_Vision_Results();
                        m_DataUnit.RFID_OP100_Buffer_SN_str = "";

                        //reset IO
                        m_outIndicatorLight_Red.Write(0);
                        m_outIndicatorLight_Green.Write(0);
                        m_outStopper_1.Write(0);
                        
                        //lock the bridge
                        if (m_DataUnit.MachineTypeNum_i == 1)
                        {
                            if (m_cylBridge_LockUnlock.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylBridge_LockUnlock); return false; }
                            if (m_inpBridgePlungerLocked.Read() != 1) { Set_Error((int)ErrorTable.en_ErrorCode.SENSOR_NOT_TRIGGERED_ERR, m_inpBridgePlungerLocked.Label); return false; }
                        }

                        //reset flags
                        Flag_Sequence_NoGo_b = false;
                        m_DataUnit.Flag_Conv_OP100_Buffer_Sending_b = false;
                        m_DataUnit.Flag_Conv_OP100_Trigger_b = false;

                        //check pallet present
                        if (m_senPalletPresent_1.Read_Sensor(1) == true)
                        {
                            m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = false;
                        }
                        else
                        {
                            //check open bridge flag request, stop part from coming in, only for machine type 1 (Leg A)
                            if (m_DataUnit.Flag_OP100_OpenBridge_b == true && m_DataUnit.MachineTypeNum_i == 1 && m_DataUnit.Flag_Conv_OP90_Sending_b == false)
                            {
                                //set flag
                                m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = false;

                                //blink red + green, to indicate it is checking the condition
                                m_iFlag_IndicatorLight_State = 5;

                                m_iFlag_SequencePos = (int)m_Sequence.enBridge_CheckCondition;
                                break;
                            }
                            else
                            {
                                m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = true;
                            }
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PalletPresent;
                    break;

                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/

                case (int)m_Sequence.enWait_PalletPresent:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            Delay_ms(500);
                        }
                        else //auto run
                        {
                            //check buffer pallet present
                            if (m_senPalletPresent_1.Read_Sensor(1) == false)
                            {
                                //check here because need to clear existing pallet first
                                //check open bridge flag request, stop part from coming in, only for machine type 1 (Leg A)
                                if (m_DataUnit.Flag_OP100_OpenBridge_b == true && m_DataUnit.MachineTypeNum_i == 1 && m_DataUnit.Flag_Conv_OP90_Sending_b == false)
                                {
                                    //set flag
                                    m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = false;

                                    //blink red + green, to indicate it is checking the condition
                                    m_iFlag_IndicatorLight_State = 5;

                                    m_iFlag_SequencePos = (int)m_Sequence.enBridge_CheckCondition;
                                    break;
                                }

                                Delay_ms(10);
                                break;
                            }
                        }

                        //set flag
                        m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b = false;

                        //check bypass flag / end lot flag
                        if (m_DataUnit.Flag_Bypass_OP100_b == true || m_DataUnit.Flag_EndLot == true)
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enWait_Outgoing_Ready;
                            break;
                        }

                        //Read RFID
                        if (Read_RFIDScanner(2) == false) { return false; }

                        //get order number from server
                        if (Server_Get_WorkOrderNumber() == false) { return false; }

                        //Get Permission
                        int iRet = Server_SkipCheck();
                        if (iRet == -1) { return false; }	//return error
                        if (iRet != 1) //no go
                        {
                            Flag_Sequence_NoGo_b = true;

                            //blink red, to indicate that the pallet need to be unloaded
                            m_iFlag_IndicatorLight_State = 1;

                            m_iFlag_SequencePos = (int)m_Sequence.enWait_Pallet_Unload;
                            break;
                        }

                        //Server: UpdateInput
                        String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                        if (Server_UpdateInput(strDateTime) == false) { return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enCheck_Result;
                    break;

                case (int)m_Sequence.enWait_Outgoing_Ready:
                    {
                        //for bypass and end lot mode only
                        //check sub zone 3 ready flag
                        if (m_DataUnit.Flag_Conv_OP100_Outgoing_Ready_b == false)
                        {
                            Delay_ms(10);
                            break;
                        }

                        //stopper down
                        m_outStopper_1.Write(1);

                        //set flag
                        m_DataUnit.Flag_Conv_OP100_Buffer_Sending_b = true;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_Outgoing_PalletPresent;
                    break;

                case (int)m_Sequence.enWait_Outgoing_PalletPresent:
                    {
                        //wait for pallet present
                        if (m_DataUnit.Flag_Conv_OP100_Outgoing_Ready_b == true) { Delay_ms(10); break; }

                        //stopper up
                        m_outStopper_1.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                case (int)m_Sequence.enCheck_Result:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            m_outIndicatorLight_Green.Write(1);
                            m_outIndicatorLight_Red.Write(1);
                        }
                        else //auto run
                        {
                            //get data from server
                            if (Server_GetData() == false) { return false; }

                            //pass
                            if (
                                m_DataUnit.OP100_VisionResult_OP85_Terminal1_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP85_Terminal2_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP85_Terminal3_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP85_Terminal4_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP85_Terminal5_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP85_Terminal6_str == "Pass" &&

                                m_DataUnit.OP100_VisionResult_OP90_Wire1_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP90_Wire2_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP90_Wire3_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP90_Wire4_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP90_Wire5_str == "Pass" &&
                                m_DataUnit.OP100_VisionResult_OP90_Wire6_str == "Pass"

                                //m_DataUnit.OP100_WT_Result_OP90_HTR_Result_str == "Pass" &&
                                //m_DataUnit.OP100_WT_Result_OP90_RTD_Result_str == "Pass"
                               )
                            {
                                m_outIndicatorLight_Red.Write(0);
                                m_outIndicatorLight_Green.Write(1);
                            }
                            else //fail
                            {
                                //turn on red light
                                m_outIndicatorLight_Red.Write(1);
                                m_outIndicatorLight_Green.Write(0);

                                //blink red, to indicate that the pallet need to be unloaded
                                m_iFlag_IndicatorLight_State = 1;

                                //request by Fozie
                                //if server off, allow force pass, to continue to hotblower
                                if (m_Server.m_Settings.m_bEnable == false)
                                {
                                    m_iFlag_SequencePos = (int)m_Sequence.enWait_NextStationReady;
                                    break;
                                }
                                else
                                {
                                    m_iFlag_SequencePos = (int)m_Sequence.enWait_Pallet_Unload;
                                    break;
                                }
                            }
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_NextStationReady;
                    break;

                case (int)m_Sequence.enWait_NextStationReady:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            //check next station ready flag
                            if (m_DataUnit.Flag_Conv_OP100_Ready_b == false) //station elevation flag
                            {
                                Delay_ms(10);
                                break;
                            }
                        }
                        else
                        {
                            Delay_ms(500);
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PalletExit;
                    break;

                case (int)m_Sequence.enWait_PalletExit:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            Delay_ms(500);
                        }
                        else
                        {
                            //wait for pallet to be empty
                            if (m_senPalletPresent_1.Read_Sensor(0) == false) { Delay_ms(10); break; }
                        }

                        //pass RFID SN to station elevation
                        m_DataUnit.RFID_OP100_Station_SN_str = m_DataUnit.RFID_OP100_Buffer_SN_str;

                        //set trigger flag for station elevation to start cycle time count
                        m_DataUnit.Flag_Conv_OP100_Trigger_b = true;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PalletReach;
                    break;

                case (int)m_Sequence.enWait_PalletReach:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            Delay_ms(500);
                        }
                        else //wait next station reach
                        {
                            if (m_DataUnit.Flag_Conv_OP100_Ready_b == true) { Delay_ms(10); break; }
                        }

                        //reset trigger flag for station elevation
                        m_DataUnit.Flag_Conv_OP100_Trigger_b = false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                case (int)m_Sequence.enWait_Pallet_Unload:
                    {
                        //change indicator light state
                        IndicatorLight_State(m_iFlag_IndicatorLight_State, 50);

                        //wait for operator to unload the current pallet
                        if (m_senPalletPresent_1.Read_Sensor(0) == false)
                        {
                            Delay_ms(10);
                            break;
                        }

                        //calculate cycle time
                        //m_sw_CycleTime.Stop();
                        //double m_db_CycleTime = m_sw_CycleTime.Elapsed.TotalMilliseconds / 1000;
                        //m_DataUnit.Cycle_Time_OP100_str = Math.Round(m_db_CycleTime, 3).ToString();

                        //if no go then skip
                        if (Flag_Sequence_NoGo_b == false)
                        {
                            //Server: UpdateOutput and UpdateCycleTime
                            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
                            if (Server_UpdateOutput(0, strDateTime) == false) { return false; }
                            //if (Server_UpdateCycleTime(double.Parse(m_DataUnit.Cycle_Time_OP100_str), strDateTime) == false) { return false; }
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //Bridge sequence --------------------------------------------------------------------------------------------
                case (int)m_Sequence.enBridge_CheckCondition:
                    {
                        //change indicator light state
                        IndicatorLight_State(m_iFlag_IndicatorLight_State, 50);

                        //ready flag need to be true because if false means got pallet, then need to loop in this sequence
                        if (
                            m_DataUnit.Flag_Conv_OP100_Ready_b            == false ||
                            m_DataUnit.Flag_Conv_OP100_Sending_b          == true  || 
                            m_DataUnit.Flag_Conv_OP100_Outgoing_Ready_b   == false ||
                            m_DataUnit.Flag_Conv_OP100_Outgoing_Sending_b == true
                           )
                        {
                            Delay_ms(10);
                            break;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enBridge_Unlock;
                    break;

                case (int)m_Sequence.enBridge_Unlock:
                    {
                        //unlock bridge
                        if (m_cylBridge_LockUnlock.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylBridge_LockUnlock); return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enBridge_WaitRunButton;
                    break;

                case (int)m_Sequence.enBridge_WaitRunButton:
                    {
                        //static red + green, to indicate that bridge is ready to open or close
                        m_iFlag_IndicatorLight_State = 4;

                        //change indicator light state
                        IndicatorLight_State(m_iFlag_IndicatorLight_State, 50);

                        //check for bridge plunger first then wait until user press the Run Button
                        if (m_inpBridgePlungerLocked.Read() == 0 || m_inpButton_Run.Read() == 0)
                        {
                            Delay_ms(10);
                            break;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enBridge_Lock;
                    break;

                case (int)m_Sequence.enBridge_Lock:
                    {
                        //change indicator light state
                        IndicatorLight_State(m_iFlag_IndicatorLight_State, 50);

                        //lock bridge
                        if (m_cylBridge_LockUnlock.Cyl_Trigger_N_WaitTaskComplete(true) == false)
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enBridge_WaitRunButton;

                            Set_CylError(m_cylBridge_LockUnlock);
                            return false;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //---------------------------------------------------------------------------------------------------------------------------

                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enEnd;
                    break;

                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStart;
                    break;
            }

            //set seq log
            SetLog_Sequence(m_iFlag_SequencePos);

            return true;
        }

		//Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public enum m_Sequence
		{
			enStart,
			enStandby,

			//sequence numbering is written here
			enWait_PalletPresent,
            enWait_Outgoing_Ready,
            enWait_Outgoing_PalletPresent,
            enCheck_Result,
			enWait_NextStationReady,
			enWait_PalletExit,
			enWait_PalletReach,

            enWait_Pallet_Unload,

            enBridge_CheckCondition,
            enBridge_Unlock,
            enBridge_WaitRunButton,
            enBridge_Lock,
            //////////////////////////////////////

			enPreEnd,
			enEnd
		}
	}
}