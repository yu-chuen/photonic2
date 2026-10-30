using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace StaticCoreLib
{
	public class RobotXYZ_APSlib_PanasonicECAT
	{
		//variables
		RobotXYZ_APSlib_PanasonicECAT_Settings m_Settings = new RobotXYZ_APSlib_PanasonicECAT_Settings();
		public Indexer_APSlib_PanasonicECAT m_indexerAxisX = null;
		public Indexer_APSlib_PanasonicECAT m_indexerAxisY1 = null;
        public Indexer_APSlib_PanasonicECAT m_indexerAxisY2 = null;
        public Indexer_APSlib_PanasonicECAT m_indexerAxisZ = null;

		APS168_Ethercat_PCIcard m_PCICard = null;

		//vars
		public int m_iErrorID = 0;
		public String m_strErrorMessage = "";
		public String m_strLabel = "";
		public String m_strFilePath = "";

		public bool m_bHomingZFirst = true;		//Home Type: Home Z First

		//constructor/////////////////////////////////////////////////////////////////////////////////////////////
		public RobotXYZ_APSlib_PanasonicECAT( APS168_Ethercat_PCIcard mCard, String strLabel, String strFilePath, int iAxisID_X, int iAxisID_Y, int iAxisID_Z, bool bHomingZFirst = true )
		{
			m_PCICard = mCard;
			m_strLabel = strLabel;
			m_strFilePath = strFilePath; if ( m_strFilePath.EndsWith("\\") == false ) m_strFilePath += "\\";

			m_indexerAxisX = new Indexer_APSlib_PanasonicECAT(iAxisID_X, m_strLabel + "_AxisX", m_strFilePath, m_PCICard);
			m_indexerAxisY1 = new Indexer_APSlib_PanasonicECAT(iAxisID_Y, m_strLabel + "_AxisY1", m_strFilePath, m_PCICard);
            m_indexerAxisY2 = new Indexer_APSlib_PanasonicECAT(iAxisID_Y, m_strLabel + "_AxisY2", m_strFilePath, m_PCICard);
            m_indexerAxisZ = new Indexer_APSlib_PanasonicECAT(iAxisID_Z, m_strLabel + "_AxisZ", m_strFilePath, m_PCICard);

			m_bHomingZFirst = bHomingZFirst;

			//get Settings
			From_Registry();
		}


		//Update the Stepper motor with the new settings AND save it to xml file//////////////////////////
		public bool To_Registry()
		{
			bool bRet = true;

			//update to xml file
			String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
			XMLSerializer.Serialize_Func<RobotXYZ_APSlib_PanasonicECAT_Settings>(m_Settings, strFilePathFull);

			m_indexerAxisX.Set_Settings(m_Settings.m_Settings_AxisX);
			m_indexerAxisY1.Set_Settings(m_Settings.m_Settings_AxisY1);
            m_indexerAxisY2.Set_Settings(m_Settings.m_Settings_AxisY2);
            m_indexerAxisZ.Set_Settings(m_Settings.m_Settings_AxisZ);

			//update motor settings
			if ( !Update_MotorDriverSettings(1) ) { if ( bRet ) bRet = false; }
			if ( !Update_MotorDriverSettings(2) ) { if ( bRet ) bRet = false; }
			if ( !Update_MotorDriverSettings(3) ) { if ( bRet ) bRet = false; }
            if ( !Update_MotorDriverSettings(4) ) { if ( bRet ) bRet = false; }

            return bRet;
		}

		//Get indexer settings parameters. Read from xml file////////////////////////////////////////////////////
		public bool From_Registry()
		{
			bool bRet = true;

			//read from xml file
			String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
			m_Settings = XMLSerializer.Deserialize_Func<RobotXYZ_APSlib_PanasonicECAT_Settings>(strFilePathFull);

			//set to settings
			m_indexerAxisX.Set_Settings(m_Settings.m_Settings_AxisX);
			m_indexerAxisY1.Set_Settings(m_Settings.m_Settings_AxisY1);
            m_indexerAxisY2.Set_Settings(m_Settings.m_Settings_AxisY2);
            m_indexerAxisZ.Set_Settings(m_Settings.m_Settings_AxisZ);

			//update motor settings
			if ( !Update_MotorDriverSettings(1) ) { Set_Error(1, m_strLabel + " AxisX"); if ( bRet ) bRet = false; }
			if ( !Update_MotorDriverSettings(2) ) { Set_Error(2, m_strLabel + " AxisY"); if ( bRet ) bRet = false; }
			if ( !Update_MotorDriverSettings(3) ) { Set_Error(3, m_strLabel + " AxisZ"); if ( bRet ) bRet = false; }
            if ( !Update_MotorDriverSettings(4) ) { Set_Error(3, m_strLabel + " AxisZ"); if ( bRet ) bRet = false; }

            return bRet;
		}

		//Get Error Code/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID;
		}
		//Get Additional Error Message//////////////////////////////////////////////////////////////////////////////////////
		public String Get_ErrorMessage()
		{
			String strError = m_strErrorMessage;
			m_strErrorMessage = "";
			return strError;
		}



		/*****************************************************************************************************************
		 * Support functions
		 *****************************************************************************************************************/
		//Set Error/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: iAxisType => 1=Axis X, 2=AxisY, 3=AxisZ
		public void Set_Error( int iAxisType, String strAddErrorMsg )
		{
			//Set error id
			switch ( iAxisType )
			{
				case 1: m_iErrorID = m_indexerAxisX.Get_ErrorID(); m_strErrorMessage = m_indexerAxisX.Get_ErrorMessage(); break;
				case 2: m_iErrorID = m_indexerAxisY1.Get_ErrorID(); m_strErrorMessage = m_indexerAxisY1.Get_ErrorMessage(); break;
                case 3: m_iErrorID = m_indexerAxisY2.Get_ErrorID(); m_strErrorMessage = m_indexerAxisY2.Get_ErrorMessage(); break;
                case 4: m_iErrorID = m_indexerAxisZ.Get_ErrorID(); m_strErrorMessage = m_indexerAxisZ.Get_ErrorMessage(); break;
			}

			//Set Additional error message
			m_strErrorMessage += strAddErrorMsg;
		}

		public void Set_Error( String strAddErrorMsg, int iError )
		{
			m_iErrorID = iError;
			m_strErrorMessage = strAddErrorMsg;
		}

		//Update Motor Settings///////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Update_MotorDriverSettings(int iAxisType)
		{
			bool bRet = true;
			String strAxis = "";

			//Update Motor Driver
			switch ( iAxisType )
			{
				case 1: bRet = m_indexerAxisX.Update_MotorDriverSettings(); strAxis = m_strLabel + "_Axis X"; break;
				case 2: bRet = m_indexerAxisY1.Update_MotorDriverSettings();  strAxis = m_strLabel + "_Axis Y1"; break;
                case 3: bRet = m_indexerAxisY2.Update_MotorDriverSettings(); strAxis = m_strLabel + "_Axis Y2"; break;
                case 4: bRet = m_indexerAxisZ.Update_MotorDriverSettings();  strAxis = m_strLabel + "_Axis Z"; break;
			}

			//Check error
			if ( bRet == false )
			{
				Set_Error(iAxisType, strAxis + "Update Motor Driver Settings");
			}

			return bRet;
		}

		//Detect Moving///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Detect_Moving( bool bWithZ, int iAxisXRun, int iAxisYRun, int iAxisZRun )
		{
			Stopwatch mSt = new Stopwatch();
			bool bXRun = false, bYRun = false, bZRun = false;

			//Change 
			if ( iAxisXRun == 1 ) bXRun = true;
			if ( iAxisYRun == 1 ) bYRun = true;
			if ( iAxisZRun == 1 || bWithZ == false ) bZRun = true;

			//process
			mSt.Start();
			while ( true )
			{
				//get motor status
				if (iAxisXRun == 0) if ( m_indexerAxisX.core_GetMotorStatus() == -1 ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
				if (iAxisYRun == 0)
				{
					if (m_indexerAxisY1.core_GetMotorStatus() == -1) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                    if (m_indexerAxisY2.core_GetMotorStatus() == -1) { Set_Error(2, m_strLabel + " AxisY2"); return false; }
                }
				if ( bWithZ )
					if ( iAxisZRun == 0 ) 
						if ( m_indexerAxisZ.core_GetMotorStatus() == -1 ) { Set_Error(3, m_strLabel + " AxisZ"); return false; }

				//time out
				if ( iAxisXRun == 0 ) if ( m_indexerAxisX.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds &&
					m_indexerAxisX.Moving_i != 1 ) 
					{ Set_Error(m_strLabel + " AxisX", ( int ) ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }

				if (iAxisYRun == 0)
				{
					if (m_indexerAxisY1.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds && m_indexerAxisY1.Moving_i != 1)
					{ Set_Error(m_strLabel + " AxisY1", (int)ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }

                    if (m_indexerAxisY2.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds && m_indexerAxisY2.Moving_i != 1)
                    { Set_Error(m_strLabel + " AxisY2", (int)ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }
                }

				if ( bWithZ )
					if ( iAxisZRun == 0 ) 
						if ( m_indexerAxisZ.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds &&
							m_indexerAxisZ.Moving_i != 1 ) 
							{ Set_Error(m_strLabel + " AxisZ", ( int ) ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }

				//check status
				if ( m_indexerAxisX.Moving_i == 1 ) bXRun = true;
				if ( m_indexerAxisY1.Moving_i == 1 && m_indexerAxisY2.Moving_i == 1)   bYRun = true;
				if ( bWithZ )
					if ( m_indexerAxisZ.Moving_i == 1 ) bZRun = true;

				if ( bXRun == true && bYRun == true && bZRun == true ) break;
			}
			mSt.Stop();

			return true;
		}








		/*****************************************************************************************************************
		 * Movement
		 *****************************************************************************************************************/
		//Turn on Motor / Servo On///////////////////////////////////////////////////////////////////////////////////
		public bool Servo_On( bool bON )
		{
			//turn on all axis servo
			if ( m_indexerAxisX.core_ServoOn(bON) == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.core_ServoOn(bON) == false ) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY2.core_ServoOn(bON) == false ) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
            if ( m_indexerAxisZ.core_ServoOn(bON) == false ) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

			//delay
			Thread.Sleep(1000);

			//check state: Get Status first
			if ( m_indexerAxisX.core_GetMotorStatus() == -1 ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.core_GetMotorStatus() == -1 ) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY2.core_GetMotorStatus() == -1 ) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
            if ( m_indexerAxisZ.core_GetMotorStatus() == -1 ) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

			if ( bON == true )
			{
				//Check state: Get servo on
				if ( m_indexerAxisX.Svon_i != 1 ) { Set_Error(m_strLabel + " AxisX", ( int ) ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
				if ( m_indexerAxisY1.Svon_i != 1 ) { Set_Error(m_strLabel + " AxisY1", ( int ) ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
                if ( m_indexerAxisY2.Svon_i != 1 ) { Set_Error(m_strLabel + " AxisY2", (int)ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
                if ( m_indexerAxisZ.Svon_i != 1 ) { Set_Error(m_strLabel + " AxisZ", ( int ) ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
			}

			return true;
		}

		//Homing//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Homing()
		{
            int iAxisZInitErr = 0;

			//HOMING all together
			if ( m_bHomingZFirst == false )
			{
				//Homing Cmd
				if ( m_indexerAxisX.core_Home() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
				if ( m_indexerAxisY1.core_Home() == false ) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if ( m_indexerAxisY2.core_Home() == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if ( m_indexerAxisZ.core_Home() == false ) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

				//Detect Moving
                //if ( m_indexerAxisX.Detect_Moving(1) == false ) { Set_Error(1, m_strLabel + " AxisX Detect Moving"); return false; }
                //if ( m_indexerAxisY.Detect_Moving(1) == false ) { Set_Error(2, m_strLabel + " AxisY Detect Moving"); return false; }
                //if ( m_indexerAxisZ.Detect_Moving(1) == false ) { Set_Error(3, m_strLabel + " AxisZ Detect Moving"); return false; }
                Thread.Sleep(1000);
				
				//XY: Detect Stop
				if ( m_indexerAxisX.Detect_Stop(1) == false ) { Set_Error(1, m_strLabel + " AxisX Detect Stop"); return false; }
				if ( m_indexerAxisY1.Detect_Stop(1) == false ) { Set_Error(2, m_strLabel + " AxisY1 Detect Stop"); return false; }
                if ( m_indexerAxisY2.Detect_Stop(1) == false ) { Set_Error(3, m_strLabel + " AxisY2 Detect Stop"); return false; }
                if ( m_indexerAxisZ.Detect_Stop(1) == false ) { Set_Error(4, m_strLabel + " AxisZ Detect Stop"); return false; }

				//Check current position
				if ( m_indexerAxisX.Check_ReachedPosition(0) == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
				if ( m_indexerAxisY1.Check_ReachedPosition(0) == false ) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if ( m_indexerAxisY2.Check_ReachedPosition(0) == false ) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if ( m_indexerAxisZ.Check_ReachedPosition(0) == false ) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

				return true;
			}

			//------------------------------------------------------------------------------------------------------------------------------------------------
			//HOming with Z Home FIRST
			//------------------------------------------------------------------------------------------------------------------------------------------------
			//Z: Homing
ReInit:
			if ( m_indexerAxisZ.core_Home() == false )					{ Set_Error(4, m_strLabel + " AxisZ"); return false; }
			//Z: detect moving
			if ( m_indexerAxisZ.Detect_Moving(1) == false ) { Set_Error(4, m_strLabel + " AxisZ Detect Moving"); return false; }
			//Z: Detect Stop
			if ( m_indexerAxisZ.Detect_Stop(1) == false ) { Set_Error(4, m_strLabel + " AxisZ Detect Stop"); return false; }
			
			//XY: homing
			if ( m_indexerAxisX.core_Home() == false )				{ Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.core_Home() == false )				{ Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY2.core_Home() == false )             { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //XY: Detect Moving
            //if (m_indexerAxisX.Detect_Moving(1) == false) { Set_Error(1, m_strLabel + " AxisX Detect Moving"); return false; }
            //if (m_indexerAxisY.Detect_Moving(1) == false) { Set_Error(2, m_strLabel + " AxisY Detect Moving"); return false; }
            Thread.Sleep(1000);

			//XY: Detect Stop
			if ( m_indexerAxisX.Detect_Stop(1) == false ) { Set_Error(1, m_strLabel + " AxisX Detect Stop"); return false; }
			if ( m_indexerAxisY1.Detect_Stop(1) == false ) { Set_Error(2, m_strLabel + " AxisY1 Detect Stop"); return false; }
            if ( m_indexerAxisY2.Detect_Stop(1) == false ) { Set_Error(3, m_strLabel + " AxisY2 Detect Stop"); return false; }

            //Check current position
            if ( m_indexerAxisX.Check_ReachedPosition(0) == false ) { if (iAxisZInitErr == 0) { iAxisZInitErr++; goto ReInit;} Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.Check_ReachedPosition(0) == false ) { if (iAxisZInitErr == 0) { iAxisZInitErr++; goto ReInit;} Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY1.Check_ReachedPosition(0) == false ) { if (iAxisZInitErr == 0) { iAxisZInitErr++; goto ReInit; } Set_Error(3, m_strLabel + " AxisY2"); return false; }
            if ( m_indexerAxisZ.Check_ReachedPosition(0) == false ) { if (iAxisZInitErr == 0) { iAxisZInitErr++; goto ReInit;} Set_Error(4, m_strLabel + " AxisZ"); return false; }
			
			return true;
		}

		//Stop Moving//////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Stop_Move()
		{
			//Stop all axis
			if ( m_indexerAxisX.core_Stop() == false )			{ Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.core_Stop() == false )			{ Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY2.core_Stop() == false)          { Set_Error(3, m_strLabel + " AxisY2"); return false; }
            if ( m_indexerAxisZ.core_Stop() == false )			{ Set_Error(4, m_strLabel + " AxisZ"); return false; }

			return true;
		}

		//Reset Alarm/////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Reset_Alarm()
		{
			//Reset all alarm
			if ( m_indexerAxisX.core_ResetAlarm() == false )			{ Set_Error(1, m_strLabel + " AxisX"); return false; }
			if ( m_indexerAxisY1.core_ResetAlarm() == false )			{ Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if ( m_indexerAxisY2.core_ResetAlarm() == false )           { Set_Error(3, m_strLabel + " AxisY2"); return false; }
            if ( m_indexerAxisZ.core_ResetAlarm() == false )			{ Set_Error(4, m_strLabel + " AxisZ"); return false; }
			
			return true;
		}

		//Move Absolute///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Move_Abs(Robot_Pos_Settings mPosition)
        {
            int iAxisXRet = 0;
            int iAxisY1Ret = 0;
            int iAxisY2Ret = 0;
            int iAxisZRet = 0;

            //Safety Window--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (SafetyWindow(mPosition) == false) return false;

            //Without interlock------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (mPosition.m_iInterlock == 0)
            {
                //Check XY Position
                iAxisXRet = m_indexerAxisX.Check_Position(mPosition.m_dbXPos); if (iAxisXRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                iAxisY1Ret = m_indexerAxisY1.Check_Position(mPosition.m_dbY1Pos); if (iAxisY1Ret < 0) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                iAxisY2Ret = m_indexerAxisY2.Check_Position(mPosition.m_dbY2Pos); if (iAxisY2Ret < 0) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                iAxisZRet = m_indexerAxisZ.Check_Position(mPosition.m_dbZPos); if (iAxisZRet < 0) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                //XY: Move XY
                if (iAxisXRet == 0) if (m_indexerAxisX.core_MoveAbs(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (iAxisY1Ret == 0) if (m_indexerAxisY1.core_MoveAbs(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if (iAxisY2Ret == 0) if (m_indexerAxisY2.core_MoveAbs(mPosition.m_dbY2Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if (iAxisZRet == 0) if (m_indexerAxisZ.core_MoveAbs(mPosition.m_dbZPos, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                //wait XY to move
                //if ( iAxisXRet == 0 ) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                //if ( iAxisYRet == 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
                //if ( iAxisZRet == 0 ) if ( m_indexerAxisZ.Detect_Moving() == false ) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                //if ( !Detect_Moving(true, iAxisXRet, iAxisYRet, iAxisZRet) ) return false;
                Thread.Sleep(700);	//unable to capture the moving signal if the distance is too small. Protection is by checking the position after movement

                //wait XY to stop
                if (iAxisXRet == 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (iAxisY1Ret == 0) if (m_indexerAxisY1.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if (iAxisY2Ret == 0) if (m_indexerAxisY2.Detect_Stop() == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if (iAxisZRet == 0) if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                //Check position
                if (m_indexerAxisX.Check_ReachedPosition(mPosition.m_dbXPos) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (m_indexerAxisY1.Check_ReachedPosition(mPosition.m_dbY1Pos) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if (m_indexerAxisY2.Check_ReachedPosition(mPosition.m_dbY2Pos) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if (m_indexerAxisZ.Check_ReachedPosition(mPosition.m_dbZPos) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                return true;
            }

            //With Interlock-------------------------------------------------------------------------------------------------------------------------------------------------------
            //Move Z Up first
            if (m_indexerAxisZ.Check_Position(mPosition.m_dbInterlock_Zht) == 0)
            {
                //Z: Move Z to m_dbInterlock_Zht pos
                if (m_indexerAxisZ.core_MoveAbs(mPosition.m_dbInterlock_Zht, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
                //if ( m_indexerAxisZ.Detect_Moving() == false ) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                Thread.Sleep(500);
                if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
                //if ( m_indexerAxisZ.Check_ReachedPosition(0) == false ) { Set_Error(3, m_strLabel + " AxisZ"); return false; } 
                if (m_indexerAxisZ.Check_ReachedPosition(mPosition.m_dbInterlock_Zht) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
            }

            //Check XY Position
            iAxisXRet = m_indexerAxisX.Check_Position(mPosition.m_dbXPos); if (iAxisXRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            iAxisY1Ret = m_indexerAxisY1.Check_Position(mPosition.m_dbY1Pos); if (iAxisY1Ret < 0) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            iAxisY2Ret = m_indexerAxisY2.Check_Position(mPosition.m_dbY2Pos); if (iAxisY2Ret < 0) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //XY: Move XY
            if (iAxisXRet == 0) if (m_indexerAxisX.core_MoveAbs(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisY1Ret == 0) if (m_indexerAxisY1.core_MoveAbs(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if (iAxisY1Ret == 0) if (m_indexerAxisY2.core_MoveAbs(mPosition.m_dbY2Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //wait XY to move
            //if ( iAxisXRet == 0)  if  ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            //if ( iAxisYRet == 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
            //if ( !Detect_Moving(false, iAxisXRet, iAxisYRet, iAxisZRet) ) return false;
            Thread.Sleep(500);	//unable to capture the moving signal if the distance is too small. Protection is by checking the position after movement

            //wait XY to stop
            if (iAxisXRet == 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisY1Ret == 0) if (m_indexerAxisY1.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if (iAxisY2Ret == 0) if (m_indexerAxisY2.Detect_Stop() == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //Check position reach
            if (iAxisXRet == 0) if (m_indexerAxisX.Check_ReachedPosition(mPosition.m_dbXPos) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisY1Ret == 0) if (m_indexerAxisY1.Check_ReachedPosition(mPosition.m_dbY1Pos) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if (iAxisY2Ret == 0) if (m_indexerAxisY2.Check_ReachedPosition(mPosition.m_dbY2Pos) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //Move Z Down
            //Z: Move Z to position
            iAxisZRet = m_indexerAxisZ.Check_Position(mPosition.m_dbZPos); if (iAxisZRet < 0) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
            if (iAxisZRet == 0)
            {
                if (m_indexerAxisZ.core_MoveAbs(mPosition.m_dbZPos, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
                //if ( m_indexerAxisZ.Detect_Moving() == false ) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                Thread.Sleep(500);
                if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
                if (m_indexerAxisZ.Check_ReachedPosition(mPosition.m_dbZPos) == false) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
            }

            return true;
        }

		//Move Relative / Jog///////////////////////////////////////////////////////////////////////////////////////////
        public bool Move_Rel(Robot_Pos_Settings mPosition)
        {
            //without interlock--------------------------------------------------------------------------------------------------------------
            if (mPosition.m_iInterlock == 0)
            {
                //XYZ: Move XYZ
                if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.core_MoveRel(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY1.core_MoveRel(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if (mPosition.m_dbY2Pos != 0) if (m_indexerAxisY2.core_MoveRel(mPosition.m_dbY2Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if (mPosition.m_dbZPos != 0) if (m_indexerAxisZ.core_MoveRel(mPosition.m_dbZPos, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                //wait XY to move
                //if ( mPosition.m_dbXPos != 0 ) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                //if ( mPosition.m_dbYPos != 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
                //if ( mPosition.m_dbZPos != 0 ) if ( m_indexerAxisZ.Detect_Moving() == false ) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                //if ( !Detect_Moving(true, 0, 0, 0) ) return false;
                Thread.Sleep(700);

                //wait XY to stop
                if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY1.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
                if (mPosition.m_dbY2Pos != 0) if (m_indexerAxisY2.Detect_Stop() == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }
                if (mPosition.m_dbZPos != 0) if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

                return true;
            }

            //With Interlock----------------------------------------------------------------------------------------------------------------
            //Move Z Up first
            if (m_indexerAxisZ.Check_Position(mPosition.m_dbInterlock_Zht) == 0)
            {
                //Z: Move Z to 0 pos
                if (m_indexerAxisZ.core_MoveAbs(mPosition.m_dbInterlock_Zht, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                if (m_indexerAxisZ.Detect_Moving() == false) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
                if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
            }

            //XY: Move XY
            if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.core_MoveRel(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY1.core_MoveRel(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if (mPosition.m_dbY2Pos != 0) if (m_indexerAxisY1.core_MoveRel(mPosition.m_dbY2Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //wait XY to move
            //if ( mPosition.m_dbXPos != 0) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            //if ( mPosition.m_dbYPos != 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
            //if ( !Detect_Moving(false, 0, 0, 0) ) return false;
            Thread.Sleep(700);

            //wait XY to stop
            if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY1.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY1"); return false; }
            if (mPosition.m_dbY2Pos != 0) if (m_indexerAxisY2.Detect_Stop() == false) { Set_Error(3, m_strLabel + " AxisY2"); return false; }

            //Move Z Down
            //Z: Move Z to position
            if (mPosition.m_dbZPos != 0) if (m_indexerAxisZ.core_MoveRel(mPosition.m_dbZPos, mPosition.m_iRunSpeed_AxisZ) == false) { Set_Error(3, m_strLabel + " AxisZ"); return false; }
            if (mPosition.m_dbZPos != 0) if (m_indexerAxisZ.Detect_Moving() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }
            if (mPosition.m_dbZPos != 0) if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ"); return false; }

            return true;
        }

		//Get Robot position///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Robot_Pos_Settings GetRobotPosition_mm(ref bool bRet)
        {
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
            bRet = true;
            double dbPosX = 0, dbPosY1 = 0, dbPosY2 = 0, dbPosZ = 0;

            //Get Position via Status command
            if (m_indexerAxisX.Get_MotorPosition(ref dbPosX) == false)      { Set_Error(1, m_strLabel + " AxisX"); bRet = false; }
            else if (m_indexerAxisY1.Get_MotorPosition(ref dbPosY1) == false) { Set_Error(2, m_strLabel + " AxisY1"); bRet = false; }
            else if (m_indexerAxisY2.Get_MotorPosition(ref dbPosY2) == false) { Set_Error(3, m_strLabel + " AxisY2"); bRet = false; }
            else if (m_indexerAxisZ.Get_MotorPosition(ref dbPosZ) == false) { Set_Error(4, m_strLabel + " AxisZ"); bRet = false; }

            //Set position to struct
            if (bRet == true)
            {
                mPosition.m_dbXPos = dbPosX;
                mPosition.m_dbY1Pos = dbPosY1;
                mPosition.m_dbY2Pos = dbPosY2;
                mPosition.m_dbZPos = dbPosZ;
            }

            return mPosition;
        }

		//Get Robot Status/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetRobotStatus()
        {
            short iRet = 0;

            //Get Indexer Status
            iRet = m_indexerAxisX.core_GetMotorStatus(); if (iRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return iRet; }
            iRet = m_indexerAxisY1.core_GetMotorStatus(); if (iRet < 0) { Set_Error(2, m_strLabel + " AxisY1"); return iRet; }
            iRet = m_indexerAxisY2.core_GetMotorStatus(); if (iRet < 0) { Set_Error(3, m_strLabel + " AxisY2"); return iRet; }
            iRet = m_indexerAxisZ.core_GetMotorStatus(); if (iRet < 0) { Set_Error(4, m_strLabel + " AxisZ"); return iRet; }

            return iRet;
        }

		//Interlock: Safety Window/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool SafetyWindow(Robot_Pos_Settings mPosition)
        {
            int iErrorID = 0;
            String strErrorAdd = "SafetyWindow ";

            //note: Draw a box with the TR (top right corner) and BL ( bottom left corner) as the limiters
            //note: can be seen as maximum (TR) and minimum (BL)
            if (mPosition.m_dbSafetyWindow_BL_X != 0 &&
                 mPosition.m_dbSafetyWindow_BL_Y != 0 &&
                 mPosition.m_dbSafetyWindow_TR_X != 0 &&
                 mPosition.m_dbSafetyWindow_TR_Y != 0)
            {
                //set error ID
                if (mPosition.m_dbXPos > mPosition.m_dbSafetyWindow_TR_X)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_X;
                else if (mPosition.m_dbXPos < mPosition.m_dbSafetyWindow_BL_X)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_X;
                else if (mPosition.m_dbY1Pos > mPosition.m_dbSafetyWindow_TR_Y || mPosition.m_dbY2Pos > mPosition.m_dbSafetyWindow_TR_Y)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_Y;
                else if (mPosition.m_dbY1Pos < mPosition.m_dbSafetyWindow_BL_Y || mPosition.m_dbY2Pos < mPosition.m_dbSafetyWindow_BL_Y)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_Y;

                //Set Error 
                switch (iErrorID)
                {
                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_X:
                        strErrorAdd += "TR_X value=" + mPosition.m_dbSafetyWindow_TR_X.ToString() + ", Pos Value=" + mPosition.m_dbXPos.ToString();
                        break;

                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_Y:
                        strErrorAdd += "TR_Y value=" + mPosition.m_dbSafetyWindow_TR_Y.ToString() + ", Pos Value Y1=" + mPosition.m_dbY1Pos.ToString() + ", Pos Value Y2=" + mPosition.m_dbY2Pos.ToString();
                        break;

                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_X:
                        strErrorAdd += "BL_X value=" + mPosition.m_dbSafetyWindow_BL_X.ToString() + ", Pos Value Y1=" + mPosition.m_dbXPos.ToString() + ", Pos Value Y2=" + mPosition.m_dbY2Pos.ToString();
                        break;

                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_Y:
                        strErrorAdd += "BL_Y value=" + mPosition.m_dbSafetyWindow_BL_Y.ToString() + ", Pos Value=" + mPosition.m_dbY1Pos.ToString();
                        break;

                    default: return true;
                }
                Set_Error(iErrorID, strErrorAdd);
                return false;
            }

            return true;
        }

        public bool Move_SafetyZHeight(double dbZHeight, int iSpeedAdjustor)
        {
            if (m_indexerAxisZ.Check_Position(dbZHeight) == 0)
            {
                //move Z to Safety Z Height
                if (m_indexerAxisZ.core_MoveAbs(dbZHeight, iSpeedAdjustor) == false) { Set_Error(3, m_strLabel + " AxisZ Move"); return false; }
                //if ( m_indexerAxisZ.Detect_Moving() == false ) { Set_Error(3, m_strLabel + " AxisZ Detect Moving"); return false; }
                Thread.Sleep(1000);
                if (m_indexerAxisZ.Detect_Stop() == false) { Set_Error(4, m_strLabel + " AxisZ Stop Moving"); return false; }
            }

            return true;
        }
	}

	//Settings/
	[Serializable]
	public class RobotXYZ_APSlib_PanasonicECAT_Settings
	{
		public Stepper_Settings m_Settings_AxisX = new Stepper_Settings();
		public Stepper_Settings m_Settings_AxisY1 = new Stepper_Settings();
        public Stepper_Settings m_Settings_AxisY2 = new Stepper_Settings();
        public Stepper_Settings m_Settings_AxisZ = new Stepper_Settings();
	}
}