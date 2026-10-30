using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data.SqlClient;

namespace StaticCoreLib
{
	public class Server_SQL
	{
		//variables
		public ServerSQL_Settings m_Settings = new ServerSQL_Settings();
		SqlConnection m_sqlCnn = null;
		SqlDataReader m_sqlReader;

		bool m_bConnected = false;
		
		public String m_strFilePath = "";
		public String m_strLabel = "";
		public String m_strDebug = "";
		String m_strErrorMsg = "";
		int m_iErrorID = 0;

		//constructor///////////////////////////////////////////////////////////////////////////
		public Server_SQL(String strLabel, String strFilePath)
		{
			m_strLabel = strLabel;
			m_strFilePath = strFilePath;
			if ( m_strFilePath.EndsWith("\\") == false ) m_strFilePath += "\\";

		}

		
		//to Registry///////////////////////////////////////////////////////////////////////////
		public void To_Registry()
		{
			String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
			XMLSerializer.Serialize_Func<ServerSQL_Settings>(m_Settings, strFulFilePath);
		}

		//from Registry//////////////////////////////////////////////////////////////////////
		public void From_Registry()
		{
			String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
			m_Settings = XMLSerializer.Deserialize_Func<ServerSQL_Settings>(strFulFilePath);
		}

		//Set Server settings////////////////////////////////////////////////////////////// Note: Placed this function since this class will be duplicated for each station. Main Class will send its settings to the clone classes
		public void Set_Settings( ServerSQL_Settings mSettings )
		{
			m_Settings.m_bEnable					= mSettings.m_bEnable;
			m_Settings.m_strDataSource			= mSettings.m_strDataSource;
			m_Settings.m_strDBFilename		= mSettings.m_strDBFilename;
			m_Settings.m_strInitialCatalog		= mSettings.m_strInitialCatalog;
			m_Settings.m_strUsername			= mSettings.m_strUsername;
			m_Settings.m_strPassword			= mSettings.m_strPassword;
			m_Settings.m_strTablename			= mSettings.m_strTablename;
		}



		//Connect to SQL Server/////////////////////////////////////////////////////
		//Note: Called during read and write operations. Best pratice to do this as to not to ocupy the connection pool resources
		public bool Connect_Server()
		{
			SqlConnectionStringBuilder m_ConnectCmd = new SqlConnectionStringBuilder();
			m_ConnectCmd.DataSource = m_Settings.m_strDataSource;
			m_ConnectCmd.InitialCatalog = m_Settings.m_strInitialCatalog;
			m_ConnectCmd.UserID = m_Settings.m_strUsername;
			m_ConnectCmd.Password = m_Settings.m_strPassword;

			//Check
			if ( m_Settings.m_bEnable == false ) return true;
			if ( m_sqlCnn != null && m_bConnected == true ) return true;

			//work
			m_sqlCnn = new SqlConnection(m_ConnectCmd.ConnectionString);
			try
			{
				m_sqlCnn.Open();
			}
			catch (SqlException ex)
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_CONNECTION_ERR; 
				m_strErrorMsg = ex.Message;
				m_bConnected = false;
				return false;
			}

			m_bConnected = true;
			return m_bConnected;
		}

		//Disconnect from SQL Server//////////////////////////////////////////////
		public void Disconnect_Server()
		{
			if ( m_sqlCnn != null && m_bConnected == true )
			{
				m_sqlCnn.Close();
			}
		}

		//Clear connection pool////////////////////////////////////////////////////////////
		public void Clear_ConnectionPool()
		{
			SqlConnection.ClearAllPools();
		}




		//Server Core function: Read/////////////////////////////////////////////////////////
		public bool Server_Read (String strCmd, ref String strData, ref int iDataCount)
		{
			bool bRet = true;
			String[] strArData = new String[50];
			SqlCommand mCommand = new SqlCommand();

			//Blocker
			if ( m_Settings.m_bEnable == false ) return true;
			if ( m_sqlCnn != null && m_bConnected == true ) return true;

			//Connect to the database
			if ( Connect_Server() == false ) return false;

			//Read from data base
			try
			{
				//set Sql Cmd and reader
				mCommand.Connection = m_sqlCnn;
				mCommand.CommandText = strCmd;

				//read from database
				m_sqlReader = mCommand.ExecuteReader();
				bool b = m_sqlReader.Read();

				//Append data
				if ( b == true ) iDataCount = m_sqlReader.GetValues(strArData);
			}
			catch ( SqlException ex )
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_READER_ERR;
				m_strErrorMsg = "Server_Read: cmd=" + strCmd + " | " + ex.Message;
				bRet = false;
			}

