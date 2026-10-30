using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_OP110n120n125Conv
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;

		Log_Data m_LogFile = null;
		Comm_TCP_RFID m_RFIDScanner = null;
		Server_COMTCP_GT_CentralPC m_Server_110 = null;

		//IO
        //OP110
		public Sensor m_senPalletPresent_HotBlower				= null;
		public Sensor m_senPalletPassThru_HotBlower				= null;
        public Sensor m_senSlidingDoor_CloseIN				    = null;
		public Sensor m_senSlidingDoor_CloseOUT					= null;
        public OutputBit m_outStopper_HotBlower                 = null;
        public Cylinder m_cylDocking_HotBlower			        = null;
        public Cylinder m_cylSlidingDoor_IN					    = null;
		public Cylinder m_cylSlidingDoor_OUT				    = null;

		public OutputBit m_outConvFwd                           = null;
        public OutputBit m_outConvRev                           = null;
		
		
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

		private readonly object m_lock_Flag_RunAgain = new object();
		bool m_bFlag_RunAgain = false;															//signal: Continue even when stop signal is high. Not to be used at wait section
		public bool Flag_RunAgain_b { set { lock ( m_lock_Flag_RunAgain ) { m_bFlag_RunAgain = value; } } get { lock ( m_lock_Flag_RunAgain ) { return m_bFlag_RunAgain; } } }

		private readonly object m_lockRun_Conv = new object();                                       //Auto Sequence Run - Conv
		bool m_bFlag_Run_Conv = false;																				
		public bool Flag_Run_Conv { set { lock ( m_lockRun_Conv ) { m_bFlag_Run_Conv = value; } } get { lock ( m_lockRun_Conv ) { return m_bFlag_Run_Conv; } } }

		private readonly object m_lockRunEnd_Conv = new object();                                //Auto Sequence Run Stop- Conv
		bool m_bFlag_RunEnd_Conv = false;																		
		public bool Flag_RunEnd_Conv { set { lock ( m_lockRunEnd_Conv ) { m_bFlag_RunEnd_Conv = value; } } get { lock ( m_lockRunEnd_Conv ) { return m_bFlag_RunEnd_Conv; } } }

		private readonly object m_lockRun_HotBlower = new object();                                       //Auto Sequence Run - HotBlower
		bool m_bFlag_Run_HotBlower = false;
		public bool Flag_Run_HotBlower { set { lock ( m_lockRun_HotBlower ) { m_bFlag_Run_HotBlower = value; } } get { lock ( m_lockRun_HotBlower ) { return m_bFlag_Run_HotBlower; } } }

		private readonly object m_lockRunEnd_HotBlower = new object();                                //Auto Sequence Run Stop- HotBlower
		bool m_bFlag_RunEnd_HotBlower = false;
		public bool Flag_RunEnd_HotBlower { set { lock ( m_lockRunEnd_HotBlower ) { m_bFlag_RunEnd_HotBlower = value; } } get { lock ( m_lockRunEnd_HotBlower ) { return m_bFlag_RunEnd_HotBlower; } } }

		#endregion



		//Position Tracker
		private readonly object m_lockPositionTracker = new object();
		public String m_strPosTracker = "none";                             
		public String Position_Tracker { set { lock ( m_lockPositionTracker ) { m_strPosTracker = value; } } get { lock ( m_lockPositionTracker ) { return m_strPosTracker; } } }

		private readonly object m_lock_RFIDAccess = new object();				//lock: RFID Read and Write Function access. One thread can access at a time.
		private readonly object m_lock_NextStatSen = new object();				//lock: Read from the next station input

		public int m_iFlag_SequencePos_Conv = 0;					
        public int m_iFlag_SequencePos_HotBlower = 0;

        int m_iFlagSeqLog_Skip_OP110 = 0;							//avoid repeat logging

		int m_iErrorID = 0;
		String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		//thread
		Thread m_thRun_Conv = null;
        Thread m_thRun_HotBlower = null;

		//stopwatch
		Stopwatch m_st_ConvSending_Conv = new Stopwatch();
		Stopwatch m_st_ConvSending_HotBlower = new Stopwatch();
        Stopwatch m_sw_CycleTime_OP110 = new Stopwatch();


		//constructor//////////////////////////////////////////////////////////////////////////////////
        public Module_OP110n120n125Conv(SeqMachineControl mSeqMachineCtrl)
		{
			m_SeqMachineCtrl		    = mSeqMachineCtrl;
			m_MachineInfo				= m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit					= m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			    = m_SeqMachineCtrl.m_MonitorInput;
			m_RFIDScanner			    = m_SeqMachineCtrl.m_RFIDReader_ManualStation;

			m_Server_110				= new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_ManualStat110", m_MachineInfo.Get_MachineFilePath());
            Set_ServerSettings(m_SeqMachineCtrl.m_Server.m_Settings);

			//setup log file
			m_LogFile = new Log_Data();
			m_LogFile.SetFolderName("Machine_Sequence_Folder\\Conv100n120");
			m_LogFile.SetFileName("LogFile");
			m_LogFile.SeqLog_StartThread();
		}

		//Remove all running threads//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Clean_Up()
		{
			m_LogFile.Clean_Up();
		}
				
        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_InputsOutputs(InterfaceCards mInterface)
		{
			int iBitID = 0, iBitID_Ret = 0;
			int iRetID = 0, iExtID = 0;
			String strFilePath = m_MachineInfo.Get_MachineFilePath();

            //OP110
            #region OP110
            //add sensor
			iBitID = 0;		m_senPalletPresent_HotBlower	= new Sensor(mInterface.Get_InputPort(6), iBitID, "OP110 - Pallet Present", strFilePath);
            iBitID = 2;		m_senPalletPassThru_HotBlower   = new Sensor(mInterface.Get_InputPort(6), iBitID, "OP110 - Pallet Pass Thru", strFilePath);
            iBitID = 7;		m_senSlidingDoor_CloseIN		= new Sensor(mInterface.Get_InputPort(6), iBitID, "OP110 - Sliding Door Close IN", strFilePath);
            iBitID = 10;    m_senSlidingDoor_CloseOUT       = new Sensor(mInterface.Get_InputPort(6), iBitID, "OP110 - Sliding Door Close OUT", strFilePath);

            //add output
            iBitID = 0;		m_outStopper_HotBlower		    = new OutputBit(mInterface.Get_OutputPort(6), iBitID, "OP110 - Stopper");

            //add cylinder
            iBitID = 2; iBitID_Ret = 3; iExtID = 3; iRetID = 4; m_cylDocking_HotBlower = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                      mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                      mInterface.Get_InputPort(6), iExtID,
                                                                                                      mInterface.Get_InputPort(6), iRetID,
                                                                                                      "OP110 - Docking", strFilePath,
                                                                                                      m_pMonitorInput,
                                                                                                      "Up", "Down");

            iBitID = 5; iBitID_Ret = 4; iExtID = 6; iRetID = 5; m_cylSlidingDoor_IN = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                   mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                   mInterface.Get_InputPort(6), iExtID,
                                                                                                   mInterface.Get_InputPort(6), iRetID,
                                                                                                   "OP110 - Sliding Door IN", strFilePath,
                                                                                                   m_pMonitorInput,
                                                                                                   "Down", "Up");


            iBitID = 7; iBitID_Ret = 6; iExtID = 9; iRetID = 8; m_cylSlidingDoor_OUT = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                    mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(6), iExtID,
                                                                                                    mInterface.Get_InputPort(6), iRetID,
                                                                                                    "OP110 - Sliding Door OUT", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "Down", "Up");
            #endregion

			//output
			iBitID = 10;	m_outConvFwd				    = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "Conveyor Forward OP110 ~ OP125");
            iBitID = 11;    m_outConvRev                    = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "Conveyor Reverse OP110 ~ OP125");
			
			return true;
		}

		//Set Server Settings////////////////////////////////////////////////////////////////////////////////
        public void Set_ServerSettings(Server_COMTCP_GT_CentralPC_Settings mSettings)
        {
            m_Server_110.Set_Settings(mSettings);
        }


		//Delay//////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: Found that Thread.Sleep(ms) is very inaccurate and sometimes sleep more than the delay count
        public void Delay_ms_HB(int iDelay_ms)
        {
            ManualResetEvent mEvent = new ManualResetEvent(false);
            mEvent.WaitOne(iDelay_ms);
            mEvent.Dispose();
        }

		//Logging///////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetLog( String strLog, int iLevel = 1 )
		{
			//disable log if level clerance failed
			if ( iLevel < m_DataUnit.LogLevel_int ) return;

			m_SeqMachineCtrl.SetLog("=====> Module_OP110_OP120_OP125_Conv: " + strLog);
		}

        private void SetLog_Sequence_OP110(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlagSeqLog_Skip_OP110 == iSequencePos)
                return;
            else
                m_iFlagSeqLog_Skip_OP110 = iSequencePos;

            //write to log
            SetLog("OP110" + strLog);
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
			String strError = "Module_OP110_OP120_OP125_Conv: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}

		public String Get_ErrorMsg_Front()
		{
            String strError = "Module_OP110_OP120_OP125_Conv" + m_strErrorMsg_Front + ": ";
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

		public void Set_CylError( Cylinder mCyl ) { Set_Error(mCyl.Get_ErrorID(), mCyl.Get_ErrorMessage(), mCyl.Get_Label()); }







		/************************************************************************************************************************
		 * Others / Supporting functions
		 ************************************************************************************************************************/
		//Read RFID///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool RFIDScanner_Read( int iChannel )
		{
			lock ( m_lock_RFIDAccess )
			{
				String strRFID_SN = "DryRun_Dummy";

				//dry run
				if ( m_DataUnit.Flag_DryRun || m_DataUnit.Flag_EndLot ) return true;

				//Read from RFID Tag
				if ( m_RFIDScanner.Send_Command(iChannel, ref strRFID_SN) == false )
				{
					Set_Error(m_RFIDScanner.Get_ErrorID(), m_RFIDScanner.Get_ErrorMessage(), m_RFIDScanner.m_strLabel);
					return false;
				}

				//Set to Variable: Set RFID SN
                //if (iChannel == m_MachineInfo.m_iRFIDChannel_HotBlower)
                {
                    m_DataUnit.RFID_OP110_SN_str = strRFID_SN.Split(',')[0];
                }

				return true;
			}
		}

		//Server: Get Go or no GO/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: OP110 => Hotblower
		/// <summary>
		/// Get Server Go or NoGo signal. Skip check: -1=>error, 0=>no go, 1=>go
		/// </summary>
		/// <returns></returns>
        public int Server_GetState_HotBlower()
        {
            bool bSkipCheck = false;

            //dry run
            if (m_DataUnit.Flag_DryRun == true) { return 1; }

            //end lot
            if (m_DataUnit.Flag_EndLot == true)
            {
                m_DataUnit.RFID_OP110_SN_str = "Dummy_TestHB";
            }

            //get Server GO Permission
            if (!m_Server_110.Cmd_SkipCheck(m_DataUnit.RFID_OP110_SN_str, m_MachineInfo.m_strOP110, ref bSkipCheck))
            {
                Set_Error(m_Server_110.Get_ErrorID(), m_Server_110.Get_ErrorMessage(), m_Server_110.m_strLabel);
                return -1;
            }

            //dry run: temporary test
            if (m_DataUnit.Flag_DryRun) bSkipCheck = true;

            //error
            if (bSkipCheck == false) SetLog("Server_GetState_Vision: No Go", 2);

            return ((bSkipCheck == true) ? 1 : 0);
        }

        //Server: Write Station Done to Server////////////////////////////////////////////////////////////////////////////////////
        public bool Server_SendStationRunComplete_OP110()
        {
            //dry run
            if (m_DataUnit.Flag_DryRun == true) return true;

            //Send Result to Server
            if (!m_Server_110.Cmd_StationPalletDone(m_MachineInfo.m_strOP110, m_DataUnit.RFID_OP110_SN_str))
            {
                Set_Error(m_Server_110.Get_ErrorID(), m_Server_110.Get_ErrorMessage(), m_Server_110.m_strLabel);
                return false;
            }

            return true;
        }

		/************************************************************************************************************************
		 * Initialization Sequence
		 ************************************************************************************************************************/
		//Start Initialization Thread//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Start_Init()
		{
			if ( Flag_Init == false && Module_Runing == false )
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
            Flag_Run_Conv = false;
            Flag_Run_HotBlower = false;

            m_DataUnit.Flag_Conv_OP110_Ready_b = false;
            m_DataUnit.Flag_Conv_OP110_Sending_b = false;
            m_DataUnit.Flag_Conv_OP110_Trigger_b = false;

            m_DataUnit.Flag_StationOP120_Ready_b = false;
            m_DataUnit.Flag_StationOP120_Sending_b = false;
            m_DataUnit.Flag_StationOP120_Trigger_b = false;

            m_DataUnit.Flag_StationOP125_Ready_b = false;
            m_DataUnit.Flag_StationOP125_Sending_b = false;
            m_DataUnit.Flag_StationOP125_Trigger_b = false;
            
            //reset sequence
            m_iFlag_SequencePos_HotBlower = 0;
            m_iFlag_SequencePos_Conv = 0;

            m_iFlagSeqLog_Skip_OP110 = 0;

            //reset IO
            m_outStopper_HotBlower.Write(0);
            m_outConvFwd.Write(0);

            //reset cylinder
            m_cylSlidingDoor_IN.Init_Cylinder();
            m_cylSlidingDoor_OUT.Init_Cylinder();

            if (m_cylSlidingDoor_IN.TaskComplete() != 1) { Set_CylError(m_cylSlidingDoor_IN); goto End; }
            if (m_cylSlidingDoor_OUT.TaskComplete() != 1) { Set_CylError(m_cylSlidingDoor_OUT); goto End; }

            //wait heater biasor down first
            Delay_ms_HB(1000);
            m_cylDocking_HotBlower.Init_Cylinder();
            if (m_cylDocking_HotBlower.TaskComplete() != 1) { Set_CylError(m_cylDocking_HotBlower); goto End; }

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
            //Start Run MAIN Thread
            if (Flag_Run_Conv == false)
            {
                //conv thread run
                m_thRun_Conv = new Thread(Run_Conv);
                m_thRun_Conv.Start();
            }

            if (Flag_Run_HotBlower == false)
            {
                m_thRun_HotBlower = new Thread(Run_ConvHotBlower);
                m_thRun_HotBlower.Start();
            }

            return true;
        }

		//Stop Run
		public void Stop_Run()
		{
			Flag_RunEnd_Conv = true;
            Flag_RunEnd_HotBlower = true;
		}

		//is running
		public bool Is_Running()
		{
			if (
                Flag_Run_Conv       == false ||
				Flag_Run_HotBlower  == false
               )
				return false;

			return true;
		}

        public bool Is_Stop()
		{
			if (
                Flag_Run_Conv       == true ||
				Flag_Run_HotBlower  == true
               )
				return true;

			return false;
		}

		//Auto Sequence Looping function////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Run_Conv()
		{
			//Set variables
			Flag_RunEnd_Conv = false;
			Flag_Error = false;
			Flag_Run_Conv = true;
            Module_Runing = true;

			//Auto sequence loop
            while (true)
            {
                //check monitor input 
                if (m_pMonitorInput.Flag_ErrorMonitor_b == true) break;

                //Auto sequence
                if (this.Auto_Sequence_Conv() == false) { Flag_Error = true; break; }

                //stop module run
                if (Flag_RunEnd_Conv == true)
                {
                    SetLog("Sequence Stopped");
                    Flag_RunEnd_Conv = false;
                    break;
                }
            }

			//end
            Module_Runing = false;
			Flag_Run_Conv = false;
		}

        public void Run_ConvHotBlower()
        {
            //Set variables
            Flag_RunEnd_HotBlower = false;
            Flag_Error = false;
            Flag_Run_HotBlower = true;

            //Auto sequence loop
            while (true)
            {
                //check monitor input 
                if (m_pMonitorInput.Flag_ErrorMonitor_b == true) break;

                //Auto sequence
                if (this.Auto_Sequence_HotBlower() == false) { Flag_Error = true; break; }

                //stop module run
                if (Flag_RunEnd_HotBlower == true)
                {
                    SetLog("Sequence Stopped");
                    Flag_RunEnd_HotBlower = false;
                    break;
                }
            }

            //end
            Flag_Run_HotBlower = false;
        }


        //***************************************************************************************************************
		//Auto Sequence//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//***************************************************************************************************************
		//Conveyor Sequence
        public bool Auto_Sequence_Conv()
        {
            switch (m_iFlag_SequencePos_Conv)
            {
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos_Conv = (int)m_Sequence.enStandby;
                    break;

                case (int)m_Sequence.enStandby:
                    {
                    }
                    m_iFlag_SequencePos_Conv = (int)m_Sequence.enRunConv;
                    break;

                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/
                case (int)m_Sequence.enRunConv:
                    {
                        //delay: reduce cpu processing power
                        Thread.Sleep(10);

                        //process: run conveyor
                        if (
                            m_DataUnit.Flag_Conv_OP100_Sending_b == true ||
                            m_DataUnit.Flag_Conv_OP110_Sending_b == true ||
                            m_DataUnit.Flag_StationOP120_Sending_b == true ||
                            m_DataUnit.Flag_StationOP125_Sending_b == true
                           )
                        {
                            //run conv
                            if (m_outConvFwd.Read() == 0) { m_outConvFwd.Write(1); }

                            //reset timer
                            m_st_ConvSending_Conv.Reset();
                        }
                        else
                        {
                            //start timer
                            if (m_st_ConvSending_Conv.IsRunning == false) m_st_ConvSending_Conv.Start();

                            //check timer
                            if (m_st_ConvSending_Conv.ElapsedMilliseconds > 5000)
                            {
                                //stop conv
                                if (m_outConvFwd.Read() == 1) { m_outConvFwd.Write(0); }

                                //stop timer
                                m_st_ConvSending_Conv.Stop();

                                Thread.Sleep(100);
                            }
                        }
                    }
                    m_iFlag_SequencePos_Conv = (int)m_Sequence.enPreEnd;
                    break;

                //------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos_Conv = (int)m_Sequence.enEnd;
                    break;

                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos_Conv = (int)m_Sequence.enStart;
                    break;
            }

            return true;
        }

		//***************************************************************************************************************
		//Hotblower Sequence
        public bool Auto_Sequence_HotBlower()
        {
            switch (m_iFlag_SequencePos_HotBlower)
            {
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enStandby;
                    break;

                //reset everything to default state here
                case (int)m_Sequence.enStandby:
                    {
                        //reset flags
                        if (m_senPalletPresent_HotBlower.Read_Sensor() == 0)
                        {
                            m_DataUnit.Flag_Conv_OP110_Ready_b = true;
                        }
                        else
                        {
                            m_DataUnit.Flag_Conv_OP110_Ready_b = false;
                        }
                        m_DataUnit.Flag_Conv_OP110_Sending_b = false;
                        m_DataUnit.Flag_Conv_OP110_Trigger_b = false;

                        //reset IO
                        m_outStopper_HotBlower.Write(0);

                        //reset cylinder
                        if (!m_cylDocking_HotBlower.Cyl_TriggerNWaitDone(false)) { Set_CylError(m_cylDocking_HotBlower); return false; }
                        if (!m_cylSlidingDoor_IN.Cyl_TriggerNWaitDone(false)) { Set_CylError(m_cylSlidingDoor_IN); return false; }
                        if (!m_cylSlidingDoor_OUT.Cyl_TriggerNWaitDone(true)) { Set_CylError(m_cylSlidingDoor_OUT); return false; }
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_PalletPresent;
                    break;


                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/
                case (int)m_Sequence.enWait_PalletPresent:
                    {
                        //wait part arrive
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            //wait part reach
                            if (m_senPalletPresent_HotBlower.Read_Sensor() == 0) 
                            { 
                                Delay_ms_HB(10); 
                                break; 
                            }

                            //bypass to next station: Kamarul request
                            if (m_SeqMachineCtrl.m_Module_OP110_HotBlower.Module_Runing == false)
                            {
                                m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_NextStationReady;
                                break;
                            }
                        }
                        else
                        {
                            Delay_ms_HB(500);
                        }

                        //reset cycle time
                        m_DataUnit.Cycle_Time_OP110_str = "";

                        //start cycle time
                        m_sw_CycleTime_OP110.Restart();

                        //reset flag
                        m_DataUnit.Flag_Conv_OP110_Ready_b = false;
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enRead_RFID;
                    break;

                case (int)m_Sequence.enRead_RFID:
                    {
                        //read RFID
                        if (RFIDScanner_Read(2) == false) { return false; }
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enServer_SkipCheck;
                    break;

                case (int)m_Sequence.enServer_SkipCheck:
                    {
                        //Server Permission
                        int iRet = Server_GetState_HotBlower();
                        if (iRet == -1) return false;	//server error
                        if (iRet != 1) { m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_NextStationReady; break; }

                        

                        //sliding door down
                        if (!m_cylSlidingDoor_IN.Cyl_TriggerNWaitDone(true)) { Set_CylError(m_cylSlidingDoor_IN); return false; }

                        //end lot
                        if (m_DataUnit.Flag_EndLot == true) { m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_NextStationReady; break; }

                        //dock up
                        if (!m_cylDocking_HotBlower.Cyl_TriggerNWaitDone(true)) { Set_CylError(m_cylDocking_HotBlower); return false; }

                        //trigger
                        m_DataUnit.Flag_Conv_OP110_Trigger_b = true;
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_WorkDone;
                    break;

                case (int)m_Sequence.enWait_WorkDone:
                    {
                        //wait station done
                        if (m_DataUnit.Flag_DryRun == true)
                        { 
                            Delay_ms_HB(500); 
                        }
                        else
                        {
                            if (m_DataUnit.Flag_Station_OP110_Done_b == false) 
                            { 
                                Delay_ms_HB(10);
                                break; 
                            }
                        }

                        //set flag
                        m_DataUnit.Flag_Conv_OP110_Trigger_b = false;

                        //docking down
                        if (!m_cylDocking_HotBlower.Cyl_TriggerNWaitDone(false)) { Set_CylError(m_cylDocking_HotBlower); return false; }

                        //send station done to server
                        if (!Server_SendStationRunComplete_OP110()) return false;
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_NextStationReady;
                    break;

                case (int)m_Sequence.enWait_NextStationReady:
                    {
                        //wait next station to be ready
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            if (m_DataUnit.Flag_StationOP120_Ready_b == false)
                            {
                                Delay_ms_HB(10);
                                break;
                            }
                        }
                        else
                        {
                            Delay_ms_HB(500);
                        }

                        //sliding door out up
                        if (!m_cylSlidingDoor_OUT.Cyl_TriggerNWaitDone(false)) { Set_CylError(m_cylSlidingDoor_OUT); return false; }

                        //stopper down
                        m_outStopper_HotBlower.Write(1);

                        //Up sending flag
                        m_DataUnit.Flag_Conv_OP110_Sending_b = true;
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_PalletExit;
                    break;

                case (int)m_Sequence.enWait_PalletExit:
                    {
                        //wait next station to be receive the part
                        if (m_DataUnit.Flag_DryRun == true) 
                        { 
                            Delay_ms_HB(500); 
                        }
                        else
                        {
                            if (m_senPalletPassThru_HotBlower.Read() == 0) 
                            { 
                                Delay_ms_HB(10); 
                                break; 
                            }
                        }

                        //Up stopper
                        m_outStopper_HotBlower.Write(0);

                        //Up ready flag to let next pallet to come
                        m_DataUnit.Flag_Conv_OP110_Ready_b = true;

                        //Start Timer: Pallet reach next station timeout
                        m_st_ConvSending_HotBlower.Restart();
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enWait_PalletReach;
                    break;

                case (int)m_Sequence.enWait_PalletReach:
                    {
                        //dry run
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            Delay_ms_HB(500);
                        }
                        else
                        {
                            //wait pallet reach
                            if (m_st_ConvSending_HotBlower.ElapsedMilliseconds < 5000)
                            {
                                if (m_DataUnit.Flag_StationOP120_Ready_b == true) 
                                { 
                                    Delay_ms_HB(10);
                                    break;
                                }
                            }
                        }

                        //stop timer
                        m_st_ConvSending_HotBlower.Stop();

                        //calculate cycle time
                        m_sw_CycleTime_OP110.Stop();
                        double m_db_CycleTime = m_sw_CycleTime_OP110.Elapsed.TotalMilliseconds / 1000;
                        m_DataUnit.Cycle_Time_OP110_str = Math.Round(m_db_CycleTime, 3).ToString();
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enPreEnd;
                    break;


                //---------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enEnd;
                    break;

                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos_HotBlower = (int)m_Sequence.enStart;
                    break;
            }

            //set log
            SetLog_Sequence_OP110(m_iFlag_SequencePos_HotBlower);

            return true;
        }

		//Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public enum m_Sequence
		{
			enStart,
			enStandby,

			//sequence numbering is written here
			enWait_PalletPresent,
            enWait_UserRemovePart,
			enWait_WorkDone,
			enRFID_SetOP,
            enRead_RFID,
            enServer_SkipCheck,
			enWait_NextStationReady,
			enWait_PalletExit,
			enWait_PalletReach,

			//Run or Stop Conv
			enRunConv,

			enPreEnd,
			enEnd
		}
	}
}