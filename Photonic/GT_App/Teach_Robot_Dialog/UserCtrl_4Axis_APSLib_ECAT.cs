using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
	public partial class UserCtrl_4Axis_APSLib_ECAT : UserControl
	{
		//variables
        SeqMachineControl m_pSeqMachineControl;
		RobotXYZ_APSlib_PanasonicECAT m_RobotStation = null;
		Data_Robot m_RobotData;

		//Lock to restrict access to the class call - prevent collision
		private readonly object m_lock_Access = new object();
		bool m_bFlag_Access = false;
		public bool Flag_Access_b
		{
			set { lock ( m_lock_Access ) { m_bFlag_Access = value; } }
			get { lock ( m_lock_Access ) { return m_bFlag_Access; } }
		}
		
		//Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public UserCtrl_4Axis_APSLib_ECAT( RobotXYZ_APSlib_PanasonicECAT mRobotStation )
		{
			InitializeComponent();

			m_pSeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_RobotStation = mRobotStation;
		}

		//Set Data robot package pointer////////////////////////////////////////////////////////////////////////////////////
		public void Set_DataRobot_Ptr( Data_Robot mRobotData ) { m_RobotData = mRobotData; }

		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_RobotXYZ_APSLib_PanasonicECAT_Load(object sender, EventArgs e)
        {
            //update position list box
            int iSettingsCount = m_RobotData.Get_Settings_Count();
            for (int iCount = 0; iCount < iSettingsCount; iCount++)
            {
                string[] ar = new string[2];
                ar[0] = m_RobotData.Get_SettingsName(iCount);
                ar[1] = m_RobotData.Get_SettingsDesc(iCount);
                
                ListViewItem lt = new ListViewItem(ar);
                listView_Positions.Items.Add(lt);
            }

            //Servo button text
            //Update Button
            if (m_RobotStation.m_indexerAxisX.Svon_i == 1 &&
                m_RobotStation.m_indexerAxisY1.Svon_i == 1 &&
                m_RobotStation.m_indexerAxisY2.Svon_i == 1 &&
                m_RobotStation.m_indexerAxisZ.Svon_i == 1)
            {
                btn_ServoOnOff.Text = "Servo is On";
                Update_PositionBoxes();
            }
            else
            {
                btn_ServoOnOff.Text = "Servo is Off";
            }
        }

		//Update position boxes//////////////////////////////////////////////////////////////////////////
		public void Update_PositionBoxes()
		{
			Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
			bool bRet = true;

            lock (m_lock_Access)
            {
                mPosition = m_RobotStation.GetRobotPosition_mm(ref bRet);
                if (bRet == false) return;

                //update current position boxes
                txtb_CurrentPosX.Text = mPosition.m_dbXPos.ToString();
                txtb_CurrentPosY1.Text = mPosition.m_dbY1Pos.ToString();
                txtb_CurrentPosY2.Text = mPosition.m_dbY2Pos.ToString();
                txtb_CurrentPosZ.Text = mPosition.m_dbZPos.ToString();

                if (m_RobotStation.GetRobotStatus() < 0) return;

                //Update Pic boxes
                Update_PicBox(picb_Alarm_X, m_RobotStation.m_indexerAxisX.Alarm_i);
                Update_PicBox(picb_Alarm_Y1, m_RobotStation.m_indexerAxisY1.Alarm_i);
                Update_PicBox(picb_Alarm_Y2, m_RobotStation.m_indexerAxisY2.Alarm_i);
                Update_PicBox(picb_Alarm_Z, m_RobotStation.m_indexerAxisZ.Alarm_i);

                Update_PicBox(picb_Home_X, m_RobotStation.m_indexerAxisX.Home_i);
                Update_PicBox(picb_Home_Y1, m_RobotStation.m_indexerAxisY1.Home_i);
                Update_PicBox(picb_Home_Y2, m_RobotStation.m_indexerAxisY2.Home_i);
                Update_PicBox(picb_Home_Z, m_RobotStation.m_indexerAxisZ.Home_i);

                Update_PicBox(picb_InPos_X, m_RobotStation.m_indexerAxisX.MoveDone_i);
                Update_PicBox(picb_InPos_Y1, m_RobotStation.m_indexerAxisY1.MoveDone_i);
                Update_PicBox(picb_InPos_Y2, m_RobotStation.m_indexerAxisY2.MoveDone_i);
                Update_PicBox(picb_InPos_Z, m_RobotStation.m_indexerAxisZ.MoveDone_i);

                Update_PicBox(picb_SVON_AxisX, m_RobotStation.m_indexerAxisX.Svon_i);
                Update_PicBox(picb_SVON_AxisY1, m_RobotStation.m_indexerAxisY1.Svon_i);
                Update_PicBox(picb_SVON_AxisY2, m_RobotStation.m_indexerAxisY2.Svon_i);
                Update_PicBox(picb_SVON_AxisZ, m_RobotStation.m_indexerAxisZ.Svon_i);

                Update_PicBox(picb_EMG_X, m_RobotStation.m_indexerAxisX.EMG_i);
                Update_PicBox(picb_EMG_Y1, m_RobotStation.m_indexerAxisY1.EMG_i);
                Update_PicBox(picb_EMG_Y2, m_RobotStation.m_indexerAxisY2.EMG_i);
                Update_PicBox(picb_EMG_Z, m_RobotStation.m_indexerAxisZ.EMG_i);

                Update_PicBox(picb_FLimit_X, m_RobotStation.m_indexerAxisX.EL_posivite_i);
                Update_PicBox(picb_FLimit_Y1, m_RobotStation.m_indexerAxisY1.EL_posivite_i);
                Update_PicBox(picb_FLimit_Y2, m_RobotStation.m_indexerAxisY2.EL_posivite_i);
                Update_PicBox(picb_FLimit_Z, m_RobotStation.m_indexerAxisZ.EL_posivite_i);

                Update_PicBox(picb_RLimit_X, m_RobotStation.m_indexerAxisX.EL_negative_i);
                Update_PicBox(picb_RLimit_Y1, m_RobotStation.m_indexerAxisY1.EL_negative_i);
                Update_PicBox(picb_RLimit_Y2, m_RobotStation.m_indexerAxisY2.EL_negative_i);
                Update_PicBox(picb_RLimit_Z, m_RobotStation.m_indexerAxisZ.EL_negative_i);

                Update_PicBox(picb_Moving_X, m_RobotStation.m_indexerAxisX.Moving_i);
                Update_PicBox(picb_Moving_Y1, m_RobotStation.m_indexerAxisY1.Moving_i);
                Update_PicBox(picb_Moving_Y2, m_RobotStation.m_indexerAxisY2.Moving_i);
                Update_PicBox(picb_Moving_Z, m_RobotStation.m_indexerAxisZ.Moving_i);
            }
		}

		//Class helper///////////////////////////////////////////////////////////////////////////////////////////
        private void Detect_Num(TextBox obj)
        {
            String strData = "";
            short iFlag_Dot = 0;

            foreach (char c in obj.Text.Trim())
            {
                if (c < '0' || c > '9')
                {
                    if (c != '.')
                    {
                        MessageBox.Show("Please enter a numeric value");
                    }
                    else if (iFlag_Dot == 1) { }
                    else
                    {
                        if (c == '.') iFlag_Dot = 1;
                        strData += c;
                    }
                }
                else
                    strData += c;
            }

            obj.Text = strData;
            obj.SelectionStart = strData.Length;
        }

        private void Update_PicBox(PictureBox obj, int bResult)
        {
            obj.Image.Dispose();
            if (bResult == 1)
                obj.Image = GT_App.Properties.Resources.Green_Ball;
            else
                obj.Image = GT_App.Properties.Resources.Red_Ball;
        }

        private void Display_ErrorMessageBox(String strAdditional)
        {
            Class_Helper.Display_ErrorMessageBox(m_pSeqMachineControl.Get_ErrorMessage(m_RobotStation.Get_ErrorID(), strAdditional, m_RobotStation.Get_ErrorMessage()));
        }


		//Extra Threads////////////////////////////////////////////////////////////////////////////////////////
        public void Run_Thread(Robot_Pos_Settings mPosition)
        {
            Thread mTh = new Thread(() => Thread_MoveToPosition(mPosition));
            mTh.Start();
        }

		//Homing thread
		private void HomingThread()
		{
			if ( m_RobotStation.Homing() == false )
				Display_ErrorMessageBox("Homing / Initialization Failed: ");
			else
				MessageBox.Show("Initialization Complete.");
		}

		//Move to position thread
        public void Thread_MoveToPosition(Robot_Pos_Settings mPosition)
        {
            if (m_RobotStation.Move_Abs(mPosition) == false) { Display_ErrorMessageBox("Go To Position: " + mPosition.m_strPosDesc); return; }
            else
            {
                Class_Helper.Display_NoticeMessageBox("Movement Done.");
            }
        }


		//button events//////////////////////////////////////////////////////////////////////////////////////
		private void btn_Init_Click( object sender, EventArgs e )
		{
			Thread mth = new Thread(HomingThread);
			mth.Start();
		}

        private void btn_ServoOnOff_Click(object sender, EventArgs e)
        {
            bool bRet = true;
            String strAdd = "";

            lock (m_lock_Access)
            {
                //Set Servo ON / OFF
                if (m_RobotStation.m_indexerAxisX.Svon_i == 1 && m_RobotStation.m_indexerAxisY1.Svon_i == 1 && m_RobotStation.m_indexerAxisY2.Svon_i == 1 && m_RobotStation.m_indexerAxisZ.Svon_i == 1)
                { bRet = m_RobotStation.Servo_On(false); strAdd = "Servo Off"; }
                else
                { bRet = m_RobotStation.Servo_On(true); strAdd = "Servo On"; }

                //Get State
                if (m_RobotStation.m_indexerAxisX.Svon_i == 1 && m_RobotStation.m_indexerAxisY1.Svon_i == 1 && m_RobotStation.m_indexerAxisY2.Svon_i == 1 && m_RobotStation.m_indexerAxisZ.Svon_i == 1)
                {
                    btn_ServoOnOff.Text = "Servo is ON";
                }
                else
                {
                    btn_ServoOnOff.Text = "Servo is OFF";
                }
            }

            if (bRet == false) Display_ErrorMessageBox(strAdd);
        }

		private void btn_Reset_Click( object sender, EventArgs e )
		{
			if ( m_RobotStation.Reset_Alarm() == false ) Display_ErrorMessageBox("Reset Alarm");
		}

		private void btn_GotoPosition_Click( object sender, EventArgs e )
		{
			Robot_Pos_Settings mPosition = new Robot_Pos_Settings();

			//Check if any item in the list is selected
            if (listView_Positions.SelectedIndices.Count != 1)
            {
                Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
                return;
            }

			mPosition.m_strPosName = listView_Positions.SelectedItems[0].SubItems[0].Text;      //position at robot controller

			//Get Position
			mPosition = m_RobotData.Get_Settings(mPosition.m_strPosName);
			mPosition.m_iRunSpeed_AxisX = trackBar_Speed.Value;
			mPosition.m_iRunSpeed_AxisY = trackBar_Speed.Value;
			mPosition.m_iRunSpeed_AxisZ = trackBar_Speed.Value;
			mPosition.m_dbInterlock_Zht = 0;
			//MessageBox.Show(mPosition.m_iRunSpeed_AxisX.ToString() + "\r\n" + mPosition.m_iRunSpeed_AxisY.ToString() + "\r\n" + mPosition.m_iRunSpeed_AxisZ + "\r\n");
			//move robot via thread
			Run_Thread(mPosition);

			//Set listview 
			listView_Positions.Select();
		}

		private void btn_SafetyWindowDlg_Click( object sender, EventArgs e )
		{
			Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
			SafetyWindow_Dialog dlg = new SafetyWindow_Dialog();

			//Check if any item in the list is selected
			if ( listView_Positions.SelectedIndices.Count != 1 )
			{
				Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
				return;
			}

			mPosition.m_strPosName = listView_Positions.SelectedItems[0].SubItems[0].Text;      //position at robot controller

			//Get Position: for displaying the name only
			mPosition = m_RobotData.Get_Settings(mPosition.m_strPosName);

			dlg.Set_DataRobot_Ptr(this.m_RobotData);
            dlg.Set_PositionName(mPosition.m_strPosName);
			dlg.ShowDialog();
			dlg.Dispose();

		}

		private void btn_StopMotor_Click( object sender, EventArgs e )
		{
			if ( m_RobotStation.Stop_Move() == false ) Display_ErrorMessageBox("Stop Move");
		}

		private void btn_Update_Click( object sender, EventArgs e )
		{
			Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
			Robot_Pos_Settings mPosData = new Robot_Pos_Settings();
			bool bRet = true;
			String strPos = "";

			//Check if any item in the list is selected
            if (listView_Positions.SelectedIndices.Count != 1)
            {
                Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
                return;
            }

			strPos = listView_Positions.SelectedItems[0].SubItems[0].Text;      //position at robot controller

			//get robot position
			mPosition = m_RobotStation.GetRobotPosition_mm(ref bRet);
			if ( bRet == false ) { Display_ErrorMessageBox("Update Setting: Get Robot position"); return; }

			//get from pkg file
			mPosData = m_RobotData.Get_Settings(strPos);

			//Set position to pkg file
			mPosData.m_dbXPos = mPosition.m_dbXPos; 
			mPosData.m_dbY1Pos = mPosition.m_dbY1Pos;
            mPosData.m_dbY2Pos = mPosition.m_dbY2Pos;
            mPosData.m_dbZPos = mPosition.m_dbZPos;
			mPosData.m_iRunSpeed_AxisX = trackBar_Speed.Value;
			mPosData.m_iRunSpeed_AxisY = trackBar_Speed.Value;
			mPosData.m_iRunSpeed_AxisZ = trackBar_Speed.Value;
			mPosData.m_dbInterlock_Zht = double.Parse(lbl_ZHeight.Text);

			//Update Settings
			m_RobotData.Update_Settings(mPosData);

			//Update pkg file
			m_pSeqMachineControl.Pkg_To_Registry();
		}

        private void btn_UpdatePosition_Click(object sender, EventArgs e)
        {
            //check if any item in the list is selected
            if (listView_Positions.SelectedIndices.Count != 1)
            {
                Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
                return;
            }

            //checker
            double d;
            if (double.TryParse(txtb_TeachPosX.Text, out d) == false) { Class_Helper.Display_ErrorMessageBox("Teached Axis X contains invalid character."); return; }
            if (double.TryParse(txtb_TeachPosY1.Text, out d) == false) { Class_Helper.Display_ErrorMessageBox("Teached Axis Y1 contains invalid character."); return; }
            if (double.TryParse(txtb_TeachPosY2.Text, out d) == false) { Class_Helper.Display_ErrorMessageBox("Teached Axis Y2 contains invalid character."); return; }
            if (double.TryParse(txtb_TeachPosZ.Text, out d) == false) { Class_Helper.Display_ErrorMessageBox("Teached Axis Z contains invalid character."); return; }

            //variable
            Robot_Pos_Settings mPosData = new Robot_Pos_Settings();
            string strPos = "";

            //get position name
            strPos = listView_Positions.SelectedItems[0].SubItems[0].Text;

            //get existing settings from pkg file
            mPosData = m_RobotData.Get_Settings(strPos);

            //set new settings
            mPosData.m_dbXPos = double.Parse(txtb_TeachPosX.Text);
            mPosData.m_dbY1Pos = double.Parse(txtb_TeachPosY1.Text);
            mPosData.m_dbY2Pos = double.Parse(txtb_TeachPosY2.Text);
            mPosData.m_dbZPos = double.Parse(txtb_TeachPosY2.Text);
            mPosData.m_iRunSpeed_AxisX = trackBar_Speed.Value;
            mPosData.m_iRunSpeed_AxisY = trackBar_Speed.Value;
            mPosData.m_iRunSpeed_AxisZ = trackBar_Speed.Value;
            mPosData.m_dbInterlock_Zht = double.Parse(lbl_ZHeight.Text);

            //update Settings
            m_RobotData.Update_Settings(mPosData);

            //update pkg file
            m_pSeqMachineControl.Pkg_To_Registry();
        }

		private void btn_RobotSettings_Click( object sender, EventArgs e )
		{
			RobotSetting_APSLib_PanasonicECAT_Dlg dlg = new RobotSetting_APSLib_PanasonicECAT_Dlg();
			dlg.Set_RobotStation_Ptr(this.m_RobotStation);

			if ( dlg.ShowDialog() == DialogResult.OK )
				if ( !m_RobotStation.To_Registry() ) Display_ErrorMessageBox("");

			dlg.Dispose();
		}

		public void btn_JogNegX_Click( object sender, EventArgs e )
		{
			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog"; 
			mPos.m_dbPos = double.Parse(txtb_JogXValue.Text) * (-1);

			if ( m_RobotStation.m_indexerAxisX.Move_Rel(mPos) == false )
			{
				m_RobotStation.Set_Error(2, "Axis X");
				Display_ErrorMessageBox("Jogging X Negative");
			}
		}

		public void btn_JogPosX_Click( object sender, EventArgs e )
		{
			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog";
			mPos.m_dbPos = double.Parse(txtb_JogXValue.Text);

			if ( m_RobotStation.m_indexerAxisX.Move_Rel(mPos) == false )
			{
				m_RobotStation.Set_Error(2, "Axis X");
				Display_ErrorMessageBox("Jogging X Positive");
			}
		}

		public void btn_JogNegY_Click( object sender, EventArgs e )
		{
			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog";
			mPos.m_dbPos = double.Parse(txtb_JogYValue.Text) * ( -1 );

			if ( m_RobotStation.m_indexerAxisY1.Move_Rel(mPos) == false && m_RobotStation.m_indexerAxisY2.Move_Rel(mPos))
			{
				m_RobotStation.Set_Error(2, "Axis Y1");
                m_RobotStation.Set_Error(3, "Axis Y2");
                Display_ErrorMessageBox("Jogging Y Negative");
			}
		}

		public void btn_JogPosY_Click( object sender, EventArgs e )
		{
			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog";
			mPos.m_dbPos = double.Parse(txtb_JogYValue.Text);

			if ( m_RobotStation.m_indexerAxisY1.Move_Rel(mPos) == false && m_RobotStation.m_indexerAxisY2.Move_Rel(mPos) == false)					
			{
				m_RobotStation.Set_Error(2, "Axis Y1");
                m_RobotStation.Set_Error(3, "Axis Y2");
                Display_ErrorMessageBox("Jogging Y Positive");
			}
		}

		public void btn_JogNegZ_Click( object sender, EventArgs e )
		{

			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog";
			mPos.m_dbPos = double.Parse(txtb_JogZValue.Text) * ( -1 );

			if ( m_RobotStation.m_indexerAxisZ.Move_Rel(mPos) == false )
			{
				m_RobotStation.Set_Error(4, "Axis Z");
				Display_ErrorMessageBox("Jogging Z Negative");
			}
		}

		public void btn_JogPosZ_Click( object sender, EventArgs e )
		{

			Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
			mPos.m_bMoveAbs = true;
			mPos.m_iRunSpeed = 50;
			mPos.m_strPosName = "jog";
			mPos.m_dbPos = double.Parse(txtb_JogZValue.Text);

			if ( m_RobotStation.m_indexerAxisZ.Move_Rel(mPos) == false )
			{
				m_RobotStation.Set_Error(4, "Axis Z");
				Display_ErrorMessageBox("Jogging Z Positive");
			}
		}

        private void listView_Positions_SelectedIndexChanged(object sender, EventArgs e)
        {
            string strPos = "";
            Robot_Pos_Settings mPosition;

            //get selected item from list
            if (listView_Positions.SelectedIndices.Count != 1) return;
            strPos = listView_Positions.SelectedItems[0].SubItems[0].Text;

            //get position from data robot
            mPosition = m_RobotData.Get_Settings(strPos);

            //update text box
            txtb_TeachPosX.Text = mPosition.m_dbXPos.ToString();
            txtb_TeachPosY1.Text = mPosition.m_dbY1Pos.ToString();
            txtb_TeachPosY2.Text = mPosition.m_dbY2Pos.ToString();
            txtb_TeachPosZ.Text = mPosition.m_dbZPos.ToString();

            trackBar_Speed.Value = mPosition.m_iRunSpeed_AxisX;
            lbl_PrimarySpeed.Text = trackBar_Speed.Value.ToString();
            lbl_ZHeight.Text = mPosition.m_dbInterlock_Zht.ToString();
            lbl_ZInterlock.Text = ((mPosition.m_iInterlock == 1) ? "Yes" : "No").ToString();
            lbl_SelectedPosition.Text = "Selected Position: " + mPosition.m_strPosName + ", " + mPosition.m_strPosDesc;
        }

        private void btn_SetZHeight_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Double();
            dlg.Set_Title("Set Z Height");
            dlg.Set_Label("Current Z Height Value = " + lbl_ZHeight.Text);
            dlg.Set_InputBoxValue(lbl_ZHeight.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                lbl_ZHeight.Text = dlg.Get_DataString();
            }
        }

        //Set Labels//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Set_LabelBox(Label obj, int value)
        {
            obj.Text = value.ToString();
        }

        //trackbar Events//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void trackBar_PrimarySpeed_Scroll(object sender, EventArgs e) { Set_LabelBox(lbl_PrimarySpeed, trackBar_Speed.Value); }
	}
}
