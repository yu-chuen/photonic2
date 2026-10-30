using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;


/******************************************************************************************************
 * Class Description: Module worker for both shutter (in and out) for the laser welder
 ******************************************************************************************************/
namespace StaticCoreLib
{
    public class Module_Shutter_A
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl = null;
        MonitorInput m_pMonitorInput = null;
        MachineInfo m_MachineInfo = null;
        Data_UnitTempStorage m_DataUnit = null;
        Data_PackageFile m_pDataPackageFile = null;
        Log_Data m_LogFile = new Log_Data();

        public Indexer_APSlib_PanasonicECAT m_IndexerShutter = null;         //shutter before laser welder (LW)


        //IO
        public InputBit m_inpLW_ReadyToReceive_OP130A       = null;         //Laser Welder Ready to receive pallet
        public InputBit m_inpLW_ReadyToReceive_OP130B       = null;
        public InputBit m_inpLW_ReadyToReceive_OP130C       = null;

        public InputBit m_inpReadyToSend_OP120A             = null;         //PC 2/3 signal that pallet is ready to be transfered
        public InputBit m_inpReadyToSend_OP120B             = null;

        public InputBit m_inpPalletPresent_ShA              = null;
        public InputBit m_inpPalletInPassThru_ShA                   = null;
        public InputBit m_inpPalletOutPassThru_ShA                  = null;
                
        public OutputBit m_outPartReady_OP130A              = null;         //Signal: Part is sending to the related LW
        public OutputBit m_outPartReady_OP130B              = null;
        public OutputBit m_outPartReady_OP130C              = null;
        
        public OutputBit m_outReadyToReceivePart_OP120A     = null;         //PC 2/3: signal pc2/3 to send the pallet to the shutter
        public OutputBit m_outReadyToReceivePart_OP120B     = null;

