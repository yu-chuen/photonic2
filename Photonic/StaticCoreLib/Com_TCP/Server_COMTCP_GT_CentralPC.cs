using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class Server_COMTCP_GT_CentralPC
    {
        //variables
        public Comm_TCP m_Com;
        public Server_COMTCP_GT_CentralPC_Settings m_Settings;

        public String m_strLabel = "";
        public String m_strFilePath = "";
        public String m_strDebug_Cmd = "";
        public String m_strDebug_Response = "";

        int m_iErrorID = 0;
        String m_strErrorMessageAdd = "";

        //Constructor////////////////////////////////////////////////////////////////////////////
        public Server_COMTCP_GT_CentralPC(Comm_TCP mCom, String strLabel, String strFilePath)
        {
            m_Com = mCom;
            m_strLabel = strLabel;
            m_strFilePath = strFilePath;

            From_Registry();
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFullFilePath = m_strFilePath + m_strLabel + ".xml";
            XMLSerializer.Serialize_Func<Server_COMTCP_GT_CentralPC_Settings>(m_Settings, strFullFilePath);
        }

        //From Registry///////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFullFilePath = m_strFilePath + m_strLabel + ".xml";
            m_Settings = XMLSerializer.Deserialize_Func<Server_COMTCP_GT_CentralPC_Settings>(strFullFilePath);
        }

        //Set Setting for Clones////////////////////////////////////////////////////////////
        public void Set_Settings(Server_COMTCP_GT_CentralPC_Settings mSettings) { m_Settings = mSettings; }


        //Core: Send Command///////////////////////////////////////////////////////////
        public bool SendCommand(String strCmd, ref String strResponse)
        {
            String strReply = "";

            //check
            if (m_Settings.m_bEnable == false) { strResponse = "ok"; return true; }

            //disconnect first if there are any existing connection
            m_Com.Disconnect();

            //connect
            if (m_Com.Connect(m_Settings.m_Settings.m_strIPAddress,
                                m_Settings.m_Settings.m_iPortNum,
                                m_Settings.m_Settings.m_iReceiveTimeout,
                                m_Settings.m_Settings.m_iSendTimeout
                              ) == false)
            {
                Set_Error(m_Com.Get_ErrorID, m_Com.Get_ErrorMsg);
                return false;
            }

            //Send command
            if (m_Com.Send_Data(strCmd) == false)
            {
                Set_Error(m_Com.Get_ErrorID, m_Com.Get_ErrorMsg);
                return false;
            }

            //Read response
            if (m_Com.Receive_Data(ref strReply) == false)
            {
                Set_Error(m_Com.Get_ErrorID, m_Com.Get_ErrorMsg);
                return false;
            }

            //disconnect
            m_Com.Disconnect();

            //set to response
            strResponse = strReply;

            //alter debug log
            if (strCmd.Contains("useraccess"))
            {
                strCmd = strCmd.Remove(strCmd.LastIndexOf(',')) + ",****";
            }

            //set to debugger
            m_strDebug_Cmd = strCmd;
            m_strDebug_Response = strResponse;

            return true;
        }

        //core: Decode response////////////////////////////////////////////////////////////////////////////////
        public bool Decode_Response(String strResponse)
        {
            String[] strArData;
            strArData = strResponse.Split(',');

            if (strArData[0].ToLower().Contains("fail") == true)
            {
                //set error
                Set_Error((int)ErrorTable.en_ErrorCode.SERVER_RETURN_ERR, "Response: " + strArData[1]);
                return false;
            }

            return true;
        }




        /*****************************************************************************************************
         * Proprietry Command: String compilation
        *****************************************************************************************************/
        //Update Station Work Done/////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Set Station Done for Part Serial Number
        /// </summary>
        /// <param name="strStationID"></param>
        /// <param name="strSerialNumber"></param>
        /// <param name="iDatabase">
        /// <para>Database Number must match with GT Central PC Software</para>
        /// <para>1 = X597Database</para>
        /// <para>2 = X597-MES</para>
        /// <para>3 = X597-Grommet</para>
        /// </param>
        /// <returns></returns>
        public bool Cmd_StationPalletDone(string strStationID, string strSerialNumber, int iDatabase = 1)
        {
            string strCmd = "";
            string strResponse = "";

            //compile string
            strCmd = "UpdateStationDone," + strSerialNumber + "," + strStationID + ",1" + "," + iDatabase;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update Station For Rework///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateStationReg(String strSerialNumber, String strResult)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * 'UpdateStationReg'                           > function identifier to be used in Server before updating to database
             * strSerialNumber                              > get current serial number
             * 'reset_stationreg' || 'overwrite_stationreg' > the table column identifier to be updated into database
             * strStationName                               > get the current station name
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateStationReg," + strSerialNumber + "," + strResult;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update Results///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateResults(String strSerialNumber, String strResult)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * Results:   Item									Cmd @ index 2 > send direct SQL string
             *				> Solder point done			> SolderPoint + Number = 'Results'
             *				> Vision result					> VisionResult + Type = 'Results'
             *				> Main result P/F				> MainResult = 'P/F'
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateResult," + strSerialNumber + "," + strResult;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update Input///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateInput(String strSerialNumber, String strStationName, String strDateTime)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * 'UpdateInput'  > function identifier to be used in Server before updating to database
             * strSerialNumber > get current serial number
             * 'output'        > the table column identifier to be updated into database
             * strStationName  > get the current station name
             * strDateTime     > gets the current date and time
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateInput," + strSerialNumber + ",input," + strStationName + "," + strDateTime;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update Output///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateOutput(String strSerialNumber, String strStationName, int iPassReject, String strDateTime)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * 'UpdateOutput'  > function identifier to be used in Server before updating to database
             * strSerialNumber > get current serial number
             * 'output'        > the table column identifier to be updated into database
             * strStationName  > get the current station name
             * iPassReject     > only accept 0 or 1			
             * strDateTime     > gets the current date and time
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateOutput," + strSerialNumber + ",output," + strStationName + "," + iPassReject + "," + strDateTime;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update ProcessTime///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateProcessTime(String strSerialNumber, String strStationName, double dbProcessTime)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * 'UpdateProcessTime'  > function identifier to be used in Server before updating to database
             * strSerialNumber      > get current serial number
             * 'processtime'        > the table column identifier to be updated into database
             * strStationName       > get the current station name
             * dbProcessTime        > get process time from station
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateProcessTime," + strSerialNumber + ",processtime," + strStationName + "," + dbProcessTime;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Update CycleTime///////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateCycleTime(String strSerialNumber, String strStationName, double dbCycleTime, String strDateTime)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * 'UpdateCycleTime'  > function identifier to be used in Server before updating to database
             * strSerialNumber    > get current serial number
             * 'cycletime'        > the table column identifier to be updated into database
             * strStationName     > get the current station name
             * dbCycleTime        > get cycle time from station
             * strDateTime        > gets the current date and time
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateCycleTime," + strSerialNumber + ",cycletime," + strStationName + "," + dbCycleTime + "," + strDateTime;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Get Current Pkg File/////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetCurrentPkg(String strZoneID, ref String strPkgData)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "currentpkg" + "," + strZoneID;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            //Set output
            strPkgData = strResponse;

            return true;
        }

        //Send Current Pkg Settings to Server///////////////////////////////////////////////////////////
        public bool Cmd_UpdatePkgSettings(String strZoneID, String strData)
        {
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "Setpkg" + "," + strZoneID + "," + strData;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return true;
        }

        //Skip Check: Get Go or No Go//////////////////////////////////////////////////////////////////////
        public bool Cmd_SkipCheck(String strSerialNumber, String strCurrentOP, ref bool bSkipCheck)
        {
            String strCmd = "";
            String strResponse = "";
            bSkipCheck = false;

            //compile string
            /*------------------------------------------------------------------------------------
             * cmd: > [cmd] , [sn] , [current Station]
             -------------------------------------------------------------------------------------*/
            strCmd = "skipcheck" + "," + strSerialNumber + "," + strCurrentOP;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            //Get Go or No Go
            bSkipCheck = (strResponse.Contains("nogo") == true) ? false : true;

            return true;
        }

        //Skip Check: Get Go or No Go//////////////////////////////////////////////////////////////////////
        //for pilot line
        public bool Cmd_SkipCheck_Multi(string strSerialNumber, string strCurrentOP, ref bool bSkipCheck)
        {
            string strCmd = "";
            string strResponse = "";
            bSkipCheck = false;

            //compile string
            /*------------------------------------------------------------------------------------
             * cmd: > [cmd] , [sn] , [current Station]
             -------------------------------------------------------------------------------------*/
            strCmd = "skipcheck_multi" + "," + strSerialNumber + "," + strCurrentOP;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            //Get Go or No Go
            bSkipCheck = (strResponse.Contains("nogo") == true) ? false : true;

            return true;
        }

        //Insert new part///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_InsertNewPart(String strSerialNumber)
        {
            bool bRet = true;
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "insertpart," + strSerialNumber;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Insert Error///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_InsertErrorLog(String strSerialNumber, String strDateTime, int iPC, int iErrorCode, String strErrorName, String strErrorDesc)
        {
            bool bRet = true;
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "inserterrorlog," + strSerialNumber + "," + strDateTime + "," + iPC.ToString() + "," + iErrorCode.ToString() + "," + strErrorName + ",;" + strErrorDesc;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return bRet;
        }

        //Insert Station DownTime///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_InsertStationDownTime(String strStation, String strDown_DateTime, String strStart_DateTime, double dbTotal_DownTime_seconds, String strDescription)
        {
            bool bRet = true;
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "insertstationdowntime," + strStation + "," + strDown_DateTime + "," + strStart_DateTime + "," + dbTotal_DownTime_seconds.ToString() + ",;" + strDescription;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return bRet;
        }

        //Check Duplicate///////////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_CheckDuplicate(String strSerialNumber)
        {
            bool bRet = true;
            String strCmd = "";
            String strResponse = "";

            //compile string
            strCmd = "CheckDuplicate," + strSerialNumber;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Check Exist///////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Check database for data existence
        /// </summary>
        /// <param name="strSerialNumber">Part Serial Number</param>
        /// <param name="iDatabase">
        /// <para>Database Number must match with GT Central PC Software</para>
        /// <para>1 = X597Database</para>
        /// <para>2 = X597-MES</para>
        /// <para>3 = X597-Grommet</para>
        /// </param>
        /// <param name="iTable">
        /// <para>Table Number must match with GT Central PC Software</para>
        /// <para>1 = ProcessData</para>
        /// <para>2 = MES Data</para>
        /// <para>3 = MES GrommetInput</para>
        /// </param>
        /// <returns></returns>
        public bool Cmd_CheckExist(string strSerialNumber, int iDatabase = 1, int iTable = 1)
        {
            bool bRet = true;
            string strCmd = "";
            string strResponse = "";

            //compile string
            strCmd = "CheckExist," + strSerialNumber + "," + iDatabase + "," + iTable;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return bRet;
        }

        //Get user access////////////////////////////////////////////////////////////////////////////////////////////
        //note: 1=>ok , 0=>not valid , -1=>TCP error
        public int Cmd_GetUserAccess(String strUserName, String strPassword, ref String strResponse)
        {
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "useraccess" + "," + strUserName + "," + strPassword;

            //if server disabled, return error
            if (m_Settings.m_bEnable == false) return -1;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return -1;
            if (Decode_Response(strResponse) == false) return 0;

            return 1;
        }

        //get data from server's local memory////////////////////////////////////////////
        //FOR AUTO LINE (SOP) OP40 PNP DURING SERVER INSERT PART USED ONLY
        //to get existing part's work order number, use GetProcessDataFromServer
        public bool Cmd_GetCurrentWorkOrderNumber(ref string strResponse)
        {
            bool bRet = true;
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "Get_CurrentWorkOrderNumber";

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return bRet;
        }

        //Get Data From Local MES Database///////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetDataFromLocalMESDatabase(String strSerialNum, String strCmdType, ref String strResponse)
        {
            bool bRet = true;
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "GetDataFromLocalMESDatabase" + "," + strSerialNum + "," + strCmdType;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Get ProcessData From Server///////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetProcessDataFromServer(String strSerialNum, String strCmdType, ref String strResponse)
        {
            bool bRet = true;
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "GetProcessDataFromServer" + "," + strSerialNum + "," + strCmdType;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Get SolderPoint ONLY///////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetSolderPoint(string strSerialNum, string strCmdType, ref string strResponse)
        {
            bool bRet = true;
            string strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "GetSolderPoint" + "," + strSerialNum + "," + strCmdType;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return bRet;
        }

        //Update SolderPoint ONLY///////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_UpdateSolderPoint(string strSerialNumber, string strResult)
        {
            string strCmd = "";
            string strResponse = "";

            //compile string
            /*------------------------------------------------------------------------------------
             * Results:   Item						Cmd @ index 2 > send direct SQL string
             *			> Solder Point done			> Solder Position Used
             -------------------------------------------------------------------------------------*/
            strCmd = "UpdateSolderPoint," + strSerialNumber + "," + strResult;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) { return false; }
            if (Decode_Response(strResponse) == false) { return false; }

            return true;
        }

        //Get StationReg From Server///////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetStationRegFromServer(String strSerialNum, String strCmdType, ref String strResponse)
        {
            bool bRet = true;
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "GetStationRegFromServer" + "," + strSerialNum + "," + strCmdType;

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Get Part Count from server//////////////////////////////////////////////////////////////////////////////////////////
        public bool Cmd_GetPassFail_Shiftly(String strStation, ref String strResponse)
        {
            bool bRet = true;
            String strCmd = "";
            strResponse = "";

            //compile string
            strCmd = "GetPassFail_Shiftly" + "," + strStation.ToUpper();

            //send cmd
            if (SendCommand(strCmd, ref strResponse) == false) return false;
            if (Decode_Response(strResponse) == false) return false;

            return bRet;
        }

        //Set Error///////////////////////////////////////////////////////////////////////////////
        public void Set_Error(int iErrorID, String strErrorMsgAdd)
        {
            m_iErrorID = iErrorID;
            m_strErrorMessageAdd = strErrorMsgAdd;
        }
        //Get Error/////////////////////////////////////////////////////////////////////////////
        public int Get_ErrorID()
        {
            int iErrorID = m_iErrorID;
            m_iErrorID = 0;
            return iErrorID;
        }
        public String Get_ErrorMessage()
        {
            String strError = m_strErrorMessageAdd;
            m_strErrorMessageAdd = "";
            return strError;
        }

        //get Debugging Message//////////////////////////////////////////////////
        public String Get_Debug()
        {
            String strDebug = m_strLabel + ": cmd=" + m_strDebug_Cmd + "\r\nResponse=" + m_strDebug_Response;
            m_strDebug_Cmd = "";
            m_strDebug_Response = "";

            return strDebug;
        }
    }


    //Server Class Settings
    public class Server_COMTCP_GT_CentralPC_Settings
    {
        public Comm_TCP_Settings m_Settings = new Comm_TCP_Settings();

        public bool m_bEnable = false;

        //constructor
        public Server_COMTCP_GT_CentralPC_Settings() { }
    }
}