/****************************************************************************************************************************************
 * Serial Port class: 
 * Desc:    This class contains the calling methods for read write to serial ports.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.IO.Ports;
using System.Threading;
using System.Diagnostics;

namespace StaticCoreLib
{
    //working class
    public class Comm_Serial
    {
        //variables
        SerialPort m_SP;
        Comm_Serial_Settings m_CommSettings;
        Stopwatch m_timer = new Stopwatch();
        
        public bool m_bConnected = false;
        String m_strDebugger = "";
        String	m_strErrorMessage = "";
		int		m_iErrorID = 0;

        public byte[] m_byResponse = new byte[256];                                         //Response in bytes
        int m_iTotalResp = 8;                                                               //Total expected bytes to be read
        
        //constructor/////////////////////////////////////////////////////////////////////////////////////
        public Comm_Serial(Comm_Serial_Settings settings)
        {
            //m_SP = new SerialPort();
            m_CommSettings = new Comm_Serial_Settings();
        }

        //destuctor//////////////////////////////////////////////////////////////////////////////////////
        ~Comm_Serial()
        {
            Close_Port();
        }

        //Set Comm Settings///////////////////////////////////////////////////////////////////////////////
        public void Set_CommSettings(Comm_Serial_Settings settings) { m_CommSettings = settings; }
        //Get Comm Settings//////////////////////////////////////////////////////////////////////////////
        public Comm_Serial_Settings Get_CommSettings() { return m_CommSettings; }

        //Connect to the serial port////////////////////////////////////////////////////////////////////////
        public bool Open_Port()
        {
            //Check serial port is open
            if (m_bConnected == true) return true;

            //Set Serial port settings
            m_SP = new SerialPort(m_CommSettings.m_strPortName);
            //m_SP.PortName = m_CommSettings.m_strPortName;
            m_SP.DataBits = m_CommSettings.m_iDataBits;
            m_SP.BaudRate = m_CommSettings.m_iBaudRate;
            m_SP.Parity = m_CommSettings.m_parParity;
            m_SP.StopBits = m_CommSettings.m_sbStopBits;
            m_SP.ReadTimeout = m_CommSettings.m_iTimeOut_Read;
            m_SP.WriteTimeout = m_CommSettings.m_iTimeOut_Write;
            m_SP.NewLine = m_CommSettings.m_strTerminator;

            //COnnect to the serial port
            try
            {
				m_SP.Open();
				m_bConnected = true;
            }
            catch (Exception ex)
            {
                m_bConnected = false;
				m_iErrorID = (int) ErrorTable.en_ErrorCode.COMM_SP_CONNECT_ERR;
                m_strErrorMessage = "Open port error: " + ex.Message;
                return false;
            }
			
            return true;
        }

        //Disconnect from Serial Port////////////////////////////////////////////////////////////////////
        public void Close_Port()
        {
            if (m_bConnected)
            {
                try
                {
                    m_SP.DiscardInBuffer();
                    m_SP.DiscardOutBuffer();
                    m_SP.Close();
                    m_SP.Dispose();
                    m_SP = null;
                }
                catch(Exception ex)
                {
                }

                m_bConnected = false;
            }
        }

        //Check connection//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Is_Connected()
        {
            if (m_bConnected == true)
            {
                if (m_SP.IsOpen == true) return true;
            }

            return false;
        }

        //Read from Serial Port//////////////////////////////////////////////////////////////////////////
        public bool Read(ref String strData)
        {
            bool ret = true;
            String strResp = "";
            Stopwatch mSt = new Stopwatch();

            //Check if the serial port is connected
            if (m_bConnected == false)
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_SP_NOTCONNECTED;
                m_strErrorMessage = "Serial Port Read Error: Serial port is not connected.";
                return false;
            }

            //read from serial port
            try
            {
                //strData = m_SP.ReadLine();                  //requires new line to signal this function to exit. If not, the time out will set it to exit
                mSt.Start();
                while (true)
                {
                    //strResp += m_SP.ReadLine();
                    //strResp = m_SP.ReadLine();
                    strResp += m_SP.ReadExisting();
                    if (strResp.Contains(m_CommSettings.m_strTerminator) == true) break;


                    //time out
                    if (mSt.ElapsedMilliseconds > m_CommSettings.m_iTimeOut_Read)
                    {
                        m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_SP_READ_TIMEOUT;
                        m_strErrorMessage = "Read Error: Time Out Reached. Response=" + strResp;
                        ret = false;
                        break;
                    }
                }
                mSt.Stop();
                strData = strResp.Replace(m_CommSettings.m_strTerminator, "");
            }
            catch (Exception ex)
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_SP_READ_ERR;
                m_strErrorMessage = "Read Error: " + ex.Message + "\r\nRead:" + strData;
                return false;
            }

            return ret;
        }


        //Flush Port clean///////////////////////////////////////////////////////////////////////////////
        public void Flush_Port()
        {
            m_SP.DiscardInBuffer();
            m_SP.DiscardOutBuffer();
        }

        //Write to Serial port///////////////////////////////////////////////////////////////////////////
        public bool Write(String strData)
        {
            //Check if the serial port is connected
            if (m_bConnected == false)
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_NOTCONNECTED;
                m_strErrorMessage = "Serial Port Write Error: Serial port is not connected.";
                return false;
            }

            //Write to serial port
            try
            {
                //m_SP.Write(strData);
				m_SP.DiscardInBuffer();             //taught by JorFei
				m_SP.DiscardOutBuffer();
				m_SP.WriteLine(strData);
            }
            catch (Exception ex)
            {
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_SP_WRITE_ERR;
                m_strErrorMessage = "Write Byte Error: " + ex.Message;
                m_strDebugger = "Write Byte Error: " + ex.Message;
                return false;
            }

            return true;
        }

        //Read Byte from Serial Port///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Reading bytes from the serial port. Modbus RTU for Oriental motor does not have a terminator.
        /// </summary>
        public byte[] ReadBytes(ref int iReturnBytes, ref bool bResult, int TotalResp = 10)
        {
            byte[] byData = new byte[256];
            int iLength = 0, iCount = 0;
            Stopwatch st = new Stopwatch();

            //Reset or Set variables
            Array.Clear(m_byResponse, 0, m_byResponse.Length); m_byResponse = new byte[256];                                    //clear and set response byte array
            m_iTotalResp = TotalResp;                                                                                              //Set total number of expected read bytes
            bResult = true;                                                                                                                                                                    //Set return value as true first. Any error, reset this flag
            m_strErrorMessage = "";

            //Check if the serial port is connected
            if (m_bConnected == false)
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_NOTCONNECTED;
				m_strErrorMessage = "Serial Port: ReadBytes: Serial port is not connected.";
                bResult = false;
                return m_byResponse;
            }

            //start reading from serial port in a loop since the data comes not all at once
            st.Start();
            try
            {
                while (true)
                {
                    //check timeout
                    if (st.ElapsedMilliseconds > m_CommSettings.m_iTimeOut_Read)
					{
						m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_READ_TIMEOUT;
                        m_strErrorMessage = "Read Byte Error: Time out.";
                        bResult = false; break;
                    }

                    //read from serial port
                    if (m_SP.BytesToRead != 0)
                    {
                        iLength = m_SP.Read(byData, 0, 256);

                        //place into the byte debugging response
                        for (int i = 0; i < iLength; i++)
                        {
                            m_byResponse[iCount + i] = byData[i];
                        }
                        iCount += iLength;
                        iReturnBytes += iLength;
                    }

                    //For Modbus RTU: There is no char end. Using total number of response bytes
                    if (m_iTotalResp != 0 && iCount >= m_iTotalResp) break;
                }
            }
            catch (Exception ex)
            {
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_READ_ERR;
                m_strErrorMessage = "Read Byte Error: " + ex.Message;
                bResult = false;
            }
            st.Stop();

            return m_byResponse;
        }

		//Write Byte to Serial Port////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Write bytes to serial port. (For ModbusRTU only)
		/// </summary>
		/// <param name="byCmd">Command in bytes</param>
		/// <param name="iLen">length of the command</param>
		/// <returns></returns>
		public bool ModBusRTU_WriteBytes( byte[] byCmd, int iLen )
		{
			bool bRet = true;

			//write bytes to serial port
			try
			{
				m_SP.DiscardInBuffer();         //taught by JorFei
				m_SP.DiscardOutBuffer();
				m_SP.Write(byCmd, 0, iLen);
			}
			catch ( Exception ex )
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_WRITE_ERR;
				m_strErrorMessage = "Write Byte Error: " + ex.Message;
				m_strDebugger = "Write Byte Error: " + ex.Message;
				bRet = false;
			}

			return bRet;
		}

        //Write Byte to Serial Port////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Write bytes to serial port.
        /// </summary>
        /// <param name="byCmd">Command in bytes</param>
        /// <param name="iLen">length of the command</param>
        /// <returns></returns>
        public bool WriteBytes(byte[] byCmd, int iLen)
        {
            bool bRet = true;

            //Check if the serial port is connected
            if (m_bConnected == false)
            {
                m_strErrorMessage = "Serial Port Write Error: Serial port is not connected.";
                return false;
            }

            //write bytes to serial port
            try
            {
                m_SP.Write(byCmd, 0, iLen);
            }
            catch (Exception ex)
            {
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.COMM_SP_WRITE_ERR;
                m_strErrorMessage = "Write Byte Error: " + ex.Message;
                m_strDebugger = "Write Byte Error: " + ex.Message;
                bRet = false;
            }

            return bRet;
        }

        //Get Serial Comm Settings////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public string PortName { set { m_CommSettings.m_strPortName = value; } get { return m_CommSettings.m_strPortName; } }
        public int TimeOut_Read { set { m_CommSettings.m_iTimeOut_Read = value; } get { return m_CommSettings.m_iTimeOut_Read; } }
        public int TimeOut_Write { set { m_CommSettings.m_iTimeOut_Write = value; } get { return m_CommSettings.m_iTimeOut_Write; } }
        public int BaudRate { set { m_CommSettings.m_iBaudRate = value; } get { return m_CommSettings.m_iBaudRate; } }
        public int DataBits { set { m_CommSettings.m_iDataBits = value; } get { return m_CommSettings.m_iDataBits; } }
        public Parity ParityBit { set { m_CommSettings.m_parParity = value; } get { return m_CommSettings.m_parParity; } }
        public StopBits StopBit { set { m_CommSettings.m_sbStopBits = value; } get { return m_CommSettings.m_sbStopBits; } }
        public String Terminator { set { m_CommSettings.m_strTerminator = value; } get { return m_CommSettings.m_strTerminator; } }


        //for debugging purpose///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_Debug() { return m_strDebugger; }
		public String Get_ErrorMessage() { return m_strErrorMessage; }
		public int Get_ErrorID { get { return m_iErrorID; } }

		public void Get_Error(ref int iErrorID, ref String strErrorMsgAdd)
		{
			iErrorID = m_iErrorID;
			strErrorMsgAdd = m_strErrorMessage;
		}

    }

    //Setting class
    [Serializable]
    public class Comm_Serial_Settings
    {
        public string m_strPortName = "COM1";
        public int m_iTimeOut_Read = 8000;                                              //2 sec / 2000 msec
        public int m_iTimeOut_Write = 2000;                                              //2 sec / 2000 msec
        public int m_iBaudRate = 9600;
        public int m_iDataBits = 8;
        public Parity m_parParity = Parity.None;
        public StopBits m_sbStopBits = StopBits.One;
        [System.Xml.Serialization.XmlIgnore] public String m_strTerminator = "\r";                                                 //ReadLine function will block (for a certain time) until this new line is found
    }
}
