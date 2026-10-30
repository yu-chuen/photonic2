using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/****************************************************************************************************************
 * For the UI display and debugging at maintenance page
 ****************************************************************************************************************/
namespace StaticCoreLib
{
	public class ViewModel_HotBlower
	{
        SeqMachineControl m_pSeqMachineCtrl = null;
        Module_OP110_HotBlower m_pModule_HotBlower = null;

		//variables
        int m_iErrorID = (int)ErrorTable.en_ErrorCode.NO_ERR;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsgFront = "";
	
		//vision label names
		List<string> m_ll_strVisionLabel = new List<string>();

        //lock: prevent double trigger.
        private readonly object m_lock_Access = new object();

		//constructor
        public ViewModel_HotBlower(SeqMachineControl pSeqMachineCtrl)
		{
            m_pSeqMachineCtrl = pSeqMachineCtrl;
            m_pModule_HotBlower = m_pSeqMachineCtrl.m_Module_OP110_HotBlower;
		}


        public String Get_ErrorMessage()
        {
            //get Error Message
            String strError = m_pSeqMachineCtrl.Get_ErrorMessage(m_iErrorID, m_strErrorMsgAdd, m_strErrorMsgFront);

            //reset
            m_iErrorID = 0;
            m_strErrorMsgAdd = "";
            m_strErrorMsgFront = "";

            //return full error message string
            return strError;
        }

         public void Set_Error(int iErrorID, String strErrorMsgAdd, String strErrorMsgFront)
        {
            m_iErrorID = iErrorID;
            m_strErrorMsgAdd = strErrorMsgAdd;
            m_strErrorMsgFront = strErrorMsgFront;
        }

        private void Set_ModuleHotBlower_Error()
        {
            Set_Error(m_pModule_HotBlower.Get_ErrorID(), m_pModule_HotBlower.Get_ErrorMsgAdd(), m_pModule_HotBlower.Get_ErrorMsg_Front());
        }
	}
}