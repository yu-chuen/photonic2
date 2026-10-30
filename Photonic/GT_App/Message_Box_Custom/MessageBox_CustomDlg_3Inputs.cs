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
    public partial class MessageBox_CustomDlg_3Inputs : Form
    {
        //variables

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MessageBox_CustomDlg_3Inputs()
        {
            InitializeComponent();
        }

        //Set functions/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title(String strTitle) { this.Text = strTitle; }
        public void Set_Message(String strMsg) { lbl_ErrorMsg.Text = strMsg; }

        //Button Events////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Fail_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.No;
        }

        private void btn_Redo_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Retry;
        }

        private void btn_Pass_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
        }
    }
}
