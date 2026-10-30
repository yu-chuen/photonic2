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
    public partial class Rework_Dlg : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl;
        Data_UnitTempStorage m_DataUnit;

        //contructor////////////////////////////////////////////////////////////////////////////////////////
        public Rework_Dlg()
        {
            InitializeComponent();
            m_SeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////
        private void Rework_Dlg_Load(object sender, EventArgs e)
        {
            //start the update ui timer
            m_Timer.Start();

            //reset flags
            m_DataUnit.Flag_Rework_Station_OP85_b = false;
            m_DataUnit.Flag_Rework_Station_OP90_b = false;
            m_DataUnit.Flag_Rework_Station_OP100_b = false;
            m_DataUnit.Flag_Rework_Station_OP115_b = false;
            m_DataUnit.Flag_Rework_Station_OP117_b = false;

            m_DataUnit.Flag_Rework_AwaitSelection_b = false;

            //disable check box first
            cb_StationOP85.Enabled = false;
            cb_StationOP90.Enabled = false;
            cb_StationOP100.Enabled = false;
            cb_StationOP115.Enabled = false;
            cb_StationOP117.Enabled = false;

            //update checkbox
            Update_CheckBox(cb_StationOP85, m_DataUnit.Flag_Rework_Station_OP85_b);
            Update_CheckBox(cb_StationOP90, m_DataUnit.Flag_Rework_Station_OP90_b);
            Update_CheckBox(cb_StationOP100, m_DataUnit.Flag_Rework_Station_OP100_b);
            Update_CheckBox(cb_StationOP115, m_DataUnit.Flag_Rework_Station_OP115_b);
            Update_CheckBox(cb_StationOP117, m_DataUnit.Flag_Rework_Station_OP117_b);
        }

        private void Rework_Dlg_FormClosing(object sender, FormClosingEventArgs e)
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

        //Update UI Timer///////////////////////////////////////////////////////////////////////////////////////
        private void m_Timer_Tick(object sender, EventArgs e)
        {
            m_Timer.Stop();

            //update UI
            if (m_DataUnit.Flag_Conv_OP85_Sending_b == true)
            {
                lbl_Status.Text = "Pallet is sending to Station OP90.";
                lbl_Instruction.Text = "Please wait until the pallet has reach the next station.";
            }
            else if (m_DataUnit.Flag_Rework_Insert_b == true) //after user press insert button
            {
                //Solder station 3 is sending pallet
                if (m_SeqMachineCtrl.m_DataUnit.Flag_Conv_OP80_Sending_b == true)
                {
                    lbl_Status.Text = "Pallet is sending from OP80";
                    lbl_Instruction.Text = "Please wait until the pallet reach OP85 and finished its process.";
                }
                //Tell user to insert pallet into OP85 when the sliding door is up
                else if (m_DataUnit.Flag_Rework_AwaitPallet_b == true)
                {
                    lbl_Status.Text = "Awaiting pallet present...";
                    lbl_Instruction.Text = "Please insert pallet.";
                }
                //enable checkbox
                else if (m_DataUnit.Flag_Rework_AwaitSelection_b == true)
                {
                    lbl_Status.Text = "Awaiting rework selection...";
                    lbl_Instruction.Text = "Please select the station for rework and press Done button.";

                    cb_StationOP85.Enabled = true;
                    cb_StationOP90.Enabled = true;
                    cb_StationOP100.Enabled = true;
                    cb_StationOP115.Enabled = true;
                    cb_StationOP117.Enabled = true;
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

        //button click//////////////////////////////////////////////////////////////////////////////////////
        private void btn_Insert_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_Rework_Insert_b = true;

            //disable button after pressed
            btn_Insert.Enabled = false;
        }

        private void btn_Cancel_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_Rework_Insert_b = false;

            //stop timer
            m_Timer.Stop();

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btn_Done_Click(object sender, EventArgs e)
        {
            //set flags
            m_DataUnit.Flag_Rework_Insert_b = false;
            m_DataUnit.Flag_Rework_Done_b = true;

            //stop timer
            m_Timer.Stop();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        //checkbox click events////////////////////////////////////////////////////////////////////////////////////
        private void cb_StationOP85_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Rework_Station_OP85_b = (m_DataUnit.Flag_Rework_Station_OP85_b == true) ? false : true;
            Update_CheckBox(cb_StationOP85, m_DataUnit.Flag_Rework_Station_OP85_b);
        }

        private void cb_StationOP90_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Rework_Station_OP90_b = (m_DataUnit.Flag_Rework_Station_OP90_b == true) ? false : true;
            Update_CheckBox(cb_StationOP90, m_DataUnit.Flag_Rework_Station_OP90_b);
        }

        private void cb_StationOP100_Click(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Rework_Station_OP100_b = (m_DataUnit.Flag_Rework_Station_OP100_b == true) ? false : true;
            Update_CheckBox(cb_StationOP100, m_DataUnit.Flag_Rework_Station_OP100_b);
        }

        private void cb_StationOP115_CheckedChanged(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Rework_Station_OP115_b = (m_DataUnit.Flag_Rework_Station_OP115_b == true) ? false : true;
            Update_CheckBox(cb_StationOP115, m_DataUnit.Flag_Rework_Station_OP115_b);
        }

        private void cb_StationOP117_CheckedChanged(object sender, EventArgs e)
        {
            m_DataUnit.Flag_Rework_Station_OP117_b = (m_DataUnit.Flag_Rework_Station_OP117_b == true) ? false : true;
            Update_CheckBox(cb_StationOP117, m_DataUnit.Flag_Rework_Station_OP117_b);
        }
    }
}