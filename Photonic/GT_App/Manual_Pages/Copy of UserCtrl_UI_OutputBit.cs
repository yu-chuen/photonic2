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
    public partial class 
        UserCtrl_UI_OutputBit_backup : UserControl
    {
        //variables
		OutputBit m_OutBit = null;
		SeqMachineControl m_SeqMachineCtrl;

        //int m_iFlag_OutputBit = 0;
        bool m_bFlag_Reverse = false;
        public String Label { get; set; }
        public String m_strBitID = "";

        private readonly object m_lock_Access = new object();

        //Constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_OutputBit_backup()
        {
			InitializeComponent();
			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_UI_OutputBit_Load(object sender, EventArgs e)
        {
            //btn_OutputBit.Text = Get_OrderEndString();
            Get_OrderEndString();
        }

        //Set output Bit ptr///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
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
                String strlabel = "";

                if (m_bFlag_Reverse == true)
                {
                    if (m_OutBit.Read() == 1)
                    {
                        strlabel = " Off";
                        btn_OutputBit.BackColor = Color.Green;
                    }
                    else
                    {
                        strlabel = " On";
                        btn_OutputBit.BackColor = Color.Red;
                    }
                }
                else
                {
                    if (m_OutBit.Read() == 1)
                    {
                        strlabel = " On";
                        btn_OutputBit.BackColor = Color.Red;
                    }
                    else
                    {
                        strlabel = " Off";
                        btn_OutputBit.BackColor = Color.Green;
                    }
                }

                //update button
                btn_OutputBit.Text = Label + strlabel + m_strBitID;
            }
             //return (Label + strlabel + m_strBitID);
        }

        //Button Event//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_OutputBit_Click(object sender, EventArgs e)
        {
            lock (m_lock_Access)
            {
                //interlock
                if (m_SeqMachineCtrl.m_ViewModel_Interlock.OutputBit_Interlock(this.Label, ((m_OutBit.Read() == 1) ? 0 : 1)) == false)
                {
                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                    return;
                }

                //toggle output bit
                m_OutBit.Toggle();
                //btn_OutputBit.Text = Get_OrderEndString();
            }
        }
    }
}
