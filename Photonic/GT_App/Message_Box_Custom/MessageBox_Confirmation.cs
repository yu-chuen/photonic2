using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace GT_App
{
    public partial class MessageBox_Confirmation : Form
    {
        //variables
        
        public MessageBox_Confirmation()
        {
            InitializeComponent();
        }

        //Set title///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title(String strMsg) { this.Text = strMsg; }
        
        //Set Label/////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Label(String strLabel) { lbl_DisplayText.Text = strLabel; }
        
        //buttons////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}