        public OutputBit m_outStopper                       = null;
        public OutputBit m_outConvFwd_ShA                   = null;
        public OutputBit m_outConvRev_ShA                   = null;
        
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
            set { lock (m_lockRun) { m_bFlag_Run = value; } }
            get { lock (m_lockRun) { return m_bFlag_Run; } }
        }                                                                //InDicate if the module is running
        public bool Module_StopRun
        {
            set { lock (m_lockRun) { m_bFlag_RunEnd = value; } }
            get { lock (m_lockRun) { return m_bFlag_RunEnd; } }
        }                                                             //Stop running thread
        public bool Process_Running
        {
            set { lock (m_lockRun) { m_bFlag_ProcessRun = value; } }
            get { lock (m_lockRun) { return m_bFlag_ProcessRun; } }
        }                                                            //indicate: Process is running
        public bool Flag_Init                                                                               //Init
        {
            set { lock (m_lockInit) { m_bFlag_Init = value; } }
            get { lock (m_lockInit) { return m_bFlag_Init; } }
        }
        public bool Flag_InitOK
        {
            set { lock (m_lockInit_OK) { m_bFlag_InitOK = value; } }
            get { lock (m_lockInit_OK) { return m_bFlag_InitOK; } }
        }                                                                      //Indicate: Init passed
        public bool Flag_Error
        {
            set { lock (m_lock_Flag_Error) { m_bFlag_Error = value; } }
            get { lock (m_lock_Flag_Error) { return m_bFlag_Error; } }
        }


        #endregion


        public int m_iFlag_SequencePos = 0;
        int m_iFlag_LogSeqSkip = 0;

        //Position Tracker
        private readonly object m_lockPositionTracker = new object();
        public String m_strPosTracker = "none";
        public String Position_Tracker { set { lock (m_lockPositionTracker) { m_strPosTracker = value; } } get { lock (m_lockPositionTracker) { return m_strPosTracker; } } }

        int m_iErrorID = 0;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsg_Front = "";

        Thread m_thRun = null;



        //constructor////////////////////////////////////////////////////////////////////////////////////
        public Module_Shutter_A(SeqMachineControl mSeqMachineCtrl )
		{
			m_pSeqMachineCtrl		= mSeqMachineCtrl;
			m_MachineInfo			= m_pSeqMachineCtrl.m_MachineInfo;
			m_DataUnit				= m_pSeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			= m_pSeqMachineCtrl.m_MonitorInput;
            m_pDataPackageFile      = m_pSeqMachineCtrl.m_DataPackageFile;

            m_IndexerShutter        = m_pSeqMachineCtrl.m_IndexerShutter_A;

            //Set Log file
            //Create Log File class. use  default extension (.txt)
            m_LogFile.SetFolderName("Machine_Sequence_Folder\\ModuleShutterA");
            m_LogFile.SetFileName("LogFile");
            m_LogFile.SeqLog_StartThread();
        }

        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Create_InputsOutputs(InterfaceCards mInterface)
        {
            int iBitID = 0;

            iBitID = 4; m_inpLW_ReadyToReceive_OP130A       = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready_OP130A");
            iBitID = 2; m_inpLW_ReadyToReceive_OP130B       = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready_OP130B");
            iBitID = 0; m_inpLW_ReadyToReceive_OP130C       = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready_OP130C");            
            iBitID = 8; m_inpReadyToSend_OP120A             = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Send_OP120A");
            iBitID = 9; m_inpReadyToSend_OP120B             = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Send_OP120B");            
            iBitID = 0; m_inpPalletPresent_ShA              = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet Present");
            iBitID = 1; m_inpPalletInPassThru_ShA           = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet In Pass Thru");
            iBitID = 2; m_inpPalletOutPassThru_ShA          = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet Out Pass Thru");
            
            iBitID = 4; m_outPartReady_OP130A               = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Part Ready_OP130A");
            iBitID = 2; m_outPartReady_OP130B               = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Part Ready_OP130B");
            iBitID = 0; m_outPartReady_OP130C               = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Part Ready_OP130C");           
            iBitID = 8; m_outReadyToReceivePart_OP120A      = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Receive Part_OP120A");
            iBitID = 9; m_outReadyToReceivePart_OP120B      = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Receive Part_OP120B");
            iBitID = 0; m_outStopper                        = new OutputBit(mInterface.Get_OutputPort(22), iBitID, "Stopper");
            iBitID = 12; m_outConvFwd_ShA                   = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Conv Fwd");
            iBitID = 13; m_outConvRev_ShA                   = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Conv Rev");

            return true;
        }

        //Clean UP/////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            m_LogFile.Clean_Up();

            m_IndexerShutter.Servo_On(false);
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

            m_pSeqMachineCtrl.SetLog("=====> Module_ShutterA: " + strLog);
        }

        public void Set_SequenceLog(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlag_LogSeqSkip == iSequencePos)
                return;
            else
                m_iFlag_LogSeqSkip = iSequencePos;

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
            String strError = "Module_ShutterA: " + m_strErrorMsgAdd;
            m_strErrorMsgAdd = "";
            return strError;
        }
        public String Get_ErrorMsg_Front()
        {
            String strError = "Module_ShutterA" + m_strErrorMsg_Front + ": ";
            m_strErrorMsg_Front = "";
            return strError;
        }

        //Set Error Message///////////////////////////////////////////////////////////////////////////////
        public void Set_Error(int iErrorID, String strErrorMsgAdd, String strErrorMsgFront = "")
        {
            this.m_iErrorID = iErrorID;
            this.m_strErrorMsgAdd = strErrorMsgAdd;
            this.m_strErrorMsg_Front = strErrorMsgFront;

            //Logger
            String strLog = "";
            strLog = "ErrorID=" + iErrorID + " : Device: " + strErrorMsgFront + " : ErrorMsg=" + strErrorMsgAdd + Enum.GetName(typeof(ErrorTable.en_ErrorCode), m_iErrorID);
            SetLog(strLog, 999);
        }
        public void Set_CylError(Cylinder mCyl) { Set_Error(mCyl.Get_ErrorID(), mCyl.Get_ErrorMessage(), mCyl.Get_Label()); }




        /************************************************************************************************************************
         * Robot Movement
         ************************************************************************************************************************/
        //Indexer: Goto Position///////////////////////////////////////////////////////////////////////////////
        public bool Goto_IndexerPos(String strPos)
        {
            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();
            bool bRet = false;

            //get position from package file
            mPosition = Get_IndexerPosition(strPos, ref bRet);
            if (bRet == false) return false;

            //interlock
            if (Interlock(strPos, mPosition) == false) return false;

            //move to position
            if (m_IndexerShutter.Move_Abs(mPosition) == false)
            {
                Set_Error(m_IndexerShutter.Get_ErrorID(), m_IndexerShutter.Get_ErrorMessage(), m_IndexerShutter.m_strLabel);
                return false;
            }

            //end
            return true;
        }

        //Get Indexer Position//////////////////////////////////////////////////////////////////////////////////
        public Indexer_Pos_Settings Get_IndexerPosition(String strPos, ref bool bRet)
        {
            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();
            bRet = true;

            //normal: get position
            mPosition = m_pDataPackageFile.m_DataIndexer_ShA.Get_Settings(strPos);

            return mPosition;
        }

        //Movement Interlock///////////////////////////////////////////////////////////////////////////////////
        public bool Interlock(String strPos, Indexer_Pos_Settings mPosition)
        {
            bool bRet = true;



            return bRet;
        }



        /************************************************************************************************************************
         * Supporting Functions
         ************************************************************************************************************************/




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
            //reset flag
            Flag_Init = true;
            Flag_InitOK = false;
            m_iFlag_SequencePos = 0; 
            m_iFlag_LogSeqSkip = 0;

            //reset IO
            m_outPartReady_OP130A.Write(0);
            m_outPartReady_OP130B.Write(0);
            m_outPartReady_OP130C.Write(0);

            m_outReadyToReceivePart_OP120A.Write(0);
            m_outReadyToReceivePart_OP120B.Write(0);

            m_outConvRev_ShA.Write(0);
            m_outConvFwd_ShA.Write(0);
            m_outStopper.Write(0);

            
            //temp solution to connect indexer
            //if (m_IndexerShutter.Svon_i == 0 && m_IndexerShutter.core_ServoOn(true) == false) goto End;

            //init indexer
            if (m_IndexerShutter.Homing() == false)
            {
                Set_Error(m_IndexerShutter.Get_ErrorID(), m_IndexerShutter.Get_ErrorMessage(), m_IndexerShutter.m_strLabel);
                SetLog("Shutter A Homing Return Error...");
                goto End;
            }
            Position_Tracker = Pos_Home;

            //end
            Flag_InitOK = true;
            Flag_Error = false;
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
            if (Module_Running == false)
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
            Module_Running = true;

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
            m_outConvFwd_ShA.Write(0);
            m_outConvRev_ShA.Write(0);
            Module_Running = false;
        }

        //Auto Sequence//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence()
        {
            switch (m_iFlag_SequencePos)
            {
                //-------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStart: { }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //-------------------------------------------------------------------------------------------------------------------------------
                //Place to set the variables to default state. 
                case (int)m_Sequence.enStandby:
                    {
                        //reset flag
                        m_DataUnit.Flag_PartSendLW_ShutterA_i = 0;

                        //reset IO
                        m_outPartReady_OP130A.Write(0);
                        m_outPartReady_OP130B.Write(0);
                        m_outPartReady_OP130C.Write(0);

                        m_outReadyToReceivePart_OP120A.Write(0);
                        m_outReadyToReceivePart_OP120B.Write(0);

                        m_outConvRev_ShA.Write(0);
                        m_outConvFwd_ShA.Write(0);
                        m_outStopper.Write(0);

                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_SolStatSignal;
                    break;



                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/                 
                case (int)m_Sequence.enWait_SolStatSignal:
                    {
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            m_outConvFwd_ShA.Write(1);
                            m_iFlag_SequencePos = (int)m_Sequence.enShutterMove;
                            break;
                        }

                        //wait for signal
                        else if (m_inpReadyToSend_OP120B.Read() == 1 || m_inpReadyToSend_OP120A.Read() == 1 )
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enMoveShutterToIncomingZone;
                            break;
                        }
                        else
                        {
                            Delay_ms(10);
                        }
                    }
                    //m_iFlag_SequencePos = (int)m_Sequence.enWait_NextStationReady;    //loop here wait for signal
                    break;

                case (int)m_Sequence.enShutterMove:
                    { 
                         if (Goto_IndexerPos(Pos_Standby) == false) return false;
                         if (Goto_IndexerPos(Pos_ZoneA) == false) return false;
                         if (Goto_IndexerPos(Pos_LWA) == false) return false;
                         if (Goto_IndexerPos(Pos_LWB) == false) return false;
                         if (Goto_IndexerPos(Pos_LWC) == false) return false;
                         if (Goto_IndexerPos(Pos_ZoneB) == false) return false;
                         if (Goto_IndexerPos(Pos_LWA) == false) return false;
                         if (Goto_IndexerPos(Pos_LWB) == false) return false;
                         if (Goto_IndexerPos(Pos_LWC) == false) return false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;
             
                case (int)m_Sequence.enMoveShutterToIncomingZone:
                    {
                        //Check Zone B
                        if (m_inpReadyToSend_OP120B.Read() == 1)
                        {
                            //goto Zone B
                            if (Goto_IndexerPos(Pos_ZoneB) == false) return false;

                            //Trigger Part to come
                            m_outReadyToReceivePart_OP120B.Write(1);
                        }

                        //Check Zone A
                        else if (m_inpReadyToSend_OP120A.Read() == 1)
                        {
                            //goto Zone A
                            if (Goto_IndexerPos(Pos_ZoneA) == false) return false;

                            //Trigger Part to come
                            m_outReadyToReceivePart_OP120A.Write(1);
                        }

                        //turn on conv
                        m_outConvFwd_ShA.Write(1);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartPresent;
                    break;


                case (int)m_Sequence.enWait_PartPresent:
                    {
                        //Check part has enter shutter
                        if (m_inpPalletPresent_ShA.Read() == 0) { Delay_ms(10); break; }
                        if (m_inpPalletInPassThru_ShA.Read() == 1) { Delay_ms(10); break; }

                        //down both signal
                        m_outReadyToReceivePart_OP120A.Write(0);
                        m_outReadyToReceivePart_OP120B.Write(0);

                        //turn off conv
                        m_outConvFwd_ShA.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_NextStationReady;
                    break;

                case (int)m_Sequence.enWait_NextStationReady:
                    {
                        //Check LW Station
                        if (m_inpLW_ReadyToReceive_OP130A.Read() == 1)
                        {
                            //goto position
                            if (Goto_IndexerPos(Pos_LWA) == false) return false;

                            //set pallet ready to send
                            m_outPartReady_OP130A.Write(1);

                            //set flag to indicate position
                            m_DataUnit.Flag_PartSendLW_ShutterA_i = 1;
                        }
                        else if (m_inpLW_ReadyToReceive_OP130B.Read() == 1)
                        {
                            //goto position
                            if (Goto_IndexerPos(Pos_LWB) == false) return false;

                            //set pallet ready to send
                            m_outPartReady_OP130B.Write(1);

                            //set flag to indicate position
                            m_DataUnit.Flag_PartSendLW_ShutterA_i = 2;
                        }
                        else if (m_inpLW_ReadyToReceive_OP130C.Read() == 1)
                        {
                            //goto position
                            if (Goto_IndexerPos(Pos_LWC) == false) return false;

                            //set pallet ready to send
                            m_outPartReady_OP130C.Write(1);

                            //set flag to indicate position
                            m_DataUnit.Flag_PartSendLW_ShutterA_i = 3;
                        }
                        else
                        {
                            Delay_ms(10);
                            break;
                        }

                        //down stopper
                        m_outStopper.Write(1);

                        //turn on conv
                        m_outConvFwd_ShA.Write(1);

                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartExit;
                    break;
                
                case (int)m_Sequence.enWait_PartExit:
                    {
                        //wait for pallet to exit
                        if (m_inpPalletPresent_ShA.Read() == 1) { Delay_ms(10); break; }
                        if (m_inpPalletOutPassThru_ShA.Read() == 1) { Delay_ms(10); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartReach;
                    break;

                case (int)m_Sequence.enWait_PartReach:
                    {
                        switch (m_DataUnit.Flag_PartSendLW_ShutterA_i)
                        {
                            case 1: if (m_inpLW_ReadyToReceive_OP130A.Read() == 1) { Delay_ms(10); } break;
                            case 2: if (m_inpLW_ReadyToReceive_OP130B.Read() == 1) { Delay_ms(10); } break;
                            case 3: if (m_inpLW_ReadyToReceive_OP130C.Read() == 1) { Delay_ms(10); } break;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;





                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
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

            //Sequence log
            Set_SequenceLog(m_iFlag_SequencePos);

            return true;
        }

        //Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum m_Sequence
        {
            enStart,
            enStandby,

            //sequence numbering is written here
            enWait_SolStatSignal,
            enShutterMove,
            enMoveShutterToIncomingZone,
            enWait_PartPresent,
            enWait_NextStationReady,
            enWait_PartExit,
            enWait_PartReach,

            enPreEnd,
            enEnd
        }


        //Position Name///////////////////////////////////////////////////////////////////////////////////
        public String Pos_Home      { get { return "Home"; } }
        public String Pos_Standby   { get { return "P0"; } }
        public String Pos_ZoneA     { get { return "P1"; } }
        public String Pos_ZoneB     { get { return "P2"; } }
        public String Pos_LWA       { get { return "PA"; } }
        public String Pos_LWB       { get { return "PB"; } }
        public String Pos_LWC       { get { return "PC"; } }
        



    }
}
