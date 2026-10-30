using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

using APS_Define_W32;
using APS168_W32;

namespace StaticCoreLib
{
	public class Indexer_APSlib_PanasonicECAT
	{
		//variables
		public Stepper_Settings m_Settings = null;
		APS168_Ethercat_PCIcard m_PCICard = null;

		//vars
		public int m_iAxis_ID = 0;
		public int m_iErrorID = 0;
		public String m_strErrorMessage = "";
		public String m_strLabel = "";
		public String m_strFilePath = "";

		private int m_iCurrentRunSpeed = 100;
		private double m_dbCurrentPosition = 0;

		//status / state
		private int m_iSvon = 0;
		private int m_iOrg = 0;
		private int m_iHoming = 0;
		private int m_iAlarm = 0;
		private int m_iEL_posivite = 0;
		private int m_iEL_negative = 0;
		private int m_iMoveDone = 0;
		private int m_iMoving = 0;
		private int m_iEmg = 0;
		private int m_iBusy = 0;

		//lock
		private readonly object m_lock_Access = new object();

		//constructor/////////////////////////////////////////////////////////////////////////////////////////////
		public Indexer_APSlib_PanasonicECAT( int iAxisID, String strLabel, String strFilePath, APS168_Ethercat_PCIcard mCard )
		{
			m_Settings = new Stepper_Settings();

			m_iAxis_ID = iAxisID;

			//set naming and filepaths
			m_strLabel = strLabel;
			m_strFilePath = strFilePath; if ( m_strFilePath.EndsWith("\\") == false ) m_strFilePath += "\\";
			//set PCI card reference
			m_PCICard = mCard;

			//get settings
			From_Registry();
		}

		//Update the Stepper motor with the new settings AND save it to xml file/////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool To_Registry()
		{
            bool bRet = true;

            //update to xml file
			String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
			XMLSerializer.Serialize_Func<Stepper_Settings>(m_Settings, strFilePathFull);

            Set_Settings(m_Settings);

            //update motor settings
            if (!Update_MotorDriverSettings()) { if (bRet) bRet = false; }

            return bRet;
		}

		//Get indexer settings parameters. Read from xml file////////////////////////////////////////////////////
		public bool From_Registry()
		{
            bool bRet = true;

            //read settings from xml file
			String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
			m_Settings = XMLSerializer.Deserialize_Func<Stepper_Settings>(strFilePathFull);

            //set to settings
            Set_Settings(m_Settings);

            //update motor settings
            if (!Update_MotorDriverSettings()) { Set_Error(m_strLabel + " Indexer"); if (bRet) bRet = false; }

            return bRet;
		}

		//Get Status/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Svon_i           { get { return m_iSvon; } }
        public int Homing_i         { get { return m_iHoming; } }
        public int Home_i           { get { return m_iOrg; } }
        public int Alarm_i          { get { return m_iAlarm; } }
        public int EL_posivite_i    { get { return m_iEL_posivite; } }
        public int EL_negative_i    { get { return m_iEL_negative; } }
        public int MoveDone_i       { get { return m_iMoveDone; } }
        public int Moving_i         { get { return m_iMoving; } }
        public int EMG_i            { get { return m_iEmg; } }
        public int Busy_i           { get { return m_iBusy; } }

		//Set Motor Settings////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_Settings( Stepper_Settings mSettings ) { m_Settings = mSettings; }

		//Get Error Code/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID() 
        {
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID; 
		}

		public String Get_ErrorMessage()
		{
			String strErrorMsg = m_strErrorMessage;
			m_strErrorMessage = "";
			return strErrorMsg;
		}

        /*****************************************************************************************************************
		 * Support functions
		 *****************************************************************************************************************/
        //Set Error/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Error(String strAddErrorMsg)
        {
            m_iErrorID = Get_ErrorID();
            m_strErrorMessage = Get_ErrorMessage();

            //Set Additional error message
            m_strErrorMessage += strAddErrorMsg;
        }

