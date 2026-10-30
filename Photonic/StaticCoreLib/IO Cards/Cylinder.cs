/****************************************************************************************************************************************
 * Cylinder class: 
 * Desc:    This class is used to control the cylinder by sending command down to the output and input bit class interface.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Threading;
using System.Windows.Forms;

//For ServiceMessages
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace StaticCoreLib
{
    //data class
    [Serializable]
    public class Cylinder_Settings
    {
        public int m_iDelay_Before_Trigger_Work = 0;
        public int m_iDelay_Before_Trigger_Home = 0;
        public int m_iDelay_Sensor_Work_On = 0;
        public int m_iDelay_Sensor_Home_On = 0;
        public int m_iTimeout = 5000;
    }

    //working class
    public class Cylinder
    {
        //For ServiceMessages
        [DllImport("user32.dll")] public static extern int PeekMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax, int wRemoveMsg);
        [DllImport("user32.dll")] public static extern int GetMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg, int hwnd, int wMsgFilterMin, int wMsgFilterMax);
        [DllImport("user32.dll")] public static extern int TranslateMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);
        [DllImport("user32.dll")] public static extern int DispatchMessage([MarshalAs(UnmanagedType.Struct)] ref MSG lpMsg);

        //variables
        protected OutputBit m_output_Work = new OutputBit();
        protected OutputBit m_output_Home = new OutputBit();
        protected InputBit m_input_Work = new InputBit();
        protected InputBit m_input_Home = new InputBit();
        public Cylinder_Settings m_Settings;
        private MonitorInput m_pMonitorInput = null;

        int m_iOutput_Work = 0;

        string m_str_Label = "";
        string m_str_FilePath = "";
        string m_str_Work_Desciption = "";
        string m_str_Home_Desciption = "";

        int m_iError = 0;
        string m_str_ErrorMessage = "";

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        /// <summary>
        /// Class for Cylinder (Solenoid)
        /// </summary>
        /// <param name="pOPort_Work">Output Card Number for GoToWork</param>
        /// <param name="OutputBitID_Work">Output Bit ID for GoToWork</param>
        /// <param name="pOPort_Home">Output Card Number for BackToHome</param>
        /// <param name="OutputBitID_Home">Output Bit ID for BackToHome</param>
        /// <param name="pIPort_Work">Input Card Number for GoToWork</param>
        /// <param name="InputBitID_Work">Input Bit ID for GoToWork</param>
        /// <param name="pIPort_Home">Input Card Number for BackToHome</param>
        /// <param name="InputBitID_Home">Input Bit ID for BackToHome</param>
        /// <param name="strLabel">Cylinder Name (must be unique from any cylinder from any module, otherwise the cylinder settings will be shared among each other)</param>
        /// <param name="strFilePath">Cylinder settings file filepath</param>
        /// <param name="pMonitorInput"></param>
        /// <param name="strWork_Description">Cylinder action (eg. Up, Extend)</param>
        /// <param name="strHome_Description">Cylinder action (eg. Down, Retract)</param>
        public Cylinder(MyOPort pOPort_Work, int OutputBitID_Work,
                        MyOPort pOPort_Home, int OutputBitID_Home,
                        MyIPort pIPort_Work, int InputBitID_Work,
                        MyIPort pIPort_Home, int InputBitID_Home,
                        string strLabel, string strFilePath,
                        MonitorInput pMonitorInput,
                        string strWork_Description, string strHome_Description)
        {
            m_output_Work.Set_OutputPort(pOPort_Work);
            m_output_Work.m_iBitID = OutputBitID_Work;
            
            m_output_Home.Set_OutputPort(pOPort_Home);
            m_output_Home.m_iBitID = OutputBitID_Home;

            m_input_Work.Set_InputPort(pIPort_Work);
            m_input_Work.m_iBitID = InputBitID_Work;

            m_input_Home.Set_InputPort(pIPort_Home);
            m_input_Home.m_iBitID = InputBitID_Home;

            m_Settings = new Cylinder_Settings();

            Set_Label(strLabel, strFilePath, strWork_Description, strHome_Description);
            Set_MonitorInput(pMonitorInput);
        }

        //Set Pointer///////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_MonitorInput(MonitorInput pMonitorInput)
        {
            m_pMonitorInput = pMonitorInput;
        }

        //Get Input///////////////////////////////////////////////////////////////////////////////////////////////////////
        public InputBit Get_Work_Sensor() { return m_input_Work; }
        public InputBit Get_Home_Sensor() { return m_input_Home; }

        //Get Ouput////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public OutputBit Get_Work_Output() { return m_output_Work; }
        public OutputBit Get_Home_Output() { return m_output_Home; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set Parameters @ Set Labels
        /// <summary>
        /// Set label, settings file filepath, and read from the settings xml file
        /// </summary>
        public void Set_Label(String strLabel, String strFilePath, String str_Work_Desciption, String str_Home_Desciption)
        {
            //assign variables
            m_str_Work_Desciption = str_Work_Desciption;
            m_str_Home_Desciption = str_Home_Desciption;

            m_str_Label = strLabel;
            m_str_FilePath = strFilePath;

            if (m_str_FilePath.EndsWith("\\") == false) { m_str_FilePath += "\\"; }

            //load settings from xml file
            this.From_Registry();

            //Set Labels
            m_input_Work.Label = strLabel + " " + m_str_Work_Desciption;
            m_input_Home.Label = strLabel + " " + m_str_Home_Desciption;

            m_output_Work.Label = strLabel + " " + m_str_Work_Desciption;
            m_output_Home.Label = strLabel + " " + m_str_Home_Desciption;
        }

        //Get Labels and descriptions/////////////////////////////////////////////////////////////////////////////////
        public String Get_Label() { return m_str_Label; }
        public String Get_Work_Desciption() { return m_str_Work_Desciption; }
        public String Get_Home_Desciption() { return m_str_Home_Desciption; }

        //From Registry///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFilePathFull = m_str_FilePath + m_str_Label + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<Cylinder_Settings>(strFilePathFull);
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFilePathFull = m_str_FilePath + m_str_Label + ".xml";
            XMLSerializer.Serialize_Func<Cylinder_Settings>(m_Settings, strFilePathFull);
        }

        /*************************************************************************************************************************
        * Trigger Cylinder Functions
        *************************************************************************************************************************/

        /// <summary>
        /// Trigger cylinder only. Require separate WaitTaskComplete() function to wait for cylinder change state.
        /// </summary>
        /// <param name="bGoToWork">true = going to work zone (extend), false = going home (retract)</param>
        /// <returns></returns>
        public bool Cyl_Trigger(bool bGoToWork)
        {
            int iDelay = (bGoToWork == true) ? m_Settings.m_iDelay_Before_Trigger_Work : m_Settings.m_iDelay_Before_Trigger_Home;

            if (iDelay != 0) { Thread.Sleep(iDelay); }
            
            if (bGoToWork == true)
            {
                if (m_output_Work.Write(1) < 0) { return false; }
                if (m_output_Home.Write(0) < 0) { return false; }

                m_iOutput_Work = 1;
            }
            else
            {
                if (m_output_Work.Write(0) < 0) { return false; }
                if (m_output_Home.Write(1) < 0) { return false; }

                m_iOutput_Work = 0;
            }

            return true;
        }

        /// <summary>
        /// Trigger cylinder with WaitTaskComplete() function to wait for cylinder change state.
        /// <para>= return true, if cylinder has fully extended or retracted.</para>
        /// <para>= return false, if cylinder timeout.</para>
        /// </summary>
        /// <param name="bGoToWork">true = going to work zone (extend), false = going home (retract)</param>
        /// <returns></returns>
        public bool Cyl_Trigger_N_WaitTaskComplete(bool bGoToWork)
        {
            //trigger cylinder 
            if (Cyl_Trigger(bGoToWork) == false) { return false; }

            //wait task complete
            if (WaitTaskComplete() == false) { return false; }

            return true;
        }

        /*************************************************************************************************************************
         * Check Cylinder State Functions
         *************************************************************************************************************************/

        /// <summary>
        /// Check if the cylinder has been fully extended or retracted based on sensor location.
        /// <para>= return true, if cylinder has fully extended or retracted.</para>
        /// <para>= return false, if cylinder timeout.</para>
        /// </summary>
        /// <returns></returns>
        public bool WaitTaskComplete()
        {
            bool bRet = true;
            InputBit inputbit = (m_iOutput_Work == 1) ? m_input_Work : m_input_Home;
            int iTimeOut = m_Settings.m_iTimeout;
            
            //invalid allocated input bit
            if (inputbit.m_iBitID < 0) { m_iError = (int)ErrorTable.en_ErrorCode.CYL_INPUTBIT_ERR; return false; }

            //loop to check the sensor
            Stopwatch mSt = new Stopwatch();
            mSt.Start();
            while (true)
            {
                ServiceMessages();

                //check monitor input
                if (m_pMonitorInput != null && m_pMonitorInput.Flag_ErrorMonitor_b == true) { m_iError = m_pMonitorInput.Get_Error(); bRet = false; break; }

                //read from cylinder sensors
                if (Read_Cyl_Sensor() == true) { break; } //cylinder sensor detected

                //timeout return error
                if (mSt.ElapsedMilliseconds > iTimeOut) 
                {
                    bRet = false;
                    m_iError = (m_iOutput_Work == 1) ? (int)ErrorTable.en_ErrorCode.CYL_EXTEND_TIMEOUT : (int)ErrorTable.en_ErrorCode.CYL_RETRACT_TIMEOUT;
                    break;
                }

                Thread.Sleep(1);
            }
            mSt.Stop();

            return bRet;
        }

        /// <summary>
        /// Read cylinder sensor function with delay on capabilities. Delay off is useless because it is better to check for the other sensor's delay on state after trigger cylinder. 
        /// <para>= return false, if the current sensor state is not matched with desired state during the duration of the delay.</para>
        /// <para>= return true, if the current sensor state is matched with desired state for the duration of the delay.</para>
        /// </summary>
        /// <returns></returns>
        private bool Read_Cyl_Sensor()
        {
            int retval = 0;
            int iDesiredState = 1; //1 = ON, 0 = OFF, must be ON state to check cylinder sensor, unless the sensor is normally closed type
            InputBit inputbit = (m_iOutput_Work == 1) ? m_input_Work : m_input_Home; //get input sensor
            int iDelay = (m_iOutput_Work == 1) ? m_Settings.m_iDelay_Sensor_Work_On : m_Settings.m_iDelay_Sensor_Home_On; //get delay

            //read from sensor to get current state
            retval = inputbit.Read();

            //check whether current state match with desired state
            if (retval != iDesiredState) { return false; }

            //loop to check the sensor
            Stopwatch mSt = new Stopwatch();
            mSt.Start();
            while (mSt.ElapsedMilliseconds < iDelay)
            {
                //read from sensor again to check item presence
                retval = inputbit.Read();

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

        //Get Cylinder Status/////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get the cylinder status based on output and sensor. -1 = Error, 0 = Retracted (Home), 1 = Extend (Work)
        /// </summary>
        /// <returns></returns>
        public int Get_Cylinder_Status()
        {
            int iRet = 0;

            int iOutput_Work = m_output_Work.Read();
            int iInput_Work = m_input_Work.Read();

            int iOutput_Home = m_output_Home.Read();
            int iInput_Home = m_input_Home.Read();

            if ((iOutput_Work == 1 || iInput_Work == 1) && iOutput_Home == 0 && iInput_Home == 0) { iRet = 1; }
            else if (iOutput_Work == 0 && iInput_Work == 0 && (iOutput_Home == 1 || iInput_Home == 1)) { iRet = 0; }
            else if ((iOutput_Work == 1 || iInput_Work == 1) && (iOutput_Home == 1 || iInput_Home == 1)) { iRet = -1; }

            return iRet;
        }

        /*************************************************************************************************************************
         * Supporting Functions
         *************************************************************************************************************************/

        //Get Error Message//////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String strError = m_str_Label + ": " + m_str_ErrorMessage;
            m_str_ErrorMessage = "";
            return strError;
        }

        public int Get_ErrorID() 
        { 
            int iError = m_iError;
            m_iError = 0;
            return iError;
        }

        //Process Window Messages//////////////////////////////////////////////////////////////////////
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
    }
}