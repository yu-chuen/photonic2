/****************************************************************************************************************************************
 * TCP / Socket wrapper class: 
 * Desc:    This class contains the calling methods for read write to sockets / servers / listener.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Net;
using System.Net.Sockets;
using System.IO;

namespace StaticCoreLib
{
    public class Comm_TCP
    {
        //variable
        TcpClient m_tcpClient;
        Stream m_st;

        String m_strErrorMsg = "";
		int m_iErrorID = 0;

        //Flags
        private readonly object m_lock_Connected = new object();

        bool m_bConnected = false;

        private bool Connected_b
        {
            set { lock (m_lock_Connected) { m_bConnected = value; } }
            get { lock (m_lock_Connected) { return m_bConnected; } }
        }

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Comm_TCP()
        {

        }

        //destructor///////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Comm_TCP()
        {
            this.Disconnect();
        }

        //Connect/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Connect(String strIPAddress, int iPortNum)
        {
            bool ret = true;

            //return true if socket is connected
            if (Connected_b == true) return true;

            try
            {
                m_tcpClient = new TcpClient();
                m_tcpClient.ReceiveTimeout = 2000;
                m_tcpClient.SendTimeout = 2000;
                m_tcpClient.Connect(IPAddress.Parse(strIPAddress), iPortNum);

                m_st = m_tcpClient.GetStream();
                Connected_b = true;
            }
            catch (Exception ex)
            {
                Connected_b = false;
                m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_CONNECT_ERR;
                m_strErrorMsg = ex.ToString();
                ret = false;
            }

            return ret;
        }
        public bool Connect(String strIPAddress, int iPortNum, int iReceiveTimeout, int iSendTimeout)
        {
            bool ret = true;

            //return true if socket is connected
            if (Connected_b == true) return true;

            //Connect to reader
            try
            {
                m_tcpClient = new TcpClient();
                m_tcpClient.ReceiveTimeout = iReceiveTimeout;
                m_tcpClient.SendTimeout = iSendTimeout;
                m_tcpClient.Connect(IPAddress.Parse(strIPAddress), iPortNum);
                
                m_st = m_tcpClient.GetStream();
                
                Connected_b = true;
            }
            catch (Exception ex)
            {
                Connected_b = false;
                m_strErrorMsg = ex.ToString();
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_CONNECT_ERR;
                ret = false;
            }

            return ret;
        }

        //disconnect/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Disconnect()
        {
            if (Connected_b == true)
            {
                m_st.Close();
                m_tcpClient.Close();
                m_st.Dispose();
                Connected_b = false;
            }
        }

        //Check connection//////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Is_Connected()
        {
            if (Connected_b == true)
            {
                if (m_tcpClient.Connected == true) return true;
               
            }

            return false;
        }

        //send data to the server//////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Send_Data(String strData)
        {
            bool ret = true;
            byte[] bySendData = new byte[1024];

            //Check connection
            if (Connected_b == false)
            {
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_NOTCONNECTED;
                m_strErrorMsg = "Device is not Connected.";
                return false;
            }

            //Send command / data through the TCP Port
            try
            {
                bySendData = Encoding.ASCII.GetBytes(strData);
                m_st.Write(bySendData, 0, strData.Length);
            }
            catch (Exception ex)
            {
                m_strErrorMsg = ex.ToString();
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_WRITE_ERR;
                ret = false;
            }
            
            return ret;
        }

        //read data from the server//////////////////////////////////////////////////////////////////////////////////////////////
        public bool Receive_Data(ref String strData)
        {
            bool ret = true;
            byte[] byReceiveData = new byte[1024];

            //Check connection
            if (Connected_b == false)
            {
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_NOTCONNECTED;
                m_strErrorMsg = "Device is not Connected.";
                return false;
            }

            //Read from the TCP Port
            try
            {
                //this Read funciton is a blocking code. It will block until the read timeout is reach. Once the time out is reached, exception is thrown
                int retval = m_st.Read(byReceiveData, 0, byReceiveData.Length);
                byReceiveData[retval] = 0;

                strData = Encoding.ASCII.GetString(byReceiveData);
                strData = strData.Substring(0, retval);
            }
            catch (Exception ex)
            {
				//Error
				m_iErrorID = (int)ErrorTable.en_ErrorCode.COMM_TCP_READ_ERR;
                m_strErrorMsg = ex.ToString();
                ret = false;
            }

            return ret;
        }

        //Get Error////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMsg { get { return m_strErrorMsg; } }
		public int Get_ErrorID { get { return m_iErrorID; } }

        public void Get_Error(ref int iErrorID, ref String strErrorMsgAdd)
        {
            iErrorID = m_iErrorID;
            strErrorMsgAdd = m_strErrorMsg;
        }
    }

    //Common Comm TCP Settings
    [Serializable]
    public class Comm_TCP_Settings
    {
        public String m_strIPAddress = "192.168.100.100";
        public int m_iPortNum = 9004;
        public int m_iReceiveTimeout = 2000;
        public int m_iSendTimeout = 2000; 
        public long m_lgReadTimeout = 2000;
    }
}
