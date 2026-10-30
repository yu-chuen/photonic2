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
    public partial class MessageBox_InputTime : Form
    {
        //variables
        String m_strTime = "0:0";

        //Constructor/////////////////////////////////////////////////////////////////////////////////
        public MessageBox_InputTime()
        {
            InitializeComponent();
        }

        //Set Time/////////////////////////////////////////////////////////////////////////////////////
        public void Set_StringTime(String strTime) { m_strTime = strTime; }
        public void Set_Label(String strLabel) { lbl_Title.Text = strLabel; }

        //Get Data/////////////////////////////////////////////////////////////////////////////////////
        public int Get_Hour() { return (int)numericbox_Hour.Value; }
        public int Get_Minute() { return (int)numericbox_minute.Value; }

        //Dialog loaded////////////////////////////////////////////////////////////////////////////
        private void MessageBox_InputTime_Load(object sender, EventArgs e)
        {
            String[] strData = new String[2];

            strData = m_strTime.Split(':');

            numericbox_Hour.Value = int.Parse(strData[0]);
            numericbox_minute.Value = int.Parse(strData[1]);
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }
    }
}
