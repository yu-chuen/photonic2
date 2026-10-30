using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

/**********************************************************************************************************************************
 * Notes: 
 *              1. Add new UserCtrl_DelayNTimeoutsCylinderStrip_Dlg control is done here. Add this based on the cylinders present at modules
 *              2. Create new function to add more cylinders and other modules.
 **********************************************************************************************************************************/

namespace GT_App
{
    public partial class DelayAndTimeOut_Dialog : Form
    {
        //variables////////////////////////////////////////////////////////////////////////////////////////////////////////////
        SeqMachineControl m_SeqMachineControl = null;

        List<UserCtrl_DelayNTimeoutsInterface_Dlg> m_ll_Module_Interface = new List<UserCtrl_DelayNTimeoutsInterface_Dlg>();
        List<TabPage> m_ll_Tabpage = new List<TabPage>();


        //constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////
        public DelayAndTimeOut_Dialog()
        {
            InitializeComponent();

            m_SeqMachineControl = GT_App.Program.m_MainApp.m_SeqMachineCtrl;
        }

        //draw the tab controls color and layout//////////////////////////////////////////////////////////////////////////////////
        private void tabCtrl_DelayNTimeouts_DrawItem(object sender, DrawItemEventArgs e)
        {
            StringFormat strFormat = new StringFormat();
            Graphics g = e.Graphics;
            //Brush br = new SolidBrush(Color.Black);
            Brush br;
            Font ft = new System.Drawing.Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

            //Get tab page
            TabPage tbPage = tabCtrl_DelayNTimeouts.TabPages[e.Index];

            //get tab bounds
            Rectangle rec_tabBounds = tabCtrl_DelayNTimeouts.GetTabRect(e.Index);

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

        //btn close///////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void btn_Close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

		//add tab page//////////////////////////////////////////////////////////////////////////////////////////////////////
		private void addtabpage( String module_name, int iCount )
		{
			m_ll_Tabpage.Add(new TabPage());
			m_ll_Tabpage[iCount].Controls.Add(m_ll_Module_Interface[iCount]);
			m_ll_Tabpage[iCount].Text = module_name;
			m_ll_Tabpage[iCount].AutoScroll = true;
			this.tabCtrl_DelayNTimeouts.TabPages.Add(m_ll_Tabpage[iCount]);
		}

        //Add tab control////////////////////////////////////////////////////////////////////////////////////////////////////
        private void Add_UserCtrl()
        {
            int iCount = 0;

			//Solder Station---------------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP60 - SolderStation1
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());


			addtabpage("OP60\r\nSolder 1", iCount);
            #endregion

            iCount++;
            #region OP70 - SolderStation2
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());


            addtabpage("OP70\r\nSolder 2", iCount);
            #endregion

            iCount++;
            #region OP80 - SolderStation3
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());


            addtabpage("OP80\r\nSolder 3", iCount);
            #endregion

			iCount++;
            //Vision-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP90 - Vision
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());
	

			addtabpage("OP90\r\nVision", iCount);
            #endregion

			iCount++;
			//OP100 - Bridge---------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP100 - Bridge
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP100_Buffer.m_senPalletPresent_1);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP100_Station.m_senPalletPresent_2);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP100_Station.m_senPalletPresent_3);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP100_Station.m_senPalletPassThru);

            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP100_Buffer.m_cylBridge_LockUnlock);

            addtabpage("OP100\r\nBridge", iCount);
            #endregion

			iCount++;
			//Hot Blower-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP110 - Hotblower
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylHotBlower_DnUp);
            //m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylBiasorPusher_ExtRet);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylBiasor_DnUp);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylReflector_ExtRet);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylReflector_UpDown);

            if (m_SeqMachineControl.m_DataUnit.MachineTypeNum_i == 1)
            {
                m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylCombPNP_AxisX_ExtRet);
                m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylCombPNP_AxisZ_DownUp);
                m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110_HotBlower.m_cylCombGripper_GripUngrip);
            }

			addtabpage("OP110\r\nHot Blower", iCount);
            #endregion

		}

        //Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DelayAndTimeOut_Dialog_Load(object sender, EventArgs e)
        {
            Add_UserCtrl();
        }
    }
}