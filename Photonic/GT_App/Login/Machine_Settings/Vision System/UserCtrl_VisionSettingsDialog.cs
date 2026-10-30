using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.IO;
using System.Threading;

/****************************************************************************************************************************************************************************************
 * NOTES: 
 *              1.  Commands are obtain via SeqMachineControl : Vision_GetCommands(...)
****************************************************************************************************************************************************************************************/

namespace GT_App
{
    public partial class UserCtrl_VisionSettingsDialog : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Vision_Cognex m_Vision;
        Vision_Settings m_Settings;
		ViewModel_Vision m_VisionViewModel;

        public int VisionCam_Number { set; get; }                           //determine the cam number (for UI purpose)

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_VisionSettingsDialog()
        {
            InitializeComponent();

			m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_VisionViewModel = m_SeqMachineControl.m_ViewModel_Vision;
        }

        //Dialog load/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_VisionSettingsDialog_Load(object sender, EventArgs e)
        {
            //Update UI
            Display_IPAddress(m_Vision.IPAddress);
            txtb_PortNumber.Text = m_Vision.PortNumber.ToString();
            txtb_ImgFileName.Text = m_Vision.ImageFileName;
            txtb_ImgFilePath.Text = m_Vision.ImageFilePath;
            txtb_TimeOut.Text = m_Vision.Delay_RunTime.ToString();
            txtb_RecipeName.Text = m_Vision.Recipe_Run;
            txtb_FileTriggerName.Text = m_Vision.Event_Name;            //event: Trigger to get image
            txtb_FileSyncName.Text = m_Vision.Mutex_Name;               //Mutex: Block resource from being acces from multiple sources

            Set_Buttons();

            //update list view
			int iCmdTotal = m_VisionViewModel.m_ll_stVisionDebugString.Count();
			int iDispCount = 1;
			for ( int iCount = 0; iCount < iCmdTotal; iCount++ )
			{
				if ( m_Vision.Label == m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_strCamLabel )
				{
					Add_ListViewItems(m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_strCommand, iDispCount);
					iDispCount++;
				}
			}

            //update enable button
            if (m_Vision.Enable == false)
            {
                btn_VisionEnable.Text = "Vision Disabled";
                btn_VisionEnable.BackColor = Color.Red;
            }
        }

		//Set pointers////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_VisionCognexPtr( Vision_Cognex mVision ) 
        {
            m_Vision = mVision;
            m_Settings = m_Vision.Get_VisionSettings();
        }

        //Add items into the list view////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_ListViewItems(String strVisionCommand, int iNum)
        {
            String[] ar = new String[2];
            ListViewItem lt;

            ar[0] = iNum.ToString();
            ar[1] = strVisionCommand;
            lt = new ListViewItem(ar);

            listView_CommandList.Items.Add(lt);
        }

