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
    public partial class UserCtrl_UI_FlagBox : UserControl
    {
        //variables
        String m_strLableMainText = "";

        //Constructor/////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_FlagBox(String strText)
        {
            InitializeComponent();

            Set_Text(strText);
        }

        //Set Picture box and text////////////////////////////////////////////////////////////////
        public void Set_Text(String strText) { m_strLableMainText = strText; }

        //Update Picture Box and text////////////////////////////////////////////////////////
        public void Update_UI(bool bFlag)
        {
            pictureBox_Flag.Image.Dispose();

            //Set
            if (bFlag == true)
            {
                lbl_Text.Text = m_strLableMainText + " On";
                pictureBox_Flag.Image = GT_App.Properties.Resources.Green_Ball;
            }
            else
            {
                lbl_Text.Text = m_strLableMainText + " Off";
                pictureBox_Flag.Image = GT_App.Properties.Resources.Red_Ball;
            }
        }
    }
}
