using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

/****************************************************************************************************************************************
 * View Model Class: This class is used to link the module robot, sliders and indexers goto function to the manual pages
****************************************************************************************************************************************/
namespace StaticCoreLib
{
	public class ViewModel_GotoFunctions
	{
		//variables
		
		int m_iErrorID = ( int ) ErrorTable.en_ErrorCode.NO_ERR;
		String m_strErrorMsgAdd = "";
		String m_strErrorMsgFront = "";

		//modules pointer
		SeqMachineControl m_pSeqMachineCtrl = null;
		MonitorInput m_pMonitorInput = null;


		private readonly object m_lock_Access = new object();				//blocker to signal end of process
		bool m_bFlag_Access = false;
		bool Flag_Access_b { set { lock ( m_lock_Access ) { m_bFlag_Access = value; } } get { lock ( m_lock_Access ) { return m_bFlag_Access; } } }


		//Constuctor//////////////////////////////////////////////////////////////////////////////////////////
		public ViewModel_GotoFunctions( SeqMachineControl mpSeqMachineCtrl )
		{
			m_pSeqMachineCtrl					= mpSeqMachineCtrl;
			m_pMonitorInput						= m_pSeqMachineCtrl.m_MonitorInput;

		}


		//Error Message: Set Error Messages and ID////////////////////////////////
		public void Set_Error( int iErrorID, String strErrorMsgAdd, String strErrorMsgFront )
		{
			m_iErrorID = iErrorID;
			m_strErrorMsgAdd = strErrorMsgAdd;
			m_strErrorMsgFront = strErrorMsgFront;
		}
		
		//Get Error Message///////////////////////////////////////////////////////////////////////////
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

		//Set Pointer/////////////////////////////////////////////////////////////////////////////////////////
		private void SetPointers()
		{
		}




		/**************************************************************************************************************
		 * Robot Goto Function For Manual page
		 * Note: Interlock is controlled by the module. please edit there
		 **************************************************************************************************************/
        public bool Goto_RobotStation(string strPos, int iTypeID)
        {
            bool bManualPageGotoZht = true;
            bool bRet = false;

            //set pointers
            SetPointers();

            //blocker
            if (Flag_Access_b == true) return true;
            if (m_pSeqMachineCtrl.Flag_InitOK == false) { Set_Error((int)ErrorTable.en_ErrorCode.MACHINE_INITFAIL, "", ""); return false; }
            if (m_pMonitorInput.Idle() == false) { Set_Error(m_pMonitorInput.Get_Error(), "", "MonitorInput"); return false; }

            Flag_Access_b = true;

            //Select module solder station
            switch (iTypeID)
            {
            }

            //set to true. Procecss complete
            bRet = true;
        End:
            Flag_Access_b = false;
            return bRet;
        }


		//Custom control : requester Kamarul///////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Goto_SolderPoint(int iSolderPoint, int iTypeID)
        {
            bool bManualPageGotoZht = true;
            bool bRet = false;
            String strPos = "";
            int iSolderProgram = 0;

            //set pointers
            SetPointers();

            //blocker and checker
            if (Flag_Access_b == true) { Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CUSTOMCMDINPROGRESS, "", ""); return false; }
            if (m_pSeqMachineCtrl.Flag_InitOK == false) { Set_Error((int)ErrorTable.en_ErrorCode.MACHINE_INITFAIL, "", ""); return false; }
            if (m_pMonitorInput.Idle() == false) { Set_Error(m_pMonitorInput.Get_Error(), "", "MonitorInput"); return false; }

            Flag_Access_b = true;

            //Select module solder station
            switch (iTypeID)
            {
            }

            //ignore: same position
            //if ( pModuleSolderStation.Position_Tracker == strPos ) return true;

            //set to true. Procecss complete
            bRet = true;
        End:
            Flag_Access_b = false;
            return bRet;
        }

		//Custom Control: Sub sequence, solder all points//////////////////////////////////////////////////////////////////////////////////
        public bool SolderAllPoints(int iTypeID)
        {
            bool bRet = false;
            String strPos = "";
            int iSolderProgram = 0;

            //set pointers
            SetPointers();

            //blocker and checker
            if (Flag_Access_b == true) { Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CUSTOMCMDINPROGRESS, "", ""); return false; }
            if (m_pSeqMachineCtrl.Flag_InitOK == false) { Set_Error((int)ErrorTable.en_ErrorCode.MACHINE_INITFAIL, "", ""); return false; }
            if (m_pMonitorInput.Idle() == false) { Set_Error(m_pMonitorInput.Get_Error(), "", "MonitorInput"); return false; }

            Flag_Access_b = true;

            //Select module solder station-------------------------------------------------------------------------------------------
            switch (iTypeID)
            {
            }

            //set to true. Procecss complete
            bRet = true;
        End:
            Flag_Access_b = false;
            return bRet;
        }


        //Custom Control: Clean solder iron////////////////////////////////////////////////////////////////////////////////////////////////
        public bool CleanSolderIron(int iTypeID)
        {
            bool bRet = false;

            //set pointers
            SetPointers();

            //blocker and checker
            if (Flag_Access_b == true) { Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CUSTOMCMDINPROGRESS, "", ""); return false; }
            if (m_pSeqMachineCtrl.Flag_InitOK == false) { Set_Error((int)ErrorTable.en_ErrorCode.MACHINE_INITFAIL, "", ""); return false; }
            if (m_pMonitorInput.Idle() == false) { Set_Error(m_pMonitorInput.Get_Error(), "", "MonitorInput"); return false; }

            Flag_Access_b = true;

            //Select module solder station-------------------------------------------------------------------------------------------
            switch (iTypeID)
            {
            }

            //process done with an OK
            bRet = true;
End:
            Flag_Access_b = false;
            return bRet;
        }
	}
}