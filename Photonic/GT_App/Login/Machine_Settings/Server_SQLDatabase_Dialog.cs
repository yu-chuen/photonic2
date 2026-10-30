using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
	public partial class Server_SQLDatabase_Dialog : Form
	{
		//variables
		Server_SQL m_pServer = null;
		ServerSQL_Settings m_Settings;
		SeqMachineControl m_pSeqMachineCtrl = null;

		//Constructor//////////////////////////////////////////////////////////////////////////////////////
		public Server_SQLDatabase_Dialog()
		{
			InitializeComponent();

			m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_pServer = m_pSeqMachineCtrl.m_Server;
			m_Settings = m_pServer.m_Settings;
		}

		//Dialog Loaded////////////////////////////////////////////////////////////////////////////////
		private void Server_SQLDatabase_Load( object sender, EventArgs e )
		{
			txtb_DataSource.Text		= m_Settings.m_strDataSource;
			txtb_DBFilename.Text		= m_Settings.m_strDBFilename;
			txtb_InitCatalog.Text			= m_Settings.m_strInitialCatalog;
			txtb_Password.Text			= m_Settings.m_strPassword;
			txtb_Username.Text			= m_Settings.m_strUsername;
			txtb_TableName.Text			= m_Settings.m_strTablename;



		}

		//Class Helper///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Update_TextBox(bool bReturn, String strResponse, String strInputData)
		{
			if ( bReturn == false){
				strResponse = m_pSeqMachineCtrl.Get_ErrorMessage(m_pServer.Get_ErrorID(), m_pServer.Get_ErrorMessage());
				Class_Helper.Display_ErrorMessageBox(strResponse);
			}

			txtb_response.Text = strResponse;
			txtb_inputString.Text = strInputData;
		}

		//Button events//////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_Update_Click( object sender, EventArgs e )
		{
			m_Settings.m_strDataSource			= txtb_DataSource.Text;
			m_Settings.m_strDBFilename		= txtb_DBFilename.Text;		
			m_Settings.m_strInitialCatalog		= txtb_InitCatalog.Text;	
			m_Settings.m_strPassword			= txtb_Password.Text;		
			m_Settings.m_strUsername			= txtb_Username.Text;			
			m_Settings.m_strTablename			= txtb_TableName.Text;

			m_pServer.To_Registry();
		}

		private void btn_ESC_Click( object sender, EventArgs e ) { this.Close(); }

		private void btn_Connect_Click( object sender, EventArgs e )
		{
			Update_TextBox(	m_pServer.Connect_Server()	, "Connected",	"");
		}

		private void btn_Disconnect_Click( object sender, EventArgs e )
		{
			m_pServer.Disconnect_Server();
		}

		private void btn_IsExist_Click( object sender, EventArgs e )
		{
			Update_TextBox(m_pServer.Is_Exist(txtb_SerialNumber.Text), 
																			"Serial Number Exist in the Database: " + txtb_SerialNumber.Text, 
																			m_pServer.m_strDebug);
		}

		private void btn_ReadDatabase_Click( object sender, EventArgs e )
		{
			String strData = "";
			int iDataCount = 0;

			//get record from database
			bool bRet = m_pServer.Database_GetData(txtb_SerialNumber.Text, ref strData, ref iDataCount);

			Update_TextBox(bRet, 
										"Return String=" + strData + "\r\nCount=" + iDataCount.ToString(),
										m_pServer.m_strDebug);
		}

		private void btn_DatabaseAdd_Click( object sender, EventArgs e )
		{
			Update_TextBox(m_pServer.Database_Add(txtb_SerialNumber.Text, 1, txtb_StationID.Text),
																							"Data added into Database. Please check the database",
																							m_pServer.m_strDebug);
		}

		private void btn_UpdateDatabase_Click( object sender, EventArgs e )
		{
			String strHeader = m_pServer.PartCount_str + "," + m_pServer.State_str;
			String strValues = "1,OK";

			Update_TextBox (	m_pServer.Database_Update(txtb_SerialNumber.Text, strValues, strHeader),
											"Database Updated.",
											m_pServer.m_strDebug);
		}

		private void btn_GetProductStatus_Click( object sender, EventArgs e )
		{
			String strData = "";
			MessageBox_InputString dlg = new MessageBox_InputString();
			dlg.Set_String();
			dlg.Set_Label("Input Targeted Station Name: Ex: OP100");

			if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				Update_TextBox(m_pServer.Database_GetProductStatus(txtb_SerialNumber.Text, dlg.Get_DataString(), ref strData),
												strData,
												m_pServer.m_strDebug);												
			}
		}

		private void btn_GetGoPermission_Click( object sender, EventArgs e )
		{
			int iRet = 0;
			String strResponse = "";

			MessageBox_InputString dlg = new MessageBox_InputString();
			dlg.Set_String();
			dlg.Set_Label("Input Targeted Station Name: Ex: OP100");

			if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				//get go permission
				iRet = m_pServer.Database_GoPermission(txtb_SerialNumber.Text, dlg.Get_DataString());

				//set response
				strResponse = "Return Response: " + iRet.ToString();

				//update UI
				Update_TextBox(	( iRet == 1) ? true : false,
												strResponse,
												m_pServer.m_strDebug);
			}
		}






	}
}
