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
	public partial class UserCtrl_Indexer_SettingPage : UserControl
	{
		//variables
		Stepper_Settings m_Settings = null;

		//Constructor/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public UserCtrl_Indexer_SettingPage()
		{
			InitializeComponent();
		}

		//Dialog Loaded///////////////////////////////////////////////////////////////////////////////////////////////////////////////
		private void UserCtrl_Indexer_SettingPage_Load( object sender, EventArgs e )
		{
			this.Update_UI();
		}

		//Set indexer pointer//////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_SettingsPtr( Stepper_Settings mSettings ) { m_Settings = mSettings; }
		//Set Label////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_Label( String strLabel ) { lbl_Label.Text = strLabel; }

		//update UI//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Update_UI()
		{
			txt_HomeDir.Text		        = m_Settings.m_iHomeDir.ToString();
			txt_HomingAcceleration.Text       = m_Settings.m_iHomeAcceleration.ToString();
            txt_HomingMaxVelocity.Text	    = m_Settings.m_iHomeVelocity.ToString();
            txt_HomingVelocity.Text	        = m_Settings.m_iHomeVO.ToString();
			txt_HomingTimeOut.Text	        = m_Settings.m_iTimeOutHome.ToString();
			txt_mmPerRev.Text		        = m_Settings.m_dbmmPerRevolution.ToString();
			txt_PulsePerRev.Text	        = m_Settings.m_dbPulsePerRevolution.ToString();
			txt_TimeOut.Text		        = m_Settings.m_iTimeOut.ToString();
			txt_Velocity.Text		        = m_Settings.m_iVelocity.ToString();
			txtb_Acc.Text			        = m_Settings.m_iAcceleration.ToString();
			txtb_Dec.Text			        = m_Settings.m_iDeceleration.ToString();

			trackBar_PrimarySpeed.Value     = m_Settings.m_iMainSpeed_Multiplier;
			lbl_trackbarMainSpeed.Text      = "Global Speed Modifier ( " + trackBar_PrimarySpeed.Value.ToString() + " %)";
		}

		//get data from UI and update registry/////////////////////////////////////////////////////////////////////////////
		public Stepper_Settings Get_Settings()
		{
			m_Settings.m_iHomeDir				= int.Parse(txt_HomeDir.Text);
            m_Settings.m_iHomeVelocity			= int.Parse(txt_HomingMaxVelocity.Text);
            m_Settings.m_iHomeVO			    = int.Parse(txt_HomingVelocity.Text);
            m_Settings.m_iHomeAcceleration		= int.Parse(txt_HomingAcceleration.Text);
            m_Settings.m_iVelocity				= int.Parse(txt_Velocity.Text);
            m_Settings.m_dbmmPerRevolution	    = double.Parse(txt_mmPerRev.Text);
            m_Settings.m_dbPulsePerRevolution	= double.Parse(txt_PulsePerRev.Text);
            m_Settings.m_iTimeOut				= int.Parse(txt_TimeOut.Text);
            m_Settings.m_iTimeOutHome			= int.Parse(txt_HomingTimeOut.Text);
			m_Settings.m_iAcceleration			= int.Parse(txtb_Acc.Text);
			m_Settings.m_iDeceleration			= int.Parse(txtb_Dec.Text);

			m_Settings.m_iMainSpeed_Multiplier  = trackBar_PrimarySpeed.Value;
			
			return m_Settings;
		}



		//Set Unit Measurement//////////////////////////////////////////////////////////////////////////////////////////////////
		/// <summary>
		/// Set the measurement label text as mm or deg
		/// </summary>
		/// <param name="iSelect">1 = mm, 2 = deg</param>
		public void Set_UnitMeasurement( int iSelect )
		{
			if ( iSelect == 2 )
			{
				lbl_Unit001.Text = "deg per Revolution";
			}
		}

		private void trackBar_PrimarySpeed_Scroll( object sender, EventArgs e )
		{
			lbl_trackbarMainSpeed.Text = "Global Speed Modifier ( " + trackBar_PrimarySpeed.Value.ToString() + " %)";
		}



	}
}
