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
    public partial class UserCtrl_VisionKeyenceSettingsDialog : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Vision_Keyence m_Vision;
        Vision_Keyence_Settings m_Settings;
		ViewModel_Vision_Keyence m_VisionViewModel;

        public int VisionCam_Number { set; get; }                           //determine the cam number (for UI purpose)

        //constructor////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_VisionKeyenceSettingsDialog()
        {
            InitializeComponent();

			m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
			m_VisionViewModel = m_SeqMachineControl.m_ViewModel_Vision_Keyence;
        }

        //Dialog load/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_VisionSettingsDialog_Load(object sender, EventArgs e)
        {
            //Update UI
            Display_IPAddress(m_Vision.IPAddress);
            txtb_PortNumber.Text = m_Vision.PortNumber.ToString();
            
            Set_Buttons();

            //update list view
			int iCmdTotal = m_VisionViewModel.m_ll_stVisionDebugString.Count();
			//int iDispCount = 1;
            for (int iCount = 0; iCount < iCmdTotal; iCount++)
            {
                if (m_Vision.Label == m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_strCamLabel)
                {
                    Add_ListViewItems(m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_strCommand, m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_iNum, m_VisionViewModel.m_ll_stVisionDebugString[iCount].m_strDescription);
                    //iDispCount++;
                }
            }
        }

		//Set pointers////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_VisionKeyencePtr(Vision_Keyence mVision)
        {
            m_Vision = mVision;
            m_Settings = m_Vision.Get_VisionSettings();
        }

        //Add items into the list view////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_ListViewItems(String strVisionCommand, int iNum, String strDescription)
        {
            String[] ar = new String[4];
            ListViewItem lt;

            ar[1] = iNum.ToString();
            ar[2] = strVisionCommand;
            ar[3] = strDescription;
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
            dlg.Set_Messages(m_SeqMachineControl.Get_ErrorMessage(m_Vision.Get_ErrorID(), m_Vision.Get_ErrorMessage()), "");

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
            //get settings
            m_Settings.m_strIPAddress = txtb_IPAddress1.Text + "." + txtb_IPAddress2.Text + "." + txtb_IPAddress3.Text + "." + txtb_IPAddress4.Text;
            m_Settings.m_iPortNumber = int.Parse(txtb_PortNumber.Text);
           
            //save to file
            m_Vision.To_Registry();
        }

        private void btn_SendCommand_Click(object sender, EventArgs e)
        {
           string[] strArCmd = new string[10];

            //reset output label boxes
            lbl_Command.Text = "";
            lbl_Response.Text = "";

            //get command
            strArCmd[0] = txtb_Command.Text;

            //send command
            if (m_Vision.Send_Command(strArCmd) == false) { Display_ErrorDialog("Send Command: "); lbl_Command.Text = m_Vision.Command; goto End; }
            lbl_Command.Text = m_Vision.Command;

            //read response
            if (m_Vision.Read_Data() == false) { Display_ErrorDialog("Read Response: "); goto End; }
            lbl_Response.Text = m_Vision.Response;

            //read response
            if (m_Vision.Read_Data() == false) { Display_ErrorDialog("Read Response: "); goto End; }
            lbl_Response.Text = m_Vision.Response;

            //reset text box
            txtb_Command.Text = "";

        End:
            Thread.Sleep(1000);
            Update_PicBox();
            return;
        }

        private void Update_PicBox()
        {
            //String strPicFilePath = m_Vision.ImageFile_FullPath;

            ////check vision directory
            //if (File.Exists(strPicFilePath) == false) { return; }

            //if (pic_Vision.Image != null) { pic_Vision.InitialImage = pic_Vision.Image; pic_Vision.Image.Dispose(); }
            //pic_Vision.Image = null;

            //Bitmap mBt;
            //using (Image img = Image.FromFile(strPicFilePath))
            //{
            //    mBt = new Bitmap(img);
            //}

            //pic_Vision.Image = mBt;
            //pic_Vision.Update();
            //pic_Vision.InitialImage = null;
        }

        private void listView_CommandList_SelectedIndexChanged(object sender, EventArgs e)
        {
            String strCmd = "";
            int iNum = 0;
            Vision_Keyence_DebugString mVisionDebug;

            //block
            if (listView_CommandList.SelectedIndices.Count <= 0) return;

            //get cmd string
            strCmd = listView_CommandList.SelectedItems[0].SubItems[1].Text;

            iNum = int.Parse(strCmd);

            //get struct
            mVisionDebug = m_VisionViewModel.Get_VisionDebugString(iNum);

            txtb_Command.Text = mVisionDebug.m_strCommand;

            lbl_Response.Text = "...";
        }
    }
}
