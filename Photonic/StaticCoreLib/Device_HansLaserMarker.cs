using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;
using System.IO;

namespace StaticCoreLib
{
    public class Device_HansLaserMarker
    {
        //variables
        Device_HansLaserMarker_Settings m_Settings;

        public String m_strFilePath = "";
        public String m_strLabel = "HansLaser";
        String m_strErrorMessage = "";

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Device_HansLaserMarker(String strMacFilePath)
        {
            m_strFilePath = strMacFilePath;
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";

            m_Settings = new Device_HansLaserMarker_Settings();
            From_Registry();
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFullFilePath = m_strFilePath + m_strLabel + ".xml";
            XMLSerializer.Serialize_Func<Device_HansLaserMarker_Settings>(m_Settings, strFullFilePath);
        }

        //From Registry////////////////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFullFilePath = m_strFilePath + m_strLabel + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<Device_HansLaserMarker_Settings>(strFullFilePath);
        }

        //get n set Settings///////////////////////////////////////////////////////////////////////////////////////////////////////
        public Device_HansLaserMarker_Settings Get_Settings() { return m_Settings; }
        public String DataFilePath
        {
            set { m_Settings.m_strFilePath = value; }
            get { return m_Settings.m_strFilePath; }
        }
        public String DataFileName
        {
            set { m_Settings.m_strFileName = value; }
            get { return m_Settings.m_strFileName; }
        }
        public String AppFilePath
        {
            set { m_Settings.m_strAppFilePath = value; }
            get { return m_Settings.m_strAppFilePath; }
        }
        public String AppFilename
        {
            set { m_Settings.m_strAppFilename = value; }
            get { return m_Settings.m_strAppFilename; }
        }

        public bool DeviceEnable
        {
            set { m_Settings.m_bEnable = value; }
            get { return m_Settings.m_bEnable; }
        }


        //Protocol and process
        //Write to ID to File//////////////////////////////////////////////////////////////////////////////////////////////
        public bool Write_ToFile(String strCode1, String strCode2)
        {
            String strFull = "";
            String strFullFilePath = "";

            //set file path
            if (DataFilePath.EndsWith("\\") == false) DataFilePath += "\\";
            strFullFilePath = DataFilePath + DataFileName + ".txt";

            //Write to file
            try
            {
                StreamWriter mSt = new StreamWriter(strFullFilePath, false);
                mSt.Write(strFull);
                mSt.Close();
            }
            catch (Exception ex)
            {
                m_strErrorMessage = ex.Message;
                return false;
            }

            return true;
        }

        //Check file Exist////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Is_DataFileExist()
        {
            String strFullFilePath = "";

            if (DataFilePath.EndsWith("\\") == false) DataFilePath += "\\";
            strFullFilePath = DataFilePath + DataFileName + ".txt";

            return File.Exists(strFullFilePath);
        }

        //Check App file Exist//////////////////////////////////////////////////////////////////////////////////////////
        public bool Is_AppFileExist()
        {
            String strFullFilePath = "";

            //get full file path
            if (m_Settings.m_strAppFilePath.EndsWith("\\") == false) m_Settings.m_strAppFilePath += "\\";
            strFullFilePath = m_Settings.m_strAppFilePath + m_Settings.m_strAppFilename + ".exe";

            //Check file exist
            if (File.Exists(strFullFilePath) == false)
            {
                m_strErrorMessage = "Error: HANS LASER application does not exist. \r\nPlease check the current file location:\r\n" + strFullFilePath;
                return false;
            }

            return true;
        }
        
        //Delete file////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Delete_DataFile()
        {
            String strFullFilePath = "";

            if (DataFilePath.EndsWith("\\") == false) DataFilePath += "\\";
            strFullFilePath = DataFilePath + DataFileName + ".txt";

            File.Delete(strFullFilePath);
        }

        //Open Hans Laser Application////////////////////////////////////////////////////////////////////////
        public bool Open_HLMApplication()
        {
            String strFullFilePath = "";

            //get full file path
            if (m_Settings.m_strAppFilePath.EndsWith("\\") == false) m_Settings.m_strAppFilePath += "\\";
            strFullFilePath = m_Settings.m_strAppFilePath + m_Settings.m_strAppFilename + ".exe";

            //Check if the file exist
            if (Is_AppFileExist() == false) return false;

            //Check if double open
            Process[] pPr = Process.GetProcesses();
            foreach (Process pr in pPr)
            {
                //MessageBox.Show(pr.ProcessName);
                if (pr.ProcessName == m_Settings.m_strAppFilename)
                {
                    m_strErrorMessage = "\r\nWarning: HANS LASER application is already opened.";
                    return false;
                }
            }

            //Open the Laser application
            ProcessStartInfo mProcessStartInfo = new ProcessStartInfo(strFullFilePath);
            mProcessStartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            System.Diagnostics.Process.Start(mProcessStartInfo);

            return true;
        }

        //Get Error Message///////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = "";
            strError = "Device Laser Marker Error: " + m_strErrorMessage;
            return strError;
        }
    }

    //Setting file
    [Serializable]
    public class Device_HansLaserMarker_Settings
    {
        public String m_strFileName = "";
        public String m_strFilePath = "";
        public String m_strAppFilename = "HLM";
        public String m_strAppFilePath = @"C:\HANS LASER\Han's Laser Marking Control SoftwareV1_1_1\";

        public bool m_bEnable = true;

		public long m_lg_TimeOutLaserMarker = 3000;

    }
}
