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
    public partial class UserCtrl_StationStatus_OP60 : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Data_UnitTempStorage m_DataUnit;
        UserCtrl_StationStatus_OP60 m_Dlg;

        List<UserCtrl_StatusStrip> m_ll_StatusStrips = new List<UserCtrl_StatusStrip>();
        UserCtrl_StatusStrip_String m_UserCtrl_StatusStripString = new UserCtrl_StatusStrip_String();

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StationStatus_OP60()
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
            lbl_Title.Text = "Solder1 OP60";

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

            //Add status strip string for cycle time
            m_UserCtrl_StatusStripString.Location = pPoint;
            this.Controls.Add(m_UserCtrl_StatusStripString);

            //Start background worker
            backgroundWorker.RunWorkerAsync();

            //resize this user control auto
            int iWidth = this.Size.Width;
            int iHeight = lbl_Title.Location.Y + lbl_Title.Size.Height + (m_ll_StatusStrips[0].Size.Height * m_ll_StatusStrips.Count) + m_UserCtrl_StatusStripString.Size.Height + 10;
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
            bool bFlag_Running = false;

            if (true
               )
            {
                bFlag_Running = true;
            }

            if (bFlag_Running == true)
            {
                m_ll_StatusStrips[0].Set_Label("Sequence: Running");
            }
            else
            {
                m_ll_StatusStrips[0].Set_Label("Sequence: Stop");
            }

            int iCount = -1;

            iCount++; m_ll_StatusStrips[iCount].Update_UI(bFlag_Running);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP60_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP60_Sending_b);
            
            if (m_DataUnit.Flag_EnableSolderStation1_b == true)
            {
                lbl_Title.BackColor = Color.Green;
            }
            else
            {
                lbl_Title.BackColor = Color.Red;
            }

            m_UserCtrl_StatusStripString.Set_Label("Cycle Time: " + m_DataUnit.Cycle_Time_OP60_str);
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

                case 0: strLabel = "Sequence: Stop"; break;
                case 1: strLabel = "Solder Station 1 Ready"; break;
                case 2: strLabel = "Solder Station 1 Sending"; break;
            }

            //add to list
            if (bRet != false) { m_ll_StatusStrips.Add(new UserCtrl_StatusStrip(strLabel)); }

            return bRet;
        }
    }
}