/****************************************************************************************************************************************
 * Data Log class: 
 * Desc:    This class is to write to data log files. 
 *               Records the results of the machine's product measurements and adjustments.
 *               Or Records the machine sequence. However, the foldername and the file name has to be change.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.IO;
using System.Collections.Concurrent;
using System.Threading;

namespace StaticCoreLib
{
    public class Log_Data
    {
        //Variables
        String m_strFilePath = "";
        String m_strFolderName = "Data_Log Folder";                                               // to set the folder name. Ex: \\Data log Folder
        String m_strFileName = "Data";                                                                       // to set the text file name. Ex: \\test.txt
        String m_strDataFileName = "";
        String m_strExtension = ".txt";
        String m_strDataToWrite = "";

        ConcurrentQueue<String> m_strQt = new ConcurrentQueue<string>();
        private readonly object m_Lock = new object();
        Thread m_thSeqLog;

        bool m_bSeqLog_ThreadStart = false;
        bool m_bFlagSeqLog_Begin = true;
        short m_iAutoSetFileName = 0;                                               //auto set filename: 0=>Day, 1=>month, 2=>hour
        String m_strHeader = "";

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public Log_Data()
        {
            //Create the data log folder if it is not created
            m_strFilePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath);
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";
            
            //Append all strings to the main filename string
            m_strDataFileName = m_strFilePath + m_strFolderName + m_strFileName + m_strExtension;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Destructor
        ~Log_Data()
        {
            if (this.m_bSeqLog_ThreadStart == true)
            {
                this.m_bFlagSeqLog_Begin = false;
                Thread.Sleep(15);
            }
        }

        //Clean Up///////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Clean_Up()
        {
            if (this.m_bSeqLog_ThreadStart == true)
            {
                this.m_bFlagSeqLog_Begin = false;
                Thread.Sleep(15);
            }
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set the FileName
        public void SetFileName(String strFileName)
        {
			if ( strFileName == "" ) return;

            m_strFileName = strFileName;
            m_strDataFileName = m_strFilePath + m_strFolderName + m_strFileName + m_strExtension;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set Folder Name
        public void SetFolderName(String strFoldername)
        {
			if ( strFoldername == "" ) return;

            m_strFolderName = strFoldername;
            if (m_strFolderName.EndsWith("\\") == false) m_strFolderName += "\\";

            m_strDataFileName = m_strFilePath + m_strFolderName + m_strFileName + m_strExtension;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set File Path
        public void SetFilePath(String strFilePath)
        {
            //Skip if empty
            if (strFilePath == "") return;
            
            //Check and add back slash
            m_strFilePath = strFilePath;
            if (m_strFilePath.EndsWith("\\") == false)      m_strFilePath += "\\";

            //Set to full file path
            m_strDataFileName = m_strFilePath + m_strFileName + m_strExtension;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Set Extension
        public void SetExtensionName(String strExtension)
        {
            m_strExtension = strExtension;
            if (m_strExtension.StartsWith(".") == false) m_strExtension = "." + m_strExtension;

            m_strDataFileName = m_strFilePath + m_strFolderName + m_strFileName + m_strExtension;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Clear Log / delete log
        public void ClearLog()
        {
            System.IO.File.Delete(m_strDataFileName);
        }

        //Auto Set Filename based on hour/day/month//////////////////////////////////////////
        public short Auto_CheckNSetFileName(String strFileNameCommon)
        {
            String strFileName = "";

			//file name selection
            switch (m_iAutoSetFileName)
            {
                //Day------------------------------------------------------------------------------------
                case 0: strFileName = strFileNameCommon + "_" + DateTime.Now.ToString("yyyyMMdd");  break;
                //Month----------------------------------------------------------------------------------
                case 1: strFileName = strFileNameCommon + "_" + DateTime.Now.ToString("yyyyMM"); break;
                //Hour-------------------------------------------------------------------------------------
                case 2: strFileName = strFileNameCommon + "_" + DateTime.Now.ToString("yyyyMMdd_HH"); break;
				//Default: bypass----------------------------------------------------------------------
				default: break;
            }

            //Check filename if it is the same
            if (strFileName == m_strFileName) return 0;

            //Set file name
            SetFileName(strFileName);

            //Return a new file name is set. REquire to write header
            return 1;
        }

        //Get and Set ////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_FullFilePath() { return m_strDataFileName; }
		//Set Header String//////////////////////////////////////////////////////////////////////////////////////
        public void Set_Header(String strHeader) { m_strHeader = strHeader; }       //set header
		//Set Data to write////////////////////////////////////////////////////////////////////////////////////////
        public void Set_DataToWrite(String strData, bool bLast = false)                     //set data to write in buffer with auto add comma for csv file formats
        {
            m_strDataToWrite += strData;
            if (bLast == false) m_strDataToWrite += ",";
        }
		//Get Data to Write///////////////////////////////////////////////////////////////////////////////////////
        public String Get_DataToWrite()                                                                          //get the data buffer to be written to file
        {
            String strData = m_strDataToWrite;
            m_strDataToWrite = "";      //reset
            return strData;
        }
		//Get and Set AutoSetFileName////////////////////////////////////////////////////////////////
		/// <summary>
		/// Automatically add time and date into the filename: 0=>yyyyMMdd, 1=>yyyyMM, 2=>yyyyMMdd_HH, else=>bypass / skip
		/// </summary>
		/// <param name="iSelect"></param>
		public void Set_AutoSetFileName( short iSelect ) { m_iAutoSetFileName = iSelect; }



        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Write To File
        public bool ToFile(String strData)
        {
            if (System.IO.File.Exists(m_strDataFileName) == false)
            {
                System.IO.Directory.CreateDirectory(m_strFilePath + "\\" + m_strFolderName);
            }

            StreamWriter stWriter = new StreamWriter(m_strDataFileName, true);
            stWriter.WriteLine(strData);
            stWriter.Close();
            
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Write To Log File. 
        //Genetec Log file has a structure where = yyyy-MM-dd, hh:mm:ss: xxxxlogsxxxxxx
        public bool Write_ToLogFile(String strData, bool bAutoSetFilename = false)
        {
            bool ret = true;
            String strDateDay = DateTime.Now.ToString("yyyy-MM-dd");
            String strDateTime = DateTime.Now.ToString("HH:mm:ss");
            String strFilename = strDateDay + " Log";

            //Get file name
            if (bAutoSetFilename == true)
                if (strFilename != m_strFileName) SetFileName(strFilename);

            //set to log string
            strData = strDateDay + ": " + strDateTime + ": " + strData;
            ret = ToFile(strData);

            return ret;
        }

        //Data log file - Genetec Data log file///////////////////////////////////////////////////////////////////////////////
        public bool Write_ToDataLog(String strFileName_Extra)
        {
            //Set file name and write header
			if (strFileName_Extra != "") strFileName_Extra = "_" + strFileName_Extra;
            //if (Auto_CheckNSetFileName("Data" + strFileName_Extra) == 1)
            //{
            //    if (File.Exists(Get_FullFilePath()) == false)
            //    {
            //        if ( m_strHeader != "")
            //            ToFile(m_strHeader);
            //    }
            //}

            Auto_CheckNSetFileName("Data" + strFileName_Extra);

            if (File.Exists(Get_FullFilePath()) == false)
            {
                ToFile(m_strHeader);
            }

            //Convert to Read-Write
            File.SetAttributes(Get_FullFilePath(), File.GetAttributes(Get_FullFilePath()) & ~FileAttributes.ReadOnly);

            //write data to data log
            if (ToFile(Get_DataToWrite()) == false) return false;

            //Convert back to Read only
            File.SetAttributes(Get_FullFilePath(), System.IO.FileAttributes.ReadOnly);

            return true;
        }



        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //For sequence log use///////////////////////////////////////////////////////////////////////////////////////////////////
        //Set to Concurent Queue: This is used so that the writing to seq log file will not interrupt the seq process.
        public void Write_ToSeqLog(String strData)
        {
            lock (m_Lock) { this.m_strQt.Enqueue(strData); }
        }

        //Start Seq Log writing thread///////////////////////////////////////////////////////////////////////////////////////////
        public void SeqLog_StartThread()
        {
            m_thSeqLog = new Thread(SeqLog_WriteThread);
            m_thSeqLog.Start();
        }

        //Thread to write to log file/////////////////////////////////////////////////////////////////////////////////////////////
        private void SeqLog_WriteThread()
        {
            String strLog = "";
            this.m_bSeqLog_ThreadStart = true;

            while (this.m_bFlagSeqLog_Begin == true)
            {
                if (this.m_strQt.Count > 0)
                {
                    this.m_strQt.TryDequeue(out strLog);
                    this.Write_ToLogFile(strLog, true);
                    strLog = "";
                }
                else
                    Thread.Sleep(10);
            }

            this.m_bSeqLog_ThreadStart = false;
        }
    }
}
