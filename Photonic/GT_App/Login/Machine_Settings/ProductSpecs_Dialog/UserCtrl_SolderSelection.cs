using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class UserCtrl_SolderSelection : UserControl
    {
        // variable
        SeqMachineControl m_pSeqMachineCtrl = null;
        Data_UnitTempStorage m_DataUnit;
        Data_PackageFile m_DataPackage;
        Data_Common_Stat m_pDataCommonStat;

        public UserCtrl_SolderSelection(Data_Common_Stat pDataCommonStat)
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
            m_DataPackage = m_pSeqMachineCtrl.m_DataPackageFile;
            m_pDataCommonStat = pDataCommonStat;
        }

        //Class helper
        //Update lable///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Label(Label obj, String strTitle, bool bDoubleSelect = false)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            if (bDoubleSelect == false) 
                dlg.Set_Integer();
            else 
                dlg.Set_Double();
            dlg.Set_Title(strTitle);
            dlg.Set_Label(strTitle);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
                obj.Text = dlg.Get_DataString();
        }

        //Dialog loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_OffsetSetting_Teach_Load(object sender, EventArgs e)
        {
            Update_ButtonEnable(btn_SolderPoint1, m_pDataCommonStat.m_bSolder_Point_1);
            Update_ButtonEnable(btn_SolderPoint2, m_pDataCommonStat.m_bSolder_Point_2);
            Update_ButtonEnable(btn_SolderPoint3, m_pDataCommonStat.m_bSolder_Point_3);
            Update_ButtonEnable(btn_SolderPoint4, m_pDataCommonStat.m_bSolder_Point_4);
            Update_ButtonEnable(btn_SolderPoint5, m_pDataCommonStat.m_bSolder_Point_5);
            Update_ButtonEnable(btn_SolderPoint6, m_pDataCommonStat.m_bSolder_Point_6);
            
        }

        
		//Get Settings////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Get_Settings()
        {
            //enable selection
            m_pDataCommonStat.m_bSolder_Point_1 = Get_Enable(btn_SolderPoint1);
            m_pDataCommonStat.m_bSolder_Point_2 = Get_Enable(btn_SolderPoint2);
            m_pDataCommonStat.m_bSolder_Point_3 = Get_Enable(btn_SolderPoint3);
            m_pDataCommonStat.m_bSolder_Point_4 = Get_Enable(btn_SolderPoint4);
            m_pDataCommonStat.m_bSolder_Point_5 = Get_Enable(btn_SolderPoint5);
            m_pDataCommonStat.m_bSolder_Point_6 = Get_Enable(btn_SolderPoint6);
        }
      

        //Update offset setting//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Update_Click(object sender, EventArgs e)
        {
            //obtain settings from dialog
            Get_Settings();

            //update package file
            m_pSeqMachineCtrl.Pkg_To_Registry();


        }


        //Update Button Enable//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_ButtonEnable(Button obj, bool bEnable)
        {
            //remove all enabled and disabled text and trim all leading and ending white spaces
            String strText = obj.Text.Replace("Enabled", "").Replace("Disabled", "").Trim();

            if (bEnable)
            {
                obj.Text = strText + " Enabled";
                obj.BackColor = Color.LimeGreen;
            }
            else
            {
                obj.Text = strText + " Disabled";
                obj.BackColor = Color.Red;
            }
        }

        //Get Enable or Disable//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private bool Get_EnableInverse(Button obj)
        {
            return (obj.Text.Contains("Enabled") == true) ? false : true;
        }
        private bool Get_Enable(Button obj)
        {
            return (obj.Text.Contains("Enabled") == true) ? true : false;
        }


    }



}
