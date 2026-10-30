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
    public partial class DefineProductData_Dialog : Form
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Data_PackageFile m_DataPackage;
        MachineInfo m_MachineInfo;

        String m_strOriPackageFilename = "";                                    //lock down the original package file name. Re-read it again when closing this window

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DefineProductData_Dialog()
        {
            InitializeComponent();

            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataPackage = m_SeqMachineControl.m_DataPackageFile;
            m_MachineInfo = m_SeqMachineControl.m_MachineInfo;
        }

        //Dialog load/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DefineProductData_Dialog_Load(object sender, EventArgs e)
        {
            //update list view with data package list
            this.Update_ListBox();

            //Update UI
            txtb_DataFileName.Text = m_MachineInfo.m_strPackageFileName;
            txtb_DataFileExtension.Text = m_MachineInfo.m_strPackageFileExt;
            txtb_DataFilePath.Text = m_MachineInfo.m_strPackageFilePath;

            //update var
            m_strOriPackageFilename = m_MachineInfo.m_strPackageFileName;
        }

        //Update List box///////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_ListBox()
        {
            String strError = "";

            //clear list box
            listb_ProductPackageList.Items.Clear();
            
            //update list view with data package list
            if (m_SeqMachineControl.Get_PackageList(ref strError) <= 0)
            {
                MessageBox.Show(strError);
                return;
            }

            //update list view
            foreach (string str in m_SeqMachineControl.m_strPackageList)
            {
                listb_ProductPackageList.Items.Add(str);
            }
        }

        //Check list box is selected/////////////////////////////////////////////////////////////////////////////////////
        private bool Check_ListBoxSelected()
        {
            if (listb_ProductPackageList.SelectedIndex < 0)
            {
                MessageBox_CustomDlg msgDlg = new MessageBox_CustomDlg();
                msgDlg.Set_Messages("The Package list box is not selected.", "Please select a package file name from the list.");
                msgDlg.Set_BitMapLogo(1);
                msgDlg.ShowDialog();
                return false;
            }

            return true;
        }
        

//Button Clicks//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_SetFilePathDlg_Click(object sender, EventArgs e)
        {
            SetFilePathDialog dlg = new SetFilePathDialog();
            dlg.ShowDialog();
            dlg.Dispose();

            //Update UI
            txtb_DataFileName.Text = m_MachineInfo.m_strPackageFileName;
            txtb_DataFileExtension.Text = m_MachineInfo.m_strPackageFileExt;
            txtb_DataFilePath.Text = m_MachineInfo.m_strPackageFilePath;
        }

        private void btn_ESC_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_AddPackageFile_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            dlg.Set_Title("Package File Name");
            dlg.Set_Label("Input Package File Name...");

            if (dlg.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    m_MachineInfo.m_strPackageFileName = dlg.Get_DataString();
                    m_SeqMachineControl.Pkg_To_Registry();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.Update_ListBox();
            }
        }

        private void btn_CopyPackageFile_Click(object sender, EventArgs e)
        {
            MessageBox_InputString dlg = new MessageBox_InputString();
            String strDestination = "", strSource = "";

            //check if the list box is selected
            if (Check_ListBoxSelected() == false) return;

            //input package file name
            dlg.Set_Title("Package File Name");
            dlg.Set_Label("Input Package File Name...");

            //copy package file to a new file
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    strSource = m_MachineInfo.m_strPackageFilePath + listb_ProductPackageList.SelectedItem.ToString();
                    strDestination = m_MachineInfo.m_strPackageFilePath + dlg.Get_DataString() + m_MachineInfo.m_strPackageFileExt;

                    System.IO.File.Copy(strSource, strDestination);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

                this.Update_ListBox();
            }
        }

        private void btn_DeletePackageFile_Click(object sender, EventArgs e)
        {
            //check if the list box is selected
            if (Check_ListBoxSelected() == false) return;

            try
            {
                String strSource = m_MachineInfo.m_strPackageFilePath+ listb_ProductPackageList.SelectedItem.ToString();
                System.IO.File.Delete(strSource);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            this.Update_ListBox();
        }

        private void listb_ProductPackageList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listb_ProductPackageList.SelectedIndex == -1) return;
            
            String strPackageFileName = listb_ProductPackageList.SelectedItem.ToString();

            strPackageFileName = strPackageFileName.Substring(0, strPackageFileName.LastIndexOf("."));
            m_SeqMachineControl.Pkg_From_Registry(strPackageFileName);

            //Update Machine Info - machine settings
            m_SeqMachineControl.m_MachineInfo.m_strPackageFileName = strPackageFileName;
            m_SeqMachineControl.MachineInfo_To_Registry();

            //update text box
            txtb_DataFileName.Text = strPackageFileName;
        }

        private void DefineProductData_Dialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (m_strOriPackageFilename != m_MachineInfo.m_strPackageFileName)
            {
                m_SeqMachineControl.Pkg_From_Registry(m_strOriPackageFilename);
            }
        }

        private void btn_ProductSpecs_Click(object sender, EventArgs e)
        {
            DefineProductSpecs_Dialog dlg = new DefineProductSpecs_Dialog();
            dlg.ShowDialog();
            dlg.Dispose();
        }

		private void btn_RobotSettings_Dlg_Click( object sender, EventArgs e )
		{
            //open gantry teach dialog
            Robot_Module_Teach dlg = new Robot_Module_Teach();
			dlg.ShowDialog();
			dlg.Dispose();

		}

        private void btn_RobotSettings_HotBlower_Dlg_Click(object sender, EventArgs e)
        {
            //open gantry teach dialog
            Robot_Module_Teach_HotBlower dlg = new Robot_Module_Teach_HotBlower();
            dlg.ShowDialog();
            dlg.Dispose();

        }

        private void btn_IndexerSettings_Click(object sender, EventArgs e)
        {
            Indexer_Module_Teach dlg = new Indexer_Module_Teach();
            dlg.ShowDialog();
            dlg.Dispose();
        }

    }
}
