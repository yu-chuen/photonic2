using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
    public class Module_OP110_HotBlower
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl = null;
        MonitorInput m_pMonitorInput = null;
        MachineInfo m_MachineInfo = null;
        Data_UnitTempStorage m_DataUnit = null;
        Comm_TCP_RFID m_RFIDScanner = null;
        Comm_Serial_Thermocouple m_Thermocouple = null;
        Log_Data m_LogFile = null;
        Server_COMTCP_GT_CentralPC m_Server = null;
        Comm_Serial_Thermocouple m_SerialThermocouple = null;
        Data_PackageFile m_DataPackageFile = null;
        Data_Robot m_pDataRobot = null;
        public RobotXY_APSlib_PanasonicECAT m_RobotStation = null;
        Data_PerStation_DownTime m_Data_DownTime = null;
        SupportFunctions m_SupportFunctions = null;
        public List<bool> m_list_HotBlowerState = null; //hot blower ON/OFF indicator

        //IO
        #region IO
        public OutputBit m_outBlowerOn = null;
        public OutputBit m_outBlowerOff = null;

        public Cylinder m_cylHotBlower_DnUp = null;
        //public Cylinder m_cylBiasorPusher_ExtRet = null;
        public Cylinder m_cylBiasor_DnUp = null;
        public Cylinder m_cylReflector_ExtRet = null;
        public Cylinder m_cylReflector_UpDown = null;

        public Cylinder m_cylCombPNP_AxisX_ExtRet = null;
        public Cylinder m_cylCombPNP_AxisZ_DownUp = null;
        public Cylinder m_cylCombGripper_GripUngrip = null;
        #endregion

        //Flags
        #region Flags
        private readonly object m_lockRun = new object();                                       //Lock for thread sequence
        private readonly object m_lockRunEnd = new object();                                //Lock for thread sequence End
        private readonly object m_lockProcessRun = new object();                        //Flag to indicate that the process is running
        private readonly object m_lockInit = new object();                                          //Lock for Init Sequence
        private readonly object m_lockInit_OK = new object();                                   //Lock for Init Sequence
        private readonly object m_lock_Flag_Error = new object();                           //Lock for Error occur at auto sequence
        private readonly object m_lock_Flag_RunAgain = new object();

        bool m_bFlag_Init = false;
        bool m_bFlag_InitOK = false;																	//indicate init is done and good
        bool m_bFlag_Run = false;																		//indicate thread is running
        bool m_bFlag_RunEnd = false;																//signal thread to stop
        bool m_bFlag_Error = false;																	//signal: Error occur
        bool m_bFlag_ProcessRun = false;														//signal process has began
        bool m_bFlag_RunAgain = false;															//signal: Continue even when stop signal is high. Not to be used at wait section

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

        public bool Flag_RunAgain_b { set { lock (m_lock_Flag_RunAgain) { m_bFlag_RunAgain = value; } } get { lock (m_lock_Flag_RunAgain) { return m_bFlag_RunAgain; } } }

        #endregion

        //Position Tracker
        private readonly object m_lockPositionTracker = new object();
        public String m_strPosTracker = "none";
        public String Position_Tracker { set { lock (m_lockPositionTracker) { m_strPosTracker = value; } } get { lock (m_lockPositionTracker) { return m_strPosTracker; } } }

        public int m_iFlag_SequencePos = 0;
        int m_iFlagSeqLog_Skip = 0;							//avoid repeat logging

        public int m_iErrorID = 0;
        public String m_strErrorMsgAdd = "";
        public int m_iFlag_HotBlowerPointCount = 0;        //Counter: Count Hot Blower points
        private bool Flag_FirstRun = true;
        public bool Flag_CheckTemp = false;
        private bool Flag_PreviousStopped = false;

        String m_strErrorMsg_Front = "";
        Thread m_thRun = null;
        Stopwatch mSt = new Stopwatch();
        public Stopwatch m_sw_NextPalletTimeout = new Stopwatch(); //timer to check for non back to back pallet
        public Stopwatch m_sw_CheckTempTimeout = new Stopwatch(); //timer to check thermocouple temp again after back to back pallet for too long
        
        //constructor//////////////////////////////////////////////////////////////////////////////////
        public Module_OP110_HotBlower(SeqMachineControl mSeqMachineCtrl)
        {
            m_SeqMachineCtrl = mSeqMachineCtrl;
            m_MachineInfo = m_SeqMachineCtrl.m_MachineInfo;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
            m_pMonitorInput = m_SeqMachineCtrl.m_MonitorInput;
            m_RFIDScanner = m_SeqMachineCtrl.m_RFIDReader_OP90_OP110;
            m_Thermocouple = m_SeqMachineCtrl.m_CommSerialThermocouple;
            m_RobotStation = m_SeqMachineCtrl.m_robotStationHotBlower;
            m_pDataRobot = m_SeqMachineCtrl.m_DataPackageFile.m_DataRobot04;
            m_SerialThermocouple = m_SeqMachineCtrl.m_CommSerialThermocouple;
            m_DataPackageFile = m_SeqMachineCtrl.m_DataPackageFile;
            m_SupportFunctions = m_SeqMachineCtrl.m_SupportFunctions;

            //for hot blower manual page trigger used only
            m_list_HotBlowerState = new List<bool>();
            m_list_HotBlowerState.Add(false);

            //create downtime object
            m_Data_DownTime = m_SeqMachineCtrl.m_Data_PerStation_DownTime_OP110;

            //create server object and set server settings
            m_Server = new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server_Module_OP110_Station", m_MachineInfo.Get_MachineFilePath());
            m_Server.Set_Settings(m_SeqMachineCtrl.m_Server.m_Settings);
            
            //setup log file
            m_LogFile = new Log_Data();
            m_LogFile.SetFolderName("Machine_Sequence_Folder\\OP110 - HotBlower");
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
            

            //output
            iBitID = 9; m_outBlowerOn = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "OP110 - Hot Blower (On)");
            iBitID = 12; m_outBlowerOff = new OutputBit(mInterface.Get_OutputPort(5), iBitID, "OP110 - Hot Blower (Off)");

            //cylinder
            iBitID = 8; iBitID_Ret = 9; iExtID = 11; iRetID = 12; m_cylHotBlower_DnUp = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                     mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                     mInterface.Get_InputPort(6), iExtID,
                                                                                                     mInterface.Get_InputPort(6), iRetID,
                                                                                                     "OP110 - Hot Blower Z-Axis", strFilePath,
                                                                                                     m_pMonitorInput,
                                                                                                     "Down", "Up");

            iBitID = 0; iBitID_Ret = 1; iExtID = 0; iRetID = 1; m_cylBiasor_DnUp = new Cylinder(mInterface.Get_OutputPort(5), iBitID,
                                                                                                mInterface.Get_OutputPort(5), iBitID_Ret,
                                                                                                mInterface.Get_InputPort(5), iExtID,
                                                                                                mInterface.Get_InputPort(5), iRetID,
                                                                                                "OP110 - Heater Biasor", strFilePath,
                                                                                                m_pMonitorInput,
                                                                                                "Down", "Up");

            //iBitID = 2; iBitID_Ret = 3; iExtID = 2; iRetID = 3; m_cylBiasorPusher_ExtRet = new Cylinder(mInterface.Get_OutputPort(5), iBitID,
            //                                                                                          mInterface.Get_OutputPort(5), iBitID_Ret,
            //                                                                                          mInterface.Get_InputPort(5), iExtID,
            //                                                                                          mInterface.Get_InputPort(5), iRetID,
            //                                                                                          "OP110 - Heater Biasor Pusher", strFilePath,
            //                                                                                          m_pMonitorInput,
            //                                                                                          "Extend", "Retract");

            iBitID = 4; iBitID_Ret = 5; iExtID = 4; iRetID = 5; m_cylReflector_ExtRet = new Cylinder(mInterface.Get_OutputPort(5), iBitID,
                                                                                                    mInterface.Get_OutputPort(5), iBitID_Ret,
                                                                                                    mInterface.Get_InputPort(5), iExtID,
                                                                                                    mInterface.Get_InputPort(5), iRetID,
                                                                                                    "OP110 - Reflector (ExtRet)", strFilePath,
                                                                                                    m_pMonitorInput,
                                                                                                    "Extend", "Retract");

            iBitID = 6; iBitID_Ret = 7; iExtID = 6; iRetID = 7; m_cylReflector_UpDown = new Cylinder(mInterface.Get_OutputPort(5), iBitID,
                                                                                                     mInterface.Get_OutputPort(5), iBitID_Ret,
                                                                                                     mInterface.Get_InputPort(5), iExtID,
                                                                                                     mInterface.Get_InputPort(5), iRetID,
                                                                                                     "OP110 - Reflector (UpDown)", strFilePath,
                                                                                                     m_pMonitorInput,
                                                                                                     "Up", "Down");

            if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                iBitID = 5; iBitID_Ret = 4; iExtID = 6; iRetID = 5; m_cylCombPNP_AxisX_ExtRet = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                             mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                             mInterface.Get_InputPort(6), iExtID,
                                                                                                             mInterface.Get_InputPort(6), iRetID,
                                                                                                             "OP110 - Comb PNP Axis X", strFilePath,
                                                                                                             m_pMonitorInput,
                                                                                                             "Extend", "Retract");

                iBitID = 6; iBitID_Ret = 7; iExtID = 8; iRetID = 9; m_cylCombPNP_AxisZ_DownUp = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                             mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                             mInterface.Get_InputPort(6), iExtID,
                                                                                                             mInterface.Get_InputPort(6), iRetID,
                                                                                                             "OP110 - Comb PNP Axis Z", strFilePath,
                                                                                                             m_pMonitorInput,
                                                                                                             "Down", "Up");

                iBitID = 10; iBitID_Ret = 11; iExtID = 15; iRetID = 14; m_cylCombGripper_GripUngrip = new Cylinder(mInterface.Get_OutputPort(6), iBitID,
                                                                                                       mInterface.Get_OutputPort(6), iBitID_Ret,
                                                                                                       mInterface.Get_InputPort(6), iExtID,
                                                                                                       mInterface.Get_InputPort(6), iRetID,
                                                                                                       "OP110 - Comb Gripper", strFilePath,
                                                                                                       m_pMonitorInput,
                                                                                                       "Grip", "Ungrip");
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

            m_LogFile.Write_ToSeqLog("=====> Module_OP110_HotBlower: " + strLog);
        }

        private void SetLog_Sequence(int iSequencePos)
        {
            String strLog = "";

            //get the enum string list
            strLog = Enum.GetName(typeof(m_Sequence), iSequencePos);

            //check waiting sequence
            if (m_iFlagSeqLog_Skip == iSequencePos)
            {
                return;
            }
            else
            {
                m_iFlagSeqLog_Skip = iSequencePos;
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
            String strError = "Module_OP110_HotBlower: " + m_strErrorMsgAdd;
            m_strErrorMsgAdd = "";
            return strError;
        }

        public String Get_ErrorMsg_Front()
        {
            String strError = "Module_OP110_HotBlower: " + m_strErrorMsg_Front + ": ";
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
            strLog = "ErrorID=" + iErrorID + " : Device: " + strErrorMsgFront + " : ErrorMsg=" + strErrorMsgAdd + ": ErrorName=" + Enum.GetName(typeof(ErrorTable.en_ErrorCode), m_iErrorID);
            SetLog(strLog, 999);
        }

        public void Set_CylError(Cylinder mCyl) { Set_Error(mCyl.Get_ErrorID(), mCyl.Get_ErrorMessage(), mCyl.Get_Label()); }



        /************************************************************************************************************************
         * Others / Supporting functions
         ************************************************************************************************************************/

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
            if (m_Server.Cmd_InsertErrorLog(m_DataUnit.RFID_OP110_SN_str, strDateTime, int.Parse(m_MachineInfo.m_strMachineID), iErrorCode, Enum.GetName(typeof(ErrorTable.en_ErrorCode), iErrorCode), strErrorDesc) == false)
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
            lock (m_DataUnit.m_lock_Set_Station_DownTime_OP110)
            {
                if (bDown == true)
                {
                    if (m_Data_DownTime.m_Data.m_str_Down_DateTime == "")
                    {
                        //set variables
                        //check MachineID is Leg A, Leg B or Leg C
                        if (m_MachineInfo.m_strMachineID == "002")
                        {
                            m_Data_DownTime.m_Data.m_str_StationName = m_MachineInfo.m_strOP110 + "A";
                        }
                        else if (m_MachineInfo.m_strMachineID == "003")
                        {
                            m_Data_DownTime.m_Data.m_str_StationName = m_MachineInfo.m_strOP110 + "B";
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

        /// <summary>
        /// Trigger HotBlower Function
        /// </summary>
        /// <param name="bON">True = ON, False = OFF</param>
        public void Trigger_HotBlower_OnOff(bool bON)
        {
            if (bON == true) //ON
            {
                //check if hotblower is OFF then trigger ON
                if (m_outBlowerOn.Read() == 0)
                {
                    m_outBlowerOn.Write(1);
                }

                //state for manual page trigger
                m_list_HotBlowerState[0] = true;
            }
            else //OFF
            {
                //check if hotblower is ON then trigger OFF
                if (m_outBlowerOn.Read() == 1)
                {
                    m_outBlowerOff.Write(1);

                    //must delay a while before turn off DO otherwise hotblower cannot detect DO has been triggered
                    Delay_ms(100);
                }

                //init DO
                m_outBlowerOn.Write(0);
                m_outBlowerOff.Write(0);

                //state for manual page trigger
                m_list_HotBlowerState[0] = false;
            }

            //must delay here again to prevent double trigger and caused the ON OFF state messed up
            Delay_ms(100);
        }


        /************************************************************************************************************************
         * Robot Movement
        ************************************************************************************************************************/
        //Robot: Goto Position///////////////////////////////////////////////////////////////////////////////
        public bool Goto_RobotPos(String strPos, bool bManualPageGotoZht = false)
        {
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
            bool bRet = true;

            //skip if same position
            if (Position_Tracker == strPos && bManualPageGotoZht == false) return true;

            //check
            if (Flag_InitOK == false) { Set_Error((int)ErrorTable.en_ErrorCode.MACHINE_INITFAIL, "" + m_RobotStation.m_strLabel); return false; }

            //get position from data package
            mPosition = Get_RobotPosition(strPos, ref bRet, bManualPageGotoZht);
            if (bRet == false) return false;

            //Interlock
            if (Interlock(strPos, mPosition) == false) return false;

            //move to position
            if (mPosition.m_iMoveType == 1)
                bRet = m_RobotStation.Move_Abs(mPosition);
            else
                bRet = m_RobotStation.Move_Rel(mPosition);

            //Error
            if (bRet == false) { Set_Error(m_RobotStation.Get_ErrorID(), m_RobotStation.Get_ErrorMessage(), m_RobotStation.m_strLabel); }

            Position_Tracker = strPos;
            return bRet;
        }

        //Get Robot Position//////////////////////////////////////////////////////////////////////////////////
        public Robot_Pos_Settings Get_RobotPosition(String strPos, ref bool bRet, bool bManualPageGotoZht)
        {
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
            String strLog = "";
            bRet = true;

            //get Position
            mPosition = m_pDataRobot.Get_Settings(strPos);

            //logger------------------------------------------------------------------
            strLog = "Get_RobotPosition from Teach memory: Pos=" + strPos +
                            ", X=" + mPosition.m_dbXPos.ToString() +
                            ", Y=" + mPosition.m_dbY1Pos.ToString() +
                            ", Z=" + mPosition.m_dbZPos.ToString() +
                            ", dbZht=" + mPosition.m_dbInterlock_Zht.ToString() +
                            ", Interlock=" + mPosition.m_iInterlock.ToString();
            SetLog(strLog);

            return mPosition;
        }

        //Movement Interlocks/////////////////////////////////////////////////////////////////////////////
        //Note: Cannot call Goto_RobotPos function here. application crashes.
        public bool Interlock(String strPos, Robot_Pos_Settings mPosition)
        {
            //if (m_cylAxisX_ExtRet.Get_Home_Sensor().Read() != 1 || m_cylAxisX_ExtRet.Get_Home_Output().Read() != 1)
            //{
            //    Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CYLINDER_COLLISION, "", m_cylAxisX_ExtRet.Get_Label());
            //    return false;
            //}

            //if (m_cylAxisZ_DownUp.Get_Home_Sensor().Read() != 1 || m_cylAxisZ_DownUp.Get_Home_Output().Read() != 1)
            //{
            //    Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CYLINDER_COLLISION, "", m_cylAxisZ_DownUp.Get_Label());
            //    return false;
            //}

            return true;
        }

        //Check robot point enable//////////////////////////////////////////////////////////////////////////////////////////////
        public bool Check_PointRobot(String strRobotPos)
        {
            bool bEnable = false;

            //dry run
            //if (m_DataUnit.Flag_DryRun) return true; //for dry run, follow the enable flag of the hotblower point otherwise if some position is not teached correctly, it will crash into something.
            if (strRobotPos == "") { SetLog("Check_PointRobotRotary: Empty string detected...", 2); return true; }

            //get selection

            if (strRobotPos == Pos_PreHeat_1) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_1;
            else if (strRobotPos == Pos_PreHeat_2) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_2;
            else if (strRobotPos == Pos_PreHeat_3) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_3;
            else if (strRobotPos == Pos_PreHeat_4) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_4;
            else if (strRobotPos == Pos_PreHeat_5) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_5;
            else if (strRobotPos == Pos_PreHeat_6) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PreHeat_6;

            else if (strRobotPos == Pos_Terminal_1_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal1_Ceramic;
            else if (strRobotPos == Pos_Terminal_1_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal1_Tip;

            else if (strRobotPos == Pos_Terminal_2_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal2_Ceramic;
            else if (strRobotPos == Pos_Terminal_2_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal2_Tip;

            else if (strRobotPos == Pos_Terminal_3_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal3_Ceramic;
            else if (strRobotPos == Pos_Terminal_3_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal3_Tip;

            else if (strRobotPos == Pos_Terminal_4_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal4_Ceramic;
            else if (strRobotPos == Pos_Terminal_4_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal4_Tip;

            else if (strRobotPos == Pos_Terminal_5_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal5_Ceramic;
            else if (strRobotPos == Pos_Terminal_5_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal5_Tip;

            else if (strRobotPos == Pos_Terminal_6_Ceramic) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal6_Ceramic;
            else if (strRobotPos == Pos_Terminal_6_Tip) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_Terminal6_Tip;

            else if (strRobotPos == Pos_PostHeat_1) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_1;
            else if (strRobotPos == Pos_PostHeat_2) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_2;
            else if (strRobotPos == Pos_PostHeat_3) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_3;
            else if (strRobotPos == Pos_PostHeat_4) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_4;
            else if (strRobotPos == Pos_PostHeat_5) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_5;
            else if (strRobotPos == Pos_PostHeat_6) bEnable = m_DataPackageFile.m_DataCommon.m_bHotBlower_PostHeat_6;

            return bEnable;
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
            m_DataUnit.Flag_Station_OP110_Done_b = false;
            Flag_FirstRun = true;
            Flag_CheckTemp = false;
            Flag_PreviousStopped = false;

            //reset sequence
            m_iFlag_SequencePos = 0;
            m_iFlagSeqLog_Skip = 0;

            //reset IO
            Trigger_HotBlower_OnOff(false);
            if (m_cylHotBlower_DnUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylHotBlower_DnUp); goto End; }
            //if (m_cylBiasorPusher_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylBiasorPusher_ExtRet); goto End; }
            if (m_cylBiasor_DnUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylBiasor_DnUp); goto End; }
            if (m_cylReflector_UpDown.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylReflector_UpDown); goto End; }
            if (m_cylReflector_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylReflector_ExtRet); goto End; }

            if (m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b == false)
            {
                if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                {
                    if (m_cylCombGripper_GripUngrip.Get_Work_Sensor().Read() == 1 || m_cylCombGripper_GripUngrip.Get_Work_Output().Read() == 1)
                    {
                        if (m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); goto End; }
                        if (m_cylCombPNP_AxisX_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisX_ExtRet); goto End; }
                        if (m_cylCombGripper_GripUngrip.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombGripper_GripUngrip); goto End; }
                    }
                    else
                    {
                        if (m_cylCombGripper_GripUngrip.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombGripper_GripUngrip); goto End; }
                        if (m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); goto End; }
                        if (m_cylCombPNP_AxisX_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisX_ExtRet); goto End; }
                    }
                }
            }

            //Init Robotstation
            if (m_RobotStation.Homing() == false)
            {
                Set_Error(m_RobotStation.Get_ErrorID(), m_RobotStation.Get_ErrorMessage(), m_RobotStation.m_strLabel);
                SetLog("RobotStation Homing Return Error...");
                goto End;
            }

            Position_Tracker = Pos_Home;

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
            if (Module_Running == false)
            {
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
            Module_Running = true;

            //set thread identifier, get start date time, upload to server, reset data
            int iThreadIdentifier = 2;
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
                    Flag_PreviousStopped = true;
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
            Trigger_HotBlower_OnOff(false); //turn off HotBlower
            m_sw_NextPalletTimeout.Reset();
            m_sw_CheckTempTimeout.Reset();
            Module_Running = false;
        }

        //Auto Sequence//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence()
        {
            switch (m_iFlag_SequencePos)
            {
                case (int)m_Sequence.enStart:
                    {
                        //if run end lot, dont let sequence continue, forever loop here
                        if (m_DataUnit.Flag_EndLot == true || m_DataUnit.Flag_Enable_TestMode_b == true) { Delay_ms(1000); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //reset everything to default state here
                case (int)m_Sequence.enStandby:
                    {
                        //reset Flag
                        m_DataUnit.Flag_Station_OP110_Done_b = false;

                        //reset counter
                        m_iFlag_HotBlowerPointCount = 1;
                        m_DataUnit.m_HotBlower_Temp_db = 0;

                        //reset cylinder
                        m_cylHotBlower_DnUp.Cyl_Trigger(false);
                        m_cylReflector_UpDown.Cyl_Trigger(false);
                        //m_cylBiasorPusher_ExtRet.Cyl_Trigger(false);
                        if (m_cylHotBlower_DnUp.WaitTaskComplete() == false) { Set_CylError(m_cylHotBlower_DnUp); return false; }
                        if (m_cylReflector_UpDown.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_UpDown); return false; }
                        //if (m_cylBiasorPusher_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylBiasorPusher_ExtRet); return false; }

                        m_cylReflector_ExtRet.Cyl_Trigger(false);
                        m_cylBiasor_DnUp.Cyl_Trigger(false);
                        if (m_cylReflector_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_ExtRet); return false; }
                        if (m_cylBiasor_DnUp.WaitTaskComplete() == false) { Set_CylError(m_cylBiasor_DnUp); return false; }

                        if (m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b == false)
                        {
                            if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                            {
                                if (m_cylCombGripper_GripUngrip.Get_Work_Sensor().Read() == 1 || m_cylCombGripper_GripUngrip.Get_Work_Output().Read() == 1) //gripper is gripping comb, continuation from enWait_CombReturn sequence so that the sequence is faster
                                {
                                    //axis Z up
                                    if (m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); return false; }

                                    //axis X retract
                                    if (m_cylCombPNP_AxisX_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisX_ExtRet); return false; }

                                    //ungrip comb
                                    if (m_cylCombGripper_GripUngrip.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombGripper_GripUngrip); return false; }
                                }
                                else
                                {
                                    m_cylCombGripper_GripUngrip.Cyl_Trigger(false);
                                    m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger(false);
                                    if (m_cylCombGripper_GripUngrip.WaitTaskComplete() == false) { Set_CylError(m_cylCombGripper_GripUngrip); return false; }
                                    if (m_cylCombPNP_AxisZ_DownUp.WaitTaskComplete() == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); return false; }

                                    if (m_cylCombPNP_AxisX_ExtRet.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisX_ExtRet); return false; }
                                }
                            }
                        }

                        //go to position
                        if (Goto_RobotPos(Pos_Standby) == false) { return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag;
                    break;

                /*********************************************************************************************************
                 * Sequence written here
                 *********************************************************************************************************/
                
                case (int)m_Sequence.enWait_TriggerFlag:
                    {
                        //check conditions to trigger check temperature sequence
                        if (m_sw_NextPalletTimeout.ElapsedMilliseconds > m_MachineInfo.m_int_HotBlower_NextPalletTimeout)
                        {
                            Trigger_HotBlower_OnOff(false); //turn off hot blower after timeout
                            Flag_CheckTemp = true;
                            m_sw_NextPalletTimeout.Reset();
                        }

                        //check conditions to trigger check temperature sequence
                        if (m_sw_CheckTempTimeout.ElapsedMilliseconds > m_MachineInfo.m_int_HotBlower_CheckTempTimeout)
                        {
                            Flag_CheckTemp = true;
                            m_sw_CheckTempTimeout.Reset();
                        }

                        if (Flag_PreviousStopped == true)
                        {
                            Flag_PreviousStopped = false; //reset
                            Flag_CheckTemp = true;
                        }

                        //normal auto run only
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            if (m_DataUnit.Flag_Conv_OP100_Outgoing_Sending_b == true) //start trigger hot blower when OP100 send pallet
                            {
                                Trigger_HotBlower_OnOff(true); //turn on HotBlower
                            }
                        }

                        //wait for trigger flag
                        if (m_DataUnit.Flag_Conv_OP110_Trigger_b == false) { Delay_ms(10); break; }

                        //normal auto run only
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            Trigger_HotBlower_OnOff(true); //turn on HotBlower
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enHeaterBiasor_Process;
                    break;

                case (int)m_Sequence.enHeaterBiasor_Process:
                    {
                        //make sure blower is up
                        if (m_cylHotBlower_DnUp.Cyl_Trigger(false) == false) { Set_CylError(m_cylHotBlower_DnUp); return false; }

                        //biasor down, reflector ext towards pallet
                        m_cylBiasor_DnUp.Cyl_Trigger(true);
                        m_cylReflector_ExtRet.Cyl_Trigger(true);
                        if (m_cylBiasor_DnUp.WaitTaskComplete() == false) { Set_CylError(m_cylBiasor_DnUp); return false; }
                        if (m_cylReflector_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_ExtRet); return false; }

                        //biasor pusher ext, reflector up
                        //m_cylBiasorPusher_ExtRet.Cyl_Trigger(true);
                        m_cylReflector_UpDown.Cyl_Trigger(true);
                        //if (m_cylBiasorPusher_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylBiasorPusher_ExtRet); return false; }
                        if (m_cylReflector_UpDown.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_UpDown); return false; }

                        if (Flag_FirstRun == true || Flag_CheckTemp == true)
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enCheck_Temperature;
                        }
                        else
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enTrigger_BlowerDown;
                        }
                    }
                    break;

                case (int)m_Sequence.enCheck_Temperature:
                    {
                        String strData = "";
                        double dbData = 0;

                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            if (Goto_RobotPos(Pos_Thermocouple) == false) { return false; }

                            //trigger hotblower on again here because if sequence stopped when temperature not reached, it will not ON hotblower back again.
                            Trigger_HotBlower_OnOff(true);

                            //start timer
                            mSt.Restart();

                            //wait blower reach temperature
                            while (dbData < m_DataPackageFile.m_DataCommon.m_dbHotBlower_Temperature)
                            {
                                //read value from thermocouple
                                if (m_Thermocouple.Read_Temperature(ref strData) == false)
                                {
                                    Set_Error((int)ErrorTable.en_ErrorCode.HOTBLOWER_TEMPREADFAIL, "");
                                    return false;
                                }

                                dbData = double.Parse(strData);
                                m_DataUnit.m_HotBlower_Temp_db = dbData; //assign data to display on monitor

                                //error if reach time out
                                if (mSt.ElapsedMilliseconds > m_DataPackageFile.m_DataCommon.m_lg_BlowerTimeout_Run)
                                {
                                    //stop timer
                                    mSt.Stop();

                                    //time out error
                                    Set_Error((int)ErrorTable.en_ErrorCode.HOTBLOWER_RUN_TIMEOUT, "");
                                    return false;
                                }

                                if (Module_StopRun == true) { return true; }
                            }

                            //stop timer
                            mSt.Stop();
                        }

                        //reset flag after check temp
                        Flag_CheckTemp = false;

                        //goto standby position
                        if (Goto_RobotPos(Pos_Standby) == false) { return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enTrigger_BlowerDown;
                    break;

                case (int)m_Sequence.enTrigger_BlowerDown:
                    {
                        //blower down
                        if (m_cylHotBlower_DnUp.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylHotBlower_DnUp); return false; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enRun_Process;
                    break;

                case (int)m_Sequence.enRun_Process:
                    {
                        String strPos = "";

                        //set to finish all points before stopping
                        Flag_RunAgain_b = true;

                        //goto blower point
                        switch (m_iFlag_HotBlowerPointCount)
                        {
                            case 1: strPos  = Pos_PreHeat_1; break;
                            case 2: strPos  = Pos_PreHeat_2; break;
                            case 3: strPos  = Pos_PreHeat_3; break;
                            case 4: strPos  = Pos_PreHeat_4; break;
                            case 5: strPos  = Pos_PreHeat_5; break;
                            case 6: strPos  = Pos_PreHeat_6; break;

                            case 7: strPos  = Pos_Terminal_1_Ceramic; break;
                            case 8: strPos  = Pos_Terminal_1_Tip; break;
                            case 9: strPos  = Pos_Terminal_1_Ceramic; break;

                            case 10: strPos = Pos_Terminal_2_Ceramic; break;
                            case 11: strPos = Pos_Terminal_2_Tip; break;
                            case 12: strPos = Pos_Terminal_2_Ceramic; break;

                            case 13: strPos = Pos_Terminal_3_Ceramic; break;
                            case 14: strPos = Pos_Terminal_3_Tip; break;
                            case 15: strPos = Pos_Terminal_3_Ceramic; break;

                            case 16: strPos = Pos_Terminal_4_Ceramic; break;
                            case 17: strPos = Pos_Terminal_4_Tip; break;
                            case 18: strPos = Pos_Terminal_4_Ceramic; break;

                            case 19: strPos = Pos_Terminal_5_Ceramic; break;
                            case 20: strPos = Pos_Terminal_5_Tip; break;
                            case 21: strPos = Pos_Terminal_5_Ceramic; break;

                            case 22: strPos = Pos_Terminal_6_Ceramic; break;
                            case 23: strPos = Pos_Terminal_6_Tip; break;
                            case 24: strPos = Pos_Terminal_6_Ceramic; break;

                            case 25: strPos = Pos_PostHeat_1; break;
                            case 26: strPos = Pos_PostHeat_2; break;
                            case 27: strPos = Pos_PostHeat_3; break;
                            case 28: strPos = Pos_PostHeat_4; break;
                            case 29: strPos = Pos_PostHeat_5; break;
                            case 30: strPos = Pos_PostHeat_6; break;

                            default:
                                m_iFlag_SequencePos = (int)m_Sequence.enProcess_Done;
                                return true;
                        }

                        //check point enable status
                        if (Check_PointRobot(strPos) == false)
                        {
                            //increment flag count 
                            m_iFlag_HotBlowerPointCount++;
                            break;
                        }

                        // goto position
                        if (Goto_RobotPos(strPos) == false) { return false; }

                        //increase counter
                        m_iFlag_HotBlowerPointCount++;
                    }
                    break;

                case (int)m_Sequence.enProcess_Done:
                    {
                        if (m_DataUnit.Flag_DryRun == false)
                        {
                            Trigger_HotBlower_OnOff(false); //turn off HotBlower
                            Trigger_HotBlower_OnOff(true); //turn on HotBlower again to reset hakko blower device internal timer
                        }

                        //goto standby position
                        if (Goto_RobotPos(Pos_Standby) == false) { return false; }

                        //blower up, reflector down, biasor pusher retract
                        m_cylHotBlower_DnUp.Cyl_Trigger(false);
                        m_cylReflector_UpDown.Cyl_Trigger(false);
                        //m_cylBiasorPusher_ExtRet.Cyl_Trigger(false);
                        if (m_cylHotBlower_DnUp.WaitTaskComplete() == false) { Set_CylError(m_cylHotBlower_DnUp); return false; }
                        if (m_cylReflector_UpDown.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_UpDown); return false; }
                        //if (m_cylBiasorPusher_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylBiasorPusher_ExtRet); return false; }

                        //reflector out, biasor up
                        m_cylReflector_ExtRet.Cyl_Trigger(false);
                        m_cylBiasor_DnUp.Cyl_Trigger(false);
                        if (m_cylReflector_ExtRet.WaitTaskComplete() == false) { Set_CylError(m_cylReflector_ExtRet); return false; }
                        if (m_cylBiasor_DnUp.WaitTaskComplete() == false) { Set_CylError(m_cylBiasor_DnUp); return false; }

                        if (m_DataUnit.Flag_Bypass_OP110_AutoPNPSpringComb_b == true)
                        {
                            m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag_Down;
                            break;
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_CombReturn;
                    break;

                case (int)m_Sequence.enWait_CombReturn:
                    {
                        if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                        {
                            //axis X extend
                            if (m_cylCombPNP_AxisX_ExtRet.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylCombPNP_AxisX_ExtRet); return false; }

                            //axis Z down
                            if (m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); return false; }

                            //grip comb
                            if (m_cylCombGripper_GripUngrip.Cyl_Trigger_N_WaitTaskComplete(true) == false) { Set_CylError(m_cylCombGripper_GripUngrip); return false; }

                            //axis Z up
                            if (m_cylCombPNP_AxisZ_DownUp.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_CylError(m_cylCombPNP_AxisZ_DownUp); return false; }
                        }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_TriggerFlag_Down;
                    break;

                case (int)m_Sequence.enWait_TriggerFlag_Down:
                    {
                        //set done flag true
                        m_DataUnit.Flag_Station_OP110_Done_b = true;

                        //check trigger flag down
                        if (m_DataUnit.Flag_Conv_OP110_Trigger_b == true) { Delay_ms(10); break; }

                        //set done flag false
                        m_DataUnit.Flag_Station_OP110_Done_b = false;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //-----------------------------------------------------------------------------------------------------------------

                case (int)m_Sequence.enPreEnd:
                    {
                        //start timer
                        m_sw_NextPalletTimeout.Restart(); //restart every cycle
                        if (m_sw_CheckTempTimeout.IsRunning == false) { m_sw_CheckTempTimeout.Restart(); } //only restart for first cycle, then restart during timeout is exceeded in enWait_TriggerFlag sequence
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enEnd;
                    break;

                case (int)m_Sequence.enEnd:
                    {
                        Flag_FirstRun = false; //set to false after the first sequence cycle
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
            enHeaterBiasor_Process,
            enCheck_Temperature,
            enTrigger_BlowerDown,
            enRun_Process,
            enProcess_Done,
            enWait_CombReturn,
            enWait_TriggerFlag_Down,
            /////////////////////////////////////////

            enPreEnd,
            enEnd
        }

        //position name
        public String Pos_Home { get { return "Home"; } }

        public String Pos_Thermocouple          { get { return "P0"; } }
        public String Pos_Standby               { get { return "P1"; } }
        public String Pos_Terminal_1_Ceramic    { get { return "P2"; } }
        public String Pos_Terminal_1_Tip        { get { return "P3"; } }
        public String Pos_Terminal_2_Ceramic    { get { return "P4"; } }
        public String Pos_Terminal_2_Tip        { get { return "P5"; } }
        public String Pos_Terminal_3_Ceramic    { get { return "P6"; } }
        public String Pos_Terminal_3_Tip        { get { return "P7"; } }
        public String Pos_Terminal_4_Ceramic    { get { return "P8"; } }
        public String Pos_Terminal_4_Tip        { get { return "P9"; } }
        public String Pos_Terminal_5_Ceramic    { get { return "P10"; } }
        public String Pos_Terminal_5_Tip        { get { return "P11"; } }
        public String Pos_Terminal_6_Ceramic    { get { return "P12"; } }
        public String Pos_Terminal_6_Tip        { get { return "P13"; } }
        public String Pos_PreHeat_1             { get { return "P14"; } }
        public String Pos_PreHeat_2             { get { return "P15"; } }
        public String Pos_PreHeat_3             { get { return "P16"; } }
        public String Pos_PreHeat_4             { get { return "P17"; } }
        public String Pos_PreHeat_5             { get { return "P18"; } }
        public String Pos_PreHeat_6             { get { return "P19"; } }
        public String Pos_PostHeat_1             { get { return "P20"; } }
        public String Pos_PostHeat_2             { get { return "P21"; } }
        public String Pos_PostHeat_3             { get { return "P22"; } }
        public String Pos_PostHeat_4             { get { return "P23"; } }
        public String Pos_PostHeat_5             { get { return "P24"; } }
        public String Pos_PostHeat_6             { get { return "P25"; } }
    }
}