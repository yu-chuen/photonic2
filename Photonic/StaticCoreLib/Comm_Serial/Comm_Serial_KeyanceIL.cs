using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace StaticCoreLib
{
    public class Comm_Serial_KeyenceIL
    {
        //variables
        public Comm_Serial m_Comm;
        public CommSerial_Thermocouple_Settings m_Settings;
        public Code_Data_KeyanceIL m_CodeData;

        String m_strLabel;
        String m_strSettingFilePath;
        String m_strErrorMessage = "";
        String m_strEndofTrans = "\r";
        public String m_strETX = "\r\n";
        int m_iErrorID = 0;
        int iDelay = 10000;

        //Constructor//////////////////////////////////////////////////////////////////////////
        public Comm_Serial_KeyenceIL(String strLabel, String strSettingFilePath)
        {
            //set label
            m_strLabel = strLabel;

            //set setting file path
            m_strSettingFilePath = strSettingFilePath;
            if (m_strSettingFilePath.EndsWith("\\") == false) m_strSettingFilePath += "\\";

            //get settings from registry
            m_Settings = new CommSerial_Thermocouple_Settings();
            From_Registry();

            //init classes
            m_Comm = new Comm_Serial(m_Settings.m_Settings);
            m_CodeData = new Code_Data_KeyanceIL();

        }

        //Setting to registry/////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strSettingFullFilePath = "";
            strSettingFullFilePath = m_strSettingFilePath + m_strLabel + ".xml";

            XMLSerializer.Serialize_Func<CommSerial_Thermocouple_Settings>(m_Settings, strSettingFullFilePath);
        }

        //Settting from Registry/////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strSettingFullFilePath = "";
            strSettingFullFilePath = m_strSettingFilePath + m_strLabel + ".xml";

            m_Settings = XMLSerializer.Deserialize_Func<CommSerial_Thermocouple_Settings>(strSettingFullFilePath);
        }

        //Get / set Settings values//////////////////////////////////////////////////////////////////////////////////////
        public CommSerial_Thermocouple_Settings Get_Settings() { return m_Settings; }
        public void Set_Settings(CommSerial_Thermocouple_Settings mSettings) { m_Settings = mSettings; }
        public int ReadTimeout
        {
            set { m_Settings.m_Settings.m_iTimeOut_Read = value; }
            get { return m_Settings.m_Settings.m_iTimeOut_Read; }
        }

        //Connect to Reader/////////////////////////////////////////////////////////////////////////////////////////////
        public bool Connect()
        {
            //Set Serial port settings
            m_Settings.m_Settings.m_strTerminator = m_strEndofTrans;
            m_Comm.Set_CommSettings(m_Settings.m_Settings);

            //Connect to serial port
            if (!m_Comm.Open_Port())
            {
                m_strErrorMessage = m_Comm.Get_ErrorMessage();
                return false;
            }

            return true;
        }

        //Disconnect///////////////////////////////////////////////////////////////////////////////////////////////////
        public void Disconnect()
        {
            m_Comm.Close_Port();
        }

        //Read from the Bank number/////////////////////////////////////////////////////////////////////////////////////
        public bool Read_TorqueValue(ref String strData)
        {
            Stopwatch mSt = new Stopwatch();
            String strResponse = "";
            String strCommand = "";
            String strError = "";

            //connect to Screwdriver
            if (Connect() == false) return false;

            //Compile command structure
            strCommand = "M0" + m_strETX;

            //Send command to trigger read
            if (!m_Comm.Write(strCommand)) { m_strErrorMessage = m_Comm.Get_ErrorMessage(); return false; }

            mSt.Start();
            while (true)
            {
                //Read from Serial Port
                if (m_Comm.Read(ref strResponse) == false)
                {
                    //MessageBox.Show("response fault");
                    m_strErrorMessage = m_Comm.Get_ErrorMessage();
                    Thread.Sleep(200);
                    break;
                    //return false;
                }

                if (strResponse != "") break;

                if (mSt.ElapsedMilliseconds > iDelay)
                {
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_UNIQUE_READTIMEOUT;
                    m_strErrorMessage = "Read Data Time Out. \r\n" + strError;
                    mSt.Stop();
                    return false;
                }

            }
            mSt.Stop();


            strData = strResponse.Replace(m_strEndofTrans, "").Trim();

            if (Decode_Response(strData) == false)
            {
                return false;
            }



            return true;
        }

        //Decode Response////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Decode_Response(String strResponse)
        {
            String[] strCodeData;
            bool bRet = true;
            int iCount = 0;

            //Check response for error
            if (strResponse.Contains("ERROR"))
            {
                m_strErrorMessage = "Error: Unable to read the Code.\r\nPlease try re-align the reader again.\r\nResponse=" + strResponse + "\r\n\r\n";
                bRet = false;
                goto End;
            }

            //Split the response
            strCodeData = strResponse.Split(',');

            //populate code data structure
            foreach (String str in strCodeData)
            {
                switch (iCount)
                {
                    case 0:
                        m_CodeData.m_strHeightCheck1 = strCodeData[2];
                        m_CodeData.m_strHeightCheck2 = strCodeData[3]; break;
                        
                }
                iCount++;
            }

            //end
        End:
            return bRet;
        }


        //Get Error Message//////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = "";
            strError = "Keyance IL Error: " + m_strErrorMessage;
            return strError;
        }

        //internal variables/////////////////////////////////////////////////////////////////////////////////////////////////
        public String CodeDisable_str { get { return "Code_Disabled"; } }

    }

    public struct Code_Data_KeyanceIL
    {
        public string m_strHeightCheck1;
        public string m_strHeightCheck2;
    }

    //Note: Settings for the wire tester Hioki class. This class is created incase of additional settings that this ready is required
    [Serializable]
    public class CommSerial_KeyanceIL_Settings
    {
        public bool m_bEnable = true;
        public Comm_Serial_Settings m_Settings = new Comm_Serial_Settings();
    }
}
