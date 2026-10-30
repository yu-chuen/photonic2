/****************************************************************************************************************************************
 * Error Message Dialog: 
 * Desc:    Dialog displays the first error that the sequence throw. 
 ****************************************************************************************************************************************/

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
    public partial class MessageBox_CustomDlg : Form
    {
        //contructor/////////////////////////////////////////////////////////////////////////////////////////////
        public MessageBox_CustomDlg()
        {
            InitializeComponent();

            lbl_ErrorMsg.Text = "";
            lbl_ErrorRecovery.Text = "";
        }

        //destructor////////////////////////////////////////////////////////////////////////////////////////////
        ~MessageBox_CustomDlg()
        {
        }

        //Page Loading////////////////////////////////////////////////////////////////////////////////////////
        private void MessageBox_CustomDlg_Load(object sender, EventArgs e)
        {

        }

        //OK Button Clicked/////////////////////////////////////////////////////////////////////////////////////
        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //cancel button clicked////////////////////////////////////////////////////////////////////////////////
        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //Set Display Messages/////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Set Error text and recovery text
        /// </summary>
        public void Set_Messages(String strMsg1, String strMsg2)
        {
            lbl_ErrorMsg.Text = strMsg1;
            lbl_ErrorRecovery.Text = strMsg2;

            this.Update();
        }

        //Set Title //////////////////////////////////////////////////////////////////////////////////////////
        public void Set_Title(String strTitle)
        {
            this.Text = strTitle;
        }

        //Set logo///////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Set Bitmap Logo. iSelect: 1=Error Icon, else=info icon
        /// </summary>
        public void Set_BitMapLogo(int iSelect = 1)
        {
            if (picb_Icon.Image != null) picb_Icon.Image.Dispose();
            
            if (iSelect == 1)
            {
                picb_Icon.Image = GT_App.Properties.Resources.icon_err;
            }
            else
            {
                picb_Icon.Image = GT_App.Properties.Resources.iconInfo;
            }

            picb_Icon.Update();
        }

        //Set Buttons/////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Display OK and or cancel button. iSelect: 1=OK visible, 2=Cancel visible, 3=All buttons visible, 4=all buttons non visible
        /// </summary>
        public void Set_Buttons(int iSelect)
        {
            switch (iSelect)
            {
                case 1:     btn_OK.Visible = true;       btn_Cancel.Visible = false;        break;
                case 2:     btn_OK.Visible = false;     btn_Cancel.Visible = true;          break;
                case 4:     btn_OK.Visible = false;     btn_Cancel.Visible = false;         break;
                case 3:     btn_OK.Visible = true;      btn_Cancel.Visible = true;          break;
            }
        }

        //Set topmost
        public void Set_TopMost()
        {
            this.TopMost = true;
        }

        //Set Control box/////////////////////////////////////////////////////////////////////////////////
        public void Set_ControlBox(bool bEnable)
        {
            this.ControlBox = bEnable;
        }

        //Set Font Color//////////////////////////////////////////////////////////////////////////////////
        public void Set_FontColor(Color mColor) { this.lbl_ErrorMsg.ForeColor = mColor; }


    }
}
