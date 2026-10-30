/****************************************************************************************************************************************
 * MachineInfo class
 * Desc:    This class is design for machine setting data storage and not package setting. All global variables that is used by the machine
 *               that needs to be stored / remembered (ex. enable module flags, IDs, IPs and etc) should be written here.
 *               
 * NOTE:   Deserializer do not allow the re-usable method because the parameters required are based on the class.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Reflection;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace StaticCoreLib
{
    [Serializable]
    public class MachineInfo
    {
        //variables
		//not set able variables
        [System.Xml.Serialization.XmlIgnore] public String m_strFileName = "Machine_Settings.xml";				//File Name

		//machine file paths and naming
        public String	m_strMachineID			= "001";
        public String	m_strMachineName		= "GT App";
        public String	m_strPackageFilePath	= @"C:\GT_App\";
        public String	m_strFilePath			= @"C:\GT_App\";
        public String	m_strPackageFileName	= "Settings_001";
        public String	m_strPackageFileExt		= ".pkg";
		public bool		m_bOfflineTest			= true;
		public int		m_iMachineNumber		= 1;
		public int		m_iMachineTypeNum		= 1;

        //counters
        public int m_iPartCount = 0;
        public int m_iPartCount_Pass = 0;
        public int m_iPartCount_Fail = 0;

		//log level
		public int LogLevel_int = 2;

		//Solder Station
		//enable / disable solder station memory
		public short m_shFlag_SolderStation1_En = 0;
		public short m_shFlag_SolderStation2_En = 0;
		public short m_shFlag_SolderStation3_En = 0;

        //Station Names: For Server Maintenance dialog use
        public String m_strOP40 = "OP40";
        public String m_strOP45 = "OP45";
        public String m_strOP47 = "OP47";
        public String m_strOP50 = "OP50";
        public String m_strOP60 = "OP60";
        public String m_strOP70 = "OP70";
        public String m_strOP80 = "OP80";
        public String m_strOP85 = "OP85";
        public String m_strOP90 = "OP90";
        public String m_strOP100 = "OP100";
        public String m_strOP110 = "OP110";
        public String m_strOP115 = "OP115";
        public String m_strOP120 = "OP120";
        public String m_strOP125 = "OP125";
        public String m_strOP130 = "OP130";
        public String m_strOP135 = "OP135";
        public String m_strOP137 = "OP137";
        public String m_strOP140 = "OP140";
        public String m_strOP150 = "OP150";
        public String m_strOP160 = "OP160";

		//SQL Tables for line pc
		public String m_strDbTable_Line = "Pkg_LinePC2";
		public String m_strDbTable_CurPkg = "CurrentPkg";
		public String m_strDbSQL_SignalZone = "ZoneUpdated_2";
		public String m_strDbSQL_CurPkgFile = "";
		public String m_strDbSQL_CurPkgUpdated = "0";

		//Cleaning offset
		public double m_dbCleaningOffset_Up = 0;
		public double m_dbCleaningOffset_Dwn = 0;

		//timer: time out
		public long m_lg_Solder_Sleep_Timeout = 100000;
		public long m_lg_FumeOffTime_Max = 3000000;
		public long m_lg_Soldering_Start_Timeout = 2000;			//solder station: Start solder time out
		public long m_lg_Soldering_Run_Timeout = 7000;				//solder station: Run time out
		public long m_lg_Solder_Cleaning_Time = 2000;				//cleaning station: Run Time out

        public int m_int_HotBlower_NextPalletTimeout = 20000; //20 seconds
        public int m_int_HotBlower_CheckTempTimeout = 3600000; //1 hour

		//Soldering station: Counters
		public int m_iIronHead_UseCount = 10000;					//Head solder use counter. Throw warning to change solder head when count exceeded.
		public int m_iCleanCount = 6;											//Start to clean the solder head when this count exceeded.

		//Module Enables
		public bool m_bEnableMod_SolderStation1 = true;
		public bool m_bEnableMod_SolderStation2 = true;
		public bool m_bEnableMod_SolderStation3 = true;
        public bool m_bEnableMod_ConvSolderStation = true;
        public bool m_bEnable_Module_OP85 = true;
        public bool m_bEnable_Module_Conv_OP85 = true;
        public bool m_bEnable_Module_OP90 = true;
        public bool m_bEnable_Module_Conv_OP90 = true;
        public bool m_bEnable_Module_FwdConv_OP85_OP90 = true;
		public bool m_bEnableMod_ManualOP100 = true;
        public bool m_bEnable_Module_OP110 = true;
        public bool m_bEnable_Module_Conv_OP110 = true;
        public bool m_bEnable_Module_FwdConv_OP110 = true;

        //Soldering station: Reverse sequence
        public bool m_bFlagEnable_ReverseSeq = true;

       // Soldering station: Cleaning process
        public bool m_bFlagEnable_CleanSolderHead = true;

        //Enable or Disable - Solder Point
        public bool m_bEnable_SolderPoint1 = true;
        public bool m_bEnable_SolderPoint2 = true;
        public bool m_bEnable_SolderPoint3 = true;
        public bool m_bEnable_SolderPoint4 = true;
        public bool m_bEnable_SolderPoint5 = true;
        public bool m_bEnable_SolderPoint6 = true;

        //Enable or Disable - HTR or RTD Grades
        public bool m_bEnable_HTR_Grade_A_LV = true;
        public bool m_bEnable_HTR_Grade_B_LV = true;
        public bool m_bEnable_HTR_Grade_C_LV = true;
        public bool m_bEnable_HTR_Grade_D_LV = true;
        public bool m_bEnable_HTR_Grade_E_LV = true;

        public bool m_bEnable_RTD_Grade_A_LV = true;
        public bool m_bEnable_RTD_Grade_B_LV = true;
        public bool m_bEnable_RTD_Grade_C_LV = true;
        public bool m_bEnable_RTD_Grade_D_LV = true;
        public bool m_bEnable_RTD_Grade_E_LV = true;

        public bool m_bEnable_HTR_Grade_A_HV = true;
        public bool m_bEnable_HTR_Grade_B_HV = true;
        public bool m_bEnable_HTR_Grade_C_HV = true;
        public bool m_bEnable_HTR_Grade_D_HV = true;
        public bool m_bEnable_HTR_Grade_E_HV = true;

        public bool m_bEnable_RTD_Grade_A_HV = true;
        public bool m_bEnable_RTD_Grade_B_HV = true;
        public bool m_bEnable_RTD_Grade_C_HV = true;
        public bool m_bEnable_RTD_Grade_D_HV = true;
        public bool m_bEnable_RTD_Grade_E_HV = true;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructors
        public MachineInfo()
        {
        }

        //Set File Path and File name//////////////////////////////////////////////////////////////////////////////////////
        public void Set_FilePathAndName(String strFilePath, String strFileName) { m_strFilePath = strFilePath; m_strFileName = strFileName; }

        //Set PackageFile Path///////////////////////////////////////////////////////////////////////////////////////////////
        public void Set_PackageFilePath(String strFilePath)
        {
            if (strFilePath.EndsWith("\\") == false)
                m_strPackageFilePath = strFilePath + "\\";
            else
                m_strPackageFilePath = strFilePath;
        }
		
        //Get PackageFile Full Path////////////////////////////////////////////////////////////////////////////////
        public String Get_FullPath_PackageFile()
        {
            String strFullFilePath = "";
            strFullFilePath = m_strPackageFilePath;

            //Check file path ends with \
            if (strFullFilePath.EndsWith("\\") == false) strFullFilePath += "\\";
            //Check filename is attached with ext
            if (m_strPackageFileName.EndsWith(m_strPackageFileExt) == false)
                strFullFilePath += m_strPackageFileName + m_strPackageFileExt;
            else
            {
                strFullFilePath += m_strPackageFileName;

                //remove ext from filename
                m_strPackageFileName = m_strPackageFileName.Replace(m_strPackageFileExt, "");
            }

            return strFullFilePath;
        }

        //Get Full File Path///////////////////////////////////////////////////////////////////////////////////////////////
        //This is to obtain the machine settings file path only
        public String Get_FullFilePath() 
        {
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";
            return (m_strFilePath + m_strFileName);  
        }

        //Get Setting folder location///////////////////////////////////////////////////////////////////////////////
        public String Get_MachineFilePath()
        {
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";
            return m_strFilePath;
        }
    }
}