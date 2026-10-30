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
    public partial class UserCtrl_StationStatus_OP100 : UserControl
    {
        //variables
        SeqMachineControl m_SeqMachineControl;
        Data_UnitTempStorage m_DataUnit;
        UserCtrl_StationStatus_OP100 m_Dlg;

        List<UserCtrl_StatusStrip> m_ll_StatusStrips = new List<UserCtrl_StatusStrip>();
        List<UserCtrl_StatusStrip_String> m_ll_StatusStripStrings = new List<UserCtrl_StatusStrip_String>();
        //List<UserCtrl_StatusStrip_String_Short> m_ll_StatusStripStrings_Short = new List<UserCtrl_StatusStrip_String_Short>();
        List<UserCtrl_StatusStrip_String_Short_Dual> m_ll_StatusStripStrings_Short_Dual = new List<UserCtrl_StatusStrip_String_Short_Dual>();

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_StationStatus_OP100()
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
            lbl_Title.Text = "Manual Station OP100";

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

            ////Add status strip string for wire tester results
            //iCount = 0;
            //while (true)
            //{
            //    //add  status strings
            //    if (Add_StatusStripsString_Short(iCount) == false) break;

            //    //position
            //    m_ll_StatusStripStrings_Short[iCount].Location = pPoint;
            //    this.Controls.Add(m_ll_StatusStripStrings_Short[iCount]);

            //    //update points position
            //    if (iCount < 6)
            //    {
            //        pPoint.Y += m_ll_StatusStripStrings_Short[iCount].Height;
            //    }
            //    else if (iCount == 6)
            //    {
            //        pPoint.X = m_ll_StatusStripStrings_Short[0].Location.X + m_ll_StatusStripStrings_Short[0].Size.Width;
            //        pPoint.Y = m_ll_StatusStripStrings_Short[0].Location.Y;
            //    }
            //    else if (iCount > 6)
            //    {
            //        pPoint.Y += m_ll_StatusStripStrings_Short[iCount].Height;
            //    }

            //    iCount++;
            //}

            //pPoint.X = m_ll_StatusStripStrings_Short[6].Location.X;

            //Add status strip string picbox
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
            int iHeight = lbl_Title.Location.Y + lbl_Title.Size.Height + (m_ll_StatusStrips[0].Size.Height * m_ll_StatusStrips.Count) + (m_ll_StatusStripStrings[0].Size.Height * m_ll_StatusStripStrings.Count) + /*(m_ll_StatusStripStrings_Short[0].Size.Height * ((m_ll_StatusStripStrings_Short.Count) / 2))*/ + (m_ll_StatusStripStrings_Short_Dual[0].Size.Height * 3) + 10;
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

            if (
                m_SeqMachineControl.m_Module_OP100_Buffer.Module_Running  == true &&
                m_SeqMachineControl.m_Module_OP100_Station.Module_Running == true
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

            //update status flag
            iCount = -1;
            iCount++; m_ll_StatusStrips[iCount].Update_UI(bFlag_Running);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Buffer_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Buffer_Sending_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Sending_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Outgoing_Ready_b);
            iCount++; m_ll_StatusStrips[iCount].Update_UI(m_DataUnit.Flag_Conv_OP100_Outgoing_Sending_b);

            //Update status strip strings
            iCount = -1;
            iCount++; m_ll_StatusStripStrings[iCount].Set_Label("Cycle Time: " + m_DataUnit.Cycle_Time_OP100_str);

            ////update result strip strings
            //iCount = -1;

            //if (m_DataUnit.OP100_WT_Result_OP90_LN1_str.ToLower() != "open") { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N1: " + m_DataUnit.OP100_WT_Result_OP90_LN1_str + " Ω"); }
            //else { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N1: " + m_DataUnit.OP100_WT_Result_OP90_LN1_str); }

            //if (m_DataUnit.OP100_WT_Result_OP90_LN2_str.ToLower() != "open") { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N2: " + m_DataUnit.OP100_WT_Result_OP90_LN2_str + " Ω"); }
            //else { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N2: " + m_DataUnit.OP100_WT_Result_OP90_LN2_str); }

            //if (m_DataUnit.OP100_WT_Result_OP90_LN3_str.ToLower() != "open") { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N3: " + m_DataUnit.OP100_WT_Result_OP90_LN3_str + " Ω"); }
            //else { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-N3: " + m_DataUnit.OP100_WT_Result_OP90_LN3_str); }

            //if (m_DataUnit.OP100_WT_Result_OP90_LR0_str.ToLower() != "open") { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-R0: " + m_DataUnit.OP100_WT_Result_OP90_LR0_str + " Ω"); }
            //else { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("L-R0: " + m_DataUnit.OP100_WT_Result_OP90_LR0_str); }

            //if (m_DataUnit.OP100_WT_Result_OP90_R0R1_str.ToLower() != "open") { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("R0-R1: " + m_DataUnit.OP100_WT_Result_OP90_R0R1_str + " Ω"); }
            //else { iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("R0-R1: " + m_DataUnit.OP100_WT_Result_OP90_R0R1_str); }

            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("Temp: " + m_DataUnit.OP100_WT_Result_OP90_Temperature_db + " °C");
            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("Grade: " + m_DataUnit.OP100_WT_Result_OP90_Overall_Grade_str);

            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("HTR Min: " + m_DataUnit.OP100_WT_Result_OP90_HTR_Min_str + " Ω");
            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("HTR Avg: " + m_DataUnit.OP100_WT_Result_OP90_HTR_Avg_str + " Ω");
            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("HTR Max: " + m_DataUnit.OP100_WT_Result_OP90_HTR_Max_str + " Ω");

            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("RTD Min: " + m_DataUnit.OP100_WT_Result_OP90_RTD_Min_str + " Ω");
            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("RTD Avg: " + m_DataUnit.OP100_WT_Result_OP90_RTD_Avg_str + " Ω");
            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("RTD Max: " + m_DataUnit.OP100_WT_Result_OP90_RTD_Max_str + " Ω");

            //iCount++; m_ll_StatusStripStrings_Short[iCount].Set_Label("Result: " + m_DataUnit.OP100_WT_Result_OP90_Overall_Result_str);

            //if (m_DataUnit.OP100_WT_Result_OP90_Overall_Result_str == "Pass")
            //{
            //    m_ll_StatusStripStrings_Short[iCount].BackColor = Color.LimeGreen;
            //}
            //else if (m_DataUnit.OP100_WT_Result_OP90_Overall_Result_str == "Fail")
            //{
            //    m_ll_StatusStripStrings_Short[iCount].BackColor = Color.IndianRed;
            //}
            //else
            //{
            //    m_ll_StatusStripStrings_Short[iCount].BackColor = Color.Transparent;
            //}

            //Update status strip strings short
            iCount = -1;
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire1_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire2_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire3_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire4_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire5_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);
            iCount++; m_ll_StatusStripStrings_Short_Dual[iCount].Set_Result_Label(m_DataUnit.OP100_VisionResult_OP90_Wire6_str); Change_StatusStripsString_Short_Dual_Result_BackColor(iCount); Change_StatusStripsString_Short_Dual_Title_BackColor(iCount);

            if (m_DataUnit.Flag_EnableManualOP100_b == true)
            {
                lbl_Title.BackColor = Color.Green;
            }
            else
            {
                lbl_Title.BackColor = Color.Red;
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
                case 4: if (m_DataUnit.RFID_OP100_Buffer_SN_str.ToUpper().Contains("LV")) { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Blue); } else if (m_DataUnit.RFID_OP100_Buffer_SN_str.ToUpper().Contains("HV")) { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.LimeGreen); } else { m_ll_StatusStripStrings_Short_Dual[iCount].Set_Title_BackColor(Color.Transparent); } break;
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
                case 1: strLabel = "Buffer Ready"; break;
                case 2: strLabel = "Buffer Sending"; break;
                case 3: strLabel = "Station Ready"; break;
                case 4: strLabel = "Station Sending"; break;
                case 5: strLabel = "Outgoing Ready"; break;
                case 6: strLabel = "Outgoing Sending"; break;
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

        //Add Status Strip String//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note; Please take note of the iCount value which must be sync with the update ui function
        //public bool Add_StatusStripsString_Short(int iCount)
        //{
        //    bool bRet = true;

        //    switch (iCount)
        //    {
        //        default: bRet = false; break;

        //        case 0: break;	//display L-N1
        //        case 1: break;	//display L-N2 
        //        case 2: break;	//display L-N3 
        //        case 3: break;  //display L-R0
        //        case 4: break;	//display R0-R1
        //        case 5: break;	//display Temperature
        //        case 6: break;	//display Overall Grade
        //        case 7: break;	//display HTR Min
        //        case 8: break;	//display HTR Avg
        //        case 9: break;	//display HTR Max
        //        case 10: break;	//display RTD Min
        //        case 11: break; //display RTD Avg
        //        case 12: break; //display RTD Max
        //        case 13: break; //display Overall Result
        //    }

        //    //add to list
        //    if (bRet == true) m_ll_StatusStripStrings_Short.Add(new UserCtrl_StatusStrip_String_Short());

        //    return bRet;
        //}

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
    }
}