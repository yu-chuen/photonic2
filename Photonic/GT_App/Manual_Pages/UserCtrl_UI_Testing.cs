using StaticCoreLib;
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
    public interface IOutputBitUI
    {
        void Set_OutputBitPtr(OutputBit mOutBit);
        void Set_OrderReverse();
        void Get_OrderEndString();
    }
    public partial class UserCtrl_UI_Testing : UserControl, IOutputBitUI
    {
        //variables
        OutputBit m_OutBit = null;
        SeqMachineControl m_SeqMachineCtrl;

        bool m_bFlag_Reverse = false;
        bool m_blocked = false;

        public String Label { get; set; }
        public String m_strBitID = "";

        private readonly Image m_imgOn = GT_App.Properties.Resources.Green_Ball;
        private readonly Image m_imgOff = GT_App.Properties.Resources.Red_Ball;

        private readonly object m_lock_Access = new object();

        public UserCtrl_UI_Testing()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        private void UserCtrl_UI_OutputBit_Load(object sender, EventArgs e)
        {
            Get_OrderEndString();
        }

        public void Set_OutputBitPtr(OutputBit mOutBit)
        {
            m_OutBit = mOutBit;
            Label = m_OutBit.Label;
            m_strBitID = " (C" + m_OutBit.Get_SlaveID().ToString() + " " + m_OutBit.m_iBitID.ToString() + ")";
        }

        //Set order reverse
        public void Set_OrderReverse() { m_bFlag_Reverse = true; }
        //Get label end string/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Get_OrderEndString()
        {
            lock (m_lock_Access)
            {
                int iRead = m_OutBit.Read();
                bool bActive = (m_bFlag_Reverse == true) ? (iRead != 1) : (iRead == 1);

                btn_OutputBit.Text = Label + (bActive ? " On" : " Off") + m_strBitID;

                Image imgNew = bActive ? m_imgOn : m_imgOff;
                if (pic_Indicator.Image != imgNew) pic_Indicator.Image = imgNew;
            }
            //return (Label + strlabel + m_strBitID);
        }

        private void btn_OutputBit_Click(object sender, EventArgs e)
        {
            lock (m_lock_Access)
            {
                //Console.WriteLine(m_OutBit.Read());
                //interlock
                if (m_SeqMachineCtrl.m_ViewModel_Interlock.OutputBit_Interlock(this.Label, ((m_OutBit.Read() == 1) ? 0 : 1)) == false)
                {
                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                    return;
                }

                //toggle output bit
                m_OutBit.Toggle();
            }
        }

        private void pic_Indicator_Click(object sender, EventArgs e)
        {
            btn_OutputBit.PerformClick();
        }
    }
}
