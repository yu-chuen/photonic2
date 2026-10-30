using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

namespace StaticCoreLib
{
    public class RobotXY_APSlib_PanasonicECAT
    {
        //variables
        RobotXY_APSlib_PanasonicECAT_Settings m_Settings = new RobotXY_APSlib_PanasonicECAT_Settings();
        public Indexer_APSlib_PanasonicECAT m_indexerAxisX = null;
        public Indexer_APSlib_PanasonicECAT m_indexerAxisY = null;

        APS168_Ethercat_PCIcard m_PCICard = null;

        //vars
        public int m_iErrorID = 0;
        public String m_strErrorMessage = "";
        public String m_strLabel = "";
        public String m_strFilePath = "";

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////
        public RobotXY_APSlib_PanasonicECAT(APS168_Ethercat_PCIcard mCard, String strLabel, String strFilePath, int iAxisID_X, int iAxisID_Y)
        {
            m_PCICard = mCard;
            m_strLabel = strLabel;
            m_strFilePath = strFilePath; if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";

            m_indexerAxisX = new Indexer_APSlib_PanasonicECAT(iAxisID_X, m_strLabel + "_AxisX", m_strFilePath, m_PCICard);
            m_indexerAxisY = new Indexer_APSlib_PanasonicECAT(iAxisID_Y, m_strLabel + "_AxisY", m_strFilePath, m_PCICard);

            //get Settings
            From_Registry();
        }


        //Update the Stepper motor with the new settings AND save it to xml file//////////////////////////
        public bool To_Registry()
        {
            bool bRet = true;

            //update to xml file
            String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
            XMLSerializer.Serialize_Func<RobotXY_APSlib_PanasonicECAT_Settings>(m_Settings, strFilePathFull);

            m_indexerAxisX.Set_Settings(m_Settings.m_Settings_AxisX);
            m_indexerAxisY.Set_Settings(m_Settings.m_Settings_AxisY);

            //update motor settings
            if (!Update_MotorDriverSettings(1)) { if (bRet) bRet = false; }
            if (!Update_MotorDriverSettings(2)) { if (bRet) bRet = false; }
            
            return bRet;
        }

        //Get indexer settings parameters. Read from xml file////////////////////////////////////////////////////
        public bool From_Registry()
        {
            bool bRet = true;

            //read from xml file
            String strFilePathFull = m_strFilePath + m_strLabel + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<RobotXY_APSlib_PanasonicECAT_Settings>(strFilePathFull);

            //set to settings
            m_indexerAxisX.Set_Settings(m_Settings.m_Settings_AxisX);
            m_indexerAxisY.Set_Settings(m_Settings.m_Settings_AxisY);

            //update motor settings
            if (!Update_MotorDriverSettings(1)) { Set_Error(1, m_strLabel + " AxisX"); if (bRet) bRet = false; }
            if (!Update_MotorDriverSettings(2)) { Set_Error(2, m_strLabel + " AxisY"); if (bRet) bRet = false; }

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
        public void Set_Error(int iAxisType, String strAddErrorMsg)
        {
            //Set error id
            switch (iAxisType)
            {
                case 1: m_iErrorID = m_indexerAxisX.Get_ErrorID(); m_strErrorMessage = m_indexerAxisX.Get_ErrorMessage(); break;
                case 2: m_iErrorID = m_indexerAxisY.Get_ErrorID(); m_strErrorMessage = m_indexerAxisY.Get_ErrorMessage(); break;
            }

            //Set Additional error message
            m_strErrorMessage += strAddErrorMsg;
        }

        public void Set_Error(String strAddErrorMsg, int iError)
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
            switch (iAxisType)
            {
                case 1: bRet = m_indexerAxisX.Update_MotorDriverSettings(); strAxis = m_strLabel + "_Axis X"; break;
                case 2: bRet = m_indexerAxisY.Update_MotorDriverSettings(); strAxis = m_strLabel + "_Axis Y"; break;
            }

            //Check error
            if (bRet == false)
            {
                Set_Error(iAxisType, strAxis + "Update Motor Driver Settings");
            }

            return bRet;
        }

