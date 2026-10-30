/****************************************************************************************************************************************
 * Sequence Machine Control class, Main Control class for the machine
 * Desc:    This class is the core of the entire program. It sets the corresponding hardware to the corresponding motion and IO classes.
 *              It also sets the log files and data log and it is the main link to all lib classes.
 *              
 * Notes:
 *              Variables will be created first, then only the constructor. 
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace StaticCoreLib
{
    public class SeqMachineControl
    {
        //Variables
        //Classes / objects / modules
        public MonitorInput							m_MonitorInput                  = null;
        public Module_ControlPanel					m_Module_ControlPanel           = null;
        public Module_FwdConv                       m_Module_FwdConv_OP85_OP90      = null;
        public Module_OP100_Buffer		            m_Module_OP100_Buffer           = null;
		public Module_OP100_Station		            m_Module_OP100_Station          = null;
        public Module_FwdConv                       m_Module_FwdConv_OP100          = null;
		public Module_OP110_HotBlower				m_Module_OP110_HotBlower        = null;
        public Module_FwdConv                       m_Module_FwdConv_OP110          = null;
		
        public InterfaceCards						m_InterfaceCards                    = null;
        public MachineInfo							m_MachineInfo                       = new MachineInfo();
        public VersionInfo                          m_VersionInfo                       = new VersionInfo();
        public Password								m_Password                          = null;
        public Watch_Timer							m_WatchTimer                        = null;
        public Data_PackageFile						m_DataPackageFile                   = new Data_PackageFile();
        public Data_UnitTempStorage				    m_DataUnit                          = new Data_UnitTempStorage();
		public Data_ErrorMessage					m_DataErrorMessage                  = new Data_ErrorMessage();
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP60     = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP70     = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP80     = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP85     = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP90     = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP100    = null;
        public Data_PerStation_DownTime             m_Data_PerStation_DownTime_OP110    = null;

        public Log_Data								m_LogFile                   = new Log_Data();
        public Log_Data								m_pData_Log_OP85            = new Log_Data();
        public Log_Data								m_pData_Log_OP90            = new Log_Data();
        public Log_Data								m_pData_Log_OP110           = new Log_Data();

		public RobotXYZ_APSlib_PanasonicECAT        m_robotStationSolder01	    = null;
		public RobotXYZ_APSlib_PanasonicECAT        m_robotStationSolder02	    = null;
		public RobotXYZ_APSlib_PanasonicECAT        m_robotStationSolder03	    = null;
        public RobotXY_APSlib_PanasonicECAT         m_robotStationHotBlower     = null;
        public Comm_Serial_KeyenceIL                m_CommSerial_KeyenceIL      = null;
        //public CommSerial_WireTester_Hioki          m_ComSerialWireTester       = null;
		public Comm_Serial_Thermocouple             m_CommSerialThermocouple    = null;
        public Comm_TCP_RFID                        m_RFIDReader_OP60_OP85      = null;
		public Comm_TCP_RFID                        m_RFIDReader_OP90_OP110     = null;
		public Vision_Cognex                        m_Camera1                   = null;
		public Vision_Cognex                        m_Camera2                   = null;
        public Vision_Keyence                       m_Vision_Keyence            = null;
		public Server_COMTCP_GT_CentralPC           m_Server                    = null;	//primary server class. the module server classes are clones and thus its settings must be set with this primary class

		//Class expose to UI
		public ViewModel_Interlock                  m_ViewModel_Interlock       = null;
		public ViewModel_GotoFunctions              m_ViewModel_GotoFunctions   = null;
		public ViewModel_Vision                     m_ViewModel_Vision          = null;
        public ViewModel_Vision_Keyence             m_ViewModel_Vision_Keyence  = null;
        public ViewModel_HotBlower                  m_ViewModel_HotBlower       = null;
		public MessageBox_Triggers                  m_MessageBoxTrigger         = new MessageBox_Triggers();
        public SupportFunctions                     m_SupportFunctions          = null;

		//Flags
        //Flags: Locks
		#region Thread_Flags
        private readonly object m_lock_RunningInit = new object();
        private readonly object m_lock_InitOK = new object();
        private readonly object m_lock_Run = new object();
        private readonly object m_lock_RunEnd = new object();
        private readonly object m_lock_MachineState = new object();

        //Flags: Flags expose
        public bool Flag_RunningInit
        {
            set { lock (m_lock_RunningInit) { m_bFlag_RunningInit = value; } }
            get { lock (m_lock_RunningInit) { return m_bFlag_RunningInit; } }
        }                                                    //Flag: Init is in progress
        public bool Flag_InitOK
        {
            set { lock (m_lock_InitOK) { m_bInitOK = value; } }
            get { lock (m_lock_InitOK) { return m_bInitOK; } }
        }                                                   //Flag: Init Done and passs or fail
        public bool Flag_Run
        {
            set { lock (m_lock_Run) { m_bFlag_Run = value; } }
            get { lock (m_lock_Run) { return m_bFlag_Run; } }
        }                                                   //Flag: Process run
        public bool Flag_RunEnd
        {
            set { lock (m_lock_RunEnd) { m_bFlag_RunEnd = value; } }
            get { lock (m_lock_RunEnd) { return m_bFlag_RunEnd; } }
        }                                                   //Flag: Process stop

        //Flags: Variables
        bool m_bInitOK = false;
        bool m_bFlag_RunningInit = false;
        bool m_bFlag_Run = false;
        bool m_bFlag_RunEnd = false;
		#endregion

        //others
        Thread m_thInit     = null;
        Thread m_thMainRun  = null;
        public List<String> m_strPackageList = new List<string>();

        //variables
        String strError = "";

        //Constructor//////////////////////////////////////////////////////////////////////////////////////////////////////
        /*Note:
         *              Create all the relevant modules here.
         */
        public SeqMachineControl()
        {
			//Interface card class---------------------------------------------------------------------------------
			m_InterfaceCards = new InterfaceCards();

            //Set Machine Info-------------------------------------------------------------------------------------
            m_MachineInfo = XMLSerializer.Deserialize_Func<MachineInfo>(m_MachineInfo.Get_FullFilePath());
            Global_Var.m_bOfflineTest = m_MachineInfo.m_bOfflineTest;

            //Set Package file location----------------------------------------------------------------------------
            if (m_MachineInfo.m_strPackageFileName != "") Pkg_From_Registry(m_MachineInfo.m_strPackageFileName);

            //create and load Data_PerStation_DownTime class
            m_Data_PerStation_DownTime_OP60 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP60", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP70 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP70", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP80 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP80", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP85 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP85", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP90 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP90", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP100 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP100", m_MachineInfo.Get_MachineFilePath());
            m_Data_PerStation_DownTime_OP110 = new Data_PerStation_DownTime("Data_PerStation_DownTime_OP110", m_MachineInfo.Get_MachineFilePath());
            
            //Set password class------------------------------------------------------------------------------------
            m_Password                      = new Password(m_MachineInfo.m_strPackageFilePath);

			//Set Data Unit / Data Store --------------------------------------------------------------------------
			m_DataUnit                      = new Data_UnitTempStorage();

			//Set Data Error Message -----------------------------------------------------------------------------
			m_DataErrorMessage              = new Data_ErrorMessage(m_MachineInfo.Get_MachineFilePath());

			//Create RFID reader--------------------------------------------------------------------------------------
            m_RFIDReader_OP60_OP85      = new Comm_TCP_RFID(m_MachineInfo.Get_MachineFilePath(), "RFID_OP60_OP85_Station");
			m_RFIDReader_OP90_OP110		= new Comm_TCP_RFID(m_MachineInfo.Get_MachineFilePath(), "RFID_OP90_OP110_Station");

			//Create Hioki Wire Tester--------------------------------------------------------------------------------
            //m_ComSerialWireTester           = new CommSerial_WireTester_Hioki("Wire_Tester", m_MachineInfo.Get_MachineFilePath(), this);

            //Create Thermocouple--------------------------------------------------------------------------------
            m_CommSerialThermocouple        = new Comm_Serial_Thermocouple("Thermocouple", m_MachineInfo.Get_MachineFilePath());

            //Create Comm IL class---------------------------------------------------------------
            m_CommSerial_KeyenceIL          = new Comm_Serial_KeyenceIL("Keyence IL", m_MachineInfo.Get_MachineFilePath());
            
			//Vision ---------------------------------------------------------------------------------------------------------
			m_Camera1                       = new Vision_Cognex("Camera_1", m_MachineInfo.Get_MachineFilePath());
			m_Camera2                       = new Vision_Cognex("Camera_2", m_MachineInfo.Get_MachineFilePath());
            m_Vision_Keyence                = new Vision_Keyence("Keyence_Vision", m_MachineInfo.Get_MachineFilePath());

            //Support Functions---------------------------------------------------------------------------------------------------------
            m_SupportFunctions = new SupportFunctions();

			//Server----------------------------------------------------------------------------------------------------------
			m_Server                        = new Server_COMTCP_GT_CentralPC(new Comm_TCP(), "Server", m_MachineInfo.Get_MachineFilePath());
        }

        //Create the relevant input and outputs and stepper motor////////////////////////////////////////////////////////////////
        /// <summary>
        /// This function is called at app start up @ Init function, main app class. Sets all modules and establist IO ports at mods. Read from the pre set pkg file also.
        /// </summary>
        /// <returns></returns>
        public bool Start_Up()
        {
            //Init the interface cards
            if (m_InterfaceCards.Init_InterfaceCards() == false) return false;

            //SolderStation 1 (first) robot
            m_robotStationSolder01 = new RobotXYZ_APSlib_PanasonicECAT(m_InterfaceCards.m_APS168_Ethercat_PCIcard,
                                                                       "Gantry_01_OP60", m_MachineInfo.Get_MachineFilePath(),
                                                                       0, 1, 2);
            //SolderStation 2 (2nd) robot
            m_robotStationSolder02 = new RobotXYZ_APSlib_PanasonicECAT(m_InterfaceCards.m_APS168_Ethercat_PCIcard,
                                                                       "Gantry_02_OP70", m_MachineInfo.Get_MachineFilePath(),
                                                                       3, 4, 5);
            //SolderStation 3 (last) robot
            m_robotStationSolder03 = new RobotXYZ_APSlib_PanasonicECAT(m_InterfaceCards.m_APS168_Ethercat_PCIcard,
                                                                       "Gantry_03_OP80", m_MachineInfo.Get_MachineFilePath(),
                                                                       6, 7, 8);

            //hot blower robot
            m_robotStationHotBlower = new RobotXY_APSlib_PanasonicECAT(m_InterfaceCards.m_APS168_Ethercat_PCIcard,
                                                                       "Gantry_04_OP110", m_MachineInfo.Get_MachineFilePath(),
                                                                       9, 10);

            //Update variables 
            Get_DataStartUp();

            //control panel
            m_Module_ControlPanel = new Module_ControlPanel(this);
            if (!m_Module_ControlPanel.Create_InputsOutputs(m_InterfaceCards))
            {
                MessageBox.Show("Error: Unable to create " + m_Module_ControlPanel.m_strLabel + " IO.");
                return false;
            }

            //Monitor Input-------------------------------------------------------------------------------------------------
            //note: please put this after module control panel because monitor class utilize control panel pointer
            m_MonitorInput = new MonitorInput(this);

            //Module Creation
            m_Module_FwdConv_OP85_OP90 = new Module_FwdConv(this, 85);
            if (m_Module_FwdConv_OP85_OP90.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            m_Module_OP100_Buffer = new Module_OP100_Buffer(this);
            if (m_Module_OP100_Buffer.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            m_Module_OP100_Station = new Module_OP100_Station(this);
            if (m_Module_OP100_Station.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            m_Module_FwdConv_OP100 = new Module_FwdConv(this, 100);
            if (m_Module_FwdConv_OP100.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            m_Module_OP110_HotBlower = new Module_OP110_HotBlower(this);
            if (m_Module_OP110_HotBlower.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            m_Module_FwdConv_OP110 = new Module_FwdConv(this, 110);
            if (m_Module_FwdConv_OP110.Create_InputsOutputs(m_InterfaceCards) == false) return false;

            //Create view model------------------------------------------------------------------------------------------
            //Note Must be placed here because these classes require the module worker classes
            m_ViewModel_Interlock = new ViewModel_Interlock(this);
            m_ViewModel_GotoFunctions = new ViewModel_GotoFunctions(this);
            m_ViewModel_Vision = new ViewModel_Vision(this);
            m_ViewModel_Vision_Keyence = new ViewModel_Vision_Keyence(this);
            m_ViewModel_HotBlower = new ViewModel_HotBlower(this);

            //Watch Timer-------------------------------------------------------------------------------------------------
            m_WatchTimer = new Watch_Timer(this);

            //Setup Log files here-----------------------------------------------------------------------------------
            Setup_LogFiles();

            //others----------------------------------------------------------------------------------------------------------
            SetLog("*** START OF APPLICATION *** " + m_VersionInfo.GetVersion());
            m_DataUnit.MachineState_Str = "Idle";

            //Turn on Maintenance light: Request Kamarul
            m_Module_ControlPanel.m_pOutMaintenanceLight.Write(1);

            return true;
        }

        //Create Data log and Log Files////////////////////////////////////////////////////////////////////////////////////////
        public void Setup_LogFiles()
        {
            //Create Log File class. use  default extension (.txt)
            m_LogFile.SetFolderName("Machine_Sequence_Folder");
            m_LogFile.SetFileName("LogFile");
            m_LogFile.SeqLog_StartThread();

            //Create data record class
            m_pData_Log_OP85.SetFolderName("Data_Log_Folder_OP85");
            m_pData_Log_OP85.SetFileName("Data");
            m_pData_Log_OP85.SetExtensionName(".csv");

            m_pData_Log_OP90.SetFolderName("Data_Log_Folder_OP90");
            m_pData_Log_OP90.SetFileName("Data");
            m_pData_Log_OP90.SetExtensionName(".csv");

            m_pData_Log_OP110.SetFolderName("Data_Log_Folder_OP110");
            m_pData_Log_OP110.SetFileName("Data");
            m_pData_Log_OP110.SetExtensionName(".csv");
        }

        //Load Data Files///////////////////////////////////////////////////////////////////////////////////////////////////
        public void Pkg_From_Registry(String strFileName)
        {
			Data_PackageFile mpDataPackage = new Data_PackageFile();

            m_MachineInfo.m_strPackageFileName = strFileName;
			mpDataPackage = XMLSerializer.Deserialize_Func<Data_PackageFile>(m_MachineInfo.Get_FullPath_PackageFile());

			//Update to package file
			m_DataPackageFile.Update_Settings(mpDataPackage);
        }

        //Save Data Files///////////////////////////////////////////////////////////////////////////////////////////////////
        public void Pkg_To_Registry() { XMLSerializer.Serialize_Func<Data_PackageFile>(m_DataPackageFile, m_MachineInfo.Get_FullPath_PackageFile()); }
        public void MachineInfo_To_Registry() { XMLSerializer.Serialize_Func<MachineInfo>(m_MachineInfo, m_MachineInfo.Get_FullFilePath()); }

		//Get Data packages///////////////////////////////////////////////////////////////////////////////
		//Note: Use this to get the list of packages in the package folder. For display purpose and obtain filenames only
		public int Get_PackageList(ref String strError)
		{
			//check file path string
			if (m_MachineInfo.m_strPackageFilePath == "")
			{
				strError = "File Path string is empty";
				return 0;
			}

			//Check directory exist
			if (Directory.Exists(m_MachineInfo.m_strPackageFilePath) == false)
			{
				strError = "Directory does not exist.\r\nFile Path = " + m_MachineInfo.m_strPackageFilePath;
				return 0;
			}

			//Clear package list array
			if (m_strPackageList.Count != 0) m_strPackageList.Clear();

			//populate list
			String[] strDir = Directory.GetFiles(m_MachineInfo.m_strPackageFilePath);

			foreach (string strFile in strDir)
			{
				if (strFile.EndsWith(m_MachineInfo.m_strPackageFileExt))
					m_strPackageList.Add(strFile.Substring(strFile.LastIndexOf("\\") + 1));
			}

			return 1;
		}

        //Clean Up/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            SetLog("*** END OF APPLICATION ***");

            Thread.Sleep(100);

            m_LogFile.Clean_Up();
            m_pData_Log_OP85.Clean_Up();
            m_pData_Log_OP90.Clean_Up();
            m_pData_Log_OP110.Clean_Up();

            m_Module_ControlPanel.TurnOff_DoorLock();
            
			//clean up modules
			m_Module_OP100_Buffer.Clean_Up();
            m_Module_OP100_Station.Clean_Up();
            m_Module_OP110_HotBlower.Clean_Up();
            
            //disconnect all devices
            m_CommSerial_KeyenceIL.Disconnect();
            //m_ComSerialWireTester.Disconnect();
            m_CommSerialThermocouple.Disconnect();
            m_RFIDReader_OP60_OP85.Disconnect();
            m_RFIDReader_OP90_OP110.Disconnect();
            m_Camera1.Disconnect();
            m_Camera2.Disconnect();
            m_Vision_Keyence.Disconnect();

            //update variables
			m_MachineInfo.m_iPartCount = m_DataUnit.PartCount_int;
			m_MachineInfo.m_iPartCount_Pass = m_DataUnit.PartCountPass_int;
			m_MachineInfo.m_iPartCount_Fail = m_DataUnit.PartCountFail_int;
			m_MachineInfo.LogLevel_int = m_DataUnit.LogLevel_int;

			m_MachineInfo.m_bEnableMod_SolderStation1			= m_DataUnit.Flag_EnableSolderStation1_b;					
			m_MachineInfo.m_bEnableMod_SolderStation2			= m_DataUnit.Flag_EnableSolderStation2_b;				
			m_MachineInfo.m_bEnableMod_SolderStation3			= m_DataUnit.Flag_EnableSolderStation3_b;
            m_MachineInfo.m_bEnableMod_ConvSolderStation		= m_DataUnit.Flag_EnableConvSolder_b;
			
            m_MachineInfo.m_bEnable_Module_OP85			        = m_DataUnit.Flag_Enable_Module_OP85_b;
            m_MachineInfo.m_bEnable_Module_Conv_OP85	        = m_DataUnit.Flag_Enable_Module_Conv_OP85_b;
			m_MachineInfo.m_bEnable_Module_OP90			        = m_DataUnit.Flag_Enable_Module_OP90_b;
            m_MachineInfo.m_bEnable_Module_Conv_OP90            = m_DataUnit.Flag_Enable_Module_Conv_OP90_b;
            m_MachineInfo.m_bEnable_Module_FwdConv_OP85_OP90    = m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b;

			m_MachineInfo.m_bEnableMod_ManualOP100				= m_DataUnit.Flag_EnableManualOP100_b;

            m_MachineInfo.m_bEnable_Module_OP110				= m_DataUnit.Flag_Enable_Module_OP110_b;
            m_MachineInfo.m_bEnable_Module_Conv_OP110           = m_DataUnit.Flag_Enable_Module_Conv_OP110_b;
            m_MachineInfo.m_bEnable_Module_FwdConv_OP110        = m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b;

			m_MachineInfo.m_iMachineTypeNum						= m_DataUnit.MachineTypeNum_i;     

            m_MachineInfo.m_bFlagEnable_ReverseSeq              = m_DataUnit.Flag_EnableReverseSeq_b;
            m_MachineInfo.m_bFlagEnable_CleanSolderHead         = m_DataUnit.Flag_EnableCleanSolderHead;

            //enable or disable grades
            m_MachineInfo.m_bEnable_HTR_Grade_A_LV = m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_B_LV = m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_C_LV = m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_D_LV = m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_E_LV = m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b;

            m_MachineInfo.m_bEnable_RTD_Grade_A_LV = m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_B_LV = m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_C_LV = m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_D_LV = m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_E_LV = m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b;

            m_MachineInfo.m_bEnable_HTR_Grade_A_HV = m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_B_HV = m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_C_HV = m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_D_HV = m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b;
            m_MachineInfo.m_bEnable_HTR_Grade_E_HV = m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b;

            m_MachineInfo.m_bEnable_RTD_Grade_A_HV = m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_B_HV = m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_C_HV = m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_D_HV = m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b;
            m_MachineInfo.m_bEnable_RTD_Grade_E_HV = m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b;

            //update registry settings and counters
            MachineInfo_To_Registry();
            Pkg_To_Registry();
        }

        //Get Data and or counters when start up////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Get_DataStartUp()
        {
            m_DataUnit.PartCount_int                            = m_MachineInfo.m_iPartCount;
            m_DataUnit.PartCountPass_int                        = m_MachineInfo.m_iPartCount_Pass;
            m_DataUnit.PartCountFail_int                        = m_MachineInfo.m_iPartCount_Fail;
			m_DataUnit.LogLevel_int                             = m_MachineInfo.LogLevel_int;
			
			m_DataUnit.Flag_EnableSolderStation1_b				= m_MachineInfo.m_bEnableMod_SolderStation1;
			m_DataUnit.Flag_EnableSolderStation2_b				= m_MachineInfo.m_bEnableMod_SolderStation2;
			m_DataUnit.Flag_EnableSolderStation3_b				= m_MachineInfo.m_bEnableMod_SolderStation3;
            m_DataUnit.Flag_EnableConvSolder_b					= m_MachineInfo.m_bEnableMod_ConvSolderStation;

            m_DataUnit.Flag_Enable_Module_OP85_b			    = m_MachineInfo.m_bEnable_Module_OP85;
            m_DataUnit.Flag_Enable_Module_Conv_OP85_b	        = m_MachineInfo.m_bEnable_Module_Conv_OP85;
			m_DataUnit.Flag_Enable_Module_OP90_b			    = m_MachineInfo.m_bEnable_Module_OP90;
            m_DataUnit.Flag_Enable_Module_Conv_OP90_b	        = m_MachineInfo.m_bEnable_Module_Conv_OP90;
            m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b	= m_MachineInfo.m_bEnable_Module_FwdConv_OP85_OP90;

			m_DataUnit.Flag_EnableManualOP100_b				    = m_MachineInfo.m_bEnableMod_ManualOP100;

			m_DataUnit.Flag_Enable_Module_OP110_b			    = m_MachineInfo.m_bEnable_Module_OP110;
            m_DataUnit.Flag_Enable_Module_Conv_OP110_b	        = m_MachineInfo.m_bEnable_Module_Conv_OP110;
            m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b	    = m_MachineInfo.m_bEnable_Module_FwdConv_OP110;

			m_DataUnit.MachineTypeNum_i							= m_MachineInfo.m_iMachineTypeNum;      

            m_DataUnit.Flag_EnableReverseSeq_b                  = m_MachineInfo.m_bFlagEnable_ReverseSeq;
            m_DataUnit.Flag_EnableCleanSolderHead               = m_MachineInfo.m_bFlagEnable_CleanSolderHead;

            //enable or disable grades
            m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b = m_MachineInfo.m_bEnable_HTR_Grade_A_LV;
            m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b = m_MachineInfo.m_bEnable_HTR_Grade_B_LV;
            m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b = m_MachineInfo.m_bEnable_HTR_Grade_C_LV;
            m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b = m_MachineInfo.m_bEnable_HTR_Grade_D_LV;
            m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b = m_MachineInfo.m_bEnable_HTR_Grade_E_LV;

            m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b = m_MachineInfo.m_bEnable_RTD_Grade_A_LV;
            m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b = m_MachineInfo.m_bEnable_RTD_Grade_B_LV;
            m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b = m_MachineInfo.m_bEnable_RTD_Grade_C_LV;
            m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b = m_MachineInfo.m_bEnable_RTD_Grade_D_LV;
            m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b = m_MachineInfo.m_bEnable_RTD_Grade_E_LV;

            m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b = m_MachineInfo.m_bEnable_HTR_Grade_A_HV;
            m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b = m_MachineInfo.m_bEnable_HTR_Grade_B_HV;
            m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b = m_MachineInfo.m_bEnable_HTR_Grade_C_HV;
            m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b = m_MachineInfo.m_bEnable_HTR_Grade_D_HV;
            m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b = m_MachineInfo.m_bEnable_HTR_Grade_E_HV;

            m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b = m_MachineInfo.m_bEnable_RTD_Grade_A_HV;
            m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b = m_MachineInfo.m_bEnable_RTD_Grade_B_HV;
            m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b = m_MachineInfo.m_bEnable_RTD_Grade_C_HV;
            m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b = m_MachineInfo.m_bEnable_RTD_Grade_D_HV;
            m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b = m_MachineInfo.m_bEnable_RTD_Grade_E_HV;
        }

        //Delay//////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Delay_ms(int iDelay_ms)
        {
            ManualResetEvent mEvent = new ManualResetEvent(false);
            mEvent.WaitOne(iDelay_ms);
            mEvent.Dispose();
        }

		//Log Sequence/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void SetLog(String strLog) { m_LogFile.Write_ToSeqLog(strLog); }

        public void Write_ToDataLog_OP85()
        {
            String strHeader = "";
            String strHeader2 = "";
            String mDT = DateTime.Now.ToString("HH:mm:ss");

            m_pData_Log_OP85.Set_DataToWrite(mDT);                                              strHeader += "Time,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.Cycle_Time_OP85_str);                   strHeader += "Cycle Time,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.RFID_OP85_SN_str); strHeader += "SN,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_WorkOrderNumber_str); strHeader += "WorkOrderNumber,";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Overall_Result_str); strHeader += "Overall Result,";

            //width
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Width_Min_str);    strHeader += "Terminal 1 Width - R0 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Width_str);        strHeader += "Terminal 1 Width - R0,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Width_Max_str);    strHeader += "Terminal 1 Width - R0 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Width_Result_str); strHeader += "Terminal 1 Width - R0 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Width_Min_str);    strHeader += "Terminal 2 Width - N3 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Width_str);        strHeader += "Terminal 2 Width - N3,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Width_Max_str);    strHeader += "Terminal 2 Width - N3 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Width_Result_str); strHeader += "Terminal 2 Width - N3 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Width_Min_str);    strHeader += "Terminal 3 Width - R1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Width_str);        strHeader += "Terminal 3 Width - R1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Width_Max_str);    strHeader += "Terminal 3 Width - R1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Width_Result_str); strHeader += "Terminal 3 Width - R1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Width_Min_str);    strHeader += "Terminal 4 Width - N2 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Width_str);        strHeader += "Terminal 4 Width - N2,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Width_Max_str);    strHeader += "Terminal 4 Width - N2 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Width_Result_str); strHeader += "Terminal 4 Width - N2 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Width_Min_str);    strHeader += "Terminal 5 Width - N1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Width_str);        strHeader += "Terminal 5 Width - N1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Width_Max_str);    strHeader += "Terminal 5 Width - N1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Width_Result_str); strHeader += "Terminal 5 Width - N1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Width_Min_str);    strHeader += "Terminal 6 Width - L (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Width_str);        strHeader += "Terminal 6 Width - L,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Width_Max_str);    strHeader += "Terminal 6 Width - L (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Width_Result_str); strHeader += "Terminal 6 Width - L (Result),";

            //length
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Length_Min_str);    strHeader += "Terminal 1 Length - R0 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Length_str);        strHeader += "Terminal 1 Length - R0,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Length_Max_str);    strHeader += "Terminal 1 Length - R0 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Length_Result_str); strHeader += "Terminal 1 Length - R0 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Length_Min_str);    strHeader += "Terminal 2 Length - N3 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Length_str);        strHeader += "Terminal 2 Length - N3,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Length_Max_str);    strHeader += "Terminal 2 Length - N3 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Length_Result_str); strHeader += "Terminal 2 Length - N3 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Length_Min_str);    strHeader += "Terminal 3 Length - R1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Length_str);        strHeader += "Terminal 3 Length - R1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Length_Max_str);    strHeader += "Terminal 3 Length - R1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Length_Result_str); strHeader += "Terminal 3 Length - R1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Length_Min_str);    strHeader += "Terminal 4 Length - N2 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Length_str);        strHeader += "Terminal 4 Length - N2,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Length_Max_str);    strHeader += "Terminal 4 Length - N2 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Length_Result_str); strHeader += "Terminal 4 Length - N2 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Length_Min_str);    strHeader += "Terminal 5 Length - N1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Length_str);        strHeader += "Terminal 5 Length - N1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Length_Max_str);    strHeader += "Terminal 5 Length - N1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Length_Result_str); strHeader += "Terminal 5 Length - N1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Length_Min_str);    strHeader += "Terminal 6 Length - L (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Length_str);        strHeader += "Terminal 6 Length - L,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Length_Max_str);    strHeader += "Terminal 6 Length - L (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Length_Result_str); strHeader += "Terminal 6 Length - L (Result),";

            //distance
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Distance_Min_str);    strHeader += "Terminal 1 Distance - R0 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Distance_str);        strHeader += "Terminal 1 Distance - R0,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Distance_Max_str);    strHeader += "Terminal 1 Distance - R0 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal1_Distance_Result_str); strHeader += "Terminal 1 Distance - R0 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Distance_Min_str);    strHeader += "Terminal 2 Distance - N3 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Distance_str);        strHeader += "Terminal 2 Distance - N3,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Distance_Max_str);    strHeader += "Terminal 2 Distance - N3 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal2_Distance_Result_str); strHeader += "Terminal 2 Distance - N3 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Distance_Min_str);    strHeader += "Terminal 3 Distance - R1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Distance_str);        strHeader += "Terminal 3 Distance - R1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Distance_Max_str);    strHeader += "Terminal 3 Distance - R1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal3_Distance_Result_str); strHeader += "Terminal 3 Distance - R1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Distance_Min_str);    strHeader += "Terminal 4 Distance - N2 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Distance_str);        strHeader += "Terminal 4 Distance - N2,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Distance_Max_str);    strHeader += "Terminal 4 Distance - N2 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal4_Distance_Result_str); strHeader += "Terminal 4 Distance - N2 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Distance_Min_str);    strHeader += "Terminal 5 Distance - N1 (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Distance_str);        strHeader += "Terminal 5 Distance - N1,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Distance_Max_str);    strHeader += "Terminal 5 Distance - N1 (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal5_Distance_Result_str); strHeader += "Terminal 5 Distance - N1 (Result),";

            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Distance_Min_str);    strHeader += "Terminal 6 Distance - L (Min),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Distance_str);        strHeader += "Terminal 6 Distance - L,";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Distance_Max_str);    strHeader += "Terminal 6 Distance - L (Max),";
            m_pData_Log_OP85.Set_DataToWrite(m_DataUnit.OP85_Vision_Terminal6_Distance_Result_str); strHeader += "Terminal 6 Distance - L (Result),";

            //set header-----------------------------------------------------------------------------------------
            m_pData_Log_OP85.Set_Header(strHeader);

            //Write to data log file------------------------------------------------------------------------------
            m_pData_Log_OP85.Write_ToDataLog("Solder_Measurement");
        }

		//Write to data log//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Write_ToDataLog_OP90()
		{	
			String strHeader = "";
            String strHeader2 = "";
            String mDT = DateTime.Now.ToString("HH:mm:ss");

            m_pData_Log_OP90.Set_DataToWrite(mDT);                                                 strHeader += "Time,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.Cycle_Time_OP90_str);                      strHeader += "Cycle Time,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.RFID_OP90_SN_str); strHeader += "SN,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WorkOrderNumber_str); strHeader += "WorkOrderNumber,";

            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_LN1_str);                   strHeader += "L-N1,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_LN2_str);                   strHeader += "L-N2,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_LN3_str);                   strHeader += "L-N3,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_LR0_str);                   strHeader += "L-R0,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_R0R1_str);                  strHeader += "R0-R1,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_Temperature_db.ToString()); strHeader += "Temperature,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_LN_Average_str);            strHeader += "LN Average,";

            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_HTR_Min_str);               strHeader += "HTR Min.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_HTR_Avg_str);               strHeader += "HTR Avg.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_HTR_Max_str);               strHeader += "HTR Max.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_HTR_Result_str);            strHeader += "HTR Result,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_HTR_Grade_str);             strHeader += "HTR Grade,";

            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_RTD_Min_str);               strHeader += "RTD Min.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_RTD_Avg_str);               strHeader += "RTD Avg.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_RTD_Max_str);               strHeader += "RTD Max.,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_RTD_Result_str);            strHeader += "RTD Result,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_RTD_Grade_str);             strHeader += "RTD Grade,";

            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_Overall_Result_str);        strHeader += "Overall Result,";
            //m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_WT_Result_Overall_Grade_str);         strHeader += "Overall Grade,";

            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire1_str);              strHeader += "Vision Wire 1,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire2_str);              strHeader += "Vision Wire 2,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire3_str);              strHeader += "Vision Wire 3,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire4_str);              strHeader += "Vision Wire 4,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire5_str);              strHeader += "Vision Wire 5,";
            m_pData_Log_OP90.Set_DataToWrite(m_DataUnit.OP90_VisionResult_Wire6_str);              strHeader += "Vision Wire 6,";

            //set header-------------------------------------------------------------------------------------------------
            m_pData_Log_OP90.Set_Header(strHeader);

			//Write to data log file------------------------------------------------------------------------------
			m_pData_Log_OP90.Write_ToDataLog("Vision");
		}

        public void Write_ToDataLog_OP110()
        {
            String strHeader = "";
            String mDT = DateTime.Now.ToString("HH:mm:ss");

            //write to log file
            m_pData_Log_OP110.Set_DataToWrite(mDT); strHeader += "Time,";
            m_pData_Log_OP110.Set_DataToWrite(m_DataUnit.Cycle_Time_OP110_str.ToString()); strHeader += "Cycle Time,";
            m_pData_Log_OP110.Set_DataToWrite(m_DataUnit.OP110_WorkOrderNumber_str); strHeader += "WorkOrderNumber,";

            //set header-------------------------------------------------------------------------------------------------
            m_pData_Log_OP110.Set_Header(strHeader);

            //Write to data log file------------------------------------------------------------------------------
            m_pData_Log_OP110.Write_ToDataLog("CycleTime");
        }

		//Get Error Messages///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public String Get_ErrorMessage(int iErrorID, String strErrorMsg_Add = "", String strErrorMsg_Front = "")
		{
			ErrorMessage_Info error_info = new ErrorMessage_Info();
			String strError_FullString = "";
			short iRet = 1;
			
			//get and set error struct
			error_info = m_DataErrorMessage.Get_ErrorInfos(iErrorID, ref iRet);
			if (iRet != 0)
            {
				error_info.m_strErrorMsg += "\r\n" + strErrorMsg_Add;
			}
			else 
            {
				error_info.m_strErrorMsg += "unknown error.....";
				error_info.m_strRecoveryMsg += "...";
			}

			//Add new line at the front
			if ( strErrorMsg_Front != "" ) strErrorMsg_Front += "\r\n";

			//Set error string
			strError_FullString = strErrorMsg_Front;
			strError_FullString += error_info.m_strErrorMsg + "\r\n";
			strError_FullString += error_info.m_strRecoveryMsg + "\r\n";
			strError_FullString += "Error Code: " + iErrorID;
			
			//set log
			SetLog(strError_FullString);

			return strError_FullString;
		}

        //Display Error Message Box (Wait for box to close) ////////////////////////////////////////////////////////////////////////////////
        public void Display_ErrorMessageBox(String StrError)
        {
            m_MessageBoxTrigger.Trigger_MessageBox_Error(StrError);

            //loop till the box is closed
			while (m_MessageBoxTrigger.Flag_MsgBoxError == true) { Thread.Sleep(10); }
        }

        //Display Warning Message Box (Wait for box to close) //////////////////////////////////////////////////////////////////////////
        public void Display_WarningMessageBox(String strWarning)
        {
            m_MessageBoxTrigger.Trigger_MessageBox_Warning(strWarning);

            //loop till the box is closed
			while (m_MessageBoxTrigger.Flag_MsgBoxWarning == true) { Thread.Sleep(10); }
        }

        //Display Request Message Box (Wait for box to close) ///////////////////////////////////////////////////////////////////////
        public short Display_RequestMessageBox(String strRequest)
        {
            short iRet = 1;
            m_MessageBoxTrigger.Trigger_MessageBox_Request(strRequest);

            //loop till the box is closed
			while (m_MessageBoxTrigger.Flag_RequestMsgbox == true) { Thread.Sleep(10); }

            //decision
			iRet = (short)m_MessageBoxTrigger.Flag_RequestState;
            SetLog("Display_RequestMessageBox: Selection=" + iRet.ToString());

            return iRet;
        }

        //Display Request MessageBox (wait for box to close: Must)//////////////////////////////////////////////////////////////////
        public short Display_RequestMessageBox_3Input(String strRequest)
        {
            short iRet = 1;
            m_MessageBoxTrigger.Trigger_MessageBox_Request3Input(strRequest);

            //loop till the box is closed
			while (m_MessageBoxTrigger.Flag_RequestMsgBox3Input == true) { Thread.Sleep(10); }

            //decision
			iRet = (short)m_MessageBoxTrigger.RequestState_3input_i;
            SetLog("Display_RequestMessageBox 3Input: Selection=" + iRet.ToString());

            return iRet;
        }

		//Single Station Trigger////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void SingleStationTrigger( String strLabel )
		{
			//Check
			if ( this.Flag_Run == true )
			{
				Display_WarningMessageBox("Main Sequence is runing. Not allowed to trigger station via this way: " + strLabel);
				return;
			}

			//single station trigger
			switch ( strLabel )
			{
				case "SolderStation 1": 
					m_DataUnit.Flag_Conv_OP60_Trigger_b = true;
					Thread.Sleep(2000);
					m_DataUnit.Flag_Conv_OP60_Trigger_b = false; 
					break;

				case "SolderStation 2": 
					m_DataUnit.Flag_Conv_OP70_Trigger_b = true;
					Thread.Sleep(2000);
					m_DataUnit.Flag_Conv_OP70_Trigger_b = false; 
					break;

				case "SolderStation 3":
					m_DataUnit.Flag_Conv_OP80_Trigger_b = true;
					Thread.Sleep(2000);
					m_DataUnit.Flag_Conv_OP80_Trigger_b = false;
					break;

				case "Vision Inspection":
					m_DataUnit.Flag_Conv_OP90_Trigger_b = true;
					Thread.Sleep(2000);
					m_DataUnit.Flag_Conv_OP90_Trigger_b = false;
					break;

				case "Hot Blower":
					m_DataUnit.Flag_Conv_OP110_Trigger_b = true;
					Thread.Sleep(2000);
					m_DataUnit.Flag_Conv_OP110_Trigger_b = false;
					break;

				default:
					Display_WarningMessageBox("Unsupported Operations: " + strLabel);
					break;
			}
		}

		/*************************************************************************************************
		 * Central PC communications
		 *************************************************************************************************/
		//Update Package class via Server////////////////////////////////////////////////////////////////////
        //public bool Update_PackageFileClass()
        //{
        //    string strResponse = "";
        //    string[] strArData;
        //    int iItems = 0;							//counter for each item
        //    int iItemsCount = 0;				//counter for item iteration
        //    int iItemsTotal = 0;					//total item that needs to be registered. Note: Please becareful, if the array is not correct, the software will crash due to out of index

        //    if (m_DataUnit.Flag_DryRun == true) return true;
        //    if (m_Server.m_Settings.m_bEnable == false) return true;

        //    //Server Get Package file
        //    if (!m_Server.Cmd_GetCurrentPkg(m_MachineInfo.m_strMachineID, ref strResponse))
        //    {
        //        //error
        //        String strError = "";
        //        strError = this.Get_ErrorMessage(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), "Server_Main: Get PackageFile");
        //        m_MessageBoxTrigger.Trigger_MessageBox_Error(strError);
        //        return false;
        //    }

        //    //split string into array
        //    strArData = strResponse.Split(',');
        //    SetLog("Server_Main: Get PackageFile: Server Response=" + strResponse); 

        //    //update package settings
        //    //Get package file setting name to load robot / indexer position
        //    m_MachineInfo.m_strPackageFileName = strArData[0];

        //    //iterate through station common settings
        //    for ( int iCount = 0; iCount < 3; iCount++ )
        //    {
        //        iItems = 0;
        //        iItemsTotal = 0;		

        //        //Set pointer
        //        Data_Common_Stat mDataCommon = null;
        //        switch (iCount)
        //        {
        //            case 0: mDataCommon = m_DataPackageFile.m_DataCommon_Stat1; break;
        //            case 1: mDataCommon = m_DataPackageFile.m_DataCommon_Stat2; break;
        //            case 2: mDataCommon = m_DataPackageFile.m_DataCommon_Stat3; break;
        //        }

        //        //set package file variables
        //        iItemsTotal = 6;
        //        mDataCommon.m_iSolder_Program_Pin_1	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);
        //        mDataCommon.m_iSolder_Program_Pin_2	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);
        //        mDataCommon.m_iSolder_Program_Pin_3	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);
        //        mDataCommon.m_iSolder_Program_Pin_4	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);
        //        mDataCommon.m_iSolder_Program_Pin_5	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);
        //        mDataCommon.m_iSolder_Program_Pin_6	= int.Parse(strArData[iItemsCount]);			    iItemsCount = (iItems++) + (iCount * iItemsTotal);

        //        SetLog("Solder Station DataCommon added successfully. SolderStation " + iCount.ToString());
        //    }

        //    m_DataPackageFile.m_DataCommon.m_dbHotBlower_Temperature = int.Parse(strArData[iItemsCount]);	iItemsCount++;

        //    //machine settings update
        //    m_MachineInfo.m_lg_Soldering_Run_Timeout			= long.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_lg_Soldering_Start_Timeout			= long.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_lg_Solder_Sleep_Timeout			= long.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_lg_FumeOffTime_Max				= long.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_lg_Solder_Cleaning_Time					= long.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_iIronHead_UseCount				= int.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_iCleanCount						= int.Parse(strArData[iItemsCount]);		iItemsCount++;
        //    m_MachineInfo.m_dbCleaningOffset_Up				= double.Parse(strArData[iItemsCount]);	    iItemsCount++;
        //    m_MachineInfo.m_dbCleaningOffset_Dwn			= double.Parse(strArData[iItemsCount]);	    iItemsCount++;

        //    //Update pkg file
        //    this.Pkg_To_Registry();

        //    return true;
        //}

		//Update to central PC
        //public bool Update_CentralPCPkgFile( String strPkgFilename )
        //{
        //    String strData = "";

        //    //bypass
        //    if ( m_DataUnit.Flag_DryRun == true ) return true;
        //    if ( m_Server.m_Settings.m_bEnable == false ) return true;

        //    strPkgFilename += m_MachineInfo.m_strPackageFileName + ",";

        //    //compile string
        //    //iterate through station common settings
        //    for ( int iCount = 0; iCount < 3; iCount++ )
        //    {
        //        //Set pointer
        //        Data_Common_Stat mDataCommon = null;
        //        switch ( iCount )
        //        {
        //            case 0: mDataCommon = m_DataPackageFile.m_DataCommon_Stat1; break;
        //            case 1: mDataCommon = m_DataPackageFile.m_DataCommon_Stat2; break;
        //            case 2: mDataCommon = m_DataPackageFile.m_DataCommon_Stat3; break;
        //        }

        //        //Add to string cmd
        //        strData += mDataCommon.m_iSolder_Program_Pin_1.ToString() + ",";
        //        strData += mDataCommon.m_iSolder_Program_Pin_2.ToString() + ",";
        //        strData += mDataCommon.m_iSolder_Program_Pin_3.ToString() + ",";
        //        strData += mDataCommon.m_iSolder_Program_Pin_4.ToString() + ",";
        //        strData += mDataCommon.m_iSolder_Program_Pin_5.ToString() + ",";
        //        strData += mDataCommon.m_iSolder_Program_Pin_6.ToString() + ",";
        //    }

        //    strData += m_DataPackageFile.m_DataCommon.m_dbHotBlower_Temperature.ToString() + ",";
        //    strData += m_MachineInfo.m_lg_Soldering_Run_Timeout	.ToString() + ",";
        //    strData += m_MachineInfo.m_lg_Soldering_Start_Timeout.ToString() + ",";	
        //    strData += m_MachineInfo.m_lg_Solder_Sleep_Timeout.ToString() + ",";	
        //    strData += m_MachineInfo.m_lg_FumeOffTime_Max.ToString() + ",";		
        //    strData += m_MachineInfo.m_lg_Solder_Cleaning_Time.ToString() + ",";				
        //    strData += m_MachineInfo.m_iIronHead_UseCount.ToString() + ",";		
        //    strData += m_MachineInfo.m_iCleanCount.ToString() + ",";						
        //    strData += m_MachineInfo.m_dbCleaningOffset_Up.ToString() + ",";
        //    strData += m_MachineInfo.m_dbCleaningOffset_Dwn.ToString() + ",";	


        //    //Send pkg setting to server
        //    if ( !m_Server.Cmd_UpdatePkgSettings("2", strData) )
        //    {
        //        //error
        //        String strError = "";
        //        strError = this.Get_ErrorMessage(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), "Server_Main: Update PackageFile");
        //        m_MessageBoxTrigger.Trigger_MessageBox_Error(strError);
        //        return false;
        //    }

        //    return true;
        //}


        //Keyence Vision Initialize function//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Init_Keyence_Vision()
        {
            //disconnect
            m_Vision_Keyence.Disconnect();

            //connect to vision
            if (m_Vision_Keyence.Connect() == false)
            {
                strError += "\r\nKeyence Vision Camera Connect Failed" + Get_ErrorMessage(m_Vision_Keyence.Get_ErrorID(), m_Vision_Keyence.Get_ErrorMessage(), m_Vision_Keyence.m_strLabel);
                return false;
            }

            return true;
        }

        //Cognex Vision Initialize function//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Init_Cognex_Vision()
        {
            //disconnect
            m_Camera1.Disconnect();
            m_Camera2.Disconnect();

            //connect to vision
            if (m_Camera1.Connect() == false)
            {
                strError += "\r\nOP90 Vision Camera (Left) Connect Failed" + Get_ErrorMessage(m_Camera1.Get_ErrorID(), m_Camera1.Get_ErrorMessage(), m_Camera1.m_strLabel);
                return false;
            }

            if (m_Camera2.Connect() == false)
            {
                strError += "\r\nOP90 Vision Camera (Right) Connect Failed" + Get_ErrorMessage(m_Camera2.Get_ErrorID(), m_Camera2.Get_ErrorMessage(), m_Camera2.m_strLabel);
                return false;
            }

            //vision load recipe
            if (m_Camera1.Load_Recipe(m_Camera1.Get_VisionSettings().m_strRecipeName) == false)
            {
                strError += "\r\nVision Load Recipe Failed" + Get_ErrorMessage(m_Camera1.Get_ErrorID(), m_Camera1.Get_ErrorMessage(), m_Camera1.m_strLabel);
                return false;
            }

            return true;
        }

        //RFID Initialize function//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Init_RFID()
        {
            //disconnect
            m_RFIDReader_OP60_OP85.Disconnect();
            m_RFIDReader_OP90_OP110.Disconnect();

            //connect to RFID
            if (m_RFIDReader_OP60_OP85.Connect() == false)
            {
                strError += "\r\nOP60 - OP85 RFID Connect Failed" + Get_ErrorMessage(m_RFIDReader_OP60_OP85.Get_ErrorID(), m_RFIDReader_OP60_OP85.Get_ErrorMessage(), m_RFIDReader_OP60_OP85.m_strLabel);
                return false;
            }

            if (m_RFIDReader_OP90_OP110.Connect() == false)
            {
                strError += "\r\nOP90 - OP110 RFID Connect Failed" + Get_ErrorMessage(m_RFIDReader_OP90_OP110.Get_ErrorID(), m_RFIDReader_OP90_OP110.Get_ErrorMessage(), m_RFIDReader_OP90_OP110.m_strLabel);
                return false;
            }

            return true;
        }


		/*************************************************************************************************************************
		 * INITIALIZE Sequence
		 **************************************************************************************************************************/
		//Start Init////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Start_Init()
        {
            if (Flag_RunningInit == false)
            {
                m_thInit = new Thread(Init);
                m_thInit.Start();
            }
        }

        //Machine Initialization/////////////////////////////////////////////////////////////////////////////////////////////
        public void Init()
        {
            Stopwatch mSt = new Stopwatch();
            //String strError = ""; //move to top for global usage
            strError = ""; //reset error message
            bool bRet = false;

            //Checking
            if (Flag_Run == true) return;

            //Safety Door Lock ON
            m_Module_ControlPanel.TurnOn_DoorLock();
            Delay_ms(500);

            //Check monitor input
            if (m_MonitorInput.Idle() == false)
            {
                strError = "Init Failed: " + Get_ErrorMessage(m_MonitorInput.Get_Error());
                goto End;
            }

            //reset to Server ON
            m_Server.m_Settings.m_bEnable = true;

            //Init Flags
            m_DataUnit.Reset_Flag();
            m_DataUnit.Reset_Data();

			//Init sequence
			Flag_InitOK = false;
			Flag_RunningInit = true;

			m_DataUnit.MachineState_Str = "Initializing M/C";
            SetLog("*** START MACHINE INITIALIZATION ***");

			m_Module_ControlPanel.Init_Module();

			//Init all modules
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b) m_Module_FwdConv_OP85_OP90.Start_Init();

            if (m_DataUnit.Flag_EnableManualOP100_b) m_Module_OP100_Buffer.Start_Init();
            if (m_DataUnit.Flag_EnableManualOP100_b) m_Module_OP100_Station.Start_Init();
            if (m_DataUnit.Flag_EnableManualOP100_b) m_Module_FwdConv_OP100.Start_Init();

            if (m_DataUnit.Flag_Enable_Module_OP110_b) m_Module_OP110_HotBlower.Start_Init();
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b) m_Module_FwdConv_OP110.Start_Init();

            //Init Modules
            Thread.Sleep(1000);

            //wait for all modules Init to end
            mSt.Start();
            while (true)
            {
                //Time out
                if (mSt.ElapsedMilliseconds > 60000) { mSt.Stop(); strError = "Error: Initialization Timeout."; break; }
                
				//All thread init finish
				if (
                    m_Module_FwdConv_OP85_OP90.Flag_Init    == false &&
                    m_Module_OP100_Buffer.Flag_Init         == false &&
                    m_Module_OP100_Station.Flag_Init        == false &&
                    m_Module_FwdConv_OP100.Flag_Init        == false &&
                    m_Module_OP110_HotBlower.Flag_Init      == false &&
                    m_Module_FwdConv_OP110.Flag_Init        == false
                   )
                {
					break;
                }
            }
            mSt.Stop();

			//Check init Error
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b && m_Module_FwdConv_OP85_OP90.Flag_InitOK == false) strError += "\r\nModule FwdConv OP85 - OP90 Init Failed: " + Get_ErrorMessage(m_Module_FwdConv_OP85_OP90.Get_ErrorID(), m_Module_FwdConv_OP85_OP90.Get_ErrorMsgAdd(), m_Module_FwdConv_OP85_OP90.Get_ErrorMsg_Front());

            if (m_DataUnit.Flag_EnableManualOP100_b && m_Module_OP100_Buffer.Flag_InitOK == false) strError += "\r\nModule OP100 Buffer Init Failed: " + Get_ErrorMessage(m_Module_OP100_Buffer.Get_ErrorID(), m_Module_OP100_Buffer.Get_ErrorMsgAdd(), m_Module_OP100_Buffer.Get_ErrorMsg_Front());
            if (m_DataUnit.Flag_EnableManualOP100_b && m_Module_OP100_Station.Flag_InitOK == false) strError += "\r\nModule OP100 Station Init Failed: " + Get_ErrorMessage(m_Module_OP100_Station.Get_ErrorID(), m_Module_OP100_Station.Get_ErrorMsgAdd(), m_Module_OP100_Station.Get_ErrorMsg_Front());
            if (m_DataUnit.Flag_EnableManualOP100_b && m_Module_FwdConv_OP100.Flag_InitOK == false) strError += "\r\nModule FwdConv OP100 Init Failed: " + Get_ErrorMessage(m_Module_FwdConv_OP100.Get_ErrorID(), m_Module_FwdConv_OP100.Get_ErrorMsgAdd(), m_Module_FwdConv_OP100.Get_ErrorMsg_Front());

            if (m_DataUnit.Flag_Enable_Module_OP110_b && m_Module_OP110_HotBlower.Flag_InitOK == false) strError += "\r\nModule OP110 Hot Blower Init Failed: " + Get_ErrorMessage(m_Module_OP110_HotBlower.Get_ErrorID(), m_Module_OP110_HotBlower.Get_ErrorMsgAdd(), m_Module_OP110_HotBlower.Get_ErrorMsg_Front());
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b && m_Module_FwdConv_OP110.Flag_InitOK == false) strError += "\r\nModule FwdConv OP110 Init Failed: " + Get_ErrorMessage(m_Module_FwdConv_OP110.Get_ErrorID(), m_Module_FwdConv_OP110.Get_ErrorMsgAdd(), m_Module_FwdConv_OP110.Get_ErrorMsg_Front());

            //initialize vision
            if (Init_Cognex_Vision() == false) goto End;
            if (Init_Keyence_Vision() == false) goto End;

            //initialize RFID
            if (Init_RFID() == false) goto End;

            //check for error
            if (strError != "") goto End;

            //Set Passed flags            
            bRet = true;

            //Set tower light
            m_Module_ControlPanel.TowerLight_Standby();

            //End of Init Sequence
        End:
            mSt.Stop();
            Flag_RunningInit = false;
            if (bRet == false)
            {
				Flag_InitOK = false;
                m_DataUnit.MachineState_Str = "Initialization Failed";
                m_DataUnit.DispErrorMessage_str = strError;
                m_MessageBoxTrigger.Trigger_MessageBox_Error(strError);
                m_Module_ControlPanel.TowerLight_Error();
                SetLog("*** MACHINE INITIALIZATION FAILED ***");
            }
            else
            {
				Flag_InitOK = true;
                m_DataUnit.MachineState_Str = "M/C Ready";
				m_MessageBoxTrigger.Trigger_MessageBox_Warning("Initialization Complete...");
                SetLog("*** MACHINE INITIALIZATION COMPLETED ***");
            }

            return;
        }



        /*************************************************************************************************************************
         * Main Run Sequence
         **************************************************************************************************************************/
        //Start Run///////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Start_Run()
        {
            //block if thread is already running
            if (Flag_Run == true) return true;
			
			//Start Main control Sequence run
            m_thMainRun = new Thread(Run);
            m_thMainRun.Start();

            return true;
        }

        //Stop Run////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Stop_Run()
        {
            //Stop all threads
            m_Module_FwdConv_OP85_OP90.Module_StopRun = true;

            m_Module_OP100_Buffer.Module_StopRun = true;
            m_Module_OP100_Station.Module_StopRun = true;
            m_Module_FwdConv_OP100.Module_StopRun = true;

            m_Module_OP110_HotBlower.Module_StopRun = true;
            m_Module_FwdConv_OP110.Module_StopRun = true;

            Flag_RunEnd = true;
            return true;
        }

        //main sequence run////////////////////////////////////////////////////////////////////////////////////////////////
        public void Run()
        {
            Stopwatch st = new Stopwatch();
            String strError = "";
            bool bRet = false;
			bool bFlag_ErrorSolderStation1 = false;					//block repeat display error if module is in error.
			bool bFlag_ErrorSolderStation2 = false;					//block repeat display error if module is in error.
			bool bFlag_ErrorSolderStation3 = false;					//block repeat display error if module is in error.

			//Check if the module has been initialized
            if (Flag_InitOK == false) { strError = "Machine is not initialize. \r\nPlease initialize the machine."; goto End; }

            //Safety Door Lock ON
            m_Module_ControlPanel.TurnOn_DoorLock();

            Delay_ms(500);

			//Skip if main thread is running
            if (Flag_Run == true) { return; }

            //Monitor input check-----------------------------------------------------------------------------------------------------------------------
            if (m_MonitorInput.Idle() == false)
            {
                strError = Get_ErrorMessage(m_MonitorInput.Get_Error());
                m_DataUnit.DispErrorMessage_str = strError;
                m_MessageBoxTrigger.Trigger_MessageBox_Error(strError);
                goto End;
            }

			//Set Flag
			Flag_Run = true;
			Flag_RunEnd = false;
			m_DataUnit.DispErrorMessage_str = "";

			SetLog("Auto Sequence Start " + DateTime.Now.ToString("HH:mm:ss"));

            //Start up Module Threads----------------------------------------------------------------------------------------------------------------------------
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b) m_Module_FwdConv_OP85_OP90.Start_Run();

			if (m_DataUnit.Flag_EnableManualOP100_b			     ) m_Module_OP100_Buffer.Start_Run();
            if (m_DataUnit.Flag_EnableManualOP100_b			     ) m_Module_OP100_Station.Start_Run();
            if (m_DataUnit.Flag_EnableManualOP100_b			     ) m_Module_FwdConv_OP100.Start_Run();

            if (m_DataUnit.Flag_Enable_Module_OP110_b			 ) m_Module_OP110_HotBlower.Start_Run();
            if (m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b	 ) m_Module_FwdConv_OP110.Start_Run();
			
            //Wait thread to start---------------------------------------------------------------------------------------------------------------------
            st.Start();
            while (true)
            {
				//Wait all modules to start
				if (
                    ((m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b	== true) ? m_Module_FwdConv_OP85_OP90.Module_Running    : true) == true &&
					((m_DataUnit.Flag_EnableManualOP100_b				== true) ? m_Module_OP100_Buffer.Module_Running         : true) == true &&
                    ((m_DataUnit.Flag_EnableManualOP100_b				== true) ? m_Module_OP100_Station.Module_Running        : true) == true &&
                    ((m_DataUnit.Flag_EnableManualOP100_b				== true) ? m_Module_FwdConv_OP100.Module_Running        : true) == true &&
                    ((m_DataUnit.Flag_Enable_Module_OP110_b             == true) ? m_Module_OP110_HotBlower.Module_Running      : true) == true &&
                    ((m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b	    == true) ? m_Module_FwdConv_OP110.Module_Running        : true) == true
				   ) 
					break;

                //Time out
                if (st.ElapsedMilliseconds > 5000)
                {
                    string strModule = "";

					//get modules which are not running
                    if (m_DataUnit.Flag_Enable_Module_FwdConv_OP85_OP90_b == true && m_Module_FwdConv_OP85_OP90.Module_Running    == false) strModule += " [ FwdConv - OP85, OP90 ]";
					if (m_DataUnit.Flag_EnableManualOP100_b		          == true && m_Module_OP100_Buffer.Module_Running         == false) strModule += " [ OP100 - Buffer ]";
                    if (m_DataUnit.Flag_EnableManualOP100_b		          == true && m_Module_OP100_Station.Module_Running        == false) strModule += " [ OP100 - Station ]";
                    if (m_DataUnit.Flag_EnableManualOP100_b               == true && m_Module_FwdConv_OP100.Module_Running        == false) strModule += " [ FwdConv - OP100 ]";
                    if (m_DataUnit.Flag_Enable_Module_OP110_b			  == true && m_Module_OP110_HotBlower.Module_Running      == false) strModule += " [ OP110 - Hot Blower ]";
                    if (m_DataUnit.Flag_Enable_Module_FwdConv_OP110_b     == true && m_Module_FwdConv_OP110.Module_Running        == false) strModule += " [ FwdConv - OP110 ]";
					
                    st.Stop();
                    strError = "Error: Some threads did not start within the allocated time frame.\r\n" + strModule;
                    Stop_Run();                  

                    goto End;
                }
            }
            st.Stop();

            //Main sequenceCtrl loop------------------------------------------------------------------------------------------------------------------------------
            m_WatchTimer.Set_StartTime();
            m_Module_ControlPanel.TowerLight_Run();
            m_DataUnit.MachineState_Str = "Running";
            while (true)
            {
                Thread.Sleep(100);

                //module stopped
                if (Flag_RunEnd) { Flag_RunEnd = false; break; }

                //Check Monitor input
                if (m_MonitorInput.Idle() == false)
                {
                    strError = Get_ErrorMessage(m_MonitorInput.Get_Error());
                    Flag_InitOK = false;
                    break;
                }

                else if (m_Module_FwdConv_OP85_OP90.Flag_Error == true)
                {
                    m_Module_FwdConv_OP85_OP90.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_FwdConv_OP85_OP90.Get_ErrorID(),
                                                        m_Module_FwdConv_OP85_OP90.Get_ErrorMsgAdd(),
                                                        m_Module_FwdConv_OP85_OP90.Get_ErrorMsg_Front());
                }

                else if (m_Module_OP100_Buffer.Flag_Error == true)
                {
                    m_Module_OP100_Buffer.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_OP100_Buffer.Get_ErrorID(),
                                                        m_Module_OP100_Buffer.Get_ErrorMsgAdd(),
                                                        m_Module_OP100_Buffer.Get_ErrorMsg_Front());
                }

                else if (m_Module_OP100_Station.Flag_Error == true)
                {
                    m_Module_OP100_Station.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_OP100_Station.Get_ErrorID(),
                                                        m_Module_OP100_Station.Get_ErrorMsgAdd(),
                                                        m_Module_OP100_Station.Get_ErrorMsg_Front());
                }

                else if (m_Module_FwdConv_OP100.Flag_Error == true)
                {
                    m_Module_FwdConv_OP100.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_FwdConv_OP100.Get_ErrorID(),
                                                        m_Module_FwdConv_OP100.Get_ErrorMsgAdd(),
                                                        m_Module_FwdConv_OP100.Get_ErrorMsg_Front());
                }

                else if (m_Module_OP110_HotBlower.Flag_Error == true)
                {
                    m_Module_OP110_HotBlower.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_OP110_HotBlower.Get_ErrorID(),
                                                        m_Module_OP110_HotBlower.Get_ErrorMsgAdd(),
                                                        m_Module_OP110_HotBlower.Get_ErrorMsg_Front());
                }

                else if (m_Module_FwdConv_OP110.Flag_Error == true)
                {
                    m_Module_FwdConv_OP110.Flag_Error = false;
                    strError = this.Get_ErrorMessage(m_Module_FwdConv_OP110.Get_ErrorID(),
                                                        m_Module_FwdConv_OP110.Get_ErrorMsgAdd(),
                                                        m_Module_FwdConv_OP110.Get_ErrorMsg_Front());
                }

                //display error message
                if (strError != "")
                {
                    SetLog(strError);
                    m_DataUnit.DispErrorMessage_str = strError;
                    Display_ErrorMessageBox(strError);
                    m_Module_ControlPanel.TowerLight_Run();
                    strError = "";
                }

            }
			
			m_WatchTimer.Set_StopTime();
			this.Stop_Run();
      
            //wait all threads to stop------------------------------------------------------------------------------------------------------------------------
            #region WaitThreadsStop
            st.Start();
            while (true)
            {
                Thread.Sleep(100);

                if (st.ElapsedMilliseconds >= 70000) 
                {
                    strError += "Error: Some threads did not stop in the allocated time frame.";

                    if (m_Module_FwdConv_OP85_OP90.Module_Running    == true) { strError += "[Module FwdConv OP85 > OP90] "; }
                    if (m_Module_OP100_Buffer.Module_Running         == true) { strError += "[Module OP100 Buffer] "; }
                    if (m_Module_OP100_Station.Module_Running        == true) { strError += "[Module OP100 Station] "; }
                    if (m_Module_FwdConv_OP100.Module_Running        == true) { strError += "[Module FwdConv OP100] "; }
                    if (m_Module_OP110_HotBlower.Module_Running		 == true) { strError += "[Module HotBlower OP110] "; }
                    if (m_Module_FwdConv_OP110.Module_Running        == true) { strError += "[Module FwdConv OP110] "; }

                    break;
                }

                //wait all thread to stop
				if ( 
                    m_Module_FwdConv_OP85_OP90.Module_Running    == false &&
                    m_Module_OP100_Buffer.Module_Running         == false &&
                    m_Module_OP100_Station.Module_Running        == false &&
                    m_Module_FwdConv_OP100.Module_Running        == false &&
                    m_Module_OP110_HotBlower.Module_Running		 == false &&
                    m_Module_FwdConv_OP110.Module_Running        == false
                   ) 
                {
					break;
                }
            }
            st.Stop();
            #endregion

            m_Module_ControlPanel.TowerLight_Standby();

            //End--------------------------------------------------------------------------------------------------------------------------------------------------

            //Check monitor input again
            if (m_MonitorInput.Idle() == false) { strError = Get_ErrorMessage(m_MonitorInput.Get_Error()); }

            //Set pass--------------------------------------------------------------------------------------------------------------------------------------------------- 
			if (strError == "")
				bRet = true;
			else
				bRet = false;

            //End of sequence
End:
            Flag_Run = false;
            if (bRet == false)
            {
				m_DataUnit.MachineState_Str = "M/C Error";
				m_DataUnit.DispErrorMessage_str = strError;
				m_MessageBoxTrigger.Trigger_MessageBox_Error(strError);
                m_Module_ControlPanel.TowerLight_Error();
				SetLog(strError);
                Flag_InitOK = false;
            }
            else
            {
                m_DataUnit.MachineState_Str = "Stopped";
                m_Module_ControlPanel.TowerLight_Standby();
            }
        }
    }
}