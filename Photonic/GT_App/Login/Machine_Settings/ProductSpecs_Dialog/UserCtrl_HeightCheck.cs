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
    public partial class UserCtrl_HeightCheck: UserControl
    {
        //variables
        SeqMachineControl m_pSeqMachineCtrl;
        Data_Common m_pDataCommon;

        //constructor/////////////////////////////////////////////////////////////////////
        public UserCtrl_HeightCheck()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_pDataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////
        private void UserCtrl_CommonData_Load(object sender, EventArgs e)
        {
            //Height Check
            lbl_Point1_Min.Text = m_pDataCommon.m_db_Point1_Min.ToString();
            lbl_Point2_Min.Text = m_pDataCommon.m_db_Point2_Min.ToString();

            lbl_Point1_Max.Text = m_pDataCommon.m_db_Point1_Max.ToString();
            lbl_Point2_Max.Text = m_pDataCommon.m_db_Point2_Max.ToString();
        }

        //Get Settings///////////////////////////////////////////////////////////////////
        public void Get_Settings()
        {
            //Height Check
            m_pDataCommon.m_db_Point1_Min = double.Parse(lbl_Point1_Min.Text);
            m_pDataCommon.m_db_Point2_Min = double.Parse(lbl_Point2_Min.Text);

            m_pDataCommon.m_db_Point1_Max = double.Parse(lbl_Point1_Max.Text);
            m_pDataCommon.m_db_Point2_Max = double.Parse(lbl_Point2_Max.Text);

            //save to pkg file
            m_pSeqMachineCtrl.MachineInfo_To_Registry();
            m_pSeqMachineCtrl.Pkg_To_Registry();
        }


        //Class Helper////////////////////////////////////////////////////////////////////
        //Update lable///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_Label(Label obj, String strTitle)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Title(strTitle);
            dlg.Set_Label(strTitle);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
                obj.Text = dlg.Get_DataString();
        }

        //Class helper/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Display_InputBox(Label obj, String strLabel, int iSelection = 1)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();

            switch (iSelection)
            {
                default: dlg.Set_Integer(); break;
                case 2: dlg.Set_Double(); break;
            }

            dlg.Set_Label(strLabel);
            dlg.Set_Title(strLabel);
            dlg.Set_InputBoxValue(obj.Text);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) obj.Text = dlg.Get_DataString();
            dlg.Dispose();
        }

        //Button Events///////////////////////////////////////////////////////////////////
        private void btn_Point1_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Point1_Min, btn_Point1_Min.Text, 2); Get_Settings(); }
        private void btn_Point2_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Point2_Min, btn_Point2_Min.Text, 2); Get_Settings(); }

        private void btn_Point1_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Point1_Max, btn_Point1_Max.Text, 2); Get_Settings(); }
        private void btn_Point2_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Point2_Max, btn_Point2_Max.Text, 2); Get_Settings(); }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            Get_Settings();
        }
    }
}
