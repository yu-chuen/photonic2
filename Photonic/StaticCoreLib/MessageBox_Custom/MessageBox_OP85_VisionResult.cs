using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Text.RegularExpressions;

namespace StaticCoreLib
{
    public partial class MessageBox_OP85_VisionResult : Form
    {
        //variables
        SeqMachineControl m_SeqMachineCtrl = null;
        Data_UnitTempStorage m_DataUnit = null;
        Data_PackageFile m_DataPackage = null;

        List<string> list_Row;

        public MessageBox_OP85_VisionResult(SeqMachineControl mSeq)
        {
            InitializeComponent();

            m_SeqMachineCtrl = mSeq;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;
            m_DataPackage = m_SeqMachineCtrl.m_DataPackageFile;
        }

        private void MessageBox_OP85_VisionResult_Load(object sender, EventArgs e)
        {
            this.TopMost = true;

            int iColumnIndex = -1;
            
            //create columns
            dgv_Result.ColumnCount = 5;
            iColumnIndex++; dgv_Result.Columns[iColumnIndex].Name = "Items";
            iColumnIndex++; dgv_Result.Columns[iColumnIndex].Name = "Min.";
            iColumnIndex++; dgv_Result.Columns[iColumnIndex].Name = "Value";
            iColumnIndex++; dgv_Result.Columns[iColumnIndex].Name = "Max.";
            iColumnIndex++; dgv_Result.Columns[iColumnIndex].Name = "Result";

            foreach (DataGridViewColumn col in dgv_Result.Columns)
            {
                col.SortMode = DataGridViewColumnSortMode.NotSortable;
            }

            dgv_Result.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            addRow("Terminal 1 - Width", m_DataUnit.OP85_Vision_Terminal1_Width_Min_str, m_DataUnit.OP85_Vision_Terminal1_Width_str, m_DataUnit.OP85_Vision_Terminal1_Width_Max_str, m_DataUnit.OP85_Vision_Terminal1_Width_Result_str);
            addRow("Terminal 2 - Width", m_DataUnit.OP85_Vision_Terminal2_Width_Min_str, m_DataUnit.OP85_Vision_Terminal2_Width_str, m_DataUnit.OP85_Vision_Terminal2_Width_Max_str, m_DataUnit.OP85_Vision_Terminal2_Width_Result_str);
            addRow("Terminal 3 - Width", m_DataUnit.OP85_Vision_Terminal3_Width_Min_str, m_DataUnit.OP85_Vision_Terminal3_Width_str, m_DataUnit.OP85_Vision_Terminal3_Width_Max_str, m_DataUnit.OP85_Vision_Terminal3_Width_Result_str);
            addRow("Terminal 4 - Width", m_DataUnit.OP85_Vision_Terminal4_Width_Min_str, m_DataUnit.OP85_Vision_Terminal4_Width_str, m_DataUnit.OP85_Vision_Terminal4_Width_Max_str, m_DataUnit.OP85_Vision_Terminal4_Width_Result_str);
            addRow("Terminal 5 - Width", m_DataUnit.OP85_Vision_Terminal5_Width_Min_str, m_DataUnit.OP85_Vision_Terminal5_Width_str, m_DataUnit.OP85_Vision_Terminal5_Width_Max_str, m_DataUnit.OP85_Vision_Terminal5_Width_Result_str);
            addRow("Terminal 6 - Width", m_DataUnit.OP85_Vision_Terminal6_Width_Min_str, m_DataUnit.OP85_Vision_Terminal6_Width_str, m_DataUnit.OP85_Vision_Terminal6_Width_Max_str, m_DataUnit.OP85_Vision_Terminal6_Width_Result_str);

            addRow("Terminal 1 - Length", m_DataUnit.OP85_Vision_Terminal1_Length_Min_str, m_DataUnit.OP85_Vision_Terminal1_Length_str, m_DataUnit.OP85_Vision_Terminal1_Length_Max_str, m_DataUnit.OP85_Vision_Terminal1_Length_Result_str);
            addRow("Terminal 2 - Length", m_DataUnit.OP85_Vision_Terminal2_Length_Min_str, m_DataUnit.OP85_Vision_Terminal2_Length_str, m_DataUnit.OP85_Vision_Terminal2_Length_Max_str, m_DataUnit.OP85_Vision_Terminal2_Length_Result_str);
            addRow("Terminal 3 - Length", m_DataUnit.OP85_Vision_Terminal3_Length_Min_str, m_DataUnit.OP85_Vision_Terminal3_Length_str, m_DataUnit.OP85_Vision_Terminal3_Length_Max_str, m_DataUnit.OP85_Vision_Terminal3_Length_Result_str);
            addRow("Terminal 4 - Length", m_DataUnit.OP85_Vision_Terminal4_Length_Min_str, m_DataUnit.OP85_Vision_Terminal4_Length_str, m_DataUnit.OP85_Vision_Terminal4_Length_Max_str, m_DataUnit.OP85_Vision_Terminal4_Length_Result_str);
            addRow("Terminal 5 - Length", m_DataUnit.OP85_Vision_Terminal5_Length_Min_str, m_DataUnit.OP85_Vision_Terminal5_Length_str, m_DataUnit.OP85_Vision_Terminal5_Length_Max_str, m_DataUnit.OP85_Vision_Terminal5_Length_Result_str);
            addRow("Terminal 6 - Length", m_DataUnit.OP85_Vision_Terminal6_Length_Min_str, m_DataUnit.OP85_Vision_Terminal6_Length_str, m_DataUnit.OP85_Vision_Terminal6_Length_Max_str, m_DataUnit.OP85_Vision_Terminal6_Length_Result_str);

            addRow("Terminal 1 - Distance", m_DataUnit.OP85_Vision_Terminal1_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal1_Distance_str, m_DataUnit.OP85_Vision_Terminal1_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal1_Distance_Result_str);
            addRow("Terminal 2 - Distance", m_DataUnit.OP85_Vision_Terminal2_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal2_Distance_str, m_DataUnit.OP85_Vision_Terminal2_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal2_Distance_Result_str);
            addRow("Terminal 3 - Distance", m_DataUnit.OP85_Vision_Terminal3_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal3_Distance_str, m_DataUnit.OP85_Vision_Terminal3_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal3_Distance_Result_str);
            addRow("Terminal 4 - Distance", m_DataUnit.OP85_Vision_Terminal4_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal4_Distance_str, m_DataUnit.OP85_Vision_Terminal4_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal4_Distance_Result_str);
            addRow("Terminal 5 - Distance", m_DataUnit.OP85_Vision_Terminal5_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal5_Distance_str, m_DataUnit.OP85_Vision_Terminal5_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal5_Distance_Result_str);
            addRow("Terminal 6 - Distance", m_DataUnit.OP85_Vision_Terminal6_Distance_Min_str, m_DataUnit.OP85_Vision_Terminal6_Distance_str, m_DataUnit.OP85_Vision_Terminal6_Distance_Max_str, m_DataUnit.OP85_Vision_Terminal6_Distance_Result_str);

            addRow("Overall", "-", "-", "-", m_DataUnit.OP85_Vision_Overall_Result_str);

            foreach (DataGridViewRow row in dgv_Result.Rows)
            {
                ChangeRowColor(row.Index);
            }

            //unselect all row when dialog loaded
            dgv_Result.ClearSelection();

            this.TopMost = false;
        }

        private void addRow(string item, string min, string value, string max, string result)
        {
            list_Row = new List<string>();
            list_Row.Add(item);
            list_Row.Add(min);
            list_Row.Add(value);
            list_Row.Add(max);
            list_Row.Add(result);

            dgv_Result.Rows.Add(list_Row.ToArray());
        }

        private void ChangeRowColor(int iRowIndex)
        {
            if (dgv_Result.Rows[iRowIndex].Cells[dgv_Result.Columns.Count - 1].Value.ToString() == "Pass") //cell for result
            {
                dgv_Result.Rows[iRowIndex].DefaultCellStyle.BackColor = Color.LimeGreen;
                dgv_Result.Rows[iRowIndex].DefaultCellStyle.ForeColor = Color.Black;
            }
            else
            {
                dgv_Result.Rows[iRowIndex].DefaultCellStyle.BackColor = Color.IndianRed;
                dgv_Result.Rows[iRowIndex].DefaultCellStyle.ForeColor = Color.White;
            }
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}