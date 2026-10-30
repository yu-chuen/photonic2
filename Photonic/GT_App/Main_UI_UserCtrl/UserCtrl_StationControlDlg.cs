using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class UserCtrl_StationControlDlg : UserControl
    {
        //variables
        public event PropertyChangedEventHandler PropertyChanged;

        public String m_strLabel = "";
        String m_strStationSelectCondition = "";

        bool m_bFlag_StationRunning = false;

        //Constructor////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StationControlDlg(String strLabel)
        {
            InitializeComponent();

            m_strLabel = strLabel;
        }

        //dialog loaded////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_StationControlDlg_Load(object sender, EventArgs e)
        {
            groupBox1.Text = m_strLabel;
        }

        //update UI///////////////////////////////////////////////////////////////////////////////////
        public void Update_UI(bool bStatRun)
        {
            if (bStatRun != m_bFlag_StationRunning)
            {
                m_bFlag_StationRunning = bStatRun;
                if (m_bFlag_StationRunning)
                {
                    btn_StartStationRun.Enabled = false;
                    btn_StationInitialize.Enabled = false;
                    lbl_Status.Text = "Running";
                }
                else
                {
                    btn_StartStationRun.Enabled = true;
                    btn_StationInitialize.Enabled = true;
                    lbl_Status.Text = "Stop";
                }
            }
        }

        public void Update_UI(int iStatus)
        {
            //iStatus: -1 => Unknown, 0 => Stop, 1 => Running, 2 => Initializing, 3 => Idle

            if (iStatus == 0)
            {
                btn_StartStationRun.Enabled = true;
                btn_StationInitialize.Enabled = true;

                btn_StartStationRun.BackColor = Color.LawnGreen;

                lbl_Status.Text = "Stop";
                lbl_Status.BackColor = Color.IndianRed;
                lbl_Status.ForeColor = Color.Black;
            }
            else if (iStatus == 1)
            {
                btn_StartStationRun.Enabled = false;
                btn_StationInitialize.Enabled = false;

                btn_StartStationRun.BackColor = Color.FromKnownColor(KnownColor.ControlLight);

                lbl_Status.Text = "Running";
                lbl_Status.BackColor = Color.LawnGreen;
                lbl_Status.ForeColor = Color.Black;
            }
            else if (iStatus == 2)
            {
                btn_StartStationRun.Enabled = false;
                btn_StationInitialize.Enabled = false;

                lbl_Status.Text = "Initializing";
                lbl_Status.BackColor = Color.Yellow;
                lbl_Status.ForeColor = Color.Black;
            }
            else if (iStatus == 3)
            {
                btn_StartStationRun.Enabled = true;
                btn_StationInitialize.Enabled = true;

                btn_StartStationRun.BackColor = Color.LawnGreen;

                lbl_Status.Text = "Idle";
                lbl_Status.BackColor = Color.White;
                lbl_Status.ForeColor = Color.Black;
            }
            else //-1
            {
                btn_StartStationRun.Enabled = false;
                btn_StationInitialize.Enabled = false;

                lbl_Status.Text = "Unknown";
                lbl_Status.BackColor = Color.Black;
                lbl_Status.ForeColor = Color.White;
            }

            btn_StopStationRun.BackColor = Color.Red;
        }

        //Properties to signal event to the main dlg//////////////////////////////
        public String StationSelectCondition_str
        {
            get { return m_strStationSelectCondition; }
            set
            {
                if (value != m_strStationSelectCondition)
                {
                    m_strStationSelectCondition = value;
                    if (PropertyChanged != null)
                        PropertyChanged(this, new PropertyChangedEventArgs(m_strStationSelectCondition));
                }
            }
        }

        //button events///////////////////////////////////////////////////////////////////////////////
        private void btn_StartStationRun_Click(object sender, EventArgs e)
        {
            StationSelectCondition_str = "run";
        }

        private void btn_StopStationRun_Click(object sender, EventArgs e)
        {
            StationSelectCondition_str = "Stop";
        }

        private void btn_StationInitialize_Click(object sender, EventArgs e)
        {
            StationSelectCondition_str = "Init";
        }

        private void btn_SinglStationTrig_Click(object sender, EventArgs e)
        {
            StationSelectCondition_str = "Trigger";
        }
    }
}