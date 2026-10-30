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
    public partial class UserCtrl_DelayNTimeoutsSensorStrip_Dlg : UserControl
    {
        //variables
        Sensor m_Sensor = null;
        
        //constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_DelayNTimeoutsSensorStrip_Dlg(Sensor mSensor)
        {
            InitializeComponent();
            m_Sensor = mSensor;
        }

        //Set label and update cylinder's settings////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_DelayNTimeoutsSensorStrip_Dlg_Load(object sender, EventArgs e)
        {
            lbl_Title.Text = m_Sensor.Get_Label();
            lbl_DelayOn.Text = m_Sensor.m_Settings.m_iDelay_On.ToString();
            lbl_DelayOff.Text = m_Sensor.m_Settings.m_iDelay_Off.ToString();
        }

        //Set label and update sensor's settings////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_Data(Label lbl_Data, String strlabel)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Title(m_Sensor.Get_Label());
            dlg.Set_Label(strlabel);
            dlg.Set_InputBoxValue(lbl_Data.Text);

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                switch (strlabel)
                {
                    case "Sensor Delay On":
                        {
                            m_Sensor.m_Settings.m_iDelay_On = dlg.Get_DataInt();
                        }
                        break;

                    case "Sensor Delay Off":
                        {
                            m_Sensor.m_Settings.m_iDelay_Off = dlg.Get_DataInt();
                        }
                        break;
                }

                m_Sensor.To_Registry();
                lbl_Data.Text = dlg.Get_DataDouble().ToString();
            }
        }

        //button event////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_DelayOn_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayOn, "Sensor Delay On"); 
        }

        private void btn_DelayOff_Click(object sender, EventArgs e)
        {
            this.Update_Data(lbl_DelayOff, "Sensor Delay Off");
        }
    }
}