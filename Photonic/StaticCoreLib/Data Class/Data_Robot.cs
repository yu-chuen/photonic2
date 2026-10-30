/****************************************************************************************************************************************
 * Data Robot class: 
 * Desc:    This class is to keep the robot positions, and related flags. Ex. robotic arm, cartesian (XYZ) robot.
 *               Robot position is kept in mm and not in pulse. Calculation to pulse is done at the stepper class
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace StaticCoreLib
{
    //Robot data storing class
    [Serializable]
    public class Data_Robot
    {
        [XmlArray("Robot_Setting_List")]
        public List<Robot_Pos_Settings> m_llRobotSettings;

        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_Robot()
        {
            m_llRobotSettings = new List<Robot_Pos_Settings>();
        }

        //Add robot position into the list//////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adding robot position to the list
        /// </summary>
        /// <param name="dbXPos">X position in mm</param>
        /// <param name="dbYPos">Y position in mm</param>
        /// <param name="dbZPos">Z position in mm</param>
        /// <param name="strPosName">position name</param>
        /// <param name="iMoveType">0=move abs, 1 move rel</param>
        /// <param name="dbRPos">R rotation in deg</param>
        /// <param name="iSpeedSelect">1=Speed1, 2=speed2(others)</param>
        public void AddSettings(	double dbXPos, double dbYPos, double dbZPos,
												string strPosName, string strPosDesc, int iMoveType, double dbRPos = 0,
												int iRunSpeed_AxisX = 100, int iRunSpeed_AxisY = 100, int iRunSpeed_AxisZ = 100, int iRunSpeed_AxisR = 100,
												int iInterlock = 1, double dbInterlockZht = 0)
        {
            Robot_Pos_Settings rb_set = new Robot_Pos_Settings();
            rb_set.m_dbXPos = dbXPos;
            rb_set.m_dbY1Pos = dbYPos;
            rb_set.m_dbY2Pos = dbYPos;
            rb_set.m_dbZPos = dbZPos;
			rb_set.m_dbRPos = dbRPos;
			rb_set.m_iMoveType = iMoveType;
			rb_set.m_strPosName = strPosName;
			rb_set.m_strPosDesc = strPosDesc;
			rb_set.m_iInterlock = iInterlock;
			rb_set.m_dbInterlock_Zht = dbInterlockZht;

			rb_set.m_iRunSpeed_AxisX	= iRunSpeed_AxisX;
			rb_set.m_iRunSpeed_AxisY	= iRunSpeed_AxisY;
			rb_set.m_iRunSpeed_AxisZ	= iRunSpeed_AxisZ;
			rb_set.m_iRunSpeed_AxisR	= iRunSpeed_AxisR;

            rb_set.m_dbSafetyWindow_TR_X = 0;
            rb_set.m_dbSafetyWindow_TR_Y = 0;
            rb_set.m_dbSafetyWindow_BL_X = 0;
            rb_set.m_dbSafetyWindow_BL_Y = 0;
            
            m_llRobotSettings.Add(rb_set);
        }
		

        //Get robot position////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get Settings via Position name
        /// </summary>
        public Robot_Pos_Settings Get_Settings(string strPosName)
        {
            Robot_Pos_Settings in_set = new Robot_Pos_Settings();
            in_set.m_dbXPos = 0;
            in_set.m_dbY1Pos = 0;
            in_set.m_dbY2Pos = 0;
            in_set.m_dbZPos = 0;
            in_set.m_dbRPos = 0;
            in_set.m_iMoveType = 0;
			in_set.m_dbInterlock_Zht = 0;
			in_set.m_iInterlock = 1;
			in_set.m_strPosName = "no Position found";
            in_set.m_strPosDesc = "";

			in_set.m_iRunSpeed_AxisX = 100;
			in_set.m_iRunSpeed_AxisY = 100;
			in_set.m_iRunSpeed_AxisZ = 100;
			in_set.m_iRunSpeed_AxisR = 100;

            in_set.m_dbSafetyWindow_TR_X = 0;
            in_set.m_dbSafetyWindow_TR_Y = 0;
            in_set.m_dbSafetyWindow_BL_X = 0;
            in_set.m_dbSafetyWindow_BL_Y = 0;

            foreach (Robot_Pos_Settings settings in m_llRobotSettings)
            {
                if (settings.m_strPosName == strPosName)
                {
                    in_set = settings;
                    break;
                }
            }

            return in_set;
        }

        /// <summary>
        /// Get Settings via position list index. iIndex starts from 0
        /// </summary>
        public Robot_Pos_Settings Get_Settings(int iIndex)
        {
            Robot_Pos_Settings in_set = new Robot_Pos_Settings();
            in_set.m_dbXPos = 0;
            in_set.m_dbY1Pos = 0;
            in_set.m_dbY2Pos = 0;
            in_set.m_dbZPos = 0;
            in_set.m_dbRPos = 0;
			in_set.m_iMoveType = 0;
			in_set.m_dbInterlock_Zht = 0;
			in_set.m_iInterlock = 1;
            in_set.m_strPosName = "no Position found";
            in_set.m_strPosDesc = "";

			in_set.m_iRunSpeed_AxisX = 100;
			in_set.m_iRunSpeed_AxisY = 100;
			in_set.m_iRunSpeed_AxisZ = 100;
			in_set.m_iRunSpeed_AxisR = 100;

            in_set.m_dbSafetyWindow_TR_X = 0;
            in_set.m_dbSafetyWindow_TR_Y = 0;
            in_set.m_dbSafetyWindow_BL_X = 0;
            in_set.m_dbSafetyWindow_BL_Y = 0;

            if (iIndex < m_llRobotSettings.Count())
            {
                in_set = m_llRobotSettings[iIndex];
            }

            return in_set;
        }

        //update Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Update_Settings(Robot_Pos_Settings settings)
        {
            int iTotal = m_llRobotSettings.Count();
            for (int i = 0; i < iTotal; i++)
            {
				if ( settings.m_strPosName == m_llRobotSettings[i].m_strPosName )
				{
					m_llRobotSettings[i] = settings;
					return true;
				}
            }
			return false;
        }

		//Update Settings or Add into list //////////////////////////////////////////////////////////////////////////
		public bool Update_Settings( Data_Robot mpDataRobot )
		{
			int iTotal_Input = mpDataRobot.Get_Settings_Count();

			//loop to add all position
			for ( int iCount = 0; iCount < iTotal_Input; iCount++ )
			{
				Robot_Pos_Settings mPos = new Robot_Pos_Settings();
				mPos = mpDataRobot.Get_Settings(iCount);

				//Update position if not add the position
                if (mPos.m_strPosName != "no Position found") Update_Settings(mPos);
					//if (!Update_Settings(mPos)) 
						//m_llRobotSettings.Add(mPos);

			}

			return true;
		}

        //get settings count///////////////////////////////////////////////////////////////////////////////////////////////////
        public int Get_Settings_Count() { return m_llRobotSettings.Count(); }

        //get settings name////////////////////////////////////////////////////////////////////////////////
        public String Get_SettingsName(int iCount) { return m_llRobotSettings[iCount].m_strPosName; }
        public String Get_SettingsDesc(int iCount) { return m_llRobotSettings[iCount].m_strPosDesc; }
    }

    //structure that contains a single robot position
    public struct Robot_Pos_Settings
    {
        public double m_dbXPos;                             //X position in mm
        public double m_dbY1Pos;                             //Y1 position in mm
        public double m_dbY2Pos;                             //Y2 position in mm
        public double m_dbZPos;                             //Z position in mm
        public double m_dbRPos;                            //R position in mm
        public string m_strPosName;                       //Position name or code
        public string m_strPosDesc;                        //Position Description
        public int m_iMoveType;                               //Move type selection - 1 => move absolute, others => move relative
		public int m_iInterlock;									//Move with the Z height up to 0 or to a certain height: 1=> interlock active, else=>interlock deactivated
		public double m_dbInterlock_Zht;				//Move with Z height up to zht

		public int m_iRunSpeed_AxisX;					//Speed adjustor:
		public int m_iRunSpeed_AxisY;					//Speed adjustor:
		public int m_iRunSpeed_AxisZ;					//Speed adjustor:
		public int m_iRunSpeed_AxisR;					//Speed adjustor:

        //safety window
        public double m_dbSafetyWindow_TR_X;
        public double m_dbSafetyWindow_TR_Y;
        public double m_dbSafetyWindow_BL_X;
        public double m_dbSafetyWindow_BL_Y;

        public String m_strAddInfo;
    }

    //structure that contains a single robot speed settings
    public struct Robot_Speed_Settings
    {
        //for Gantry XYZ robot use
        public int m_iXSpeed;
        public int m_iYSpeed;
        public int m_iZSpeed;

        //for Epson use
        public int m_iArmSpeed;
        public int m_iArmRotarySpeed;
    }
}
