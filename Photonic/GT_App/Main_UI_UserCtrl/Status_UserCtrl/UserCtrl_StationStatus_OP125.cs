using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Threading;

namespace GT_App
{
    public partial class UserCtrl_StationStatus_OP125 : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Data_UnitTempStorage m_DataUnit;
        UserCtrl_StationStatus_OP125 m_Dlg;

        List<UserCtrl_StatusStrip> m_ll_StatusStrips = new List<UserCtrl_StatusStrip>();
        List<UserCtrl_StatusStrip_String> m_ll_StatusStripStrings = new List<UserCtrl_StatusStrip_String>();

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StationStatus_OP125()
        {
            InitializeComponent();
            m_Dlg = this;
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
            m_DataUnit = m_SeqMachineControl.m_DataUnit;
        }

        //Clean Up/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            if (backgroundWorker.IsBusy == true)
            {
                backgroundWorker.CancelAsync();
            }
        }

        //Dialog Loaded////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_StationStatus_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(3, 20);
            int iCount = 0;

            //Set title
            lbl_Title.Text = "Manual Station OP125";

            //Status strip ui position adjustment
            pPoint.X = 5;
            pPoint.Y = lbl_Title.Size.Height + 5;

            //add Status strips
            while (iCount < 50)
            {
                if (Add_StatusStrips(iCount) == false) break;

                //position
                m_ll_StatusStrips[iCount].Location = pPoint;
                this.Controls.Add(m_ll_StatusStrips[iCount]);

                //update points
                pPoint.Y += m_ll_StatusStrips[iCount].Height;
                iCount++;
            }

            pPoint.Y += 2;

            iCount = 0;

            while (iCount < 50)
            {
                if (Add_StatusStripsString(iCount) == false) break;

                //Add status strip string for Serial number
                m_ll_StatusStripStrings[iCount].Location = pPoint;
                this.Controls.Add(m_ll_StatusStripStrings[iCount]);

                pPoint.Y += m_ll_StatusStripStrings[iCount].Height;
                iCount++;
            }

            //Start background worker
            backgroundWorker.RunWorkerAsync();

            //resize this user control auto
            int iWidth = this.Size.Width;
            int iHeight = lbl_Title.Location.Y + lbl_Title.Size.Height + (m_ll_StatusStrips[0].Size.Height * m_ll_StatusStrips.Count) + (m_ll_StatusStripStrings[0].Size.Height * m_ll_StatusStripStrings.Count) + 10;
            this.Size = new Size(iWidth, iHeight);
        }

        //Back Ground Worker Thread////////////////////////////////////////////////////////////////////////////////////
        private void backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker)sender;
            while (worker.CancellationPending == false)
            {
                m_Dlg.Invoke((MethodInvoker)delegate { Update_UI(); });
                Thread.Sleep(100);
            }
        }

        //Update UI////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI()
        {
            int iCount = 0;

                      m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_StationOP125_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_StationOP125_Sending_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_StationOP125_Trigger_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_StationOP125_Done_b);

            if (m_DataUnit.Flag_EnableManualOP125_b == true)
            {
                lbl_Title.BackColor = Color.Green;
            }
            else
            {
                lbl_Title.BackColor = Color.Red;
            }

            iCount = 0;
                      m_ll_StatusStripStrings[iCount].Set_Label("Cycle Time: " + m_DataUnit.Cycle_Time_OP125);
            iCount++; m_ll_StatusStripStrings[iCount].Set_Label("Result: " + m_DataUnit.HeightCheck_Result_str);
        }


        //Add Status Strips////////////////////////////////////////////////////////////////////////
        //Note; Please take note of the iCount value which must be sync with the update ui function
        public bool Add_StatusStrips(int iCount)
        {
            bool bRet = true;
            String strLabel = "";

            //Get String
            switch (iCount)
            {
                default: bRet = false; break;

                case 0: strLabel = "Station Ready"; break;
                case 1: strLabel = "Pallet Sending"; break;
                case 2: strLabel = "Station Triggered"; break;
                case 3: strLabel = "Station Done"; break;
            }

            //add to list
            if (bRet != false) { m_ll_StatusStrips.Add(new UserCtrl_StatusStrip(strLabel)); }

            return bRet;
        }

        //Add Status Strip String//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note; Please take note of the iCount value which must be sync with the update ui function
        public bool Add_StatusStripsString(int iCount)
        {
            bool bRet = true;

            switch (iCount)
            {
                default: bRet = false; break;

                case 0: break;		//display cycle time
                case 1: break;      //display result

            }
            //add to list
            if (bRet == true) m_ll_StatusStripStrings.Add(new UserCtrl_StatusStrip_String());

            return bRet;
        }
    }
}