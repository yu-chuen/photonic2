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
    public partial class UserCtrl_DelayNTimeoutsMiscInterface : UserControl
    {
        //Variables
        SeqMachineControl m_SeqMachineControl = null;
        MachineInfo m_MachineInfo = null;

        List<UserCtrl_DelayNTimeoutsMisc> m_llUserCtrl_Mis = new List<UserCtrl_DelayNTimeoutsMisc>();
        Point m_Point = new Point(0, 0);

        //Constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_DelayNTimeoutsMiscInterface()
        {
            InitializeComponent();
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_MachineInfo = m_SeqMachineControl.m_MachineInfo;
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_DelayNTimeoutsMiscInterface_Load(object sender, EventArgs e)
        {
			//Add_UserCtrl("Last Unit Time Out", (int)m_MachineInfo.m_lg_TimeOutPartatWorkzone);
			//Add_UserCtrl("Laser Marker Timeout", (int)m_MachineInfo.m_lg_TimeOutLaserMarker);
			//Add_UserCtrl("Fume Extractor Off (minute)", m_MachineInfo.m_i_TimeOutTurnOffFumeExtractor, "min");
        }

        //Add User Control Page/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Add_UserCtrl(String strLabel, int iValue, String strIdentifier = "")
        {
            UserCtrl_DelayNTimeoutsMisc dlg = new UserCtrl_DelayNTimeoutsMisc(strLabel, iValue);

            //Adding identifier
            if (strIdentifier != "") dlg.Set_Identifier(strIdentifier);

            m_llUserCtrl_Mis.Add(dlg);

            dlg.Location = m_Point;
            this.Controls.Add(m_llUserCtrl_Mis[m_llUserCtrl_Mis.Count - 1]);
            m_Point.Y += dlg.Size.Height;
        }

        //Button Event////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Update_Click(object sender, EventArgs e)
        {
            //loop through the list to update variables
            foreach (UserCtrl_DelayNTimeoutsMisc dlg in m_llUserCtrl_Mis)
            {
                switch (dlg.m_strLabel)
                {
					//case "Last Unit Time Out": m_MachineInfo.m_lg_TimeOutPartatWorkzone = (long)dlg.Get_Value(); break;
					//case "Laser Marker Timeout": m_MachineInfo.m_lg_TimeOutLaserMarker = (long)dlg.Get_Value(); break;
					//case "Fume Extractor Off (minute)": m_MachineInfo.m_i_TimeOutTurnOffFumeExtractor = dlg.Get_Value(); break;

					default: break;
                }
            }

            m_SeqMachineControl.MachineInfo_To_Registry();
        }
    }
}
