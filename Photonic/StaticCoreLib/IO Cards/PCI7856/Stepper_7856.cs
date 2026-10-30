/****************************************************************************************************************************************
 * Stepper 7856 motor class
 * Desc:    Main calling class for each stepper motor. 
 *              This class should not contain the robots movement protocol or alogrithm but only the Stepper's calling methods.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using APS_Define_W32;
using APS168_W32;

namespace StaticCoreLib
{
    //Stepper working class
    public class Stepper_7856 : Stepper_Interface
    {
        //variables
        String m_strLabel;

        int m_iAxisID;
        public int m_iMaxVelocity;
        int m_iHomeVelocity;
        int m_iAcceleration;
        int m_iDeceleration;
        int m_iHomeDir;
        int m_iPulsePerRevolution;
        int m_iRunSpeed;                                       //Percentage of the motor set velocity
        int m_iRunSpeed_2;                                  //Percentage of the motor 2nd velocity
        int m_iFlag2Speed = 0;                             //Select primary or secondary speed. Always set to primary speed.
        public int m_iTimeOut {get; set;}                             //time in miliseconds
        public int m_iTimeOutHome { get; set; }                   //time in miliseconds for homing only
        double m_dbmmPerRevolution;
        double m_dbMinStep;

        //motor status
        public int m_iHome {get; set;}                   //home flag
        public int m_iRLimit { get; set; }               //exceeded reverse limit flag
        public int m_iFLimit { get; set; }               //exceeded forward limit flag
        public int m_iSVon { get; set; }                 //motor is on
        public int m_iInPosition { get; set; }           //motor is in position
        public int m_iAlarm { get; set; }                //Alarm
        public int m_iEMG { get; set; }                  //EMG signal
        public int m_iBusy { get; set; }                 //busy signal
        public int m_iMoving { get; set; }              //Motor is moving

        //Constructor/////////////////////////////////////////////////////////
        public Stepper_7856(int iAxisID)
        {
            m_iAxisID = iAxisID;
            m_iMaxVelocity = 10000;
            m_iHomeVelocity = 10000;
            m_iAcceleration = 100000;
            m_iDeceleration = 100000;
            m_iHomeDir = 1;
            m_iPulsePerRevolution = 1000;
            m_iRunSpeed = 100;
            m_iRunSpeed_2 = 100;
            m_dbmmPerRevolution = 10;
            m_dbMinStep = 1;
            m_iTimeOut = 5000;
            m_iTimeOutHome = 10000;

            m_strLabel = "Stepper";
        }

        //Destructor//////////////////////////////////////////////////////////
        ~Stepper_7856()
        {
        }

        //Variables setup//////////////////////////////////////////////////////
        public void Set_HomeDir(int value) { m_iHomeDir = value;}

        public void Set_Velocity(int iHomeSpeed, int iVelocity) 
        { 
            m_iHomeVelocity = iHomeSpeed; 
            m_iMaxVelocity = iVelocity; 

            //set card param
            APS168.APS_set_axis_param(m_iAxisID, (int)APS_Define.PRA_HOME_VM, m_iHomeVelocity);
        }

        public void Set_AccDec(int iAcc, int iDec) 
        { 
            m_iAcceleration = iAcc; 
            m_iDeceleration = iDec;

            //Set card param
            APS168.APS_set_axis_param(m_iAxisID, (int)APS_Define.PRA_ACC, m_iAcceleration);
            APS168.APS_set_axis_param(m_iAxisID, (int)APS_Define.PRA_DEC, m_iDeceleration);
        }
        public void Set_Label(String strLabel) { m_strLabel = strLabel; }
        public void Set_PulsePerRevolution(int pulse) { m_iPulsePerRevolution = pulse; }
        public void Set_mmPerRevolution(double dbmm) { m_dbmmPerRevolution = dbmm; }
        public void Set_RunSpeed(int iRunSpeed) { m_iRunSpeed = iRunSpeed; }
        public void Set_RunSpeed_2(int iRunSpeed) { m_iRunSpeed_2 = iRunSpeed; }

        //Set Movement params////////////////////////////////////////////////
        public void Set_RunAt2ndSpeed(bool bRun)  { if (bRun)  m_iFlag2Speed = 1; }

        //update stepper settings variables//////////////////////////////////////
        public void Update_ToStepper(Stepper_Settings mSettings)
        {
            m_iMaxVelocity = mSettings.m_iVelocity;
            m_iHomeVelocity = mSettings.m_iHomeVelocity;
            m_iHomeDir = mSettings.m_iHomeDir;
            m_iRunSpeed = mSettings.m_iRunSpeed;
            m_iRunSpeed_2 = mSettings.m_iRunSpeed_2;
            m_iPulsePerRevolution = mSettings.m_iPulsePerRevolution;
            m_dbmmPerRevolution = mSettings.m_dbmmPerRevolution;
            m_iTimeOut = mSettings.m_iTimeOut;
            m_iTimeOutHome = mSettings.m_iTimeOutHome;

            m_dbMinStep = m_dbmmPerRevolution / (double)m_iPulsePerRevolution;
        }

        //Turn On of Off//////////////////////////////////////////////////////
        public void Servo_On(int iServoOn)  { APS168.APS_set_servo_on(m_iAxisID, iServoOn); }

        //Go to home position///////////////////////////////////////////////////
        public int Home()
        {
            if (Global_Var.m_bOfflineTest) return 1;

            return APS168.APS_home_move(m_iAxisID);
        }

        //Move Relative ////////////////////////////////////////////////////////////////////
        //note: actual running speed = runspeed (percentage) x velocity
        public int MoveRel(int iPulseValue)
        {
            int iSpeed = 0;

            if (Global_Var.m_bOfflineTest) return 1;

            if (m_iFlag2Speed == 0)
                iSpeed = (m_iRunSpeed * m_iMaxVelocity) / 100;
            else
            {
                iSpeed = (m_iRunSpeed_2 * m_iMaxVelocity) / 100;
                m_iFlag2Speed = 0;
            }

            return APS168.APS_relative_move(m_iAxisID, iPulseValue, iSpeed);
        }

        //Move Absolute///////////////////////////////////////////////////////
        //Note: actual running speed = runspeed (percentage) x velocity
        public int MoveAbs(int iPulsePosition)
        {
            int iSpeed = 0;

            if (Global_Var.m_bOfflineTest) return 1;

            if (m_iFlag2Speed == 0)
                iSpeed = (m_iRunSpeed * m_iMaxVelocity) / 100;
            else
            {
                iSpeed = (m_iRunSpeed_2 * m_iMaxVelocity) / 100;
                m_iFlag2Speed = 0;
            }

            return APS168.APS_absolute_move(m_iAxisID, iPulsePosition, iSpeed);
        }

        //Stop//////////////////////////////////////////////////////////////////
        public void Stop()
        {
            APS168.APS_stop_move(m_iAxisID);
        }

        //Get Motor Status////////////////////////////////////////////////////
        public int GetMotorStatus()
        {
            m_iHome = 0;
            m_iRLimit = 0;
            m_iFLimit = 0;
            m_iSVon = 0;
            m_iInPosition = 0;
            m_iAlarm = 0;
            m_iEMG = 0;

            if (Global_Var.m_bOfflineTest)
            {
                m_iHome = 1;
                m_iInPosition = 1;
                return 1;
            }

            int iStatus1 = APS168.APS_motion_io_status(m_iAxisID);
            int iStatus2 = APS168.APS_motion_status(m_iAxisID);

            if ((iStatus1 & 1) == 1) m_iAlarm = 1;
            if ((iStatus1 & 2) == 1) m_iFLimit = 1;
            if ((iStatus1 & 4) == 1) m_iRLimit = 1;
            if ((iStatus1 & 8) == 1) m_iHome = 1;
            if ((iStatus1 & 16) == 1) m_iEMG = 1;
            if ((iStatus1 & 128) == 1) m_iSVon = 1;

            return 1;
        }

        //Get Motor Position////////////////////////////////////////////////////
        public double Get_MotorPosition_Pulse()
        {
            double dbRet = 0;
            APS168.APS_get_position_f(m_iAxisID, ref dbRet);

            return dbRet;
        }

        public double Get_MotorPosition_mm()
        {
            double dbRet = 0, dbRetmm = 0;

            APS168.APS_get_position_f(m_iAxisID, ref dbRet);
            dbRetmm = PulseToMm(dbRet);

            return dbRetmm;
        }

        //Calculate: Get min step////////////////////////////////////////////////
        public double GetMinStep()
        {
            m_dbMinStep = m_dbmmPerRevolution / (double)m_iPulsePerRevolution;
            return m_dbMinStep;
        }

        //Calculate: Pulse to mm/////////////////////////////////////////////////
        public double PulseToMm(double iPulse)
        {
            return ( iPulse * m_dbMinStep );
        }

        //Calculate: mm to Pulse/////////////////////////////////////////////////
        public int MmToPulse(double dbMm)
        {
            double iTemp = dbMm / m_dbMinStep;

            if (iTemp + 0.5 > (int)iTemp + 1)
                return (int)iTemp + 1;
            else
                return (int)iTemp;
        }
    }
}
