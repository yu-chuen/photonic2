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
    public partial class UserCtrl_Indexer_Teach : UserControl
    {
        //variables
        SeqMachineControl m_pSeqMachineControl = null;
		Indexer_APSlib_PanasonicECAT m_RobotIndexer = null;
        Data_Indexer m_IndexerData = null;

        int m_iHome = 0;
        int m_iAlarm = 0;
        int m_iMotionDone = 0;
        int m_iRLimit = 0;
        int m_iFLimit = 0;
        int m_iServoOn = 0;
		int m_iMoving = 0;
		int m_iEMG = 0;
        int m_iFlag_UnitMeasurement = 1;                // unit measure. 1 = mm , 2 = deg

		private readonly object m_lock_Access = new object();		//prevent collision when same input is calling
        bool m_bFlag_Access = false;
        public bool Flag_Access_b
        {
            set { lock (m_lock_Access) { m_bFlag_Access = value; } }
            get { lock (m_lock_Access) { return m_bFlag_Access; } }
        }

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Indexer_Teach(Indexer_APSlib_PanasonicECAT mRobotIndexer)
        {
            InitializeComponent();

            //GT_App_MainApp pApp = GT_App.Program.m_MainApp;
            //m_pSeqMachineCtrl = pApp.m_SeqMachineCtrl;
            m_pSeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			//m_DataIndexer = m_pSeqMachineCtrl.m_DataPackageFile.m_DataIndexer;
            m_RobotIndexer = mRobotIndexer;

        }


        //Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_Indexer_Teach_Load(object sender, EventArgs e)
        {
            //Indexer Name
            txtb_ModuleName.Text = m_RobotIndexer.m_strLabel;

            //Update List box
            int iSettingsCount = m_IndexerData.Get_Settings_Count();
            for (int iCount = 0; iCount < iSettingsCount; iCount++)
            {
                String[] ar = new string[2];
                ListViewItem lt;
                ar[0] = m_IndexerData.Get_SettingsName(iCount);
                ar[1] = m_IndexerData.Get_SettingsDesc(iCount);
                lt = new ListViewItem(ar);

                this.listView_Positions.Items.Add(lt);
            }

            //Servo button text
            //Update Button
            if (m_RobotIndexer.Svon_i == 1)
            {
                btn_ServoOnOff.Text = "Servo is On";
                Update_PositionBoxes();
            }
            else btn_ServoOnOff.Text = "Servo is Off";

			//Set
			lbl_AxisID.Text = m_RobotIndexer.m_iAxis_ID.ToString();
        }

        //Set Indexer Class obj////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_RobotIndexer_Ptr( Indexer_APSlib_PanasonicECAT mIndexer ) { m_RobotIndexer = mIndexer; }
        //Set Indexer Data Class obj///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_DataIndexer_Ptr(Data_Indexer mIndexerData) { m_IndexerData = mIndexerData; }
        //Set unit text////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Set the measurement label text as mm or deg
        /// </summary>
        /// <param name="iSelect">1 = mm, 2 = deg</param>
        public void Set_UnitMeasurement(int iSelect)
        {
            if (iSelect == 2) lbl_Unit001.Text = lbl_Unit002.Text = lbl_Unit003.Text = "deg";
            m_iFlag_UnitMeasurement = iSelect;
        }

        //Set UI////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_ModuleName(String strName) { txtb_ModuleName.Text = strName; }
        public void Set_PositionName(String strPos) { txtb_TeachPos.Text = strPos; }
        public void Set_CurrentPos(String strPos) { txtb_CurrentPos.Text = strPos; }

        //Update UI based on timer. Created via forms.timer/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_PositionBoxes()
        {
            //lock ( m_lock_Access )
            //{
            //    //get motor status
            //    if ( m_RobotIndexer.core_GetMotorStatus() != 1 ) return;

            //    //get stepper status bits and update UI
            //    Update_PicBox(m_RobotIndexer.Alarm_i, ref m_iAlarm, picb_Alarm);
            //    Update_PicBox(m_RobotIndexer.EL_posivite_i, ref m_iFLimit, picb_FLimit);
            //    Update_PicBox(m_RobotIndexer.Home_i, ref m_iHome, picb_Home);
            //    Update_PicBox(m_RobotIndexer.MoveDone_i, ref m_iMotionDone, picb_InPos);
            //    Update_PicBox(m_RobotIndexer.EL_negative_i, ref m_iRLimit, picb_RLimit);
            //    Update_PicBox(m_RobotIndexer.Svon_i, ref m_iServoOn, picb_Servo);
            //    Update_PicBox(m_RobotIndexer.Moving_i, ref m_iMoving, pic_Moving);
            //    Update_PicBox(m_RobotIndexer.EMG_i, ref m_iEMG, pic_EMG);

            //    //Update Current Position     
            //    double dbPosition = 0;
            //    if ( m_RobotIndexer.Get_MotorPosition(ref dbPosition) == false )
            //        txtb_CurrentPos.Text = m_RobotIndexer.Get_ErrorID().ToString();
            //    else
            //        txtb_CurrentPos.Text = dbPosition.ToString();
            //}

            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();
            bool bRet = true;

            lock (m_lock_Access)
            {
                mPosition = m_RobotIndexer.GetRobotPosition_mm(ref bRet);
                if (bRet == false) return;

                //update current positio boxes
                txtb_CurrentPos.Text = mPosition.m_dbPos.ToString();

                if (m_RobotIndexer.GetRobotStatus() < 0) return;

                //Update Pic boxes
                Update_PicBox(picb_Alarm, m_RobotIndexer.Alarm_i);

                Update_PicBox(picb_Home, m_RobotIndexer.Home_i);

                Update_PicBox(picb_InPos, m_RobotIndexer.MoveDone_i);

                Update_PicBox(picb_Servo, m_RobotIndexer.Svon_i);

                Update_PicBox(picb_EMG, m_RobotIndexer.EMG_i);

                Update_PicBox(picb_FLimit, m_RobotIndexer.EL_posivite_i);

                Update_PicBox(picb_RLimit, m_RobotIndexer.EL_negative_i);

                Update_PicBox(picb_Moving, m_RobotIndexer.Moving_i);
            }
        }

        //class helper//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void WriteToUserActivityLog(String strLog) { /*m_pSeqMachineCtrl.Write_ToUserActivityLog("Indexer Teach: " + m_RobotIndexer.m_strLabel + ": " + strLog);*/ }

		//Helper Functions
        //update pic box common function///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note: iStatusVar1 = RobotStation Status Variable
        //private void Update_PicBox(int iStatusVar1, ref int iStatusVar2, PictureBox pBox)
        //{
        //    if (iStatusVar1 == iStatusVar2) return;

        //    if (iStatusVar1 == 1) pBox.BackColor = Color.Green;
        //    else pBox.BackColor = Color.Red;

        //    iStatusVar2 = iStatusVar1;
        //}

        private void Update_PicBox(PictureBox obj, int bResult)
        {
            if (bResult == 1)
                obj.BackColor = Color.Green;
            else
                obj.BackColor = Color.Red;
        }


		//Determine if the jog resolutions are all numeric/////////////////////////////////////////////////////////////////////////////////////////////////////
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

		

		//user select from list box////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //private void list_Positions_MouseDown(object sender, MouseEventArgs e)
        //{
        //    String strPos = "";
        //    Indexer_Pos_Settings mSettings;

        //    //check
        //    if (listView_Positions.SelectedIndices.Count == -1) return;

        //    //get from list
        //    strPos = this.listView_Positions.SelectedItems[0].SubItems[0].Text;

        //    //get position
        //    mSettings = m_IndexerData.Get_Settings(strPos);
            
        //    this.Set_PositionName(mSettings.m_dbPos.ToString());

        //    if ( mSettings.m_bMoveAbs ) lbl_MoveAbs.Text = "Absolute";
        //    else lbl_MoveAbs.Text = "Relative";
        //}

        private void listView_Positions_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strPos = "";
            Indexer_Pos_Settings mPosition;

            //get selected item from list
            if (listView_Positions.SelectedIndices.Count != 1) return;
            strPos = listView_Positions.SelectedItems[0].SubItems[0].Text;

            //get position from data robot
            mPosition = m_IndexerData.Get_Settings(strPos);

            //update text box
            txtb_TeachPos.Text = mPosition.m_dbPos.ToString();

            trackBar_PrimarySpeed.Value = mPosition.m_iRunSpeed;
        }


		//button events//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Init_Click(object sender, EventArgs e)
        {
            Thread mInitThread = new Thread(HomingThread);
            mInitThread.Start();

            //record activity
            //WriteToUserActivityLog("Initialize: ");
        }

        //Extra Threads////////////////////////////////////////////////////////////////////////////////////////
        public void Run_Thread(Indexer_Pos_Settings mPosition)
        {
            Thread mTh = new Thread(() => Thread_MoveToPosition(mPosition));
            mTh.Start();
        }

        private void HomingThread()
        {
            //lock ( m_lock_Access )
            //{
				if ( m_RobotIndexer.Homing() == false )
					Display_ErrorMessageBox("Homing / Initialize Failed: \r\n" + m_pSeqMachineControl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
				else
				{
					MessageBox.Show("Initialization Complete");
				}
            //}
        }

        //Move to position thread
        public void Thread_MoveToPosition(Indexer_Pos_Settings mPosition)
        {
            if (m_RobotIndexer.Move_Abs(mPosition) == false) { Display_ErrorMessageBox("Go to Position: " + mPosition.m_strDescription); return; }
            else
            {
                Class_Helper.Display_NoticeMessageBox("Movement Done.");
            }
        }

        private void btn_StopMotor_Click(object sender, EventArgs e)
        {
            //lock ( m_lock_Access )
            //{
                //if ( m_RobotIndexer.Stop_Move() == false ) Display_ErrorMessageBox("Stop Motor Failed \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            if (m_RobotIndexer.Stop_Move() == false) Display_ErrorMessageBox("Stop Motor Failed");
            //}
		}

        private void btn_ServoOnOff_Click(object sender, EventArgs e)
        {
            bool bRet = true;
            String strAdd = "";

            lock (m_lock_Access)
            {
                if (m_RobotIndexer.Svon_i == 1)
                {
                    bRet = m_RobotIndexer.Servo_On(false);
                    strAdd = "Servo OFF";
                }
                else
                {
                    bRet = m_RobotIndexer.Servo_On(true);
                    strAdd = "Servo ON";
                }

                //Get State
                if (m_RobotIndexer.Svon_i == 1)
                {
                    btn_ServoOnOff.Text = "Servo is ON";
                }
                else
                {
                    btn_ServoOnOff.Text = "Servo is OFF";
                }
            }

            //if ( bRet == false ) Class_Helper.Display_ErrorMessageBox("Servo On Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));

            if (bRet == false) Display_ErrorMessageBox(strAdd);

            //record activity
            //WriteToUserActivityLog(" " + btn_ServoOnOff.Text);
        }

        private void btn_ResetAlarm_Click(object sender, EventArgs e)
        {
            //lock ( m_lock_Access )
            //{
            //if (m_RobotIndexer.core_ResetAlarm() == false) Display_ErrorMessageBox("Reset Alarm");
            if (m_RobotIndexer.Reset_Alarm() == false) Display_ErrorMessageBox("Reset Alarm");
                //Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            //}
        }

        public void btn_JogNegative_Click(object sender, EventArgs e)
        {
            //double dbPos_mm = double.Parse(txtb_JogResolution.Text) * (-1);

            ////record activity
            //WriteToUserActivityLog("Jogging to the left:  " + txtb_JogResolution.Text);

            //lock ( m_lock_Access )
            //{
            //    if ( m_RobotIndexer.core_MoveRel(dbPos_mm, 50) == false )
            //        Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            //}

            Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
            mPos.m_bMoveAbs = true;
            mPos.m_iRunSpeed = 50;
            mPos.m_strPosName = "jog";
            mPos.m_dbPos = double.Parse(txtb_JogResolution.Text) * (-1);

            if (m_RobotIndexer.Move_Rel(mPos) == false)
            {
                //m_RobotIndexer.Get_ErrorID();
                //m_RobotIndexer.Get_ErrorMessage();
                m_RobotIndexer.Set_Error(txtb_ModuleName + " Indexer");
                Display_ErrorMessageBox("Jogging Negative");
            }
        }

        public void btn_JogPositive_Click(object sender, EventArgs e)
        {
            //double dbPos_mm = double.Parse(txtb_JogResolution.Text);

            ////record activity
            //WriteToUserActivityLog("Jogging to the right:  " + txtb_JogResolution.Text);

            //lock ( m_lock_Access )
            //{
            //    if ( m_RobotIndexer.core_MoveRel(dbPos_mm, 50) == false )
            //        Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            //}

            Indexer_Pos_Settings mPos = new Indexer_Pos_Settings();
            mPos.m_bMoveAbs = true;
            mPos.m_iRunSpeed = 50;
            mPos.m_strPosName = "jog";
            mPos.m_dbPos = double.Parse(txtb_JogResolution.Text);

            if (m_RobotIndexer.Move_Rel(mPos) == false)
            {
                //m_RobotIndexer.Get_ErrorID();
                //m_RobotIndexer.Get_ErrorMessage();
                m_RobotIndexer.Set_Error(txtb_ModuleName + " Indexer");
                Display_ErrorMessageBox("Jogging Positive");
            }
        }

        private void Display_ErrorMessageBox(String strAdditional)
        {
            Class_Helper.Display_ErrorMessageBox(m_pSeqMachineControl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID(), strAdditional, m_RobotIndexer.Get_ErrorMessage()));
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            //Indexer_Pos_Settings mSettings;
            //String strPos = "";

            ////Check if the list is selected
            //if (this.listView_Positions.SelectedIndices.Count == -1)
            //{
            //    MessageBox.Show("Please select a position from the list.");
            //    return;
            //}

            ////get position string
            //strPos = this.listView_Positions.SelectedItems[0].SubItems[0].Text;

            ////Checking
            //if (strPos == "")
            //{
            //    MessageBox.Show("Please select a position from the list");
            //    return;
            //}

            ////Get Settings
            //mSettings = m_IndexerData.Get_Settings(strPos);

            ////Update position
            //mSettings.m_strPosName = strPos;
            //mSettings.m_dbPos = double.Parse(txtb_CurrentPos.Text);

            ////Update Settings
            //m_IndexerData.Update_Settings(mSettings);

            ////update package file
            //m_pSeqMachineControl.Pkg_To_Registry();

            ////record activity
            ////WriteToUserActivityLog("Update Settings");

            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();
            Indexer_Pos_Settings mPosData = new Indexer_Pos_Settings();
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
            mPosition = m_RobotIndexer.GetRobotPosition_mm(ref bRet);
            if (bRet == false) { Display_ErrorMessageBox("Update Setting: Get Robot position"); return; }

            //get from pkg file
            mPosData = m_IndexerData.Get_Settings(strPos);

            //Set position to pkg file
            mPosData.m_dbPos = mPosition.m_dbPos;
            mPosData.m_iRunSpeed = trackBar_PrimarySpeed.Value;

            //Update Settings
            m_IndexerData.Update_Settings(mPosData);

            //Update pkg file
            m_pSeqMachineControl.Pkg_To_Registry();
        }

        private void txtb_JogResolution_TextChanged(object sender, EventArgs e)
        {
            this.Detect_Num(txtb_JogResolution);
        }

        private void btn_Settings_Click(object sender, EventArgs e)
        {
            //Indexer_SettingPage dlg = new Indexer_SettingPage();
            //dlg.Set_SettingsPtr(this.m_RobotIndexer.m_Settings);
            //dlg.Set_UnitMeasurement(m_iFlag_UnitMeasurement);
            //dlg.Set_Label(this.m_RobotIndexer.m_strLabel);

            ////Save and set settings
            //if ( dlg.ShowDialog() == DialogResult.OK ) m_RobotIndexer.Update_ToRobot();

            //dlg.Dispose();

            Indexer_SettingPage dlg = new Indexer_SettingPage();
            dlg.Set_RobotIndexer_Ptr(this.m_RobotIndexer);

            if (dlg.ShowDialog() == DialogResult.OK)
                if (!m_RobotIndexer.To_Registry()) Display_ErrorMessageBox("");

            dlg.Dispose();
        }

        private void btn_MoveAbs_Click(object sender, EventArgs e)
        {
            //Indexer_Pos_Settings pos = new Indexer_Pos_Settings();
            //pos.m_dbPos = double.Parse(txtb_TeachPos.Text);
            //pos.m_iRunSpeed = trackBar_Speed.Value;

            ////record activity
            //WriteToUserActivityLog("Move Absolute: Move to Position: " + txtb_TeachPos.Text);

            //lock ( m_lock_Access )
            //{
            //    //Check if it is relative or absolute
            //    if ( lbl_MoveAbs.Text == "Relative" )
            //    {
            //        if ( !m_RobotIndexer.core_MoveAbs(pos.m_dbPos, pos.m_iRunSpeed) )
            //            Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            //    }
            //    else
            //    {
            //        if ( !m_RobotIndexer.core_MoveRel(pos.m_dbPos, pos.m_iRunSpeed) )
            //            Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineCtrl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
            //    }
            //}

            Indexer_Pos_Settings mPosition = new Indexer_Pos_Settings();

            //Check if any item in the list is selected
            if (listView_Positions.SelectedIndices.Count != 1)
            {
                Class_Helper.Display_ErrorMessageBox("Please select a position from the list.");
                return;
            }

            mPosition.m_strPosName = listView_Positions.SelectedItems[0].SubItems[0].Text;      //position at robot controller

            //Get Position
            mPosition = m_IndexerData.Get_Settings(mPosition.m_strPosName);
            mPosition.m_iRunSpeed = trackBar_PrimarySpeed.Value;
            //MessageBox.Show(mPosition.m_iRunSpeed.ToString());
            //move robot via thread
            Run_Thread(mPosition);

            //Set listview 
            listView_Positions.Select();
        }

        private void btn_SetHome_Click(object sender, EventArgs e)
        {
			lock ( m_lock_Access )
			{
				if ( m_RobotIndexer.Set_MotorPosition(0) == false )
					Class_Helper.Display_ErrorMessageBox("Homing Failed: \r\n" + m_pSeqMachineControl.Get_ErrorMessage(m_RobotIndexer.Get_ErrorID()));
			}
        }        
    }
}
