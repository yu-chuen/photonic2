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
    public partial class UserCtrl_OP85_Settings : UserControl
    {
        //variables
        Data_Common m_DataCommon;
        Data_UnitTempStorage m_DataUnit;
        SeqMachineControl m_pSeqMachineCtrl;
        MachineInfo m_pMachineInfo;

        //constructor
        public UserCtrl_OP85_Settings()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
            m_pMachineInfo = m_pSeqMachineCtrl.m_MachineInfo;
        }

        //dialog loaded
        private void UserCtrl_OP85_Settings_Load(object sender, EventArgs e)
        {
            Load_Settings();
        }

        public void Load_Settings()
        {
            //spec
            lbl_Terminal1_Width_Spec.Text = m_DataCommon.m_db_Terminal1_Width_Spec.ToString();
            lbl_Terminal1_Length_Spec.Text = m_DataCommon.m_db_Terminal1_Length_Spec.ToString();
            lbl_Terminal1_Distance_Spec.Text = m_DataCommon.m_db_Terminal1_Distance_Spec.ToString();

            lbl_Terminal2_Width_Spec.Text = m_DataCommon.m_db_Terminal2_Width_Spec.ToString();
            lbl_Terminal2_Length_Spec.Text = m_DataCommon.m_db_Terminal2_Length_Spec.ToString();
            lbl_Terminal2_Distance_Spec.Text = m_DataCommon.m_db_Terminal2_Distance_Spec.ToString();

            lbl_Terminal3_Width_Spec.Text = m_DataCommon.m_db_Terminal3_Width_Spec.ToString();
            lbl_Terminal3_Length_Spec.Text = m_DataCommon.m_db_Terminal3_Length_Spec.ToString();
            lbl_Terminal3_Distance_Spec.Text = m_DataCommon.m_db_Terminal3_Distance_Spec.ToString();

            lbl_Terminal4_Width_Spec.Text = m_DataCommon.m_db_Terminal4_Width_Spec.ToString();
            lbl_Terminal4_Length_Spec.Text = m_DataCommon.m_db_Terminal4_Length_Spec.ToString();
            lbl_Terminal4_Distance_Spec.Text = m_DataCommon.m_db_Terminal4_Distance_Spec.ToString();

            lbl_Terminal5_Width_Spec.Text = m_DataCommon.m_db_Terminal5_Width_Spec.ToString();
            lbl_Terminal5_Length_Spec.Text = m_DataCommon.m_db_Terminal5_Length_Spec.ToString();
            lbl_Terminal5_Distance_Spec.Text = m_DataCommon.m_db_Terminal5_Distance_Spec.ToString();

            lbl_Terminal6_Width_Spec.Text = m_DataCommon.m_db_Terminal6_Width_Spec.ToString();
            lbl_Terminal6_Length_Spec.Text = m_DataCommon.m_db_Terminal6_Length_Spec.ToString();
            lbl_Terminal6_Distance_Spec.Text = m_DataCommon.m_db_Terminal6_Distance_Spec.ToString();

            //tolerance min
            lbl_Terminal1_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal1_Width_Tolerance_Min.ToString();
            lbl_Terminal1_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal1_Length_Tolerance_Min.ToString();
            lbl_Terminal1_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal1_Distance_Tolerance_Min.ToString();

            lbl_Terminal2_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal2_Width_Tolerance_Min.ToString();
            lbl_Terminal2_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal2_Length_Tolerance_Min.ToString();
            lbl_Terminal2_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal2_Distance_Tolerance_Min.ToString();

            lbl_Terminal3_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal3_Width_Tolerance_Min.ToString();
            lbl_Terminal3_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal3_Length_Tolerance_Min.ToString();
            lbl_Terminal3_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal3_Distance_Tolerance_Min.ToString();

            lbl_Terminal4_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal4_Width_Tolerance_Min.ToString();
            lbl_Terminal4_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal4_Length_Tolerance_Min.ToString();
            lbl_Terminal4_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal4_Distance_Tolerance_Min.ToString();

            lbl_Terminal5_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal5_Width_Tolerance_Min.ToString();
            lbl_Terminal5_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal5_Length_Tolerance_Min.ToString();
            lbl_Terminal5_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal5_Distance_Tolerance_Min.ToString();

            lbl_Terminal6_Width_Tolerance_Min.Text = m_DataCommon.m_db_Terminal6_Width_Tolerance_Min.ToString();
            lbl_Terminal6_Length_Tolerance_Min.Text = m_DataCommon.m_db_Terminal6_Length_Tolerance_Min.ToString();
            lbl_Terminal6_Distance_Tolerance_Min.Text = m_DataCommon.m_db_Terminal6_Distance_Tolerance_Min.ToString();

            //tolerance max
            lbl_Terminal1_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal1_Width_Tolerance_Max.ToString();
            lbl_Terminal1_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal1_Length_Tolerance_Max.ToString();
            lbl_Terminal1_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal1_Distance_Tolerance_Max.ToString();

            lbl_Terminal2_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal2_Width_Tolerance_Max.ToString();
            lbl_Terminal2_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal2_Length_Tolerance_Max.ToString();
            lbl_Terminal2_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal2_Distance_Tolerance_Max.ToString();

            lbl_Terminal3_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal3_Width_Tolerance_Max.ToString();
            lbl_Terminal3_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal3_Length_Tolerance_Max.ToString();
            lbl_Terminal3_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal3_Distance_Tolerance_Max.ToString();

            lbl_Terminal4_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal4_Width_Tolerance_Max.ToString();
            lbl_Terminal4_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal4_Length_Tolerance_Max.ToString();
            lbl_Terminal4_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal4_Distance_Tolerance_Max.ToString();

            lbl_Terminal5_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal5_Width_Tolerance_Max.ToString();
            lbl_Terminal5_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal5_Length_Tolerance_Max.ToString();
            lbl_Terminal5_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal5_Distance_Tolerance_Max.ToString();

            lbl_Terminal6_Width_Tolerance_Max.Text = m_DataCommon.m_db_Terminal6_Width_Tolerance_Max.ToString();
            lbl_Terminal6_Length_Tolerance_Max.Text = m_DataCommon.m_db_Terminal6_Length_Tolerance_Max.ToString();
            lbl_Terminal6_Distance_Tolerance_Max.Text = m_DataCommon.m_db_Terminal6_Distance_Tolerance_Max.ToString();
        }

        //update setting
        public void Update_Settings()
        {
            //spec
            m_DataCommon.m_db_Terminal1_Width_Spec = double.Parse(lbl_Terminal1_Width_Spec.Text);
            m_DataCommon.m_db_Terminal1_Length_Spec = double.Parse(lbl_Terminal1_Length_Spec.Text);
            m_DataCommon.m_db_Terminal1_Distance_Spec = double.Parse(lbl_Terminal1_Distance_Spec.Text);

            m_DataCommon.m_db_Terminal2_Width_Spec = double.Parse(lbl_Terminal2_Width_Spec.Text);
            m_DataCommon.m_db_Terminal2_Length_Spec = double.Parse(lbl_Terminal2_Length_Spec.Text);
            m_DataCommon.m_db_Terminal2_Distance_Spec = double.Parse(lbl_Terminal2_Distance_Spec.Text);

            m_DataCommon.m_db_Terminal3_Width_Spec = double.Parse(lbl_Terminal3_Width_Spec.Text);
            m_DataCommon.m_db_Terminal3_Length_Spec = double.Parse(lbl_Terminal3_Length_Spec.Text);
            m_DataCommon.m_db_Terminal3_Distance_Spec = double.Parse(lbl_Terminal3_Distance_Spec.Text);

            m_DataCommon.m_db_Terminal4_Width_Spec = double.Parse(lbl_Terminal4_Width_Spec.Text);
            m_DataCommon.m_db_Terminal4_Length_Spec = double.Parse(lbl_Terminal4_Length_Spec.Text);
            m_DataCommon.m_db_Terminal4_Distance_Spec = double.Parse(lbl_Terminal4_Distance_Spec.Text);

            m_DataCommon.m_db_Terminal5_Width_Spec = double.Parse(lbl_Terminal5_Width_Spec.Text);
            m_DataCommon.m_db_Terminal5_Length_Spec = double.Parse(lbl_Terminal5_Length_Spec.Text);
            m_DataCommon.m_db_Terminal5_Distance_Spec = double.Parse(lbl_Terminal5_Distance_Spec.Text);

            m_DataCommon.m_db_Terminal6_Width_Spec = double.Parse(lbl_Terminal6_Width_Spec.Text);
            m_DataCommon.m_db_Terminal6_Length_Spec = double.Parse(lbl_Terminal6_Length_Spec.Text);
            m_DataCommon.m_db_Terminal6_Distance_Spec = double.Parse(lbl_Terminal6_Distance_Spec.Text);

            //tolerance min
            m_DataCommon.m_db_Terminal1_Width_Tolerance_Min = double.Parse(lbl_Terminal1_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal1_Length_Tolerance_Min = double.Parse(lbl_Terminal1_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal1_Distance_Tolerance_Min = double.Parse(lbl_Terminal1_Distance_Tolerance_Min.Text);

            m_DataCommon.m_db_Terminal2_Width_Tolerance_Min = double.Parse(lbl_Terminal2_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal2_Length_Tolerance_Min = double.Parse(lbl_Terminal2_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal2_Distance_Tolerance_Min = double.Parse(lbl_Terminal2_Distance_Tolerance_Min.Text);

            m_DataCommon.m_db_Terminal3_Width_Tolerance_Min = double.Parse(lbl_Terminal3_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal3_Length_Tolerance_Min = double.Parse(lbl_Terminal3_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal3_Distance_Tolerance_Min = double.Parse(lbl_Terminal3_Distance_Tolerance_Min.Text);

            m_DataCommon.m_db_Terminal4_Width_Tolerance_Min = double.Parse(lbl_Terminal4_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal4_Length_Tolerance_Min = double.Parse(lbl_Terminal4_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal4_Distance_Tolerance_Min = double.Parse(lbl_Terminal4_Distance_Tolerance_Min.Text);

            m_DataCommon.m_db_Terminal5_Width_Tolerance_Min = double.Parse(lbl_Terminal5_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal5_Length_Tolerance_Min = double.Parse(lbl_Terminal5_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal5_Distance_Tolerance_Min = double.Parse(lbl_Terminal5_Distance_Tolerance_Min.Text);

            m_DataCommon.m_db_Terminal6_Width_Tolerance_Min = double.Parse(lbl_Terminal6_Width_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal6_Length_Tolerance_Min = double.Parse(lbl_Terminal6_Length_Tolerance_Min.Text);
            m_DataCommon.m_db_Terminal6_Distance_Tolerance_Min = double.Parse(lbl_Terminal6_Distance_Tolerance_Min.Text);

            //tolerance max
            m_DataCommon.m_db_Terminal1_Width_Tolerance_Max = double.Parse(lbl_Terminal1_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal1_Length_Tolerance_Max = double.Parse(lbl_Terminal1_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal1_Distance_Tolerance_Max = double.Parse(lbl_Terminal1_Distance_Tolerance_Max.Text);

            m_DataCommon.m_db_Terminal2_Width_Tolerance_Max = double.Parse(lbl_Terminal2_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal2_Length_Tolerance_Max = double.Parse(lbl_Terminal2_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal2_Distance_Tolerance_Max = double.Parse(lbl_Terminal2_Distance_Tolerance_Max.Text);

            m_DataCommon.m_db_Terminal3_Width_Tolerance_Max = double.Parse(lbl_Terminal3_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal3_Length_Tolerance_Max = double.Parse(lbl_Terminal3_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal3_Distance_Tolerance_Max = double.Parse(lbl_Terminal3_Distance_Tolerance_Max.Text);

            m_DataCommon.m_db_Terminal4_Width_Tolerance_Max = double.Parse(lbl_Terminal4_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal4_Length_Tolerance_Max = double.Parse(lbl_Terminal4_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal4_Distance_Tolerance_Max = double.Parse(lbl_Terminal4_Distance_Tolerance_Max.Text);

            m_DataCommon.m_db_Terminal5_Width_Tolerance_Max = double.Parse(lbl_Terminal5_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal5_Length_Tolerance_Max = double.Parse(lbl_Terminal5_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal5_Distance_Tolerance_Max = double.Parse(lbl_Terminal5_Distance_Tolerance_Max.Text);

            m_DataCommon.m_db_Terminal6_Width_Tolerance_Max = double.Parse(lbl_Terminal6_Width_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal6_Length_Tolerance_Max = double.Parse(lbl_Terminal6_Length_Tolerance_Max.Text);
            m_DataCommon.m_db_Terminal6_Distance_Tolerance_Max = double.Parse(lbl_Terminal6_Distance_Tolerance_Max.Text);

            //save to pkg file
            m_pSeqMachineCtrl.MachineInfo_To_Registry();
            m_pSeqMachineCtrl.Pkg_To_Registry();
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

        //button events/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Update_Click(object sender, EventArgs e)
        {
            Update_Settings();
        }

        private void btn_Terminal1_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal1_Width_Spec, btn_Terminal1_Width_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal2_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal2_Width_Spec, btn_Terminal2_Width_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal3_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal3_Width_Spec, btn_Terminal3_Width_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal4_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal4_Width_Spec, btn_Terminal4_Width_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal5_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal5_Width_Spec, btn_Terminal5_Width_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal6_Width_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal6_Width_Spec, btn_Terminal6_Width_Spec.Text + "Spec", 2); Update_Settings(); }

        private void btn_Terminal1_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal1_Length_Spec, btn_Terminal1_Length_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal2_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal2_Length_Spec, btn_Terminal2_Length_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal3_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal3_Length_Spec, btn_Terminal3_Length_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal4_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal4_Length_Spec, btn_Terminal4_Length_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal5_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal5_Length_Spec, btn_Terminal5_Length_Spec.Text + "Spec", 2); Update_Settings(); }
        private void btn_Terminal6_Length_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal6_Length_Spec, btn_Terminal6_Length_Spec.Text + "Spec", 2); Update_Settings(); }

        private void btn_Terminal1_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal1_Distance_Spec, btn_Terminal1_Distance_Spec.Text + " Spec", 2); Update_Settings(); }
        private void btn_Terminal2_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal2_Distance_Spec, btn_Terminal2_Distance_Spec.Text + " Spec", 2); Update_Settings(); }
        private void btn_Terminal3_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal3_Distance_Spec, btn_Terminal3_Distance_Spec.Text + " Spec", 2); Update_Settings(); }
        private void btn_Terminal4_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal4_Distance_Spec, btn_Terminal4_Distance_Spec.Text + " Spec", 2); Update_Settings(); }
        private void btn_Terminal5_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal5_Distance_Spec, btn_Terminal5_Distance_Spec.Text + " Spec", 2); Update_Settings(); }
        private void btn_Terminal6_Distance_Spec_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal6_Distance_Spec, btn_Terminal6_Distance_Spec.Text + " Spec", 2); Update_Settings(); }

        private void btn_Terminal1_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal1_Width_Tolerance_Min, btn_Terminal1_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal2_Width_Tolerance_Min, btn_Terminal2_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal3_Width_Tolerance_Min, btn_Terminal3_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal4_Width_Tolerance_Min, btn_Terminal4_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal5_Width_Tolerance_Min, btn_Terminal5_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Width_Tolerance_Min_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal6_Width_Tolerance_Min, btn_Terminal6_Width_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
                                                                                            
        private void btn_Terminal1_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal1_Length_Tolerance_Min, btn_Terminal1_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal2_Length_Tolerance_Min, btn_Terminal2_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal3_Length_Tolerance_Min, btn_Terminal3_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal4_Length_Tolerance_Min, btn_Terminal4_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal5_Length_Tolerance_Min, btn_Terminal5_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Length_Tolerance_Min_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal6_Length_Tolerance_Min, btn_Terminal6_Length_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }

        private void btn_Terminal1_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal1_Distance_Tolerance_Min, btn_Terminal1_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal2_Distance_Tolerance_Min, btn_Terminal2_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal3_Distance_Tolerance_Min, btn_Terminal3_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal4_Distance_Tolerance_Min, btn_Terminal4_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal5_Distance_Tolerance_Min, btn_Terminal5_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Distance_Tolerance_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal6_Distance_Tolerance_Min, btn_Terminal6_Distance_Tolerance_Min.Text + " Tolerance", 2); Update_Settings(); }

        private void btn_Terminal1_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal1_Width_Tolerance_Max, btn_Terminal1_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal2_Width_Tolerance_Max, btn_Terminal2_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal3_Width_Tolerance_Max, btn_Terminal3_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal4_Width_Tolerance_Max, btn_Terminal4_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal5_Width_Tolerance_Max, btn_Terminal5_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Width_Tolerance_Max_Click(object sender, EventArgs e)    { Display_InputBox(lbl_Terminal6_Width_Tolerance_Max, btn_Terminal6_Width_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }

        private void btn_Terminal1_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal1_Length_Tolerance_Max, btn_Terminal1_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal2_Length_Tolerance_Max, btn_Terminal2_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal3_Length_Tolerance_Max, btn_Terminal3_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal4_Length_Tolerance_Max, btn_Terminal4_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal5_Length_Tolerance_Max, btn_Terminal5_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Length_Tolerance_Max_Click(object sender, EventArgs e)   { Display_InputBox(lbl_Terminal6_Length_Tolerance_Max, btn_Terminal6_Length_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }

        private void btn_Terminal1_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal1_Distance_Tolerance_Max, btn_Terminal1_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal2_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal2_Distance_Tolerance_Max, btn_Terminal2_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal3_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal3_Distance_Tolerance_Max, btn_Terminal3_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal4_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal4_Distance_Tolerance_Max, btn_Terminal4_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal5_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal5_Distance_Tolerance_Max, btn_Terminal5_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
        private void btn_Terminal6_Distance_Tolerance_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_Terminal6_Distance_Tolerance_Max, btn_Terminal6_Distance_Tolerance_Max.Text + " Tolerance", 2); Update_Settings(); }
    }
}