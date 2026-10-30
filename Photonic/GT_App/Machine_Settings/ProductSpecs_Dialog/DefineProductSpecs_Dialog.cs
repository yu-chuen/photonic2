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
    public partial class DefineProductSpecs_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Data_PackageFile m_DataPackage;

        List<UserCtrl_OP60_OP70_OP80_SolderStation_Settings_Dlg> m_list_userctrl_OP60_OP70_OP80_SolderStation_Settings = new List<UserCtrl_OP60_OP70_OP80_SolderStation_Settings_Dlg>();
        List<TabPage> m_ll_TabPage = new List<TabPage>();

        private readonly object m_lock_SelectTab = new object();
        int m_iSelectedTab = 0;
        int SelectedTab_i
        {
            set { lock (m_lock_SelectTab) { m_iSelectedTab = value; } }
            get { lock (m_lock_SelectTab) { return m_iSelectedTab; } }
        }

        //constructor/////////////////////////////////////////////////////////////////////////
        public DefineProductSpecs_Dialog()
        {
            InitializeComponent();

            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataPackage = m_SeqMachineCtrl.m_DataPackageFile;
        }

        //Dialog Loaded/////////////////////////////////////////////////////////////////
        private void DefineProductSpecs_Dialog_Load(object sender, EventArgs e)
        {
            Add_UserCtrl_Common_Settings();
            Add_UserCtrl_OP85_Settings();
            Add_UserCtrl_OP90_Settings();
            Add_UserCtrl_OP110_HotBlower_Settings();
        }

        //wire tester------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_Common_Settings()
        {
            //add user ctrl
            UserCtrl_Common_Settings mUserCtrl = new UserCtrl_Common_Settings();

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(mUserCtrl);
            mTabPage.Text = "Common";
            mTabPage.AutoScroll = true;

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
        }

        //OP85 vision------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_OP85_Settings()
        {
            //add user ctrl
            UserCtrl_OP85_Settings mUserCtrl = new UserCtrl_OP85_Settings();

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(mUserCtrl);
            mTabPage.Text = "OP85 - Vision";
            mTabPage.AutoScroll = true;

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
        }

        //wire tester------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_OP90_Settings()
        {
            //add user ctrl
            UserCtrl_OP90_Settings mUserCtrl = new UserCtrl_OP90_Settings();

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(mUserCtrl);
            mTabPage.Text = "OP90 - Vision";
            mTabPage.AutoScroll = true;

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
        }

        //HotBlower-----------------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_OP110_HotBlower_Settings()
        {
            //add user ctrl
            UserCtrl_OP110_HotBlower_Settings mUserCtrl = new UserCtrl_OP110_HotBlower_Settings();

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(mUserCtrl);
            mTabPage.Text = "OP110 - Hot Blower";
            mTabPage.AutoScroll = true;

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
        }

        //Class helper/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Display_InputBox(Label obj, String strLabel)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Label(strLabel);
            dlg.Set_Title(strLabel);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) obj.Text = dlg.Get_DataString();
            dlg.Dispose();
        }

        //Button events////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        //Selected tab control page///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedTab_i = tabControl1.SelectedIndex;
        }
    }
}