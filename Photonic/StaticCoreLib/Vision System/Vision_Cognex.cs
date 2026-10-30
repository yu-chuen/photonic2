/****************************************************************************************************************************************
 * Vision class:
 * Camera: Cognex
 * Vendor: Unique, contact JorFei
 * Desc: This class sends commands to the vision pro application to trigger the camera and obtain the picture and data.
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
    public class Vision_Cognex
    {
        //variables
        Vision_Settings m_Settings = new Vision_Settings();
        public Comm_TCP m_Comm = new Comm_TCP();

        public String m_strFilePath = "";
        public String m_strLabel = "Vision_Cognex";
        String m_strResponse = "";                                                  //Full response from the TCP socket
        String m_strErrorMessage = "";
        String m_strDebug = "";
		int m_iErrorID = 0;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Note: Set file path and file name here for this class setting file. 
        /// </summary>
        public Vision_Cognex(String strLabel, String strFilePath)
        {
            m_strLabel = strLabel;
            m_strFilePath = strFilePath;
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";

            this.From_Registry();
        }

        //Destructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Vision_Cognex()
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
            m_Settings = XMLSerializer.Deserialize_Func<Vision_Settings>(strFulFilePath);
        }
        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Save settings to xml file
        /// </summary>
        public void     To_Registry()
        {
            if (m_Settings.m_strImgFilePath.EndsWith("\\") == false) m_Settings.m_strImgFilePath += "\\";
            if (m_Settings.m_strImgFileName.EndsWith(".bmp") == false) m_Settings.m_strImgFileName += ".bmp";
            
            String strFulFilePath = m_strFilePath + m_strLabel +".xml";
            XMLSerializer.Serialize_Func<Vision_Settings>(m_Settings, strFulFilePath);
        }




        //Get and Set Vision Settings class//////////////////////////////////////////////////////////////////////////////////////////////
        public Vision_Settings Get_VisionSettings() { return m_Settings; }
        public void Set_VisionSettings(Vision_Settings mSettings) { m_Settings = mSettings; }

        //Set and Get Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String ImageFilePath
        {
            set
            {
                m_Settings.m_strImgFilePath = value;
                if (m_Settings.m_strImgFilePath.EndsWith("\\") == false) m_Settings.m_strImgFilePath += "\\";           //check for back slash
            }
            get { return m_Settings.m_strImgFilePath; }
        }
        public String Label { get { return m_strLabel; } }
        public String ImageFileName { set { m_Settings.m_strImgFileName = value; } get { return m_Settings.m_strImgFileName; } }
        public String IPAddress { set { m_Settings.m_strIPAddress = value; } get { return m_Settings.m_strIPAddress; } }
        public int PortNumber { set { m_Settings.m_iPortNumber = value; } get { return m_Settings.m_iPortNumber; } }
        public bool Connected { get { return m_Comm.Is_Connected(); } }

        public String ImageFile_FullPath { get { return m_Settings.m_strImgFilePath + m_Settings.m_strImgFileName; } }              //get full file path
        public String Event_Name { get { return m_Settings.m_strEventName; } }
        public String Mutex_Name { get { return m_Settings.m_strMutexName; } }

        public double Calib_OffsetX { set { m_Settings.m_dbCalib_OffsetX = value; } get { return m_Settings.m_dbCalib_OffsetX; } }
        public double Calib_OffsetY { set { m_Settings.m_dbCalib_OffsetY = value; } get { return m_Settings.m_dbCalib_OffsetY; } }
        public double Calib_OffsetZ { set { m_Settings.m_dbCalib_OffsetZ = value; } get { return m_Settings.m_dbCalib_OffsetZ; } }
        public double Calib_OffsetR { set { m_Settings.m_dbCalib_OffsetR = value; } get { return m_Settings.m_dbCalib_OffsetR; } }
        public double Calib_OffsetExtend { set { m_Settings.m_dbCalib_OffsetExtend = value; } get { return m_Settings.m_dbCalib_OffsetExtend; } }

        public int Delay_RunTime { set { m_Settings.m_iDelay_Run = value; } get { return m_Settings.m_iDelay_Run; } }
        public int Delay_Calib { set { m_Settings.m_iDelay_Calib = value; } get { return m_Settings.m_iDelay_Calib; } }
        public bool Enable { set { m_Settings.m_bEnable = value; } get { return m_Settings.m_bEnable; } }

        public String Recipe_Calib { set { m_Settings.m_strCalibName = value; } get { return m_Settings.m_strCalibName; } }
        public String Recipe_Repeat { set { m_Settings.m_strRepeatName = value; } get { return m_Settings.m_strRepeatName; } }
        public String Recipe_Run { set { m_Settings.m_strRecipeName = value; } get { return m_Settings.m_strRecipeName; } }

        public String Response { set { m_strResponse = value; } get { return m_strResponse; } }
        public String Command { set; get; }





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

            //Check enable
            if (Enable == false) return true;

            //connect
            if (Connect() == false) return false;

            //check string
            if (strArCmd[0] == "") { m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_UNIQUE_COMMANDEMPTY; return false; }
            if (strArCmd[1] == "") { m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_UNIQUE_TOKENEMPTY; return false; }


            //Construct the command - command, tracking_token, ExtraCommand		
            foreach (String str in strArCmd)
            {
                if (str == "" || str == null) break;
                else
                    strCmdFull += str + ",";
            }
            if (strCmdFull.EndsWith(",") == true) strCmdFull = strCmdFull.Remove(strCmdFull.LastIndexOf(","), 1);
            strCmdFull += "\r\n";												//Terminator
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
            int iDelay = 0;
            m_strResponse = "";                 //reset
            Stopwatch mSt = new Stopwatch();

            //Check enable
            if (Enable == false) return true;

            //connect
            if (Connect() == false) return false;
            
            //Set Delay
            if (bRecipe == true) iDelay = m_Settings.m_iDelay_LoadRecipe;
            else iDelay = 2000;

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
                if (m_strResponse.Contains("\r")) break;

                //Time out
                if (mSt.ElapsedMilliseconds > iDelay)
                { 
					m_iErrorID = (int) ErrorTable.en_ErrorCode.VISION_UNIQUE_READTIMEOUT;
                    m_strErrorMessage = "Read Data Time Out. \r\n" + strError; 
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
            String[] strArData = new String[20];
            bResults = true;

            //Split the string
            strArData = m_strResponse.Split(',');
            iLength = strArData.Length;

            //Check if the camera returns error
            if (strArData[2].Trim().Contains("NG"))
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.VISION_UNIQUE_RETURNERROR;
                m_strErrorMessage = "Camera Error: " + strArData[3].Replace('\'', '\"');

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
            String[] strDataDummy = new String[10];
            int iLength = 0;
            bool bRet = true;

            //Check enable
            if (Enable == false) { return true; }

            //set parameters
            strDataDummy[0] = "LOAD_RECIPE";
            strDataDummy[1] = "Load_Recipe";
            strDataDummy[2] = strRecipe;

            //send command
            if (!Send_Command(strDataDummy)) return false;

            //read command
            if (!Read_Data(true)) return false;

            //Decode data
            strDataDummy = this.Get_Data(ref iLength, ref bRet);

            //check for error
            if (bRet == false) return false;

            return true;
        }

        //Get Error Message//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage() 
        {
            String str = "Vision: " + m_strLabel + ": " + m_strErrorMessage;
            m_strErrorMessage = "";
            return str; 
        }
        public String Get_Debug() 
        {
            String str = "xxxx Vision Cam " + m_strLabel + ": " + m_strDebug;
            m_strDebug = "";
            return str; 
        }
		public int Get_ErrorID() { return m_iErrorID; }

    }


    //vision settings class
    [Serializable]
    public class Vision_Settings
    {
        //variables
        public String m_strImgFilePath = "C:\\";
        public String m_strImgFileName = "Img_Cognex.bmp";
        public String m_strIPAddress = "127.0.0.1";
        public String m_strRecipeName = "Generic";
        public String m_strCalibName = "calib";
        public String m_strRepeatName = "repeat";
        public String m_strEventName = "001";                                          //Event name. Define by vision system to trigger for main ui picture box
        public String m_strMutexName = "00a";                                          //Mutex name. Define by vision system to block from reading from file (picture) which will be used by the picturebox
        public int m_iPortNumber = 10000;

        public double m_dbCalib_OffsetR = 0;
        public double m_dbCalib_OffsetX = 0;
        public double m_dbCalib_OffsetY = 0;
        public double m_dbCalib_OffsetZ = 0;
        public double m_dbCalib_OffsetExtend = 0;

        public int m_iDelay_Calib = 1000;                         //ms
        public int m_iDelay_Run = 300;                              //ms
        public int m_iDelay_LoadRecipe = 60000;            //ms

        public bool m_bEnable = true;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Vision_Settings()
        {
        }
    }



	/********************************************************************************************************************************
	 * Vision command class: Contains all the vision commands
	 * //Note Please add this into the view model list as well
	 ********************************************************************************************************************************/
	//Vision Command Listing
	public static class Vision_Command
	{
		public const String LOAD_RECIPE = "LOAD_RECIPE";
		public const String SAVE_RECIPE = "SAVE_RECIPE";

		public const String INSPECT_PART_FRONT = "INSPECT_PART_FRONT";
		public const String INSPECT_PART_REAR = "INSPECT_PART_REAR";
	}

}
