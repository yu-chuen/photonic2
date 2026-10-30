using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Diagnostics;
using System.Windows.Forms;

namespace StaticCoreLib
{
	public class Comm_TCP_KeyenceGT2
	{
		//variables
		Comm_TCP m_Comm;
		Comm_TCP_KeyenceGT2_Settings m_Settings;

		String m_strLabel;
		String m_strSettingFilePath;
		String m_strErrorMessage = "";
		public String m_strETX = "\r\n";
		String m_strErrorMsg = "";
		int m_iErrorID = 0;

		public List<Code_Data_IL> m_ll_CodeData;

		public Comm_TCP_KeyenceGT2(String strLabel, String strSettingFilePath)
        {
            //set label
            m_strLabel = strLabel;

            //set setting file path
            m_strSettingFilePath = strSettingFilePath;
            if (m_strSettingFilePath.EndsWith("\\") == false) m_strSettingFilePath += "\\";

            //Init classes
			m_Settings = new Comm_TCP_KeyenceGT2_Settings();
            m_Comm = new Comm_TCP();

            //get from registry
            From_Registry();

            m_ll_CodeData = new List<Code_Data_IL>();
        }

		public void To_Registry()
		{
			String strSettingFullFilePath = "";
			strSettingFullFilePath = m_strSettingFilePath + m_strLabel + ".xml";

			XMLSerializer.Serialize_Func<Comm_TCP_KeyenceGT2_Settings>(m_Settings, strSettingFullFilePath);
		}

		public void From_Registry()
		{
			String strSettingFullFilePath = "";
			strSettingFullFilePath = m_strSettingFilePath + m_strLabel + ".xml";

			m_Settings = XMLSerializer.Deserialize_Func<Comm_TCP_KeyenceGT2_Settings>(strSettingFullFilePath);
		}

		public Comm_TCP_Settings Get_CommSettings() { return m_Settings.m_ComSettings; }
		public String IPAddress { get { return m_Settings.m_ComSettings.m_strIPAddress; } }
		public int PortNum { get { return m_Settings.m_ComSettings.m_iPortNum; } }
		public int ReceiveTimeout { get { return m_Settings.m_ComSettings.m_iReceiveTimeout; } }
		public int SendTimeout { get { return m_Settings.m_ComSettings.m_iSendTimeout; } }
	

	 public String Trigger
        {
            set{m_Settings.m_strTrigger = value;}
            get{return m_Settings.m_strTrigger;}
        }
        public String TriggerEnd
        {
            set { m_Settings.m_strEndTrigger = value; }
            get { return m_Settings.m_strEndTrigger; }
        }
		
		public bool Connect()
        {
            bool bRet = true;

            //enable or disable
            if (m_Settings.m_bEnable == false) return true;

            //connect to module
            if (! m_Comm.Connect(m_Settings.m_ComSettings.m_strIPAddress,
                                                    m_Settings.m_ComSettings.m_iPortNum,
                                                    m_Settings.m_ComSettings.m_iReceiveTimeout,
                                                    m_Settings.m_ComSettings.m_iSendTimeout))
            {
                bRet = false;
				m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg);
            }

            return bRet;
        }

        //disconnect//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Disconnect()
        {
            m_Comm.Disconnect();
        }

        //Check Connection/////////////////////////////////////////////////////////////////////////////////////////////
        public bool Is_Connected()
        {
            if (m_Comm.Is_Connected() == false)
            {
                m_strErrorMessage = "Not Connected";
                return false;
            }

            return true;
        }

		//Read from the Barcode/////////////////////////////////////////////////////////////////////////////////////
		public bool ReadValue(ref Double dValue)
		{
            Stopwatch mSt = new Stopwatch();
			bool bRet = true;
			String strResponse = "", strError = "", strTemp2 = "";
            String strCommand = "";

            //Compile command structure
            strCommand = m_Settings.m_strTrigger + m_strETX;
            
            //Connect to TCP Port
            if (Connect() == false) return false;

			//Send command to trigger read
			if (!m_Comm.Send_Data(strCommand))
			{
				m_Comm.Get_Error(ref m_iErrorID, ref m_strErrorMessage);
				return false;
			}

			//Loop to stop the read
			mSt.Start();
			while (true)
			{
				String strTemp = "";

				//timeout
				if (mSt.ElapsedMilliseconds > m_Settings.m_ComSettings.m_lgReadTimeout)
				{
                    Set_Error((int)ErrorTable.en_ErrorCode.KEYENCE_IL_READ_TIMEOUT, "Read Timeout reached.\r\nReturn String=" + strResponse);
					//m_Comm.Send_Data(m_Settings.m_strEndTrigger + m_strETX);     //stop barcode from reading
					bRet = false;
					break;
				}

				//Read from TCP Port
				if (m_Comm.Receive_Data(ref strTemp) == false)
				{
					m_Comm.Get_Error(ref m_iErrorID, ref strError);
					bRet = false; break;
				}

				strResponse += strTemp;
				if (strResponse.Contains(m_strETX)) break;
			}
			mSt.Stop();

            strTemp2 = strResponse.Substring(strResponse.LastIndexOf(",") + 1);
            dValue = double.Parse(strTemp2);
            dValue = dValue / 1000;
            
			//stop reading
			if (bRet == false)
			{
				m_Comm.Send_Data(m_Settings.m_strEndTrigger + m_strETX);     //stop barcode from reading
			}

            m_Comm.Disconnect();

			//end
			return bRet; ;

			
		}

		//Set Error///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Set_Error( int iErrorID, String strErrorMessageAdd = "" )
		{
			m_iErrorID = iErrorID;
			m_strErrorMessage = strErrorMessageAdd;
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

	}

	public struct Code_Data_IL
    {
        public string m_strBarcode;
        public string m_strReadRate_UEC;
        public int MatchingValue;
	}

	[Serializable]
	public class Comm_TCP_KeyenceGT2_Settings
	{
		public bool m_bEnable = true;

		//TCP settings
		public Comm_TCP_Settings m_ComSettings = new Comm_TCP_Settings();

		//Command Settings
		public String m_strTrigger = "M0";
		public String m_strEndTrigger = "LOFF";

		//Reader settings
		public int m_iBarcode1MatchingValueThreshold = 60;
		public int m_iBarcode2MatchingValueThreshold = 60;
		public int m_iBarcodeCount = 2;
	}
}
