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
    public partial class UserCtrl_StatusStrip_String_Short : UserControl
    {
        //constructor//////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StatusStrip_String_Short()
        {
            InitializeComponent();
        }

        //Set Label///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Label(String strLabel) { lbl_Label.Text = strLabel; }
        public String Get_Label() { return lbl_Label.Text; }
    }
}