			//Close Connection
			m_sqlReader.Close();
			m_sqlReader.Dispose();
			mCommand.Dispose();

			//disconnect server
			Disconnect_Server();

			//Compile responose
			for ( int iCount = 0; iCount < iDataCount; iCount++ )
			{
				strData += strArData[iCount];
				strData += ( iCount < iDataCount - 1 ) ? "," : "";
			}

			//debugger
			m_strDebug = "Read Data=" + strData + " | Count=" + iDataCount.ToString();
			
			return bRet;
		}

		//Server core Function: Write///////////////////////////////////////////////
		public bool Server_Query(String strCmd)
		{
			bool bRet = true;
			int iRowAffected = 0;
			SqlCommand mCommand = new SqlCommand();
			SqlDataAdapter mWriter = new SqlDataAdapter();

			//Blocker
			if ( m_Settings.m_bEnable == false ) return true;
			if ( m_sqlCnn != null && m_bConnected == true ) return true;

			//Connect to the database
			if ( Connect_Server() == false ) return false;

			//SQL Write to database
			try
			{
				//set Sql Cmd
				mCommand.Connection = m_sqlCnn;
				mCommand.CommandText = strCmd;

				//write to database
				mWriter.InsertCommand = mCommand;
				iRowAffected = mWriter.InsertCommand.ExecuteNonQuery();

				//Check
				if ( iRowAffected <= 0 ) { bRet = false; m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_RECORD_NONEXIST; m_strErrorMsg = "Server_Query: cmd=" + strCmd; }
				if ( iRowAffected > 1 ) { bRet = false; m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_DUPLICATEID; m_strErrorMsg = "Server_Query: cmd=" + strCmd; }
			}
			catch ( SqlException ex )
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_QUERY_ERR;
				m_strErrorMsg = "Server_Query: cmd=" + strCmd + " | " + ex.Message;
				bRet = false;
			}
			finally
			{
				//Close Connection
				mCommand.Dispose();
				mWriter.Dispose();
			}

			//disconnect server
			Disconnect_Server();

			//debugger
			m_strDebug = "Send Cmd Data=" + strCmd;
			
			return bRet;
		}



		/*********************************************************************************************************
		 * Database primitive functions
		 *********************************************************************************************************/
		//Primitive - Get Data from SQL table////////////////////////////////////////////////////////////////////////////////////////////////
		//note: this function is to return which ever data (set by strcollumn) from the sql table
		public bool Database_GetData( String strIdentifier, String strCollumn, String strTableName, ref String strData, ref int iDataCount )
		{
			bool bRet = true;
			String strCmd = "";

			iDataCount = 0;

			//Enabled
			if ( m_Settings.m_bEnable == false ) return true;

			strCmd += "SELECT " + strCollumn + " FROM " + strTableName + " ";
			strCmd += "WHERE Serial_Number='" + strIdentifier + "'";

			//Read from database
			bRet = Server_Read(strCmd, ref strData, ref iDataCount);

			//error
			if ( iDataCount <= 0 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_RECORD_NONEXIST; bRet = false; }

			return bRet;
		}

		//Primitive - Update Table//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// update the database items. station dependent. 
		/// </summary>
		/// <param name="strIdentifier"></param>
		/// <param name="strData">String of items which is separated with a comma</param>
		/// <param name="strCollumn">Header of the item which is separated with a comma</param>
		/// <returns></returns>
		public bool Database_Update( String strIdentifier, String strData, String strCollumn, String strTableName )
		{
			bool bRet = true;
			int iHeaderTotal = 0;
			String strCmd = "";
			String[] strHeader = new String[10];
			String[] strValues = new String[10];

			//split the strings
			strHeader = strCollumn.Split(',');
			strValues = strData.Split(',');
			iHeaderTotal = strHeader.Count();

			//construct command
			strCmd = "UPDATE " + strTableName + " SET ";

			for ( int iCount = 0; iCount < iHeaderTotal; iCount++ )
			{
				//add header and data to cmd string
				if ( strHeader[iCount] != "" && strValues[iCount] != "" )
					strCmd += strHeader[iCount] + "='" + strValues[iCount] + "'";

				//add comma for next header and data
				if ( iCount < iHeaderTotal - 1 ) strCmd += ",";
			}

			strCmd += "WHERE " + SerialNumber_str + "='" + strIdentifier + "'";

			//Write into database (insert)
			bRet = Server_Query(strCmd);

			return bRet;
		}








		/*********************************************************************************************************
		 * Database wrap functions
		 *********************************************************************************************************/
		//Check if item is in the server/////////////////////////////////////////////////////////////////////////////
		public bool Is_Exist (String strSerialNum)
		{
			String strCmd = "";
			String strResponse = "";
			int iDataCount = 0;
			bool bRet = true;

			//Enabled
			if ( m_Settings.m_bEnable == false ) return true;

			//Set Cmd String
			strCmd = "SELECT " + SerialNumber_str + " FROM " + m_Settings.m_strTablename + " WHERE " + SerialNumber_str + "='" + strSerialNum + "'";
			//Read from database
			bRet = Server_Read(strCmd, ref strResponse, ref iDataCount);

			//Check if data exist
			if ( iDataCount <= 0 || strResponse.Contains(strSerialNum) == false )
			{
				m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_RECORD_NONEXIST;
				bRet = false;
			}
			
			return bRet;
		}

		//Get Data from Database////////////////////////////////////////////////////////////////////////////////////////
		public bool Database_GetData( String strSerialNumber, ref String strData , ref int iDataCount)
		{
			String strCmd = "";
			String strCollumn = "";			
			bool bRet = true;

			iDataCount = 0;

			//Enabled
			if ( m_Settings.m_bEnable == false ) return true;

			//Set Cmd String
			strCollumn += PartCount_str + "," + State_str;

			strCmd += "SELECT " + strCollumn + " FROM " + m_Settings.m_strTablename + " ";
			strCmd += "WHERE Serial_Number='" + strSerialNumber + "'";

			//Read from database
			bRet = Server_Read(strCmd, ref strData, ref iDataCount);

			return bRet;
		}

		
		//Get Status/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public bool Database_GetProductStatus( String strSerialNumber, String strPreviousOP, ref String strData )
		{
			bool bRet = true;
			int iDataCount = 0;
			String strCmd = "";
			String strCollumn = "";

			//Enabled
			if ( m_Settings.m_bEnable == false ) return true;

			//construct command
			strCollumn += strPreviousOP + "," + Result_str;

			strCmd += "SELECT " + strCollumn + " FROM " + m_Settings.m_strTablename + " ";
			strCmd += "WHERE "+ SerialNumber_str + "='" + strSerialNumber + "'";

			//Read from database
			bRet = Server_Read(strCmd, ref strData, ref iDataCount);

			//error
			if ( iDataCount <= 0 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_RECORD_NONEXIST; bRet = false; }

			return bRet;
		}

		//Get Database Go Permission////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Get Data from sql server. Receive the Go or NoGo signal from the server in the form of previous station passed or failed. 1=>Go, 0=>NoGo, -1=>Error, -2=>previous Station failed
		/// </summary>
		/// <param name="strSerialNumber">Serial number of the product / pallet</param>
		/// <param name="strPreviousOP">Previous station name</param>
		/// <returns>1=>Go, 0=>NoGo, -1=>Error, -2=>previous Station failed</returns>
		public int Database_GoPermission( String strSerialNumber, String strPreviousOP )
		{
			bool bRet = true;
			int iDataCount = 0;
			int iRet = 0;
			String strCmd = "";
			String strCollumn = "";
			String strData = "";
			String[] strArData = new String [50];

			//Enabled
			if ( m_Settings.m_bEnable == false ) return 1;

			//construct command
			strCollumn += strPreviousOP + "," + Result_str;

			strCmd += "SELECT " + strCollumn + " FROM " + m_Settings.m_strTablename + " ";
			strCmd += "WHERE " + SerialNumber_str + "='" + strSerialNumber + "'";

			//Read from database
			bRet = Server_Read(strCmd, ref strData, ref iDataCount);

			//error
			if ( iDataCount <= 0 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_RECORD_NONEXIST; iRet = -1; }
			
			//decode data
			if ( bRet == true )
			{
				strArData = strData.Split(',');
				iRet = ( strArData[1] == "F" ) ? 0 : 1;			//Get Result Status
				if (iRet == 1)
					iRet = ( strArData[0] == "1" ) ? 1 : -2;		//Get Previous OP status

				//set error
				if ( iRet == 0 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_NOTALLOWEDTORUN; }
				if ( iRet == -2 ) { m_iErrorID = ( int ) ErrorTable.en_ErrorCode.SERVER_PREVSTAT_FAIL; }
			}
			else
				iRet = -1;

			return iRet;
		}

		//Add To Database/////////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Add record into the data base
		/// </summary>
		/// <param name="strSerialNum">Pallet serial number</param>
		/// <returns></returns>
		public bool Database_Add( String strSerialNum, int PartCount , String strStationID="0" )
		{
			bool bRet = true;
			String strCmd = "";
			String strCollumn = "";

			//check if record exist
			if ( Is_Exist(strSerialNum) == false ) return false;

			//construct command
			strCollumn = SerialNumber_str + "," + PartCount_str + "," + State_str;

			strCmd += "INSERT INTO " + m_Settings.m_strTablename + " (" + strCollumn + ") VALUES (";
			strCmd += "'" + strSerialNum + "',";
			strCmd += "'" + PartCount.ToString() + "',";
			strCmd += "'new'";
			strCmd += ")";

			//Write into database (insert)
			bRet = Server_Query(strCmd);

			return bRet;
		}

		//Update Database///////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// update the database items. station dependent. 
		/// </summary>
		/// <param name="strSerialNumber"></param>
		/// <param name="strData">String of items which is separated with a comma</param>
		/// <param name="strCollumn">Header of the item which is separated with a comma</param>
		/// <returns></returns>
		public bool Database_Update(String strSerialNumber, String strData, String strCollumn)
		{
			bool bRet = true;
			int iHeaderTotal = 0;
			String strCmd = "";
			String[] strHeader = new String[10];
			String[] strValues = new String[10];

			//split the strings
			strHeader = strCollumn.Split(',');
			strValues = strData.Split(',');
			iHeaderTotal = strHeader.Count();

			//construct command
			strCmd = "UPDATE " + m_Settings.m_strTablename + " SET ";

			for ( int iCount = 0; iCount < iHeaderTotal; iCount++ )
			{
				//add header and data to cmd string
				if ( strHeader[iCount] != "" && strValues[iCount] != "" )
					strCmd += strHeader[iCount] + "='" + strValues[iCount] + "'";
				 
				//add comma for next header and data
				if ( iCount < iHeaderTotal - 1 ) strCmd += ",";
			}

			strCmd += "WHERE " + SerialNumber_str + "='" + strSerialNumber + "'";
			
			//Write into database (insert)
			bRet = Server_Query(strCmd);

			return bRet;
		}



		//Get Error////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID;
		}
		public String Get_ErrorMessage()
		{
			String strErrorMsg = m_strErrorMsg;
			m_strErrorMsg = "";
			return strErrorMsg;
		}


		//table header Strings//////////////////////////////////////////////////////////////////////////////////////////////
		public String SerialNumber_str			{ get { return "Serial_Number"; } }
		public String PartCount_str					{ get { return "PartCount_in";} }
		public String PartCountDone_str		{ get { return "PartCount_out"; } }
		public String State_str							{ get { return "State";} }
		public String Result_str							{ get { return "Result"; } }

	}


	//Server Class settings
	[Serializable]
	public class ServerSQL_Settings
	{
		public bool m_bEnable = false;

		//Connection
		public String m_strDataSource = "";
		public String m_strDBFilename = "";
		public String m_strInitialCatalog = "";
		public String m_strUsername = "";
		public String m_strPassword = "";

		//Data Access
		public String	m_strTablename = "table";
	}

}
