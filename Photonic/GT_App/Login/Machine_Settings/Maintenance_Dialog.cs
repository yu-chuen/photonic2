using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class Maintenance_Dialog : Form
    {
        //variables

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Maintenance_Dialog()
        {
            InitializeComponent();
        }

        //button events///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_DelayNTimeOuts_Click(object sender, EventArgs e)
        {
            DelayAndTimeOut_Dialog dlg = new DelayAndTimeOut_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_MachineInfo_Click(object sender, EventArgs e)
        {
            MachineInfo_Dialog dlg = new MachineInfo_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_ChangeLoginPassword_Click(object sender, EventArgs e)
        {
            Login_AddUser_Dialog dlg = new Login_AddUser_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_ChangeProcessFlag_Click(object sender, EventArgs e)
        {
            ChangeProcessFlag_Dialog dlg = new ChangeProcessFlag_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_DefineProductData_Click(object sender, EventArgs e)
        {
            DefineProductData_Dialog dlg = new DefineProductData_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

		private void btn_DefineProductMenu_Click( object sender, EventArgs e )
		{
			DefineProductData_Dialog dlg = new DefineProductData_Dialog();
			dlg.ShowDialog();
			dlg.Dispose();
		}

		private void btn_IFMRFIDReader_Click( object sender, EventArgs e )
		{
			Comm_TCP_IFMRFID_MainDlg dlg = new Comm_TCP_IFMRFID_MainDlg();
			dlg.ShowDialog();
			dlg.Dispose();
		}

		private void btn_WireTesterDlg_Click( object sender, EventArgs e )
		{
			WireTester_Hioki_Dialog dlg = new WireTester_Hioki_Dialog();
			dlg.ShowDialog();
			dlg.Dispose();
		}

		private void btn_VisionSettings_Click( object sender, EventArgs e )
		{
			Vision_ControlDialog dlg = new Vision_ControlDialog();
			dlg.ShowDialog();
			dlg.Dispose();
		}

		private void btn_ServerSettingsDlg_Click( object sender, EventArgs e )
		{
			Server_COMTCP_GT_CentralPC_Dlg dlg = new Server_COMTCP_GT_CentralPC_Dlg();
			dlg.ShowDialog();
			dlg.Dispose();
		}

        private void btn_Thermocouple_Click(object sender, EventArgs e)
        {
            Thermocouple_Serial_MainDialog dlg = new Thermocouple_Serial_MainDialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

        private void btn_KeyanceIL_Click(object sender, EventArgs e)
        {
            KeyanceIL_Serial_MainDialog dlg = new KeyanceIL_Serial_MainDialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }
    }
}
