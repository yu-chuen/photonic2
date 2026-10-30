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
    public partial class MessageBox_InputString : Form
    {
        //variables
        bool m_iFlag_Interger = false;
        bool m_iFlag_Double = false;

        public MessageBox_InputString()
        {
            InitializeComponent();
        }

        //Set title///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title(String strMsg) { this.Text = strMsg; }
        //Set input string////////////////////////////////////////////////////////////////////////////////////////
        public void Set_InputBoxValue(String strData) { txtb_InputData.Text = strData; }
        //Set Label/////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Label(String strLabel) { lbl_DisplayText.Text = strLabel; }
        //Set input as numeric///////////////////////////////////////////////////////////////////////////////////
        public void Set_Integer() { m_iFlag_Interger = true; m_iFlag_Double = false; }
        public void Set_Double() { m_iFlag_Double = true; m_iFlag_Interger = false; }
        public void Set_String() { m_iFlag_Double = false; m_iFlag_Interger = false; }

        //Get Data////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_DataString() { return txtb_InputData.Text; }
        public int Get_DataInt() { return int.Parse(txtb_InputData.Text); }
        public double Get_DataDouble() { return double.Parse(txtb_InputData.Text); }

        //Determine if the text are all numeric//////////////////////////////////////////////////////////////////////////////////////////
        private void Detect_Num(TextBox obj)
        {
            if (m_iFlag_Double == true)
            {
                int iErrorType = 0;
                bool bError = false;

                //error checking
                if (Class_Helper.Detect_DoubleOnly(obj.Text, ref iErrorType) == false)
                {
                    //clear alphabets
                    obj.Text = Regex.Replace(obj.Text, "[^0-9.-]", "");

                    //check error type and clear additional - or .
                    if (iErrorType == -2)           //addtional "."
                        obj.Text = obj.Text.Remove(obj.Text.LastIndexOf('.'), 1);
                    else if (iErrorType == -3)      //additional "-"
                        obj.Text = obj.Text.Remove(obj.Text.LastIndexOf('-'), 1);

                    bError = true;
                }

                //check for '.' at the front
                if (obj.Text.Contains("."))
                {
                    //add "0" into the front if the "." is typed without the 0
                    if (obj.Text.LastIndexOf('.') == 0) { obj.Text = "0" + obj.Text; bError = true; }
                    //add "0" into the front if the "." is typed without the 0 and there is a "-"
                    //if (obj.Text.LastIndexOf('.') == 1 && obj.Text.LastIndexOf('-') == 0)
                    //{ obj.Text = obj.Text.Insert(1, "0"); bError = true; }
                }
                //check for '-' in the middle
                if (obj.Text.Contains("-"))
                    if (obj.Text.LastIndexOf('-') != 0) { obj.Text = obj.Text.Remove(obj.Text.LastIndexOf('-'), 1); }

                //set cursor at the back of the text box if typo
                if (bError)
                {
                    obj.Focus();
                    obj.SelectionStart = obj.Text.Length;
                }
            }
            else
            {
                if (Class_Helper.Detect_IntegerOnly(obj.Text) == false)
                    obj.Text = Regex.Replace(obj.Text, "[^0-9-]", "");
            }
        }

        //Text Changed/////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtb_InputData_TextChanged(object sender, EventArgs e)
        {
            if (m_iFlag_Double == true || m_iFlag_Interger == true)
                this.Detect_Num(txtb_InputData);
        }

        //buttons////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_OK_Click(object sender, EventArgs e)
        {
            //adding a "0" in the front if there is only a "."
            if (m_iFlag_Double == true)
                txtb_InputData.Text = double.Parse(txtb_InputData.Text).ToString();
            if ( m_iFlag_Interger == true)
                txtb_InputData.Text = int.Parse(txtb_InputData.Text).ToString();

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
