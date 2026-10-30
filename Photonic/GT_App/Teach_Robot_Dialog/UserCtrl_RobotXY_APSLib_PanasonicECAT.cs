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
	public partial class UserCtrl_RobotXY_APSLib_PanasonicECAT : UserControl
	{
		//variables
		SeqMachineControl m_pSeqMachineControl;
		RobotXY_APSlib_PanasonicECAT m_RobotStation = null;
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
		public UserCtrl_RobotXY_APSLib_PanasonicECAT( RobotXY_APSlib_PanasonicECAT mRobotStation )
		{
			InitializeComponent();
			m_pSeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_RobotStation = mRobotStation;
		}

		//Set Data robot package pointer////////////////////////////////////////////////////////////////////////////////////
		public void Set_DataRobot_Ptr( Data_Robot mRobotData ) { m_RobotData = mRobotData; }



		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_RobotXY_APSLib_PanasonicECAT_Load( object sender, EventArgs e )
		{
			//update position list box
			txtb_ModuleName.Text = m_RobotStation.m_strLabel;

			//update position list box
			int iSettingsCount = m_RobotData.Get_Settings_Count();
			for ( int iCount = 0; iCount < iSettingsCount; iCount++ )
			{
				String[] ar = new string[2];
				ListViewItem lt;
				ar[0] = m_RobotData.Get_SettingsName(iCount);
				ar[1] = m_RobotData.Get_SettingsDesc(iCount);
				lt = new ListViewItem(ar);

				listView_Positions.Items.Add(lt);
			}

			//Servo button text
			//Update Button
			if ( m_RobotStation.m_indexerAxisX.Svon_i == 1 &&
				m_RobotStation.m_indexerAxisY.Svon_i == 1 
                )
			{
				btn_ServoOnOff.Text = "Servo is On";
				Update_PositionBoxes();
			}
			else btn_ServoOnOff.Text = "Servo is Off";

		}

		//Update position boxes//////////////////////////////////////////////////////////////////////////
		public void Update_PositionBoxes()
		{
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
			bool bRet = true;

			lock ( m_lock_Access )
			{
				mPosition = m_RobotStation.GetRobotPosition_mm(ref bRet);
				if ( bRet == false ) return;
								
				//update current positio boxes
				txtb_CurrentPosX.Text = mPosition.m_dbXPos.ToString();
				txtb_CurrentPosY.Text = mPosition.m_dbY1Pos.ToString();

				if ( m_RobotStation.GetRobotStatus() < 0 ) return;

				//Update Pic boxes
				Update_PicBox(picb_Alarm_X, m_RobotStation.m_indexerAxisX.Alarm_i);
				Update_PicBox(picb_Alarm_Y, m_RobotStation.m_indexerAxisY.Alarm_i);

				Update_PicBox(picb_Home_X, m_RobotStation.m_indexerAxisX.Home_i);
				Update_PicBox(picb_Home_Y, m_RobotStation.m_indexerAxisY.Home_i);

				Update_PicBox(picb_InPos_X, m_RobotStation.m_indexerAxisX.MoveDone_i);
				Update_PicBox(picb_InPos_Y, m_RobotStation.m_indexerAxisY.MoveDone_i);

				Update_PicBox(picb_SVON_AxisX, m_RobotStation.m_indexerAxisX.Svon_i);
				Update_PicBox(picb_SVON_AxisY, m_RobotStation.m_indexerAxisY.Svon_i);

				Update_PicBox(picb_EMG_X, m_RobotStation.m_indexerAxisX.EMG_i);
				Update_PicBox(picb_EMG_Y, m_RobotStation.m_indexerAxisY.EMG_i);

				Update_PicBox(picb_FLimit_X, m_RobotStation.m_indexerAxisX.EL_posivite_i);
				Update_PicBox(picb_FLimit_Y, m_RobotStation.m_indexerAxisY.EL_posivite_i);

				Update_PicBox(picb_RLimit_X, m_RobotStation.m_indexerAxisX.EL_negative_i);
				Update_PicBox(picb_RLimit_Y, m_RobotStation.m_indexerAxisY.EL_negative_i);

				Update_PicBox(picb_Moving_X, m_RobotStation.m_indexerAxisX.Moving_i);
				Update_PicBox(picb_Moving_Y, m_RobotStation.m_indexerAxisY.Moving_i);
			}

		}

		//Class helper///////////////////////////////////////////////////////////////////////////////////////////
		private void Detect_Num( TextBox obj )
		{
			String strData = "";
			short iFlag_Dot = 0;

			foreach ( char c in obj.Text.Trim() )
			{
				if ( c < '0' || c > '9' )
				{
					if ( c != '.' )
					{
						MessageBox.Show("Please enter a numeric value");
					}
					else if ( iFlag_Dot == 1 ) { }
					else
					{
						if ( c == '.' ) iFlag_Dot = 1;
						strData += c;
					}
				}
				else
					strData += c;
			}

			obj.Text = strData;
			obj.SelectionStart = strData.Length;
		}
		private void Update_PicBox( PictureBox obj, int bResult )
		{
			obj.Image.Dispose();
			if ( bResult == 1 )
				obj.Image = GT_App.Properties.Resources.Green_Ball;
			else
				obj.Image = GT_App.Properties.Resources.Red_Ball;
		}

		private void Display_ErrorMessageBox( String strAdditional )
		{
			Class_Helper.Display_ErrorMessageBox(m_pSeqMachineControl.Get_ErrorMessage(m_RobotStation.Get_ErrorID(), strAdditional, m_RobotStation.Get_ErrorMessage())); 
		}


		//Extra Threads////////////////////////////////////////////////////////////////////////////////////////
		public void Run_Thread( Robot_Pos_Settings mPosition )
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
			if ( m_RobotStation.Move_Abs(mPosition) == false ) { Display_ErrorMessageBox("Go to Position: " + mPosition.m_strPosDesc); return; }
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
                if (m_RobotStation.m_indexerAxisX.Svon_i == 1 && m_RobotStation.m_indexerAxisY.Svon_i == 1)
                { bRet = m_RobotStation.Servo_On(false); strAdd = "Servo Off"; }
                else
                { bRet = m_RobotStation.Servo_On(true); strAdd = "Servo On"; }

                //Get State
                if (m_RobotStation.m_indexerAxisX.Svon_i == 1 && m_RobotStation.m_indexerAxisY.Svon_i == 1)
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
			if ( listView_Positions.SelectedIndices.Count != 1 )
			{
				Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
				return;
			}

			mPosition.m_strPosName = listView_Positions.SelectedItems[0].SubItems[0].Text;      //position at robot controller

			//Get Position
			mPosition = m_RobotData.Get_Settings(mPosition.m_strPosName);
			mPosition.m_iRunSpeed_AxisX = trackBar_PrimarySpeed.Value;
			mPosition.m_iRunSpeed_AxisY = trackBar_PrimarySpeed.Value;
			//MessageBox.Show(mPosition.m_iRunSpeed_AxisX.ToString() + "\r\n" + mPosition.m_iRunSpeed_AxisY.ToString() + "\r\n" + mPosition.m_iRunSpeed_AxisZ + "\r\n");
			//move robot via thread
			Run_Thread(mPosition);

			//Set listview 
			listView_Positions.Select();

		}

        private void btn_SafetyWindowDlg_Click(object sender, EventArgs e)
        {
            Robot_Pos_Settings mPosition = new Robot_Pos_Settings();
            SafetyWindow_Dialog dlg = new SafetyWindow_Dialog();

            //Check if any item in the list is selected
            if (listView_Positions.SelectedIndices.Count != 1)
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
            if (bRet == false) { Display_ErrorMessageBox("Update Setting: Get Robot position"); return; }

            //get from pkg file
            mPosData = m_RobotData.Get_Settings(strPos);

            //Set position to pkg file
            mPosData.m_dbXPos = mPosition.m_dbXPos;
            mPosData.m_dbY1Pos = mPosition.m_dbY1Pos;
            mPosData.m_dbZPos = mPosition.m_dbZPos;
            mPosData.m_iRunSpeed_AxisX = trackBar_PrimarySpeed.Value;
            mPosData.m_iRunSpeed_AxisY = trackBar_PrimarySpeed.Value;
            mPosData.m_iRunSpeed_AxisZ = trackBar_PrimarySpeed.Value;

            //Update Settings
            m_RobotData.Update_Settings(mPosData);

            //Update pkg file
            m_pSeqMachineControl.Pkg_To_Registry();
		}

		private void btn_RobotSettings_Click( object sender, EventArgs e )
		{
            RobotSetting_XY_APSLib_PanasonicECAT_Dlg dlg = new RobotSetting_XY_APSLib_PanasonicECAT_Dlg();
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

			if ( m_RobotStation.m_indexerAxisY.Move_Rel(mPos) == false )
			{
				m_RobotStation.Set_Error(2, "Axis Y");
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

			if ( m_RobotStation.m_indexerAxisY.Move_Rel(mPos) == false )					
			{
				m_RobotStation.Set_Error(2, "Axis Y");
				Display_ErrorMessageBox("Jogging Y Positive");
			}
		}


        private void listView_Positions_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strPos = "";
            Robot_Pos_Settings mPosition;

            //get selected item from list
            if (listView_Positions.SelectedIndices.Count != 1) return;
            strPos = listView_Positions.SelectedItems[0].SubItems[0].Text;

            //get position from data robot
            mPosition = m_RobotData.Get_Settings(strPos);

            //update text box
            txtb_TeachPosX.Text = mPosition.m_dbXPos.ToString();
            txtb_TeachPosY.Text = mPosition.m_dbY1Pos.ToString();

            trackBar_PrimarySpeed.Value = mPosition.m_iRunSpeed_AxisX;
            lbl_PrimarySpeed.Text = trackBar_PrimarySpeed.Value.ToString();
        }


        //Set Labels//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Set_LabelBox(Label obj, int value)
        {
            obj.Text = value.ToString();
        }

        //trackbar Events//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void trackBar_PrimarySpeed_Scroll(object sender, EventArgs e) { Set_LabelBox(lbl_PrimarySpeed, trackBar_PrimarySpeed.Value); }

        private void btn_UpdatePosition_Click(object sender, EventArgs e)
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
            if (bRet == false) { Display_ErrorMessageBox("Update Setting: Get Robot position"); return; }

            //get from pkg file
            mPosData = m_RobotData.Get_Settings(strPos);

            //Set position to pkg file
            mPosData.m_dbXPos = double.Parse(txtb_TeachPosX.Text);
            mPosData.m_dbY1Pos = double.Parse(txtb_TeachPosY.Text);
            mPosData.m_iRunSpeed_AxisX = trackBar_PrimarySpeed.Value;
            mPosData.m_iRunSpeed_AxisY = trackBar_PrimarySpeed.Value;
            mPosData.m_iRunSpeed_AxisZ = trackBar_PrimarySpeed.Value;
 

            //Update Settings
            m_RobotData.Update_Settings(mPosData);

            //Update pkg file
            m_pSeqMachineControl.Pkg_To_Registry();
        }

        

	}
}
