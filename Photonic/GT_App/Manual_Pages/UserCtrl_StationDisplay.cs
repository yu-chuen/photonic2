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
	public partial class UserCtrl_StationDisplay : UserControl
	{
		//variables


		//Constructor//////////////////////////////////////////////////////////////////////////////////////
		public UserCtrl_StationDisplay(String strLabel)
		{
			InitializeComponent();
			lbl_StationDisplay.Text = strLabel;
		}

		//Dialog Loaded////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_StationDisplay_Load( object sender, EventArgs e )
		{

		}

		//Set Color////////////////////////////////////////////////////////////////////////////////////////
		public void Set_LabelColor( Color mColor )
		{
			lbl_StationDisplay.BackColor = mColor;
		}



	}
}
