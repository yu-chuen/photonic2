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
 * Notes: Interface only. To display the UserCtrl_DelayNTimeoutsCylinderStrip_Dlg control
 **********************************************************************************************************************************/

namespace GT_App
{
    public partial class UserCtrl_DelayNTimeoutsInterface_Dlg : UserControl
    {
        //variables
        List<UserCtrl_DelayNTimeoutsCylinderStrip_Dlg> m_llUserCtrl_CylinderStrip;
        List<UserCtrl_DelayNTimeoutsSensorStrip_Dlg> m_llUserCtrl_SensorStrip;
        Point m_Point = new Point(0,0);
        
        public UserCtrl_DelayNTimeoutsInterface_Dlg()
        {
            InitializeComponent();

            m_llUserCtrl_CylinderStrip = new List<UserCtrl_DelayNTimeoutsCylinderStrip_Dlg>();
            m_llUserCtrl_SensorStrip = new List<UserCtrl_DelayNTimeoutsSensorStrip_Dlg>();
        }

        //add cylinder - delay and timeout strip/////////////////////////////////////////////////////////////////////////////////
        public bool Add_Cylinder(Cylinder mCylinder)
        {
            UserCtrl_DelayNTimeoutsCylinderStrip_Dlg dlg = new UserCtrl_DelayNTimeoutsCylinderStrip_Dlg(mCylinder);
            m_llUserCtrl_CylinderStrip.Add(dlg);

            dlg.Location = m_Point;
            this.Controls.Add(m_llUserCtrl_CylinderStrip[m_llUserCtrl_CylinderStrip.Count - 1]);
            m_Point.Y += dlg.Size.Height;
            
            return true;
        }

        //add sensor - delay and timeout strip//////////////////////////////////////////////////////////////////////////////////
        public bool Add_Sensor(Sensor mSensor)
        {
            UserCtrl_DelayNTimeoutsSensorStrip_Dlg dlg = new UserCtrl_DelayNTimeoutsSensorStrip_Dlg(mSensor);
            m_llUserCtrl_SensorStrip.Add(dlg);

            dlg.Location = m_Point;
            this.Controls.Add(m_llUserCtrl_SensorStrip[m_llUserCtrl_SensorStrip.Count - 1]);
            m_Point.Y += dlg.Size.Height;
            
            return true;
        }
    }
}
