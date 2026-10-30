using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_ManualStationOP125
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;
		Comm_TCP_RFID m_RFIDScanner = null;
		Log_Data m_LogFile = null;
		Server_COMTCP_GT_CentralPC m_Server = null;
        Data_PackageFile m_DataPackageFile = null;
        Comm_Serial_KeyenceIL m_LaserSensor = null;
		
		//IO
        public InputBit m_inpButton_Red = null;
        public InputBit m_inpButton_Yellow = null;

        public Sensor m_senTopEnclosure_PartPresent = null;

        public OutputBit m_outIndicatorLight_Green = null;
        public OutputBit m_outIndicatorLight_Red = null;
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

		public bool Module_Runing
		{
			set { lock ( m_lockRun ) { m_bFlag_Run = value; } }
			get { lock ( m_lockRun ) { return m_bFlag_Run; } }
		}                                                                //InDicate if the module is running
		public bool Module_StopRun
		{
			set { lock ( m_lockRun ) { m_bFlag_RunEnd = value; } }
			get { lock ( m_lockRun ) { return m_bFlag_RunEnd; } }
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
			
		#endregion

		//Position Tracker
		private readonly object m_lockPositionTracker = new object();
		public String m_strPosTracker = "none";                             
		public String Position_Tracker { set { lock ( m_lockPositionTracker ) { m_strPosTracker = value; } } get { lock ( m_lockPositionTracker ) { return m_strPosTracker; } } }


		public int m_iFlag_SequencePos = 0;
		int m_iFlagSeqLog_Skip = 0;							//avoid repeat logging

		public int m_iErrorID = 0;
		public String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";
        public int m_iFlag_HotBlowerPointCount = 0;        //Counter: Count Hot Blower points
        Stopwatch m_sw_CycleTime = new Stopwatch();
        int iRetryCount = 0;                  //Counter for retry sequence
		Thread m_thRun = null;

		//constructor//////////////////////////////////////////////////////////////////////////////////
        public Module_ManualStationOP125(SeqMachineControl mSeqMachineCtrl)
		{
			m_SeqMachineCtrl		= mSeqMachineCtrl;
			m_MachineInfo			= m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit				= m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			= m_SeqMachineCtrl.m_MonitorInput;
			m_RFIDScanner			= m_SeqMachineCtrl.m_RFIDReader_ManualStation;
            m_DataPackageFile       = m_SeqMachineCtrl.m_DataPackageFile;
            m_LaserSensor           = m_SeqMachineCtrl.m_CommSerial_KeyenceIL;

            //server
			m_Server				= new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_HotBlower", m_MachineInfo.Get_MachineFilePath());
            m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);

			//setup log file
			m_LogFile = new Log_Data();
            m_LogFile.SetFolderName("Machine_Sequence_Folder\\OP125");
			m_LogFile.SetFileName("LogFile");
			m_LogFile.SeqLog_StartThread();
		}

		//Clean up////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Clean_Up()
		{
			m_LogFile.Clean_Up();
		}
				
        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_InputsOutputs( InterfaceCards mInterface )
		{
			int iBitID = 0, iBitID_Ret = 0;
			int iRetID = 0, iExtID = 0;

			String strFilePath	= m_MachineInfo.Get_MachineFilePath();

            //sensor
            iBitID = 11;    m_senTopEnclosure_PartPresent   = new Sensor(mInterface.Get_InputPort(5), iBitID, "OP125 - Top Jig Present", strFilePath);
            
            iBitID = 1;     m_inpButton_Red                 = new InputBit(mInterface.Get_InputPort(12), iBitID, "OP125 - Fail Button");
            iBitID = 2;     m_inpButton_Yellow              = new InputBit(mInterface.Get_InputPort(12), iBitID, "OP125 - Pass Button");
            
            iBitID = 1;     m_outIndicatorLight_Green       = new OutputBit(mInterface.Get_OutputPort(12), iBitID, "OP125 - Indicator Green");
            iBitID = 2;     m_outIndicatorLight_Red         = new OutputBit(mInterface.Get_OutputPort(12), iBitID, "OP125 - Indicator Red");

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

            m_LogFile.Write_ToSeqLog("=====> Module_OP125_ManualStation: " + strLog);
        }

        private void SetLog_Sequence(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlagSeqLog_Skip == iSequencePos)
                return;
            else
                m_iFlagSeqLog_Skip = iSequencePos;

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
            String strError = "Module_OP125_ManualStation: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}
		public String Get_ErrorMsg_Front()
		{
            String strError = "Module_OP125_ManualStation: " + m_strErrorMsg_Front + ": ";
			m_strErrorMsg_Front = "";
			return strError;
		}

		//Set Error Message///////////////////////////////////////////////////////////////////////////////
		public void Set_Error( int iErrorID, String strErrorMsgAdd, String strErrorMsgFront = "" )
		{
			this.m_iErrorID = iErrorID;
			this.m_strErrorMsgAdd = strErrorMsgAdd;
			this.m_strErrorMsg_Front = strErrorMsgFront;

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
        
        //Server: Write Result to Server/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Server_SendResult()
        {
            String strData = "";

            //dry run
            if (m_DataUnit.Flag_DryRun == true) return true;

            //compile string: data with vision results
            strData += "heightcheck_result_OP125" + ",";
            strData += m_DataUnit.HeightCheck_1_str + ",";
            strData += m_DataUnit.HeightCheck_2_str + ",";
            strData += m_DataUnit.HeightCheck_Result_str + ",";

            //Send Result to Server
            if (!m_Server.Cmd_UpdateResults(m_DataUnit.RFID_SN_OP125_str, strData))
            { Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel); return false; }

            return true;
        }

        //keyence IL read height check value then compare the value///////////////////////////////////////////////////////////////
        public bool Read_HeightCheckValue(int iPosition)
        {
            double dbValue = 0;
            String strValue = "";

            //dry run then skip
            if (m_DataUnit.Flag_DryRun == true) return false;

            //read height check value based on 4 different position
            if (m_LaserSensor.Read_TorqueValue(ref strValue) == false)
            {
                Set_Error((int)ErrorTable.en_ErrorCode.KEYENCE_IL_READ_ERR, ""); return false;
            }

            if (m_LaserSensor.Decode_Response(strValue) == false)
            {
                Set_Error((int)ErrorTable.en_ErrorCode.KEYENCE_IL_READ_ERR, ""); return false;
            }

            //compare each value
            switch (iPosition)
            {
                case 1:
                    {
                        dbValue = double.Parse(m_LaserSensor.m_CodeData.m_strHeightCheck1);
                        //replace value read from height check into data unit temp
                        m_DataUnit.Value_1 = Math.Round(dbValue,2) - 0.1;
                        m_DataUnit.HeightCheck_1_str = m_DataUnit.Value_1.ToString();

                        //Compare value
                        if ((m_DataUnit.Value_1 >= m_DataPackageFile.m_DataCommon.m_db_Point1_Min) && (m_DataUnit.Value_1 <= m_DataPackageFile.m_DataCommon.m_db_Point1_Max))
                        {
                            m_DataUnit.Value1_OK = true;
                        }
                        else
                        {
                            m_DataUnit.Value1_OK = false;
                        }
                    }
                    break;

                case 2:
                    {
                        dbValue = double.Parse(m_LaserSensor.m_CodeData.m_strHeightCheck2);
                        //replace value read from height check into data unit temp
                        m_DataUnit.Value_2 = Math.Round(dbValue,2) - 0.1;
                        m_DataUnit.HeightCheck_2_str = m_DataUnit.Value_2.ToString();

                        //Compare value
                        if ((m_DataUnit.Value_2 >= m_DataPackageFile.m_DataCommon.m_db_Point2_Min) && (m_DataUnit.Value_2 <= m_DataPackageFile.m_DataCommon.m_db_Point2_Max))
                        {
                            m_DataUnit.Value2_OK = true;
                        }
                        else
                        {
                            m_DataUnit.Value2_OK = false;
                        }
                    }
                    break;
            }

            return true;
        }

   

		/************************************************************************************************************************
		 * Initialization Sequence
		 ************************************************************************************************************************/
		//Start Initialization Thread//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Start_Init()
        {
            if (Flag_Init == false && Module_Runing == false)
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

            //reset sequence
            m_iFlag_SequencePos = 0;
            m_iFlagSeqLog_Skip = 0;

            //reset flag
            m_DataUnit.Flag_StationOP125_Done_b = false;

            //reset IO
            m_outIndicatorLight_Green.Write(0);
            m_outIndicatorLight_Red.Write(0);

            //end
            Flag_InitOK = true;
        //End:
            Flag_Init = false;
        }


		/************************************************************************************************************************
		 * Auto Sequence
		 ************************************************************************************************************************/
		//Start thread///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Start_Run()
        {
            //Start Run MAIN Thread
            if (Module_Runing == false)
            {
                //Set server settings
                m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);

                //Thread run
                m_thRun = new Thread(Run);
                m_thRun.Start();
            }

            return true;
        }

		//Auto Sequence Looping function////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            //Set variables
            Module_StopRun = false;
            Flag_Error = false;
            Module_Runing = true;

            //Auto sequence loop
            while (true)
            {
                //check monitor input 
                if (m_pMonitorInput.Flag_ErrorMonitor_b == true) break;

                //Auto sequence
                if (this.Auto_Sequence() == false)
                {
                    Flag_Error = true;
                    break;
                }

                //stop module run
                if (Module_StopRun == true)
                {
                    SetLog("Sequence Stopped");
                    Module_StopRun = false;
                    break;
                }
            }

            //end
            Module_Runing = false;
        }

		//Auto Sequence//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence()
        {
            switch (m_iFlag_SequencePos)
            {
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                case (int)m_Sequence.enStandby:
                    {
                        //Reset Flag
                        m_DataUnit.Flag_StationOP125_Done_b = false;

                        //reset IO
                        //m_outIndicatorLight_Green.Write(0);
                        //m_outIndicatorLight_Red.Write(0);

                        //reset counter
                        iRetryCount = 0;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag;
                    break;

                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/
                case (int)m_Sequence.enWait_TriggerFlag:
                    {
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            Delay_ms(2000);
                            m_outIndicatorLight_Green.Write(1);
                            m_outIndicatorLight_Red.Write(1);
                            Delay_ms(2000);
                            m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                            break;
                        }
                        
                        //wait for trigger flag
                        if (m_DataUnit.Flag_StationOP125_Trigger_b == false) { Delay_ms(10); break; }

                        //check top jig present
                        if (m_senTopEnclosure_PartPresent.Read_Sensor() == 0) { Delay_ms(10); break; }

                        //reset IO due to the sequence is to fast. Operator unable to see the light.
                        m_outIndicatorLight_Green.Write(0);
                        m_outIndicatorLight_Red.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enHeight_Check;
                    break;

                case (int)m_Sequence.enHeight_Check:
                    {
                        //check retry counter, if not 0, skip the yellow button.
                        if (iRetryCount == 0)
                        {
                            //wait user press yellow button
                            if (m_inpButton_Yellow.Read() == 0) { Delay_ms(10); break; }
                        }

                        if (Read_HeightCheckValue(1) == false) return false;
                        if (Read_HeightCheckValue(2) == false) return false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enCalculate_Result;
                    break;

                case (int)m_Sequence.enCalculate_Result:
                    {
                        //check result
                        if (m_DataUnit.Value1_OK == true && m_DataUnit.Value2_OK == true)
                        {
                            m_DataUnit.OverallResult_OK = true;
                            if (m_DataUnit.OverallResult_OK == true) { m_DataUnit.HeightCheck_Result_str = "Pass"; }
                        }
                        else
                        {
                            m_DataUnit.OverallResult_OK = false;
                            if (m_DataUnit.OverallResult_OK == false) { m_DataUnit.HeightCheck_Result_str = "Fail"; }
                        }

                        //trigger on green indicator
                       if (m_DataUnit.HeightCheck_Result_str == "Pass")
                       {
                           m_outIndicatorLight_Green.Write(1);
                           m_outIndicatorLight_Red.Write(0);

                           m_iFlag_SequencePos = (int)m_Sequence.enWait_SendData;
                           break;
                       }
                       else
                       {
                           //trigger on red indicator
                           m_outIndicatorLight_Green.Write(0);
                           m_outIndicatorLight_Red.Write(1);
                       }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_UserInput;
                    break;

                case (int)m_Sequence.enWait_UserInput:
                    {
                        //check result, if fail, wait for user input
                        if (iRetryCount < 2 && m_DataUnit.OverallResult_OK == false)
                        {
                            //check user input
                            if (m_inpButton_Yellow.Read() == 1) //retry
                            {
                                //turn off indicator light
                                m_outIndicatorLight_Green.Write(0);
                                m_outIndicatorLight_Red.Write(0);

                                //increase retry counter
                                iRetryCount++;

                                m_iFlag_SequencePos = (int)m_Sequence.enHeight_Check;
                                break;
                            }
                            else if (m_inpButton_Red.Read() == 1) //reject
                            {
                                m_iFlag_SequencePos = (int)m_Sequence.enWait_SendData;
                                break;
                            }
                            else //loop this sequence
                            {
                                Delay_ms(10);
                                break;
                            }
                        }
                        else if (iRetryCount > 2)
                        {
                            //if retry
                            if (m_inpButton_Red.Read() == 0)
                            {
                                Delay_ms(10);
                                break;
                            }
                        }
                        else //if unknown flags, reset station
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                            break;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_SendData;
                    break;

                case (int)m_Sequence.enWait_SendData:
                    {
                        //write to data log
                        m_SeqMachineCtrl.Write_ToDataLog_OP125();

                        //send result to server
                        //if (!Server_SendResult()) return false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag_Down;
                    break;

                case (int)m_Sequence.enWait_TriggerFlag_Down:
                    {
                        m_DataUnit.Flag_StationOP125_Done_b = true;

                        if (m_DataUnit.Flag_StationOP125_Trigger_b == true) { Delay_ms(100); break; }

                        m_DataUnit.Flag_StationOP125_Done_b = false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

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
			enWait_TriggerFlag,
            enWait_PutTopEnclosure,
            enHeight_Check,
            enCalculate_Result,
            enWait_UserInput,
            enWait_SendData,
            enWait_TriggerFlag_Down,
            ///////////////////////////////////

			enPreEnd,
			enEnd
		}
	}
}