        public void Set_Error(String strAddErrorMsg, int iError)
        {
            m_iErrorID = iError;
            m_strErrorMessage = strAddErrorMsg;
        }
        

		//Update Motor Settings///////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Update_MotorDriverSettings()
		{
			int iRet = 0;
			String strError = "";

			lock ( m_lock_Access )
			{
				//is connected
				if ( IsConnected() == false ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

				for ( int iCount = 0; iCount < 10; iCount++ )
				{
					switch ( iCount )
					{
						case 0: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_MODE, 0); strError = "Set Param: Home mode to 0 Set Error"; break;
						case 1: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_DIR, m_Settings.m_iHomeDir); strError = " Set Param: Home Direction Set Error"; break;
						case 2: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_CURVE, 0); strError = " Set Param: Home Curve Setting Error"; break;
						case 3: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_ACC, m_Settings.m_iHomeAcceleration); strError = " Set Param: Home Acceleration Set Error"; break;
						case 4: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_VM, m_Settings.m_iHomeVelocity); strError = " Set Param: Home VM Set Error"; break;
						case 5: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_VO, m_Settings.m_iHomeVO); strError = " Set Param: Home VO Set Error"; break;
						//case 6: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_HOME_POS, 0) ; strError = " Set Param: Home Position Set Error"; break;
						case 7: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_ACC, m_Settings.m_iAcceleration); strError = "Set Param: Acceleration Set Error"; break;
						case 8: iRet = APS168.APS_set_axis_param(m_iAxis_ID, ( Int32 ) APS_Define.PRA_DEC, m_Settings.m_iDeceleration); strError = " Set Param: Decceleration Set Error"; break;
						case 9:
							double dbVal = (double) ( ( m_Settings.m_iVelocity / 100 ) * m_Settings.m_iMainSpeed_Multiplier );
							iRet = APS168.APS_set_axis_param_f (m_iAxis_ID, ( Int32 ) APS_Define.PRA_VM, dbVal);
							strError = "Set Param: Velocity Set Error: Speed Adjusted = " + dbVal.ToString();  
							break;
					}

					if ( iRet < 0 )
					{
						m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_UPSETTNGS_ERR;
						m_strErrorMessage = strError + "\r\nPCI Error ID=" + iRet.ToString() + ": " + m_PCICard.Get_ErrorMessage(iRet);
						return false;

					}
				}
			}

			return true;
		}



		/*****************************************************************************************************************
		 * Core Movement
		 *****************************************************************************************************************/
		//Core: Turn on Motor/////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool core_ServoOn(bool bOn)
		{
			//check PICe Card has been init and connected
			if ( IsConnected() == false ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

			lock ( m_lock_Access )
			{
				//Set Servo
				if ( bOn == true )
					m_iErrorID = APS168.APS_set_servo_on(m_iAxis_ID, 1);
				else
					m_iErrorID = APS168.APS_set_servo_on(m_iAxis_ID, 0);

				//Error check
				if ( m_iErrorID != 0 ) return false;
			}

			return true;
		}

		//Core: Homing//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool core_Home()
		{
			//check PICe Card has been init and connected
			if ( IsConnected() == false ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

			lock ( m_lock_Access )
			{
				//Get Status
				if ( core_GetMotorStatus(true) == -1 ) return false;
				//alarm
				if ( Alarm_i == 1 ) core_ResetAlarm();
				//turn on motor
				if ( Svon_i == 0 && core_ServoOn(true) == false ) return false;

				//go to home
				m_iErrorID = APS168.APS_home_move(m_iAxis_ID);
				if ( m_iErrorID != 0 ) return false;

				//get status and set servo on is it is off
				core_GetMotorStatus();

				//Set Current position Tracker
				m_dbCurrentPosition = 0;
			}

			return true;
		}

		//Core: Stop Moving/////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool core_Stop()
		{
			//check PICe Card has been init and connected
			if ( IsConnected() == false ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

			lock ( m_lock_Access )
			{
				m_iErrorID = APS168.APS_emg_stop(m_iAxis_ID);
				if ( m_iErrorID != 0 ) return false;
			}

			return true;
		}

		//Core: Reset Alarm/////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool core_ResetAlarm()
		{
			//check PICe Card has been init and connected
			if ( IsConnected() == false ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

			lock ( m_lock_Access )
			{
				//require to servo on again
				m_iErrorID = APS168.APS_set_servo_on(m_iAxis_ID, 1);
				if ( m_iErrorID != 0 ) return false;
			}

			return true;
		}

		//Core: Move Absolute///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool core_MoveAbs(double dbPosition_mm, int iRunSpeed)
        {
            double dbPosition_pulse = 0;
            ASYNCALL asyn = new ASYNCALL();
            
            //Set here for not a waiting call
            asyn.i32_ret = 0;

            //check PCIe Card has been init and connected
            if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

            //get status and set servo on is it is off
            if (core_GetMotorStatus() != 1) return false;

            lock (m_lock_Access)
            {
                //turn on motor
                if (Svon_i == 0 && core_ServoOn(true) == false) return false;

                //Set Run Speed
                if (m_iCurrentRunSpeed != iRunSpeed)
                {
                    m_iCurrentRunSpeed = iRunSpeed;
                    double dbVal_RunSpeed = (double)((m_Settings.m_iVelocity / 100) * m_Settings.m_iMainSpeed_Multiplier);
                    dbVal_RunSpeed = (dbVal_RunSpeed * iRunSpeed / 100);

                    m_iErrorID = APS168.APS_set_axis_param_f(m_iAxis_ID, (Int32)APS_Define.PRA_VM, dbVal_RunSpeed);
                }

                //calculate the pulse
                dbPosition_pulse = Convert_mmToPulse(dbPosition_mm);

                //move absolute position
                m_iErrorID = APS168.APS_ptp(m_iAxis_ID, 0, dbPosition_pulse, ref asyn);
                if (m_iErrorID != 0) return false;
            }

            return true;
        }

		//Move Relative / Jog/////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool core_MoveRel(double dbMove_mm, int iRunSpeed)
        {
            ASYNCALL asyn = new ASYNCALL();
            double dbPosition_pulse = 0;

            //Set here for not a waiting call
            asyn.i32_ret = 0;

            //check PICe Card has been init and connected
            if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

            //get status and set servo on is it is off
            if (core_GetMotorStatus() != 1) return false;

            lock (m_lock_Access)
            {
                //turn on motor
                if (Svon_i == 0 && core_ServoOn(true) == false) return false;

                //Set Run Speed
                if (m_iCurrentRunSpeed != iRunSpeed)
                {
                    m_iCurrentRunSpeed = iRunSpeed;
                    double dbVal_RunSpeed = (double)((m_Settings.m_iVelocity / 100) * m_Settings.m_iMainSpeed_Multiplier);
                    dbVal_RunSpeed = (dbVal_RunSpeed * iRunSpeed / 100);

                    m_iErrorID = APS168.APS_set_axis_param_f(m_iAxis_ID, (Int32)APS_Define.PRA_VM, dbVal_RunSpeed);
                }

                //calculate the pulse
                dbPosition_pulse = Convert_mmToPulse(dbMove_mm);

                //move absolute position
                m_iErrorID = APS168.APS_ptp(m_iAxis_ID, 1, dbPosition_pulse, ref asyn);
                if (m_iErrorID != 0) return false;
            }
            return true;
        }

		//Core: Get Motor Status////////////////////////////////////////////////////////////////////////////////////////////////
        public short core_GetMotorStatus(bool bIgnoreError = false)
        {
            //check PICe Card has been init and connected
            if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return -1; }

            lock (m_lock_Access)
            {
                int iRes_MotionStatus = 0;
                int iRes_IOStatus = 0;

                int iCSTP = 0;
                int iVM = 0;
                int iAcc = 0;
                int iDec = 0;
                //int iDir = 0;
                int iMDN = 0;
                int iHMV = 0;
                //int iAstp = 0;
                int iInPos = 0;

                int iAlarm = 0;
                int iPel = 0;
                int iMel = 0;
                int iOrg = 0;
                int iEmg = 0;
                int iSVON = 0;

                //motion status
                iRes_MotionStatus = APS168.APS_motion_status(m_iAxis_ID);
                if (iRes_MotionStatus < 0) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_GETSTATUS_ERR; return -1; }

                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_CSTP_V) != 0) iCSTP = 1;
                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_VM_V) != 0) iVM = 1;
                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_ACC_V) != 0) iAcc = 1;
                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_DEC_V) != 0) iDec = 1;
                //if ( ( iRes_MotionStatus & ( Int32 ) APS_Define.MTS_DIR_V )		!= 0 ) iDir = 1;
                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_NSTP_V) != 0) iMDN = 1;
                if ((iRes_MotionStatus & (Int32)APS_Define.MTS_HMV_V) != 0) iHMV = 1;
                //if ( ( iRes_MotionStatus & ( Int32 ) APS_Define.MTS_ASTP_V ) != 0 ) iAstp = 1;

                //motion io status
                iRes_IOStatus = APS168.APS_motion_io_status(m_iAxis_ID);
                if (iRes_MotionStatus < 0) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_GETSTATUS_ERR; return -1; }

                if ((iRes_IOStatus & (Int32)APS_Define.MIO_ALM_V) != 0) iAlarm = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_PEL_V) != 0) iPel = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_MEL_V) != 0) iMel = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_ORG_V) != 0) iOrg = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_EMG_V) != 0) iEmg = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_SVON_V) != 0) iSVON = 1;
                if ((iRes_IOStatus & (Int32)APS_Define.MIO_INP_V) != 0) iInPos = 1;

                //Set to status
                m_iSvon = iSVON;
                m_iHoming = iHMV;
                m_iOrg = iOrg;
                m_iAlarm = iAlarm;
                m_iEL_posivite = iPel;
                m_iEL_negative = iMel;
                m_iEmg = iEmg;

                if (iMDN == 1 && iCSTP == 1 && iInPos == 1) m_iMoveDone = 1; else m_iMoveDone = 0;
                if (iMDN == 0 && iCSTP == 0) m_iMoving = 1; else m_iMoving = 0;
                if (iVM == 1 || iAcc == 1 || iDec == 1) m_iBusy = 1; else m_iBusy = 0;
                //if ( iAstp == 1 ) m_iAlarm = 1;

                //Decision:
                if (m_iAlarm == 1) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_ALARM; return 0; }
                if (m_iEmg == 1) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_EMG; return 0; }
                if (m_iSvon != 1) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_ON_ERR; return 0; }

                if (m_iEL_posivite == 1 && bIgnoreError == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_ENDLIMIT_P; return 0; }
                if (m_iEL_negative == 1 && bIgnoreError == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_ENDLIMIT_N; return 0; }
            }
            return 1;
        }

		//Core: Get motor position/////////////////////////////////////////////////////////////////////////////////////////////
        //public bool Get_MotorPosition(ref double dbCurrentPosition)
        //{
        //    int iPosition = 0;

        //    //check PCIe Card has been init and connected
        //    if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

        //    lock (m_lock_Access)
        //    {
        //        //get position
        //        m_iErrorID = APS168.APS_get_position(m_iAxis_ID, ref iPosition);

        //        //conversion
        //        dbCurrentPosition = (double)Convert_PulseTomm(iPosition);
        //        m_dbCurrentPosition = dbCurrentPosition;

        //        //check error
        //        if (m_iErrorID != 0) return false;
        //    }

        //    return true;
        //}

        public bool Get_MotorPosition(ref double dbCurrentPosition)
        {
            double dbPosition = 0;

            //check PCIe Card has been init and connected
            if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

            lock (m_lock_Access)
            {
                //get position
                m_iErrorID = APS168.APS_get_position_f(m_iAxis_ID, ref dbPosition);

                //conversion
                dbCurrentPosition = Convert_PulseTomm(dbPosition);
                m_dbCurrentPosition = dbCurrentPosition;

                //check error
                if (m_iErrorID != 0) return false;
            }

            return true;
        }

		//Core: Set Position///////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Set_MotorPosition(double dbPosition_mm)
        {
            double dbPosition_pulse = 0;

            //check PCIe Card has been init and connected
            if (IsConnected() == false) { m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_NOTCONNECTED; return false; }

            //get status and set servo on is it is off
            if (core_GetMotorStatus() != 1) return false;

            lock (m_lock_Access)
            {
                //turn on motor
                if (Svon_i == 0 && core_ServoOn(true) == false) return false;

                //conversion
                dbPosition_pulse = Convert_mmToPulse(dbPosition_mm);

                //Set motor position
                m_iErrorID = APS168.APS_set_position_f(m_iAxis_ID, dbPosition_mm);
                if (m_iErrorID != 0) return false;
            }

            return true;
        }

        



		/*****************************************************************************************************************
		 * Supporting Movement
		 *****************************************************************************************************************/
		//Check Position//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Check the current position of the motor: Return 1 => same position, -1 => error, 0 => not same position
		/// </summary>
		/// <param name="dbPosition_mm">Position in mm to be compared against</param>
		/// <returns>Return 1 => same position, -1 => error, 0 => not same position</returns>
        public short Check_Position(double dbPosition_mm)
        {
            double dbCurrentPosition_mm = 0;
            short iRet = 1;

            //get current position
            if (Get_MotorPosition(ref dbCurrentPosition_mm) == false) return -1;

            //Compare
            if (dbCurrentPosition_mm > dbPosition_mm + 0.01 || dbCurrentPosition_mm < dbPosition_mm - 0.01)
                iRet = 0;

            //set to global variable
            m_dbCurrentPosition = dbCurrentPosition_mm;

            return iRet;
        }

		//Check Reached Position//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Check_ReachedPosition(double dbPosition_mm)
        {
            bool bRet = true;
            double dbCurrentPosition_mm = 0;
            double dbTolerance_mm = 0.3;
            string str = "";

            //get current position
            if (Get_MotorPosition(ref dbCurrentPosition_mm) == false) return false;

            //Compare
            if (dbCurrentPosition_mm > dbPosition_mm + dbTolerance_mm || dbCurrentPosition_mm < dbPosition_mm - dbTolerance_mm)
            {
                str = m_strLabel + "\r\n";
                str += "Teached Position (mm): " + dbPosition_mm.ToString() + "\r\n";
                str +="Current Position (mm): " + dbCurrentPosition_mm.ToString() + "\r\n";
                str +="Tolerance +-     (mm): " + dbTolerance_mm.ToString() + "\r\n";
                str +="Difference       (mm): " + (dbCurrentPosition_mm - dbPosition_mm).ToString();
                MessageBox.Show(str);

                m_iErrorID = (int)ErrorTable.en_ErrorCode.INDEXER_SLIP;
                bRet = false;
            }

            //set to global variable
            m_dbCurrentPosition = dbCurrentPosition_mm;

            return bRet;
        }

		//Scan for the motor to begin its movement////////////////////////////////////////////////////////////////////////////////////////////
        public bool Detect_Moving(int iHoming = 0)
        {
            Stopwatch mSt = new Stopwatch();
            bool bRet = true;
            bool bIgnoreError = false;
            int iTimeOut = 0;
            int iErrorCode = 0;

            //Get variables
            iTimeOut = (iHoming == 0) ? m_Settings.m_iTimeOut : m_Settings.m_iTimeOutHome;
            iErrorCode = (iHoming == 0) ? (int)ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START : (int)ErrorTable.en_ErrorCode.INDEXER_HOMING_TIMEOUT;
            bIgnoreError = (iHoming == 0) ? false : true;

            //Process
            mSt.Start();
            while (true)
            {
                //get motor status
                if (core_GetMotorStatus(bIgnoreError) != 1) { bRet = false; break; }

                //time out
                if (iTimeOut < mSt.ElapsedMilliseconds) { m_iErrorID = iErrorCode; bRet = false; break; }

                //Check Status
                if (Moving_i == 1) break;
            }
            mSt.Stop();

            return bRet;
        }

		//Scan for the motor to stop. Move Done///////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Detect_Stop( int iHoming = 0 )
		{
			Stopwatch mSt = new Stopwatch();
			bool bRet = true;
			bool bIgnoreError = false;
			int iTimeOut = 0;
			int iErrorCode = 0;

			//Get variables
			iTimeOut = ( iHoming == 0 ) ? m_Settings.m_iTimeOut : m_Settings.m_iTimeOutHome;
			iErrorCode = ( iHoming == 0 ) ? ( int ) ErrorTable.en_ErrorCode.INDEXER_TIMEOUT : ( int ) ErrorTable.en_ErrorCode.INDEXER_HOMING_TIMEOUT;
			bIgnoreError = ( iHoming == 0 ) ? false : true;

			//Process
			mSt.Start();
			while ( true )
			{
				//get motor status
				if ( core_GetMotorStatus(bIgnoreError) != 1 ) { bRet = false; break; }
				//time out
				if ( iTimeOut < mSt.ElapsedMilliseconds ) { m_iErrorID = iErrorCode; bRet = false; break; }

				//Check Status
				if ( MoveDone_i == 1 ) break;
			}
			mSt.Stop();

			return bRet;
		}

		//Check Connected////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool IsConnected()
		{
			return m_PCICard.Flag_Connected_b;
		}

		//Conversion: mm to Pulse////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public double Convert_mmToPulse(double dbPosition_mm)
        {
            double dbPulse = 0;
            dbPulse = dbPosition_mm / Get_MinStep();

            return dbPulse;
        }

		//Conversion: pulse to mm///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public double Convert_PulseTomm(double dbPosition_pulse)
        {
            double dbmm = 0;
            dbmm = dbPosition_pulse * Get_MinStep();

            return dbmm;
        }

		//Conversion: Get Min Step///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public double Get_MinStep()
        {
            return (m_Settings.m_dbmmPerRevolution / m_Settings.m_dbPulsePerRevolution);
        }







		/*****************************************************************************************************************
		 * Indexer Movement
		 *****************************************************************************************************************/
		//Indexer: Turn on Motor / Servo On///////////////////////////////////////////////////////////////////////////////////
        public bool Servo_On(bool bON)
        {
            //turn on axis
            if (core_ServoOn(bON) == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            //delay
            Thread.Sleep(1000);

            //check state: Get Status first
            if (core_GetMotorStatus() == -1) { Set_Error(m_strLabel + " Indexer"); return false; }

            if (bON == true)
            {
                //Check state: Get servo on
                if (Svon_i != 1) { Set_Error(m_strLabel + " Indexer", (int)ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
            }

            return true;
        }

		//Indexer: Homing//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: No need to set position to 0. Motor driver will reset it for homing command
        public bool Homing()
        {
            int iAxisInitErr = 0;

        ReInit:

            //send motor to move home
            if (core_Home() == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            //Detect moving
            //if ( Home_i != 1) if ( Detect_Moving(1) == false ) return false;
            //if (Detect_Moving(1) == false) { Set_Error(m_strLabel + " Indexer Detect Moving"); return false; }
            Thread.Sleep(1000);

            //Detect Stop
            //if ( Home_i != 1 ) if ( Detect_Stop(1) == false ) return false;
            if (Detect_Stop(1) == false) { Set_Error(m_strLabel + " Detect Stop"); return false; }

            //Check current position
            if (Check_ReachedPosition(0) == false) { if (iAxisInitErr == 0) { iAxisInitErr++; goto ReInit; } Set_Error(m_strLabel + " Indexer"); return false; }

            return true;
        }

		//Indexer: Stop Moving//////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Stop_Move()
		{
            if (core_Stop() == false) { Set_Error(m_strLabel + " Indexer"); return false; }
			return true;
		}

		//Indexer: Reset Alarm/////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Reset_Alarm()
		{
			//require to servo on again
			//if ( core_ServoOn(true) == false ) return false;

            //Reset alarm
            if (core_ResetAlarm() == false) { Set_Error(m_strLabel + " Indexer"); return false; }

			return true;
		}

		//Indexer: Move Absolute///////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Move_Abs( Indexer_Pos_Settings mPosition )
		{
            ////check position
            //short iRet = Check_Position(mPosition.m_dbPos);
            //if ( iRet == -1 ) return false;
            //else if ( iRet == 0 ) return true;

            ////trigger command
            //if ( core_MoveAbs(mPosition.m_dbPos, mPosition.m_iRunSpeed) == false ) return false;
            ////Detect Moving
            //if ( Detect_Moving() == false ) return false;
            ////Detect Stop
            //if ( Detect_Stop() == false ) return false;

            ////Check reach position
            //if ( Check_ReachedPosition(mPosition.m_dbPos) == false ) return false;

            int iAxisRet = 0;

            //Check XY Position
            iAxisRet = Check_Position(mPosition.m_dbPos); if (iAxisRet < 0) { Set_Error(m_strLabel + " Indexer"); return false; }

            //XY: Move XY
            if (iAxisRet == 0) if (core_MoveAbs(mPosition.m_dbPos, mPosition.m_iRunSpeed) == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            //wait XY to move
            //if (iAxisRet == 0) if (Detect_Moving() == false) { Set_Error(m_strLabel + " Indexer"); return false; }
            //if (!Detect_Moving(true, iAxisXRet, iAxisYRet, iAxisZRet)) return false;
            Thread.Sleep(700);	//unable to capture the moving signal if the distance is too small. Protection is by checking the position after movement

            //wait to stop
            if (iAxisRet == 0) if (Detect_Stop() == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            //Check position
            if (Check_ReachedPosition(mPosition.m_dbPos) == false) { Set_Error(m_strLabel + " Indexer"); return false; }

			return true;
		}

		//Indexer: Move Relative / Jog///////////////////////////////////////////////////////////////////////////////////////////
        public bool Move_Rel(Indexer_Pos_Settings mPosition)
        {
            ////trigger command
            //if (core_MoveRel(mPosition.m_dbPos, mPosition.m_iRunSpeed) == false) return false;
            ////Detect Moving
            //if (Detect_Moving() == false) return false;
            ////Detect Stop
            //if (Detect_Stop() == false) return false;

            //Move relative
            if (mPosition.m_dbPos != 0) if (core_MoveRel(mPosition.m_dbPos, mPosition.m_iRunSpeed) == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            //wait XY to move
            //if ( mPosition.m_dbPos != 0 ) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            //if ( !Detect_Moving(true, 0, 0, 0) ) return false;
            Thread.Sleep(700);

            //wait XY to stop
            if (mPosition.m_dbPos != 0) if (Detect_Stop() == false) { Set_Error(m_strLabel + " Indexer"); return false; }

            return true;
        }

        //Get Robot position///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Indexer_Pos_Settings GetRobotPosition_mm(ref bool bRet)
        {
            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();
            bRet = true;
            double dbPos = 0;

            //Get Position via Status command
            if (Get_MotorPosition(ref dbPos) == false) { Set_Error(m_strLabel + " Indexer"); bRet = false; }

            //Set position to struct
            if (bRet == true)
            {
                mPosition.m_dbPos = dbPos;
            }

            return mPosition;
        }

        //Get Robot Status/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetRobotStatus()
        {
            short iRet = 0;

            //Get Indexer Status
            iRet = core_GetMotorStatus(); if (iRet < 0) { Set_Error(m_strLabel + " Indexer"); return iRet; }

            return iRet;
        }

	}
}
