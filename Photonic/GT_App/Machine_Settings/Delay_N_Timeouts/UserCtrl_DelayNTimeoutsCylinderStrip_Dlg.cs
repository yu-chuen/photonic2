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
            lbl_Title.Text = m_Cylinder.Get_Label();
            lbl_DelayBeforeWork_Trigger.Text = m_Cylinder.m_Settings.m_iDelay_Before_Trigger_Work.ToString();
            lbl_DelayBeforeHome_Trigger.Text = m_Cylinder.m_Settings.m_iDelay_Before_Trigger_Home.ToString();
            lbl_DelayWork_SensorON.Text = m_Cylinder.m_Settings.m_iDelay_Sensor_Work_On.ToString();
            lbl_DelayHome_SensorON.Text = m_Cylinder.m_Settings.m_iDelay_Sensor_Home_On.ToString();
            lbl_Timeout.Text = m_Cylinder.m_Settings.m_iTimeout.ToString();
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
                    case "Delay Before Trigger Work":
                        {
                            m_Cylinder.m_Settings.m_iDelay_Before_Trigger_Work = dlg.Get_DataInt(); 
                        }
                        break;

                    case "Delay Before Trigger Home":
                        {
                            m_Cylinder.m_Settings.m_iDelay_Before_Trigger_Home = dlg.Get_DataInt();
                        }
                        break;

                    case "Delay Work Sensor ON":
                        {
                            m_Cylinder.m_Settings.m_iDelay_Sensor_Work_On = dlg.Get_DataInt();
                        }
                        break;

                    case "Delay Home Sensor ON":
                        {
                            m_Cylinder.m_Settings.m_iDelay_Sensor_Home_On = dlg.Get_DataInt();
                        }
                        break;

                    case "Timeout":
                        {
                            m_Cylinder.m_Settings.m_iTimeout = dlg.Get_DataInt();
                        }
                        break;
                }
                
                m_Cylinder.To_Registry();
                lbl_Data.Text = dlg.Get_DataDouble().ToString();
            }
        }

        //btn control////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_DelayBeforeWork_Trigger_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayBeforeWork_Trigger, "Delay Before Trigger Work");
        }

        private void btn_DelayBeforeHome_Trigger_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayBeforeHome_Trigger, "Delay Before Trigger Home");
        }

        private void btn_DelayWork_SensorON_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayWork_SensorON, "Delay Work Sensor ON");
        }

        private void btn_DelayHome_SensorON_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayHome_SensorON, "Delay Home Sensor ON");
        }

        private void btn_Timeout_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_Timeout, "Timeout");
        }
    }
}
