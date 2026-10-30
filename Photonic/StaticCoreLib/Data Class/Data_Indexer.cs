/****************************************************************************************************************************************
 * Data Indexer class: 
 * Desc:    This class is to keep the single stepper / servo motor settings, positions, and related flags. Ex. Single motor for rotary gripper or End Effectors
 *              Example: Single rotary motor, single Y or X or Z stepper motor
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
    //Indexer data storing class
    [Serializable]
    public class Data_Indexer
    {
        [XmlArray("Indexer_Setting_List")]
        public List<Indexer_Pos_Settings> m_llIndexerSettings;
        
        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_Indexer()
        {
            m_llIndexerSettings = new List<Indexer_Pos_Settings>();
        }

        //Add indexer position into the list//////////////////////////////////////////////////////////////////////////
        public void AddSettings(double dbPos, string strPosName, string strDescription)
        {
            Indexer_Pos_Settings in_set = new Indexer_Pos_Settings();
            in_set.m_dbPos = dbPos;
            in_set.m_strPosName = strPosName;
            in_set.m_strDescription = strDescription;
			in_set.m_iRunSpeed = 100;
			in_set.m_bMoveAbs = true;

            m_llIndexerSettings.Add(in_set);
        }

        //Get Indexer position////////////////////////////////////////////////////////////////////////////////
        public Indexer_Pos_Settings Get_Settings(string strPosName)
        {
            Indexer_Pos_Settings in_set = new Indexer_Pos_Settings();

            foreach (Indexer_Pos_Settings settings in m_llIndexerSettings)
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
		public Indexer_Pos_Settings Get_Settings( int iIndex )
		{
			Indexer_Pos_Settings in_set = new Indexer_Pos_Settings();
			in_set.m_strPosName = "no Position found";

			if ( iIndex < m_llIndexerSettings.Count )
				in_set = m_llIndexerSettings[iIndex];

			return in_set;
		}

        //update Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note: This is meant to update the current indexer pos settings only.
        public bool Update_Settings(Indexer_Pos_Settings settings)
        {
            int iTotal = m_llIndexerSettings.Count();
            for (int i = 0; i < iTotal; i++)
            {
				if ( settings.m_strPosName == m_llIndexerSettings[i].m_strPosName )
				{
					m_llIndexerSettings[i] = settings;
					return true;
				}
            }

			return false;
        }
		
		//Update Settings or Add into list //////////////////////////////////////////////////////////////////////////
		public bool Update_Settings( Data_Indexer mpDataIndexer )
		{
			int iTotal_Input = mpDataIndexer.Get_Settings_Count();

			//loop to add all position
			for ( int iCount = 0; iCount < iTotal_Input; iCount++ )
			{
				Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
				mPos = mpDataIndexer.Get_Settings(iCount);

				//Update position if not add the position
                if (mPos.m_strPosName != "no Position found") Update_Settings(mPos);
                    //if ( !Update_Settings(mPos) )
                    //    m_llIndexerSettings.Add(mPos);
			}

			return true;
		}



        //get settings count////////////////////////////////////////////////////////////////////////////////
        public int Get_Settings_Count() { return m_llIndexerSettings.Count(); }

        //get settings name////////////////////////////////////////////////////////////////////////////////
        public String Get_SettingsName(int iCount) { return m_llIndexerSettings[iCount].m_strPosName; }
        public String Get_SettingsDesc(int iCount) { return m_llIndexerSettings[iCount].m_strDescription; }



    }

    //structure that contains a single indexer position
    public struct Indexer_Pos_Settings
    {
		public double		m_dbPos;
        public string		m_strPosName;
        public string       m_strDescription;
        public int			m_iRunSpeed;
		public bool			m_bMoveAbs;
    }
}
