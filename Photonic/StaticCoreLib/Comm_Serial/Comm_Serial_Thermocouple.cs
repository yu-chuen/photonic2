using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;
using System.Threading;

namespace StaticCoreLib
{
    public class Comm_Serial_Thermocouple
    {
        //variables
        public Comm_Serial m_ComPort;
        public CommSerial_Thermocouple_Settings m_Settings = new CommSerial_Thermocouple_Settings();

        public string m_strTemperature = "";
        String m_strLabel;
        String m_strFilePath;
        String m_strErrorMessage = "";
        String m_strEndofTrans = "\r";
        int m_iErrorID = 0;
        int iDelay = 10000;

        //Constructor//////////////////////////////////////////////////////////////////////////
        public Comm_Serial_Thermocouple(String strLabel, String strFilePath)
        {
            m_strLabel = strLabel;
            m_strFilePath = strFilePath;

            m_ComPort = new Comm_Serial(m_Settings.m_Settings);
            From_Registry();
        }

        //Setting to registry/////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFullFilePath = m_strFilePath + m_strLabel + ".xml";
            XMLSerializer.Serialize_Func<CommSerial_Thermocouple_Settings>(m_Settings, strFullFilePath);
        }

        //Settting from Registry/////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<CommSerial_Thermocouple_Settings>(strFulFilePath);

            //set to com port settings
            m_ComPort.Set_CommSettings(m_Settings.m_Settings);
        }

        public int ReadTimeout
        {
            set { m_Settings.m_Settings.m_iTimeOut_Read = value; }
            get { return m_Settings.m_Settings.m_iTimeOut_Read; }
        }

        //Get Settings///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Enable { set { m_Settings.m_bEnable = value; } get { return m_Settings.m_bEnable; } }

        //Connect to Reader/////////////////////////////////////////////////////////////////////////////////////////////
        public bool Connect()
        {
            //enable / disable
            //if (m_Settings.m_bEnable == false) return true;

            //Set Serial port settings
            m_Settings.m_Settings.m_strTerminator = m_strEndofTrans;
            m_ComPort.Set_CommSettings(m_Settings.m_Settings);

            //Connect to serial port
            if (!m_ComPort.Open_Port())
            {
                m_strErrorMessage = m_ComPort.Get_ErrorMessage();
                return false;
            }

            return true;
        }

        //Disconnect///////////////////////////////////////////////////////////////////////////////////////////////////
        public void Disconnect()
        {
            m_ComPort.Close_Port();
        }
        
        //Read function/////////////////////////////////////////////////////////////////////////////////////
        public bool Read_Temperature(ref String strTemperature)
        {
            Stopwatch mSt = new Stopwatch();
            String strResponse = "";
            String strCommand = "";
            String strError = "";
            
            //connect to com port
            if (Connect() == false) return false;

            //Compile command structure
            strCommand ="#010";

            //Send command to trigger read
            if (!m_ComPort.Write(strCommand)) { m_strErrorMessage = m_ComPort.Get_ErrorMessage(); return false; }

            mSt.Start();
            while (true)
            {              
                //Read from Serial Port
                if (m_ComPort.Read(ref strResponse) == false)
                {
                    //MessageBox.Show("response fault");
                    m_strErrorMessage = m_ComPort.Get_ErrorMessage();
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

            strResponse = strResponse.Substring(3, 5);
            strTemperature = strResponse;
            strTemperature = strTemperature.Replace(m_strEndofTrans, "").Trim();

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
            strCodeData = strResponse.Split(' ');

            //populate code data structure
            foreach (String str in strCodeData)
            {
                switch (iCount)
                {
                    case 0:
                        m_strTemperature = strCodeData[iCount]; break;
                }
                iCount++;
            }

            //end
        End:
            return bRet;
        }

        //Get Error////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public int Get_ErrorID()
        {
            int iErrorID = m_iErrorID;
            m_iErrorID = 0;
            return iErrorID;
        }

        //Get Error Message//////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = "";
            strError = "Thermocouple Error: " + m_strErrorMessage;
            return strError;
        }
    }

    //Note: Settings for the wire tester Hioki class. This class is created incase of additional settings that this ready is required
    [Serializable]
    public class CommSerial_Thermocouple_Settings
    {
        public bool m_bEnable = true;
        public Comm_Serial_Settings m_Settings = new Comm_Serial_Settings();
    }
}
