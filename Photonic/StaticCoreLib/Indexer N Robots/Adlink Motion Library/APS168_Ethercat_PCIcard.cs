using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Threading;
using APS_Define_W32;
using APS168_W32;

namespace StaticCoreLib
{
	public class APS168_Ethercat_PCIcard
	{
		//variables
		private readonly object m_lock_Flag_Connected = new object();			//Flag: Signal that the PCI has been initialized.
		public bool m_bConnected = false;
		public bool Flag_Connected_b
		{
			set { lock ( m_lock_Flag_Connected ) { m_bConnected = value; } }
			get { lock ( m_lock_Flag_Connected ) { return m_bConnected; } }
		}

		int m_iCardErrorID = 0;
		public Int32 m_iCardID = 1;
		public Int32 m_CardMode = 0;

		//constructor//////////////////////////////////////////////////////////////////////////////////////////////
		public APS168_Ethercat_PCIcard()
		{
		}

		//Initialize//////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Initialize_PCI()
        {
            int iRet = 0;
            int iCardName = 0;
            int iTotalAxisNum = 0;
            int iStartAxisID = 0;
            int iBoardID = 0;
            int iTemp = 0;
            int iBoardBits = 0;

            //Cannot call APS_initial many time. Require block
            if (Flag_Connected_b == true) return true;

            //init Ethercat PCIe Card
            iRet = APS168.APS_initial(ref iBoardBits, 0);
            if (iRet < 0)
            {
                m_iCardErrorID = iRet;
                return false;
            }

            //APS Protocol
            for (int iCount = 0; iCount < 16; iCount++)
            {
                iTemp = (iBoardBits >> iCount) & 1;

                if (iTemp == 1)
                {
                    iRet = APS168.APS_get_card_name(iCount, ref iCardName);
                    if (iRet < 0)
                    {
                        m_iCardErrorID = iRet;
                        return false;
                    }

                    if (iCardName == (Int32)APS_Define.DEVICE_NAME_PCIE_8338 ||
                        iCardName == (Int32)APS_Define.DEVICE_NAME_PCIE_8334 ||
                        iCardName == (Int32)APS_Define.DEVICE_NAME_PCIE_8332)
                    {
                        iRet = APS168.APS_get_first_axisId(iCount, ref iStartAxisID, ref iTotalAxisNum);
                        if (iRet < 0)
                        {
                            m_iCardErrorID = iRet;
                            return false;
                        }

                        iBoardID = iCount;
                        break;
                    }
                }
            }

            //PCI8332 require this to be commented out.
            //iRet = APS168.APS_scan_field_bus(iBoardID, 0);		//note: Require to call this to start the APS library with HSL library. HSL library need to be called first
            //Start field bus			
            iRet = APS168.APS_start_field_bus(iBoardID, 0, iStartAxisID);
            if (iRet < 0)
            {
                m_iCardErrorID = iRet;
                return false;
            }

            //Init complete
            Flag_Connected_b = true;
            return true;
        }

		//De-Init//////////////////////////////////////////////////////////////////////////////////////////////////////
		public void DeInit_PCI()
		{
			APS168.APS_close();
			Flag_Connected_b = false;
		}

		//Get Error ID/////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iError = m_iCardErrorID;
			m_iCardErrorID = 0;
			return iError;
		}

