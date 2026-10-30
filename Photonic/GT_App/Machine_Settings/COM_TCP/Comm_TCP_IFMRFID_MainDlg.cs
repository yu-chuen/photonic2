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
        UserCtrl_TCP_IFMRFID_Dlg m_userctrl_IFMScannerDlg_OP60_OP85_Station = null;
        UserCtrl_TCP_IFMRFID_Dlg m_userctrl_IFMScannerDlg_OP90_OP110_Station = null;

        //Constructor//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public Comm_TCP_IFMRFID_MainDlg()
        {
            InitializeComponent();
        }

        //Dialog loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Comm_TCP_IFMRFID_MainDlg_Load(object sender, EventArgs e)
        {
            SeqMachineControl mpSeqMacCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            TabPage mTab1 = new TabPage();
            TabPage mTab2 = new TabPage();

            m_userctrl_IFMScannerDlg_OP60_OP85_Station = new UserCtrl_TCP_IFMRFID_Dlg();
            m_userctrl_IFMScannerDlg_OP60_OP85_Station.Set_RFIDPtr(mpSeqMacCtrl.m_RFIDReader_OP60_OP85);
            mTab1.Controls.Add(m_userctrl_IFMScannerDlg_OP60_OP85_Station);
            mTab1.Text = mpSeqMacCtrl.m_RFIDReader_OP60_OP85.m_strLabel;
            this.tabCtrl_ComIFMScannerDlg.TabPages.Add(mTab1);

            m_userctrl_IFMScannerDlg_OP90_OP110_Station = new UserCtrl_TCP_IFMRFID_Dlg();
            m_userctrl_IFMScannerDlg_OP90_OP110_Station.Set_RFIDPtr(mpSeqMacCtrl.m_RFIDReader_OP90_OP110);
            mTab2.Controls.Add(m_userctrl_IFMScannerDlg_OP90_OP110_Station);
            mTab2.Text = mpSeqMacCtrl.m_RFIDReader_OP90_OP110.m_strLabel;
            this.tabCtrl_ComIFMScannerDlg.TabPages.Add(mTab2);
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}