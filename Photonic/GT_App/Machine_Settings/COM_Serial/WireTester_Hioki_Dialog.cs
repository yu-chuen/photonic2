using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

/**********************************************************************************************************************
 * wrapper class for the Wire tester Hioki triggering and reading protocols
**********************************************************************************************************************/
namespace GT_App
{
	public partial class WireTester_Hioki_Dialog : Form
	{
		//variables
		SeqMachineControl m_SeqMachineCtrl;
		UserCtrl_SerialPortSettings m_userctrl_SerialPortSettings = new UserCtrl_SerialPortSettings();
		CommSerial_WireTester_Hioki m_CommDevice;
		
		Cylinder m_cylClamper = null;
		bool m_bExtended = false;

		//constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public WireTester_Hioki_Dialog()
		{
			InitializeComponent();

			m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_CommDevice = m_SeqMachineCtrl.m_ComSerialWireTester;
			m_cylClamper = m_SeqMachineCtrl.m_Module_OP90_Vision.m_cylClamp_Wire;
		}

		//Dialog loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void WireTester_Hioki_Dialog_Load(object sender, EventArgs e)
        {
            //load user control
            Point pPoint = new Point();
            pPoint.X = groupBox_Operations.Size.Width + 5;
            pPoint.Y = 0;

            m_userctrl_SerialPortSettings.Set_CommSettingsPtr(m_CommDevice.m_Settings.m_Settings);
            m_userctrl_SerialPortSettings.Location = pPoint;
            this.Controls.Add(m_userctrl_SerialPortSettings);

            //Set enable button
            if (m_CommDevice.Enable == false)
            {
                btn_EnableWireTester.Text = " Wire Tester Disabled";
                btn_EnableWireTester.BackColor = Color.Red;
            }

            //update clamp button
            Update_Buttons(btn_WTClamp, ((m_cylClamper.Get_Work_Output().Read() == 1) ? true : false));
        }


		//button event/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ReadResistance_Click(object sender, EventArgs e)
        {
            String strData = "";
            Module_OP90_VisionInspection pModule_OP90_VisionInspection;

            pModule_OP90_VisionInspection = m_SeqMachineCtrl.m_Module_OP90_Vision;

            //Selection
            if (radbtn_WT_LN1.Checked == true) pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_LN1_str);
            else if (radbtn_WT_LN2.Checked == true) pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_LN2_str);
            else if (radbtn_WT_LN3.Checked == true) pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_LN3_str);
            else if (radbtn_WT_LR0.Checked == true) pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_LR0_str);
            else if (radbtn_WT_R0R1.Checked == true) pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_R0R1_str);

            //read from wire tester
            if (m_CommDevice.Read_Resistance(ref strData) == false)
            {
                String strError = m_SeqMachineCtrl.Get_ErrorMessage(m_CommDevice.Get_ErrorID(), m_CommDevice.Get_ErrorMessage(), m_CommDevice.m_strLabel);
                Class_Helper.Display_ErrorMessageBox(strError);
                lbl_OperationResponse.Text = strError;
            }
            else
            {
                lbl_OperationResponse.Text = strData;
                lbl_OperationResponse.Text += "\r\nDecode: " + m_CommDevice.m_strOhmReading;
            }

            //close pins
            pModule_OP90_VisionInspection.WireTester_SetPins(Module_OP90_VisionInspection.WT_CLOSE_str);
        }

		private void btn_Update_Click( object sender, EventArgs e )
		{
			m_CommDevice.m_Settings.m_Settings = m_userctrl_SerialPortSettings.Get_Settings();
			m_CommDevice.To_Registry();
		}

		private void btn_ESC_Click( object sender, EventArgs e )
		{
			this.Close();
		}

		private void btn_ZeroAdjustment_Click( object sender, EventArgs e )
		{
			if ( m_CommDevice.ZeroAdjustment() == false )
			{
                String strError = m_SeqMachineCtrl.Get_ErrorMessage(m_CommDevice.Get_ErrorID(), m_CommDevice.Get_ErrorMessage(), m_CommDevice.m_strLabel);
				Class_Helper.Display_ErrorMessageBox(strError);
			}
			else
				MessageBox.Show("Adjustment Complete");
		}

		private void btn_ReadTemperature_Click( object sender, EventArgs e )
		{
			String strData = "";

			//read from wire tester
			if ( m_CommDevice.Read_Temperature(ref strData) == false )
			{
                String strError = m_SeqMachineCtrl.Get_ErrorMessage(m_CommDevice.Get_ErrorID(), m_CommDevice.Get_ErrorMessage(), m_CommDevice.m_strLabel);
				Class_Helper.Display_ErrorMessageBox(strError);
				lbl_OperationResponse.Text = strError;
			}
			else
			{
				lbl_OperationResponse.Text = strData;
				lbl_OperationResponse.Text += "\r\nDecode: " + m_CommDevice.m_strTemperatureReading;
			}
		}

        private void btn_EnableWireTester_Click(object sender, EventArgs e)
        {
            if (m_CommDevice.Enable == true)
            {
                m_CommDevice.Enable = false;
                btn_EnableWireTester.Text = " Wire Tester Disabled";
                btn_EnableWireTester.BackColor = Color.Red;
            }
            else
            {
                m_CommDevice.Enable = true;
                btn_EnableWireTester.Text = " Wire Tester Enabled";
                btn_EnableWireTester.BackColor = Color.LimeGreen;
            }

        }

        private void btn_WTClamp_Click(object sender, EventArgs e)
        {
            bool bTrigger = false;

            //Interlock
            if (m_SeqMachineCtrl.m_ViewModel_Interlock.Cylinder_Interlock(m_cylClamper.Get_Label(), ((m_bExtended == true) ? 0 : 1)) == false)
            {
                Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.m_ViewModel_Interlock.Get_ErrorMessage());
                return;
            }

            //trigger cylinder
            m_bExtended = ((m_bExtended == true) ? false : true);
            m_cylClamper.Cyl_Trigger(m_bExtended);

            //Check Cylinder state
            if (m_cylClamper.WaitTaskComplete() == false)
            {
                Class_Helper.Display_ErrorMessageBox(m_SeqMachineCtrl.Get_ErrorMessage(m_cylClamper.Get_ErrorID(), "", m_cylClamper.Get_Label()));
            }

            //Set Button UI
            Update_Buttons(btn_WTClamp, ((m_cylClamper.Get_Work_Output().Read() == 1) ? true : false));
        }

		//Button Event///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void Update_Buttons( Button mObj, bool bActivated )
		{
			String strExtend = " Extend", strRetract = " Retract";

			//Remove back text - make it original
			mObj.Text = mObj.Text.Replace(strExtend, "");
			mObj.Text = mObj.Text.Replace(strRetract, "");

			//Set button
			if ( bActivated == true )
			{
				mObj.Text = mObj.Text + strExtend;
				mObj.BackColor = Color.LightGreen;
			}
			else
			{
				mObj.Text = mObj.Text + strRetract;
				mObj.BackColor = Color.LightGray;
			}
		}
	}
}