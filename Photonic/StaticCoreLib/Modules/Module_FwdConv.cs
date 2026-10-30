using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
    public class Module_FwdConv
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl = null;
        MonitorInput m_pMonitorInput = null;
        MachineInfo m_MachineInfo = null;
        Data_UnitTempStorage m_DataUnit = null;
        Server_COMTCP_GT_CentralPC m_Server = null;

        //IO
        public OutputBit m_outConv_MoveFWD = null;
        public OutputBit m_outConv_MoveREV = null;

        //Flags
        #region Flags
        private readonly object m_lockRun = new object();                                       //Lock for thread sequence
        private readonly object m_lockRunEnd = new object();                                //Lock for thread sequence End
        private readonly object m_lockInit = new object();                                          //Lock for Init Sequence
        private readonly object m_lockInit_OK = new object();                                   //Lock for Init Sequence
        private readonly object m_lock_Flag_Error = new object();                           //Lock for Error occur at auto sequence

        bool m_bFlag_Init = false;
        bool m_bFlag_InitOK = false;																	//indicate init is done and good
        bool m_bFlag_Run = false;																		//indicate thread is running
        bool m_bFlag_RunEnd = false;																//signal thread to stop
        bool m_bFlag_Error = false;																	//signal: Error occur

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

        bool m_bFlag_RunConv = false;
        public int m_iFlag_SequencePos = 0;
        public short m_iModule_StationID = 0;

        public int m_iErrorID = 0;
        public String m_strErrorMsgAdd = "";
        String m_strErrorMsg_Front = "";

        Thread m_thRun = null;

        Stopwatch m_st_ConvTimer = new Stopwatch();

        //constructor//////////////////////////////////////////////////////////////////////////////////
        public Module_FwdConv(SeqMachineControl mSeqMachineCtrl, short iModuleID)
        {
            m_SeqMachineCtrl = mSeqMachineCtrl;
            m_MachineInfo = m_SeqMachineCtrl.m_MachineInfo;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
            m_pMonitorInput = m_SeqMachineCtrl.m_MonitorInput;
            m_iModule_StationID = iModuleID;

            //create server object and set server settings
            m_Server = new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_Module_FwdConv_OP" + m_iModule_StationID, m_MachineInfo.Get_MachineFilePath());
            m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);
        }

        //Init or Create cylinders and IO ports///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Create_InputsOutputs(InterfaceCards mInterface)
        {
            int iBitID = 0;
            String strFilePath = m_MachineInfo.Get_MachineFilePath();

            switch (m_iModule_StationID)
            {
                case 85:
                    {
                        //output bit
                        iBitID = 13; m_outConv_MoveFWD = new OutputBit(mInterface.Get_OutputPort(4), iBitID, "Conveyor Forward OP85 > OP90");
                        iBitID = 14; m_outConv_MoveREV = new OutputBit(mInterface.Get_OutputPort(4), iBitID, "Conveyor Reverse OP85 > OP90");
                    }
                    break;

                case 100:
                    {
                        //output bit
                        iBitID = 5; m_outConv_MoveFWD = new OutputBit(mInterface.Get_OutputPort(9), iBitID, "Conveyor Forward OP100");
                        iBitID = 6; m_outConv_MoveREV = new OutputBit(mInterface.Get_OutputPort(9), iBitID, "Conveyor Reverse OP100");
                    }
                    break;

                case 110:
                    {
                        //output bit
                        iBitID = 10; m_outConv_MoveFWD = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "Conveyor Forward OP110");
                        iBitID = 11; m_outConv_MoveREV = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "Conveyor Reverse OP110");
                    }
                    break;
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

            m_SeqMachineCtrl.SetLog(">>>> Module_FwdConv: " + strLog);
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
            String strError = "Module_FwdConv: " + m_strErrorMsgAdd;
            m_strErrorMsgAdd = "";
            return strError;
        }

        public String Get_ErrorMsg_Front()
        {
            String strError = "Module_FwdConv: " + m_strErrorMsg_Front + ": ";
            m_strErrorMsg_Front = "";
            return strError;
        }

        //Set Error Message///////////////////////////////////////////////////////////////////////////////
        public void Set_Error(int iErrorID, String strErrorMsgAdd, String strErrorMsgFront = "")
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
            strLog = "ErrorID=" + iErrorID + " : Device: " + strErrorMsgFront + " : ErrorMsg=" + strErrorMsgAdd;
            SetLog(strLog, 999);
        }

        /************************************************************************************************************************
        * Others / Supporting functions
        ************************************************************************************************************************/

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
            if (m_Server.Cmd_InsertErrorLog("Module_FwdConv_OP" + m_iModule_StationID, strDateTime, int.Parse(m_MachineInfo.m_strMachineID), iErrorCode, Enum.GetName(typeof(ErrorTable.en_ErrorCode), iErrorCode), strErrorDesc) == false)
            {
                //Set_Error(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), m_Server.m_strLabel);

                //prompt error but dont stop machine
                m_SeqMachineCtrl.Display_ErrorMessageBox("Error. Unable to insert ErrorLog into server.\r\nErrorCode: " + m_Server.Get_ErrorID() + "\r\nErrorDesc: " + m_Server.m_strLabel + ", " + m_Server.Get_ErrorMessage()); //blocking call until message box is closed
                m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Run(); //reset the tower light back to green

                return;
            }
        }

        public void ResetConv()
        {
            if (m_bFlag_RunConv == false)
            {
                m_outConv_MoveFWD.Write(0);
                m_outConv_MoveREV.Write(0);
            }
            else
            {
                Delay_ms(5000);
                m_outConv_MoveFWD.Write(0);
                m_outConv_MoveREV.Write(0);
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
            //start
            Flag_Init = true;
            Flag_InitOK = false;

            //reset sequence
            m_iFlag_SequencePos = 0;

            //reset IO
            m_outConv_MoveFWD.Write(0);
            m_outConv_MoveREV.Write(0);

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
            //Start Run conv Thread
            if (Module_Running == false)
            {
                m_thRun = new Thread(Run);
                m_thRun.Start();
            }

            return true;
        }

        public void Stop_Run()
        {
            Module_StopRun = true;
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
                    ResetConv();
                    SetLog("Sequence Stopped");
                    Module_StopRun = false;
                    break;
                }
            }

            //end
            Module_Running = false;
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

                //Place to set the variables to default state. 
                case (int)m_Sequence.enStandby:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enRunConv;
                    break;

                //Sequence
                case (int)m_Sequence.enRunConv:
                    {
                        switch (m_iModule_StationID)
                        {
                            case 85:
                                {
                                    //check flag from other thread that triggers sending of pallet
                                    if (
                                        m_DataUnit.Flag_Conv_OP80_Sending_b == true ||
                                        m_DataUnit.Flag_Conv_OP85_Sending_b == true ||
                                        m_DataUnit.Flag_Conv_OP90_Sending_b == true
                                       )
                                    {
                                        m_bFlag_RunConv = true;
                                    }
                                    else
                                    {
                                        m_bFlag_RunConv = false;
                                    }
                                }
                                break;

                            case 100:
                                {
                                    //check flag from other thread that triggers sending of pallet
                                    if (
                                        m_DataUnit.Flag_Conv_OP90_Sending_b           == true ||
                                        m_DataUnit.Flag_Conv_OP100_Buffer_Sending_b   == true ||
                                        m_DataUnit.Flag_Conv_OP100_Outgoing_Sending_b == true
                                       )
                                    {
                                        m_bFlag_RunConv = true;
                                    }
                                    else
                                    {
                                        m_bFlag_RunConv = false;
                                    }
                                }
                                break;

                            case 110:
                                {
                                    //check flag from other thread that triggers sending of pallet
                                    if (
                                        m_DataUnit.Flag_Conv_OP100_Outgoing_Sending_b == true ||
                                        m_DataUnit.Flag_Conv_OP110_Sending_b          == true
                                       )
                                    {
                                        m_bFlag_RunConv = true;
                                    }
                                    else
                                    {
                                        m_bFlag_RunConv = false;
                                    }
                                }
                                break;
                        }

                        //turn on conv
                        if (m_bFlag_RunConv == true && m_DataUnit.Flag_DryRun == false)
                        {
                            //trigger conv
                            m_outConv_MoveFWD.Write(1);

                            //reset stopwatch
                            m_st_ConvTimer.Reset();
                        }
                        //turn off conv
                        else
                        {
                            //start stopwatch
                            if (m_st_ConvTimer.IsRunning == false) m_st_ConvTimer.Start();

                            if (m_st_ConvTimer.ElapsedMilliseconds > 5000)
                            {
                                //stop conv
                                m_outConv_MoveFWD.Write(0);

                                //stop stopwatch
                                m_st_ConvTimer.Stop();
                            }
                        }

                        //delay to reduce cpu process
                        Thread.Sleep(10);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //---------------------------------------------------------------------------------------------------------------------------------

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

            return true;
        }


        //Sequence steps////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public enum m_Sequence
        {
            enStart,
            enStandby,

            //sequence numbering is written here
            enRunConv,

            enPreEnd,
            enEnd
        }
    }
}