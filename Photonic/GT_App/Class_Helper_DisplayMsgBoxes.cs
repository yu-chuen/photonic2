using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StaticCoreLib;

namespace GT_App
{
    public class Class_Helper_DisplayMsgBoxes
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Data_UnitTempStorage m_DataUnit;
        MessageBox_Triggers m_MessageBox_Triggers; 

        MessageBox_CustomDlg m_ModelessMsgBox = new MessageBox_CustomDlg();

        //constructor//////////////////////////////////////////////////////////////////////////////////////
        public Class_Helper_DisplayMsgBoxes()
        {
			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
            m_MessageBox_Triggers = m_SeqMachineCtrl.m_MessageBoxTrigger;

            m_ModelessMsgBox.Visible = false;
        }

        //Display MessageBoxes///////////////////////////////////////////////////////////////
        //Display Error Message Box
        public void Display_ErrorMsgBox()
        {
            if (m_MessageBox_Triggers.Flag_MsgBoxError == true)
            {
                m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Error();

                MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
                dlg.Set_BitMapLogo(1);
                dlg.Set_Buttons(1);
                dlg.Set_Title("Error");
                dlg.Set_Messages(m_MessageBox_Triggers.MessageError_str, "");
                dlg.Set_FontColor(System.Drawing.Color.Red);
                dlg.ShowDialog();
                dlg.Dispose();

                m_MessageBox_Triggers.Flag_MsgBoxError = false; //reset flag
                m_SeqMachineCtrl.m_Module_ControlPanel.TurnOff_Buzzer();
            }
        }

        //Display Warning Message box
        public void Display_WarningMsgBox()
        {
            if (m_MessageBox_Triggers.Flag_MsgBoxWarning == true)
            {
                if (m_SeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Static.Read() == 1)
                    m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Error();

                MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
                dlg.Set_BitMapLogo(0);
                dlg.Set_Buttons(1);
                dlg.Set_Title("Warning");
                dlg.Set_Messages(m_MessageBox_Triggers.MessageWarning_str, "");
                dlg.ShowDialog();
                dlg.Dispose();

                m_MessageBox_Triggers.Flag_MsgBoxWarning = false;      //reset flag

                if (m_SeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Blink.Read() == 1)
                    m_SeqMachineCtrl.m_Module_ControlPanel.TowerLight_Run();
            }
        }

        //Display Request Message Box
        public void Display_RequestMsgBox()
        {
            if (m_MessageBox_Triggers.Flag_RequestMsgbox == true)
            {
                m_MessageBox_Triggers.Flag_RequestState = 0;

                MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
                dlg.Set_BitMapLogo(2);
                dlg.Set_Buttons(3);
                dlg.Set_Title("Request");
                dlg.Set_Messages(m_MessageBox_Triggers.MessageRequest_str, "");

                if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK) m_MessageBox_Triggers.Flag_RequestState = 1;
                else m_MessageBox_Triggers.Flag_RequestState = -1;

                dlg.Dispose();

                m_MessageBox_Triggers.Flag_RequestMsgbox = false;              //lower the flag to tell the thread that the decision is done
            }
        }

        //Display Modeless Message Box
        public void Display_ModelessMsgBox()
        {
            if (m_MessageBox_Triggers.Flag_MsgBoxModeless == true)
            {
                m_ModelessMsgBox.Set_Messages(m_MessageBox_Triggers.ModelessMsg_str, "");

                if (m_ModelessMsgBox.Visible == true) return;

                //set modeless msg box
                m_ModelessMsgBox.Set_FontColor(System.Drawing.Color.Red);
                m_ModelessMsgBox.Set_BitMapLogo(0);
                m_ModelessMsgBox.Set_Buttons(4);
                m_ModelessMsgBox.Set_Title("Notice");
                m_ModelessMsgBox.Set_ControlBox(false);

                //Display msg box
                m_ModelessMsgBox.Show();
                m_ModelessMsgBox.Visible = true;
            }
            else
            {
                if (m_ModelessMsgBox.Visible == false) return;
                m_ModelessMsgBox.Hide();
            }
        }

		//Display Request Message Box with 3 Input
		public void Display_RequestMsgBox3Input()
		{
			if (m_MessageBox_Triggers.Flag_RequestMsgBox3Input == true)
			{
				m_MessageBox_Triggers.RequestState_3input_i = 0;

				MessageBox_CustomDlg_3Inputs dlg = new MessageBox_CustomDlg_3Inputs();
				dlg.Set_Title("Selection");
				dlg.Set_Message(m_MessageBox_Triggers.MessageRequest3Input_str);
				m_MessageBox_Triggers.RequestState_3input_i = (int)dlg.ShowDialog();
				dlg.Dispose();

				m_MessageBox_Triggers.Flag_RequestMsgBox3Input = false;
			}
		}
    }
}
