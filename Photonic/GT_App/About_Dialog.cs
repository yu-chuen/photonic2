using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using StaticCoreLib;
using System.Reflection;

namespace GT_App
{
    public partial class About_Dialog : Form
    {
        public About_Dialog()
        {
            InitializeComponent();

            GT_App_MainApp pApp = GT_App.Program.m_MainApp;

			//Set App Version display
			//Note: To display the correct build date, require to comment out [assembly: AssemblyVersion("1.0.0.0")] and uncomment [assembly: AssemblyVersion("1.0.*")] @ AssembyInfo.cs 
            var version = Assembly.GetExecutingAssembly().GetName().Version;
            var builddate = new DateTime(2000, 1, 1).Add(new TimeSpan(TimeSpan.TicksPerDay * version.Build + TimeSpan.TicksPerSecond * 2 * version.Revision));
            //this.lbl_Data.Text = "Version " + pApp.m_SeqMachineCtrl.m_MachineInfo.Get_FullVersionName(/*builddate.ToString("dd-MMM-yyyy")*/);
            this.lbl_Version.Text = pApp.m_SeqMachineCtrl.m_VersionInfo.GetVersion();

            this.lbl_Title.Text = pApp.m_SeqMachineCtrl.m_MachineInfo.m_strMachineName;
        }

        private void btn_OK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
