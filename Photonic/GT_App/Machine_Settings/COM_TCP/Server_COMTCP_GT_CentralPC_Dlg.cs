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
        Data_UnitTempStorage m_DataUnit;
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
            m_DataUnit          = m_pSeqMachineCtrl.m_DataUnit;
		}

		//Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////
        private void Server_COMTCP_GT_CentralPC_Dlg_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point();

            //Load TCP Setting user control
            m_UserCtrl_TCPSettingDlg.Set_Settings(m_Settings.m_Settings);

            pPoint.X = groupBox_Operations.Location.X + groupBox_Operations.Size.Width + 5;
            pPoint.Y = groupBox_Operations.Location.Y;
            m_UserCtrl_TCPSettingDlg.Location = pPoint;
            this.Controls.Add(m_UserCtrl_TCPSettingDlg);

            //adjust UI
            pPoint.X = m_UserCtrl_TCPSettingDlg.Location.X;
            pPoint.Y = m_UserCtrl_TCPSettingDlg.Location.Y + m_UserCtrl_TCPSettingDlg.Size.Height + 5;
            btn_Enable.Location = pPoint;

            pPoint.Y = btn_Enable.Location.Y + btn_Enable.Size.Height + 50;
            btn_ForceDoneOP60.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP60.Location.Y + btn_ForceDoneOP60.Size.Height + 5;
            btn_ForceDoneOP70.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP70.Location.Y + btn_ForceDoneOP70.Size.Height + 5;
            btn_ForceDoneOP80.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP80.Location.Y + btn_ForceDoneOP80.Size.Height + 5;
            btn_ForceDoneOP85.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP85.Location.Y + btn_ForceDoneOP85.Size.Height + 5;
            btn_ForceDoneOP90.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP90.Location.Y + btn_ForceDoneOP90.Size.Height + 5;
            btn_ForceDoneOP100.Location = pPoint;

            pPoint.Y = btn_ForceDoneOP100.Location.Y + btn_ForceDoneOP100.Size.Height + 5;
            btn_ForceDoneOP110.Location = pPoint;

            //update UI
            Update_UI();

            //add items to the combo box drop down list (Station ID)
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP40);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP45);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP47);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP50);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP60);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP70);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP80);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP85);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP90);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP100);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP110);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP115);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP120);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP125);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP130);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP135);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP137);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP140);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP150);
            cmbBox_StationID.Items.Add(m_pMachineInfo.m_strOP160);
            //cmbBox_StationID.Text = cmbBox_StationID.Items[0].ToString(); /*select the first item by default*/

            //add items to the combo box drop down list (Command Strings)
            cmbBox_CommandStrings.Items.Add("result");
            cmbBox_CommandStrings.Items.Add("rework");
            cmbBox_CommandStrings.Items.Add("OP50_vision_result");
            cmbBox_CommandStrings.Items.Add("SolderPoint1");
            cmbBox_CommandStrings.Items.Add("SolderPoint2");
            cmbBox_CommandStrings.Items.Add("SolderPoint3");
            cmbBox_CommandStrings.Items.Add("SolderPoint4");
            cmbBox_CommandStrings.Items.Add("SolderPoint5");
            cmbBox_CommandStrings.Items.Add("SolderPoint6");
            cmbBox_CommandStrings.Items.Add("OP90_wt_result");
            cmbBox_CommandStrings.Items.Add("OP90_vision_result");
            cmbBox_CommandStrings.Items.Add("OP100_data");
            cmbBox_CommandStrings.Items.Add("OP140_heightcheck_result");
            cmbBox_CommandStrings.Items.Add("OP150_lasermarking_data");
            cmbBox_CommandStrings.Items.Add("OP150_check_data");
            cmbBox_CommandStrings.Items.Add("OP160_data");
            cmbBox_CommandStrings.Items.Add("OP160_station");

            //add items to the combo box drop down list (Results (Testing))
            cmbBox_Results.Items.Add("Result,Pass,Pass");
            cmbBox_Results.Items.Add("OP50_vision_result,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1");
            cmbBox_Results.Items.Add("AllSolderPoint,0,0,0,0,0,0");
            cmbBox_Results.Items.Add("SolderPoint1,1");
            cmbBox_Results.Items.Add("SolderPoint2,1");
            cmbBox_Results.Items.Add("SolderPoint3,1");
            cmbBox_Results.Items.Add("SolderPoint4,1");
            cmbBox_Results.Items.Add("SolderPoint5,1");
            cmbBox_Results.Items.Add("SolderPoint6,1");
            cmbBox_Results.Items.Add("OP90_wt_result,LN1,LN2,LN3,LR0,R0R1,Temperature,LN Avg,HTR Min.,HTR Avg.,HTR Max.,HTR Result,HTR Grade,RTD Min.,RTD Avg.,RTD Max.,RTD Result,RTD Grade,Overall Result,Overall Grade");
            cmbBox_Results.Items.Add("OP90_vision_result,R0,R1,N3,N2,N1,L");
            //cmbBox_Results.Text = cmbBox_Results.Items[0].ToString(); /*select the first item by default*/
        }

        private void Update_UI()
        {
            btn_Enable.Text = ((m_Settings.m_bEnable == true) ? "Server Enabled" : "Server Disabled");
            btn_Enable.BackColor = ((m_Settings.m_bEnable == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP60.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP60 == true) ? "Force Done OP60\r\nEnabled" : "Force Done OP60\r\nDisabled");
            btn_ForceDoneOP60.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP60 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP70.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP70 == true) ? "Force Done OP70\r\nEnabled" : "Force Done OP70\r\nDisabled");
            btn_ForceDoneOP70.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP70 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP80.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP80 == true) ? "Force Done OP80\r\nEnabled" : "Force Done OP80\r\nDisabled");
            btn_ForceDoneOP80.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP80 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP85.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP85 == true) ? "Force Done OP85\r\nEnabled" : "Force Done OP85\r\nDisabled");
            btn_ForceDoneOP85.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP85 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP90.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP90 == true) ? "Force Done OP90\r\nEnabled" : "Force Done OP90\r\nDisabled");
            btn_ForceDoneOP90.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP90 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP100.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP100 == true) ? "Force Done OP100\r\nEnabled" : "Force Done OP100\r\nDisabled");
            btn_ForceDoneOP100.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP100 == true) ? Color.Green : Color.Red);

            btn_ForceDoneOP110.Text = ((m_DataUnit.m_Flag_ServerOn_BypassOP110 == true) ? "Force Done OP110\r\nEnabled" : "Force Done OP110\r\nDisabled");
            btn_ForceDoneOP110.BackColor = ((m_DataUnit.m_Flag_ServerOn_BypassOP110 == true) ? Color.Green : Color.Red);
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

        private void Update_Settings()
        {
            //update TCP Settings
            //m_Settings.m_Settings = m_UserCtrl_TCPSettingDlg.Get_Settings();
            m_UserCtrl_TCPSettingDlg.Update_Settings();

            //save to registry
            m_Server.To_Registry();
        }


		//Button Events/////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Enable_Click(object sender, EventArgs e)
        {
            if (m_Settings.m_bEnable == true) { m_Settings.m_bEnable = false; }
            else { m_Settings.m_bEnable = true; }

            btn_Enable.Text = ((m_Settings.m_bEnable == true) ? "Server Enabled" : "Server Disabled");
            btn_Enable.BackColor = ((m_Settings.m_bEnable == true) ? Color.Green : Color.Red);

            Update_Settings();
        }

        private void btn_CmdPalletStationDone_Click(object sender, EventArgs e)
        {
            //checker
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select a station to test"); return; }
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }

            //Set that this station completed.
            if (!m_Server.Cmd_StationPalletDone(cmbBox_StationID.Text, txtb_SN.Text))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_UpdateInput_Click(object sender, EventArgs e)
        {
            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select or insert a station to test."); return; }

            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            //Update Output
            if (!m_Server.Cmd_UpdateInput(txtb_SN.Text, cmbBox_StationID.Text, strDateTime))
            {
                DisplayErrorMessage();
            }
            else
            {
                Set_ResponseLabel();
            }
        }

        private void btn_UpdateOutput_Click(object sender, EventArgs e)
        {
            String[] strArData;
            int iResult;

            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select or insert a station to test."); return; }

            if (txtb_SN.Text.Contains(',') == false) { txtb_SN.Text += ","; }
            strArData = txtb_SN.Text.Split(',');

            if (int.TryParse(strArData[1], out iResult) == false || iResult < 0 || iResult > 1)
            {
                Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: SN,PassReject\r\nPass = 1, Reject = 0");
                return;
            }

            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            //Update Output
            if (!m_Server.Cmd_UpdateOutput(strArData[0], cmbBox_StationID.Text, int.Parse(strArData[1]), strDateTime))
            {
                DisplayErrorMessage();
            }
            else
            {
                Set_ResponseLabel();
            }
        }

        private void btn_UpdateProcessTime_Click(object sender, EventArgs e)
        {
            String[] strArData;
            double dbResult;

            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select or insert a station to test."); return; }

            if (txtb_SN.Text.Contains(',') == false) { txtb_SN.Text += ","; }
            strArData = txtb_SN.Text.Split(',');

            if (double.TryParse(strArData[1], out dbResult) == false)
            {
                Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: SN,ProcessTime");
                return;
            }

            //Update ProcessTime
            if (!m_Server.Cmd_UpdateProcessTime(strArData[0], cmbBox_StationID.Text, double.Parse(strArData[1])))
            {
                DisplayErrorMessage();
            }
            else
            {
                Set_ResponseLabel();
            }
        }

        private void btn_UpdateCycleTime_Click(object sender, EventArgs e)
        {
            String[] strArData;
            double dbResult;

            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select or insert a station to test."); return; }

            if (txtb_SN.Text.Contains(',') == false) { txtb_SN.Text += ","; }
            strArData = txtb_SN.Text.Split(',');

            if (double.TryParse(strArData[1], out dbResult) == false)
            {
                Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: SN,CycleTime");
                return;
            }

            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            //Update CycleTime
            if (!m_Server.Cmd_UpdateCycleTime(strArData[0], cmbBox_StationID.Text, double.Parse(strArData[1]), strDateTime))
            {
                DisplayErrorMessage();
            }
            else
            {
                Set_ResponseLabel();
            }
        }

        private void btn_UpdateResults_Click(object sender, EventArgs e)
        {
            //checker
            if (cmbBox_Results.Text == "") { Class_Helper.Display_ErrorMessageBox("Results combo box is empty. Please select a result string to test"); return; }
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }

            //Update results
            if (!m_Server.Cmd_UpdateResults(txtb_SN.Text, cmbBox_Results.Text))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_ResetStationReg_Click(object sender, EventArgs e)
        {
            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID box is empty. Please insert a Station ID to test"); return; }
            
            //Update results
            if (!m_Server.Cmd_UpdateStationReg(txtb_SN.Text, "reset_stationreg," + cmbBox_StationID.Text))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_OverwriteStationReg_Click(object sender, EventArgs e)
        {
            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID box is empty. Please insert a Station ID to test"); return; }

            //Update results
            if (!m_Server.Cmd_UpdateStationReg(txtb_SN.Text, "overwrite_stationreg," + cmbBox_StationID.Text))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

		private void btn_GetCurrentPkg_Click( object sender, EventArgs e )
		{
			String strTemp = "";

			//get current package file
			if ( !m_Server.Cmd_GetCurrentPkg("1", ref strTemp) )
				DisplayErrorMessage();
			else
				Set_ResponseLabel();
		}

        private void btn_SkipCheck_Click(object sender, EventArgs e)
        {
            bool bSkipCheck = false;

            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select a station to test"); return; }

            //get Skip check
            if (!m_Server.Cmd_SkipCheck(txtb_SN.Text, cmbBox_StationID.Text, ref bSkipCheck))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_InsertErrorLog_Click(object sender, EventArgs e)
        {
            String strData = "";
            String[] strArData;
            int iResult;

            //checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number textbox is empty. Please insert a SN to test."); return; }

            //Note:   Serial Number, + PC, + ErrorCode, + ErrorName, + ;ErrorDesc;
            //index:       [0]         [1]      [2]          [3]           [4]
            if (txtb_SN.Text.Contains(',') == false) { txtb_SN.Text += ","; }
            if (txtb_SN.Text.Contains(';') == false) { Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: Serial Number,PC Number,ErrorCode,ErrorName,;ErrorDesc"); return; }
            
            strArData = txtb_SN.Text.Split(',');
            if (strArData.Count() < 5) { Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: Serial Number,PC Number,ErrorCode,ErrorName,;ErrorDesc"); return; }
            
            if (int.TryParse(strArData[1], out iResult) == false || int.TryParse(strArData[2], out iResult) == false)
            {
                Class_Helper.Display_ErrorMessageBox("Error: Invalid Format.\r\nPlease make sure Serial Number textbox is in the correct format.\r\nFormat: Serial Number,PC Number,ErrorCode,ErrorName,;ErrorDesc");
                return;
            }

            strData = txtb_SN.Text.Substring(txtb_SN.Text.IndexOf(';') + 1).Trim();
            if (strData.EndsWith(",")) { strData = strData.Remove(strData.LastIndexOf(",")); }

            String strDateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");

            //Update CycleTime
            if (!m_Server.Cmd_InsertErrorLog(strArData[0], strDateTime, int.Parse(strArData[1]), int.Parse(strArData[2]), strArData[3], strData))
            {
                DisplayErrorMessage();
            }
            else
            {
                Set_ResponseLabel();
            }
        }

        private void btn_InserNewPart_Click(object sender, EventArgs e)
        {
            //Checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test."); return; }

            //Insert new part
            if (!m_Server.Cmd_InsertNewPart(txtb_SN.Text))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_GetUserAccess_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //Checker
            if (txtb_Username.Text == "") { Class_Helper.Display_ErrorMessageBox("Username box is empty. Please insert a username."); return; }
            if (txtb_Password.Text == "") { Class_Helper.Display_ErrorMessageBox("Password box is empty. Please insert a password."); return; }

            //get user access
            if (m_Server.Cmd_GetUserAccess(txtb_Username.Text, txtb_Password.Text, ref strResponse) == 0)
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
            Update_Settings();
		}

        private void btn_GetProcessDataFromServer_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //Checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_CommandStrings.Text == "") { Class_Helper.Display_ErrorMessageBox("Command Strings box is empty. Please select a command to test"); return; }

            //get data
            if (!m_Server.Cmd_GetProcessDataFromServer(txtb_SN.Text, cmbBox_CommandStrings.Text, ref strResponse))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_GetPassFail_Shiftly_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //checker
            if (cmbBox_StationID.Text == "") { Class_Helper.Display_ErrorMessageBox("Station ID combo box is empty. Please select a station to test"); return; }

            //get data
            if (!m_Server.Cmd_GetPassFail_Shiftly(cmbBox_StationID.Text, ref strResponse))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_GetStationRegFromServer_Click(object sender, EventArgs e)
        {
            String strResponse = "";

            //Checker
            if (txtb_SN.Text == "") { Class_Helper.Display_ErrorMessageBox("Serial Number box is empty. Please insert a SN to test"); return; }
            if (cmbBox_CommandStrings.Text == "") { Class_Helper.Display_ErrorMessageBox("Command Strings box is empty. Please select a command to test"); return; }

            //get data
            if (!m_Server.Cmd_GetStationRegFromServer(txtb_SN.Text, cmbBox_CommandStrings.Text, ref strResponse))
                DisplayErrorMessage();
            else
                Set_ResponseLabel();
        }

        private void btn_ForceDoneOP60_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP60 = (m_DataUnit.m_Flag_ServerOn_BypassOP60 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP70_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP70 = (m_DataUnit.m_Flag_ServerOn_BypassOP70 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP80_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP80 = (m_DataUnit.m_Flag_ServerOn_BypassOP80 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP85_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP85 = (m_DataUnit.m_Flag_ServerOn_BypassOP85 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP90_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP90 = (m_DataUnit.m_Flag_ServerOn_BypassOP90 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP100_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP100 = (m_DataUnit.m_Flag_ServerOn_BypassOP100 == true) ? false : true;
            Update_UI();
        }

        private void btn_ForceDoneOP110_Click(object sender, EventArgs e)
        {
            m_DataUnit.m_Flag_ServerOn_BypassOP110 = (m_DataUnit.m_Flag_ServerOn_BypassOP110 == true) ? false : true;
            Update_UI();
        }
	}
}