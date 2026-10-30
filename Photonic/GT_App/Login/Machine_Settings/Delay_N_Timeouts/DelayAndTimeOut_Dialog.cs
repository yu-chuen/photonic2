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
            #region SolderStation
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPartPresent_SolderStat1);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPartPresent_SolderStat2);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPartPresent_SolderStat3);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPassThrough_SolderStat1);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPassThrough_SolderStat2);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_SolderingConv.m_senPassThrough_SolderStat3);

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation01.m_cylCleaning_Dev);	
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation01.m_cylGantry_Rotary	);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation01.m_cylClamp);

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation02.m_cylCleaning_Dev);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation02.m_cylGantry_Rotary);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation02.m_cylClamp);

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation03.m_cylCleaning_Dev);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation03.m_cylGantry_Rotary);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderStation03.m_cylClamp);	

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderingConv.m_cylDocking_SolderStat1);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderingConv.m_cylDocking_SolderStat2);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_SolderingConv.m_cylDocking_SolderStat3);

			addtabpage("Solder Station Station", iCount);
            #endregion

			iCount++;
            //Vision-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Vision
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_VisionConv.m_senPartPresent_VisionStat);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_VisionConv.m_senPassThrough_Station);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_VisionConv.m_senPassThrough_Buffer);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_VisionConv.m_senSlidingDoorClose_IN);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_VisionConv.m_senSlidingDoorClose_OUT);

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_VisionInspection.m_cylVisionLight);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_VisionInspection.m_cylClamp_Wire);
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_VisionConv.m_cylDocking_VisionStat);	
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_VisionConv.m_cylSlidingDoor_IN);			
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_VisionConv.m_cylSlidingDoor_OUT);		

			addtabpage("Vision Inspection", iCount);
            #endregion

			iCount++;
			//Manual Station---------------------------------------------------------------------------------------------------------------------------------------------------
            #region ManualStation
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP100.m_senPartPresent);		
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP100.m_senPassThrough_1);	
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP100.m_senPassThrough_2);

            //m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP100.m_cylConv_Lock);

			addtabpage("ManualStation", iCount);
            #endregion

			iCount++;
			//Hot Blower-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region Hotblower
			m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPartPresent_HotBlower);	
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPassThrough_HotBlower);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_HotBlower.m_senTopCoverPresent);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_HotBlower.m_senBlowerInsert1);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_HotBlower.m_senBlowerInsert2);
            
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senSlidingDoor_CloseIN);
			m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senSlidingDoor_CloseOUT);

			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110n120Conv.m_cylDocking_HotBlower);	
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110n120Conv.m_cylSlidingDoor_IN);			
			m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110n120Conv.m_cylSlidingDoor_OUT);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_HotBlower.m_cylBlower_DnUp);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_HotBlower.m_cylBiasorPusher_InOut);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_HotBlower.m_cylBiasor_DnUp);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_HotBlower.m_cylReflector_InOut);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_HotBlower.m_cylReflector_UpDown);

			addtabpage("Hot Blower", iCount);
            #endregion

            iCount++;
            //OP120-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP120
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());

            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPartPresent_OP120);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPassThrough_OP120);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP120.m_senGripper_PartPresent);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP120.m_senSlideTable_PartPresent);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP120.m_senGripper_InPos);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP120.m_senSafetyCurtain);

            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110n120Conv.m_cylDocking_HotBlower);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP120.m_cylNest_UpDn);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP120.m_cylGripper_InOut);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP120.m_cylGripper_UpDn);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP120.m_cylSlideTable_GripUngrip);
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_ManualStatOP120.m_cylGripper_GripUngrip);

            addtabpage("OP 120", iCount);
            #endregion

            iCount++;
            //OP125-------------------------------------------------------------------------------------------------------------------------------------------------------------------
            #region OP125
            m_ll_Module_Interface.Add(new UserCtrl_DelayNTimeoutsInterface_Dlg());
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPartPresent_OP125);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_OP110n120Conv.m_senPassThrough_OP125);
            m_ll_Module_Interface[iCount].Add_Sensor(m_SeqMachineControl.m_Module_ManualStatOP125.m_senTopEnclosure_PartPresent);
            
            m_ll_Module_Interface[iCount].Add_Cylinder(m_SeqMachineControl.m_Module_OP110n120Conv.m_cylDocking_OP125);
            addtabpage("OP125", iCount);
            #endregion

		}

        //Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        private void DelayAndTimeOut_Dialog_Load(object sender, EventArgs e)
        {
            Add_UserCtrl();
        }
    }
}
