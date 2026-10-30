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
    public partial class UserCtrl_ManualControlPanel : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;
        Module_ControlPanel m_ModuleControlPanel = null; 

        List<UserCtrl_UI_InputBit> m_list_userctrl_UI_Inputbit;
        List<UserCtrl_UI_OutputBit> m_list_userctrl_UI_Outputbit;
        List<UserCtrl_UI_Cylinder> m_list_userctrl_UI_Cylinder;
        
        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_ManualControlPanel()
        {
            InitializeComponent();
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_ModuleControlPanel = m_SeqMachineControl.m_pModule_ControlPanel;

            m_list_userctrl_UI_Inputbit = new List<UserCtrl_UI_InputBit>();
            m_list_userctrl_UI_Outputbit = new List<UserCtrl_UI_OutputBit>();
            m_list_userctrl_UI_Cylinder = new List<UserCtrl_UI_Cylinder>();
        }

        //Dialog Loaded//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //no need to alter here. Just add sensors and input bits at the add user control section
        private void UserCtrl_ManualControlPanel_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(0, 0);
            int iCount = 0;
            short iRet = 1;

            pPoint.X = 0;
            pPoint.Y = 0;

            //Add Input bits
            while (true )
            {
                //Add Input bit
                iRet = Add_UserCtrlInput(iCount);
                if (iRet == 0) break;

                //    //Set user control position
                m_list_userctrl_UI_Inputbit[iCount].Location = pPoint;
                this.Controls.Add(m_list_userctrl_UI_Inputbit[iCount]);

                //    //Set point for user control position (next user control)
                pPoint.Y += m_list_userctrl_UI_Inputbit[iCount].Height;

                //increment count
                iCount++;
            }

            //Reset the count
            iCount = 0;

            //Set Output bit position
            pPoint.X = m_list_userctrl_UI_Inputbit[0].Width + 5;
            pPoint.Y = 0;

            //Add output bits
            while(true)
            {
                iRet = Add_UserCtrlOutput(iCount);
                if (iRet == 0) break;

                //set user control position
                m_list_userctrl_UI_Outputbit[iCount].Location = pPoint;
                this.Controls.Add(m_list_userctrl_UI_Outputbit[iCount]);

                //Set point for user control position (next user control)
                pPoint.Y += m_list_userctrl_UI_Outputbit[iCount].Height;

                //increment count
                iCount++;
            }
        }

        //Add user control//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private short Add_UserCtrlInput(int iCount)
        {
            InputBit mInput = null;
            
            switch (iCount)
            {
                case 0: mInput = m_ModuleControlPanel.m_pInAirPressure; break;
                case 1: mInput = m_ModuleControlPanel.m_pInAutoManualSwitch; break;
                case 2: mInput = m_ModuleControlPanel.m_pInEStopButton; break;
                case 3: mInput = m_ModuleControlPanel.m_pInSafetyRelay; break;
                case 4: mInput = m_ModuleControlPanel.m_pInDoorLockSignal; break;
                default: return 0;
            }

            m_list_userctrl_UI_Inputbit.Add(new UserCtrl_UI_InputBit());
            m_list_userctrl_UI_Inputbit[iCount].Set_InputBitPtr(mInput);

            return 1;
        }

        private short Add_UserCtrlOutput(int iCount)
        {
            OutputBit mOutput = null;

            switch (iCount)
            {
                case 0: mOutput = m_ModuleControlPanel.m_pOutTowerLightRed_Flash; break;
                case 1: mOutput = m_ModuleControlPanel.m_pOutTowerLightGreen_Flash; break;
                case 2: mOutput = m_ModuleControlPanel.m_pOutTowerLightYellow_Flash; break;
                case 3: mOutput = m_ModuleControlPanel.m_pOutTowerLightRed_Cont; break;
                case 4: mOutput = m_ModuleControlPanel.m_pOutTowerLightGreen_Cont; break;
                case 5: mOutput = m_ModuleControlPanel.m_pOutTowerLightYellow_Cont; break;
                case 6: mOutput = m_ModuleControlPanel.m_pOutTowerLightBuzzer; break;
                case 7: mOutput = m_ModuleControlPanel.m_pOutMaintenanceLight; break;

                default: return 0;
            }

            m_list_userctrl_UI_Outputbit.Add(new UserCtrl_UI_OutputBit());
            m_list_userctrl_UI_Outputbit[iCount].Set_OutputBitPtr(mOutput);

            return 1;
        }

        //change button text and color display/////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_Button(Button mBtn, OutputBit mOutput)
        {
            //update text and color
            if (mOutput.Read() == 1)
            {
                mBtn.Text = mBtn.Text.Replace("On", "Off");
                mBtn.BackColor = Color.Lime;
            }
            else
            {
                mBtn.Text = mBtn.Text.Replace("Off", "On");
                mBtn.BackColor = Color.Red;
            }
        }

        //Error Message Box////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Error_MsgBox(String strLabel, int iRes)
        {
            if (iRes < 0)
            {
                MessageBox_CustomDlg Error_Dlg = new MessageBox_CustomDlg();

                Error_Dlg.Set_BitMapLogo(1);
                Error_Dlg.Set_Buttons(1);
                Error_Dlg.Set_Messages("Error Unable to Trigger " + strLabel + " bit", "Please Check the related output device.");
                Error_Dlg.ShowDialog();
            }
        }

        //Update UI///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //note: Loop at main manual page window
        public void Update_UI()
        {
            for (int i = 0; i < m_list_userctrl_UI_Inputbit.Count; i++)
            {
                m_list_userctrl_UI_Inputbit[i].Update_PicBox();
            }
        }
    }
}
