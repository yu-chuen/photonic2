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
    public class Data_ErrorMessage
    {
        [XmlArray("ErrorMessage_List")]
        public List<ErrorMessage_Info> m_llErrorMessageInfo;
        String m_strFilePath = "";

        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_ErrorMessage()
        {
            m_llErrorMessageInfo = new List<ErrorMessage_Info>();
        }

        //Constructor//////////////////////////////////////////////////////////////////////////////////
        public Data_ErrorMessage(string strFilePath)
        {
            m_llErrorMessageInfo = new List<ErrorMessage_Info>();
			m_strFilePath = strFilePath;
			Default_Message();
        }

        //Create///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Create()
        {
            Default_Message();

			//for (int errorID = 1000; errorID <= 2000; errorID++)
			//{
			//    short msg_available = 0;
			//    Get_ErrorInfos(errorID, ref msg_available);
			//    if (msg_available == 0)
			//        AddErrorInfos(errorID, "Error " + errorID.ToString(), "Recover " + errorID.ToString());
			//}
        }

        //From Registry///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Default_Message()
        {
			ErrorTable mErrorTable = new ErrorTable();

			int iCount = 0;
			int iTotalNum = Enum.GetNames(typeof(ErrorTable.en_ErrorCode)).Length;

			for (iCount = 0; iCount < iTotalNum; iCount++)
			{
				AddErrorInfos (iCount, mErrorTable.Get_ErrorMessage(iCount), mErrorTable.Get_ErrorRecovery(iCount));
			}
        } 

        //From Registry///////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            String strFilePathFull = m_strFilePath + "ErrorMessage.xml";
            if (File.Exists(strFilePathFull) == false) return;
            m_llErrorMessageInfo = XMLSerializer.Deserialize_Func<List<ErrorMessage_Info>>(strFilePathFull);
        }

        //To Registry/////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            String strFilePathFull = m_strFilePath + "ErrorMessage.xml";
            XMLSerializer.Serialize_Func<List<ErrorMessage_Info>>(m_llErrorMessageInfo, strFilePathFull);
        }

        //Add Instruction into the list//////////////////////////////////////////////////////////////////////////
        public void AddErrorInfos(int errorID, string strErrorMsg, string strRecoveryMsg)
        {
            ErrorMessage_Info in_info = new ErrorMessage_Info();

            in_info.m_ErrorID = Math.Abs(errorID);
            in_info.m_strErrorMsg = strErrorMsg;
            in_info.m_strRecoveryMsg = strRecoveryMsg;

            m_llErrorMessageInfo.Add(in_info);
        }

        //Get Instruction position////////////////////////////////////////////////////////////////////////////////
        public ErrorMessage_Info Get_ErrorInfos(int errorID, ref short available)
        {
            ErrorMessage_Info in_info = new ErrorMessage_Info();
            available = 0;
            foreach (ErrorMessage_Info settings in m_llErrorMessageInfo)
            {
                if (settings.m_ErrorID == Math.Abs(errorID))
                {
                    in_info = settings;
                    available = 1;
                    break;
                }
            }
            
            return in_info;
        }

        //update Settings/////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Note: This is meant to update the current indexer pos settings only.
        public void Update_Errors(ErrorMessage_Info error)
        {
            int iTotal = m_llErrorMessageInfo.Count();
            for (int i = 0; i < iTotal; i++)
            {
                if (error.m_ErrorID == m_llErrorMessageInfo[i].m_ErrorID)
                    m_llErrorMessageInfo[i] = error;
            }
        }

        //get Error count////////////////////////////////////////////////////////////////////////////////
        public int Get_Infos_Count() { return m_llErrorMessageInfo.Count(); }

        //get Error ID////////////////////////////////////////////////////////////////////////////////
        public int Get_InfosID(int iCount) { return m_llErrorMessageInfo[iCount].m_ErrorID; }
    }

    //structure that contains a single instruction
    public struct ErrorMessage_Info
    {
        public int m_ErrorID;
        public string m_strErrorMsg;
        public string m_strRecoveryMsg;
    }
}