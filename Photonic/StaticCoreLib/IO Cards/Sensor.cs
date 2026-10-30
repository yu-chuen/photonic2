/****************************************************************************************************************************************
 * Sensor class: 
 * Desc:    This class is used for detection of the sensor bits. 
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace StaticCoreLib
{
    //data class
    [Serializable]
    public class Sensor_Settings
    {
        public int m_iDelay_On = 0;
        public int m_iDelay_Off = 0;
    }

    //working class
    public class Sensor : InputBit
    {
        //DLL imports
        [DllImport("user32.dll")] public static extern int PeekMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg);
        [DllImport("user32.dll")] public static extern int GetMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")] public static extern int TranslateMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);
        [DllImport("user32.dll")] public static extern int DispatchMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);

        //variables
        public Sensor_Settings m_Settings;
        string m_str_Label = "";
        string m_str_FilePath = "";

        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Sensor(MyIPort pIPort, int iBitID, String strLabel, String strFilePath) : base(pIPort, iBitID, "")
        {
            m_Settings = new Sensor_Settings();
            Set_Label(strLabel, strFilePath);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set Parameters @ Set Labels
        /// <summary>
        /// Set label, setting file filepath, and read from the setting xml file
        /// </summary>
        private void Set_Label(String strLabel, String strFilePath)
        {
            //assign variables
            m_str_Label = strLabel;
            m_str_FilePath = strFilePath;

            if (m_str_FilePath.EndsWith("\\") == false) { m_str_FilePath += "\\"; }

            //load settings from xml file
            this.From_Registry();

            //to base
            base.Label = m_str_Label;
        }

        //Get Labels and descriptions/////////////////////////////////////////////////////////////////////////////////
        public String Get_Label() { return m_str_Label; }

        //From Registry///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFilePathFull = m_str_FilePath + m_str_Label + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<Sensor_Settings>(strFilePathFull);
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFilePathFull = m_str_FilePath + m_str_Label + ".xml";
            XMLSerializer.Serialize_Func<Sensor_Settings>(m_Settings, strFilePathFull);
        }

        /// <summary>
        /// Main read sensor function with delay on and delay off capabilities.
        /// </summary>
        /// <param name="iDesiredState">The required state which is needed to proceed to the next sequence, 0 = OFF, 1 = ON</param>
        /// <returns>If the current sensor state is not matched with desired state during the duration of the delay, return false, otherwise return true</returns>
        public bool Read_Sensor(int iDesiredState)
        {
            int retval = 0;
            int iDelay = (iDesiredState == 1) ? m_Settings.m_iDelay_On : m_Settings.m_iDelay_Off;

            //read from sensor to get current state
            retval = Read();

            //check whether current state match with desired state
            if (retval != iDesiredState) { return false; }

            //loop to check the sensor
            Stopwatch mSt = new Stopwatch();
            mSt.Start();
            while (mSt.ElapsedMilliseconds < iDelay)
            {
                //read from sensor again to check item presence
                retval = Read();

                //Error: Break;
                if (retval == -1)
                {
                    mSt.Stop();
                    return false;
                }

                //break loop if current state not match with desired state
                if (retval != iDesiredState)
                {
                    mSt.Stop();
                    return false;
                }

                Thread.Sleep(1);
            }
            mSt.Stop();

            return true;
        }

        //Delay/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// User programmable delay to delay next sequence line when the sensors has been checked.
        /// </summary>
        private void Delay(short mOutput)
        {
            Stopwatch st = new Stopwatch();
            int iDelay = (mOutput != 0) ? m_Settings.m_iDelay_On : m_Settings.m_iDelay_Off;
            if (iDelay == 0) return;

            st.Start();
            while (true)
            {
                Thread.Sleep(1);
                ServiceMessages();
                if (st.ElapsedMilliseconds > iDelay)
                    break;
            }
            st.Stop();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Process Window Messages
        private void ServiceMessages()
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
                {
                    stateQuit = true;
                }

                if (stateQuit)
                {
                    break;
                }
            }
        }
    }
}