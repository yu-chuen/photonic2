/********************************************************************************************************************************************
 * Desc: This class contains the error list which is used to be saved at the Data_ErrorMessage class.
 ********************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class ErrorTable
    {
		//Error Codes
        public enum en_ErrorCode
		{
			NO_ERR = 0,

			ESTOP_PRESS_ERR,
			AIR_PRESSURE_UL_ERR,
			AIR_PRESSURE_LL_ERR,
			SAFETYRELAY_OPEN,
			DOOKLOCK_OPEN,
			MACHINE_INITFAIL,
           
			//Keyence Barcode or Code scanner
			CODE_SCANNER_CONNECT_ERR,
			CODE_SCANNER_READ_ERR,
			CODE_SCANNER_READ_FAILURE,

			//Com TCP 
			COMM_TCP_NOTCONNECTED,
			COMM_TCP_CONNECT_ERR,
			COMM_TCP_WRITE_ERR,
			COMM_TCP_READ_ERR,

			//Serial port 
			COMM_SP_NOTCONNECTED,
			COMM_SP_CONNECT_ERR,
			COMM_SP_WRITE_ERR,
			COMM_SP_READ_ERR,
			COMM_SP_READ_TIMEOUT,

			//Vision Cognex: Unique
			VISION_UNIQUE_READTIMEOUT,
			VISION_UNIQUE_RETURNERROR,
			VISION_UNIQUE_COMMANDEMPTY,
			VISION_UNIQUE_TOKENEMPTY,

            //Vision Keyence
            VISION_KEYENCE_COMMAND_EMPTY,
            VISION_KEYENCE_READ_TIMEOUT,
            VISION_KEYENCE_RETURN_ERROR,
            VISION_KEYENCE_INSUFFICIENT_DATA_RECEIVED,

			//Indexer: Common error
			INDEXER_SLIP,
			INDEXER_NOTCONNECTED,
			INDEXER_ON_ERR,
			INDEXER_ENDLIMIT_P,
			INDEXER_ENDLIMIT_N,
			INDEXER_ALARM,
			INDEXER_EMG,
			INDEXER_TIMEOUT,
			INDEXER_TIMEOUT_START,
			INDEXER_HOMING_TIMEOUT,
			INDEXER_GETSTATUS_ERR,
			INDEXER_UPSETTNGS_ERR,
            INDEXER_GOTOPOS_ERR,
            INDEXER_DOWN_ERR,

			//Safety Window
			SAFETYWINDOW_TOPRIGHT_BREACH_X,
			SAFETYWINDOW_TOPRIGHT_BREACH_Y,
			SAFETYWINDOW_BTMLEFT_BREACH_X,
			SAFETYWINDOW_BTMLEFT_BREACH_Y,

			//Cylinder
			CYL_EXTEND_TIMEOUT,
			CYL_RETRACT_TIMEOUT,
			CYL_INPUTBIT_ERR,
            CYL_INPOS_ERR,
            CYL_UNKNOWN_STATE,

			//IFM ERROR
			IFM_CONNECT_FAIL,
			IFM_UNABLE_SET1ST_PARAM,
			IFM_UNABLE_SET2ND_PARAM,
			IFM_RETURN_EMPTY_STRING,
			IFM_SENDDATA_TIMEOUT,
			IFM_WRITE_ERROR,

			//Server
			SERVER_RETURN_ERR,
			SERVER_CONNECTION_ERR,
			SERVER_RECORD_NONEXIST,
			SERVER_READER_ERR,
			SERVER_QUERY_ERR,
			SERVER_DUPLICATEID,
			SERVER_NOTALLOWEDTORUN,
			SERVER_PREVSTAT_FAIL,
            SERVER_RESULT_NOT_ENOUGH_DATA,

			//Soldering Device: Unix
			SOLDER_UNIX_SOLDERALARM,
			SOLDER_UNIX_HEATERALARM,
			SOLDER_UNIX_HEATEROFF,
			SOLDER_UNIX_NOTREADY,
			SOLDER_UNIX_START_TIMEOUT,
			SOLDER_UNIX_RUN_TIMEOUT,
            SOLDER_IRON_HEAD_CURRENT_USE_COUNT_EXCEEDED_THRESHOLD,
            SOLDER_LEAD_CURRENT_USED_LENGTH_EXCEEDED_THRESHOLD,

			//Hot Blower
			HOTBLOWER_ALARM,
            HOTBLOWER_TEMPREADFAIL,
            HOTBLOWER_RUN_TIMEOUT,

			//Wire Tester
			WIRETESTER_ERROR,
			WIRETESTER_HIGH,
			WIRETESTER_LOW,
			WIRETESTER_TIMEOUT,
            WIRETESTER_UNKNOWNCMD,
            WIRETESTER_DECODER,

			//Interlocks
			INTERLOCK_SOLDERIRONDN,
			INTERLOCK_SOLDERCLAMPDN,
			INTERLOCK_SOLDERCLEANER,
			INTERLOCK_SOLDER_ROTARY,
			INTERLOCK_VI_LIGHTCOLLISION,
			INTERLOCK_VI_WTCOLLISION,
            INTERLOCK_CUSTOMCMDINPROGRESS,
            INTERLOCK_GANTRYROTATE,
            INTERLOCK_VI_OP120GripperDownCOLLISION,
            INTERLOCK_VI_OP120NestUpCOLLISION,
            INTERLOCK_GANTRYZAXIS,
            INTERLOCK_REFLECTOROUT,
            INTERLOCK_CYLINDER_COLLISION,

            //Safety Curtain
            SAFETYCURTAIN_ON,

            //Part absent
            SENSOR_PARTABSENT,

            //Pressure switch OP120
            PRESSURESWITCH_UPPERLIMIT,
            OP120VACUUM_RUN_TIMEOUT,

            //Keyance IL
            KEYENCE_IL_CONNECT_ERR,
            KEYENCE_IL_READ_ERR,
            KEYENCE_IL_READ_FAILURE,
            KEYENCE_IL_READ_TIMEOUT,
            KEYANCE_IL_FAIL,

            //Sensor
            SENSOR_TRIGGERED_ERR,
            SENSOR_NOT_TRIGGERED_ERR,

            //MISC
            LV_HV_MODE_DETECTION_FAIL,
            VISION_FAIL_LIMIT_REACHED,
		}
		
		//Error Desc / message//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public String Get_ErrorMessage(int iErrorID)
		{
			String strMsg = "";
			
			switch (iErrorID)
			{
				case (int)en_ErrorCode.NO_ERR:													strMsg = "";break;

				case (int) en_ErrorCode.MACHINE_INITFAIL:								strMsg = "Machine is not Initialized. "; break;
				case (int) en_ErrorCode.ESTOP_PRESS_ERR:						strMsg = "EStop Button Pressed"; break;
				case (int) en_ErrorCode.AIR_PRESSURE_UL_ERR:				strMsg = "Main Air Pressure too High"; break;
				case (int) en_ErrorCode.AIR_PRESSURE_LL_ERR:					strMsg = "Main Air Pressure too Low"; break;
				case (int) en_ErrorCode.SAFETYRELAY_OPEN:						strMsg = "Safety Relay is Opened."; break;
				case (int) en_ErrorCode.DOOKLOCK_OPEN:							strMsg = "Door Open"; break;

				case (int) en_ErrorCode.CODE_SCANNER_CONNECT_ERR:			strMsg = "Code Scanner Connection Failure"; break;
				case (int) en_ErrorCode.CODE_SCANNER_READ_ERR:					strMsg = "Code Scanner Read Return Error"; break;
				case (int) en_ErrorCode.CODE_SCANNER_READ_FAILURE:			strMsg = "Code Scanner Read Trigger Failure"; break;

				case (int) en_ErrorCode.COMM_TCP_NOTCONNECTED:		strMsg = "TCP Port is not Connected"; break;
				case (int) en_ErrorCode.COMM_TCP_CONNECT_ERR:			strMsg = "TCP Connection Failure"; break;
				case (int) en_ErrorCode.COMM_TCP_WRITE_ERR:					strMsg = "TCP Read Failure"; break;
				case (int) en_ErrorCode.COMM_TCP_READ_ERR:					strMsg = "TCP Write Failure"; break;

				case (int) en_ErrorCode.COMM_SP_NOTCONNECTED:			strMsg = "COMM Serial Port is not Connected"; break;
				case (int) en_ErrorCode.COMM_SP_CONNECT_ERR:			strMsg = "COMM Serial Port Connection Failure"; break;
				case (int) en_ErrorCode.COMM_SP_WRITE_ERR:					strMsg = "COMM Serial Port Read Failure"; break;
				case (int) en_ErrorCode.COMM_SP_READ_ERR:					strMsg = "COMM Serial Port Write Failure"; break;
				case (int) en_ErrorCode.COMM_SP_READ_TIMEOUT:			strMsg = "COMM Serial Port Read TimeOut"; break;
				
				case (int) en_ErrorCode.VISION_UNIQUE_READTIMEOUT:			strMsg = "Vision System Response Time Out"; break;
				case (int) en_ErrorCode.VISION_UNIQUE_RETURNERROR:		strMsg = "Vision System Error"; break;
				case (int) en_ErrorCode.VISION_UNIQUE_COMMANDEMPTY:	strMsg = "Vision System Send Command: Error: Command is empty"; break;
				case (int) en_ErrorCode.VISION_UNIQUE_TOKENEMPTY:			strMsg = "Vision System Send Command: Error: Tracking token is empty"; break;

                case (int)en_ErrorCode.VISION_KEYENCE_COMMAND_EMPTY: strMsg = "Vision System (Keyence) Error: Command is empty"; break;
                case (int)en_ErrorCode.VISION_KEYENCE_READ_TIMEOUT: strMsg = "Vision System (Keyence) Response Timeout"; break;
                case (int)en_ErrorCode.VISION_KEYENCE_RETURN_ERROR: strMsg = "Vision System (Keyence) Error"; break;
                case (int)en_ErrorCode.VISION_KEYENCE_INSUFFICIENT_DATA_RECEIVED: strMsg = "Vision System (Keyence) insufficient data received."; break;

				case (int) en_ErrorCode.INDEXER_SLIP:									strMsg = "Indexer did not reach its intended position"; break;
				case (int) en_ErrorCode.INDEXER_NOTCONNECTED:			strMsg = "Indexer is not connected."; break;
				case (int) en_ErrorCode.INDEXER_ENDLIMIT_P:						strMsg = "Forward \\ Positive End Limit Reached"; break;
				case (int) en_ErrorCode.INDEXER_ENDLIMIT_N:						strMsg = "Reverse \\ Negative End Limit Reached"; break;
				case (int) en_ErrorCode.INDEXER_ALARM:								strMsg = "Motor Alarm ON"; break;
				case (int) en_ErrorCode.INDEXER_EMG:									strMsg = "Motor Emergency Signal ON"; break;		
				case (int) en_ErrorCode.INDEXER_TIMEOUT_START:				strMsg = "Start Movement Timeout"; break;					
				case (int) en_ErrorCode.INDEXER_TIMEOUT:							strMsg = "Movement Timeout. "; break;
				case (int) en_ErrorCode.INDEXER_HOMING_TIMEOUT:			strMsg = "Homing Timeout"; break;
				case (int) en_ErrorCode.INDEXER_ON_ERR:							strMsg = "Not Turned On"; break;
				case (int) en_ErrorCode.INDEXER_GETSTATUS_ERR:			strMsg = "Get Status Error"; break;
				case (int) en_ErrorCode.INDEXER_UPSETTNGS_ERR:			strMsg = "Update Setting Failed. "; break;
                case (int) en_ErrorCode.INDEXER_GOTOPOS_ERR: strMsg = "Motor fail to go to position."; break;
                case (int)en_ErrorCode.INDEXER_DOWN_ERR: strMsg = "Blower not allowed to down."; break;

				case (int) en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_X:	strMsg = "Safety Window Breached. Top Right X"; break;
				case (int) en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_Y:		strMsg = "Safety Window Breached. Top Right Y"; break;
				case (int) en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_X:		strMsg = "Safety Window Breached. Bottom Left X"; break;
				case (int) en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_Y:		strMsg = "Safety Window Breached. Bottom Left Y"; break;

				case (int) en_ErrorCode.CYL_EXTEND_TIMEOUT:					strMsg = "Cylinder Extend Timeout: "; break;
				case (int) en_ErrorCode.CYL_RETRACT_TIMEOUT:				strMsg = "Cylinder Retract Timeout: "; break;
				case (int) en_ErrorCode.CYL_INPUTBIT_ERR:						strMsg = "Cylinder's input bit error: "; break;
                case (int) en_ErrorCode.CYL_INPOS_ERR:                      strMsg = "Cylinder in position error: "; break;
                case (int)en_ErrorCode.CYL_UNKNOWN_STATE: strMsg = "Cylinder position is unknown error: "; break;
				
				case (int) en_ErrorCode.IFM_CONNECT_FAIL:							strMsg = "IFM RFID Reader: Unable to connect to reader."; break;
				case (int) en_ErrorCode.IFM_UNABLE_SET1ST_PARAM:		strMsg = "IFM RFID Reader: Unable to set first parameter."; break;
				case (int) en_ErrorCode.IFM_UNABLE_SET2ND_PARAM:		strMsg = "IFM RFID Reader: Unable to set second parameter."; break;
				case (int) en_ErrorCode.IFM_RETURN_EMPTY_STRING:		strMsg = "IFM RFID Reader: Reader returns empty string"; break;
				case (int) en_ErrorCode.IFM_SENDDATA_TIMEOUT:				strMsg = "IFM RFID Reader: Write data to RFID Tag respond timeout."; break;
				case (int) en_ErrorCode.IFM_WRITE_ERROR:							strMsg = "IFM RFID Reader: Write to Tag Failed."; break;
										
				case (int) en_ErrorCode.SERVER_RETURN_ERR:						strMsg = "Server return Error"; break;
				case (int) en_ErrorCode.SERVER_CONNECTION_ERR:			strMsg = "Server Error: Unable to connect to server"; break;
				case (int) en_ErrorCode.SERVER_RECORD_NONEXIST:		strMsg = "Server Error: Record does not exist in the database"; break;
				case (int) en_ErrorCode.SERVER_READER_ERR:					strMsg = "Server Error: Read from database failure"; break;
				case (int) en_ErrorCode.SERVER_QUERY_ERR:						strMsg = "Server Error: Query database failed"; break;
				case (int) en_ErrorCode.SERVER_DUPLICATEID:					strMsg = "Server Error: Detected duplicate ID within the records."; break;
				case (int) en_ErrorCode.SERVER_NOTALLOWEDTORUN:		strMsg = "Server Return: Not Allowed to Run."; break;
                case (int)en_ErrorCode.SERVER_RESULT_NOT_ENOUGH_DATA: strMsg = "Insufficient data received."; break;
										
				case (int) en_ErrorCode.SOLDER_UNIX_SOLDERALARM:	strMsg = "SolderDev Unix: Solder alarm"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_HEATERALARM:		strMsg = "SolderDev Unix: Heater alarm"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_NOTREADY:				strMsg = "SolderDev Unix: Not Ready"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_START_TIMEOUT:	strMsg = "SolderDev Unix: Timeout. Device has not started within the allocated time"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_RUN_TIMEOUT:		strMsg = "SolderDev Unix: Timeout. Device run time timeout."; break;
				case (int) en_ErrorCode.SOLDER_UNIX_HEATEROFF:			strMsg = "SolderDev Unix: Heater is turned off."; break;
                case (int)en_ErrorCode.SOLDER_IRON_HEAD_CURRENT_USE_COUNT_EXCEEDED_THRESHOLD: strMsg = "Solder Iron Head has exceeded the use count threshold."; break;
                case (int)en_ErrorCode.SOLDER_LEAD_CURRENT_USED_LENGTH_EXCEEDED_THRESHOLD: strMsg = "Solder Lead has exceeded the used length threshold."; break;

				case (int) en_ErrorCode.HOTBLOWER_ALARM:						strMsg = "Hot Blower: Alarm"; break;
                case (int) en_ErrorCode.HOTBLOWER_TEMPREADFAIL: strMsg = "Hot Blower: Temperature fail to read."; break;
                case (int)en_ErrorCode.HOTBLOWER_RUN_TIMEOUT: strMsg = "Hot Blower: Timeout. Device run time timeout."; break;

				case (int) en_ErrorCode.WIRETESTER_ERROR:						strMsg = "Wire Tester: Device output bit Error high"; break;
				case (int) en_ErrorCode.WIRETESTER_HIGH:							strMsg = "Wire Tester: Measurement High"; break;
				case (int) en_ErrorCode.WIRETESTER_LOW:							strMsg = "Wire Tester: Measurement Low"; break;
				case (int) en_ErrorCode.WIRETESTER_TIMEOUT:					strMsg = "Wire Tester: TimeOut. End of measurement signal was not received within the appointed time."; break;
                case (int) en_ErrorCode.WIRETESTER_UNKNOWNCMD:          strMsg = "Wire Tester: Unknown response from the Wire tester device."; break;
                case (int) en_ErrorCode.WIRETESTER_DECODER:           strMsg = "Wire Tester: Decoder Error."; break;

				case (int) en_ErrorCode.INTERLOCK_SOLDERIRONDN:			strMsg = "Interlock: Not allowed to move gantry when solder device is down.";break;
				case (int) en_ErrorCode.INTERLOCK_SOLDERCLAMPDN:		    strMsg = "Interlock: Not allowed to move gantry to position when the clamper is not down."; break;
				case (int) en_ErrorCode.INTERLOCK_SOLDERCLEANER:	    strMsg = "Interlock: Not allowed to move gantry to position when the cleaning module is extended."; break;
				case (int) en_ErrorCode.INTERLOCK_VI_LIGHTCOLLISION:	strMsg = "Interlock: Not allowed to extend cylinder. Vision Light is extended"; break;
				case (int) en_ErrorCode.INTERLOCK_VI_WTCOLLISION:		strMsg = "Interlock: Not allowed to extend cylinder. Wire Tester is extended"; break;
                case (int)en_ErrorCode.INTERLOCK_SOLDER_ROTARY: strMsg = "Interlock: Not allowed to run because the rotary is rotated."; break;
                case (int) en_ErrorCode.INTERLOCK_CUSTOMCMDINPROGRESS:  strMsg = "Interlock: Process is still running. Not allow to duplicate the process."; break;
                case (int) en_ErrorCode.INTERLOCK_GANTRYROTATE: strMsg = "Interlock: Not allowed to rotate gantry. Gantry is down."; break;
                case (int) en_ErrorCode.INTERLOCK_VI_OP120GripperDownCOLLISION: strMsg = "Interlock: Not allowed to extend cylinder. gripper cylinder is down"; break;
                case (int) en_ErrorCode.INTERLOCK_VI_OP120NestUpCOLLISION: strMsg = "Interlock: Not allowed to extend cylinder. Nest cylinder is up"; break;
                case (int) en_ErrorCode.SAFETYCURTAIN_ON: strMsg = "Sequence STOP due to human factors."; break;
                case (int) en_ErrorCode.INTERLOCK_GANTRYZAXIS: strMsg = "Interlock: Not allow to move Gantry. Z axis is not down."; break;
                case (int) en_ErrorCode.INTERLOCK_REFLECTOROUT: strMsg = "Interlock: Not allow to move reflector out. Reflector is up."; break;
                case (int)en_ErrorCode.INTERLOCK_CYLINDER_COLLISION: strMsg = "Interlock: Not allowed to move cylinder."; break;

                case (int) en_ErrorCode.SENSOR_PARTABSENT: strMsg = "Part detects absent."; break;

                case (int)en_ErrorCode.PRESSURESWITCH_UPPERLIMIT: strMsg = "Pressure switch OP120 reach upper limit."; break;
                case (int)en_ErrorCode.OP120VACUUM_RUN_TIMEOUT: strMsg = "OP120: Timeout. Vacuum run time timeout."; break;
                    
                case (int)en_ErrorCode.KEYENCE_IL_CONNECT_ERR: strMsg = "Keyence IL Connection Failure"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_ERR: strMsg = "Keyence IL Read return Error"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_FAILURE: strMsg = "Keyence IL Read Trigger Failure"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_TIMEOUT: strMsg = "Keyence IL Read Timeout"; break;
                case (int)en_ErrorCode.KEYANCE_IL_FAIL: strMsg = "Keyence IL height check fail"; break;

                case (int)en_ErrorCode.SENSOR_TRIGGERED_ERR: strMsg = "Sensor triggered error"; break;
                case (int)en_ErrorCode.SENSOR_NOT_TRIGGERED_ERR: strMsg = "Sensor not triggered error"; break;

                case (int)en_ErrorCode.LV_HV_MODE_DETECTION_FAIL: strMsg = "Pallet RFID Serial Number does not contain either LV or HV."; break;
                case (int)en_ErrorCode.VISION_FAIL_LIMIT_REACHED: strMsg = "Vision had failed consecutively and reached the limit."; break;
                    
				default: strMsg = "unknown error"; break;
			}

			return strMsg;
		}

		//Error recovery message//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public String Get_ErrorRecovery(int iErrorID)
		{
			String strMsg = "";

			switch (iErrorID)
			{
				case (int) en_ErrorCode.MACHINE_INITFAIL:						strMsg = "Please initialize the machine first before continuing "; break;
				case (int) en_ErrorCode.ESTOP_PRESS_ERR:						strMsg = "Please Unlock the EMG button, press the Reset button, initialize the machine"; break;
				case (int) en_ErrorCode.AIR_PRESSURE_UL_ERR:					strMsg = "Please Check Main air pressure inlet"; break;
				case (int) en_ErrorCode.AIR_PRESSURE_LL_ERR:					strMsg = "Please Check Main air pressure inlet"; break;
				case (int) en_ErrorCode.SAFETYRELAY_OPEN:						strMsg = "Please press the Reset Button and then initialize the machine"; break;
				case (int) en_ErrorCode.DOOKLOCK_OPEN:							strMsg = "Please close the door and initialize the machine."; break;

				case (int) en_ErrorCode.CODE_SCANNER_CONNECT_ERR:			    strMsg = "Please check the code scanner connection"; break;
				case (int) en_ErrorCode.CODE_SCANNER_READ_ERR:					strMsg = "Please make sure that the code is within the scanning range"; break;
				case (int) en_ErrorCode.CODE_SCANNER_READ_FAILURE:			    strMsg = "Please check the Code Scanner connection and reading function via maintenance page"; break;
				
				case (int) en_ErrorCode.COMM_TCP_CONNECT_ERR:			        strMsg = "Please Check the TCP Port via Maintenance page."; break;
				case (int) en_ErrorCode.COMM_TCP_WRITE_ERR:					    strMsg = "Please Check the TCP Port via Maintenance page."; break;
				case (int) en_ErrorCode.COMM_TCP_READ_ERR:					    strMsg = "Please Check the TCP Port via Maintenance page."; break;
				
				case (int) en_ErrorCode.COMM_SP_CONNECT_ERR:			        strMsg = "Please Check the Serial Port via Maintenance page."; break;
				case (int) en_ErrorCode.COMM_SP_WRITE_ERR:					    strMsg = "Please Check the Serial Port via Maintenance page."; break;
				case (int) en_ErrorCode.COMM_SP_READ_ERR:					    strMsg = "Please Check the Serial Port via Maintenance page."; break;
				case (int) en_ErrorCode.COMM_SP_READ_TIMEOUT:			        strMsg = "Please Check the Serial Port via Maintenance page."; break;

                case (int)en_ErrorCode.VISION_UNIQUE_READTIMEOUT: strMsg = "Please check Vision system feedback via manual page."; break;
                case (int)en_ErrorCode.VISION_UNIQUE_RETURNERROR: strMsg = "Please check Vision system feedback via manual page."; break;
                case (int)en_ErrorCode.VISION_UNIQUE_COMMANDEMPTY: strMsg = "Please check Vision system feedback via manual page."; break;
                case (int)en_ErrorCode.VISION_UNIQUE_TOKENEMPTY: strMsg = "Please check Vision system feedback via manual page."; break;

                case (int)en_ErrorCode.VISION_KEYENCE_COMMAND_EMPTY: strMsg = "Please check vision system via Maintenance page."; break;
                case (int)en_ErrorCode.VISION_KEYENCE_READ_TIMEOUT: strMsg = "Please check vision system via Maintenance page."; break;
                case (int)en_ErrorCode.VISION_KEYENCE_RETURN_ERROR: strMsg = "Please check vision system via Maintenance page."; break;
                case (int)en_ErrorCode.VISION_KEYENCE_INSUFFICIENT_DATA_RECEIVED: strMsg = "Please check vision system via Maintenance page."; break;

                case (int)en_ErrorCode.INDEXER_SLIP: strMsg = "Please check mechanical gantry does not have any collision. Please check manual jog gantry movement is smooth. Then initialize the motor."; break;
				case (int) en_ErrorCode.INDEXER_NOTCONNECTED:			        strMsg = "Please connect to the indexer first before continuing"; break;
				case (int) en_ErrorCode.INDEXER_ENDLIMIT_P:						strMsg = "Please Check the motor position. Then Initialize the motor"; break;
				case (int) en_ErrorCode.INDEXER_ENDLIMIT_N:						strMsg = "Please Check the motor position. Then Initialize the motor"; break;
				case (int) en_ErrorCode.INDEXER_ALARM:							strMsg = "Please inspect the motor. Then Initialize the motor"; break;
				case (int) en_ErrorCode.INDEXER_EMG:							strMsg = "Please inspect the motor. Then Initialize the motor"; break;		
				case (int) en_ErrorCode.INDEXER_TIMEOUT_START:				    strMsg = "Please check the motor via maintenance"; break;							
				case (int) en_ErrorCode.INDEXER_TIMEOUT:						strMsg = "Please check the motor via maintenance"; break;
				case (int) en_ErrorCode.INDEXER_HOMING_TIMEOUT:			        strMsg = "Please check the motor via maintenance"; break;
				case (int) en_ErrorCode.INDEXER_ON_ERR:							strMsg = "Please inspect the motor driver via its related Setup Software."; break;
				case (int) en_ErrorCode.INDEXER_UPSETTNGS_ERR:			        strMsg = "Please Check the Ethercat card for any error."; break;
                case (int) en_ErrorCode.INDEXER_GOTOPOS_ERR:                    strMsg = "Please go to maintenance page to turn on the motor or initialize the motor first."; break;
                case (int)en_ErrorCode.INDEXER_DOWN_ERR:                        strMsg = "Please move to appraoch position."; break;
                    
				case (int) en_ErrorCode.CYL_EXTEND_TIMEOUT:					strMsg = "Please check the cylinder."; break;
				case (int) en_ErrorCode.CYL_RETRACT_TIMEOUT:				    strMsg = "Please check the cylinder."; break;
				case (int) en_ErrorCode.CYL_INPUTBIT_ERR:						strMsg = "Please check if the IO card is connected."; break;
                case (int)en_ErrorCode.CYL_INPOS_ERR: strMsg = "Please check the cylinder."; break;
                case (int)en_ErrorCode.CYL_UNKNOWN_STATE: strMsg = "Please check the cylinder."; break;
					
				case (int) en_ErrorCode.IFM_CONNECT_FAIL:						strMsg = "IFM RFID Reader: Please Check the RFID via maintenance window."; break;
				case (int) en_ErrorCode.IFM_UNABLE_SET1ST_PARAM:		        strMsg = "IFM RFID Reader: Please Check the RFID via maintenance window"; break;
				case (int) en_ErrorCode.IFM_UNABLE_SET2ND_PARAM:		        strMsg = "IFM RFID Reader: Please Check the RFID via maintenance window"; break;
				case (int) en_ErrorCode.IFM_SENDDATA_TIMEOUT:				    strMsg = "IFM RFID Reader: Please Check the RFID via maintenance window"; break;
				case (int) en_ErrorCode.IFM_WRITE_ERROR:					    strMsg = "IFM RFID Reader: Please Check the RFID via maintenance window"; break;
				case (int) en_ErrorCode.IFM_RETURN_EMPTY_STRING:		        strMsg = "IFM RFID Reader: Please check if the pallet is align and within the Reader's range"; break;
											
				case (int) en_ErrorCode.SERVER_RETURN_ERR:						strMsg = "Please check with the server."; break;
				case (int) en_ErrorCode.SERVER_CONNECTION_ERR:			        strMsg = "Please ensure that the server is running and connected to the network"; break;
				case (int) en_ErrorCode.SERVER_RECORD_NONEXIST:		            strMsg = "Please ensure that the record exist in the database before attempting to read or query from it"; break;
				case (int) en_ErrorCode.SERVER_READER_ERR:					    strMsg = "Please check the database PC."; break;
				case (int) en_ErrorCode.SERVER_QUERY_ERR:						strMsg = "Please check the database PC"; break;
				case (int) en_ErrorCode.SERVER_DUPLICATEID:					    strMsg = "Please remove the duplicate record from the database manually"; break;
				case (int) en_ErrorCode.SERVER_NOTALLOWEDTORUN:		            strMsg = "Please reject this part."; break;
                case (int)en_ErrorCode.SERVER_RESULT_NOT_ENOUGH_DATA: strMsg = "Please check with the server."; break;

				case (int) en_ErrorCode.SOLDER_UNIX_SOLDERALARM:	            strMsg = "Please check the solder unit"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_HEATERALARM:		        strMsg = "Please check the solder unit"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_NOTREADY:				    strMsg = "Please check the solder unit"; break;
				case (int) en_ErrorCode.SOLDER_UNIX_START_TIMEOUT:				    strMsg = "Please check the solder unit"; break;
                case (int) en_ErrorCode.SOLDER_UNIX_RUN_TIMEOUT:				    strMsg = "Please check the solder unit"; break;
                case (int) en_ErrorCode.SOLDER_UNIX_HEATEROFF:			        strMsg = "Please turn the heater On before continuing."; break;
                case (int)en_ErrorCode.SOLDER_IRON_HEAD_CURRENT_USE_COUNT_EXCEEDED_THRESHOLD: strMsg = "Please change the solder iron head before continuing."; break;
                case (int)en_ErrorCode.SOLDER_LEAD_CURRENT_USED_LENGTH_EXCEEDED_THRESHOLD: strMsg = "Please change the solder lead before continuing."; break;
					
				case (int) en_ErrorCode.HOTBLOWER_ALARM:						strMsg = "Please check the Hot blower device";break;
                case (int) en_ErrorCode.HOTBLOWER_TEMPREADFAIL:                 strMsg = "Please check the thermocouple setting in Maintenance page"; break;
                case (int)en_ErrorCode.HOTBLOWER_RUN_TIMEOUT: strMsg = "Please make sure thermocouple and hot blower devices are function"; break;
                    
				case (int) en_ErrorCode.WIRETESTER_ERROR:						strMsg = "Please check the wire tester or make sure that the wire tester pins are connected properly to the target."; break;
				case (int) en_ErrorCode.WIRETESTER_HIGH:					    strMsg = "Please check the Wire Tester device"; break;
                case (int)en_ErrorCode.WIRETESTER_LOW: strMsg = "Please check the Wire Tester device"; break;
				case (int) en_ErrorCode.WIRETESTER_TIMEOUT:					    strMsg = "Please check the wire tester for any errors or increase the wait time."; break;
                case (int) en_ErrorCode.WIRETESTER_UNKNOWNCMD:                  strMsg = "Please check the wire tester settings at the wire tester unit."; break;
                case (int)en_ErrorCode.WIRETESTER_DECODER: strMsg = "Please check the Wire Tester device"; break;
                    
				case (int) en_ErrorCode.INTERLOCK_SOLDERIRONDN:			        strMsg = "Please release the down bit first before moving the gantry";break;
				case (int) en_ErrorCode.INTERLOCK_SOLDERCLAMPDN:		        strMsg = "Please clamp the clamper before continuing."; break;
				case (int) en_ErrorCode.INTERLOCK_SOLDERCLEANER:	            strMsg = "Please retract the cleaning module before continuing."; break;
				case (int) en_ErrorCode.INTERLOCK_VI_LIGHTCOLLISION:	        strMsg = "Please retract the related cylinder before continuing."; break;
				case (int) en_ErrorCode.INTERLOCK_VI_WTCOLLISION:		        strMsg = "Please retract the related cylinder before continuing."; break;
                case (int) en_ErrorCode.INTERLOCK_CUSTOMCMDINPROGRESS:          strMsg = "Please wait until the process is complete before attempting the next process."; break;
                case (int) en_ErrorCode.INTERLOCK_GANTRYROTATE:                 strMsg = "Please wait until gantry move up."; break;
                case (int) en_ErrorCode.INTERLOCK_VI_OP120NestUpCOLLISION:      strMsg = "Please retract the Nest cylinder before continuing."; break;
                case (int) en_ErrorCode.INTERLOCK_VI_OP120GripperDownCOLLISION: strMsg = "Please extend the gripper cylinder before continuing."; break;
                case (int) en_ErrorCode.SAFETYCURTAIN_ON:                       strMsg = "Please make sure no interruption during the process."; break;
                case (int) en_ErrorCode.INTERLOCK_GANTRYZAXIS:                  strMsg = "Please extend the Z axis before continuing."; break;
                case (int) en_ErrorCode.INTERLOCK_REFLECTOROUT:                 strMsg = "Please move reflector down before continuing."; break;
                case (int)en_ErrorCode.INTERLOCK_CYLINDER_COLLISION: strMsg = "Please check the cylinder movement path before continuing."; break;

                case (int) en_ErrorCode.SENSOR_PARTABSENT:                      strMsg = "Please initialise the station and make sure part is present."; break;
                case (int)en_ErrorCode.OP120VACUUM_RUN_TIMEOUT: strMsg = "Please check the vacuum at OP120"; break;
                    
                case (int)en_ErrorCode.KEYENCE_IL_CONNECT_ERR: strMsg = "Please check the Keyence IL connection"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_ERR: strMsg = "Please make sure that the height is within the scanning range"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_FAILURE: strMsg = "Please check the Keyence IL connection and  make sure the device is able to read value via maintenance page"; break;
                case (int)en_ErrorCode.KEYENCE_IL_READ_TIMEOUT: strMsg = "Please check the Keyence IL connection and  make sure the device is able to read value via maintenance page"; break;

                case (int)en_ErrorCode.SENSOR_TRIGGERED_ERR: strMsg = "Please check the sensor status."; break;
                case (int)en_ErrorCode.SENSOR_NOT_TRIGGERED_ERR: strMsg = "Please check the sensor status."; break;

                case (int)en_ErrorCode.LV_HV_MODE_DETECTION_FAIL: strMsg = "Please check the pallet RFID via Maintenance page."; break;
                case (int)en_ErrorCode.VISION_FAIL_LIMIT_REACHED: strMsg = "Please check the vision result."; break;

				default: strMsg = ""; break;
			}

			return strMsg;
		}

    }
}