        //Display IP Address////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Display_IPAddress(String strIP)
        {
            String[] strIPArr = strIP.Split('.');

            txtb_IPAddress1.Text = strIPArr[0];
            txtb_IPAddress2.Text = strIPArr[1];
            txtb_IPAddress3.Text = strIPArr[2];
            txtb_IPAddress4.Text = strIPArr[3];
        }
        //Update IP Address text box///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void txtb_IPAddress1_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress1); }
        private void txtb_IPAddress2_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress2); }
        private void txtb_IPAddress3_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress3); }
        private void txtb_IPAddress4_TextChanged(object sender, EventArgs e) { Class_Helper.IPAddress_TextBoxChanged(txtb_IPAddress4); }


        //Display error from button commands////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Display_ErrorDialog(String strError)
        {
            MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
            dlg.Set_Messages(strError + m_Vision.Get_ErrorMessage(), "");
            dlg.ShowDialog();
            dlg.Dispose();
        }

        //Set the Connect and Disconnect buttons according to the comm connected var/////////////////////////////////////////////////////////////
        private void Set_Buttons()
        {
            if (m_Vision.Connected)
            {
                btn_Connect.Enabled = false;
                btn_Disconnect.Enabled = true;
            }
            else
            {
                btn_Connect.Enabled = true;
                btn_Disconnect.Enabled = false;
            }
        }

        //button controls////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Connect_Click(object sender, EventArgs e)
        {
            if (m_Vision.Connect() == false) Display_ErrorDialog("Connect");
            Set_Buttons();
        }

        private void btn_Disconnect_Click(object sender, EventArgs e)
        {
            m_Vision.Disconnect();
            Set_Buttons();
        }

        private void btn_UpdateSettings_Click(object sender, EventArgs e)
        {
            m_Settings.m_strIPAddress = txtb_IPAddress1.Text + "." + txtb_IPAddress2.Text + "." + txtb_IPAddress3.Text + "." + txtb_IPAddress4.Text;
            m_Settings.m_iPortNumber = int.Parse(txtb_PortNumber.Text);
            m_Settings.m_strImgFileName = txtb_ImgFileName.Text;
            m_Settings.m_strImgFilePath = txtb_ImgFilePath.Text;
            m_Settings.m_iDelay_Run = int.Parse(txtb_TimeOut.Text);
            m_Settings.m_strRecipeName = txtb_RecipeName.Text;
            m_Settings.m_strEventName = txtb_FileTriggerName.Text;
            m_Settings.m_strMutexName = txtb_FileSyncName.Text;
            
            m_Vision.To_Registry();
        }

        private void btn_SendCommand_Click(object sender, EventArgs e)
        {
			String[] strArCmd = new string[50];

            //reset output label boxes
            lbl_Command.Text = "";
            lbl_Response.Text = "";

            //check if the list view is selected and get command and token list
            if (listView_CommandList.SelectedIndices.Count == 1)
            {
				strArCmd[0] = listView_CommandList.SelectedItems[0].SubItems[1].Text;
				strArCmd[1] = m_Vision.Label;
                listView_CommandList.SelectedIndices.Clear();
            }
            else
            {
                MessageBox.Show("Please select a command from the command list");
                return;
            }

			strArCmd[2] = txtb_AdditionalInfo.Text;

            //if ( txtb_AdditionalInfo.Text == "" ) strArCmd[3] += ",dummytest_serial";           //2019 05 14: bug fix: extra ','
            //else strArCmd[3] += "dummytest_serial";

            txtb_AdditionalInfo.Text = "";

            //Send command
			if ( !m_Vision.Send_Command(strArCmd) ) { Display_ErrorDialog("Send Command: "); lbl_Command.Text = m_Vision.Command; goto End; }
            lbl_Command.Text = m_Vision.Command;

            //Read response
            if (!m_Vision.Read_Data()) { Display_ErrorDialog("Read Response: "); goto End; }
            lbl_Response.Text = m_Vision.Response;

        End:
            Thread.Sleep(1000);
            Update_PicBox();
            return;
        }

        private void txtb_TimeOut_TextChanged(object sender, EventArgs e)
        {
            txtb_TimeOut.Text = Class_Helper.Detect_IntegerOnly_OutString(txtb_TimeOut.Text);
        }

        private void Update_PicBox()
        {
            String strPicFilePath = m_Vision.ImageFile_FullPath;

            //check vision directory
            if (File.Exists(strPicFilePath) == false) { return; }

            if (pic_Vision.Image != null) { pic_Vision.InitialImage = pic_Vision.Image; pic_Vision.Image.Dispose(); }
            pic_Vision.Image = null;

            Bitmap mBt;
            using (Image img = Image.FromFile(strPicFilePath))
            {
                mBt = new Bitmap(img);
            }

            pic_Vision.Image = mBt;
            pic_Vision.Update();
            pic_Vision.InitialImage = null;
        }

        private void btn_FileDialog_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                int iPos = dlg.FileName.LastIndexOf("\\");
                int iLength = dlg.FileName.Length;

                txtb_ImgFilePath.Text = dlg.FileName.Substring(0, iPos + 1);
                txtb_ImgFileName.Text = dlg.FileName.Substring(iPos + 1, iLength - iPos - 1);
            }
        }

		private void listView_CommandList_SelectedIndexChanged( object sender, EventArgs e )
		{
			String strCmd = "";
			Vision_DebugString mVisionDebug;

			//block
			if ( listView_CommandList.SelectedIndices.Count <= 0 ) return;

			//get cmd string
			strCmd = listView_CommandList.SelectedItems[0].SubItems[1].Text;
			//get struct
			mVisionDebug = m_VisionViewModel.Get_VisionDebugString(strCmd);

			txtb_AdditionalInfo.Text = mVisionDebug.m_strData;
		}

        private void btn_VisionEnable_Click(object sender, EventArgs e)
        {
            if (m_Vision.Enable == false)
            {
                m_Vision.Enable = true;
                btn_VisionEnable.Text = "Vision Enabled";
                btn_VisionEnable.BackColor = Color.LimeGreen;
            }
            else
            {
                m_Vision.Enable = false;
                btn_VisionEnable.Text = "Vision Disabled";
                btn_VisionEnable.BackColor = Color.Red;
            }
        }

    }
}
