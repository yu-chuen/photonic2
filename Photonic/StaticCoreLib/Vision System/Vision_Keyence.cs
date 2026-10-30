/****************************************************************************************************************************************
 * Vision class:
 * Camera: Keyence
 * Vendor: Keyence
 * Desc: This class sends commands to the keyence application to trigger the camera and obtain the picture and data.
****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace StaticCoreLib
{
    //vision working class
    public class Vision_Keyence
    {
        //variables
        Vision_Keyence_Settings m_Settings = new Vision_Keyence_Settings();
        public Comm_TCP m_Comm = new Comm_TCP();

        public String m_strFilePath = "";
        public String m_strLabel = "Vision_Keyence";
        String m_strResponse = "";         //Full response from the TCP socket
        String m_strErrorMessage = "";
        String m_strDebug = "";
        int m_iErrorID = 0;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Note: Set file path and file name here for this class setting file. 
        /// </summary>
        public Vision_Keyence(String strLabel, String strFilePath)
        {
            m_strLabel = strLabel;
            m_strFilePath = strFilePath;
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";

            this.From_Registry();
        }

        //Destructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Vision_Keyence()
        {
            Disconnect();
        }

        //From registry//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Get Settings from xml file
        /// </summary>
        public void From_Registry()
        {
            String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<Vision_Keyence_Settings>(strFulFilePath);
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Save settings to xml file
        /// </summary>
        public void To_Registry()
        {
            String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
            XMLSerializer.Serialize_Func<Vision_Keyence_Settings>(m_Settings, strFulFilePath);
        }

        //Get and Set Vision Settings class//////////////////////////////////////////////////////////////////////////////////////////////
        public Vision_Keyence_Settings Get_VisionSettings() { return m_Settings; }
        public void Set_VisionSettings(Vision_Keyence_Settings mSettings) { m_Settings = mSettings; }

        public String Label { get { return m_strLabel; } }
        public bool Connected { get { return m_Comm.Is_Connected(); } }
        public String Response { set { m_strResponse = value; } get { return m_strResponse; } }
        public String Command { set; get; }

        //public String Recipe_Name { set { m_Settings.m_strRecipeName = value; } get { return m_Settings.m_strRecipeName; } }
        //public String Recipe_Directory { set { m_Settings.m_strRecipeDirectory = value; } get { return m_Settings.m_strRecipeDirectory; } }
        //public String Recipe_Selection { set { m_Settings.m_strRecipeSelection = value; } get { return m_Settings.m_strRecipeSelection; } }

        public String IPAddress { set { m_Settings.m_strIPAddress = value; } get { return m_Settings.m_strIPAddress; } }
        public int PortNumber { set { m_Settings.m_iPortNumber = value; } get { return m_Settings.m_iPortNumber; } }

        //Connect to Camera/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Connect()
        {
            if (!m_Comm.Connect(m_Settings.m_strIPAddress, m_Settings.m_iPortNumber))
            {
                m_strResponse = "" + m_Comm.Get_ErrorMsg;
                m_Comm.Get_Error(ref m_iErrorID, ref m_strErrorMessage);
                return false;
            }

            return true;
        }

        //Disconnect///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Disconnect() { m_Comm.Disconnect(); }



        //Commands///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Send_Command(String[] strArCmd)
        {
            String strCmdFull = "";
            int iArrLength = strArCmd.Count();
            int iCount = 0;

            //check string
            if (strArCmd[0] == "") { m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_KEYENCE_COMMAND_EMPTY; return false; }

            //connect to camera
            if (m_Comm.Is_Connected() == false) { if (this.Connect() == false) { return false; } }

            //construct the command - command, tracking_token, extra command...
            foreach (String str in strArCmd)
            {
                strCmdFull += str;

                //Check
                if (str == "" || str == null) break;
                else
                {
                    strCmdFull += ",";
                }

                iCount++;
            }
            strCmdFull = strCmdFull.Replace("\r", "").Replace("\n", "").Trim();
            if (strCmdFull.EndsWith(",")) { strCmdFull = strCmdFull.Remove(strCmdFull.LastIndexOf(","), 1); }
            strCmdFull += "\r\n";	//Terminator
            Command = strCmdFull;

            //Send Command
            if (m_Comm.Send_Data(strCmdFull) == false)
            {
                m_Comm.Get_Error(ref m_iErrorID, ref m_strErrorMessage);
                return false;
            }

            //debugging
            m_strDebug = "Command: " + strCmdFull;

            return true;
        }

        //Reply/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Read data from TCP port
        /// </summary>
        /// <param name="bRecipe">Recipe Loaded</param>
        /// <returns></returns>
        public bool Read_Data(bool bRecipe = false)
        {
            String strResponse = "", strError = "";
            int iTimeout = 0;
            m_strResponse = "";   //reset
            Stopwatch mSt = new Stopwatch();

            //Set Delay
            if (bRecipe == true) { iTimeout = m_Settings.m_iTimeout_LoadRecipe; }
            else { iTimeout = 5000; }

            //Read from TCP Port
            mSt.Start();
            while (true)
            {
                strResponse = "";

                if (m_Comm.Receive_Data(ref strResponse) == false)
                {
                    m_Comm.Get_Error(ref m_iErrorID, ref strError);
                }

                m_strResponse += strResponse;

                if (m_strResponse.Contains("\r")) { break; }

                //timeout
                if (mSt.ElapsedMilliseconds > iTimeout)
                {
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_KEYENCE_READ_TIMEOUT;
                    m_strErrorMessage = "Read Data Timeout: Response: " + m_strResponse + "\r\nAdditional Error:" + strError;
                    
                    mSt.Stop();

                    return false;
                }
            }
            mSt.Stop();

            //clean up the response
            m_strResponse = m_strResponse.Substring(0, m_strResponse.IndexOf("\r"));
            m_strResponse.Trim();

            //debugging
            m_strDebug = "Response: " + m_strResponse;

            return true;
        }

        //Get Data set////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Gets Data in the string array format and decodes it
        /// </summary>
        public String[] Get_Data(ref int iLength, ref bool bResults)
        {
            String[] strArData = new String[100];
            bResults = true;

            //Split the string
            strArData = m_strResponse.Split(',');
            iLength = strArData.Length;

            //Check if the camera returns error
            if (strArData[0].Trim().Contains("ER"))
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_KEYENCE_RETURN_ERROR;
                m_strErrorMessage = "Camera Error: " + strArData[0];
                bResults = false;
            }

            return strArData;
        }

        //Load Recipe////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Load the corresponding recipe. Use this class's properties to set the string input.
        /// </summary>
        /// <param name="strRecipe">Recipe name. Use this class property to set the string input</param>
        /// <returns></returns>
        public bool Load_Recipe(String strRecipe)
        {
            String[] strCmd = new String[10];
            int iLength = 0;
            bool bRet = true;

            //set parameters
            strCmd[0] = strRecipe;

            //send command
            if (Send_Command(strCmd) == false) { return false; }

            //read command
            if (Read_Data(true) == false) { return false; }

            //Decode data
            strCmd = this.Get_Data(ref iLength, ref bRet);

            //check error
            if (bRet == false) { return false; }

            return true;
        }

        //Get Error Message//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            String str = "Vision System (Keyence): " + m_strLabel + ": " + m_strErrorMessage;
            m_strErrorMessage = "";
            return str;
        }

        public String Get_Debug()
        {
            String str = "Debug: Vision System (Keyence): " + m_strLabel + ": " + m_strDebug;
            m_strDebug = "";
            return str;
        }
        public int Get_ErrorID() { return m_iErrorID; }

    }

    //vision settings class
    [Serializable]
    public class Vision_Keyence_Settings
    {
        //variables
        //public String m_strRecipeName = "";
        //public String m_strRecipeDirectory = "";
        //public String m_strRecipeSelection = "";

        public String m_strIPAddress = "192.168.0.10"; //default IP address
        public int m_iPortNumber = 8500; //default port number

        public int m_iTimeout_LoadRecipe = 10000; //ms
    }
}