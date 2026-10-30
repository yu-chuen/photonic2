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
    public partial class UserCtrl_UI_Cylinder_backup : UserControl
    {
        //variables
        Cylinder m_cylExtend = null;
		Cylinder m_cylRetract = null;
		SeqMachineControl m_SeqMachineCtrl;

        public String Label { get; set; }
        public String m_strRetBitID = "";
		public String m_strExtBitID = "";
		String m_strOp_Ext = "Ext";
		String m_strOp_Ret = "Ret";
        bool m_bExtended = false;

		bool m_bFullCyclinder = false;

        private readonly object m_lock_Access = new object();           //lock:

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_Cylinder_backup()
        {
			InitializeComponent();
			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Set cylinder pointer//////////////////////////////////////////////////////////////////////////////////////////
		//Note: String strOp_Ext = "Ext", String strOp_Ret = "Ret" is obsolete. Not used
		public void Set_CylinderPtr( Cylinder mcylExtend, Cylinder mcylRetract, String strOp_Ext = "Ext", String strOp_Ret = "Ret" ) 
        { 
            m_cylExtend = mcylExtend; 
            m_cylRetract = mcylRetract; 
            Label = m_cylExtend.Get_Label();

			m_strExtBitID = " (C" + mcylExtend.GetSolenoid().Get_SlaveID().ToString() + " " + mcylExtend.GetSolenoid().m_iBitID.ToString() + ")";
			m_strRetBitID = " (C" + mcylRetract.GetSolenoid().Get_SlaveID().ToString() + " " + mcylRetract.GetSolenoid().m_iBitID.ToString() + ")";

			m_strOp_Ext = mcylExtend.Get_ExtDesciption();
			m_strOp_Ret = mcylExtend.Get_RetDesciption();
        }

		public void Set_CylinderPtr( Cylinder mCylinder, String strOp_Ext = "Ext", String strOp_Ret = "Ret" )
		{
			m_cylExtend = mCylinder;
			Label = m_cylExtend.Get_Label();

			m_strExtBitID = " (C" + mCylinder.GetSolenoid().Get_SlaveID().ToString() + " " + mCylinder.GetSolenoid().m_iBitID.ToString() + ")";
			m_strRetBitID = " (C" + mCylinder.GetSolenoid_Ret().Get_SlaveID().ToString() + " " + mCylinder.GetSolenoid_Ret().m_iBitID.ToString() + ")";

			m_strOp_Ext = mCylinder.Get_ExtDesciption();
			m_strOp_Ret = mCylinder.Get_RetDesciption();
			m_bFullCyclinder = true;
			Get_OrderEndString();
		}

        //Get label end string/////////////////////////////////////////////////////////////////////////////////////////
        public void Get_OrderEndString()
        {
            String strlabel = "";
            String strBitID = "";
            int iStatus = 0;

            lock (m_lock_Access)
            {

                //iStatus = m_cylExtend.Get_CylinderStatus();
                iStatus = m_cylExtend.GetSolenoid().Read();
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
                //return (Label + strlabel + strBitID);
            }
        }

        //button event//////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_OutputBit_Click(object sender, EventArgs e)
        {
            lock (m_lock_Access)
            {

                //Interlock
                if (m_SeqMachineCtrl.m_ViewModel_Interlock.Cyclinder_Interlock(Label, ((m_bExtended == true) ? 0 : 1)) == false)
                {
                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                    return;
                }

                //Retract cylinder
                if (m_bFullCyclinder == false)
                {
                    if (m_bExtended)
                    {
                        m_cylExtend.Output_Off(); // off
                        m_cylRetract.Output_On();
                    }
                    //Extend Cylinder
                    else
                    {
                        m_cylExtend.Output_On(); // on
                        m_cylRetract.Output_Off();
                    }
                }
                else
                    m_cylExtend.Cyl_Trigger(((m_bExtended == true) ? false : true));

                //Check Cylinder state
                if (m_cylExtend.WaitTaskComplete() == false)
                {
                    //retract cyclinder------------------------------------------------------------//removed: for sensor tuning and to display the actual bit activation
                    //if ( m_bFullCyclinder == false )
                    //{
                    //    m_cylExtend.Output_Off();
                    //    m_cylRetract.Output_On();
                    //}
                    //else
                    //    m_cylExtend.Cyl_Trigger(false);
                    //----------------------------------------------------------------------------------

                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.Get_ErrorMessage(m_cylExtend.Get_ErrorID(), "", m_cylExtend.Get_Label()));
                }

            }

            //update button
            //btn_OutputBit.Text = Get_OrderEndString();
        }

        //dialog loaded/////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_UI_Cylinder_Load(object sender, EventArgs e)
        {
            //btn_OutputBit.Text = Get_OrderEndString();
            Get_OrderEndString();
        }
    }
}
