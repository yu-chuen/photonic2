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
	public partial class Server_COMTCP_GT_CentralPC_Dlg : Form
	{
		//variables
		SeqMachineControl m_pSeqMachineCtrl;
		Server_COMTCP_GT_CentralPC m_Server;
		Server_COMTCP_GT_CentralPC_Settings m_Settings;
		MachineInfo m_pMachineInfo;

		UserCtrl_TCP_SettingPage m_UserCtrl_TCPSettingDlg = new UserCtrl_TCP_SettingPage();


		//Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////
		public Server_COMTCP_GT_CentralPC_Dlg()
		{
			InitializeComponent();

			//get objects
			m_pSeqMachineCtrl   = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_Server            = m_pSeqMachineCtrl.m_Server;
			m_Settings          = m_Server.m_Settings;
			m_pMachineInfo      = m_pSeqMachineCtrl.m_MachineInfo;
		}

		//Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////
		private void Server_COMTCP_GT_CentralPC_Dlg_Load( object sender, EventArgs e )
		{
			Point pPoint = new Point();
			
			//Load TCP Setting user control
			m_UserCtrl_TCPSettingDlg.Set_Settings(m_Settings.m_Settings);

            pPoint.X = 10;
			m_UserCtrl_TCPSettingDlg.Location = pPoint;
			this.Controls.Add(m_UserCtrl_TCPSettingDlg);

			//adjust UI
			pPoint.Y = m_UserCtrl_TCPSettingDlg.Size.Height;
			btn_Enable.Location = pPoint;

			pPoint.X = m_UserCtrl_TCPSettingDlg.Size.Width + 10;
            pPoint.Y = 0;
			groupBox_Operations.Location = pPoint;

			//update UI
			btn_Enable.Text = ( ( m_Settings.m_bEnable == true ) ? "Server Enabled" : "Server Disabled" );
			btn_Enable.BackColor = ( ( m_Settings.m_bEnable == true ) ? Color.Green : Color.Red );

            //add items to the combo box drop down list (Station ID)
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP40);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP50);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP60);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP70);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP80);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP90);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP100);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP110);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP120);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP130);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP135);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP140);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP150);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP160);
            //cmbBox_StationID.Text = cmbBox_StationID.Items[0].ToString(); /*select the first item by default*/

            //add items to the combo box drop down list (Command Strings)
            cmbBox_CommandStrings.Items.Add("result");
            cmbBox_CommandStrings.Items.Add("rework");
            cmbBox_CommandStrings.Items.Add("op50_vision_result");
            cmbBox_CommandStrings.Items.Add("SolderPoint1");
            cmbBox_CommandStrings.Items.Add("SolderPoint2");
            cmbBox_CommandStrings.Items.Add("SolderPoint3");
            cmbBox_CommandStrings.Items.Add("SolderPoint4");
            cmbBox_CommandStrings.Items.Add("SolderPoint5");
            cmbBox_CommandStrings.Items.Add("SolderPoint6");
            cmbBox_CommandStrings.Items.Add("wt_result");
            cmbBox_CommandStrings.Items.Add("vision_wt_result");
            cmbBox_CommandStrings.Items.Add("heightcheck_result");
            cmbBox_CommandStrings.Items.Add("lasermark_result");
            cmbBox_CommandStrings.Items.Add("op160_result");

            //add items to the combo box drop down list (Results (Testing))
            cmbBox_Results.Items.Add("Result,P,Pass");
            cmbBox_Results.Items.Add("op50_vision_result,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1");
            cmbBox_Results.Items.Add("SolderPoint1,1");
            cmbBox_Results.Items.Add("SolderPoint2,1");
            cmbBox_Results.Items.Add("SolderPoint3,1");
            cmbBox_Results.Items.Add("SolderPoint4,1");
            cmbBox_Results.Items.Add("SolderPoint5,1");
            cmbBox_Results.Items.Add("SolderPoint6,1");
            cmbBox_Results.Items.Add("wt_result,0.1,0.2,0.3,Open,0.5,35,0.6,89.1,X,123.45,X,X");
            cmbBox_Results.Items.Add("vision_wt_result,1,1,1,1,1,1,1,1,1,1,1,1");
            //cmbBox_Results.Text = cmbBox_Results.Items[0].ToString(); /*select the first item by default*/
		}


		//class helper////////////////////////////////////////////////////////////////////////////////////////////////////
		private void DisplayErrorMessage()
		{
            String strError = m_pSeqMachineCtrl.Get_ErrorMessage(m_Server.Get_ErrorID(), m_Server.Get_ErrorMessage(), "Server");

			//display error message
			Class_Helper.Display_ErrorMessageBox(strError);

			//set to response labels
			Set_ResponseLabel();
		}

		private void Set_ResponseLabel()
		{
			//set command string
			lbl_Command.Text = m_Server.m_strDebug_Cmd;
			lbl_Response.Text = m_Server.m_strDebug_Response;

			//reset debuggers
			m_Server.m_strDebug_Cmd = "";
			m_Server.m_strDebug_Response = "";
		}



		//Button Events/////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_Enable_Click( object sender, EventArgs e )
		{
			if ( m_Settings.m_bEnable == true ) m_Settings.m_bEnable = false;
			else m_Settings.m_bEnable = true;

			btn_Enable.Text = ( ( m_Settings.m_bEnable == true ) ? "Server Enabled" : "Server Disabled" );
			btn_Enable.BackColor = ( ( m_Settings.m_bEnable == true ) ? Color.Green : Color.Red );
		}

		private void btn_CmdPalletStationDone_Click( object sender, EventArgs e )
		{
			//checker
			if ( cmbBox_StationID.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select a station to test"); return; }
			if ( txtb_SN.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }

			//Set that this station completed.
			if ( !m_Server.Cmd_StationPalletDone(cmbBox_StationID.Text, txtb_SN.Text) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

		private void btn_UpdateResults_Click( object sender, EventArgs e )
		{
			//checker
			if ( cmbBox_Results.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Results combo box is empty. Please select a result string to test"); return; }
			if ( txtb_SN.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }

			//Update results
			if ( !m_Server.Cmd_UpdateResults(txtb_SN.Text, cmbBox_Results.Text) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

		private void btn_GetCurrentPkg_Click( object sender, EventArgs e )
		{
			String strTemp = "";

			//get current package file
			if ( !m_Server.Cmd_GetCurrentPkg(m_pMachineInfo.m_strMachineID, ref strTemp) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

		private void btn_SkipCheck_Click( object sender, EventArgs e )
		{
			bool bSkipCheck = false;

			//checker
			if ( cmbBox_StationID.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select a station to test"); return; }
			if ( txtb_SN.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }

			//get Skip check
			if ( !m_Server.Cmd_SkipCheck(txtb_SN.Text, cmbBox_StationID.Text, ref bSkipCheck) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

		private void btn_InserNewPart_Click( object sender, EventArgs e )
		{
			//Checker
			if ( txtb_SN.Text == "" ) { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }

			//Insert new part
			if ( !m_Server.Cmd_InsertNewPart(txtb_SN.Text) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

        private void btn_GetUserAccess_Click(object sender, EventArgs e)
        {
            //Checker
            if (txtb_Username.Text == "") { Class_Helper.Display_ErrorMessageBox("Username box is empty. Please insert a username."); return; }
            if (txtb_Password.Text == "") { Class_Helper.Display_ErrorMessageBox("Password box is empty. Please insert a password."); return; }

            //Insert new part
            if (m_Server.Cmd_GetUserAccess(txtb_Username.Text, txtb_Password.Text) == 0)
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void btn_Update_Click( object sender, EventArgs e )
		{
			//update TCP Settings
			//m_Settings.m_Settings = m_UserCtrl_TCPSettingDlg.Get_Settings();
			m_UserCtrl_TCPSettingDlg.Update_Settings();
			m_Server.To_Registry();
		}

        private void btn_GetDataFromServer_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //Checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_CommandStrings.Text == "") { Class_Helper.Display_ErrorMessageBox("Command Strings box is empty. Please select a command to test"); return; }

            //get data
            if (!m_Server.Cmd_GetDataFromServer(txtb_SN.Text, cmbBox_CommandStrings.Text, ref strResponse))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_GetPartCount_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //get data
            if (!m_Server.Cmd_GetPartCount(ref strResponse))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }
	}
}
