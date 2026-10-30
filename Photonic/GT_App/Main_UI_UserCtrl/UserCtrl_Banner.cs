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
    public partial class UserCtrl_Banner : UserControl
    {
        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_Banner()
        {
            InitializeComponent();
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_Banner_Load(object sender, EventArgs e)
        {
			Update_UI();

			//get screen size
			this.Size = new Size(Screen.PrimaryScreen.WorkingArea.Width - 30, this.Size.Height);
        }

        //Update the UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI()
        {
            SeqMachineControl mSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            lbl_MachineName.Text = mSeqMachineCtrl.m_MachineInfo.m_strMachineName;
            
            this.Update();
        }
    }
}
