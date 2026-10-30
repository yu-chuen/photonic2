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
    public partial class UserCtrl_Report_ProductInfo : UserControl
    {
        //variable
        SeqMachineControl m_SeqMachineControl = null;
        
        public UserCtrl_Report_ProductInfo()
        {
            InitializeComponent();
            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

            m_SeqMachineControl = pApp.m_SeqMachineCtrl;

            Update_UI();
        }

        ~UserCtrl_Report_ProductInfo()
        {
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////
        public void Update_UI()
        {
            lbl_MachineID.Text = m_SeqMachineControl.m_MachineInfo.m_strMachineID;
            lbl_MachineNumber.Text = m_SeqMachineControl.m_MachineInfo.m_iMachineNumber.ToString();
            lbl_PackageName.Text = m_SeqMachineControl.m_MachineInfo.m_strPackageFileName + m_SeqMachineControl.m_MachineInfo.m_strPackageFileExt;
        }
    }
}
