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
    public partial class UserCtrl_StatusStrip : UserControl
    {
        //Variables
        String m_strLabel = "";
        bool m_bFlag = false;

        //Constructor////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StatusStrip(String strLabel)
        {
            InitializeComponent();

            m_strLabel = strLabel;
        }

        //Dialog Loaded/////////////////////////////////////////////////////////////////////////
        private void UserCtrl_StatusStrip_Load(object sender, EventArgs e)
        {
            lbl_Description.Text = m_strLabel;
        }

        //Set Label////////////////////////////////////////////////////////////////////////////////
        public void Set_Label(String strLabel) { lbl_Description.Text = strLabel; }

        //Update Display//////////////////////////////////////////////////////////////////////
        public void Update_UI(bool bFlag)
        {
            //bypass if flags are the same
            if (bFlag == m_bFlag) return;
            
            //discard image
            pic_Status.Image.Dispose();
            pic_Status.Image = null;

            //update image
            if (bFlag)
                pic_Status.Image = GT_App.Properties.Resources.Green_Ball;
            else
                pic_Status.Image = GT_App.Properties.Resources.Red_Ball;

            m_bFlag = bFlag;
        }
    }
}
