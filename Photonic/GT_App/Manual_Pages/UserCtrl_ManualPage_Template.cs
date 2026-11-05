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
    
    public partial class UserCtrl_ManualPage_Template : UserControl
    {

        private const int HEADER_HEIGHT = 30;
        public bool m_bShowHeaders = false;

        public bool m_bPairRows = false;
       
        public static bool UserTestingStyle = true;
        //variables
        SeqMachineControl m_SeqMachineControl = null;

        List<UserCtrl_UI_InputBit> m_list_userctrl_UI_Inputbit; 

        List<UserCtrl_UI_Testing> m_list_userctrl_UI_Outputbit; //
        //List<UserCtrl_UI_OutputBit> m_list_userctrl_UI_Outputbit;

        List<UserCtrl_UI_Cylinder> m_list_userctrl_UI_Cylinder;
        List<UserCtrl_UI_Dual_OutputBit> m_list_userctrl_UI_Dual_Output;
        List<UserCtrl_RobotStation_Goto> m_list_userctrl_UI_RobotGoto;

        //custom templates
        UserCtrl_StationDisplay m_userctrl_StationDisp;
        UserCtrl_Custom_GotoSolderPoint m_userctrl_GotoCustom;
        UserCtrl_Custom_HotBlower m_userctrl_HotBlower;
        UserCtrl_Custom_OP85_VisionTest m_userctrl_OP85;
        UserCtrl_Custom_OP90_VisionTest m_userctrl_OP90;

        private void Add_Headers()
        {
            int iInW = (m_list_userctrl_UI_Inputbit.Count > 0) ? m_list_userctrl_UI_Inputbit[0].Width : 528;
            int iInH = (m_list_userctrl_UI_Inputbit.Count > 0) ? m_list_userctrl_UI_Inputbit[0].Height : 36;
            int iOutW = (m_list_userctrl_UI_Outputbit.Count > 0) ? m_list_userctrl_UI_Outputbit[0].Width : 456;
            int iOutH = (m_list_userctrl_UI_Outputbit.Count > 0) ? m_list_userctrl_UI_Outputbit[0].Height : 56;

            int iColHeight = Math.Max(m_list_userctrl_UI_Inputbit.Count * iInH,
                                      m_list_userctrl_UI_Outputbit.Count * iOutH);

            UserCtrl_PortHeader hdr = new UserCtrl_PortHeader();
            hdr.Set_Sizes(iInW, iOutW, HEADER_HEIGHT + iColHeight);
            hdr.Location = new Point(0, 0);
            this.Controls.Add(hdr);
            hdr.SendToBack();            // keep the rows in front of it
        }

        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public UserCtrl_ManualPage_Template()
        {
            InitializeComponent();
            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;

            m_list_userctrl_UI_Inputbit = new List<UserCtrl_UI_InputBit>();

            m_list_userctrl_UI_Outputbit = new List<UserCtrl_UI_Testing>(); //
            //m_list_userctrl_UI_Outputbit = new List<UserCtrl_UI_OutputBit>();

            m_list_userctrl_UI_Cylinder = new List<UserCtrl_UI_Cylinder>();
            m_list_userctrl_UI_RobotGoto = new List<UserCtrl_RobotStation_Goto>();
            m_list_userctrl_UI_Dual_Output = new List<UserCtrl_UI_Dual_OutputBit>();
        }

        //Update UI///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //note: Loop at main manual page window
        public void Update_UI()
        {
            //update for input bit / sensor UI
            for (int i = 0; i < m_list_userctrl_UI_Inputbit.Count; i++)
            {
                m_list_userctrl_UI_Inputbit[i].Update_PicBox();
            }

            //Update for output bit 
            //foreach (UserCtrl_UI_OutputBit mUserCtrl in m_list_userctrl_UI_Outputbit)
            //{
            //    mUserCtrl.Get_OrderEndString();

            //}

            foreach (UserCtrl_UI_Testing mUserCtrl in m_list_userctrl_UI_Outputbit) //
            {
                mUserCtrl.Get_OrderEndString();
            }


            //update for boolean type button
            foreach (UserCtrl_UI_Dual_OutputBit userCtrl in m_list_userctrl_UI_Dual_Output)
            {
                userCtrl.Get_Status();
            }

            ////Update for cylinder
            //foreach (UserCtrl_UI_Cylinder mUserCtrl in m_list_userctrl_UI_Cylinder)
            //    mUserCtrl.Get_OrderEndString();
        }

        //Dialog loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void UserCtrl_ManualPage_Template_Load(object sender, EventArgs e)
        {
            Point pPoint = new Point(0, 0);
            int iTotalItem = 0;

            int iTop = (m_bShowHeaders == true) ? HEADER_HEIGHT : 0;


            //Add Input bit-----------------------------------------------------------------------------------------			
            iTotalItem = m_list_userctrl_UI_Inputbit.Count;
            pPoint.X = 0;
            pPoint.Y = 0;
            
            for (int iCount = 0; iCount < iTotalItem; iCount++)
            {
                if (iCount == 25)
                {
                    pPoint.X = m_list_userctrl_UI_Inputbit[0].Width + 50;
                    pPoint.Y = 0;
                }

                if (iCount == 50)
                {
                    pPoint.X = (m_list_userctrl_UI_Inputbit[0].Width * 2) + 100;
                    pPoint.Y = 0;
                }
                //Set user control position
                m_list_userctrl_UI_Inputbit[iCount].Location = pPoint;
                this.Controls.Add(m_list_userctrl_UI_Inputbit[iCount]);

                //Set point for user control position (next user control)
                pPoint.Y += m_list_userctrl_UI_Inputbit[iCount].Height;
            }

            //Add Output Bit--------------------------------------------------------------------------------------
            //Set Output bit position
            iTotalItem = m_list_userctrl_UI_Outputbit.Count;
            pPoint.X = m_list_userctrl_UI_Inputbit[0].Width + 50;
            pPoint.Y = 0;
            if (iTotalItem > 0)
            {
                for (int iCount = 0; iCount < iTotalItem; iCount++)
                {
                    //set user control position
                    m_list_userctrl_UI_Outputbit[iCount].Location = pPoint;
                    this.Controls.Add(m_list_userctrl_UI_Outputbit[iCount]);

                    //Set point for user control position (next user control)
                    pPoint.Y += m_list_userctrl_UI_Outputbit[iCount].Height;
                }
            }
            foreach (UserCtrl_UI_Dual_OutputBit userCtrl in m_list_userctrl_UI_Dual_Output)
            {
                //set user control position
                userCtrl.Location = pPoint;
                this.Controls.Add(userCtrl);

                //Set point for user control position (next user control)
                pPoint.Y += userCtrl.Height;
            }
            //Space for cylinders
            pPoint.Y += 40;

            //Add Cylinder-------------------------------------------------------------------------------------
            iTotalItem = m_list_userctrl_UI_Cylinder.Count;
            if (iTotalItem > 0)
            {
                for (int iCount = 0; iCount < iTotalItem; iCount++)
                {
                    //set user control position
                    m_list_userctrl_UI_Cylinder[iCount].Location = pPoint;
                    this.Controls.Add(m_list_userctrl_UI_Cylinder[iCount]);

                    //Set point for user control position (next user control)
                    pPoint.Y += m_list_userctrl_UI_Cylinder[iCount].Height;
                }
            }

            //Add robot Station----------------------------------------------------------------------------------
            iTotalItem = m_list_userctrl_UI_RobotGoto.Count;
            pPoint.X = m_list_userctrl_UI_Inputbit[0].Size.Width + 50; // + m_list_userctrl_UI_Outputbit[0].Size.Width + 50;
            pPoint.Y = 0;

            for (int iCount = 0; iCount < iTotalItem; iCount++)
            {
                //set user control position
                m_list_userctrl_UI_RobotGoto[iCount].Location = pPoint;
                this.Controls.Add(m_list_userctrl_UI_RobotGoto[iCount]);

                //Set point for user control position (next user control)
                pPoint.Y += m_list_userctrl_UI_RobotGoto[iCount].Height;
            }

            if (m_bShowHeaders == true) Add_Headers();
        }

        //Add user control: Input Bit/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrlInput(InputBit mInputBit)
        {
            m_list_userctrl_UI_Inputbit.Add(new UserCtrl_UI_InputBit());
            m_list_userctrl_UI_Inputbit[(m_list_userctrl_UI_Inputbit.Count - 1)].Set_InputBitPtr(mInputBit);
        }

        //Add user control: Output Bit////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrlOutput(OutputBit mOutputBit)
        {

            m_list_userctrl_UI_Outputbit.Add(new UserCtrl_UI_Testing());
            m_list_userctrl_UI_Outputbit[(m_list_userctrl_UI_Outputbit.Count - 1)].Set_OutputBitPtr(mOutputBit);

            //m_list_userctrl_UI_Outputbit.Add(new UserCtrl_UI_OutputBit());
            //m_list_userctrl_UI_Outputbit[(m_list_userctrl_UI_Outputbit.Count - 1)].Set_OutputBitPtr(mOutputBit);
        }

        //Add user control: Boolean////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrlDualOutput(OutputBit mOutput1, OutputBit mOutput2, List<bool> mState, Action<bool> act)
        {
            m_list_userctrl_UI_Dual_Output.Add(new UserCtrl_UI_Dual_OutputBit(mOutput1, mOutput2, mState, act));
        }

        //Add user control: Cyclinder///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //public void Add_UserCtrlCylinder( Cylinder mExtCyl, Cylinder mRecCyl, String strOp_Ext = "Ext", String strOp_Ret = "Ret" )
        //{
        //	m_list_userctrl_UI_Cylinder.Add(new UserCtrl_UI_Cylinder());
        //	m_list_userctrl_UI_Cylinder[ ( m_list_userctrl_UI_Cylinder.Count - 1 ) ].Set_CylinderPtr(mExtCyl, mRecCyl, strOp_Ext, strOp_Ret);
        //}

        public void Add_UserCtrlCylinder(Cylinder mCyl, String strOp_Ext = "Ext", String strOp_Ret = "Ret")
        {
            //add cylinder
            m_list_userctrl_UI_Cylinder.Add(new UserCtrl_UI_Cylinder());
            m_list_userctrl_UI_Cylinder[(m_list_userctrl_UI_Cylinder.Count - 1)].Set_CylinderPtr(mCyl, strOp_Ext, strOp_Ret);

            //add ext & ret sensor
            Add_UserCtrlInput(mCyl.Get_Work_Sensor());
            Add_UserCtrlInput(mCyl.Get_Home_Sensor());
        }

        //Add user control: RobotStation///////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Adding robot station goto function which is called by the ViewModel_GotoFunctions class. 
        /// </summary>
        /// <param name="strPosName">String position</param>
        /// <param name="strPosDesc">String position description</param>
        /// <param name="iTypeID">robot number (based on the goto function at ViewModel_GotoFunctions class)</param>
        public void Add_UserCtrlRobotGoto(String strPosName, String strPosDesc, int iTypeID = 0)
        {
            m_list_userctrl_UI_RobotGoto.Add(new UserCtrl_RobotStation_Goto(strPosName, strPosDesc, iTypeID));
        }

        //Add Custom control: ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrlCustom(int iSelect, String strParam = "")
        {
            Point pPoint = new Point();
            int iHeight = 0, iWidth = 0;

            iWidth = m_list_userctrl_UI_Inputbit[0].Location.X + 20;
            iHeight = m_list_userctrl_UI_Inputbit[0].Size.Height;
            
            //get coordinates
            pPoint.X = iWidth;
            pPoint.Y = (iHeight * m_list_userctrl_UI_Inputbit.Count) + 50;

            //set m_userctrl_GotoCustom--------------------------------------------------------------------------------------
            m_userctrl_GotoCustom = new UserCtrl_Custom_GotoSolderPoint(iSelect);
            m_userctrl_GotoCustom.Location = pPoint;
            this.Controls.Add(m_userctrl_GotoCustom);

            //set UserCtrl_StationDisplay----------------------------------------------------------------------------------------
            pPoint.Y += m_userctrl_GotoCustom.Size.Height + 10;
            m_userctrl_StationDisp = new UserCtrl_StationDisplay(strParam);
            m_userctrl_StationDisp.Location = pPoint;

            switch (iSelect)
            {
                case 1: m_userctrl_StationDisp.Set_LabelColor(Color.FromArgb(128, 255, 255)); break;
                case 2: m_userctrl_StationDisp.Set_LabelColor(Color.FromArgb(255, 128, 255)); break;
                case 3: m_userctrl_StationDisp.Set_LabelColor(Color.FromArgb(255, 255, 128)); break;
            }

            this.Controls.Add(m_userctrl_StationDisp);
        }

        //Add Custom control: /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrl_OP90()
        {
            Point pPoint = new Point();
            int iHeight = 0, iWidth = 0;

            iWidth = m_list_userctrl_UI_Inputbit[0].Size.Width + 50 + m_list_userctrl_UI_Outputbit[0].Size.Width + 50;
            iHeight = m_list_userctrl_UI_Inputbit[0].Location.Y;

            //get coordinates
            pPoint.X = iWidth;
            //pPoint.Y = (iHeight * m_list_userctrl_UI_Outputbit.Count) + (iHeight * m_list_userctrl_UI_Cylinder.Count) + 50;
            pPoint.Y = iHeight;

            //set m_userctrl_GotoCustom--------------------------------------------------------------------------------------
            m_userctrl_OP90 = new UserCtrl_Custom_OP90_VisionTest();
            m_userctrl_OP90.Location = pPoint;
            this.Controls.Add(m_userctrl_OP90);
        }

        //Add Custom control: ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrlHotBlower()
        {
            Point pPoint = new Point();
            int iHeight = 0, iWidth = 0;

            iWidth = m_list_userctrl_UI_Inputbit[0].Size.Width + 50 + m_list_userctrl_UI_Outputbit[0].Size.Width + 50;
            iHeight = m_list_userctrl_UI_Inputbit[0].Location.Y;

            //get coordinates
            pPoint.X = iWidth;
            //pPoint.Y = (iHeight * m_list_userctrl_UI_Outputbit.Count) + (iHeight * m_list_userctrl_UI_Cylinder.Count) + 50;
            pPoint.Y = iHeight;

            //set m_userctrl_GotoCustom--------------------------------------------------------------------------------------
            m_userctrl_HotBlower = new UserCtrl_Custom_HotBlower();
            m_userctrl_HotBlower.Location = pPoint;
            this.Controls.Add(m_userctrl_HotBlower);
        }

        //Add Custom control: ///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_UserCtrl_OP85()
        {
            Point pPoint = new Point();
            int iHeight = 0, iWidth = 0;

            iWidth = m_list_userctrl_UI_Inputbit[0].Size.Width + 50 + m_list_userctrl_UI_Outputbit[0].Size.Width + 50;
            iHeight = m_list_userctrl_UI_Inputbit[0].Location.Y;

            //get coordinates
            pPoint.X = iWidth;
            //pPoint.Y = (iHeight * m_list_userctrl_UI_Outputbit.Count) + (iHeight * m_list_userctrl_UI_Cylinder.Count) + 50;
            pPoint.Y = iHeight;

            //set m_userctrl--------------------------------------------------------------------------------------
            m_userctrl_OP85 = new UserCtrl_Custom_OP85_VisionTest();
            m_userctrl_OP85.Location = pPoint;
            this.Controls.Add(m_userctrl_OP85);
        }
    }
}