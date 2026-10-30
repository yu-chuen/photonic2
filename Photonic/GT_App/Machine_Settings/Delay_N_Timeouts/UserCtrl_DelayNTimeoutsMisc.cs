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
    public partial class UserCtrl_DelayNTimeoutsMisc : UserControl
    {
        //Variables
        SeqMachineControl m_seqMachineControl = null;

        public String m_strLabel = "";
        int m_iValue = 0;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_DelayNTimeoutsMisc(String strLabel, int iValue)
        {
            InitializeComponent();
            m_seqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            Set_Values(strLabel, iValue);
        }

        //Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_DelayNTimeoutsMisc_Load(object sender, EventArgs e)
        {
            lbl_Delay.Text = m_iValue.ToString();
            lbl_Label.Text = m_strLabel;
        }

        //Set values//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Values(String strLabel, int iValue)
        {
            m_strLabel = strLabel;
            m_iValue = iValue;
        }

        public int Get_Value() { return m_iValue; }

        public void Set_Identifier(String str) { lbl_Identifier.Text = str; }

        //Button Events////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_DelayExtend_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Title(m_strLabel);
            dlg.Set_Label(m_strLabel + " Delay (ms)");
            dlg.Set_InputBoxValue(m_iValue.ToString());

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                m_iValue = dlg.Get_DataInt();
                m_seqMachineControl.MachineInfo_To_Registry();
                lbl_Delay.Text = m_iValue.ToString();
            }
        }
    }
}