        //Detect Moving///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Detect_Moving(int iAxisXRun, int iAxisYRun)
        {
            Stopwatch mSt = new Stopwatch();
            bool bXRun = false, bYRun = false;

            //Change 
            if (iAxisXRun == 1) bXRun = true;
            if (iAxisYRun == 1) bYRun = true;

            //process
            mSt.Start();
            while (true)
            {
                //get motor status
                if (iAxisXRun == 0) if (m_indexerAxisX.core_GetMotorStatus() == -1) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (iAxisYRun == 0) if (m_indexerAxisY.core_GetMotorStatus() == -1) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                //time out
                if (iAxisXRun == 0) if (m_indexerAxisX.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds &&
                  m_indexerAxisX.Moving_i != 1)
                    { Set_Error(m_strLabel + " AxisX", (int)ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }

                if (iAxisYRun == 0) if (m_indexerAxisY.m_Settings.m_iTimeOut < mSt.ElapsedMilliseconds &&
                  m_indexerAxisY.Moving_i != 1)
                    { Set_Error(m_strLabel + " AxisY", (int)ErrorTable.en_ErrorCode.INDEXER_TIMEOUT_START); return false; }


                //check status
                if (m_indexerAxisX.Moving_i == 1) bXRun = true;
                if (m_indexerAxisY.Moving_i == 1) bYRun = true;

                if (bXRun == true && bYRun == true) break;
            }
            mSt.Stop();

            return true;
        }




        /*****************************************************************************************************************
         * Movement
         *****************************************************************************************************************/
        //Turn on Motor / Servo On///////////////////////////////////////////////////////////////////////////////////
        public bool Servo_On(bool bON)
        {
            //turn on all axis servo
            if (m_indexerAxisX.core_ServoOn(bON) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.core_ServoOn(bON) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //delay
            Thread.Sleep(1000);

            //check state: Get Status first
            if (m_indexerAxisX.core_GetMotorStatus() == -1) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.core_GetMotorStatus() == -1) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            if (bON == true)
            {
                //Check state: Get servo on
                if (m_indexerAxisX.Svon_i != 1) { Set_Error(m_strLabel + " AxisX", (int)ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
                if (m_indexerAxisY.Svon_i != 1) { Set_Error(m_strLabel + " AxisY", (int)ErrorTable.en_ErrorCode.INDEXER_ON_ERR); return false; }
            }

            return true;
        }

        //Homing//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Homing()
        {
            //Homing Cmd
            if (m_indexerAxisX.core_Home() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.core_Home() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //Detect Moving
            //if ( m_indexerAxisX.Detect_Moving(1) == false ) { Set_Error(1, m_strLabel + " AxisX Detect Moving"); return false; }
            //if ( m_indexerAxisY.Detect_Moving(1) == false ) { Set_Error(2, m_strLabel + " AxisY Detect Moving"); return false; }
            Thread.Sleep(1000);

            //XY: Detect Stop
            if (m_indexerAxisX.Detect_Stop(1) == false) { Set_Error(1, m_strLabel + " AxisX Detect Stop"); return false; }
            if (m_indexerAxisY.Detect_Stop(1) == false) { Set_Error(2, m_strLabel + " AxisY Detect Stop"); return false; }

            //Check current position
            if (m_indexerAxisX.Check_ReachedPosition(0) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.Check_ReachedPosition(0) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            return true;
        }



        //Stop Moving//////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Stop_Move()
        {
            //Stop all axis
            if (m_indexerAxisX.core_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.core_Stop() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            return true;
        }

        //Reset Alarm/////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Reset_Alarm()
        {
            //Reset all alarm
            if (m_indexerAxisX.core_ResetAlarm() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (m_indexerAxisY.core_ResetAlarm() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            return true;
        }

        //Move Absolute///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Move_Abs(Robot_Pos_Settings mPosition)
        {
            int iAxisXRet = 0;
            int iAxisYRet = 0;

            //Safety Window--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (SafetyWindow(mPosition) == false) return false;

            //Without interlock------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
            if (mPosition.m_iInterlock == 0)
            {
                //Check XY Position
                iAxisXRet = m_indexerAxisX.Check_Position(mPosition.m_dbXPos); if (iAxisXRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                iAxisYRet = m_indexerAxisY.Check_Position(mPosition.m_dbY1Pos); if (iAxisYRet < 0) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                //XY: Move XY
                if (iAxisXRet == 0) if (m_indexerAxisX.core_MoveAbs(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (iAxisYRet == 0) if (m_indexerAxisY.core_MoveAbs(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                //wait XY to move
                //if ( iAxisXRet == 0 ) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                //if ( iAxisYRet == 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
                //if ( !Detect_Moving(iAxisXRet, iAxisYRet) ) return false;
                Thread.Sleep(700);	//unable to capture the moving signal if the distance is too small. Protection is by checking the position after movement

                //wait XY to stop
                if (iAxisXRet == 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (iAxisYRet == 0) if (m_indexerAxisY.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                //Check position
                if (m_indexerAxisX.Check_ReachedPosition(mPosition.m_dbXPos) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (m_indexerAxisY.Check_ReachedPosition(mPosition.m_dbY1Pos) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                return true;
            }

            //With Interlock-------------------------------------------------------------------------------------------------------------------------------------------------------
            //Check XY Position
            iAxisXRet = m_indexerAxisX.Check_Position(mPosition.m_dbXPos); if (iAxisXRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            iAxisYRet = m_indexerAxisY.Check_Position(mPosition.m_dbY1Pos); if (iAxisYRet < 0) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //XY: Move XY
            if (iAxisXRet == 0) if (m_indexerAxisX.core_MoveAbs(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisYRet == 0) if (m_indexerAxisY.core_MoveAbs(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //wait XY to move
            //if ( iAxisXRet == 0)  if  ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            //if ( iAxisYRet == 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
            //if ( !Detect_Moving(iAxisXRet, iAxisYRet) ) return false;
            Thread.Sleep(500);	//unable to capture the moving signal if the distance is too small. Protection is by checking the position after movement

            //wait XY to stop
            if (iAxisXRet == 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisYRet == 0) if (m_indexerAxisY.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //Check position reach
            if (iAxisXRet == 0) if (m_indexerAxisX.Check_ReachedPosition(mPosition.m_dbXPos) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (iAxisYRet == 0) if (m_indexerAxisY.Check_ReachedPosition(mPosition.m_dbY1Pos) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

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
                if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY.core_MoveRel(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }


                //wait XY to move
                //if ( mPosition.m_dbXPos != 0 ) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                //if ( mPosition.m_dbYPos != 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
                //if ( !Detect_Moving(0, 0) ) return false;
                Thread.Sleep(700);

                //wait XY to stop
                if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
                if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

                return true;
            }

            //With Interlock----------------------------------------------------------------------------------------------------------------

            //XY: Move XY
            if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.core_MoveRel(mPosition.m_dbXPos, mPosition.m_iRunSpeed_AxisX) == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY.core_MoveRel(mPosition.m_dbY1Pos, mPosition.m_iRunSpeed_AxisY) == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }

            //wait XY to move
            //if ( mPosition.m_dbXPos != 0) if ( m_indexerAxisX.Detect_Moving() == false ) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            //if ( mPosition.m_dbYPos != 0 ) if ( m_indexerAxisY.Detect_Moving() == false ) { Set_Error(2, m_strLabel + " AxisY"); return false; }
            //if ( !Detect_Moving(0, 0) ) return false;
            Thread.Sleep(700);

            //wait XY to stop
            if (mPosition.m_dbXPos != 0) if (m_indexerAxisX.Detect_Stop() == false) { Set_Error(1, m_strLabel + " AxisX"); return false; }
            if (mPosition.m_dbY1Pos != 0) if (m_indexerAxisY.Detect_Stop() == false) { Set_Error(2, m_strLabel + " AxisY"); return false; }


            return true;
        }

        //Get Robot position///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Robot_Pos_Settings GetRobotPosition_mm(ref bool bRet)
        {
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
            bRet = true;
            double dbPosX = 0, dbPosY = 0;

            //Get Position via Status command
            if (m_indexerAxisX.Get_MotorPosition(ref dbPosX) == false) { Set_Error(1, m_strLabel + " AxisX"); bRet = false; }
            else if (m_indexerAxisY.Get_MotorPosition(ref dbPosY) == false) { Set_Error(2, m_strLabel + " AxisY"); bRet = false; }

            //Set position to struct
            if (bRet == true)
            {
                mPosition.m_dbXPos = dbPosX;
                mPosition.m_dbY1Pos = dbPosY;
            }

            return mPosition;
        }

        //Get Robot Status/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int GetRobotStatus()
        {
            short iRet = 0;

            //Get Indexer Status
            iRet = m_indexerAxisX.core_GetMotorStatus(); if (iRet < 0) { Set_Error(1, m_strLabel + " AxisX"); return iRet; }
            iRet = m_indexerAxisY.core_GetMotorStatus(); if (iRet < 0) { Set_Error(2, m_strLabel + " AxisY"); return iRet; }

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
                else if (mPosition.m_dbY1Pos > mPosition.m_dbSafetyWindow_TR_Y)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_Y;
                else if (mPosition.m_dbY1Pos < mPosition.m_dbSafetyWindow_BL_Y)
                    iErrorID = (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_Y;

                //Set Error 
                switch (iErrorID)
                {
                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_X:
                        strErrorAdd += "TR_X value=" + mPosition.m_dbSafetyWindow_TR_X.ToString() + ", Pos Value=" + mPosition.m_dbXPos.ToString();
                        break;

                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_TOPRIGHT_BREACH_Y:
                        strErrorAdd += "TR_Y value=" + mPosition.m_dbSafetyWindow_TR_Y.ToString() + ", Pos Value=" + mPosition.m_dbY1Pos.ToString();
                        break;

                    case (int)ErrorTable.en_ErrorCode.SAFETYWINDOW_BTMLEFT_BREACH_X:
                        strErrorAdd += "BL_X value=" + mPosition.m_dbSafetyWindow_BL_X.ToString() + ", Pos Value=" + mPosition.m_dbXPos.ToString();
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
    }


	//Settings/
	[Serializable]
	public class RobotXY_APSlib_PanasonicECAT_Settings
	{
		public Stepper_Settings m_Settings_AxisX = new Stepper_Settings();
		public Stepper_Settings m_Settings_AxisY = new Stepper_Settings();
	}
}
