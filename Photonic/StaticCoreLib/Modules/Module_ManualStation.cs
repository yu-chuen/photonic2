using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_ManualStation
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;
		Comm_TCP_RFID m_RFIDScanner = null;
		Log_Data m_LogFile = null;
		Server_SQL m_Server = null;

		//IO
		InputBit m_inpButton_Green = null;
		InputBit m_inpButton_Red = null;
		InputBit m_inpButton_Yellow = null;
		InputBit m_inpNextStation_ReadyToReceive = null;

		Sensor m_senPartPresent = null;

		OutputBit m_outNextStation_ReadyToSend = null;
		OutputBit m_outConv = null;
		OutputBit m_outStopper = null;

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
		#endregion

		//Position Tracker
		private readonly object m_lockPositionTracker = new object();
		public String m_strPosTracker = "none";                             
		public String Position_Tracker { set { lock ( m_lockPositionTracker ) { m_strPosTracker = value; } } get { lock ( m_lockPositionTracker ) { return m_strPosTracker; } } }


		public int m_iFlag_SequencePos = 0;
		public short m_iModule_StationID = 100;

		public int m_iErrorID = 0;
		public String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		Thread m_thRun = null;

		//constructor//////////////////////////////////////////////////////////////////////////////////
		public Module_ManualStation( SeqMachineControl mSeqMachineCtrl, short iModuleID = 100 )
		{
			m_SeqMachineCtrl		= mSeqMachineCtrl;
			m_MachineInfo				= m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit					= m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			= m_SeqMachineCtrl.m_MonitorInput;
			m_RFIDScanner			= m_SeqMachineCtrl.m_RFIDReader_ManualStation;

			m_iModule_StationID	= iModuleID;
			m_Server						= new Server_SQL("Server_ManualStat"+iModuleID.ToString(), m_MachineInfo.Get_MachineFilePath());

			//setup log file
			m_LogFile = new Log_Data();
			m_LogFile.SetFolderName("Machine_Sequence_Folder\\Manual_Station_" + iModuleID.ToString());
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
			int iBitID = 0;
			String strFilePath = m_MachineInfo.Get_MachineFilePath();
			MyIPort pPort = null;
			MyOPort pOPort = null;

			switch ( m_iModule_StationID )
			{
				//Station OP100-------------------------------------------------------------------
				case 100:
					{
						//Set port
						pPort = mInterface.Get_InputPort(1);
						pOPort = mInterface.Get_OutputPort(1);

						//add input bit
						iBitID = 0;		m_inpButton_Green = new InputBit(pPort, iBitID, "Button Green");

						//add sensor
						iBitID = 0;		m_senPartPresent = new Sensor(pPort, iBitID);	m_senPartPresent.Set_Label("Part Present", strFilePath);

						//add output bit
						iBitID = 0;		m_outConv			= new OutputBit (pOPort, iBitID, "Conveyor Trigger");
						iBitID = 0;		m_outStopper	= new OutputBit (pOPort, iBitID, "Stopper");

						//add cyclinder
					}
					break;

				//Station OP120---------------------------------------------------------------------
				case 120:
					{
						//set port
						pPort = mInterface.Get_InputPort(1);
						pOPort = mInterface.Get_OutputPort(1);
						
						//add input bit
						iBitID = 0;		m_inpButton_Green								= new InputBit(pPort, iBitID, "Button Green");
						iBitID = 0;		m_inpButton_Red									= new InputBit(pPort, iBitID, "Button Red");
						iBitID = 0;		m_inpButton_Yellow								= new InputBit(pPort, iBitID, "Button Yellow");
						iBitID = 0;		m_inpNextStation_ReadyToReceive	= new InputBit(pPort, iBitID, "NextStation ReadyToReceive");

						//add sensor
						iBitID = 0;		m_senPartPresent = new Sensor(pPort, iBitID);	m_senPartPresent.Set_Label("Part Present", strFilePath);

						//add output bit
						iBitID = 0;		m_outNextStation_ReadyToSend = new OutputBit (pOPort, iBitID, "Ready To Send");
						iBitID = 0;		m_outConv			= new OutputBit (pOPort, iBitID, "Conveyor Trigger");
						iBitID = 0;		m_outStopper	= new OutputBit (pOPort, iBitID, "Stopper");

						//add cyclinder
					}
					break;
			}

			return true;
		}


		//Delay//////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: Found that Thread.Sleep(ms) is very inaccurate and sometimes sleep more than the delay count
		public void Delay_ms( int iDelay_ms )
		{
			Stopwatch mSt = new Stopwatch();
			mSt.Start();
			while ( mSt.ElapsedMilliseconds < ( long ) iDelay_ms ) { }
			mSt.Stop();
		}

		//Logging///////////////////////////////////////////////////////////////////////////////////////////////////
		public void SetLog( String strLog, int iLevel = 1 )
		{
			//disable log if level clerance failed
			if ( iLevel < m_DataUnit.LogLevel_int ) return;

			m_LogFile.Write_ToSeqLog("=====> Module_ManualStation: [" + m_iModule_StationID.ToString() + "]: " + strLog);
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
			String strError = "ManualStation [" + m_iModule_StationID.ToString() + "]: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}
		public String Get_ErrorMsg_Front()
		{
			String strError = "ManualStation [" + m_iModule_StationID.ToString() + "]: " + m_strErrorMsg_Front + ": ";
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
			strLog = "ErrorID=" + iErrorID + " : Device: " + strErrorMsgFront + " : ErrorMsg=" + strErrorMsgAdd;
			SetLog(strLog, 999);
		}





		/************************************************************************************************************************
		 * Others / Supporting functions
		 ************************************************************************************************************************/
		//Set Flag Ready////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_Flag_ManualStation_Ready( bool bFlag )
		{
			if ( m_iModule_StationID == 100 )	//OP100
			{
				m_DataUnit.Flag_ManualStatOP100_Ready_b = bFlag;
			}

			if ( m_iModule_StationID == 120 ) //OP120
			{
				m_DataUnit.Flag_ManualStatOP120_Ready_b = bFlag;
			}
		}

		public void Set_Flag_ManualStation_Sending( bool bFlag )
		{
			if ( m_iModule_StationID == 100 )	//OP100
			{
				m_DataUnit.Flag_ManualStatOP100_Sending_b = bFlag;
			}

			if ( m_iModule_StationID == 120 ) //OP120
			{
				m_DataUnit.Flag_ManualStatOP120_Sending_b = bFlag;
				//set IO signal
				m_outNextStation_ReadyToSend.Write( ((bFlag == true) ? 1 : 0) );
			}
		}

		//Get Current Station Sending flag State//////////////////////////////////////////////////////////////////////////////////////////////
		public bool Check_Flag_ManualStation_Sending()
		{
			bool bFlag = false;

			switch ( m_iModule_StationID )
			{
				case 100: bFlag = m_DataUnit.Flag_ManualStatOP100_Sending_b; break;
				case 120: bFlag = m_DataUnit.Flag_ManualStatOP120_Sending_b; break;
			}

			return bFlag;
		}


		//Check next OP Ready flag///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Check_NextOPReadyFlag()
		{
			bool bFlag = false;

			switch ( m_iModule_StationID )
			{
				case 100: bFlag = m_DataUnit.Flag_HotBlowerStat_Ready_b; break;
				case 120:
					bFlag = ( m_inpNextStation_ReadyToReceive.Read() == 1 ) ? true : false;
					m_DataUnit.Flag_NextStationReady_b = bFlag;
					break;
			}

			return bFlag;
		}

		//Checl previous OP sending flag/////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Check_PreviousOPSendingFlag()
		{
			bool bFlag = false;

			switch ( m_iModule_StationID )
			{
				case 100: bFlag = m_DataUnit.Flag_ConvSendingStat_Vis1; break;
				case 120: bFlag = m_DataUnit.Flag_HotBlowerStat_Sending_b; break;
			}

			return bFlag;
		}

		//Reset RFID SN and other data////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Reset_Flags()
		{
			switch ( m_iModule_StationID )
			{
				case 100:
					{
						m_DataUnit.RFID_SN_OP100_str = "";
						m_DataUnit.Result_ManualStat_OP100_str = "N";
					}
					break;

				case 120:
					{
						m_DataUnit.RFID_SN_OP120_str = "";
						m_DataUnit.Result_ManualStat_OP120_str = "N";
					}
					break;
			}
		}

		//User Input/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// User input into the system via buttons. Pass, Reject or Rework. -1=>reject, 1=>pass, 2=>rework, 0=>no input
		/// </summary>
		/// <returns></returns>
		public int Check_UserInput()
		{
			int iRet = 1;

			switch ( m_iModule_StationID )
			{
				//OP100--------------------------------------------------------------------------
				case 100:
					{
						//user press go button
						if ( m_inpButton_Green.Read() == 0 ) { return 0; }

						//set result
						m_DataUnit.Result_ManualStat_OP100_str = "P";
					}
					break;

				//OP120--------------------------------------------------------------------------
				case 120:
					{
						//user select go, fail or rework button
						if ( m_inpButton_Green.Read() == 1 )		{ m_DataUnit.Result_ManualStat_OP120_str = "P"; iRet = 1; }
						else if (m_inpButton_Red.Read() == 1)		{ m_DataUnit.Result_ManualStat_OP120_str = "F"; iRet = -1; }
						else if ( m_inpButton_Yellow.Read() == 1 ) { m_DataUnit.Result_ManualStat_OP120_str = "R"; iRet = 2; }
						else
							return 0;
					}
					break;
			}

			return iRet;
		}

		//Read from RFID///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Read_RFIDScanner()
		{
			int iChannel = 0;
			String strRFID = "";

			switch ( m_iModule_StationID )
			{
				case 100: iChannel = 1; break;
				case 120: iChannel = 2; break;
			}

			//scan rfid
			if ( m_RFIDScanner.Send_Command(iChannel, ref strRFID) == false )
			{
				Set_Error(m_RFIDScanner.Get_ErrorID(), m_RFIDScanner.Get_ErrorMessage(), m_RFIDScanner.m_strLabel);
				return false;
			}

			//Set to rfid var
			switch ( m_iModule_StationID )
			{
				case 100: m_DataUnit.RFID_SN_OP100_str = strRFID; break;
				case 120: m_DataUnit.RFID_SN_OP120_str = strRFID; break;
			}

			return true;
		}

		//Server: Get Go or no GO/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Server_GetState()
		{
			String strPreviousStationName = "";
			String strRFID = "";
			int iRet = 0;

			//compile string
			switch ( m_iModule_StationID )
			{
				case 100:
					strRFID = m_DataUnit.RFID_SN_OP100_str; strPreviousStationName = m_MachineInfo.m_strVisionInspection;
					break;

				case 120:
					strRFID = m_DataUnit.RFID_SN_OP120_str; strPreviousStationName = m_MachineInfo.m_strHotBlower;
					break;
			}

			//get Server GO Permission
			iRet = m_Server.Database_GoPermission(strRFID, strPreviousStationName);

			//error
			if ( iRet == -1 ) Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), "");

			return iRet;
		}

		//Server: Write Result to Server/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Server_SendResult()
		{
			String strResult = "";
			String strCollumn = "";

			//compile string
			strResult += "1,";
			switch ( m_iModule_StationID )
			{
				case 100: 
					strCollumn += m_MachineInfo.m_strManualStationOP100; 
					strResult += m_DataUnit.Result_ManualStat_OP100_str; 
					break;
				
				case 120: 
					strCollumn += m_MachineInfo.m_strManualStationOP120; 
					strResult += m_DataUnit.Result_ManualStat_OP120_str; 
					break;
			}

			strCollumn += "," + m_Server.Result_str;

			//Send Result to Server
			if ( m_Server.Database_Update(m_DataUnit.RFID_SN_VIS_str, strResult, strCollumn) == false ) return false;

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
			//reset flag
			Flag_Init = true;
			Flag_InitOK = false;
			m_iFlag_SequencePos = 0;

			//IO
			m_outConv.Write(0);
			m_outStopper.Write(0);
			if ( m_iModule_StationID == 120 ) m_outNextStation_ReadyToSend.Write(0);


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
			if ( Module_Runing == false )
			{
				//Set server setting
				m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);

				//Start thread
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
			while ( true )
			{
				//check monitor input 
				if ( m_pMonitorInput.Flag_ErrorMonitor_b == true ) break;

				//Auto sequence
				if ( this.Auto_Sequence() == false )
				{
					Flag_Error = true;
					break;
				}

				//stop module run
				if ( Module_StopRun == true )
				{
					SetLog("Sequence Stopped");
					Module_StopRun = false;
					break;
				}
			}

			//end
			Module_Runing = false;
			//Additional
			Set_Flag_ManualStation_Ready(false);
		}

		//Auto Sequence//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Auto_Sequence()
		{
			switch ( m_iFlag_SequencePos )
			{
                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                //Place to set the variables to default state. 
				case ( int ) m_Sequence.enStandby:
					{
						//Reset Flag
						Set_Flag_ManualStation_Ready(true);
						Set_Flag_ManualStation_Sending(false);

						//reset stopper
						m_outStopper.Write(0);
						m_outConv.Write(0);
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_PartPresent;
					break;


				/*********************************************************************************************************
				 * Sequence written here
				 *********************************************************************************************************/
				case ( int ) m_Sequence.enWait_PartPresent:
					{
						//Trigger conv to run
						if ( Check_PreviousOPSendingFlag() == true ) 	{ m_outConv.Write(1); Delay_ms(100); break; }
						else
						{
							if ( m_outConv.Read() == 1 ) m_outConv.Write(0);		//stop conv from running
						}
						
						//wait for part to arrive: Check sensor
						if ( m_DataUnit.Flag_DryRun == true ) { Delay_ms(1000); }
						else
						{
							if ( m_senPartPresent.Read_Sensor() == 0 ) { Delay_ms(100); break; }
						}

						//part arrived. Down flag
						Set_Flag_ManualStation_Ready(false);
						//reset data flags
						Reset_Flags();
						//turn off conv
						m_outConv.Write(0);

						//Read RFID
						if ( !Read_RFIDScanner() ) return false;

						//Get Permission
						int iRet = Server_GetState();
						if ( iRet == -1 ) return false;					//return error
						if ( iRet <= 0 ) { m_iFlag_SequencePos = ( int ) m_Sequence.enWait_NextStationReady; break; }

					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_UserInput;
					break;

				case ( int ) m_Sequence.enWait_UserInput:
					{
						//dry run: Skip this sequence-----------------------------------------------------------------------------
						if ( m_DataUnit.Flag_DryRun == true )
						{
							m_iFlag_SequencePos = ( int ) m_Sequence.enWait_NextStationReady;
							break;
						}//---------------------------------------------------------------------------------------------------------------

						//Check part present; User manually removed pallet. send the station to standby again
						if ( m_senPartPresent.Read_Sensor() == 0 )
						{
							m_iFlag_SequencePos = ( int ) m_Sequence.enPreEnd;
							break;
						}

						//Check user input and set the results based on the user input buttons
						int iRet = Check_UserInput();
						if ( iRet == 0 ) { Delay_ms(100); break; }
						if ( iRet == 2 ) { m_iFlag_SequencePos = ( int ) m_Sequence.enRework_WaitPalletRemove; break; }

					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_NextStationReady;
					break;


				//rework sequence: remove pallet------------------------------------------------------------------------------
				case ( int ) m_Sequence.enRework_WaitPalletRemove:
					{
						//wait for pallet to be removed
						if ( m_senPartPresent.Read_Sensor() == 1 ) { Delay_ms(100); break; }
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enPreEnd;
					break;


				//normal sequence---------------------------------------------------------------------------------------------------
				case ( int ) m_Sequence.enWait_NextStationReady:
					{
						//wait next OP ready
						if ( Check_NextOPReadyFlag() == false ) { Delay_ms(100); break; }
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enStopperDown;
					break;

				case ( int ) m_Sequence.enStopperDown:
					{
						//retract stopper
						m_outStopper.Write(1);

						//Start conv to send part to exit
						m_outConv.Write(1);

						//Set flag
						Set_Flag_ManualStation_Sending(true);
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_PalletExit;
					break;

				case ( int ) m_Sequence.enWait_PalletExit:
					{
						//Set flag
						Set_Flag_ManualStation_Sending(true);

						//wait for next OP to down its flag to signal that the pallet has arrived.
						if ( Check_NextOPReadyFlag() == true ) { Delay_ms(100); break; }

						//Set flag
						Set_Flag_ManualStation_Sending(false);
						//Stopper up
						m_outStopper.Write(0);
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enPreEnd;
					break;





				//-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
				case ( int ) m_Sequence.enPreEnd:
					{
						SetLog("Main Seq: Pre End");
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enEnd;
					break;

				case ( int ) m_Sequence.enEnd:
					{
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enStart;
					break;
			}

			return true;
		}

		//Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public enum m_Sequence
		{
			enStart,
			enStandby,

			//sequence numbering is written here
			enWait_PartPresent,
			enStopperUp,
			enWait_UserInput,
			enWait_NextStationReady,
			enStopperDown,
			enWait_PalletExit,

			enRework_WaitPalletRemove,

			enPreEnd,
			enEnd
		}



	}
}
