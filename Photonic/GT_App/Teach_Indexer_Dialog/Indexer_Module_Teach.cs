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
    public partial class Indexer_Module_Teach : Form
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl = null;
        List<UserCtrl_Indexer_Teach> m_ll_userctrl_IndexerTeach_Dlg = new List<UserCtrl_Indexer_Teach>();
        List<TabPage> m_ll_TabPage = new List<TabPage>();

        //tab selection for UI Update
        private readonly object m_lock_SelectTab = new object();
        int m_iSelectedTab = 0;
        int SelectedTab_i
        {
            set { lock (m_lock_SelectTab) { m_iSelectedTab = value; } }
            get { lock (m_lock_SelectTab) { return m_iSelectedTab; } }
        }

        //key press
        private readonly object m_lock_FlagEnableKeyPress = new object();
        bool m_bFlagEnableKeyPress = false;
        bool FlagEnableKeyPress_b { set { lock (m_lock_FlagEnableKeyPress) { m_bFlagEnableKeyPress = value; } } get { lock (m_lock_FlagEnableKeyPress) { return m_bFlagEnableKeyPress; } } }

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Indexer_Module_Teach()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Destructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        ~Indexer_Module_Teach()
		{
			m_Timer.Stop();
		}

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Indexer_Module_Teach_Load(object sender, EventArgs e)
        {
            //Add user control
            //Add_UserCtrl(m_pSeqMachineCtrl.m_IndexerShutter_A, m_pSeqMachineCtrl.m_DataPackageFile.m_DataIndexer_ShA);
            //Add_UserCtrl(m_pSeqMachineCtrl.m_IndexerShutter_B, m_pSeqMachineCtrl.m_DataPackageFile.m_DataIndexer_ShB);
            
            //Start Timer
            m_Timer.Start();
        }

        //Add user control/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrl(Indexer_APSlib_PanasonicECAT mIndexer, Data_Indexer mDataIndexer)
        {
            //add user ctrl
            UserCtrl_Indexer_Teach mUserCtrl = new UserCtrl_Indexer_Teach(mIndexer);
            mUserCtrl.Set_RobotIndexer_Ptr(mIndexer);
            mUserCtrl.Set_DataIndexer_Ptr(mDataIndexer);
            m_ll_userctrl_IndexerTeach_Dlg.Add(mUserCtrl);

            //add tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_ll_userctrl_IndexerTeach_Dlg[m_ll_userctrl_IndexerTeach_Dlg.Count - 1]);
            mTabPage.Text = mIndexer.m_strLabel;
            m_ll_TabPage.Add(mTabPage);

            //set to main UI
            this.tabctrl_IndexerPg.TabPages.Add(mTabPage);
        }

		//Closing the form/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Indexer_Module_Teach_FormClosing(object sender, FormClosingEventArgs e)
        {
			m_Timer.Stop();

            //dispose userctrl
            for (int iCount = 0; iCount < m_ll_userctrl_IndexerTeach_Dlg.Count; iCount++)
                m_ll_userctrl_IndexerTeach_Dlg[iCount].Dispose();
        }

        //Tab change/////////////////////////////////////////////////////////////////////////////////////////////////
        private void tabctrl_IndexerPg_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTab_i = tabctrl_IndexerPg.SelectedIndex;
        }

        //Button ESC Pressed//////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		//Timer Event/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void OnTimer( object sender, EventArgs e )
		{
			m_Timer.Stop();

            if (m_ll_userctrl_IndexerTeach_Dlg.Count > 0)
                m_ll_userctrl_IndexerTeach_Dlg[SelectedTab_i].Update_PositionBoxes();

			m_Timer.Start();
		}


        //Key Press/////////////////////////////////////////////////////////////////////////////////////////////////
        private void Indexer_Module_Teach_KeyDown(object sender, KeyEventArgs e)
        {
            //blocker
            if (FlagEnableKeyPress_b == false) return;
            
            //Key press selection
            if (e.KeyCode == Keys.Left)
            {
                m_ll_userctrl_IndexerTeach_Dlg[SelectedTab_i].btn_JogPositive_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
            else if (e.KeyCode == Keys.Right)
            {
                m_ll_userctrl_IndexerTeach_Dlg[SelectedTab_i].btn_JogNegative_Click(null, EventArgs.Empty);
                e.Handled = true;
            }
        }

        //Button: Enable / Disable key press///////////////////////////////////////////////////////////////////
        private void btn_KeyPressEnable_Click(object sender, EventArgs e)
        {
            //selection
            FlagEnableKeyPress_b = (FlagEnableKeyPress_b == true) ? false : true;

            //display button
            if (FlagEnableKeyPress_b)
            {
                btn_KeyPressEnable.BackColor = Color.Green;
                btn_KeyPressEnable.Text = "Key Press Enabled";
            }
            else
            {
                btn_KeyPressEnable.BackColor = Color.Red;
                btn_KeyPressEnable.Text = "Key Press Disabled";
            }
        }




    }
}
