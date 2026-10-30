using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace StaticCoreLib
{
    public class Module_LeakTester
    {
        //variables
        //Classes
        public SeqMachineControl m_SeqMachineCtrl = null;
        public Data_UnitTempStorage m_DataUnit = null;                     //data global
        public Data_UnitStorage m_DataUnitNest = null;                      //data according to the nest only
        public Data_PackageFile m_DataPackageFile = null;
        public Data_Common m_DataCommon = null;
        public MachineInfo m_MachineInfo = null;
        public Comm_Serial_KeyenceBarcodeScanner m_CodeScanner = null;
        public Comm_Serial_AirLeakTester_LSR700 m_AirLeakTester = null;
        public Log_Data m_pData_Log = null;

        Stopwatch m_stCycleTime = new Stopwatch();
        Stopwatch m_stDelay_AirleakTester = new Stopwatch();

        //IO
        public InputBit m_inPushButton;
        public Sensor m_senPartPresent;
        public Sensor m_senLeakTester_Error;
        public Sensor m_senLeakTester_Pass;
        public Sensor m_senLeakTester_Busy;
        public Sensor m_senLeakTester_End;
        public Sensor m_senLeakTester_ULFail;
        public Sensor m_senLeakTester_LLFail;
        public Sensor m_senLeakTester_UL2Fail;
        public Sensor m_senLeakTester_LL2Fail;


        public OutputBit m_outValve_UUTSelect;
        public OutputBit m_outValve_TopMasterA;
        public OutputBit m_outValve_TopMasterB;
        public OutputBit m_outValve_BottomMasterA;
        public OutputBit m_outValve_BottomMasterB;
        public OutputBit m_outLeakTester_Start;
        public OutputBit m_outLeakTester_Stop;
        public OutputBit m_outLeakTester_CH0;
        public OutputBit m_outLeakTester_CH1;
        public OutputBit m_outLeakTester_CH2;
        public OutputBit m_outLeakTester_CH3;
        public OutputBit m_outLeakTester_CH4;
        public OutputBit m_outTestPressureSelection;



        //Flags
        private readonly object m_lockRun = new object();                                       //Lock for thread sequence
        private readonly object m_lockRunEnd = new object();                                //Lock for thread sequence End
        private readonly object m_lock_Flag_Error = new object();                           //Lock for Error occur at auto sequence
        private readonly object m_lockInit = new object();                                          //Lock for Init Sequence
        private readonly object m_lockInit_OK = new object();                                   //Lock for Init Sequence

        bool m_bFlag_Init = false;
        bool m_bFlag_InitOK = false;                                     //indicate init is done and good
        bool m_bFlag_Run = false;                                         //indicate thread is running
        bool m_bFlag_RunEnd = false;                                  //signal thread to stop
        bool m_bFlag_Error = false;                                      //signal: Error occur
        
        public bool Module_Runing
        {
            set { lock (m_lockRun) { m_bFlag_Run = value; } }
            get { lock (m_lockRun) { return m_bFlag_Run; } }
        }                                                                //Indicate if the module is running
        public bool Module_StopRun
        {
            set { lock (m_lockRun) { m_bFlag_RunEnd = value; } }
            get { lock (m_lockRun) { return m_bFlag_RunEnd; } }
        }
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

        //others
        Thread m_thRun = null;
        String m_strErrorMessage = "";
        String m_strLabel = "";
        int m_iFlag_SequencePos = 0;                                 //Contain the sequence position
        int m_iNest = 1;                                                            //indicate nest A or nest B




        //constructor/////////////////////////////////////////////////////////////////
        public Module_LeakTester(SeqMachineControl mSeqMachineCtrl)
        {
            m_SeqMachineCtrl    = mSeqMachineCtrl;
            m_MachineInfo           = m_SeqMachineCtrl.m_MachineInfo;
            m_DataUnit                 = m_SeqMachineCtrl.m_DataUnit;
            m_DataPackageFile = m_SeqMachineCtrl.m_DataPackageFile;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Init or Create cylinders and IO ports
        public bool Create_InputsOutputs(InterfaceCards mInterface)
        {
            MyIPort pIPort1 = mInterface.Get_InputPort(1);
            MyIPort pIPort2 = mInterface.Get_InputPort(2);
            MyOPort pOPort1 = mInterface.Get_OutputPort(1);
            MyOPort pOPort2 = mInterface.Get_OutputPort(2);
            int iBitID = 0;
            String strSettingFilePath = m_MachineInfo.Get_MachineFilePath();

            //NEST A------------------------------------------------------------------------------------------------------------------------
            if (m_iNest == 1)
            {
                //Input bit
                iBitID = 4; m_inPushButton = new InputBit(pIPort1, iBitID, "Push Button Nest A");
                iBitID = 6; m_senPartPresent = new Sensor(pIPort1, iBitID);                 m_senPartPresent.Set_Label("Part Present", strSettingFilePath);
                iBitID = 0; m_senLeakTester_Error = new Sensor(pIPort2, iBitID);        m_senLeakTester_Error.Set_Label("Leak Tester Error", strSettingFilePath);
                iBitID = 1; m_senLeakTester_Pass = new Sensor(pIPort2, iBitID);        m_senLeakTester_Pass.Set_Label("Leak Tester Pass", strSettingFilePath);
                iBitID = 3; m_senLeakTester_Busy = new Sensor(pIPort2, iBitID);        m_senLeakTester_Busy.Set_Label("Leak Tester Busy", strSettingFilePath);
                iBitID = 4; m_senLeakTester_End = new Sensor(pIPort2, iBitID);          m_senLeakTester_End.Set_Label("Leak Tester End", strSettingFilePath);
                iBitID = 2; m_senLeakTester_ULFail = new Sensor(pIPort2, iBitID);      m_senLeakTester_ULFail.Set_Label("Leak Tester UL Fail", strSettingFilePath);
                iBitID = 6; m_senLeakTester_LLFail = new Sensor(pIPort2, iBitID);      m_senLeakTester_LLFail.Set_Label("Leak Tester LL Fail", strSettingFilePath);
                iBitID = 7; m_senLeakTester_UL2Fail = new Sensor(pIPort2, iBitID);    m_senLeakTester_UL2Fail.Set_Label("Leak Tester UL2 Fail", strSettingFilePath); 
                iBitID = 5; m_senLeakTester_LL2Fail = new Sensor(pIPort2, iBitID);    m_senLeakTester_LL2Fail.Set_Label("Leak Tester LL2 Fail", strSettingFilePath); 


                //output bit
                iBitID = 8; m_outValve_UUTSelect            = new OutputBit(pOPort1, iBitID, "Valve UUT Select");
                iBitID = 9; m_outValve_TopMasterA          = new OutputBit(pOPort1, iBitID, "Valve Top Master A");
                iBitID = 10; m_outValve_TopMasterB        = new OutputBit(pOPort1, iBitID, "Valve Top Master B");
                iBitID = 11; m_outValve_BottomMasterA  = new OutputBit(pOPort1, iBitID, "Valve Bottom Master A");
                iBitID = 5; m_outValve_BottomMasterB    = new OutputBit(pOPort1, iBitID, "Valve Bottom Master B");
                iBitID = 0;   m_outLeakTester_Start           = new OutputBit(pOPort2, iBitID, "Leak Tester Start");
                iBitID = 1;   m_outLeakTester_Stop           = new OutputBit(pOPort2, iBitID, "Leak Tester Stop");
                iBitID = 6;   m_outLeakTester_CH0           = new OutputBit(pOPort2, iBitID, "Leak Tester CH0");
                iBitID = 5;   m_outLeakTester_CH1           = new OutputBit(pOPort2, iBitID, "Leak Tester CH1");
                iBitID = 4;   m_outLeakTester_CH2           = new OutputBit(pOPort2, iBitID, "Leak Tester CH2");
                iBitID = 3;   m_outLeakTester_CH3           = new OutputBit(pOPort2, iBitID, "Leak Tester CH3");
                iBitID = 2;   m_outLeakTester_CH4           = new OutputBit(pOPort2, iBitID, "Leak Tester CH4");
                iBitID = 7; m_outTestPressureSelection    = new OutputBit(pOPort2, iBitID, "Valve TestPressure Select");
            }

            //NEST B------------------------------------------------------------------------------------------------------------------------
            else
            {
                //Input bit
                iBitID = 5; m_inPushButton = new InputBit(pIPort1, iBitID, "Push Button Nest B");
                iBitID = 7; m_senPartPresent = new Sensor(pIPort1, iBitID);                  m_senPartPresent.Set_Label("Part Present", strSettingFilePath);
                iBitID = 8; m_senLeakTester_Error = new Sensor(pIPort2, iBitID);         m_senLeakTester_Error.Set_Label("Leak Tester Error", strSettingFilePath);
                iBitID = 9; m_senLeakTester_Pass = new Sensor(pIPort2, iBitID);         m_senLeakTester_Pass.Set_Label("Leak Tester Pass", strSettingFilePath);
                iBitID = 11; m_senLeakTester_Busy = new Sensor(pIPort2, iBitID);       m_senLeakTester_Busy.Set_Label("Leak Tester Busy", strSettingFilePath);
                iBitID = 12; m_senLeakTester_End = new Sensor(pIPort2, iBitID);         m_senLeakTester_End.Set_Label("Leak Tester End", strSettingFilePath);
                iBitID = 10; m_senLeakTester_ULFail = new Sensor(pIPort2, iBitID);     m_senLeakTester_ULFail.Set_Label("Leak Tester UL Fail", strSettingFilePath);
                iBitID = 14; m_senLeakTester_LLFail = new Sensor(pIPort2, iBitID);      m_senLeakTester_LLFail.Set_Label("Leak Tester LL Fail", strSettingFilePath);
                iBitID = 15; m_senLeakTester_UL2Fail = new Sensor(pIPort2, iBitID);   m_senLeakTester_UL2Fail.Set_Label("Leak Tester UL2 Fail", strSettingFilePath); 
                iBitID = 13; m_senLeakTester_LL2Fail = new Sensor(pIPort2, iBitID);    m_senLeakTester_LL2Fail.Set_Label("Leak Tester LL2 Fail", strSettingFilePath); 


                //output bit
                iBitID = 12; m_outValve_UUTSelect             = new OutputBit(pOPort1, iBitID, "Valve UUT Select");
                iBitID = 13; m_outValve_TopMasterA           = new OutputBit(pOPort1, iBitID, "Valve Top Master A");
                iBitID = 14; m_outValve_TopMasterB           = new OutputBit(pOPort1, iBitID, "Valve Top Master B");
                iBitID = 15; m_outValve_BottomMasterA    = new OutputBit(pOPort1, iBitID, "Valve Bottom Master A");
                iBitID = 7; m_outValve_BottomMasterB      = new OutputBit(pOPort1, iBitID, "Valve Bottom Master A");
                iBitID = 8;   m_outLeakTester_Start              = new OutputBit(pOPort2, iBitID, "Leak Tester Start");
                iBitID = 9;   m_outLeakTester_Stop              = new OutputBit(pOPort2, iBitID, "Leak Tester Stop");
                iBitID = 14;   m_outLeakTester_CH0            = new OutputBit(pOPort2, iBitID, "Leak Tester CH0");
                iBitID = 13;   m_outLeakTester_CH1            = new OutputBit(pOPort2, iBitID, "Leak Tester CH1");
                iBitID = 12;   m_outLeakTester_CH2            = new OutputBit(pOPort2, iBitID, "Leak Tester CH2");
                iBitID = 11;   m_outLeakTester_CH3            = new OutputBit(pOPort2, iBitID, "Leak Tester CH3");
                iBitID = 10;   m_outLeakTester_CH4            = new OutputBit(pOPort2, iBitID, "Leak Tester CH4");
                iBitID = 15; m_outTestPressureSelection    = new OutputBit(pOPort2, iBitID, "Valve TestPressure Select");
            }

            return true;
        }

        //Set Nest A or Nest B//////////////////////////////////////////////////////////////////////////////
        //Note: Re-Use this class to work on nest A n B
        public void Set_NestNumber(int iNum)
        {
            m_iNest = iNum;
        }

        //Set Pointers///////////////////////////////////////////////////////////////////////////////////////////
        public void Set_ClassPointers()
        {
            m_DataCommon = null;
            m_DataPackageFile = m_SeqMachineCtrl.m_DataPackageFile;

            if (m_iNest == 1)
            {
                m_DataUnitNest = m_DataUnit.m_DataUnitNest_A;
                m_DataCommon = m_DataPackageFile.m_DataCommon_NestA;

                //String strMsg = m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterA.ToString() + 
                //    m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterB.ToString() +
                //    m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterA.ToString() + 
                //    m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterB.ToString();
                //MessageBox.Show(strMsg);
            }
            else
            {
                m_DataUnitNest = m_DataUnit.m_DataUnitNest_B;
                m_DataCommon = m_DataPackageFile.m_DataCommon_NestB;

                //String strMsg = m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterA.ToString() +
                //    m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterB.ToString() +
                //    m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterA.ToString() +
                //    m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterB.ToString();
                //MessageBox.Show(strMsg);
            }
        }

        //Set Code Scanner pointer////////////////////////////////////////////////////////////////////
        public void Set_CodeScanner(Comm_Serial_KeyenceBarcodeScanner mCodeScanner) { m_CodeScanner = mCodeScanner; }
        //Set Air Tester pointer////////////////////////////////////////////////////////////////////////////
        public void Set_AirTesterPtr(Comm_Serial_AirLeakTester_LSR700 mAirTester) { m_AirLeakTester = mAirTester; }
        //Set Data log pointer/////////////////////////////////////////////////////////////////////////////
        public void Set_DataLogPtr(Log_Data mDataLog) { m_pData_Log = mDataLog; }
        //Set Label//////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Label(String strLabel) { m_strLabel = strLabel; }

        //Logging///////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetLog(String strLog, int iLevel = 1)
        {
            //disable log if level clerance failed
            if (iLevel < m_DataUnit.LogLevel_int) return;

            m_SeqMachineCtrl.SetLog("#### Module Leak Tester: " + strLog);
        }

        //Log Data - Write to log/////////////////////////////////////////////////////////////////////////
        public void Write_ToDataLog()
        {
            String strNest = "";
            String strHeader = "";

            //write to log file
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.PartCount.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.m_str_Barcode);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.m_str_CycleTime);

            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_strResult);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_strTesterID);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_strChannel);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbLeak.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbDET_UL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbDET_LL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbTP_UP.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbTP_LL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbDeltaP.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Top_m_dbTestPressure.ToString());

            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_strResult);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_strTesterID);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_strChannel);
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbLeak.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbDET_UL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbDET_LL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbTP_UP.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbTP_LL.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbDeltaP.ToString());
            m_pData_Log.Set_DataToWrite(m_DataUnitNest.Bottom_m_dbTestPressure.ToString());


            //set header-------------------------------------------------------------------------------------------------
            strHeader += "Part Count , Scan Code , Cycle Time(s) , ";
            strHeader += "Result (Top) , TestID (Top) , Channel (Top) , Leak (Top) , DET UL (Top) , DET LL (Top) , TP UL (Top) , TP LL (Top) , Delta Pressure (Top) , Test Pressure (Top) ,";
            strHeader += "Result (Bottom) , TestID (Bottom) , Channel (Bottom) , Leak (Bottom) , DET UL (Bottom) , DET LL (Bottom) , TP UL (Bottom) , TP LL (Bottom) , Delta Pressure (Bottom) , Test Pressure (Bottom) ";
            m_pData_Log.Set_Header(strHeader);            

            //write to data log-------------------------------------------------------------------------------------------
            if (m_iNest == 1) strNest = "NestA"; else strNest = "NestB";
            m_pData_Log.Write_ToDataLog(strNest);
        }

        //Set to display Data//////////////////////////////////////////////////////////////////////////////
        public void Set_DataUnitVar()
        {
            //Set to data unit variables
            if (m_DataUnitNest.Flag_str_TestPressureValueSeq.ToLower() == "top")
            {
                m_DataUnitNest.Top_m_strResult = m_AirLeakTester.m_struct_Data.m_strResult;
                m_DataUnitNest.Top_m_strTesterID = m_AirLeakTester.m_struct_Data.m_strTesterID;
                m_DataUnitNest.Top_m_strChannel = m_AirLeakTester.m_struct_Data.m_strChannel;
                m_DataUnitNest.Top_m_dbLeak = m_AirLeakTester.m_struct_Data.m_dbLeak;
                m_DataUnitNest.Top_m_dbDET_UL = m_AirLeakTester.m_struct_Data.m_dbDET_UL;
                m_DataUnitNest.Top_m_dbDET_LL = m_AirLeakTester.m_struct_Data.m_dbDET_LL;
                m_DataUnitNest.Top_m_dbTP_UP = m_AirLeakTester.m_struct_Data.m_dbTP_UP;
                m_DataUnitNest.Top_m_dbTP_LL = m_AirLeakTester.m_struct_Data.m_dbTP_LL;
                m_DataUnitNest.Top_m_dbDeltaP = m_AirLeakTester.m_struct_Data.m_dbDeltaP;
                m_DataUnitNest.Top_m_dbTestPressure = m_AirLeakTester.m_struct_Data.m_dbTestPressure;

                m_DataUnitNest.DisplayLeakTop_str = m_DataUnitNest.Top_m_dbLeak.ToString();
            }
            else
            {
                m_DataUnitNest.Bottom_m_strResult = m_AirLeakTester.m_struct_Data.m_strResult;
                m_DataUnitNest.Bottom_m_strTesterID = m_AirLeakTester.m_struct_Data.m_strTesterID;
                m_DataUnitNest.Bottom_m_strChannel = m_AirLeakTester.m_struct_Data.m_strChannel;
                m_DataUnitNest.Bottom_m_dbLeak = m_AirLeakTester.m_struct_Data.m_dbLeak;
                m_DataUnitNest.Bottom_m_dbDET_UL = m_AirLeakTester.m_struct_Data.m_dbDET_UL;
                m_DataUnitNest.Bottom_m_dbDET_LL = m_AirLeakTester.m_struct_Data.m_dbDET_LL;
                m_DataUnitNest.Bottom_m_dbTP_UP = m_AirLeakTester.m_struct_Data.m_dbTP_UP;
                m_DataUnitNest.Bottom_m_dbTP_LL = m_AirLeakTester.m_struct_Data.m_dbTP_LL;
                m_DataUnitNest.Bottom_m_dbDeltaP = m_AirLeakTester.m_struct_Data.m_dbDeltaP;
                m_DataUnitNest.Bottom_m_dbTestPressure = m_AirLeakTester.m_struct_Data.m_dbTestPressure;

                m_DataUnitNest.DisplayLeakBottom_str = m_DataUnitNest.Bottom_m_dbLeak.ToString();
            }
        }

        //Selection Chanel based on package///////////////////////////////////////////////////////
        public void Selection_LeakTesterChannel()
        {
            if (m_DataCommon.m_bFlag_Nest_Ch0) m_outLeakTester_CH0.Write(1); else m_outLeakTester_CH0.Write(0);
            if (m_DataCommon.m_bFlag_Nest_Ch1) m_outLeakTester_CH1.Write(1); else m_outLeakTester_CH1.Write(0);
            if (m_DataCommon.m_bFlag_Nest_Ch2) m_outLeakTester_CH2.Write(1); else m_outLeakTester_CH2.Write(0);
            if (m_DataCommon.m_bFlag_Nest_Ch3) m_outLeakTester_CH3.Write(1); else m_outLeakTester_CH3.Write(0);
            if (m_DataCommon.m_bFlag_Nest_Ch4) m_outLeakTester_CH4.Write(1); else m_outLeakTester_CH4.Write(0); 
        }

        //Selection: Master Part/////////////////////////////////////////////////////////////////////////////////
        public void Selection_MasterPart()
        {
            //Selection - Top Section
            if (m_DataUnitNest.Flag_str_TestPressureValueSeq.ToLower() == "top")
            {
                m_outValve_UUTSelect.Write(0);
                
                //Select Test pressure
                if (m_DataCommon.m_iFlag_TestPressureInputSequence == 0) m_outTestPressureSelection.Write(0);
                else m_outTestPressureSelection.Write(1);
            }
            //Selection - Bottom Section
            else
            {
                m_outValve_UUTSelect.Write(1);
                
                //Select Test pressure
                if (m_DataCommon.m_iFlag_TestPressureInputSequence == 0)  m_outTestPressureSelection.Write(1);
                else m_outTestPressureSelection.Write(0);
            }

            //Select master part
            if (m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterA) m_outValve_TopMasterA.Write(1); else m_outValve_TopMasterA.Write(0);
            if (m_DataCommon.m_bFlag_MasterPartSelect_Top_MasterB) m_outValve_TopMasterB.Write(1); else m_outValve_TopMasterB.Write(0);
            if (m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterA) m_outValve_BottomMasterA.Write(1); else m_outValve_BottomMasterA.Write(0);
            if (m_DataCommon.m_bFlag_MasterPartSelect_Bottom_MasterB) m_outValve_BottomMasterB.Write(1); else m_outValve_BottomMasterB.Write(0); 

        }










        //Initialization////////////////////////////////////////////////////////////////////////////////////////////////
        public void Init_Module()
        {
            Flag_Init = true;                       //signal init start

            //reset Flag
            Flag_InitOK = false;
            m_iFlag_SequencePos = 0;
            m_DataUnitNest.Reset_Flags();
            //reset stop watches
            m_stCycleTime.Reset();
            m_stDelay_AirleakTester.Reset();

            //Reset IO
            m_outValve_UUTSelect.Write(0);
            m_outValve_TopMasterA.Write(0);
            m_outValve_TopMasterB.Write(0);
            m_outValve_BottomMasterA.Write(0);
            m_outValve_BottomMasterB.Write(0);
            m_outTestPressureSelection.Write(0);
            m_outLeakTester_Start.Write(0);
            m_outLeakTester_Stop.Write(0);
            m_outLeakTester_CH0.Write(0);
            m_outLeakTester_CH1.Write(0);
            m_outLeakTester_CH2.Write(0);
            m_outLeakTester_CH3.Write(0);
            m_outLeakTester_CH4.Write(0);

            //reset leak tester unit
            m_outLeakTester_Stop.Write(1);
            Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Stop);
            m_outLeakTester_Stop.Write(0);

            Flag_InitOK = true;                 //signal Init ok

            End:
            Flag_Init = false;                      //signal init end
        }





        //Start Thread / Auto Sequence///////////////////////////////////////////////////////////
        public void Start_Run()
        {
            //Start Run MAIN Thread
            if (Module_Runing == false)
            {
                m_thRun = new Thread(Run);
                m_thRun.Start();
            }
        }
        public void Run()
        {
            //Set variables
            Module_StopRun = false;
            Flag_Error = false;
            Module_Runing = true;

            //Auto sequence loop
            while (true)
            {
                //Auto sequence
                if (this.Auto_Sequence() == false)
                {
                    Flag_Error = true;
                    break;
                }

                //stop module run
                if (Module_StopRun == true )//&& m_DataUnitNest.Flag_LTWorking == false)    //temporary
                {
                    SetLog("Sequence Stopped");
                    Module_StopRun = false;
                    break;
                }
            }

            //end
            Module_Runing = false;
            m_DataUnitNest.Flag_LTWorking = false;
        }
        //Auto Sequence///////////////////////////////////////////////////////////////////////////////////
        public bool Auto_Sequence()
        {
            //disabled
            if (m_DataUnitNest.Flag_NestEnable == false) { Thread.Sleep(1000); return true; }
            
            //sequence
            switch (m_iFlag_SequencePos)
            {
                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStandby:
                    {
                        //reset flags
                        m_DataUnitNest.Flag_PartLoadedNest = false;
                        m_DataUnitNest.Flag_str_TestPressureValueSeq = "Top";

                        //select channel
                        Selection_MasterPart();

                        //select channel
                        Selection_LeakTesterChannel();

                        //reset leak tester unit
                        m_outLeakTester_Stop.Write(1);
                        Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Stop);
                        m_outLeakTester_Stop.Write(0);

                        //Connect to the leak tester
                        if (!m_AirLeakTester.Comm_Connect()) { m_strErrorMessage = m_AirLeakTester.Get_ErrorMessage(); break; }

                        m_DataUnitNest.RequestStateMessage_str = "Await Part. Please load part into the nest.";
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_PressStart;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enWait_PressStart:
                    {
                        if (m_DataUnit.m_DataUnitNest_A.SeqSelection == 1)  //wet run
                        {
                            Thread.Sleep(2000);
                        }
                        else
                        {
                            //wait part loaded
                            if (m_senPartPresent.Read_Sensor() == 0) { Thread.Sleep(100); break; }
                            m_DataUnitNest.Flag_PartLoadedNest = true;
                            m_DataUnitNest.RequestStateMessage_str = "Please press the Start button to being the testing process.";

                            //wait user to push the start button
                            if (m_inPushButton.Read() != 1) { Thread.Sleep(100); break; }
                        }

                        //reset data 
                        m_DataUnitNest.Reset();
                        //Update UI
                        m_DataUnitNest.Flag_GuiUpdateResult = true;
                        //start cycle time
                        m_stCycleTime.Restart();
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enRead_Barcode;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enRead_Barcode:
                    {
                        String strBarcodeID = "";

                        //Scan code (barcode, Datamatrix or QR Code)
                        if (m_CodeScanner.ReadBarcode_PerBank(1, ref strBarcodeID) == false)
                        {
                            m_strErrorMessage = m_CodeScanner.Get_ErrorMessage();
                            return false;
                        }

                        //set to storage for data logging
                        m_DataUnitNest.m_str_Barcode = strBarcodeID;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enTrigger_Valve;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enTrigger_Valve:
                    {
                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module Start";

                        //select channel
                        Selection_MasterPart();

                        //trigger tester via IO
                        m_outLeakTester_Start.Write(1);
                        Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Start);
                        m_outLeakTester_Start.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_LTStart;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enWait_LTStart:
                    {
                        //wait for tester to start
                        if (m_senLeakTester_Busy.Read_Sensor() == 0) { Thread.Sleep(100); break; }
                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module working...";
                        //Set flag to indicate LT is working
                        m_DataUnitNest.Flag_LTWorking = true;
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_LTEnd;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enWait_LTEnd:
                    {
                        //wait for tester to end
                        if (m_senLeakTester_End.Read_Sensor() == 0) { Thread.Sleep(10); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enRead_Tester;
                    break;


                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enRead_Tester:
                    {
                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module done...";

                        //require long delay
                        if (m_stDelay_AirleakTester.IsRunning == false) m_stDelay_AirleakTester.Restart();
                        if (m_stDelay_AirleakTester.ElapsedMilliseconds < m_AirLeakTester.Get_Settings().m_lg_DelayTime) { Thread.Sleep(10); break; }
                        m_stDelay_AirleakTester.Stop();

                        //read from tester
                        if (!m_AirLeakTester.Read_FromModule())
                        {
                            m_outLeakTester_Stop.Write(1);
                            Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Stop);
                            m_outLeakTester_Stop.Write(0);

                            m_strErrorMessage = m_AirLeakTester.Get_ErrorMessage(); return false;
                        }

                        //Set to display data
                        Set_DataUnitVar();

                        //Loop read leak tester with bottom section
                        if (m_DataUnitNest.Flag_str_TestPressureValueSeq.ToLower() == "top")
                        {
                            m_DataUnitNest.Flag_str_TestPressureValueSeq = "Bottom";
                            m_iFlag_SequencePos = (int)m_Sequence.enTrigger_Valve;
                            break;
                        }

                        //disconnect from air leak  tester serial port
                        m_AirLeakTester.Comm_Disconnect();

                        //record cycle time
                        m_DataUnitNest.m_str_CycleTime = (((double)m_stCycleTime.ElapsedMilliseconds) / 1000).ToString();
                        m_stCycleTime.Stop();

                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWriteToDataLog;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                //Set pass or fail and write to data log file
                case (int)m_Sequence.enWriteToDataLog:
                    {
                        //increase part count
                        m_DataUnitNest.PartCount++;
                        m_DataUnit.PartCount_int++;

                        //increment pass of fail count
                        if (m_DataUnitNest.Top_m_strResult.Contains("Pass") == false)
                        {
                            m_DataUnitNest.PartCount_Fail++;
                            m_DataUnit.PartCountFail_int++;
                            //set display message
                            m_DataUnitNest.RequestStateMessage_str = "Part FAILED Top\r\nPlease remove the part from the Nest.";
                        }
                        else if (m_DataUnitNest.Bottom_m_strResult.Contains("Pass") == false)
                        {
                            m_DataUnitNest.PartCount_Fail++;
                            m_DataUnit.PartCountFail_int++;
                            //set display message
                            m_DataUnitNest.RequestStateMessage_str = "Part FAILED Bottom\r\nPlease remove the part from the Nest.";
                        }
                        else
                        {
                            m_DataUnitNest.PartCount_Pass++;
                            m_DataUnit.PartCountPass_int++;
                            //set display message
                            m_DataUnitNest.RequestStateMessage_str = "Please remove the part from the Nest.";
                        }

                        //write to log file
                        Write_ToDataLog();
                        //Update UI
                        m_DataUnitNest.Flag_GuiUpdateResult = true;

                        //Set flag to indicate LT is finish working
                        m_DataUnitNest.Flag_LTWorking = false;

                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_RemovePart;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enWait_RemovePart:
                    {
                        //wait part remove
                        if (m_DataUnit.m_DataUnitNest_A.SeqSelection != 1)  //wet run
                        if (m_senPartPresent.Read_Sensor() == 1) { Thread.Sleep(100); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enEnd;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStart;
                    break;
            }

            return true;
        }


        #region Obsolete
        //Sub Auto Sequence/////////////////////////////////////////////////////////////////////////
        public void Start_RunSub()
        {
            //Start Run MAIN Thread
            if (Module_Runing == false)
            {
                m_thRun = new Thread(Run_Sub);
                m_thRun.Start();
            }
        }
        public void Run_Sub()
        {
            //Set variables
            Module_StopRun = false;
            Flag_Error = false;
            Module_Runing = true;

            //Auto sequence loop
            while (true)
            {
                //Auto sequence
                if (this.Auto_SubSequence_LTTest() == false)
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
        //sub thread for testing purpose only
        public bool Auto_SubSequence_LTTest()
        {
            switch (m_iFlag_SequencePos)
            {
                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStart:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStandby;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enStandby:
                    {
                        //reset flags
                        m_DataUnitNest.Flag_PartLoadedNest = false;

                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module Standby";

                        //reset leak tester unit
                        m_outLeakTester_Stop.Write(1);
                        Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Stop);
                        m_outLeakTester_Stop.Write(0);
                        
                        Thread.Sleep(2000);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enTrigger_Valve;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enTrigger_Valve:
                    {
                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module Start";

                        //select channel
                        Selection_MasterPart();

                        //trigger tester via IO
                        m_outLeakTester_Start.Write(1);
                        Thread.Sleep(m_AirLeakTester.Get_Settings().m_i_DelayTime_Start);
                        m_outLeakTester_Start.Write(0);
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enWait_LTStart;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enWait_LTStart:
                    {
                        //wait for tester to start
                        if (m_senLeakTester_Busy.Read_Sensor() == 0) { Thread.Sleep(100); break; }
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enRead_Tester;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enRead_Tester:
                    {
                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Air Leak Tester module working...";

                        //wait for tester to end
                        if (m_senLeakTester_End.Read_Sensor() == 0) { Thread.Sleep(10); break; }

                        //require long delay
                        if (m_stDelay_AirleakTester.IsRunning == false) m_stDelay_AirleakTester.Restart();
                        if (m_stDelay_AirleakTester.ElapsedMilliseconds < m_AirLeakTester.Get_Settings().m_lg_DelayTime) { Thread.Sleep(100); break; }
                        m_stDelay_AirleakTester.Stop();

                        //read from tester
                        if (m_AirLeakTester.Read_FromModule() == false)
                        {
                            m_outLeakTester_Stop.Write(1);
                            Thread.Sleep(100);
                            m_outLeakTester_Stop.Write(0);

                            m_strErrorMessage = m_AirLeakTester.Get_ErrorMessage(); return false;
                        }

                        //Set to display data
                        Set_DataUnitVar();
                        
                        //write to log file
                        Write_ToDataLog();

                        //Update UI
                        m_DataUnitNest.Flag_GuiUpdateResult = true;

                        //set display message
                        m_DataUnitNest.RequestStateMessage_str = "Please remove the part from the Nest.";
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enPreEnd;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enPreEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enEnd;
                    break;

                //----------------------------------------------------------------------------------------------------------------------------------------------
                case (int)m_Sequence.enEnd:
                    {
                    }
                    m_iFlag_SequencePos = (int)m_Sequence.enStart;
                    break;
            }

            return true;
        }
        #endregion



        //Sequence////////////////////////////////////////////////////////////////////////////////////////////
        public enum m_Sequence
        {
            enStart,
            enStandby,

            //Auto Sequence
            enWait_PressStart,
            enRead_Barcode,
            enTrigger_Valve,
            enWait_LTStart,
            enWait_LTEnd,
            enRead_Tester,
            enWriteToDataLog,
            enWait_RemovePart,

            enPreEnd,
            enEnd
        }


        //Error Message Report///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = "####Module Laser Marker " + m_strLabel + ": " + m_strErrorMessage;
            m_strErrorMessage = "";

            return strError;
        }

    }
}
