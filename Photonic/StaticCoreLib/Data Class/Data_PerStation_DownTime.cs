using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    [Serializable]
    public class Data
    {
        //fixed
        public string m_str_StationName    = "";

        //resettable
        public int m_i_ThreadIdentifier    = 0;
        public string m_str_Down_DateTime  = "";
        public string m_str_Start_DateTime = "";
        public double m_db_Total_DownTime  = 0;
        public string m_str_Description    = "";
    }

    public class Data_PerStation_DownTime
    {
        //variables
        string m_strFileName = "";
        string m_strFilePath = "";
        public Data m_Data = new Data();

        //constructor
        public Data_PerStation_DownTime(string strFileName, string strFilePath)
        {
            m_strFileName = strFileName;
            m_strFilePath = strFilePath;
            if (m_strFilePath.EndsWith("\\") == false) { m_strFilePath += "\\"; }

            //load settings from file
            Load_Settings();
        }

        //Load From File
        public void Load_Settings()
        {
            string strFullFilePath = m_strFilePath + m_strFileName + ".xml";
            m_Data = XMLSerializer.Deserialize_Func<Data>(strFullFilePath);
        }

        //Save To File
        public void Save_Settings()
        {
            string strFullFilePath = m_strFilePath + m_strFileName + ".xml";
            XMLSerializer.Serialize_Func<Data>(m_Data, strFullFilePath);
        }

        //reset data
        public void Reset_Data()
        {
            m_Data.m_i_ThreadIdentifier = 0;
            m_Data.m_str_Down_DateTime = "";
            m_Data.m_str_Start_DateTime = "";
            m_Data.m_db_Total_DownTime = 0;
            m_Data.m_str_Description = "";
        }
    }
}