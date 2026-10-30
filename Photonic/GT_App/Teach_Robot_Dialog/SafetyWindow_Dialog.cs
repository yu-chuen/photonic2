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
    public partial class SafetyWindow_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Data_Robot m_DataRobot;
        Data_Robot m_DataRobot_XYaxis;

        String m_strPositionName = "";

        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////
        public SafetyWindow_Dialog()
        {
            InitializeComponent();

            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////
        private void SafetyWindow_Dialog_Load(object sender, EventArgs e)
        {
            Robot_Pos_Settings mSettings;

            //get position from robot data
            mSettings = m_DataRobot.Get_Settings(m_strPositionName);

            //populate label box
            lbl_TRX.Text = mSettings.m_dbSafetyWindow_TR_X.ToString();
            lbl_TRY.Text = mSettings.m_dbSafetyWindow_TR_Y.ToString();
            lbl_BLX.Text = mSettings.m_dbSafetyWindow_BL_X.ToString();
            lbl_BLY.Text = mSettings.m_dbSafetyWindow_BL_Y.ToString();

            lbl_PositionName.Text = m_strPositionName;
            lbl_PositionDesc.Text = mSettings.m_strPosDesc;
        }

        //Set/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_DataRobot_Ptr(Data_Robot mDataRobot) { m_DataRobot = mDataRobot; }
        //public void Set_DataRobot_Ptr(Data_Robot mDataRobot_XYaxis) { m_DataRobot_XYaxis = mDataRobot_XYaxis; }
        public void Set_PositionName(String strPos) { m_strPositionName = strPos; }

        //Support button events/////////////////////////////////////////////////////////////////////////////////////////
        private void Update_LabelBox(Label obj, String strDesc)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Double();
            dlg.Set_InputBoxValue(obj.Text);
            dlg.Set_Label(strDesc);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                obj.Text = dlg.Get_DataDouble().ToString();
            }

            dlg.Dispose();

            return;
        }

        //Button Events/////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_UpdateEsc_Click(object sender, EventArgs e)
        {
            Robot_Pos_Settings mSettings;

            //get position from robot data
            mSettings = m_DataRobot.Get_Settings(m_strPositionName);

            //equate all values
            mSettings.m_dbSafetyWindow_TR_X = double.Parse(lbl_TRX.Text);
            mSettings.m_dbSafetyWindow_TR_Y = double.Parse(lbl_TRY.Text);
            mSettings.m_dbSafetyWindow_BL_X = double.Parse(lbl_BLX.Text);
            mSettings.m_dbSafetyWindow_BL_Y = double.Parse(lbl_BLY.Text);

            //update package file
            m_DataRobot.Update_Settings(mSettings);
            m_SeqMachineCtrl.Pkg_To_Registry();
            this.Close();
        }

        private void btn_ESC_Click(object sender, EventArgs e) { this.Close(); }

        private void btn_TRX_Click(object sender, EventArgs e) { Update_LabelBox(lbl_TRX, "Top Right X"); }
        private void btn_TRY_Click(object sender, EventArgs e) { Update_LabelBox(lbl_TRY, "Top Right Y"); }
        private void btn_BLX_Click(object sender, EventArgs e) { Update_LabelBox(lbl_BLX, "Bottom Left X"); }
        private void btn_BLY_Click(object sender, EventArgs e) { Update_LabelBox(lbl_BLY, "Bottom Left Y"); }
    }
}
