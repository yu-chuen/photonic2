using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class UserCtrl_UI_InputBit : UserControl
    {
        //variables
        InputBit m_InputBit = null;

        int m_iFlag_InputBit = 0;
        public String Label { get; set; }
        public String m_strBitID = "";
        int m_iTest = 0;

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_InputBit()
        {
            InitializeComponent();
        }

        //Set Input bit ptr/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_InputBitPtr(InputBit mInput) 
        { 
            
            m_InputBit = mInput; 
            Label = m_InputBit.Label;
			m_strBitID = " (C" + m_InputBit.Get_SlaveID().ToString() + " " + m_InputBit.m_iBitID.ToString() + ")";
        }

        //Update PicBox UI///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_PicBox()
        {
            int iFlag = 0;

            iFlag = m_InputBit.Read();
            //if (m_iFlag_InputBit != iFlag)
            {
                pic_Indicator.Image.Dispose();

                if (iFlag == 1) pic_Indicator.Image = GT_App.Properties.Resources.Green_Ball;
                else pic_Indicator.Image = GT_App.Properties.Resources.Red_Ball;

                if (iFlag == 1) lbl_Label.Text = Label + " On";
                else lbl_Label.Text = Label + " Off";

                lbl_Label.Text += m_strBitID;

                this.Update();
            }
            //else if (iFlag == -1 && m_iTest == 0)
            //{
            //    m_iTest = 1;
            //    MessageBox.Show(Label + " Error");
            //}
            m_iFlag_InputBit = iFlag;
        }

        //Dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_UI_InputBit_Load(object sender, EventArgs e)
        {
            //lbl_Label.Text = Label;
            this.Update_PicBox();
        }

    }
}
