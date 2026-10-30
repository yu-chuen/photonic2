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
    public partial class Indexer_SettingPage : Form
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl = null;
        Indexer_APSlib_PanasonicECAT m_RobotIndexer = null;

		UserCtrl_Indexer_SettingPage m_userctrl_SettingPage = null;

        Stepper_Settings m_Settings = null;

		int m_iUnitMeasurement = 1;					//set unit measurement
		String m_strLabel = "";

        //Constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Indexer_SettingPage()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Set indexer pointer//////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_SettingsPtr( Stepper_Settings mSettings ) { m_Settings = mSettings; }
		public void Set_Label( String strLabel ) { m_strLabel = strLabel; }

        //Set Robot Station//////////////////////////////////////////////////////////////////////////////////////
        public void Set_RobotIndexer_Ptr(Indexer_APSlib_PanasonicECAT mRobotIndexer) { m_RobotIndexer = mRobotIndexer; }

        //Dialog load/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Indexer_SettingPage_Load(object sender, EventArgs e)
        {
			//Create user ctrl
			m_userctrl_SettingPage = new UserCtrl_Indexer_SettingPage();
			m_userctrl_SettingPage.Set_SettingsPtr(m_RobotIndexer.m_Settings);
			//m_userctrl_SettingPage.Set_UnitMeasurement(m_iUnitMeasurement);
			m_userctrl_SettingPage.Set_Label(m_RobotIndexer.m_strLabel);
			this.Controls.Add(m_userctrl_SettingPage);

            //this.Size = new Size(m_userctrl_SettingPage.Width + 150, m_userctrl_SettingPage.Height + 55);
        }

        //buttons///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_UpdateEsc_Click(object sender, EventArgs e)
        {
            m_RobotIndexer.m_Settings = m_userctrl_SettingPage.Get_Settings();

            //this.Update_Registry();

            //return OK
			this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
			this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        //get data from UI and update registry/////////////////////////////////////////////////////////////////////////////
        public void Update_Registry()
        {
			m_Settings = m_userctrl_SettingPage.Get_Settings();
        }

		//Set Unit Measurement//////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Set the measurement label text as mm or deg
		/// </summary>
		/// <param name="iSelect">1 = mm, 2 = deg</param>
		public void Set_UnitMeasurement( int iSelect ) { m_iUnitMeasurement = iSelect; }


    }
}
