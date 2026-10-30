using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
	public class Module_ManualStationOP120
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;
		MachineInfo m_MachineInfo = null;
		Data_UnitTempStorage m_DataUnit = null;
        Data_PackageFile m_DataPackageFile = null;

		Log_Data m_LogFile = null;
		Comm_TCP_RFID m_RFIDScanner = null;
		Server_COMTCP_GT_CentralPC m_Server = null;


		//IO
		public Sensor m_senGripper_PartPresent = null;
		public Sensor m_senSlideTable_PartPresent = null;
		public Sensor m_senGripper_InPos = null;
        public Sensor m_senSafetyCurtain = null;
        public Sensor m_senSpringComb = null;

		public InputBit m_inpButton_Green = null;
        public InputBit m_inpOP120Vacuum_On = null;
        public InputBit m_inpPressureSwitch_Upper = null;

		public OutputBit m_outOP120Vacuum_On = null;
        public OutputBit m_outSafetyCurtain_Indicator = null;
       
		public Cylinder m_cylNest_UpDn = null;
		public Cylinder m_cylGripper_InOut = null;
        public Cylinder m_cylPNPGripper_UpDn = null;
        public Cylinder m_cylNestGripper_UpDn = null;
		public Cylinder m_cylSlideTable_GripUngrip = null;
		public Cylinder m_cylGripper_GripUngrip = null;


		//Flags
		#region Flags
		private readonly object m_lockRun = new object();                                       //Lock for thread sequence
		private readonly object m_lockRunEnd = new object();                                //Lock for thread sequence End
		private readonly object m_lockProcessRun = new object();                        //Flag to indicate that the process is running
		private readonly object m_lockInit = new object();                                          //Lock for Init Sequence
		private readonly object m_lockInit_OK = new object();                                   //Lock for Init Sequence
		private readonly object m_lock_Flag_Error = new object();                           //Lock for Error occur at auto sequence
        private readonly object m_lockRun_SafetyCurtain = new object();  

		bool m_bFlag_Init = false;
		bool m_bFlag_InitOK = false;																	//indicate init is done and good
		bool m_bFlag_Run = false;																		//indicate thread is running
		bool m_bFlag_RunEnd = false;																//signal thread to stop
		bool m_bFlag_Error = false;																	//signal: Error occur
		bool m_bFlag_ProcessRun = false;														//signal process has began
        bool m_bFlag_RunSafetyCurtain = false;
        bool m_bFlag_RunEnd_SafetyCurtain = false;

		public bool Module_Runing
		{
            set { lock (m_lockRun) { m_bFlag_Run = value; } }
            get { lock (m_lockRun) { return m_bFlag_Run; } }
		}                                                               //InDicate if the module is running

        public bool Module_Runing_SafetyCurtain
        {
            set { lock (m_lockRun_SafetyCurtain) { m_bFlag_RunSafetyCurtain = value; } }
            get { lock (m_lockRun_SafetyCurtain) { return m_bFlag_RunSafetyCurtain; } }
        }                                                               //InDicate if the module is running

        public bool Module_StopRun
		{
			set { lock ( m_lockRun ) { m_bFlag_RunEnd = value; } }
			get { lock ( m_lockRun ) { return m_bFlag_RunEnd; } }
		}                                                             //Stop running thread

        public bool Module_StopRun_SafetyCurtain
        {
            set { lock (m_lockRun_SafetyCurtain) { m_bFlag_RunEnd_SafetyCurtain = value; } }
            get { lock (m_lockRun_SafetyCurtain) { return m_bFlag_RunEnd_SafetyCurtain; } }
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
 
      

		#endregion

		//Position Tracker
		private readonly object m_lockPositionTracker = new object();
		public String m_strPosTracker = "none";                             
		public String Position_Tracker { set { lock ( m_lockPositionTracker ) { m_strPosTracker = value; } } get { lock ( m_lockPositionTracker ) { return m_strPosTracker; } } }


		public int m_iFlag_SequencePos = 0;
        public int m_iFlag_SequencePos_SafetyCurtain = 0;
		int m_iFlagSeqLog_Skip = 0;							//avoid repeat logging
        int m_iFlagSeqLog_Skip_SafetyCurtain = 0;							//avoid repeat logging
        int m_iFlagSeqLog_Skip_ManualStation = 0;							//avoid repeat logging
        
        long lg_Timeout_Run = 0;
		int m_iErrorID = 0;
		String m_strErrorMsgAdd = "";
		String m_strErrorMsg_Front = "";

		Thread m_thRun = null;
        Thread m_thRun_SafetyCurtain = null;
        Stopwatch mSt = new Stopwatch();


		//constructor//////////////////////////////////////////////////////////////////////////////////
		public Module_ManualStationOP120( SeqMachineControl mSeqMachineCtrl )
		{
			m_SeqMachineCtrl  = mSeqMachineCtrl;
			m_MachineInfo	  = m_SeqMachineCtrl.m_MachineInfo;
			m_DataUnit		  = m_SeqMachineCtrl.m_DataUnit;
			m_pMonitorInput	  = m_SeqMachineCtrl.m_MonitorInput;
			m_RFIDScanner	  = m_SeqMachineCtrl.m_RFIDReader_ManualStation;
            m_DataPackageFile = m_SeqMachineCtrl.m_DataPackageFile;

            //server
            m_Server          = new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_ManualStat120", m_MachineInfo.Get_MachineFilePath());
            m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);

			//setup log file
			m_LogFile = new Log_Data();
			m_LogFile.SetFolderName("Machine_Sequence_Folder\\OP120");
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
            int iBitID = 0, iBitID_Ret = 0;
            int iRetID = 0, iExtID = 0;
            String strFilePath = m_MachineInfo.Get_MachineFilePath();

            //sensor
            //card 6 (DI13) malfunction
            iBitID = 7;     m_senSafetyCurtain              = new Sensor(mInterface.Get_InputPort(12), iBitID, "OP120 - Safety Curtain", strFilePath);
            iBitID = 7;     m_senGripper_InPos              = new Sensor(mInterface.Get_InputPort(11), iBitID, "OP120 - Lead In Gripper In Position", strFilePath);
            iBitID = 12;    m_senSlideTable_PartPresent     = new Sensor(mInterface.Get_InputPort(11), iBitID, "OP120 - Slide Table Part Present", strFilePath);
            iBitID = 15;    m_senGripper_PartPresent        = new Sensor(mInterface.Get_InputPort(11), iBitID, "OP120 - Lead In Gripper Part Present", strFilePath);
            iBitID = 10;    m_senSpringComb                 = new Sensor(mInterface.Get_InputPort(12), iBitID, "OP120 - Spring Comb Present", strFilePath);

            iBitID = 0;     m_inpButton_Green               = new InputBit(mInterface.Get_InputPort(12), iBitID, "OP120 - Start Button");
            iBitID = 2;     m_inpOP120Vacuum_On             = new InputBit(mInterface.Get_InputPort(11), iBitID, "OP120 - Vacuum");
            iBitID = 15;    m_inpPressureSwitch_Upper       = new InputBit(mInterface.Get_InputPort(12), iBitID, "OP120 - Pressure Switch Upper Limit");

            //output
            iBitID = 5;     m_outOP120Vacuum_On             = new OutputBit(mInterface.Get_OutputPort(12), iBitID, "OP120 - Vacuum");
            iBitID = 4;     m_outSafetyCurtain_Indicator    = new OutputBit(mInterface.Get_OutputPort(12), iBitID, "OP120 - Safety Curtain");

            //cylinder
            iBitID = 0; iBitID_Ret = 1; iExtID = 0; iRetID = 1; m_cylNest_UpDn = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                mInterface.Get_InputPort(11), iExtID,
                                                                                                mInterface.Get_InputPort(11), iRetID,
                                                                                                "OP120 - Lead In Nest", strFilePath,
                                                                                                m_pMonitorInput,
                                                                                                "Up", "Down");

            iBitID = 2; iBitID_Ret = 3; iExtID = 3; iRetID = 4; m_cylGripper_InOut = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                    mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(11), iExtID,
                                                                                                    mInterface.Get_InputPort(11), iRetID,
                                                                                                    "OP120 - Lead In Gripper (InOut)", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "In", "Out");

            iBitID = 4; iBitID_Ret = 5; iExtID = 5; iRetID = 6; m_cylPNPGripper_UpDn = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                    mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(11), iExtID,
                                                                                                    mInterface.Get_InputPort(11), iRetID,
                                                                                                    "OP120 - PNP Gripper (UpDown)", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "Up", "Down");

            iBitID = 4; iBitID_Ret = 5; iExtID = 5; iRetID = 7; m_cylNestGripper_UpDn = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                    mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(11), iExtID,
                                                                                                    mInterface.Get_InputPort(11), iRetID,
                                                                                                    "OP120 - Nest Gripper (UpDown)", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "Up", "Down");

            iBitID = 6; iBitID_Ret = 7; iExtID = 8; iRetID = 9; m_cylSlideTable_GripUngrip = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                            mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                            mInterface.Get_InputPort(11), iExtID,
                                                                                                            mInterface.Get_InputPort(11), iRetID,
                                                                                                            "OP120 - SlidingTable", strFilePath,
                                                                                                            m_pMonitorInput,
                                                                                                            "Grip", "Ungrip");


            iBitID = 10; iBitID_Ret = 11; iExtID = 13; iRetID = 14; m_cylGripper_GripUngrip = new Cylinder(mInterface.Get_OutputPort(11), iBitID,
                                                                                                            mInterface.Get_OutputPort(11), iBitID_Ret,
                                                                                                            mInterface.Get_InputPort(11), iExtID,
                                                                                                            mInterface.Get_InputPort(11), iRetID,
                                                                                                            "OP120 - Lead In Gripper (GripUngrip)", strFilePath,
                                                                                                            m_pMonitorInput,
                                                                                                            "Grip", "Ungrip");

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

            m_LogFile.Write_ToSeqLog("=====> Module_OP120_ManualStation: " + strLog);
        }

        private void SetLog_Sequence(int iSequencePos, String str_Sequence_Name)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (str_Sequence_Name.Contains("ManualStation"))
            {
                if (m_iFlagSeqLog_Skip_ManualStation == iSequencePos) { return; }
                else { m_iFlagSeqLog_Skip_ManualStation = iSequencePos; }
            }
            else if (str_Sequence_Name.Contains("SafetyCurtain"))
            {
                if (m_iFlagSeqLog_Skip_SafetyCurtain == iSequencePos) { return; }
                else { m_iFlagSeqLog_Skip_SafetyCurtain = iSequencePos; }
            }
            else
            {
                if (m_iFlagSeqLog_Skip == iSequencePos) { return; }
                else { m_iFlagSeqLog_Skip = iSequencePos; }
            }
            
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
            String strError = "Module_OP120_ManualStation: " + m_strErrorMsgAdd;
			m_strErrorMsgAdd = "";
			return strError;
		}
		public String Get_ErrorMsg_Front()
		{
            String strError = "Module_OP120_ManualStation" + m_strErrorMsg_Front + ": ";
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

		public void Set_CylError( Cylinder mCyl )
		{
			Set_Error(mCyl.Get_ErrorID(), mCyl.Get_ErrorMessage(), mCyl.Get_Label());
		}





		/************************************************************************************************************************
		 * Others / Supporting functions
		 ************************************************************************************************************************/
		//User Input/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// User input into the system via buttons. Pass, Reject or Rework. -1=>fail, 1=>pass, 2=>rework, 0=>no input
		/// </summary>
		/// <returns></returns>
        //public int Check_UserInput()
        //{
        //    int iRet = 1;

        //    //dry run
        //    if ( m_DataUnit.Flag_DryRun == true ) { Delay_ms(2000); return 1; }

        //    //user select go, fail or rework button
        //    //if ( m_inpButton_Green.Read() == 1 )			{ m_DataUnit.Result_ManualStat_OP120_str = "P"; iRet = 1;	}
        //    if ( m_inpButton_Red.Read() == 1 )		{ m_DataUnit.Result_ManualStat_OP120_str = "F"; iRet = -1;	}
        //    else if ( m_inpButton_Yellow.Read() == 1 )	{ m_DataUnit.Result_ManualStat_OP120_str = "P"; iRet = 1;	}
        //    else
        //        iRet = 0;

        //    return iRet;
        //}

		//Read from RFID///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Read_RFIDScanner()
		{
			int iChannel = 4;
			String strRFID = "";

			//dry run
			if ( m_DataUnit.Flag_DryRun == true )
			{
				m_DataUnit.RFID_SN_OP120_str = "DryRun_Dummy";
				return true;
			}

			//scan rfid
			if ( m_RFIDScanner.Send_Command(iChannel, ref strRFID) == false )
			{
				Set_Error(m_RFIDScanner.Get_ErrorID(), m_RFIDScanner.Get_ErrorMessage(), m_RFIDScanner.m_strLabel);
				return false;
			}

			//Set to rfid var
			m_DataUnit.RFID_SN_OP120_str = strRFID;

			return true;
		}

		//Server: Write Result to Server/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Server_SendResult()
		{
			String strData = "";

			//dry run
			if ( m_DataUnit.Flag_DryRun == true ) return true;

            //Set command string
            if (m_DataUnit.Result_ManualStat_OP120_str.ToLower() == "p") return true;
            if (m_DataUnit.Result_ManualStat_OP120_str.ToLower() == "f") strData += "result," + m_DataUnit.Result_ManualStat_OP120_str;
            //if (m_DataUnit.Result_ManualStat_OP120_str.ToLower() == "r") strData += "rework";
                
            ////compile string: strData
            //strData += "result," + m_DataUnit.Result_ManualStat_OP120_str;

			//Send Result to Server
			if ( !m_Server.Cmd_UpdateResults(m_DataUnit.RFID_SN_OP120_str, strData) )
				{ Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel); return false; }

			return true;
		}
		
        //gripper//////////////////////////////////////////////////////////////////
        /// <summary>
        /// Grip, ungrip, default state of the gripper. 0=> ungrip all, 1=>initialize, 2=>move to nest, 3=> goto stadby position,4=> put part on pallet
        /// </summary>
        /// <param name="iState"></param>
        /// <returns></returns>
        public bool Gripper_Grip(int iState)
        {
            switch (iState)
            {
                case 0: //ungrip all grip (initialise state)
                    {
                        m_cylSlideTable_GripUngrip.Cyl_Trigger(true);
                        if (m_cylSlideTable_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylSlideTable_GripUngrip); return false; }

                        //gripper ungrip
                        m_cylGripper_GripUngrip.Cyl_Trigger(true);
                        //if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }

                        //gripper initialize
                        if (m_cylNest_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylNest_UpDn); return false; }
                        //if (m_cylGripper_GripUngrip.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylGripper_GripUngrip); return false; }
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                        if (m_cylGripper_InOut.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylGripper_InOut); return false; }
                        
                        // put part back to pallet
                        if (m_senGripper_PartPresent.Read() == 1)
                            if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }

                        // ungrip gripper
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }

                        //nest down
                        if (m_cylNest_UpDn.GetExtSensor().Read() == 1)
                            if (m_cylNest_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }

                        // gripper up
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                    }
                    break;


                case 1: // module grip bottom enclosure
                    {
                        // gripper ungrip
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }
                        Delay_ms(20);

                        //gripper down 
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                        Delay_ms(20);

                        //gripper grip
                        m_cylGripper_GripUngrip.Cyl_Trigger(true);
                        m_cylSlideTable_GripUngrip.Cyl_Trigger(true);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }
                        if (m_cylSlideTable_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylSlideTable_GripUngrip); return false; }
                    }
                    break;

                case 2: // goto nest
                    {
                        //gripper up
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) //{ Set_CylError(m_cylPNPGripper_UpDn); return false; }
                            Delay_ms(20);
                        //gripper in
                        if (m_cylNest_UpDn.GetExtSensor().Read() == 1) { Set_Error((int)ErrorTable.en_ErrorCode.CYCL_INPOS_ERR, "", m_cylNest_UpDn.Get_Label()); return false; }
                        if (m_cylGripper_InOut.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylGripper_InOut); return false; }
                        //gripper down 
                        m_cylPNPGripper_UpDn.Cyl_Trigger(false);
                        Delay_ms(1000);
                        if (m_senGripper_InPos.Read() == 0) { Set_Error((int)ErrorTable.en_ErrorCode.CYCL_INPOS_ERR, "", m_senGripper_InPos.Get_Label()); return false; }
                        //gripper ungrip
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }
                    }
                    break;

                case 3: //goto standby position
                    {
                        //gripper ungrip
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        m_cylSlideTable_GripUngrip.Cyl_Trigger(true);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }
                        if (m_cylSlideTable_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylSlideTable_GripUngrip); return false; }

                        //gripper up
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                        Delay_ms(20);

                        //gripper out
                        if (m_cylNest_UpDn.GetExtSensor().Read() == 1) { Set_Error((int)ErrorTable.en_ErrorCode.CYCL_INPOS_ERR, "", m_cylNest_UpDn.Get_Label()); return false; }
                        if (m_cylGripper_InOut.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylGripper_InOut); return false; }
                        Delay_ms(20);
                    }
                    break;

                case 4: // put part back to pallet
                    {
                        //gripper grip
                        m_cylGripper_GripUngrip.Cyl_Trigger(true);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }

                        //gripper up
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                        Delay_ms(20);
                        //gripper out
                        if (m_cylNest_UpDn.GetExtSensor().Read() == 1) { Set_Error((int)ErrorTable.en_ErrorCode.CYCL_INPOS_ERR, "", m_cylNest_UpDn.Get_Label()); return false; }
                        if (m_cylGripper_InOut.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylGripper_InOut); return false; }
                        Delay_ms(20);
                        //gripper down
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }
                        Delay_ms(20);
                        //gripper ungrip
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }
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
            m_iFlag_SequencePos_SafetyCurtain = 0;

            m_iFlagSeqLog_Skip_ManualStation = 0;
            m_iFlagSeqLog_Skip_SafetyCurtain = 0;
            m_iFlagSeqLog_Skip = 0;

            //reset flag
            m_DataUnit.Flag_StationOP120_Done_b = false;
            m_DataUnit.Flag_SafetyCurtain_OP120 = false;

            //reset IO
            m_outSafetyCurtain_Indicator.Write(0);
            m_outOP120Vacuum_On.Write(0);

            //reset cylinder
            if (Gripper_Grip(0) == false) { goto End; };
            
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
            if (Module_Runing == false)
            {
                //auto run
                m_thRun = new Thread(Run);
                m_thRun.Start();
            }

           if (Module_Runing_SafetyCurtain == false)
           {
               m_thRun_SafetyCurtain = new Thread(Run_SafetyCurtain);
               m_thRun_SafetyCurtain.Start();
           }
            return true;
        }

        //Stop Run
		public void Stop_Run()
		{
			Module_StopRun = true;
            Module_StopRun_SafetyCurtain = true;
		}
        
		//is running
        public bool Is_Running()
        {
            if (Module_Runing == false || Module_Runing_SafetyCurtain == false)
            {
                return false;
            }

            return true;
        }

        public bool Is_Stop()
        {
            if (Module_Runing == true || Module_Runing_SafetyCurtain == true)
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

        public void Run_SafetyCurtain()
        {
            //Set variables
            Module_StopRun_SafetyCurtain = false;
            Flag_Error = false;
            Module_Runing_SafetyCurtain = true;

            //Auto sequence loop
            while (true)
            {
                //check monitor input 
                if (m_pMonitorInput.Flag_ErrorMonitor_b == true) break;

                //Auto sequence
                if (this.Auto_Sequence_SafetyCurtain() == false) { Flag_Error = true; break; }

                //stop module run
                if (Module_StopRun_SafetyCurtain == true)
                {
                    SetLog("Sequence Stopped");
                    Module_StopRun_SafetyCurtain = false;
                    break;
                }
            }

            //end
            Module_Runing_SafetyCurtain = false;
        }

        //Auto Sequence Safety Curtain////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence_SafetyCurtain()
        {
            switch (m_iFlag_SequencePos_SafetyCurtain)
            {
                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enStandby;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStandby: 
                    { 
                        //reset IO
                        m_outSafetyCurtain_Indicator.Write(0); 
                    }
                    m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enCheck_SafetyCurtain;
                    break;

                /*********************************************************************************************************
                * Sequence written here
                *********************************************************************************************************/
                case (int)m_Sequence.enCheck_SafetyCurtain:
                    {
                        //process check safety curtain

                        if (m_senSafetyCurtain.Read() == 0)
                        {
                            m_outSafetyCurtain_Indicator.Write(1);
                            m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enStop_Process;
                        }      
                        else
                        { Delay_ms(100); break; }

                    }
                    //m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enPreEnd;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStop_Process:
                    {
                        if (m_DataUnit.Flag_SafetyCurtain_OP120 == true)
                        {
                            m_outSafetyCurtain_Indicator.Write(1);
                            { 
                                Set_Error((int)ErrorTable.en_ErrorCode.SAFETYCURTAIN_ON, "", "");

                                //main sequence stop run due to safety curtain on (safety curtain job indicator not function) 
                                Module_StopRun = true; 
                                return false; 
                            }
                        }

                        else
                        {
                            m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enPreEnd;
                        }
                    }
                    m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enPreEnd;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enEnd;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos_SafetyCurtain = (int)m_Sequence.enStart;
                    break;
            }

            //set log
            //SetLog_Sequence(m_iFlag_SequencePos_SafetyCurtain, "SafetyCurtain");

            return true;
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

                //reset everything to default state here
                case (int)m_Sequence.enStandby:
                    {
                        //reset flags
                        m_DataUnit.Flag_StationOP120_Done_b = false;
                        m_DataUnit.Flag_SafetyCurtain_OP120 = false;

                        //reset IO
                        if (Gripper_Grip(3) == false) { return false; } //goto standby state
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag;
                    break;

                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/

                case (int)m_Sequence.enWait_TriggerFlag:
                    {
                        //wait trigger by conveyor
                        if (m_DataUnit.Flag_StationOP120_Trigger_b == false) { Delay_ms(10); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_RemoveTopCover;
                    break;

                case (int)m_Sequence.enWait_RemoveTopCover:
                    {
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            //check slide table part present
                            if (m_senSlideTable_PartPresent.Read() == 0)
                            {
                                Delay_ms(10);
                                break;
                            }

                            //check spring comb sensor
                            if (m_senSpringComb.Read() == 1)
                            {
                                Delay_ms(10);
                                break;
                            }

                            //wait user input
                            if (m_inpButton_Green.Read() == 0)
                            {
                                Delay_ms(10);
                                break;
                            }
                        }
                        else
                        {
                            Delay_ms(500);
                        }

                        //trigger on safety curtain
                        m_DataUnit.Flag_SafetyCurtain_OP120 = true;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_GripperDown;
                    break;

                case (int)m_Sequence.enWait_GripperDown:
                    {
                        #region old code
                        //		//wait user input selection
                        //		int iRet = Check_UserInput();
                        //		if ( iRet == 0 ) { Delay_ms(100); break; }
                        //		if ( iRet == 2 ) { m_iFlag_SequencePos = ( int ) m_Sequence.enRework_WaitPalletRemove; break; }
                        //
                        //		//send result to server
                        //		if ( !Server_SendResult() ) return false;
                        //		//pallet pass this station
                        //		if ( !Server_SendStationRunComplete() ) return false;
                        #endregion

                        //safety curtain check
                        if (m_senSafetyCurtain.Read() == 0)
                        {
                            Delay_ms(20); break;
                        }

                        // module grip bottom enclosure
                        if (Gripper_Grip(1) == false) { return false; }

                        //m_DataUnit.Flag_SafetyCurtain_OP120 = false;

                        //dry run then skip
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            //make sure part is grip
                            if (m_senGripper_PartPresent.Read() == 0) { Set_Error((int)ErrorTable.en_ErrorCode.SENSOR_PARTABSENT, "", ""); return false; }
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TopEnclosure;
                    break;

                case (int)m_Sequence.enWait_TopEnclosure:
                    {
                        //trigger on safety curtain
                        m_DataUnit.Flag_SafetyCurtain_OP120 = true;

                        // goto nest
                        if (Gripper_Grip(2) == false) { return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_VacuumOn;
                    break;

                case (int)m_Sequence.enWait_VacuumOn:
                    {
                        //safety curtain check
                        if (m_senSafetyCurtain.Read() == 0) { Delay_ms(20); break; }

                        lg_Timeout_Run = m_DataPackageFile.m_DataCommon.m_lg_OP120Vacuum_Timeout_Run;

                        //on vacuum
                        m_outOP120Vacuum_On.Write(1);

                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            mSt.Restart();

                            //check bottom enclosure is flat
                            while (true)
                            {
                                //error if reach time out
                                if (mSt.ElapsedMilliseconds > lg_Timeout_Run)
                                {
                                    mSt.Stop();

                                    //time out error
                                    Set_Error((int)ErrorTable.en_ErrorCode.OP120VACUUM_RUN_TIMEOUT, ""); 
                                    return false;
                                }

                                if (m_inpOP120Vacuum_On.Read() == 1) { mSt.Stop(); break; }

                                if (Module_StopRun == true) { break; }
                            }
                            mSt.Stop();
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_NestUp;
                    break;

                case (int)m_Sequence.enWait_NestUp:
                    {
                        //safety curtain
                        if (m_senSafetyCurtain.Read() == 0) { Delay_ms(20); break; }

                        //ungrip gripper
                        m_cylGripper_GripUngrip.Cyl_Trigger(false);
                        if (m_cylGripper_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylGripper_GripUngrip); return false; }

                        //nest cylinder move up
                        if (m_cylNest_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylNest_UpDn); return false; }
                        Delay_ms(20);

                        //check pressure switch
                        if (m_inpPressureSwitch_Upper.Read() == 1)
                        { 
                            Set_Error((int)ErrorTable.en_ErrorCode.PRESSURESWITCH_UPPERLIMIT, ""); 
                            return false; 
                        }

                        //slide table move out
                        m_cylSlideTable_GripUngrip.Cyl_Trigger(false);
                        if (m_cylSlideTable_GripUngrip.TaskComplete() != 1) { Set_CylError(m_cylSlideTable_GripUngrip); return false; }
                        Delay_ms(20);

                        //nest cylinder move down
                        if (m_cylNest_UpDn.Cyl_TriggerNWaitDone(false) == false) { Set_CylError(m_cylNest_UpDn); return false; }
                        Delay_ms(20);

                        //off vacuum
                        m_outOP120Vacuum_On.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartReach;
                    break;

                case (int)m_Sequence.enWait_PartReach:
                    {
                        //safety curtain
                        if (m_senSafetyCurtain.Read() == 0) { Delay_ms(20); break; }

                        // put part back to pallet
                        if (Gripper_Grip(4) == false) return false;

                        //gripper up
                        if (m_cylPNPGripper_UpDn.Cyl_TriggerNWaitDone(true) == false) { Set_CylError(m_cylPNPGripper_UpDn); return false; }

                        //trigger off safety curtain
                        m_DataUnit.Flag_SafetyCurtain_OP120 = false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag_Down;
                    break;

                case (int)m_Sequence.enWait_TriggerFlag_Down:
                    {
                        m_DataUnit.Flag_StationOP120_Done_b = true;

                        if (m_DataUnit.Flag_StationOP120_Trigger_b == true) { Delay_ms(10); break; }

                        m_DataUnit.Flag_StationOP120_Done_b = false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                #region rework old code
                //Rework------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                //case ( int ) m_Sequence.enRework_WaitPalletRemove:
                //	{
                //		//wait for the trigger to down
                //		if ( m_DataUnit.Flag_StationOP120_Trigger_b == true ) { Delay_ms(100); break; }
                //	}
                //	m_iFlag_SequencePos = ( int ) m_Sequence.enWait_TriggerFlag_Down;
                //	break;
                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                #endregion

                //-------------------------------------------------------------------------------------------------------------------------------
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

            //set log
            SetLog_Sequence(m_iFlag_SequencePos, "ManualStation");

            return true;
        }

        //Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum m_Sequence
        {
            enStart,
            enStandby,

            //check safety curtain
            enCheck_SafetyCurtain,
            enStop_Process,

            //sequence numbering is written here
            enWait_TriggerFlag,
            enWait_RemoveTopCover,
            enWait_GripperDown,
            enWait_TopEnclosure,
            enWait_VacuumOn,
            enWait_NestUp,
            enWait_PartReach,
            enWait_UserInput,
            enWait_TriggerFlag_Down,
            //////////////////////////////////////

            enPreEnd,
            enEnd
        }
    }
}