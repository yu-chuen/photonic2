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
    public class Module_Shutter_B
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl = null;
        MonitorInput m_pMonitorInput = null;
        MachineInfo m_MachineInfo = null;
        Data_UnitTempStorage m_DataUnit = null;
        Data_PackageFile m_pDataPackageFile = null;
        Log_Data m_LogFile = new Log_Data();

        public Indexer_APSlib_PanasonicECAT m_IndexerShutter = null;        //shutter after laser welder (LW)


        //IO
        public InputBit m_inpLW_ReadyToSend_OP130A          = null;         //Laser Welder pallet is done. Ready to transfer to next station
        public InputBit m_inpLW_ReadyToSend_OP130B          = null;
        public InputBit m_inpLW_ReadyToSend_OP130C          = null;

        public InputBit m_inpOP135_ReadyToReceive           = null;         //OP135 Signal ready to receive pallet
        public InputBit m_inpPalletPresent_ShB              = null;
        public InputBit m_inpPalletInPassThru_ShB                   = null;
        public InputBit m_inpPalletOutPassThru_ShB                  = null;
    
        public OutputBit m_outShB_ReadyToReceive_OP130A     = null;         //Signal: Signal LW to send the pallet over
        public OutputBit m_outShB_ReadyToReceive_OP130B     = null;
        public OutputBit m_outShB_ReadyToReceive_OP130C     = null;
        
        public OutputBit m_outShB_ReadyToSend_OP135         = null;         //Signal: OP135 that pallet is ready to be transfered

        public OutputBit m_outStopper                       = null;
        public OutputBit m_outConvFwd_ShB                   = null;
        public OutputBit m_outConvRev_ShB                   = null;
        


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
        public int m_iFlagSeqLog_WaitSkip = 0;      //To block from logging multi times the same sequence (wait portion especially)

        //Position Tracker
        private readonly object m_lockPositionTracker = new object();
        public String m_strPosTracker = "none";
        public String Position_Tracker { set { lock (m_lockPositionTracker) { m_strPosTracker = value; } } get { lock (m_lockPositionTracker) { return m_strPosTracker; } } }

        int m_iErrorID = 0;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsg_Front = "";

        Thread m_thRun = null;



        //constructor////////////////////////////////////////////////////////////////////////////////////
        public Module_Shutter_B(SeqMachineControl mSeqMachineCtrl )
		{
			m_pSeqMachineCtrl		= mSeqMachineCtrl;
			m_MachineInfo			= m_pSeqMachineCtrl.m_MachineInfo;
			m_DataUnit				= m_pSeqMachineCtrl.m_DataUnit;
			m_pMonitorInput			= m_pSeqMachineCtrl.m_MonitorInput;
            m_pDataPackageFile      = m_pSeqMachineCtrl.m_DataPackageFile;

            m_IndexerShutter        = m_pSeqMachineCtrl.m_IndexerShutter_B;

            //Set Log file
            //Create Log File class. use  default extension (.txt)
            m_LogFile.SetFolderName("Machine_Sequence_Folder\\ModuleShutterB");
            m_LogFile.SetFileName("LogFile");
            m_LogFile.SeqLog_StartThread();
        }

        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Create_InputsOutputs(InterfaceCards mInterface)
        {
            int iBitID = 0;

            iBitID = 5; m_inpLW_ReadyToSend_OP130A          = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Send_OP130A");
            iBitID = 3; m_inpLW_ReadyToSend_OP130B          = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Send_OP130B");
            iBitID = 1; m_inpLW_ReadyToSend_OP130C          = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Send_OP130C");            
            iBitID = 6; m_inpOP135_ReadyToReceive           = new InputBit(mInterface.Get_InputPort(21), iBitID, "Ready To Receive_OP135");            
            iBitID = 3; m_inpPalletPresent_ShB              = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet Present");
            iBitID = 4; m_inpPalletInPassThru_ShB           = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet In Pass Thru");
            iBitID = 5; m_inpPalletOutPassThru_ShB          = new InputBit(mInterface.Get_InputPort(22), iBitID, "Pallet Out Pass Thru");

            iBitID = 5; m_outShB_ReadyToReceive_OP130A      = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Receive_OP130A");
            iBitID = 3; m_outShB_ReadyToReceive_OP130B      = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Receive_OP130B");
            iBitID = 1; m_outShB_ReadyToReceive_OP130C      = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Receive_OP130C");            
            iBitID = 6; m_outShB_ReadyToSend_OP135          = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Ready To Send_OP135");            
            iBitID = 14; m_outConvFwd_ShB                   = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Conv Fwd");
            iBitID = 15; m_outConvRev_ShB                   = new OutputBit(mInterface.Get_OutputPort(21), iBitID, "Conv Rev"); 
            iBitID = 1; m_outStopper                        = new OutputBit(mInterface.Get_OutputPort(22), iBitID, "Stopper");


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

            m_pSeqMachineCtrl.SetLog("=====> Module_ShutterB: " + strLog);
        }

        private void SetLog_Sequence(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlagSeqLog_WaitSkip == iSequencePos)
                return;
            else
                m_iFlagSeqLog_WaitSkip = iSequencePos;

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
            String strError = "Module_Shutter: " + m_strErrorMsgAdd;
            m_strErrorMsgAdd = "";
            return strError;
        }
        public String Get_ErrorMsg_Front()
        {
            String strError = "Module_Shutter" + m_strErrorMsg_Front + ": ";
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
            mPosition = m_pDataPackageFile.m_DataIndexer_ShB.Get_Settings(strPos);

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
            m_iFlagSeqLog_WaitSkip = 0;

            //Reset all Output
            m_outShB_ReadyToReceive_OP130A.Write(0);
            m_outShB_ReadyToReceive_OP130B.Write(0);
            m_outShB_ReadyToReceive_OP130C.Write(0);            
            m_outShB_ReadyToSend_OP135.Write(0);
            m_outStopper.Write(0);
            m_outConvFwd_ShB.Write(0);    
            m_outConvRev_ShB.Write(0);

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
            m_outConvFwd_ShB.Write(0);
            m_outConvRev_ShB.Write(0);
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
                        //reset IO
                        m_outShB_ReadyToReceive_OP130A.Write(0);
                        m_outShB_ReadyToReceive_OP130B.Write(0);
                        m_outShB_ReadyToReceive_OP130C.Write(0);
                        m_outShB_ReadyToSend_OP135.Write(0);
                        m_outStopper.Write(0);
                        m_outConvFwd_ShB.Write(0);
                        m_outConvRev_ShB.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_SolStatSignal;
                    break;



                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/                 
                case (int)m_Sequence.enWait_SolStatSignal:
                    {
                        //go to the related station
                        if (m_DataUnit.Flag_DryRun == true)
                        {
                            m_outConvFwd_ShB.Write(1);
                            m_iFlag_SequencePos = (int)m_Sequence.enShutterMove;
                            break;
                        }
                        else if (m_inpPalletPresent_ShB.Read() == 1)
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enWait_PalletArrive;
                            break;
                        }
                        else if (m_inpLW_ReadyToSend_OP130A.Read() == 1)
                        {
                            //goto LW A
                            if (Goto_IndexerPos(Pos_LWA) == false) return false;

                            //set pallet ready to receive signal
                            m_outShB_ReadyToReceive_OP130A.Write(1);
                        }
                        else if (m_inpLW_ReadyToSend_OP130B.Read() == 1)
                        {
                            //goto LW B
                            if (Goto_IndexerPos(Pos_LWB) == false) return false;

                            //set pallet ready to receive signal
                            m_outShB_ReadyToReceive_OP130B.Write(1);
                        }
                        else if (m_inpLW_ReadyToSend_OP130C.Read() == 1)
                        {
                            //goto LW C
                            if (Goto_IndexerPos(Pos_LWC) == false) return false;

                            //set pallet ready to receive signal
                            m_outShB_ReadyToReceive_OP130C.Write(1);
                        }
                        //no output from any station
                        else
                        {
                            Delay_ms(10);
                            break;
                        }

                        //trig IO. Pallet moving to next zone
                        m_outConvFwd_ShB.Write(1);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PalletArrive;
                    break;

                case (int)m_Sequence.enShutterMove:
                    {
                        if (Goto_IndexerPos(Pos_Standby) == false) return false;
                        if (Goto_IndexerPos(Pos_LWA) == false) return false;
                        if (Goto_IndexerPos(Pos_OP135) == false) return false;
                        if (Goto_IndexerPos(Pos_LWB) == false) return false;
                        if (Goto_IndexerPos(Pos_OP135) == false) return false;
                        if (Goto_IndexerPos(Pos_LWC) == false) return false;
                        if (Goto_IndexerPos(Pos_OP135) == false) return false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                case (int)m_Sequence.enWait_PalletArrive:
                    {
                        //wait pallet arrive into shutter
                        if (m_DataUnit.Flag_DryRun == true) { Delay_ms(1000); }
                        else
                        {
                            if (m_inpPalletPresent_ShB.Read() == 0) { Delay_ms(10); break; }
                            if (m_inpPalletInPassThru_ShB.Read() == 1) { Delay_ms(10); break; }
                            
                            m_outConvFwd_ShB.Write(0);
                        }

                        //reset ready to receive signal
                        m_outShB_ReadyToReceive_OP130A.Write(0);
                        m_outShB_ReadyToReceive_OP130B.Write(0);
                        m_outShB_ReadyToReceive_OP130C.Write(0);

                        //goto Op130 position
                        if (Goto_IndexerPos(Pos_OP135) == false) return false;

                        //signal next station pallet is ready to send
                        m_outShB_ReadyToSend_OP135.Write(1);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_NextStationReady;
                    break;

                case (int)m_Sequence.enWait_NextStationReady:
                    {
                        //wait next station to be ready
                        if (m_inpOP135_ReadyToReceive.Read() == 0) { Delay_ms(10); break; }

                        //stopper down
                        m_outStopper.Write(1);

                        //run conv
                        m_outConvFwd_ShB.Write(1);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartExit;
                    break;

                case (int)m_Sequence.enWait_PartExit:
                    {
                        //no pass through sensor
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PartReach;
                    break;

                case (int)m_Sequence.enWait_PartReach:
                    {
                        //wait signal down for part reached next station
                        if (m_inpOP135_ReadyToReceive.Read() == 1) { Delay_ms(10); break; }

                        //stopper up
                        m_outStopper.Write(0);

                        //run conv
                        m_outConvFwd_ShB.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;




                //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd: { }
                    m_iFlag_SequencePos = (int)m_Sequence.enEnd;
                    break;

                case (int)m_Sequence.enEnd: { }
                    m_iFlag_SequencePos = (int)m_Sequence.enStart;
                    break;
            }

            //logger
            SetLog_Sequence(m_iFlag_SequencePos);

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
            enWait_PalletArrive,
            enWait_NextStationReady,
            enWait_PartExit,
            enWait_PartReach,

            enPreEnd,
            enEnd
        }


        //Position Name///////////////////////////////////////////////////////////////////////////////////
        public String Pos_Home      { get { return "Home"; } }
        public String Pos_Standby   { get { return "P0"; } }
        public String Pos_OP135     { get { return "P1"; } }
        public String Pos_LWA       { get { return "PA"; } }
        public String Pos_LWB       { get { return "PB"; } }
        public String Pos_LWC       { get { return "PC"; } }
    }
}
