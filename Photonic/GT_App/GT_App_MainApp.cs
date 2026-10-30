using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using StaticCoreLib;
using System.Windows.Forms;
using System.Diagnostics;

namespace GT_App
{
	public class GT_App_MainApp
	{
		//variables
		public SeqMachineControl m_SeqMachineCtrl;

		//constructor//////////////////////////////////////////////////////////////////////////////////////////
		public GT_App_MainApp() { }

		//Init classes///////////////////////////////////////////////////////////////
		//This is called at Program.cs
		public bool Init()
		{
			//check if this app is running
			String strProcess = Process.GetCurrentProcess().ProcessName;
			if (Process.GetProcesses().Count(p => p.ProcessName == strProcess) > 1)
			{
                MessageBox.Show(new Form() { TopMost = true }, "Multiple apps detected.");
				return false;
			}

			//init seqmachinecontrol
			m_SeqMachineCtrl = new SeqMachineControl();
			if (m_SeqMachineCtrl.Start_Up() == false) return false;

			return true;
		}
	}
}
