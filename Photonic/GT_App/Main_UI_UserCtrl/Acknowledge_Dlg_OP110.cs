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
    public partial class Rework_Dlg_OP30 : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Data_UnitTempStorage m_DataUnit;

        //contructor////////////////////////////////////////////////////////////////////////////////////////
        public Rework_Dlg_OP30()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////
        private void Rework_Dlg_OP30_Load(object sender, EventArgs e)
        {
            //start the update ui timer
            m_Timer.Start();

            //set warning label text
            lbl_Warning.Text = "Warning:\r\n\r\n";
            lbl_Warning.Text += "This will skip OP30 pallet RFID clearing function.";

            m_DataUnit.Flag_OP30_Rework_AwaitSelection_b = false;
        }

        private void Rework_Dlg_OP30_FormClosing(object sender, FormClosingEventArgs e)
        {
            m_Timer.Stop();
        }

        //check box event////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Update_CheckBox(CheckBox mObj, bool bActivated)
        {
            //set checkbox
            if (bActivated == true)
            {
                mObj.Checked = true;
            }
            else
            {
                mObj.Checked = false;
            }
        }

        //Update UI Timer event///////////////////////////////////////////////////////////////////////////////////////
        private void m_Timer_Tick(object sender, EventArgs e)
        {
            m_Timer.Stop();

            if (m_DataUnit.Flag_OP30_ManualStation_Sending_b == true) //if OP30 is sending pallet
            {
                lbl_Status.Text = "Pallet is sending out to OP40 Buffer.";
                lbl_Instruction.Text = "Please wait until the pallet is fully released to OP40 Buffer.";
            }
            else if (m_DataUnit.Flag_OP30_Rework_Insert_b == true) //after user press insert button
            {
                //Tell user to insert pallet
                if (m_DataUnit.Flag_OP30_Rework_AwaitPallet_b == true)
                {
                    lbl_Status.Text = "Awaiting pallet present...";
                    lbl_Instruction.Text = "Please insert pallet.";
                }
                //enable check box
                else if (m_DataUnit.Flag_OP30_Rework_AwaitSelection_b == true)
                {
                    lbl_Status.Text = "Awaiting rework selection...";
                    lbl_Instruction.Text = "Please make sure the pallet is meant for rework and press Done button.";

                    btn_Done.Enabled = true;
                }
                else
                {
                    lbl_Status.Text = "There is a pallet at current station.";
                    lbl_Instruction.Text = "Please wait until the pallet is released to next station.";
                }
            }
            else //wait user to press a button
            {
                lbl_Status.Text = "...";
                lbl_Instruction.Text = "Please press Insert button.";
            }

            m_Timer.Start();
        }

        //button click events//////////////////////////////////////////////////////////////////////////////////////
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_OP30_Rework_Insert_b = true;

            //disable button after pressed.
            btn_.Enabled = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_OP30_Rework_Insert_b = false;

            //stop timer
            m_Timer.Stop();

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_OP30_Rework_Insert_b = false;
            m_DataUnit.Flag_OP30_Rework_Done_b = true;

            //stop timer
            m_Timer.Stop();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}