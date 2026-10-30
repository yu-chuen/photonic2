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

        List<UserCtrl_DataCommonStat_Dlg> m_list_userctrl_DataCommonStat = new List<UserCtrl_DataCommonStat_Dlg>();
        List<UserCtrl_WireTesterSpecs_Dlg> m_list_userctrl_DataCommon = new List<UserCtrl_WireTesterSpecs_Dlg>();
        List<UserCtrl_CommonData> m_list_userctrl_CommonData = new List<UserCtrl_CommonData>();
        List<UserCtrl_HeightCheck> m_list_userctrl_HeightCheck = new List<UserCtrl_HeightCheck>();
        Data_PackageFile m_DataPackage;


        //UserCtrl_DataCommonStat_Dlg m_userctrl_DataCommonStat1;
        //UserCtrl_DataCommonStat_Dlg m_userctrl_DataCommonStat2;
        //UserCtrl_DataCommonStat_Dlg m_userctrl_DataCommonStat3;
        
        UserCtrl_WireTesterSpecs_Dlg m_userctrl_WireTesterSpecs;
        UserCtrl_CommonData m_userctrl_CommonData;

        List<TabPage> m_list_TabPageDataCommonStat = new List<TabPage>();

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

            if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat3, "Solder Program OP80");
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat2, "Solder Program OP70");
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat1, "Solder Program OP60");
            }
            else
            {
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat1, "Solder Program OP60");
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat2, "Solder Program OP70");
                Add_UserCtrl(m_DataPackage.m_DataCommon_Stat3, "Solder Program OP80");
            }

            Add_UserCtrl_WireTester();
            Add_UserCtrl_CommonData();
            Add_UserCtrl_HeightCheck();
           // m_userctrl_WireTesterSpecs = new UserCtrl_WireTesterSpecs_Dlg(m_DataPackage.m_DataCommon);
            //m_userctrl_CommonData = new UserCtrl_CommonData();


        }

        private void btn_UpdateEsc_Click(object sender, EventArgs e)
        {
            //m_userctrl_DataCommonStat1.Get_Settings();
            //m_userctrl_DataCommonStat2.Get_Settings();
            //m_userctrl_DataCommonStat3.Get_Settings();
            
            m_userctrl_WireTesterSpecs.UpdateSettings();
            m_userctrl_CommonData.Get_Settings();

			m_SeqMachineCtrl.Pkg_To_Registry();

            this.Close();
        }
        //Add user ctrl//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //solder station-----------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl(Data_Common_Stat mRobotStation, String solderStat)
        {
            //add user ctrl
            //for ( int iCount = 0; iCount < 3; iCount++ )
            //{
            //    switch ( iCount )
            //    {
                    //case 0:
                    UserCtrl_DataCommonStat_Dlg mUserCtrl1 = new UserCtrl_DataCommonStat_Dlg(mRobotStation, solderStat);
                    m_list_userctrl_DataCommonStat.Add(mUserCtrl1);

                    //Add Tab page
                    TabPage mTabPage = new TabPage();
                    mTabPage.Controls.Add(m_list_userctrl_DataCommonStat[m_list_userctrl_DataCommonStat.Count - 1]);
                    mTabPage.Text = solderStat;
                    mTabPage.AutoScroll = true;

                    //set to main ui
                    m_ll_TabPage.Add(mTabPage);
                    this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);
            
                    //break;
            //    }
            //}
        }


        //wire tester------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_WireTester()
        {
            //add user ctrl
            UserCtrl_WireTesterSpecs_Dlg mUserCtrl_WireTester = new UserCtrl_WireTesterSpecs_Dlg();
            m_list_userctrl_DataCommon.Add(mUserCtrl_WireTester);

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_list_userctrl_DataCommon[m_list_userctrl_DataCommon.Count - 1]);
            mTabPage.Text = "Wire Tester";

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);

        }

        //hot blower-----------------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_CommonData()
        {
            //add user ctrl
            UserCtrl_CommonData mUserCtrl_CommonData = new UserCtrl_CommonData();
            m_list_userctrl_CommonData.Add(mUserCtrl_CommonData);

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_list_userctrl_CommonData[m_list_userctrl_CommonData.Count - 1]);
            mTabPage.Text = "Hot Blower";

            //set to main ui
            m_ll_TabPage.Add(mTabPage);
            this.tabControl1.TabPages.Add(m_ll_TabPage[m_ll_TabPage.Count - 1]);

        }

        //Height check-----------------------------------------------------------------------------------------------------------------
        public void Add_UserCtrl_HeightCheck()
        {
            //add user ctrl
            UserCtrl_HeightCheck mUserCtrl_HeightCheck = new UserCtrl_HeightCheck();
            m_list_userctrl_HeightCheck.Add(mUserCtrl_HeightCheck);

            //Add Tab page
            TabPage mTabPage = new TabPage();
            mTabPage.Controls.Add(m_list_userctrl_HeightCheck[m_list_userctrl_HeightCheck.Count - 1]);
            mTabPage.Text = "Height Check";

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

        public void Add_UserCtrl()
        {
            Module_SolderingStation m_pModuleSolderStation = null;
            int iCountDisplay = 0;
            String strStationName = "";

            //add SolderStation1--------------------------------------------------------------------------
            for (int iCount = 0; iCount < 3; iCount++)
            {
                switch (iCount)
                {
                    case 0:
                        m_pModuleSolderStation = m_SeqMachineCtrl.m_Module_SolderStation03;
                        iCountDisplay = 3;
                        strStationName = "OP80";
                        break;

                    case 1:
                        m_pModuleSolderStation = m_SeqMachineCtrl.m_Module_SolderStation02;
                        iCountDisplay = 2;
                        strStationName = "OP70";
                        break;

                    case 2:
                        m_pModuleSolderStation = m_SeqMachineCtrl.m_Module_SolderStation01;
                        iCountDisplay = 1;
                        strStationName = "OP60";
                        break;
                }
            }
        }


    }
}