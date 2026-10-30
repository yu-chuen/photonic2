using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_VisionInspection_Buffer
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;

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

		public int m_iErrorID = 0;
		public String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		Thread m_thRun = null;

		//constructor//////////////////////////////////////////////////////////////////////////////////
		public Module_VisionInspection_Buffer( SeqMachineControl mSeqMachineCtrl )
		{
			m_SeqMachineCtrl		= mSeqMachineCtrl;
			m_MachineInfo				= m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit					= m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			= m_SeqMachineCtrl.m_MonitorInput;
		}
				
        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_InputsOutputs( InterfaceCards mInterface )
		{
			int iBitID = 0;

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

			m_SeqMachineCtrl.SetLog("=====> Module_VisionInspection_Buffer: " + strLog);
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
			String strError = "Module_VisionInspection_Buffer: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}
		public String Get_ErrorMsg_Front()
		{
			String strError = "Module_VisionInspection_Buffer: " + m_strErrorMsg_Front + ": ";
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

			m_DataUnit.Flag_VisionStationBuffer_Insert_b = false;
			m_DataUnit.Flag_VisionStationBuffer_Ready_b = false;
			m_DataUnit.Flag_VisionStationBuffer_Done_b = false;


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
			if ( Module_Runing == false )
			{
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
						//reset flags

					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_Pallet;
					break;



				/*********************************************************************************************************
				 * Sequence written here
				 *********************************************************************************************************/
				case ( int ) m_Sequence.enWait_Pallet:
					{
						//wait for pallet to be triggered by conveyor
						if ( m_DataUnit.Flag_ConvReachTrigStat_VisBuffer_b == false ) { Delay_ms(100); break; }
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enPreEnd;
					break;


				case ( int ) m_Sequence.enWait_PalletByUser:
					{
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_NextStationReady;
					break;


				case ( int ) m_Sequence.enWait_NextStationReady:
					{
					}
					m_iFlag_SequencePos = ( int ) m_Sequence.enWait_PartExit;
					break;


				case ( int ) m_Sequence.enWait_PartExit:
					{
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
			enWait_Pallet,
			enWait_PalletByUser,
			enWait_NextStationReady,
			enWait_PartExit,

			enPreEnd,
			enEnd
		}



	}
}
