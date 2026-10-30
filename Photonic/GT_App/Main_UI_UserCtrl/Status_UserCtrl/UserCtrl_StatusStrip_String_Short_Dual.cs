using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class UserCtrl_StatusStrip_String_Short_Dual : UserControl
    {
        //variables
        string m_strTitle = "...";
        Color m_clrTitle_ForeColor = Color.Black;
        Color m_clrTitle_BackColor = Color.Transparent;

        //constructor//////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StatusStrip_String_Short_Dual(string strTitle, Color clrTitle_ForeColor, Color clrTitle_BackColor)
        {
            InitializeComponent();

            m_strTitle = strTitle;
            m_clrTitle_ForeColor = clrTitle_ForeColor;
            m_clrTitle_BackColor = clrTitle_BackColor;
        }

        private void UserCtrl_StatusStrip_String_Short_Dual_Load(object sender, EventArgs e)
        {
            Set_Title_Label(m_strTitle);
            Set_Title_ForeColor(m_clrTitle_ForeColor);
            Set_Title_BackColor(m_clrTitle_BackColor);
        }

        public String Get_Result_Label() { return lbl_Result.Text; }

        //Set Label///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title_Label(String strLabel) { lbl_Title.Text = strLabel; }
        public void Set_Result_Label(String strLabel) { lbl_Result.Text = strLabel; }

        //Set Color///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title_ForeColor(Color clrColor) { lbl_Title.ForeColor = clrColor; }
        public void Set_Title_BackColor(Color clrColor) { lbl_Title.BackColor = clrColor; }
        public void Set_Result_ForeColor(Color clrColor) { lbl_Result.ForeColor = clrColor; }
        public void Set_Result_BackColor(Color clrColor) { lbl_Result.BackColor = clrColor; }

        
    }
}