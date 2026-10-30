using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
    public partial class UserCtrl_UI_Dual_OutputBit : UserControl
    {
        //variables
        private SeqMachineControl m_SeqMachineCtrl = null;
        private List<bool> m_list_State = null; //state identifier to switch trigger
        private Action<bool> _action = null; //function from other class to pass into this class, need to use delegate()
        
        private OutputBit m_Output_1 = null;
        private OutputBit m_Output_2 = null;

        private string m_strBitID_1 = "";
        private string m_strBitID_2 = "";

        private string strOutputLabel_1 { get; set; }
        private string strOutputLabel_2 { get; set; }

        private readonly object m_lock_Access = new object();

        //Constructor//////////////////////////////////////////////////////////////////////////
        public UserCtrl_UI_Dual_OutputBit(OutputBit mOutput1, OutputBit mOutput2, List<bool> mState, Action<bool> act)
        {
			InitializeComponent();
			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            m_Output_1 = mOutput1;
            m_Output_2 = mOutput2;

            strOutputLabel_1 = m_Output_1.Label;
            strOutputLabel_2 = m_Output_2.Label;

            m_strBitID_1 = " (C" + m_Output_1.Get_SlaveID().ToString() + " " + m_Output_1.m_iBitID.ToString() + ")";
            m_strBitID_2 = " (C" + m_Output_2.Get_SlaveID().ToString() + " " + m_Output_2.m_iBitID.ToString() + ")";

            m_list_State = mState;

            _action = act;
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////
        private void UserCtrl_UI_Dual_OutputBit_Load(object sender, EventArgs e)
        {
            Get_Status();
        }

        //Get label and current state/////////////////////////////////////////////////////////////////
        public void Get_Status()
        {
            lock (m_lock_Access)
            {
                string strLabel_1 = "";
                string strLabel_2 = "";

                //check status
                if (m_Output_1.Read() == 0)
                {
                    strLabel_1 = strOutputLabel_1 + " Off";
                }
                else
                {
                    strLabel_1 = strOutputLabel_1 + " On";
                }

                if (m_Output_2.Read() == 0)
                {
                    strLabel_2 = strOutputLabel_2 + " Off";
                }
                else
                {
                    strLabel_2 = strOutputLabel_2 + " On";
                }

                //update button and label
                btn_Boolean.Text = strLabel_1 + m_strBitID_1 + "\r\n" + strLabel_2 + m_strBitID_2;

                if (m_list_State[0] == true)
                {
                    btn_Boolean.BackColor = Color.Red;
                    lbl_Status.Text = "ON";
                }
                else
                {
                    btn_Boolean.BackColor = Color.Green;
                    lbl_Status.Text = "OFF";
                }
            }
        }

        //Button Event//////////////////////////////////////////////////////////////////////////////
        private void btn_Boolean_Click(object sender, EventArgs e)
        {
            lock (m_lock_Access)
            {
                //interlock
                if (m_SeqMachineCtrl.m_ViewModel_Interlock.OutputBit_Interlock(m_Output_1.Label, ((m_Output_1.Read() == 1) ? 0 : 1)) == false)
                {
                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                    return;
                }

                if (m_SeqMachineCtrl.m_ViewModel_Interlock.OutputBit_Interlock(m_Output_2.Label, ((m_Output_2.Read() == 1) ? 0 : 1)) == false)
                {
                    Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                    return;
                }

                if (m_list_State[0] == true)
                {
                    _action(false);
                }
                else
                {
                    _action(true);
                }

                //Thread.Sleep(1000);
            }
        }
    }
}
