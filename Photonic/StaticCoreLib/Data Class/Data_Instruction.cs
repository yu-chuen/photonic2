using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace StaticCoreLib
{
    //Instruction data storing class
    [Serializable]
    public class Data_Instruction
    {
        [XmlArray("Instruction_Setting_List")]
        public List<Instruction_Settings> m_llInstructionSettings;
        String m_strFilePath = "";

        [System.Xml.Serialization.XmlIgnore] public String m_strImageFolder = "Image";                              //Folder: Where images are stored
        [System.Xml.Serialization.XmlIgnore] public String m_strFolderPath = "Data_Instruction";                 //Folder: Where the data instructions are stored
        [System.Xml.Serialization.XmlIgnore] public String m_strFilename = "Instruction_Ori";                    //Filename: Data_instruction filename
        
        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_Instruction()
        {
            m_llInstructionSettings = new List<Instruction_Settings>();
        }

        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_Instruction(string strFilePath)
        {
            m_llInstructionSettings = new List<Instruction_Settings>();
            m_strFilePath = strFilePath;
            if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";
            m_strFilePath += "\\Data_Instruction\\";
            
            //From_Registry();
        }

        //Create///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Create_Instruction()
        {
            String strImageFile = "";
            //int stepNum = 1;

            for (int stepNum = 1; stepNum <= 100; stepNum++)
            {
                GetImageFileName(stepNum, ref strImageFile);
                AddSettings(stepNum, "Step " + stepNum.ToString(), "Step " + stepNum.ToString(), "Step " + stepNum.ToString(), strImageFile);
            }

            ////Check and Create folder if not existing
            //if (m_strFilePath.EndsWith("\\") == false) m_strFilePath += "\\";
            //if (Directory.Exists(m_strFilePath + "Image") == false) Directory.CreateDirectory(m_strFilePath + "Image");
        }

        //Get File paths/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_FilePath_Images() { return (m_strFilePath + "\\" + m_strImageFolder); }



        //Create Image Files Name////////////////////////////////////////////////////////////////////////////////////////
        private void GetImageFileName(int stepNum, ref string filename)
        {
            if (m_strFilePath.EndsWith("\\") == false)
                filename = m_strFilePath + "\\" + m_strImageFolder + "\\Step_" + stepNum.ToString() + ".bmp";
            else
                filename = m_strFilePath + "\\" + m_strImageFolder + "\\Step_" + stepNum.ToString() + ".bmp";

            return;
        }

        //From Registry///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFilePathFull = "";

            //Set folder / filename name with back slash
            if (m_strFolderPath.EndsWith("\\") == false) m_strFolderPath += "\\";
            if (m_strFilename.EndsWith(".xml") == false) m_strFilename += ".xml";

            //Set full file path
            strFilePathFull = m_strFilePath + m_strFolderPath + m_strFilename;
            //MessageBox.Show(strFilePathFull);

            m_llInstructionSettings.Clear();
            m_llInstructionSettings = XMLSerializer.Deserialize_Func<List<Instruction_Settings>>(strFilePathFull);

            //create pic list
            if (File.Exists(strFilePathFull) == false) Create_Instruction();
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFilePathFull = "";//m_strFilePath + "Instruction.xml";

            //Set folder / filename name with back slash
            if (m_strFolderPath.EndsWith("\\") == false) m_strFolderPath += "\\";
            if (m_strFilename.EndsWith(".xml") == false) m_strFilename += ".xml";

            //Set full file path
            strFilePathFull = m_strFilePath + m_strFolderPath + m_strFilename;

            //Write  to registry
            XMLSerializer.Serialize_Func<List<Instruction_Settings>>(m_llInstructionSettings, strFilePathFull);
        }

        //Add Instruction into the list//////////////////////////////////////////////////////////////////////////
        public void AddSettings(int seqState, string strStepName, string strStepMsg, string strInfoMsg, string strImageFile)
        {
            Instruction_Settings in_set = new Instruction_Settings();

            in_set.m_SeqState = seqState;
            in_set.m_strStepName = strStepName;
            in_set.m_strStepMsg = strStepMsg;
            in_set.m_strInfoMsg = strInfoMsg;
            in_set.m_strImageFile = strImageFile;

            m_llInstructionSettings.Add(in_set);
        }

        //Get Instruction position////////////////////////////////////////////////////////////////////////////////
        public Instruction_Settings Get_Settings(string strStepName)
        {
            Instruction_Settings in_set = new Instruction_Settings();

            foreach (Instruction_Settings settings in m_llInstructionSettings)
            {
                if (settings.m_strStepName == strStepName)
                {
                    in_set = settings;
                    break;
                }
            }

            return in_set;
        }

        //Get Instruction position////////////////////////////////////////////////////////////////////////////////
        public Instruction_Settings Get_Settings(int seqState)
        {
            Instruction_Settings in_set = new Instruction_Settings();

            foreach (Instruction_Settings settings in m_llInstructionSettings)
            {
                if (settings.m_SeqState == seqState)
                {
                    in_set = settings;
                    break;
                }
            }

            return in_set;
        }

        //update Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note: This is meant to update the current indexer pos settings only.
        public void Update_Settings(Instruction_Settings settings)
        {
            int iTotal = m_llInstructionSettings.Count();
            for (int i = 0; i < iTotal; i++)
            {
                if (settings.m_SeqState == m_llInstructionSettings[i].m_SeqState)
                    m_llInstructionSettings[i] = settings;
            }
        }

        //get settings count////////////////////////////////////////////////////////////////////////////////
        public int Get_Settings_Count() { return m_llInstructionSettings.Count(); }

        //get settings name////////////////////////////////////////////////////////////////////////////////
        public String Get_SettingsName(int iCount) { return m_llInstructionSettings[iCount].m_strStepName; }

        //get settings name////////////////////////////////////////////////////////////////////////////////
        public int Get_SettingsState(int iCount) { return m_llInstructionSettings[iCount].m_SeqState; }



    }

    //structure that contains a single instruction
    public struct Instruction_Settings
    {
        public int m_SeqState;
        public string m_strStepName;
        public string m_strStepMsg;
        public string m_strInfoMsg;
        public string m_strImageFile;
    }

}
