////////////////////////////////////////
/* Comment = Disable */
/* Uncomment = Enable */

#define BETA_MODE

////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class VersionInfo
    {
        //constructor
        public VersionInfo()
        {
        }

        /// <summary>
        /// Get current application's version number.
        /// </summary>
        /// <returns></returns>
        public string GetVersion()
        {
            string strVersion = "Version ";

            string strReleaseVersion = "0.0.0.0"; //increment only
            string strReleaseDate = "2023-08-01"; //yyyy-MM-dd

            string strBetaVersion = "0"; //increment only
            string strBetaDate = "2023-08-01"; //yyyy-MM-dd

#if BETA_MODE
            strVersion += strReleaseVersion + "_beta" + strBetaVersion + " (" + DateTime.Parse(strBetaDate).ToString("d MMMM yyyy") + ")";
#else
            strVersion += strReleaseVersion + " (" + DateTime.Parse(strReleaseDate).ToString("d MMMM yyyy") + ")";
#endif

            if (Global_Var.m_bOfflineTest == true) { strVersion += " OFFLINE MODE"; }
            
            return strVersion;
        }
    }
}