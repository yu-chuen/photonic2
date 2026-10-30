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
    public partial class UserCtrl_UI_Cylinder : UserControl
    {
        //variables
		SeqMachineControl m_SeqMachineCtrl;
        Cylinder m_Cylinder = null;

        public String Label { get; set; }
        public String m_strRetBitID = "";
		public String m_strExtBitID = "";
		String m_strOp_Ext = "Ext";
		String m_strOp_Ret = "Ret";
        bool m_bExtended = false;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_Cylinder()
        {
			InitializeComponent();
			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Set cylinder pointer//////////////////////////////////////////////////////////////////////////////////////////
		public void Set_CylinderPtr( Cylinder mCylinder, String strOp_Ext = "Ext", String strOp_Ret = "Ret")
		{
            m_Cylinder = mCylinder;
            Label = m_Cylinder.Get_Label();

            m_strExtBitID = " (C" + mCylinder.Get_Work_Output().Get_SlaveID().ToString() + " " + mCylinder.Get_Work_Output().m_iBitID.ToString() + ")";
            m_strRetBitID = " (C" + mCylinder.Get_Home_Output().Get_SlaveID().ToString() + " " + mCylinder.Get_Home_Output().m_iBitID.ToString() + ")";

            m_strOp_Ext = mCylinder.Get_Work_Desciption();
            m_strOp_Ret = mCylinder.Get_Home_Desciption();

			btn_OutputBit.Text = Get_OrderEndString();
		}

        //Get label end string/////////////////////////////////////////////////////////////////////////////////////////
        private String Get_OrderEndString()
        {
            String strlabel = "";
            String strBitID = "";
            int iStatus = 0;

            //iStatus = m_Cylinder.Get_Cylinder_Status();
            iStatus = m_Cylinder.Get_Work_Output().Read();
            if (iStatus == 1)
            {
                m_bExtended = true;
                strlabel = " " + m_strOp_Ext;
                btn_OutputBit.BackColor = Color.Red;
                strBitID = m_strExtBitID;
            }
            else if (iStatus == 0)
            {
                m_bExtended = false;
                strlabel = " " + m_strOp_Ret;
                btn_OutputBit.BackColor = Color.Green;
                strBitID = m_strRetBitID;
            }
            else
            {
                strlabel = " unknown";
            }

            //Set to button text
            btn_OutputBit.Text = (Label + strlabel + strBitID);

            //return string
            return (Label + strlabel + strBitID);
        }

        //button event//////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_OutputBit_Click(object sender, EventArgs e)
        {
            //Interlock
            if (m_SeqMachineCtrl.m_ViewModel_Interlock.Cylinder_Interlock(Label, ((m_bExtended == true) ? 1 : 0)) == false)
            {
                Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                return;
            }

            //trigger cylinder
            m_Cylinder.Cyl_Trigger(((m_bExtended == true) ? false : true));

            //Check Cylinder state
            if (m_Cylinder.WaitTaskComplete() == false)
            {
                Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.Get_ErrorMessage(m_Cylinder.Get_ErrorID(), "", m_Cylinder.Get_Label()));
            }

            btn_OutputBit.Text = Get_OrderEndString();
        }

        //dialog loaded/////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_UI_Cylinder_Load(object sender, EventArgs e)
        {
            btn_OutputBit.Text = Get_OrderEndString();
        }
    }
}