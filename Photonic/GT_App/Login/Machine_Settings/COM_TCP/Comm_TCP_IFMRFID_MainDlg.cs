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
	public partial class Comm_TCP_IFMRFID_MainDlg : Form
	{
		//variables
		List<UserCtrl_TCP_IFMRFID_Dlg> m_list_userctrl_TCP_IFMRFID_Dlg = new List<UserCtrl_TCP_IFMRFID_Dlg>();
		List<TabPage> m_list_Tabpage = new List<TabPage>();

		//Constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public Comm_TCP_IFMRFID_MainDlg()
		{
			InitializeComponent();
		}

		//Dialog loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Comm_TCP_IFMRFID_MainDlg_Load( object sender, EventArgs e )
		{
			SeqMachineControl mpSeqMacCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			
			//Add User Control
			Add_UserCtrl(mpSeqMacCtrl.m_RFIDReader_SolderStation);
			Add_UserCtrl(mpSeqMacCtrl.m_RFIDReader_ManualStation);										


		}

		//Add User Ctrl///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Add_UserCtrl(Comm_TCP_RFID mComTCPRFID)
		{
			//user control add
			int iCount = m_list_userctrl_TCP_IFMRFID_Dlg.Count;
			m_list_userctrl_TCP_IFMRFID_Dlg.Add(new UserCtrl_TCP_IFMRFID_Dlg());
			m_list_userctrl_TCP_IFMRFID_Dlg[iCount].Set_RFIDPtr(mComTCPRFID);

			//tab page add
			m_list_Tabpage.Add(new TabPage());
			m_list_Tabpage[iCount].Controls.Add(m_list_userctrl_TCP_IFMRFID_Dlg[iCount]);
			m_list_Tabpage[iCount].Text = mComTCPRFID.m_strLabel;

			//tab control add
			this.tabCtrl_ComIFMScannerDlg.TabPages.Add(m_list_Tabpage[iCount]);
		}



		//ESC button/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		//Dialog closing
		private void Comm_TCP_IFMRFID_MainDlg_FormClosing( object sender, FormClosingEventArgs e )
		{
			//dispose user control
			foreach ( UserCtrl_TCP_IFMRFID_Dlg dlg in m_list_userctrl_TCP_IFMRFID_Dlg )
				dlg.Dispose();
		}




	}
}
