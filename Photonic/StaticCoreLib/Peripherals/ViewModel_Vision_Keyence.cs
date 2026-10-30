using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/****************************************************************************************************************
 * For the UI display and debugging at maintenance page
 ****************************************************************************************************************/
namespace StaticCoreLib
{
    public class ViewModel_Vision_Keyence
    {
        SeqMachineControl m_pSeqMachineCtrl = null;
        Data_UnitTempStorage m_DataUnit = null;
        Data_PackageFile m_DataPackage = null;

        //vision setting dlg - list of commands based on camera
        public List<Vision_Keyence_DebugString> m_ll_stVisionDebugString = new List<Vision_Keyence_DebugString>();

        //lock: prevent double trigger.
        private readonly object m_lock_Access = new object();

        //variables
        int m_iErrorID = (int)ErrorTable.en_ErrorCode.NO_ERR;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsgFront = "";

        //constructor
        public ViewModel_Vision_Keyence(SeqMachineControl pSeqMachineCtrl)
        {
            m_pSeqMachineCtrl = pSeqMachineCtrl;
            m_DataUnit = m_pSeqMachineCtrl.m_DataUnit;
            m_DataPackage = m_pSeqMachineCtrl.m_DataPackageFile;

            int iCount = 0;

            //add command, token and strData / command parameters
        }

        //Add String//////////////////////////////////////////////////////////////////////
        public void Add_String(String strCamLabel, int iNum, String strCommand, String strDescription)
        {
            Vision_Keyence_DebugString mVisionDebug = new Vision_Keyence_DebugString();
            mVisionDebug.m_strCamLabel = strCamLabel;
            mVisionDebug.m_iNum = iNum;
            mVisionDebug.m_strCommand = strCommand;
            mVisionDebug.m_strDescription = strDescription;

            m_ll_stVisionDebugString.Add(mVisionDebug);
        }

        //Get String via Command String//////////////////////////////////////////////////////////////////////
        public Vision_Keyence_DebugString Get_VisionDebugString(int iNum)
        {
            Vision_Keyence_DebugString mVisionDebug = new Vision_Keyence_DebugString();
            mVisionDebug.m_strCommand = "unknown";

            foreach (Vision_Keyence_DebugString st in m_ll_stVisionDebugString)
            {
                if (st.m_iNum == iNum)
                {
                    mVisionDebug = st;
                    break;
                }
            }

            return mVisionDebug;
        }

        //Get Error Message/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public String Get_ErrorMessage()
        {
            //get Error Message
            String strError = m_pSeqMachineCtrl.Get_ErrorMessage(m_iErrorID,m_strErrorMsgAdd,m_strErrorMsgFront);

            //return full error message string
            return strError;
        }

        //Error Message: Set Error Messages and ID////////////////////////////////
        public void Set_Error(int iErrorID, String strErrorMsgAdd, String strErrorMsgFront)
        {
            m_iErrorID = iErrorID;
            m_strErrorMsgAdd = strErrorMsgAdd;
            m_strErrorMsgFront = strErrorMsgFront;
        }

        //Set Module OP85 Vision Error///////////////////////////////////////////////////////////////////////////////
        private void Set_Module_OP85_Error()
        {
        }

        //repeat vision test subsequence
        public bool VisionTest_DockingOnce(int iCount) //iCount = repeat trigger camera how many times
        {
            bool bRet = false;

            //lock
            lock (m_lock_Access)
            {
                //set variables
                m_DataUnit.RFID_OP85_SN_str = "OP85_VisionTest";

            }

            bRet = true;

        End:
            return bRet;
        }
    }

    //Vision Command struct
    public struct Vision_Keyence_DebugString
    {
        public String m_strCamLabel;
        public int m_iNum;
        public String m_strCommand;
        public String m_strDescription;
    }
}