        //Get Error Message///////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage(int iErrorID)
        {
            String strError = "unknown error";

            switch (iErrorID)
            {
                case -1: strError = "Operating system version error.\r\nThe current operating system you used are not supported by this function."; break;
                case -2: strError = "Open driver failed. Create driver interface failed.\r\nCheck device driver is installed correctly.\r\nCheck devices are installed correctly in your system."; break;
                case -3: strError = "System memory insufficiently.\r\nThere is not enough memory in your system."; break;
                case -4: strError = "The device or the card is not be initialized.\r\nCheck the card ID.\r\nThe device has been closed.\r\nThe device is not be initialized."; break;
                case -5: strError = "Devices not found.\r\nCheck device driver is installed correctly.\r\nCheck devices are installed correctly in your system."; break;
                case -6: strError = "Card ID duplicated.\r\nCheck the card ID settings (SW jump).\r\nCheck the parameter of initial function is correctly."; break;
                case -7: strError = "The devices have already been initialed.\r\n1. Check the close card function is work correctly."; break;
                case -8: strError = "Interrupt events not be enabled.\r\n1. Enable the hardware interrupt.\r\n2. Check the interrupt factor is set correctly."; break;
                case -9: strError = "Function timeout."; break;

                case -10: strError = "The value of the parameters is incorrect.\r\nCheck the setting range of parameters.\r\nCompare the setting value of parameters with user manual."; break;
                case -11: strError = "Hardware memory write error."; break;
                case -12: strError = "Hardware memory read error."; break;
                case -13: strError = "The function is not available in current stage.\r\nThe device is not support this function.\r\nSystem is in error state.\r\n1. Check the function library.\r\n2. Check the hardware connection (servo drive connection).\r\n3. Reinitial(Reboot) the system."; break;
                case -14: strError = "Firmware process error.\r\n1. Check the firmware version."; break;
                case -15: strError = "The previous command is in process."; break;
                case -16: strError = "Axes' ID is duplicated."; break;
                case -17: strError = "Slave module not found."; break;
                case -18: strError = "System ModuleNo insufficiently."; break;
                case -19: strError = "HandShake with the DSP out of time."; break;

                case -20: strError = "Config file format error. (cannot be parsed)"; break;
                case -21: strError = "Function parameters read only."; break;
                case -22: strError = "Distant is not enough for motion."; break;
                case -23: strError = "Function not yet enabled."; break;
                case -24: strError = "Server already closed."; break;
                case -25: strError = "Could’t find virtual DLL."; break;

                case -32: strError = "Could’t find specified function on virtual DLL."; break;
                case -33: strError = "Feeder abnormally stop."; break;

                case -40: strError = "Double format parameter is overflow."; break;
                case -41: strError = "Slave ID number error."; break;
                case -42: strError = "The status of EtherCAT slave is error."; break;
                case -43: strError = "The input data over assigned range."; break;
                case -46: strError = "Execute latch function error."; break;
                case -48: strError = "Axis is in servo on state would cause error.\r\nNow only used in PCI(e)-8154/58 APS_spiral_ce_xxx function. Due to 8154/58 limit, 4th / 8th axis operation will be a dummy motion and it can’t be used for any other puspose. This axis need to be set servo-off. If not, it will return ERR_InServoOnState"; break;

                case -901: strError = "Initialization error."; break;
                case -902: strError = "No initialization."; break;
                case -903: strError = "No input parameters setup."; break;
                case -904: strError = "Error input parameters."; break;
                case -905: strError = "PC status is not ready."; break;
                case -906: strError = "The state of specified axis is busy."; break;
                case -907: strError = "Network connection has error."; break;
                case -908: strError = "Network connection is time-out."; break;
                case -909: strError = "Checking CRC is failed."; break;

                case -910: strError = "Invalid parameter setting for EMX-100."; break;
                case -911: strError = "Not in servo on status."; break;
                case -912: strError = "API is timeout (150ms)."; break;
                case -913: strError = "Asynch status update timeout."; break;
                case -915: strError = "Mismatch configuration of Board ID and Axis ID in XML file."; break;

                case -1000: strError = "No such event number, or WIN32_API error, contact with ADLINK's FAE staff."; break;
                case -1001: strError = "Generating ADLINK_Config2.xml file (ENI file) is failed."; break;
                case -1002: strError = "Set voltage enable time out with servo on process."; break;
                case -1003: strError = "Set ready to switch time out with servo on process."; break;
                case -1004: strError = "Set shut down time out with servo on process."; break;
                case -1005: strError = "Set switch on time out with servo on process."; break;
                case -1006: strError = "Set operation enable time out with servo on process."; break;
                case -1007: strError = "System registry path fails or no registry."; break;
                case -1008: strError = "Master is not in OP state."; break;
                case -1009: strError = "Slave is not in OP state."; break;

                case -1010: strError = "The scanned number of EtherCAT slaves' axes exceeds the number of maximum axes that PCIe 8334/8 can support."; break;
                case -1011: strError = "Miss ESI file or ENI path."; break;
                case -1012: strError = "Miss Config_1 xml file."; break;
                case -1013: strError = "Copy Config_1 xml file fail."; break;
                case -1014: strError = "Miss Config_2 xml file."; break;
                case -1015: strError = "Copy Config_2 xml file fail."; break;
                case -1016: strError = "Invalid slave local axis."; break;
                case -1017: strError = "Invalid home mode."; break;
                case -1018: strError = "File name length over 32 bytes."; break;
                case -1019: strError = "File compare result failed."; break;

                case -1020: strError = "Auto recovery function is enabled. Must “DISABLE” auto recovery function before using FoE download."; break;
                case -1021: strError = "FoE download file is too large. The limitation is 10M bytes."; break;

                case -2001: strError = "Axis range error."; break;
                case -2002: strError = "Channel range error."; break;
                case -2003: strError = "Parameter number undefine."; break;
                case -2004: strError = "Parameter data is wrong."; break;
                case -2005: strError = "This state cannot do this thing. (command position only can be set when axis is in idle)"; break;
                case -2006: strError = "Checksum error (Internal error),(Check code error)"; break;
                case -2007: strError = "Timeout error."; break;
                case -2008: strError = "Memory test error."; break;
                case -2009: strError = "Unknown command or this state cannot accept this command."; break;

                case -2010: strError = "The dimension of the axes is invalid."; break;
                case -2011: strError = "Motion buffer is full"; break;
                case -2012: strError = "This function cannot be accept when axes are in blending, ( all spgs are busy )"; break;
                case -2013: strError = "The transition parameter /% is out of range."; break;
                case -2014: strError = "Transition mode is undefine or not support."; break;
                case -2015: strError = "Transform matrix column index > MAX_AXES (invaild)"; break;
                case -2016: strError = "Soft-inp width invaild ( >= 0 )"; break;
                case -2017: strError = "Soft-inp stable time invaild ( < 65535 )"; break;
                case -2018: strError = "Gantry master axis invaild"; break;
                case -2019: strError = "The size of compare date is over range."; break;

                case -2020: strError = "Start velocity invalid, (vs >= 0)"; break;
                case -2021: strError = "End velocity invalid.   (ve >= 0)"; break;
                case -2022: strError = "Maximum velocity invalid. (vm > 0)"; break;
                case -2023: strError = "Acceleration invalid. (acc > 0)"; break;
                case -2024: strError = "Deceleration invalid. (dec > 0)"; break;
                case -2025: strError = "S invalid. ( 0 <= S <= 1 )"; break;
                case -2026: strError = "SD Dec invalid. (>0)"; break;
                case -2027: strError = "Axes number cannot overlapping."; break;
                case -2028: strError = "The transition parameter ResidueDistance cannot < 0.0."; break;
                case -2029: strError = "Error position check level must >= 0.0"; break;

                case -2030: strError = "Wait mode not accept when axis in moving"; break;
                case -2031: strError = "Axis is disable (Servo off)"; break;
                case -2032: strError = "Axis is in error state (AX_ERR_STOPPING/AX_ERR_STOPPED). you should reset the error state."; break;
                case -2033: strError = "Axis is in moving and command cannot accept (cannot overwrite), axes must same dimension, same axes"; break;
                case -2034: strError = "pre-event distance must >= 0"; break;
                case -2035: strError = "post-event distance must >= 0"; break;

                case -2040: strError = "This function cannot accept half arc or full arc parameter"; break;
                case -2041: strError = "The finalR cannot be negative value."; break;
                case -2042: strError = "Normal vector invaild. or arc parameters invaild."; break;

                case -2050: strError = "Gantry deviation protect value must >= 0"; break;
                case -2051: strError = "This command is not allow in gantry mode."; break;
                case -2052: strError = "Gear master is not define"; break;
                case -2053: strError = ""; break;
                case -2054: strError = ""; break;
                case -2055: strError = ""; break;
                case -2056: strError = "Cannot be a gear loop."; break;
                case -2057: strError = "The jog offset paremeter is wrong. must > 0"; break;

                case -2062: strError = "DI group number is wrong."; break;
                case -2063: strError = "DI channel number is wrong."; break;

                case -2070: strError = "Filter coefficient is wrong"; break;
                case -2071: strError = ""; break;

                case -2080: strError = "Axis (Axes) are now in moving state, cannot start point buffer"; break;
                case -2081: strError = "Invalid axes dimension setting"; break;
                case -2082: strError = "Axes number are already in use by another PTB"; break;
                case -2083: strError = "PTBUFF not enable"; break;
                case -2084: strError = "PTBUFF is full."; break;
                case -2085: strError = "Invalid motion curve type"; break;
                case -2086: strError = "Miss mach line move dimension and PTBUFF dimension"; break;
                case -2087: strError = "Curve type and it's dimension miss match"; break;
                case -2088: strError = "Axes abnornmal stop, please check axis stop code."; break;
                case -2089: strError = "Axes motion queue exception."; break;

                case -2090: strError = "Target axes index is over PTBUFF dimension"; break;
                case -2091: strError = "Invalid PTBUFF ID. Check the input parameter."; break;
                case -2092: strError = "Invalid PTBUF control command. Check the input parameter."; break;
                case -2093: strError = "PTBUFF detected axes are in motion when start PTBUFF."; break;
                case -2094: strError = "PTBUFF extra-commands must < = 7"; break;
                case -2095: strError = "Execute extra command but command queue is empty."; break;
                case -2096: strError = "Push extra command but command queue is full."; break;
                case -2097: strError = "Invalid extra command code."; break;
                case -2098: strError = "Invalid command when point buffer is running."; break;
                case -2099: strError = "Dwell time must >= 0"; break;

                case -2100: strError = "Handshake data error. command unknown."; break;
                case -2101: strError = "Handshake data error. size or dimension invaild."; break;
                case -2102: strError = "Sub functions have errors. please check sub return code"; break;

                case -2201: strError = "DSP initialize error. ( Call adlink R&D staff )"; break;
                case -2208: strError = "Load calibration data failed."; break;
                case -2209: strError = "DPRAM hardware test failed. ( Call adlink staff )"; break;

                case -2210: strError = "FPGA version outdated. ( Call adlink staff )"; break;
                case -2211: strError = ""; break;
                case -2212: strError = ""; break;
                case -2213: strError = ""; break;

                case -2300: strError = "The timing of motion loop is out of range."; break;

                case -2401: strError = ""; break;
                case -2402: strError = ""; break;
                case -2403: strError = ""; break;
                case -2404: strError = ""; break;
                case -2405: strError = ""; break;
                case -2406: strError = ""; break;
                case -2407: strError = ""; break;
                case -2408: strError = "No calibration data in EEPROM. ( Not all gain/offset of AO/AI are calibrated )"; break;
                case -2409: strError = ""; break;

                case -2410: strError = "Backdoor password wrong."; break;
                case -2411: strError = "(Flash) Byte count of data to much or Offset is over range."; break;

                case -2500: strError = "Queue size is less than input array"; break;

                case -2600: strError = "State of frequency response process is invalid"; break;
                case -2601: strError = "State of relay control process is invalid"; break;

                case -2700: strError = "Task number is wrong"; break;
                case -2701: strError = "Unknown program register."; break;
                case -2702: strError = "Program is runing, you cannot issue this command"; break;
                case -2703: strError = "Over stack size range"; break;

                case -2800: strError = "Access type is undefined"; break;
                case -2801: strError = "Parameter access is denied since it is in save/load process"; break;
                case -2802: strError = "Erase section is failed due to time out"; break;
                case -2803: strError = "Load data error; ; last mark in flash is error"; break;
                case -2804: strError = "Load data error; parity bit is error"; break;
                case -2805: strError = "Load data error; over max data type define"; break;
                case -2806: strError = "Load data error; over max parameter type define"; break;
                case -2807: strError = "Load data error; over max board parameter define"; break;
                case -2808: strError = "Load data error; over max axis parameter define"; break;

                case -2900: strError = "Watchdog timer 1 has been enabled"; break;
                case -2901: strError = "Max reset period of watchdog timer 1 is out of range"; break;
                case -2903: strError = "Time interval between two points of PVT/PT motion is a negative value or zero. For example: t0 = 0.0sec, t1 = 0.5sec, t2 = 0.1sec"; break;

                case -4001: strError = "Initialized EtherCAT master error"; break;

                case -4011: strError = "Get total slave number error"; break;
                case -4012: strError = "Configure EtherCAT master error"; break;
                case -4013: strError = "Topology information is not match with current data"; break;
                case -4014: strError = "Read configuration data error"; break;
                case -4015: strError = "Don’t support safeop and op state"; break;

                case -4021: strError = "Configure DC parameter error"; break;
                case -4022: strError = "The dcm mode is not support"; break;
                case -4023: strError = "The dcm feature was disabled"; break;
                case -4024: strError = "Configure DCM parameter error"; break;

                case -4031: strError = "Register client error"; break;

                case -4041: strError = "Set EtherCAT master to initial state error"; break;
                case -4042: strError = "Set EtherCAT master to preop state error"; break;
                case -4043: strError = "Set EtherCAT master to safeop state error"; break;
                case -4044: strError = "Set EtherCAT master to op state error"; break;

                case -4051: strError = "Deinitialized EtherCAT master error"; break;

                case -4061: strError = "Can’t open ENI information"; break;
                case -4062: strError = "Can’t read ENI information"; break;
                case -4063: strError = "Scan the EtherCAT bus error"; break;

                case -4081: strError = "Input wrong EtherCAT master port number"; break;

                case -4091: strError = "Get slave information error"; break;

                case -4101: strError = "Execute sdo upload input data error"; break;
                case -4102: strError = "Input ECAT servo home mode error or wrong data."; break;
                case -4103: strError = "Input ECAT servo home ACC/DEC error or wrong data."; break;
                case -4104: strError = "Input ECAT servo home limit switch error or wrong data."; break;
                case -4105: strError = "Input ECAT servo home zero error or wrong data."; break;
                case -4106: strError = "Input ECAT servo home offset error or wrong data."; break;
                case -4107: strError = "Input ECAT servo home control word error or wrong data."; break;
                case -4108: strError = "Input ECAT servo home stop error or wrong data."; break;
                case -4109: strError = "Input ECAT servo home limit switch error or wrong data."; break;

                case -4110: strError = "Set OP mode error with ECAT home process."; break;

                case -4201: strError = "Input slave number is over the limit"; break;
                case -4202: strError = "Input module number is over the limit"; break;
                case -4203: strError = "Input AI channel number is over the limit"; break;
                case -4204: strError = "Input AO channel number is over the limit"; break;
                case -4205: strError = "Execute sdo download input data error"; break;

                case -4301: strError = "Create memory error when initialized object dictionary"; break;
                case -4302: strError = "Get object dictionary number error"; break;
                case -4303: strError = "The input object dictionary number is the last"; break;
                case -4304: strError = "Get object dictionary description error"; break;
                case -4305: strError = "Get object dictionary description entry error"; break;
                case -4306: strError = "Get object dictionary is pending"; break;

                case -4405: strError = "PDO offset is invalid."; break;

                case -4503: strError = "The slave ID number duplicate occurrence"; break;
                case -4504: strError = "Get slave register error"; break;
                case -4505: strError = "Set slave register error"; break;
                case -4506: strError = "Generate ENI file error while start field bus."; break;
                case -4507: strError = "Load ENI file error while loading from flash."; break;

                case -4600: strError = "FoE download file name is null."; break;
                case -4601: strError = "FoE file open failed"; break;
                case -4602: strError = "Memory allocate failed when creating memory for FoE file."; break;
                case -4603: strError = "Copy FoE file content to buffer failed."; break;
                case -4604: strError = "Slave module without mailbox support"; break;
                case -4605: strError = "FoE file delete failed."; break;
                case -4606: strError = "FoE file download time out"; break;
                case -4607: strError = "Slave module not support FoE download. Without “Bootstrap” state."; break;
                case -4608: strError = "FoE download password not valid."; break;
            }

            return ("ADLINK APS Error!\r\n" + strError + "\r\n\r\nErrorID = " + iErrorID.ToString());
        }
	}
}