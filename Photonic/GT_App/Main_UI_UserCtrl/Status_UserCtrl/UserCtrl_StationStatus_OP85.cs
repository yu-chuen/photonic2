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
    public partial class UserCtrl_StationStatus_OP85 : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Data_UnitTempStorage m_DataUnit;
        UserCtrl_StationStatus_OP85 m_Dlg;

        int m_iSizeHeight = 0;

        List<UserCtrl_StatusStrip> m_ll_StatusStrips = new List<UserCtrl_StatusStrip>();
        List<UserCtrl_StatusStrip_String> m_ll_StatusStripStrings = new List<UserCtrl_StatusStrip_String>();
        List<UserCtrl_StatusStrip_String_Short_Dual> m_ll_StatusStripStrings_Short_Dual = new List<UserCtrl_StatusStrip_String_Short_Dual>();

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StationStatus_OP85()
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
            lbl_Title.Text = "Vision Inspection Station OP85";

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

            //add status strip string
            iCount = 0;
            while (iCount < 50)
            {
                if (Add_StatusStripsString(iCount) == false) break;

                //Add status strip string
                m_ll_StatusStripStrings[iCount].Location = pPoint;
                this.Controls.Add(m_ll_StatusStripStrings[iCount]);

                pPoint.Y += m_ll_StatusStripStrings[iCount].Height;
                iCount++;
            }

            //Add status strip string dual
            iCount = 0;
            while (true)
            {
                //add  status strings
                if (Add_StatusStripsString_Short_Dual(iCount) == false) break;

                //position
                m_ll_StatusStripStrings_Short_Dual[iCount].Location = pPoint;
                this.Controls.Add(m_ll_StatusStripStrings_Short_Dual[iCount]);

                //update points position
                if (iCount < 2)
                {
                    pPoint.Y += m_ll_StatusStripStrings_Short_Dual[iCount].Height;
                }
                else if (iCount == 2)
                {
                    pPoint.X = m_ll_StatusStripStrings_Short_Dual[0].Location.X + m_ll_StatusStripStrings_Short_Dual[0].Size.Width;
                    pPoint.Y = m_ll_StatusStripStrings_Short_Dual[0].Location.Y;
                }
                else if (iCount > 2)
                {
                    pPoint.Y += m_ll_StatusStripStrings_Short_Dual[iCount].Height;
                }

                iCount++;
            }

            //Start background worker
            backgroundWorker.RunWorkerAsync();

            //resize this user control auto
            int iWidth = this.Size.Width;
            int iHeight = m_iSizeHeight = lbl_Title.Location.Y + lbl_Title.Size.Height + (m_ll_StatusStrips[0].Size.Height * m_ll_StatusStrips.Count) + (m_ll_StatusStripStrings[0].Size.Height * m_ll_StatusStripStrings.Count) + (m_ll_StatusStripStrings_Short_Dual[0].Size.Height * ((m_ll_StatusStripStrings_Short_Dual.Count) / 2)) + 10;
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

            int iCount;

            //Update status strips
            iCount = -1;
            iCount++; m_ll_StatusStrips[iCount].Update_UI(bFlag_Running);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP85_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP85_Sending_b);

            //update string
            iCount = -1;
            iCount++; m_ll_StatusStripStrings[iCount].Set_Label("Cycle Time: " + m_DataUnit.Cycle_Time_OP85_str);

            //Update status strip strings short dual
            iCount = -1;
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal1_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal2_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal3_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal4_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal5_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP85_VisionResult_Terminal6_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);

            if (m_DataUnit.Flag_Enable_Module_OP85_b == true)
            {
                lbl_Title.BackColor = Color.Green;
            }
            else
            {
                lbl_Title.BackColor = Color.Red;
            }

            //for test mode only
            if (m_DataUnit.Flag_Enable_TestMode_b == true)
            {
                this.Size = new Size(this.Size.Width, m_iSizeHeight + 53);
                btn_OP85_VisionResult.Visible = true;
            }
            else
            {
                this.Size = new Size(this.Size.Width, m_iSizeHeight);
                btn_OP85_VisionResult.Visible = false;
            }
        }

        public void Change_StatusStripsString_Short_Dual_Result_BackColor(int iCount)
        {
            if (m_ll_StatusStripStrings_Short_Dual[iCount].Get_Result_Label().ToLower().Contains("pass"))
            {
                m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_BackColor(Color.LawnGreen);
            }
            else if (m_ll_StatusStripStrings_Short_Dual[iCount].Get_Result_Label().ToLower().Contains("fail"))
            {
                m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_BackColor(Color.IndianRed);
            }
            else
            {
                m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_BackColor(Color.Transparent);
            }
        }

        public void Change_StatusStripsString_Short_Dual_Title_BackColor(int iCount)
        {
            switch (iCount)
            {
                //case 0: m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Yellow); break;
                //case 1: m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Purple); break;
                //case 2: m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Orange); break;
                //case 3: m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.White); break;
                case 4: if (m_DataUnit.RFID_OP85_SN_str.ToUpper().Contains("LV")) { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Blue); } else if (m_DataUnit.RFID_OP85_SN_str.ToUpper().Contains("HV")) { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.LimeGreen); } else { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Transparent); } break;
                //case 5: m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Black); break;
            }
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
                case 1: strLabel = "Station Ready"; break;
                case 2: strLabel = "Station Sending"; break;
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
            }

            //add to list
            if (bRet == true) m_ll_StatusStripStrings.Add(new UserCtrl_StatusStrip_String());

            return bRet;
        }

        //Add Status Strip String Short Dual//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note; Please take note of the iCount value which must be sync with the update ui function
        public bool Add_StatusStripsString_Short_Dual(int iCount)
        {
            bool bRet = true;
            string strTitle = "...";
            Color clrTitle_ForeColor = Color.Black;
            Color clrTitle_BackColor = Color.Transparent;

            switch (iCount)
            {
                default: bRet = false; break;

                case 0: { strTitle = "R0"; clrTitle_BackColor = Color.Yellow; } break;	                                    //display Terminal 1
                case 1: { strTitle = "N3"; clrTitle_BackColor = Color.Orange; } break;	                                    //display Terminal 2
                case 2: { strTitle = "R1"; clrTitle_ForeColor = Color.White; clrTitle_BackColor = Color.Purple; } break;	//display Terminal 3
                case 3: { strTitle = "N2"; clrTitle_BackColor = Color.White; } break;                                       //display Terminal 4
                case 4: { strTitle = "N1"; } break;	                                                                        //display Terminal 5
                case 5: { strTitle = "L"; clrTitle_ForeColor = Color.White; clrTitle_BackColor = Color.Black; } break;	    //display Terminal 6
            }

            //add to list
            if (bRet == true) m_ll_StatusStripStrings_Short_Dual.Add(new UserCtrl_StatusStrip_String_Short_Dual(strTitle, clrTitle_ForeColor, clrTitle_BackColor));

            return bRet;
        }

        private void btn_OP85_VisionResult_Click(object sender, EventArgs e)
        {
            //test mode only
            if (m_DataUnit.Flag_Enable_TestMode_b == true)
            {
                MessageBox_OP85_VisionResult dlg = new MessageBox_OP85_VisionResult(m_SeqMachineControl);
                dlg.ShowDialog();
                dlg.Dispose();
            }
        }
    }
}