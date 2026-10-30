using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
    public partial class MachineInfo_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineControl = null;

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public MachineInfo_Dialog()
        {
            InitializeComponent();

            //Set SeqMachineControl pointer
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //Dialog loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void MachineInfo_Dialog_Load(object sender, EventArgs e)
        {
            //Update UI
            txtb_MachineID.Text				= m_SeqMachineControl.m_MachineInfo.m_strMachineID;
            txtb_MachineName.Text		= m_SeqMachineControl.m_MachineInfo.m_strMachineName;
            lbl_LogLevelSet.Text				= m_SeqMachineControl.m_DataUnit.LogLevel_int.ToString();
            lbl_MachineNumber.Text		= m_SeqMachineControl.m_MachineInfo.m_iMachineNumber.ToString();

        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        private void btn_UpdateEsc_Click(object sender, EventArgs e)
        {
            Main_Dlg dlg = GT_App.Program.m_MainDialog;

            //get data
            m_SeqMachineControl.m_MachineInfo.m_strMachineID = txtb_MachineID.Text;
            m_SeqMachineControl.m_MachineInfo.m_strMachineName = txtb_MachineName.Text;

            //update to registry
            m_SeqMachineControl.MachineInfo_To_Registry();

            //Update UI
            //dlg.Update_UserCtrlUI();
            dlg = null;

            this.Close();
        }

        private void btn_LogLevelSet_Click(object sender, EventArgs e)
        {
            int iLevel = int.Parse(lbl_LogLevelSet.Text);

            if (MessageBox.Show("Setting Log level will affect the machine sequence log gathering. \r\nNote: This setting will not be save.\r\nPlease set to 1 to log all logs", "Notice", MessageBoxButtons.OKCancel) == System.Windows.Forms.DialogResult.OK)
            {
				m_SeqMachineControl.m_DataUnit.LogLevel_int = iLevel;
            }
            else
                return;

            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Integer();
            dlg.Set_Label("Set Machine Sequence Log file Level");
            dlg.Set_Title("Machine sequence Log file level");
            dlg.Set_InputBoxValue(lbl_LogLevelSet.Text);

            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                iLevel = dlg.Get_DataInt();
				m_SeqMachineControl.m_DataUnit.LogLevel_int = iLevel;

                lbl_LogLevelSet.Text = iLevel.ToString();
            }
        }

		private void btn_MachineType_Click( object sender, EventArgs e )
		{
			MessageBox_InputString dlg = new MessageBox_InputString();
			dlg.Set_Integer();
			dlg.Set_Label("Set Machine Type Number");
			dlg.Set_Title("Machine Type 1=>LineA, 2=>LineB");

			if ( dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK )
			{
				m_SeqMachineControl.m_DataUnit.MachineTypeNum_i = dlg.Get_DataInt();
			}
		}
    }
}
