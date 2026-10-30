using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using StaticCoreLib;

namespace GT_App
{
    public partial class SplashScreen : Form
    {
        public SplashScreen()
        {
            InitializeComponent();
        }

        private void SplashScreen_Load(object sender, EventArgs e)
        {
            timer_LoadingBar.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            pnl_LoadingBar.Left += 2;

            if (pnl_LoadingBar.Left > 302)
            {
                pnl_LoadingBar.Left = -40;
            }
        }

        private void SplashScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer_LoadingBar.Stop();
        }
    }
}
