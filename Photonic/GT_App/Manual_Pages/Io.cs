using StaticCoreLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace GT_App
{
    public partial class Io : Form
    {

        SeqMachineControl m_pSeqMachineCtrl;
        System.Windows.Forms.Timer m_Timer = new Timer();

        List<UserCtrl_ManualPage_Template> m_list_userctrl_manualPage = new List<UserCtrl_ManualPage_Template>();
        List<TabPage> m_list_TabPage = new List<TabPage>();
        public Io()
        {
            InitializeComponent();
            m_pSeqMachineCtrl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            //Add tab Pages
            this.Add_UserCtrl();

            //set timer
            m_Timer.Interval = 10;
            m_Timer.Tick += new EventHandler(Update_UI);
            m_Timer.Start();
        }

        //Set the tab color and font/////////////////////////////////////////////////////////////////////////////////////////////////////
        private void tabctrl_IO_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat strFormat = new StringFormat();
            Graphics g = e.Graphics;
            //Brush br = new SolidBrush(Color.Black);
            Brush br;
            Font ft = new System.Drawing.Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

            //Get tab page
            TabPage tbPage = tabctrl_IO.TabPages[e.Index];

            //get tab bounds
            Rectangle rec_tabBounds = tabctrl_IO.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {
                br = new SolidBrush(Color.Red);
                g.FillRectangle(Brushes.White, e.Bounds);
            }
            else
            {
                br = new System.Drawing.SolidBrush(e.ForeColor);
                //e.DrawBackground();       //this will paint tab background color white
            }

            strFormat.Alignment = StringAlignment.Center;
            strFormat.LineAlignment = StringAlignment.Center;

            g.DrawString(tbPage.Text, ft, br, rec_tabBounds, strFormat);
        }




        //buttons control//////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_ESC_Click(object sender, EventArgs e)
        {
            foreach (UserCtrl_ManualPage_Template mUserCtrl in m_list_userctrl_manualPage)
                mUserCtrl.Dispose();

            m_Timer.Stop();
            this.Close();
        }

        //Update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Update_UI(object sender, EventArgs e)
        {
            m_Timer.Stop();

            String strTabText = tabctrl_IO.SelectedTab.Text;
            int iCount = 0;

            foreach (TabPage mtabpage in m_list_TabPage)
            {
                if (mtabpage.Text == strTabText) { m_list_userctrl_manualPage[iCount].Update_UI(); break; }
                iCount++;
            }

            m_Timer.Start();
        }

        //Add tabpage////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Add_TabPage(String strLabel, int iCount)
        {
            m_list_TabPage[iCount].Controls.Add(m_list_userctrl_manualPage[iCount]);
            m_list_TabPage[iCount].Text = strLabel;
            m_list_TabPage[iCount].AutoScroll = true;
            //MessageBox.Show(strLabel);
            this.tabctrl_IO.TabPages.Add(m_list_TabPage[iCount]);
        }

        private void Add_AllIO(UserCtrl_ManualPage_Template page, object module)
        {
            //var fields = module.GetType().GetFields(
            //    System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Instance);

            var fields = module.GetType().GetFields();

            foreach (var f in fields)
            {
                object v = f.GetValue(module);
                if (v == null) continue;                       // not created on this machine type

                if (v is Sensor || v is InputBit) page.Add_UserCtrlInput(v as dynamic);
                else if (v is OutputBit) page.Add_UserCtrlOutput(v as OutputBit);
                else if (v is Cylinder) page.Add_UserCtrlCylinder(v as Cylinder);
            }
        }

        private static MyIPort PortOf(InputBit b)
        {
            return (MyIPort)typeof(InputBit)
                .GetField("m_IPort", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(b);
        }
        private static MyOPort PortOf(OutputBit b)
        {
            return (MyOPort)typeof(OutputBit)
                .GetField("m_OPort", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(b);
        }

        private void Collect
            (object module,
                Dictionary<MyIPort, List<InputBit>> ins,
                Dictionary<MyOPort, List<OutputBit>> outs
            )
        {
            foreach (var f in module.GetType().GetFields())
            {
                object v = f.GetValue(module);
                if (v == null) continue;

                if (v is InputBit)
                {
                    var p = PortOf((InputBit)v);
                    //if (p == null) continue;
                    if (!ins.ContainsKey(p)) ins[p] = new List<InputBit>();
                    ins[p].Add((InputBit)v);
                    // ins[4] = {[Safety Relay (bit 2), EStop - OP90 (bit 3)]}
                    // ins = { 4: [Safety Relay (bit 2), EStop - OP90 (bit 3)]}
                }
                else if (v is OutputBit)
                {
                    var p = PortOf((OutputBit)v);
                    //if (p == null) continue;
                    if (!outs.ContainsKey(p)) outs[p] = new List<OutputBit>();
                    outs[p].Add((OutputBit)v);
                }
            }
        }


        public void Add_UserCtrl()
        {
            int iCount = 0;

            //add Control Panel------------------------------------------------------------------------------
            #region ControlPanel
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInAirPressure_UL);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInAirPressure_LL);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInSafetyRelay);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInEStopButton);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pInEStopButton_OP110);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation1_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation2_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation3_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation1_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation2_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_SolderStation3_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_VisionStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_HotBlower_Front);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpDoorLock_HotBlower_Back);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpKeySwitch);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_inpKeySwitch1);

            // DO
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Static);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightRed_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightYellow_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightGreen_Blink);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutTowerLightBuzzer);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutMaintenanceLight_VisionStat);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_SolderStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_HotBlowerStation);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_ControlPanel.m_pOutDoorLock_VisionStation);

            Add_TabPage("IO 1", iCount);
            #endregion

            //Add testing------------------------------------------------------------------------------
            iCount++;
            #region IO2
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            Add_AllIO(m_list_userctrl_manualPage[iCount], m_pSeqMachineCtrl.m_Module_ControlPanel);

            Add_TabPage("IO 2", iCount);
            #endregion

            //Add testing------------------------------------------------------------------------------
            #region IO by port
            var ins = new Dictionary<MyIPort, List<InputBit>>();
            var outs = new Dictionary<MyOPort, List<OutputBit>>();

            object[] modules =
            {
                m_pSeqMachineCtrl.m_Module_ControlPanel,
            };

            foreach (var m in modules)
                if (m != null) Collect(m, ins, outs);

            //// 1) find each port's number: ask InterfaceCards for 1, 2, 3 ... and match the object
            //var ic = m_pSeqMachineCtrl.m_InterfaceCards;
            //var inNo = new Dictionary<MyIPort, int>();
            //var outNo = new Dictionary<MyOPort, int>();

            //for (int n = 1; inNo.Count < ins.Count && n <= 8; n++)
            //{
            //    var p = ic.Get_InputPort(n);
            //    if (p != null && ins.ContainsKey(p) && !inNo.ContainsKey(p)) inNo[p] = n;
            //}

            //for (int n = 1; outNo.Count < outs.Count && n <= 8; n++)
            //{
            //    var p = ic.Get_OutputPort(n);
            //    if (p != null && outs.ContainsKey(p) && !outNo.ContainsKey(p)) outNo[p] = n;
            //}

            //// 2) one tab per port number
            //var portNums = new SortedSet<int>(inNo.Values); //{1,4,5}
            //portNums.UnionWith(outNo.Values);

            //foreach (int n in portNums)
            //{
            //    iCount++;                               // next page index
            //    m_list_TabPage.Add(new TabPage());
            //    m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            //    foreach (var kv in inNo.Where(k => k.Value == n))   // Go though inNo, find value = n, find port
            //        foreach (var b in ins[kv.Key].OrderBy(x => x.m_iBitID)) // find variables based on port 
            //            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(b);

            //    foreach (var kv in outNo.Where(k => k.Value == n))
            //    {
            //        foreach (var b in outs[kv.Key].OrderBy(x => x.m_iBitID))
            //        {
            //            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(b);
            //            //Console.WriteLine(iCount);
            //            //Console.WriteLine(b);
            //        }
            //    }

            //    Add_TabPage("Port " + n, iCount);
            //}

            //2) Auto display for reserved
            var ic = m_pSeqMachineCtrl.m_InterfaceCards;

            for (int n = 1; n <= 8; n++)
            {
                iCount++;
                m_list_TabPage.Add(new TabPage());
                m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());
                var page = m_list_userctrl_manualPage[iCount];

                MyIPort inPort = ic.Get_InputPort(n);
                MyOPort outPort = ic.Get_OutputPort(n);

                for (int bit = 0; bit < 16; bit++)
                {
                    // ---- input bit ----
                    var inDeclared = (inPort != null && ins.ContainsKey(inPort))
                                     ? ins[inPort].Where(x => x.m_iBitID == bit).ToList()
                                     : new List<InputBit>();

                    if (inDeclared.Count > 0)
                        foreach (var b in inDeclared) page.Add_UserCtrlInput(b);
                    else if (inPort != null)
                        page.Add_UserCtrlInput(new InputBit(inPort, bit, "Reserved"));

                    // ---- output bit ----
                    var outDeclared = (outPort != null && outs.ContainsKey(outPort))
                                      ? outs[outPort].Where(x => x.m_iBitID == bit).ToList()
                                      : new List<OutputBit>();

                    if (outDeclared.Count > 0)
                        foreach (var b in outDeclared) page.Add_UserCtrlOutput(b);
                    else if (outPort != null)
                        page.Add_UserCtrlOutput(new OutputBit(outPort, bit, "Reserved"));   
                }
                Add_TabPage("Port " + n, iCount);
            }
                #endregion


                //Add testing----------------------------------------------------------------------------

                //Add OP100------------------------------------------------------------------------------
                iCount++;
            #region OP100
            m_list_TabPage.Add(new TabPage());
            m_list_userctrl_manualPage.Add(new UserCtrl_ManualPage_Template());

            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_senPalletPresent_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPresent_2);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPresent_3);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_senPalletPassThru);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlInput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_inpButton_Start);

            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP100.m_outConv_MoveFWD);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_FwdConv_OP100.m_outConv_MoveREV);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outStopper_1);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Station.m_outStopper_2);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outIndicatorLight_Red);
            m_list_userctrl_manualPage[iCount].Add_UserCtrlOutput(m_pSeqMachineCtrl.m_Module_OP100_Buffer.m_outIndicatorLight_Green);

           

            Add_TabPage("Manual\r\n(OP100)", iCount);
            #endregion

        }

        private void Io_Load(object sender, EventArgs e)
        {

        }

        private void tabctrl_IO_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
