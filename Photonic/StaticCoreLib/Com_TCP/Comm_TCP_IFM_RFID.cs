/****************************************************************************************************************************************
 * IFM, RFID Reader: 
 * Desc:    This class wraps the protocols for this RFID reader module which uses TCP/IP to communicate.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;

namespace StaticCoreLib
{
	//IFM RFID Class
	public class Comm_TCP_RFID
	{
		//variables
		public IFM_Settings m_Settings = null;
		Comm_TCP m_Comm = new Comm_TCP();

		public String m_strReadCmd = "";
		public String m_strWriteData = "";
		public String m_strReceiveData = "";
		public String m_strRFID_ID = "";
		public String m_strLabel = "IFM";
		public String m_strFilePath = "";
		public String m_strDebug = "";
		bool m_bConnected = false;

		String m_strErrorMsg = "";
		int m_iErrorID = 0;

		private readonly object m_lock_Access = new object();				//lock, only allow a reader to read at a time only

		//Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public Comm_TCP_RFID( String strPath, String strLabel )
		{
			m_strFilePath = strPath;
			if ( m_strFilePath.EndsWith("\\") == false ) m_strFilePath += "\\";

			m_strLabel = strLabel;

			m_Settings = new IFM_Settings();
			From_Registry();
		}

		//Destructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		~Comm_TCP_RFID()
		{
			this.Disconnect();
		}

		//From registry////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void From_Registry()
		{
			String strFileFullPath = m_strFilePath + m_strLabel + ".xml";
			m_Settings = XMLSerializer.Deserialize_Func<IFM_Settings>(strFileFullPath);
		}

		//To Registry////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void To_Registry()
		{
			String strFileFullPath = m_strFilePath + m_strLabel + ".xml";
			XMLSerializer.Serialize_Func<IFM_Settings>(m_Settings, strFileFullPath);
		}

		//Connect to RFID Reader///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Connect()
		{
			if ( m_bConnected ) return true;
			bool ret = true;
			String strSettingCmd = "";
			String strDebug = "";

			//Disable
			if ( m_Settings.m_bEnable == false ) return true;

			//Connect to module
			if ( m_Comm.Connect(m_Settings.m_strIPAddress,
													m_Settings.m_iPortNum)
													== false )
			{
				m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg);
				return false;
			}
			m_bConnected = true;
			
			//Configure Unit
			if ( m_Comm.Send_Data("CU_00_00_00_00_00_AS\r\n") == false ) { m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg); ret = false; goto Finish; }
			if ( m_Comm.Receive_Data(ref m_strReceiveData) == false ) { m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg); ret = false; goto Finish; }

			if ( Decode_Response(1) == false ) { m_Comm.Disconnect(); return false; }
			strDebug += "Configure: " + m_strReceiveData + "\r\n";

			//Set Parameters
			for ( int iCount = 1; iCount <= 4; iCount++ )
			{
				//check reader available
				if ( iCount == 1 && m_Settings.m_bChannel1 == false ) continue;
				if ( iCount == 2 && m_Settings.m_bChannel2 == false ) continue;
				if ( iCount == 3 && m_Settings.m_bChannel3 == false ) continue;
				if ( iCount == 4 && m_Settings.m_bChannel4 == false ) continue;

				//compile command
				strSettingCmd = "CI_" + iCount.ToString("D2") + "_11_1000_004_250_01_01_00";

				//send command
				if ( m_Comm.Send_Data(strSettingCmd + "\r\n") == false ) { m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg); ret = false; goto Finish; }
				if ( m_Comm.Receive_Data(ref m_strReceiveData) == false ) { m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg); ret = false; goto Finish; }

				if ( Decode_Response(2) == false ) { m_Comm.Disconnect(); return false; }
				strDebug += "Set_Param: " + m_strReceiveData;
			}

Finish:
			m_strReceiveData = strDebug;
			if ( ret == false )
			{
				m_Comm.Disconnect();
				return false;
			}

			return true;
		}

		//Disconnect from RFID Reader/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Disconnect()
		{
			if ( m_bConnected )
			{
				m_Comm.Disconnect();
				m_bConnected = false;
			}
		}

		//Send Command////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Send_Command( int iChannel, ref String strRFID, int iExtraDataLength = 0 )
		{
			lock ( m_lock_Access )
			{
				//RFID disable
				if ( m_Settings.m_bEnable == false )
				{
					strRFID = "Dummy_Test1";
					return true;
				}

				bool ret = true;
				int iRetry = 0;
				int iDataLength = m_Settings.m_iByteLength;
				this.m_strReadCmd = "";
				this.m_strReceiveData = "";
				this.m_strRFID_ID = "";

RETRY:
				//check
				if ( m_bConnected == false ) if ( this.Connect() == false ) return false;

				//Addition Data to be read
				iDataLength += iExtraDataLength;

				//Compile Sending String
				this.m_strReadCmd = "RD_" + iChannel.ToString("D2") + "_00009_" + iDataLength.ToString("D4");
				
				//send command
				if ( m_Comm.Send_Data(this.m_strReadCmd + "\r\n") == false ) { ret = false; goto Finish; }
				if ( m_Comm.Receive_Data(ref m_strReceiveData) == false ) { ret = false; goto Finish; }
				
				//get the SN from the response
				if ( Decode_Response(3) == false ) return false;

				//retry if the string is empty
				if ( this.m_strRFID_ID == "" && iRetry < 3 ) { iRetry++; goto RETRY; }
				//Check: If String is empty. return error
				if ( this.m_strRFID_ID == "" && iRetry >= 3 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.IFM_RETURN_EMPTY_STRING; return false; }
Finish:
				//Comm failure. return error
				if ( ret == false )
				{
					m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg);
					return false;
				}

				//return via reference
				strRFID = this.m_strRFID_ID;		//cannot use this global variable, might crash the program due to competing threads
			}
			return true;
		}

		//Write Command////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Write_Command( int iChannel, String strData )
		{
			lock ( m_lock_Access )
			{
				bool ret = true;
				Stopwatch stimer = new Stopwatch();
				int iDatalength = 0;
				this.m_strWriteData = "";
				this.m_strReceiveData = "";
				this.m_strRFID_ID = strData;
				String str_receive = "";

				//check
				if ( m_bConnected == false ) if ( this.Connect() == false ) return false;

				//Compile Write string
				iDatalength = strData.Length;
				this.m_strWriteData = "WR_" + iChannel.ToString("D2") + "_00009_" + iDatalength.ToString("D4") + "_" + strData;

				//send command
				if ( m_Comm.Send_Data(this.m_strWriteData + "\r\n") == false ) { ret = false; goto Finish; }
				//loop to seek end of receive data
				stimer.Start();
				while ( true )
				{
					if ( m_Comm.Receive_Data(ref str_receive) == false ) { ret = false; goto Finish; }
					m_strReceiveData += str_receive;
					if ( m_strReceiveData.Contains("\r") ) break;
					if ( stimer.ElapsedMilliseconds > 1000 )
					{
						m_iErrorID = ( int ) ErrorTable.en_ErrorCode.IFM_SENDDATA_TIMEOUT;
						m_strErrorMsg = "Additional Info: writeCmd" + m_strWriteData + ", return=" + m_strReceiveData;
						stimer.Stop();
						return false;
					}
				}
				stimer.Stop();

				if ( Decode_Response(4, strData) == false ) return false;

Finish:
				if ( ret == false )
				{
					m_Comm.Get_Error(ref this.m_iErrorID, ref this.m_strErrorMsg);
					return false;
				}
			}

			return true;
		}

		//Decode Receive Data/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/* iSelect:
			1 = decode response for setting of 1st parameters
			2 = decode response for setting of 2nd parameters
			3 = decode response for Get RFID, and extract the ID from the response
		*/
		public bool Decode_Response( int iSelect, String strCompare = "" )
		{
			switch ( iSelect )
			{
				case 1:
					if ( m_strReceiveData.Contains("CU_00_00_00_00_00_00_AS") == false )
					{
						this.m_iErrorID = ( int ) ErrorTable.en_ErrorCode.IFM_UNABLE_SET1ST_PARAM;
						return false;
					}
					break;

				case 2:
					{
						if ( m_strReceiveData.Contains("CI_00_00_00_0000_000_000_00_00_00") == true )
						{
							this.m_iErrorID = ( int ) ErrorTable.en_ErrorCode.IFM_UNABLE_SET2ND_PARAM;
							return false;
						}
					}
					break;

				case 3:
					int pos = m_strReceiveData.LastIndexOf("_");
					int iUIDCount = 0;

					if ( m_strReceiveData != "" )
					{
						iUIDCount = int.Parse(m_strReceiveData.Substring(pos - 2, 2));
						m_strRFID_ID = m_strReceiveData.Substring(pos + 1, iUIDCount);
						m_strRFID_ID = this.m_strRFID_ID.Replace("\r", "").Replace("\n", "").Replace("\t", "").Trim().Replace("\0", String.Empty);
					}

					break;

				case 4:
					if ( this.m_strWriteData.Contains(strCompare) == false )
					{
						m_iErrorID = ( int ) ErrorTable.en_ErrorCode.IFM_WRITE_ERROR;
						m_strErrorMsg = "Additional Info: writeCmd" + m_strWriteData + ", return=" + m_strReceiveData;
						return false;
					}
					break;
			}

			return true;
		}

		//Get Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public String IPAddress { set { m_Settings.m_strIPAddress = value; } get { return m_Settings.m_strIPAddress; } }
		public int PortNumber { set { m_Settings.m_iPortNum = value; } get { return m_Settings.m_iPortNum; } }
		public int CmdSendDelay { set { m_Settings.m_iCmdSendDelay = value; } get { return m_Settings.m_iCmdSendDelay; } }
		public int Data_ByteLength { set { m_Settings.m_iByteLength = value; } get { return m_Settings.m_iByteLength; } }

		public bool Channel1_Enable_b { set { m_Settings.m_bChannel1 = value; } get { return m_Settings.m_bChannel1; } }
		public bool Channel2_Enable_b { set { m_Settings.m_bChannel2 = value; } get { return m_Settings.m_bChannel2; } }
		public bool Channel3_Enable_b { set { m_Settings.m_bChannel3 = value; } get { return m_Settings.m_bChannel3; } }
		public bool Channel4_Enable_b { set { m_Settings.m_bChannel4 = value; } get { return m_Settings.m_bChannel4; } }

		public bool Enable { set { m_Settings.m_bEnable = value; } get { return m_Settings.m_bEnable; } }

		public String Get_Debug()
		{
			String str = "IFM RFID Reader:" + m_strDebug;
			m_strDebug = "";

			return str;
		}



		//Get Error////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID;
		}

		public string Get_ErrorMessage()
		{
			String strError = m_strErrorMsg;
			m_strErrorMsg = "";
			return strError;
		}
		public void Get_Error( ref int iErrorID, ref String strErrorMsgAdd )
		{
			iErrorID = m_iErrorID;
			strErrorMsgAdd = m_strErrorMsg;

			m_strErrorMsg = "";
			m_iErrorID = 0;
		}
	}

	//IFM RFID module settings 
	[Serializable]
	public class IFM_Settings
	{
		public bool m_bEnable = true;
		public String m_strIPAddress = "192.168.0.79";
		public int m_iPortNum = 33000;
		public int m_iCmdSendDelay = 10;
		public int m_iByteLength = 26;

		public bool m_bChannel1 = true;					//channel / RFID module attached to main controller
		public bool m_bChannel2 = false;
		public bool m_bChannel3 = false;
		public bool m_bChannel4 = false;

	}
}
