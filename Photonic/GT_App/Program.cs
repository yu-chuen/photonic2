using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace GT_App
{
	static class Program
	{
		public static Main_Dlg m_MainDialog = null;
        public static GT_App_MainApp m_MainApp = null;
        public static SplashScreen m_SplashScreen = null;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);

            //show splash screen form first to show that the app is loading - david
            #region splash screen
            m_SplashScreen = new SplashScreen();
            var splashThread = new Thread(new ThreadStart(
                () => Application.Run(m_SplashScreen)));
            splashThread.SetApartmentState(ApartmentState.STA);
            splashThread.Start();
            #endregion

			//init lib
			m_MainApp = new GT_App_MainApp();
			if (m_MainApp.Init() == false)
            {
                MainForm_LoadCompleted(); //run this function if init failed
                return;
            }

			//must place here if not, app will crash due to null reference
            m_MainDialog = new Main_Dlg();
            m_MainDialog.Load += MainForm_LoadCompleted; //add one more function to the OnLoad function of main dialog - david 
			Application.Run(m_MainDialog);

			//Application.Run(new Main_Dlg());
		}

        private static void MainForm_LoadCompleted()
        {
            throw new NotImplementedException();
        }

        //run this function to dispose the splash screen form after main dialog loaded to release resources
        private static void MainForm_LoadCompleted(object sender, EventArgs e)
        {
            if (m_SplashScreen != null && !m_SplashScreen.Disposing && !m_SplashScreen.IsDisposed)
            {
                m_SplashScreen.Invoke(new Action(() => m_SplashScreen.Close()));
            }
            m_MainDialog.TopMost = true;
            m_MainDialog.Activate();
            m_MainDialog.TopMost = false;
        }
	}
}
