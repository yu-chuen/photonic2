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
    public partial class UserCtrl_OP90_Settings : UserControl
    {
        //variables
        Data_Common m_DataCommon;
        Data_UnitTempStorage m_DataUnit;
        SeqMachineControl m_pSeqMachineCtrl;
        MachineInfo m_pMachineInfo;

        //constructor
        public UserCtrl_OP90_Settings()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataCommon = m_pSeqMachineCtrl.m_DataPackageFile.m_DataCommon;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
            m_pMachineInfo = m_pSeqMachineCtrl.m_MachineInfo;
        }

        //dialog loaded
        private void UserCtrl_OP90_Settings_Load(object sender, EventArgs e)
        {
            Load_Settings();
        }

        private void Load_Settings()
        {
            //update checkbox
            Update_CheckBox(cb_HTR_Grade_A_LV, m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b);
            Update_CheckBox(cb_HTR_Grade_B_LV, m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b);
            Update_CheckBox(cb_HTR_Grade_C_LV, m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b);
            Update_CheckBox(cb_HTR_Grade_D_LV, m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b);
            Update_CheckBox(cb_HTR_Grade_E_LV, m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b);

            Update_CheckBox(cb_RTD_Grade_A_LV, m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b);
            Update_CheckBox(cb_RTD_Grade_B_LV, m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b);
            Update_CheckBox(cb_RTD_Grade_C_LV, m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b);
            Update_CheckBox(cb_RTD_Grade_D_LV, m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b);
            Update_CheckBox(cb_RTD_Grade_E_LV, m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b);

            Update_CheckBox(cb_HTR_Grade_A_HV, m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b);
            Update_CheckBox(cb_HTR_Grade_B_HV, m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b);
            Update_CheckBox(cb_HTR_Grade_C_HV, m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b);
            Update_CheckBox(cb_HTR_Grade_D_HV, m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b);
            Update_CheckBox(cb_HTR_Grade_E_HV, m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b);

            Update_CheckBox(cb_RTD_Grade_A_HV, m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b);
            Update_CheckBox(cb_RTD_Grade_B_HV, m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b);
            Update_CheckBox(cb_RTD_Grade_C_HV, m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b);
            Update_CheckBox(cb_RTD_Grade_D_HV, m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b);
            Update_CheckBox(cb_RTD_Grade_E_HV, m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b);

            //vision
            lbl_VisionFailCountLimit.Text = m_DataCommon.m_i_OP90_VisionFailCountLimit.ToString();

            //HTR
            lbl_HTR_A_Min_LV.Text = m_DataCommon.m_db_HTR_A_Min_LV.ToString();
            lbl_HTR_B_Min_LV.Text = m_DataCommon.m_db_HTR_B_Min_LV.ToString();
            lbl_HTR_C_Min_LV.Text = m_DataCommon.m_db_HTR_C_Min_LV.ToString();
            lbl_HTR_D_Min_LV.Text = m_DataCommon.m_db_HTR_D_Min_LV.ToString();
            lbl_HTR_E_Min_LV.Text = m_DataCommon.m_db_HTR_E_Min_LV.ToString();

            lbl_HTR_A_Max_LV.Text = m_DataCommon.m_db_HTR_A_Max_LV.ToString();
            lbl_HTR_B_Max_LV.Text = m_DataCommon.m_db_HTR_B_Max_LV.ToString();
            lbl_HTR_C_Max_LV.Text = m_DataCommon.m_db_HTR_C_Max_LV.ToString();
            lbl_HTR_D_Max_LV.Text = m_DataCommon.m_db_HTR_D_Max_LV.ToString();
            lbl_HTR_E_Max_LV.Text = m_DataCommon.m_db_HTR_E_Max_LV.ToString();

            lbl_HTR_A_Nominal_LV.Text = m_DataCommon.m_db_HTR_A_Nominal_LV.ToString();
            lbl_HTR_B_Nominal_LV.Text = m_DataCommon.m_db_HTR_B_Nominal_LV.ToString();
            lbl_HTR_C_Nominal_LV.Text = m_DataCommon.m_db_HTR_C_Nominal_LV.ToString();
            lbl_HTR_D_Nominal_LV.Text = m_DataCommon.m_db_HTR_D_Nominal_LV.ToString();
            lbl_HTR_E_Nominal_LV.Text = m_DataCommon.m_db_HTR_E_Nominal_LV.ToString();

            lbl_HTR_A_Min_HV.Text = m_DataCommon.m_db_HTR_A_Min.ToString();
            lbl_HTR_B_Min_HV.Text = m_DataCommon.m_db_HTR_B_Min.ToString();
            lbl_HTR_C_Min_HV.Text = m_DataCommon.m_db_HTR_C_Min.ToString();
            lbl_HTR_D_Min_HV.Text = m_DataCommon.m_db_HTR_D_Min.ToString();
            lbl_HTR_E_Min_HV.Text = m_DataCommon.m_db_HTR_E_Min.ToString();

            lbl_HTR_A_Max_HV.Text = m_DataCommon.m_db_HTR_A_Max.ToString();
            lbl_HTR_B_Max_HV.Text = m_DataCommon.m_db_HTR_B_Max.ToString();
            lbl_HTR_C_Max_HV.Text = m_DataCommon.m_db_HTR_C_Max.ToString();
            lbl_HTR_D_Max_HV.Text = m_DataCommon.m_db_HTR_D_Max.ToString();
            lbl_HTR_E_Max_HV.Text = m_DataCommon.m_db_HTR_E_Max.ToString();

            lbl_HTR_A_Nominal_HV.Text = m_DataCommon.m_db_HTR_A_Nominal.ToString();
            lbl_HTR_B_Nominal_HV.Text = m_DataCommon.m_db_HTR_B_Nominal.ToString();
            lbl_HTR_C_Nominal_HV.Text = m_DataCommon.m_db_HTR_C_Nominal.ToString();
            lbl_HTR_D_Nominal_HV.Text = m_DataCommon.m_db_HTR_D_Nominal.ToString();
            lbl_HTR_E_Nominal_HV.Text = m_DataCommon.m_db_HTR_E_Nominal.ToString();

            //RTD
            lbl_RTD_A_Min_LV.Text = m_DataCommon.m_db_RTD_A_Min_LV.ToString();
            lbl_RTD_B_Min_LV.Text = m_DataCommon.m_db_RTD_B_Min_LV.ToString();
            lbl_RTD_C_Min_LV.Text = m_DataCommon.m_db_RTD_C_Min_LV.ToString();
            lbl_RTD_D_Min_LV.Text = m_DataCommon.m_db_RTD_D_Min_LV.ToString();
            lbl_RTD_E_Min_LV.Text = m_DataCommon.m_db_RTD_E_Min_LV.ToString();

            lbl_RTD_A_Max_LV.Text = m_DataCommon.m_db_RTD_A_Max_LV.ToString();
            lbl_RTD_B_Max_LV.Text = m_DataCommon.m_db_RTD_B_Max_LV.ToString();
            lbl_RTD_C_Max_LV.Text = m_DataCommon.m_db_RTD_C_Max_LV.ToString();
            lbl_RTD_D_Max_LV.Text = m_DataCommon.m_db_RTD_D_Max_LV.ToString();
            lbl_RTD_E_Max_LV.Text = m_DataCommon.m_db_RTD_E_Max_LV.ToString();

            lbl_RTD_A_Nominal_LV.Text = m_DataCommon.m_db_RTD_A_Nominal_LV.ToString();
            lbl_RTD_B_Nominal_LV.Text = m_DataCommon.m_db_RTD_B_Nominal_LV.ToString();
            lbl_RTD_C_Nominal_LV.Text = m_DataCommon.m_db_RTD_C_Nominal_LV.ToString();
            lbl_RTD_D_Nominal_LV.Text = m_DataCommon.m_db_RTD_D_Nominal_LV.ToString();
            lbl_RTD_E_Nominal_LV.Text = m_DataCommon.m_db_RTD_E_Nominal_LV.ToString();

            lbl_RTD_A_Min_HV.Text = m_DataCommon.m_db_RTD_A_Min.ToString();
            lbl_RTD_B_Min_HV.Text = m_DataCommon.m_db_RTD_B_Min.ToString();
            lbl_RTD_C_Min_HV.Text = m_DataCommon.m_db_RTD_C_Min.ToString();
            lbl_RTD_D_Min_HV.Text = m_DataCommon.m_db_RTD_D_Min.ToString();
            lbl_RTD_E_Min_HV.Text = m_DataCommon.m_db_RTD_E_Min.ToString();

            lbl_RTD_A_Max_HV.Text = m_DataCommon.m_db_RTD_A_Max.ToString();
            lbl_RTD_B_Max_HV.Text = m_DataCommon.m_db_RTD_B_Max.ToString();
            lbl_RTD_C_Max_HV.Text = m_DataCommon.m_db_RTD_C_Max.ToString();
            lbl_RTD_D_Max_HV.Text = m_DataCommon.m_db_RTD_D_Max.ToString();
            lbl_RTD_E_Max_HV.Text = m_DataCommon.m_db_RTD_E_Max.ToString();

            lbl_RTD_A_Nominal_HV.Text = m_DataCommon.m_db_RTD_A_Nominal.ToString();
            lbl_RTD_B_Nominal_HV.Text = m_DataCommon.m_db_RTD_B_Nominal.ToString();
            lbl_RTD_C_Nominal_HV.Text = m_DataCommon.m_db_RTD_C_Nominal.ToString();
            lbl_RTD_D_Nominal_HV.Text = m_DataCommon.m_db_RTD_D_Nominal.ToString();
            lbl_RTD_E_Nominal_HV.Text = m_DataCommon.m_db_RTD_E_Nominal.ToString();

            //enable or disable editing
            Update_ObjEnabledState(cb_HTR_Grade_A_LV, lbl_HTR_A_Min_LV, lbl_HTR_A_Max_LV, lbl_HTR_A_Nominal_LV, btn_HTR_A_Min_LV, btn_HTR_A_Max_LV, btn_HTR_A_Nominal_LV);
            Update_ObjEnabledState(cb_HTR_Grade_B_LV, lbl_HTR_B_Min_LV, lbl_HTR_B_Max_LV, lbl_HTR_B_Nominal_LV, btn_HTR_B_Min_LV, btn_HTR_B_Max_LV, btn_HTR_B_Nominal_LV);
            Update_ObjEnabledState(cb_HTR_Grade_C_LV, lbl_HTR_C_Min_LV, lbl_HTR_C_Max_LV, lbl_HTR_C_Nominal_LV, btn_HTR_C_Min_LV, btn_HTR_C_Max_LV, btn_HTR_C_Nominal_LV);
            Update_ObjEnabledState(cb_HTR_Grade_D_LV, lbl_HTR_D_Min_LV, lbl_HTR_D_Max_LV, lbl_HTR_D_Nominal_LV, btn_HTR_D_Min_LV, btn_HTR_D_Max_LV, btn_HTR_D_Nominal_LV);
            Update_ObjEnabledState(cb_HTR_Grade_E_LV, lbl_HTR_E_Min_LV, lbl_HTR_E_Max_LV, lbl_HTR_E_Nominal_LV, btn_HTR_E_Min_LV, btn_HTR_E_Max_LV, btn_HTR_E_Nominal_LV);

            Update_ObjEnabledState(cb_RTD_Grade_A_LV, lbl_RTD_A_Min_LV, lbl_RTD_A_Max_LV, lbl_RTD_A_Nominal_LV, btn_RTD_A_Min_LV, btn_RTD_A_Max_LV, btn_RTD_A_Nominal_LV);
            Update_ObjEnabledState(cb_RTD_Grade_B_LV, lbl_RTD_B_Min_LV, lbl_RTD_B_Max_LV, lbl_RTD_B_Nominal_LV, btn_RTD_B_Min_LV, btn_RTD_B_Max_LV, btn_RTD_B_Nominal_LV);
            Update_ObjEnabledState(cb_RTD_Grade_C_LV, lbl_RTD_C_Min_LV, lbl_RTD_C_Max_LV, lbl_RTD_C_Nominal_LV, btn_RTD_C_Min_LV, btn_RTD_C_Max_LV, btn_RTD_C_Nominal_LV);
            Update_ObjEnabledState(cb_RTD_Grade_D_LV, lbl_RTD_D_Min_LV, lbl_RTD_D_Max_LV, lbl_RTD_D_Nominal_LV, btn_RTD_D_Min_LV, btn_RTD_D_Max_LV, btn_RTD_D_Nominal_LV);
            Update_ObjEnabledState(cb_RTD_Grade_E_LV, lbl_RTD_E_Min_LV, lbl_RTD_E_Max_LV, lbl_RTD_E_Nominal_LV, btn_RTD_E_Min_LV, btn_RTD_E_Max_LV, btn_RTD_E_Nominal_LV);

            Update_ObjEnabledState(cb_HTR_Grade_A_HV, lbl_HTR_A_Min_HV, lbl_HTR_A_Max_HV, lbl_HTR_A_Nominal_HV, btn_HTR_A_Min_HV, btn_HTR_A_Max_HV, btn_HTR_A_Nominal_HV);
            Update_ObjEnabledState(cb_HTR_Grade_B_HV, lbl_HTR_B_Min_HV, lbl_HTR_B_Max_HV, lbl_HTR_B_Nominal_HV, btn_HTR_B_Min_HV, btn_HTR_B_Max_HV, btn_HTR_B_Nominal_HV);
            Update_ObjEnabledState(cb_HTR_Grade_C_HV, lbl_HTR_C_Min_HV, lbl_HTR_C_Max_HV, lbl_HTR_C_Nominal_HV, btn_HTR_C_Min_HV, btn_HTR_C_Max_HV, btn_HTR_C_Nominal_HV);
            Update_ObjEnabledState(cb_HTR_Grade_D_HV, lbl_HTR_D_Min_HV, lbl_HTR_D_Max_HV, lbl_HTR_D_Nominal_HV, btn_HTR_D_Min_HV, btn_HTR_D_Max_HV, btn_HTR_D_Nominal_HV);
            Update_ObjEnabledState(cb_HTR_Grade_E_HV, lbl_HTR_E_Min_HV, lbl_HTR_E_Max_HV, lbl_HTR_E_Nominal_HV, btn_HTR_E_Min_HV, btn_HTR_E_Max_HV, btn_HTR_E_Nominal_HV);

            Update_ObjEnabledState(cb_RTD_Grade_A_HV, lbl_RTD_A_Min_HV, lbl_RTD_A_Max_HV, lbl_RTD_A_Nominal_HV, btn_RTD_A_Min_HV, btn_RTD_A_Max_HV, btn_RTD_A_Nominal_HV);
            Update_ObjEnabledState(cb_RTD_Grade_B_HV, lbl_RTD_B_Min_HV, lbl_RTD_B_Max_HV, lbl_RTD_B_Nominal_HV, btn_RTD_B_Min_HV, btn_RTD_B_Max_HV, btn_RTD_B_Nominal_HV);
            Update_ObjEnabledState(cb_RTD_Grade_C_HV, lbl_RTD_C_Min_HV, lbl_RTD_C_Max_HV, lbl_RTD_C_Nominal_HV, btn_RTD_C_Min_HV, btn_RTD_C_Max_HV, btn_RTD_C_Nominal_HV);
            Update_ObjEnabledState(cb_RTD_Grade_D_HV, lbl_RTD_D_Min_HV, lbl_RTD_D_Max_HV, lbl_RTD_D_Nominal_HV, btn_RTD_D_Min_HV, btn_RTD_D_Max_HV, btn_RTD_D_Nominal_HV);
            Update_ObjEnabledState(cb_RTD_Grade_E_HV, lbl_RTD_E_Min_HV, lbl_RTD_E_Max_HV, lbl_RTD_E_Nominal_HV, btn_RTD_E_Min_HV, btn_RTD_E_Max_HV, btn_RTD_E_Nominal_HV);
        }

        private void Update_CheckBox(CheckBox mObj, bool bActivated)
        {
            //set checkbox
            if (bActivated == true)
            {
                mObj.Checked = true;
            }
            else
            {
                mObj.Checked = false;
            }
        }

        private void Update_ObjEnabledState(CheckBox m_cb_Grade, Label m_lbl_Min, Label m_lbl_Max, Label m_lbl_Nominal, Button m_btn_Min, Button m_btn_Max, Button m_btn_Nominal)
        {
            if (m_cb_Grade.Checked == true)
            {
                m_lbl_Min.Enabled = true;
                m_lbl_Max.Enabled = true;
                m_lbl_Nominal.Enabled = true;

                m_btn_Min.Enabled = true;
                m_btn_Max.Enabled = true;
                m_btn_Nominal.Enabled = true;
            }
            else
            {
                m_lbl_Min.Enabled = false;
                m_lbl_Max.Enabled = false;
                m_lbl_Nominal.Enabled = false;

                m_btn_Min.Enabled = false;
                m_btn_Max.Enabled = false;
                m_btn_Nominal.Enabled = false;
            }
        }
        //get setting
        public void UpdateSettings()
        {
            //vision
            m_DataCommon.m_i_OP90_VisionFailCountLimit = int.Parse(lbl_VisionFailCountLimit.Text);

            //HTR
            m_DataCommon.m_db_HTR_A_Min_LV = double.Parse(lbl_HTR_A_Min_LV.Text);
            m_DataCommon.m_db_HTR_B_Min_LV = double.Parse(lbl_HTR_B_Min_LV.Text);
            m_DataCommon.m_db_HTR_C_Min_LV = double.Parse(lbl_HTR_C_Min_LV.Text);
            m_DataCommon.m_db_HTR_D_Min_LV = double.Parse(lbl_HTR_D_Min_LV.Text);
            m_DataCommon.m_db_HTR_E_Min_LV = double.Parse(lbl_HTR_E_Min_LV.Text);

            m_DataCommon.m_db_HTR_A_Max_LV = double.Parse(lbl_HTR_A_Max_LV.Text);
            m_DataCommon.m_db_HTR_B_Max_LV = double.Parse(lbl_HTR_B_Max_LV.Text);
            m_DataCommon.m_db_HTR_C_Max_LV = double.Parse(lbl_HTR_C_Max_LV.Text);
            m_DataCommon.m_db_HTR_D_Max_LV = double.Parse(lbl_HTR_D_Max_LV.Text);
            m_DataCommon.m_db_HTR_E_Max_LV = double.Parse(lbl_HTR_E_Max_LV.Text);

            m_DataCommon.m_db_HTR_A_Nominal_LV = double.Parse(lbl_HTR_A_Nominal_LV.Text);
            m_DataCommon.m_db_HTR_B_Nominal_LV = double.Parse(lbl_HTR_B_Nominal_LV.Text);
            m_DataCommon.m_db_HTR_C_Nominal_LV = double.Parse(lbl_HTR_C_Nominal_LV.Text);
            m_DataCommon.m_db_HTR_D_Nominal_LV = double.Parse(lbl_HTR_D_Nominal_LV.Text);
            m_DataCommon.m_db_HTR_E_Nominal_LV = double.Parse(lbl_HTR_E_Nominal_LV.Text);

            m_DataCommon.m_db_HTR_A_Min = double.Parse(lbl_HTR_A_Min_HV.Text);
            m_DataCommon.m_db_HTR_B_Min = double.Parse(lbl_HTR_B_Min_HV.Text);
            m_DataCommon.m_db_HTR_C_Min = double.Parse(lbl_HTR_C_Min_HV.Text);
            m_DataCommon.m_db_HTR_D_Min = double.Parse(lbl_HTR_D_Min_HV.Text);
            m_DataCommon.m_db_HTR_E_Min = double.Parse(lbl_HTR_E_Min_HV.Text);
                                                                    
            m_DataCommon.m_db_HTR_A_Max = double.Parse(lbl_HTR_A_Max_HV.Text);
            m_DataCommon.m_db_HTR_B_Max = double.Parse(lbl_HTR_B_Max_HV.Text);
            m_DataCommon.m_db_HTR_C_Max = double.Parse(lbl_HTR_C_Max_HV.Text);
            m_DataCommon.m_db_HTR_D_Max = double.Parse(lbl_HTR_D_Max_HV.Text);
            m_DataCommon.m_db_HTR_E_Max = double.Parse(lbl_HTR_E_Max_HV.Text);

            m_DataCommon.m_db_HTR_A_Nominal = double.Parse(lbl_HTR_A_Nominal_HV.Text);
            m_DataCommon.m_db_HTR_B_Nominal = double.Parse(lbl_HTR_B_Nominal_HV.Text);
            m_DataCommon.m_db_HTR_C_Nominal = double.Parse(lbl_HTR_C_Nominal_HV.Text);
            m_DataCommon.m_db_HTR_D_Nominal = double.Parse(lbl_HTR_D_Nominal_HV.Text);
            m_DataCommon.m_db_HTR_E_Nominal = double.Parse(lbl_HTR_E_Nominal_HV.Text);

            //RTD
            m_DataCommon.m_db_RTD_A_Min_LV = double.Parse(lbl_RTD_A_Min_LV.Text);
            m_DataCommon.m_db_RTD_B_Min_LV = double.Parse(lbl_RTD_B_Min_LV.Text);
            m_DataCommon.m_db_RTD_C_Min_LV = double.Parse(lbl_RTD_C_Min_LV.Text);
            m_DataCommon.m_db_RTD_D_Min_LV = double.Parse(lbl_RTD_D_Min_LV.Text);
            m_DataCommon.m_db_RTD_E_Min_LV = double.Parse(lbl_RTD_E_Min_LV.Text);

            m_DataCommon.m_db_RTD_A_Max_LV = double.Parse(lbl_RTD_A_Max_LV.Text);
            m_DataCommon.m_db_RTD_B_Max_LV = double.Parse(lbl_RTD_B_Max_LV.Text);
            m_DataCommon.m_db_RTD_C_Max_LV = double.Parse(lbl_RTD_C_Max_LV.Text);
            m_DataCommon.m_db_RTD_D_Max_LV = double.Parse(lbl_RTD_D_Max_LV.Text);
            m_DataCommon.m_db_RTD_E_Max_LV = double.Parse(lbl_RTD_E_Max_LV.Text);

            m_DataCommon.m_db_RTD_A_Nominal_LV = double.Parse(lbl_RTD_A_Nominal_LV.Text);
            m_DataCommon.m_db_RTD_B_Nominal_LV = double.Parse(lbl_RTD_B_Nominal_LV.Text);
            m_DataCommon.m_db_RTD_C_Nominal_LV = double.Parse(lbl_RTD_C_Nominal_LV.Text);
            m_DataCommon.m_db_RTD_D_Nominal_LV = double.Parse(lbl_RTD_D_Nominal_LV.Text);
            m_DataCommon.m_db_RTD_E_Nominal_LV = double.Parse(lbl_RTD_E_Nominal_LV.Text);

            m_DataCommon.m_db_RTD_A_Min = double.Parse(lbl_RTD_A_Min_HV.Text);
            m_DataCommon.m_db_RTD_B_Min = double.Parse(lbl_RTD_B_Min_HV.Text);
            m_DataCommon.m_db_RTD_C_Min = double.Parse(lbl_RTD_C_Min_HV.Text);
            m_DataCommon.m_db_RTD_D_Min = double.Parse(lbl_RTD_D_Min_HV.Text);
            m_DataCommon.m_db_RTD_E_Min = double.Parse(lbl_RTD_E_Min_HV.Text);
                                                                    
            m_DataCommon.m_db_RTD_A_Max = double.Parse(lbl_RTD_A_Max_HV.Text);
            m_DataCommon.m_db_RTD_B_Max = double.Parse(lbl_RTD_B_Max_HV.Text);
            m_DataCommon.m_db_RTD_C_Max = double.Parse(lbl_RTD_C_Max_HV.Text);
            m_DataCommon.m_db_RTD_D_Max = double.Parse(lbl_RTD_D_Max_HV.Text);
            m_DataCommon.m_db_RTD_E_Max = double.Parse(lbl_RTD_E_Max_HV.Text);

            m_DataCommon.m_db_RTD_A_Nominal = double.Parse(lbl_RTD_A_Nominal_HV.Text);
            m_DataCommon.m_db_RTD_B_Nominal = double.Parse(lbl_RTD_B_Nominal_HV.Text);
            m_DataCommon.m_db_RTD_C_Nominal = double.Parse(lbl_RTD_C_Nominal_HV.Text);
            m_DataCommon.m_db_RTD_D_Nominal = double.Parse(lbl_RTD_D_Nominal_HV.Text);
            m_DataCommon.m_db_RTD_E_Nominal = double.Parse(lbl_RTD_E_Nominal_HV.Text);

            //enable or disable grades
            m_pMachineInfo.m_bEnable_HTR_Grade_A_LV = m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_B_LV = m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_C_LV = m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_D_LV = m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_E_LV = m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b;

            m_pMachineInfo.m_bEnable_RTD_Grade_A_LV = m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_B_LV = m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_C_LV = m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_D_LV = m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_E_LV = m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b;

            m_pMachineInfo.m_bEnable_HTR_Grade_A_HV = m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_B_HV = m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_C_HV = m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_D_HV = m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b;
            m_pMachineInfo.m_bEnable_HTR_Grade_E_HV = m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b;

            m_pMachineInfo.m_bEnable_RTD_Grade_A_HV = m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_B_HV = m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_C_HV = m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_D_HV = m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b;
            m_pMachineInfo.m_bEnable_RTD_Grade_E_HV = m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b;

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

        private void btn_Update_Click(object sender, EventArgs e)
        {
            UpdateSettings();
        }

        //HTR Grade A
        private void btn_HTR_A_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Min_LV, btn_HTR_A_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_A_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Max_LV, btn_HTR_A_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_A_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Nominal_LV, btn_HTR_A_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_HTR_A_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Min_HV, btn_HTR_A_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_A_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Max_HV, btn_HTR_A_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_A_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_A_Nominal_HV, btn_HTR_A_Nominal_HV.Text, 2); UpdateSettings(); }

        //HTR Grade B
        private void btn_HTR_B_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Min_LV, btn_HTR_B_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_B_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Max_LV, btn_HTR_B_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_B_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Nominal_LV, btn_HTR_B_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_HTR_B_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Min_HV, btn_HTR_B_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_B_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Max_HV, btn_HTR_B_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_B_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_B_Nominal_HV, btn_HTR_B_Nominal_HV.Text, 2); UpdateSettings(); }

        //HTR Grade C
        private void btn_HTR_C_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Min_LV, btn_HTR_C_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_C_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Max_LV, btn_HTR_C_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_C_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Nominal_LV, btn_HTR_C_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_HTR_C_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Min_HV, btn_HTR_C_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_C_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Max_HV, btn_HTR_C_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_C_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_C_Nominal_HV, btn_HTR_C_Nominal_HV.Text, 2); UpdateSettings(); }

        //HTR Grade D
        private void btn_HTR_D_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Min_LV, btn_HTR_D_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_D_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Max_LV, btn_HTR_D_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_D_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Nominal_LV, btn_HTR_D_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_HTR_D_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Min_HV, btn_HTR_D_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_D_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Max_HV, btn_HTR_D_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_D_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_D_Nominal_HV, btn_HTR_D_Nominal_HV .Text, 2); UpdateSettings(); }

        //HTR Grade E
        private void btn_HTR_E_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Min_LV, btn_HTR_E_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_E_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Max_LV, btn_HTR_E_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_HTR_E_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Nominal_LV, btn_RTD_E_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_HTR_E_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Min_HV, btn_HTR_E_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_E_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Max_HV, btn_HTR_E_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_HTR_E_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_HTR_E_Nominal_HV, btn_RTD_E_Nominal_HV.Text, 2); UpdateSettings(); }

        //RTD Grade A       
        private void btn_RTD_A_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Min_LV, btn_RTD_A_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_A_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Max_LV, btn_RTD_A_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_A_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Nominal_LV, btn_RTD_A_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_RTD_A_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Min_HV, btn_RTD_A_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_A_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Max_HV, btn_RTD_A_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_A_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_A_Nominal_HV, btn_RTD_A_Nominal_HV.Text, 2); UpdateSettings(); }

        //RTD Grade B
        private void btn_RTD_B_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Min_LV, btn_RTD_B_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_B_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Max_LV, btn_RTD_B_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_B_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Nominal_LV, btn_RTD_B_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_RTD_B_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Min_HV, btn_RTD_B_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_B_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Max_HV, btn_RTD_B_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_B_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_B_Nominal_HV, btn_RTD_B_Nominal_HV.Text, 2); UpdateSettings(); }

        //RTD Grade C
        private void btn_RTD_C_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Min_LV, btn_RTD_C_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_C_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Max_LV, btn_RTD_C_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_C_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Nominal_LV, btn_RTD_C_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_RTD_C_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Min_HV, btn_RTD_C_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_C_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Max_HV, btn_RTD_C_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_C_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_C_Nominal_HV, btn_RTD_C_Nominal_HV.Text, 2); UpdateSettings(); }

        //RTD Grade D
        private void btn_RTD_D_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Min_LV, btn_RTD_D_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_D_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Max_LV, btn_RTD_D_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_D_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Nominal_LV, btn_RTD_D_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_RTD_D_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Min_HV, btn_RTD_D_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_D_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Max_HV, btn_RTD_D_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_D_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_D_Nominal_HV, btn_RTD_D_Nominal_HV.Text, 2); UpdateSettings(); }

        //RTD Grade E
        private void btn_RTD_E_Min_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Min_LV, btn_RTD_E_Min_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_E_Max_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Max_LV, btn_RTD_E_Max_LV.Text, 2); UpdateSettings(); }
        private void btn_RTD_E_Nominal_LV_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Nominal_LV, btn_RTD_E_Nominal_LV.Text, 2); UpdateSettings(); }

        private void btn_RTD_E_Min_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Min_HV, btn_RTD_E_Min_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_E_Max_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Max_HV, btn_RTD_E_Max_HV.Text, 2); UpdateSettings(); }
        private void btn_RTD_E_Nominal_Click(object sender, EventArgs e) { Display_InputBox(lbl_RTD_E_Nominal_HV, btn_RTD_E_Nominal_HV.Text, 2); UpdateSettings(); }

        //vision
        private void btn_VisionFailCountLimit_Click(object sender, EventArgs e) { Display_InputBox(lbl_VisionFailCountLimit, btn_VisionFailCountLimit.Text, 1); UpdateSettings(); }
        private void btn_Reset_OP90_FailCount_Click(object sender, EventArgs e)
        {
            MessageBox_Confirmation dlg = new MessageBox_Confirmation();
            dlg.Set_Title("Are you sure?");
            dlg.Set_Label("Do you want to reset the current fail count for OP90 vision?\r\nThis action cannot be undone.");

            if (dlg.ShowDialog() == DialogResult.OK)
            {

                Load_Settings();
            }
        }

        //checkbox events/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void cb_HTR_Grade_A_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b = (m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_A_LV, m_DataUnit.Flag_Enable_HTR_Grade_A_LV_b);
            Update_ObjEnabledState(cb_HTR_Grade_A_LV, lbl_HTR_A_Min_LV, lbl_HTR_A_Max_LV, lbl_HTR_A_Nominal_LV, btn_HTR_A_Min_LV, btn_HTR_A_Max_LV, btn_HTR_A_Nominal_LV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_B_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b = (m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_B_LV, m_DataUnit.Flag_Enable_HTR_Grade_B_LV_b);
            Update_ObjEnabledState(cb_HTR_Grade_B_LV, lbl_HTR_B_Min_LV, lbl_HTR_B_Max_LV, lbl_HTR_B_Nominal_LV, btn_HTR_B_Min_LV, btn_HTR_B_Max_LV, btn_HTR_B_Nominal_LV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_C_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b = (m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_C_LV, m_DataUnit.Flag_Enable_HTR_Grade_C_LV_b);
            Update_ObjEnabledState(cb_HTR_Grade_C_LV, lbl_HTR_C_Min_LV, lbl_HTR_C_Max_LV, lbl_HTR_C_Nominal_LV, btn_HTR_C_Min_LV, btn_HTR_C_Max_LV, btn_HTR_C_Nominal_LV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_D_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b = (m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_D_LV, m_DataUnit.Flag_Enable_HTR_Grade_D_LV_b);
            Update_ObjEnabledState(cb_HTR_Grade_D_LV, lbl_HTR_D_Min_LV, lbl_HTR_D_Max_LV, lbl_HTR_D_Nominal_LV, btn_HTR_D_Min_LV, btn_HTR_D_Max_LV, btn_HTR_D_Nominal_LV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_E_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b = (m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_E_LV, m_DataUnit.Flag_Enable_HTR_Grade_E_LV_b);
            Update_ObjEnabledState(cb_HTR_Grade_E_LV, lbl_HTR_E_Min_LV, lbl_HTR_E_Max_LV, lbl_HTR_E_Nominal_LV, btn_HTR_E_Min_LV, btn_HTR_E_Max_LV, btn_HTR_E_Nominal_LV);
            UpdateSettings();
        }



        private void cb_RTD_Grade_A_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b = (m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_A_LV, m_DataUnit.Flag_Enable_RTD_Grade_A_LV_b);
            Update_ObjEnabledState(cb_RTD_Grade_A_LV, lbl_RTD_A_Min_LV, lbl_RTD_A_Max_LV, lbl_RTD_A_Nominal_LV, btn_RTD_A_Min_LV, btn_RTD_A_Max_LV, btn_RTD_A_Nominal_LV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_B_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b = (m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_B_LV, m_DataUnit.Flag_Enable_RTD_Grade_B_LV_b);
            Update_ObjEnabledState(cb_RTD_Grade_B_LV, lbl_RTD_B_Min_LV, lbl_RTD_B_Max_LV, lbl_RTD_B_Nominal_LV, btn_RTD_B_Min_LV, btn_RTD_B_Max_LV, btn_RTD_B_Nominal_LV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_C_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b = (m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_C_LV, m_DataUnit.Flag_Enable_RTD_Grade_C_LV_b);
            Update_ObjEnabledState(cb_RTD_Grade_C_LV, lbl_RTD_C_Min_LV, lbl_RTD_C_Max_LV, lbl_RTD_C_Nominal_LV, btn_RTD_C_Min_LV, btn_RTD_C_Max_LV, btn_RTD_C_Nominal_LV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_D_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b = (m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_D_LV, m_DataUnit.Flag_Enable_RTD_Grade_D_LV_b);
            Update_ObjEnabledState(cb_RTD_Grade_D_LV, lbl_RTD_D_Min_LV, lbl_RTD_D_Max_LV, lbl_RTD_D_Nominal_LV, btn_RTD_D_Min_LV, btn_RTD_D_Max_LV, btn_RTD_D_Nominal_LV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_E_LV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b = (m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_E_LV, m_DataUnit.Flag_Enable_RTD_Grade_E_LV_b);
            Update_ObjEnabledState(cb_RTD_Grade_E_LV, lbl_RTD_E_Min_LV, lbl_RTD_E_Max_LV, lbl_RTD_E_Nominal_LV, btn_RTD_E_Min_LV, btn_RTD_E_Max_LV, btn_RTD_E_Nominal_LV);
            UpdateSettings();
        }




        private void cb_HTR_Grade_A_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b = (m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_A_HV, m_DataUnit.Flag_Enable_HTR_Grade_A_HV_b);
            Update_ObjEnabledState(cb_HTR_Grade_A_HV, lbl_HTR_A_Min_HV, lbl_HTR_A_Max_HV, lbl_HTR_A_Nominal_HV, btn_HTR_A_Min_HV, btn_HTR_A_Max_HV, btn_HTR_A_Nominal_HV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_B_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b = (m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_B_HV, m_DataUnit.Flag_Enable_HTR_Grade_B_HV_b);
            Update_ObjEnabledState(cb_HTR_Grade_B_HV, lbl_HTR_B_Min_HV, lbl_HTR_B_Max_HV, lbl_HTR_B_Nominal_HV, btn_HTR_B_Min_HV, btn_HTR_B_Max_HV, btn_HTR_B_Nominal_HV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_C_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b = (m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_C_HV, m_DataUnit.Flag_Enable_HTR_Grade_C_HV_b);
            Update_ObjEnabledState(cb_HTR_Grade_C_HV, lbl_HTR_C_Min_HV, lbl_HTR_C_Max_HV, lbl_HTR_C_Nominal_HV, btn_HTR_C_Min_HV, btn_HTR_C_Max_HV, btn_HTR_C_Nominal_HV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_D_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b = (m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_D_HV, m_DataUnit.Flag_Enable_HTR_Grade_D_HV_b);
            Update_ObjEnabledState(cb_HTR_Grade_D_HV, lbl_HTR_D_Min_HV, lbl_HTR_D_Max_HV, lbl_HTR_D_Nominal_HV, btn_HTR_D_Min_HV, btn_HTR_D_Max_HV, btn_HTR_D_Nominal_HV);
            UpdateSettings();
        }

        private void cb_HTR_Grade_E_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b = (m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b == true) ? false : true;
            Update_CheckBox(cb_HTR_Grade_E_HV, m_DataUnit.Flag_Enable_HTR_Grade_E_HV_b);
            Update_ObjEnabledState(cb_HTR_Grade_E_HV, lbl_HTR_E_Min_HV, lbl_HTR_E_Max_HV, lbl_HTR_E_Nominal_HV, btn_HTR_E_Min_HV, btn_HTR_E_Max_HV, btn_HTR_E_Nominal_HV);
            UpdateSettings();
        }


        private void cb_RTD_Grade_A_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b = (m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_A_HV, m_DataUnit.Flag_Enable_RTD_Grade_A_HV_b);
            Update_ObjEnabledState(cb_RTD_Grade_A_HV, lbl_RTD_A_Min_HV, lbl_RTD_A_Max_HV, lbl_RTD_A_Nominal_HV, btn_RTD_A_Min_HV, btn_RTD_A_Max_HV, btn_RTD_A_Nominal_HV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_B_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b = (m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_B_HV, m_DataUnit.Flag_Enable_RTD_Grade_B_HV_b);
            Update_ObjEnabledState(cb_RTD_Grade_B_HV, lbl_RTD_B_Min_HV, lbl_RTD_B_Max_HV, lbl_RTD_B_Nominal_HV, btn_RTD_B_Min_HV, btn_RTD_B_Max_HV, btn_RTD_B_Nominal_HV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_C_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b = (m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_C_HV, m_DataUnit.Flag_Enable_RTD_Grade_C_HV_b);
            Update_ObjEnabledState(cb_RTD_Grade_C_HV, lbl_RTD_C_Min_HV, lbl_RTD_C_Max_HV, lbl_RTD_C_Nominal_HV, btn_RTD_C_Min_HV, btn_RTD_C_Max_HV, btn_RTD_C_Nominal_HV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_D_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b = (m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_D_HV, m_DataUnit.Flag_Enable_RTD_Grade_D_HV_b);
            Update_ObjEnabledState(cb_RTD_Grade_D_HV, lbl_RTD_D_Min_HV, lbl_RTD_D_Max_HV, lbl_RTD_D_Nominal_HV, btn_RTD_D_Min_HV, btn_RTD_D_Max_HV, btn_RTD_D_Nominal_HV);
            UpdateSettings();
        }

        private void cb_RTD_Grade_E_HV_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b = (m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b == true) ? false : true;
            Update_CheckBox(cb_RTD_Grade_E_HV, m_DataUnit.Flag_Enable_RTD_Grade_E_HV_b);
            Update_ObjEnabledState(cb_RTD_Grade_E_HV, lbl_RTD_E_Min_HV, lbl_RTD_E_Max_HV, lbl_RTD_E_Nominal_HV, btn_RTD_E_Min_HV, btn_RTD_E_Max_HV, btn_RTD_E_Nominal_HV);
            UpdateSettings();
        }
    }
}
