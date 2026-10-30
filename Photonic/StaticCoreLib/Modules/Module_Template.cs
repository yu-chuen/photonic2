using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_Template
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;
		ROBOTSTATION_Interface m_interfaceRobotStation = null;

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

		int m_iErrorID = 0;
		String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		Thread m_thRun = null;

		//constructor//////////////////////////////////////////////////////////////////////////////////
		public Module_Template( SeqMachineControl mSeqMachineCtrl )
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

		//Set Robot station///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: Main class is created at SeqMachineControl
		public void Set_RobotStation()
		{
		}

		//Create robot classes/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_RobotStation()
		{
			bool bRet = true;
			return bRet;
		}

		//Create Indexer Classes////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Create_Indexer()
		{
			bool bRet = true;
			return bRet;
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

			m_SeqMachineCtrl.SetLog("=====> Module_Template: " + strLog);
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
			String strError = "Module_Template: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}
		public String Get_ErrorMsg_Front()
		{
			String strError = "Module_Template" + m_strErrorMsg_Front + ": ";
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
		 * Robot Movement
		 ************************************************************************************************************************/
		//Robot: Goto Position///////////////////////////////////////////////////////////////////////////////
		public bool Goto_RobotPos(String strPos)
		{
			bool bRet = true;



			Position_Tracker = strPos;
			return bRet;
		}

		//Get Robot Position//////////////////////////////////////////////////////////////////////////////////
		public Robot_Pos_Settings Get_RobotPosition( String strPos, ref bool bRet )
		{
			Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
			bRet = true;



			return mPosition;
		}





		/************************************************************************************************************************
		 * Initialization Sequence
		 ************************************************************************************************************************/
        //Initialize module/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Init_Module()
		{
			//reset flag
			Flag_Init = true;
			Flag_InitOK = false;
			m_iFlag_SequencePos = 0;



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
					}
					break;



				/*********************************************************************************************************
				 * Sequence written here
				 *********************************************************************************************************/


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

			enPreEnd,
			enEnd
		}



	}
}
