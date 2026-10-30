/****************************************************************************************************************************************
 * Output Bit class: 
 * Desc:    Functional class for Output bit only such as tower light and single bit trigger
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace StaticCoreLib
{
    public class OutputBit
    {
        [DllImport("user32.dll")] public static extern int PeekMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg);
        [DllImport("user32.dll")] public static extern int GetMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")] public static extern int TranslateMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);
        [DllImport("user32.dll")] public static extern int DispatchMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);
        
        //variables
        protected MyOPort m_OPort;
        public int m_iBitID;

        public String Label { get; set; }
        String m_strErrorMessage = "";

        //////////////////////////////////////////////////////////////////////
        //Constructor
        public OutputBit()
        {
            m_iBitID = 0;
            m_OPort = null;
        }

        public OutputBit(MyOPort pOPort, int iBitID, String strLabel)
        {
            m_OPort = pOPort;
            m_iBitID = iBitID;
            Label = strLabel;
        }

        //////////////////////////////////////////////////////////////////////
        //Set MyOPort class
        public void Set_OutputPort(MyOPort pOPort)
        {
            m_OPort = pOPort;
        }

        //////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Trigger output bit. Using global variable.
        /// </summary>
        /// <param name="iValue"></param>
        /// <returns></returns>
        public int Write(int iValue)
        {
            if (m_iBitID < 0)
            {
                //MessageBox.Show("BitID is less than 0");
                return -1;
            }

            while (true)
            {
                this.ServiceMessages();
                bool retval = m_OPort.WriteBit(m_iBitID, iValue);
                if (retval == false) break;

                if (retval != false)
                {
                    if (this.Read() == iValue)
                    {
                        return iValue;
                    }
                }
            }

            return -1;
        }

        /////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Trigger output bit. Using function parameters. Intended for cylinder class use.
        /// </summary>
        public int Write(int iValue, int iBitID)
        {
            if (m_iBitID < 0)
            {
                //MessageBox.Show("BitID is less than 0");
                m_strErrorMessage = "BitID is less than 0";
                return -1;
            }

            while (true)
            {
                this.ServiceMessages();
                bool retval = m_OPort.WriteBit(iBitID, iValue);
                if (retval == false) break;

                if (retval != false)
                {
                    if (this.Read() == iValue)
                    {
                        return iValue;
                    }
                }
            }

            m_strErrorMessage = "Write to output bit failed.";
            return -1;
        }

        ////////////////////////////////////////////////////////////////////////////////
        //read output port
        public int Read()
        {
            int iTempValue = 0;

            if (m_OPort == null)
            {
                MessageBox.Show("Output Port is null");
                m_strErrorMessage = "Output Port is null.";
                iTempValue = -1;
            }
            if (!m_OPort.Read(m_iBitID, ref iTempValue))
            {
                m_strErrorMessage = "Read from Output bit failed.";
                iTempValue = -1;
            }

            return iTempValue;
        }

        //////////////////////////////////////////////////////////////////////
        public int Toggle()
        {
            int tempState;

            tempState = Read();
            switch (tempState)
            {
                case 0:
                    if (Write(1) == -1) return -1;
                    return 1;

                case 1:
                    if (Write(0) == -1) return -1;
                    return 0;

                default:
                    return -1;
            }
        }

        //////////////////////////////////////////////////////////////////////
        //Process Windows Messages. 
        public void ServiceMessages()
        {
            MSG msg = new MSG();
            bool stateQuit = false;
            while (PeekMessage(ref msg, 0, 0, 0, 0) != 0)
            {
                if (GetMessage(ref msg, 0, 0, 0) != 0)
                {
                    TranslateMessage(ref msg);
                    DispatchMessage(ref msg);
                }
                else
                    stateQuit = true;

                if (stateQuit)
                    break;
            }
        }

        //Error message/////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = Label + ": " + m_strErrorMessage;
            m_strErrorMessage = "";

            return strError;
        }

        //Get Slave ID///////////////////////////////////////////
        public int Get_SlaveID() { return m_OPort.Get_SlaveID(); }
    }
}
