using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;

namespace GT_App
{
	public partial class UserCtrl_ProductionNInstruction_MainDlg : UserControl
	{
		//variables
		UserCtrl_ProductionControlDialog m_UserCtrl_ProductionCtrlDlg;
		UserCtrl_InstructionDialog m_UserCtrl_InstructionDlg;

		TabPage m_tabpage_InstructionDlg= new TabPage();
		TabPage m_tabpage_ProductionDlg = new TabPage();

		//constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public UserCtrl_ProductionNInstruction_MainDlg()
		{
			InitializeComponent();

			m_UserCtrl_ProductionCtrlDlg = new UserCtrl_ProductionControlDialog();
			m_UserCtrl_InstructionDlg = new UserCtrl_InstructionDialog();

			//add into tab control
			m_tabpage_InstructionDlg.Controls.Add(m_UserCtrl_InstructionDlg);
			m_tabpage_InstructionDlg.Text = "Instruction";
			this.tabcontrol_Main.TabPages.Add(m_tabpage_InstructionDlg);

			m_tabpage_ProductionDlg.Controls.Add(m_UserCtrl_ProductionCtrlDlg);
			m_tabpage_ProductionDlg.Text = "Production Control";
			this.tabcontrol_Main.TabPages.Add(m_tabpage_ProductionDlg);

		}

		//Set the tab color and font///////////////////////////////////////////////////////////////////////////////////////////////////// NOT USED
		private void tabcontrol_Main_DrawItem( object sender, DrawItemEventArgs e )
		{
			//StringFormat strFormat = new StringFormat();
			//Graphics g = e.Graphics;
			////Brush br = new SolidBrush(Color.Black);
			//Brush br;
			//Font ft = new System.Drawing.Font(e.Font.FontFamily, e.Font.Size, FontStyle.Bold);

			////Get tab page
			//TabPage tbPage = tabcontrol_Main.TabPages[e.Index];

			////get tab bounds
			//Rectangle rec_tabBounds = tabcontrol_Main.GetTabRect(e.Index);

			//if ( e.State == DrawItemState.Selected )
			//{
			//    br = new SolidBrush(Color.Red);
			//    g.FillRectangle(Brushes.White, e.Bounds);
			//}
			//else
			//{
			//    br = new System.Drawing.SolidBrush(e.ForeColor);
			//    //e.DrawBackground();       //this will paint tab background color white
			//}

			//strFormat.Alignment = StringAlignment.Far;
			//strFormat.LineAlignment = StringAlignment.Center;

			//g.DrawString(tbPage.Text, ft, br, rec_tabBounds, strFormat);
		}

		//Dialog Loaded/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_ProductionNInstruction_MainDlg_Load( object sender, EventArgs e )
		{
			

		}




	}
}
