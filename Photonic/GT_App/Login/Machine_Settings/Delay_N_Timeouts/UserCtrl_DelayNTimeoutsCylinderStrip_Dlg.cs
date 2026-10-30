using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

/**********************************************************************************************************************************
 * Notes: Common class shared between all delay and timeout peripherals (cylinders).
 *              This is class is used to set the delays and timeouts of the peripherals.
 **********************************************************************************************************************************/

namespace GT_App
{
    public partial class UserCtrl_DelayNTimeoutsCylinderStrip_Dlg : UserControl
    {
        //variables
        Cylinder m_Cylinder = null;
        

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_DelayNTimeoutsCylinderStrip_Dlg(Cylinder mCylinder)
        {
            InitializeComponent();
            m_Cylinder = mCylinder;
        }

        //dialog load///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_DelayNTimeoutsStrip_Dlg_Load(object sender, EventArgs e)
        {
            lbl_DelayExtend.Text = m_Cylinder.m_Settings.m_iDelay_On.ToString();
            lbl_DelayRetract.Text = m_Cylinder.m_Settings.m_iDelay_Off.ToString();
            lbl_TimeOutExtend.Text = m_Cylinder.m_Settings.m_iTimeOut_On.ToString();
            lbl_TimeOutRetract.Text = m_Cylinder.m_Settings.m_iTimeOut_Off.ToString();
            lbl_Title.Text = m_Cylinder.Get_Label();
        }

        //Set label and update cylinder's settings////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_Data(Label lbl_Data, String strlabel)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Title(m_Cylinder.Get_Label());
            dlg.Set_Label(strlabel);
            dlg.Set_InputBoxValue(lbl_Data.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (strlabel)
                {
                    case "Cylinder Extend": m_Cylinder.m_Settings.m_iDelay_On = dlg.Get_DataInt();
                        break;

                    case "Cylinder Retract": m_Cylinder.m_Settings.m_iDelay_Off = dlg.Get_DataInt();
                        break;

                    case "Time Out Extend": m_Cylinder.m_Settings.m_iTimeOut_On = dlg.Get_DataInt();
                        break;

                    case "Time Out Retract": m_Cylinder.m_Settings.m_iTimeOut_Off = dlg.Get_DataInt();
                        break;
                }
                
                m_Cylinder.To_Registry();
                lbl_Data.Text = dlg.Get_DataDouble().ToString();
            }
        }

        //btn control////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_DelayExtend_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayExtend, "Cylinder Extend"); 
        }

        private void btn_DelayRetract_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayRetract, "Cylinder Retract");
        }

        private void btn_TimeOutExtend_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_TimeOutExtend, "Time Out Extend");
        }

        private void btn_TimeOutRetract_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_TimeOutRetract, "Time Out Retract");
        }
    }
}
