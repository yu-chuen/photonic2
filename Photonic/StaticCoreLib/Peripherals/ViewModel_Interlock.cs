using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/****************************************************************************************************************************************
 * View Model Class: This class is used to link the libraries to the main UI manual pages, cyclinder and output bits
****************************************************************************************************************************************/
namespace StaticCoreLib
{
    public class ViewModel_Interlock
    {
        SeqMachineControl m_pSeqMachineCtrl = null;
        MonitorInput m_pMonitorInput = null;

        Module_OP110_HotBlower m_pModuleHotBlower = null;

        //variables
        int m_iErrorID = (int)ErrorTable.en_ErrorCode.NO_ERR;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsgFront = "";

        //constructor///////////////////////////////////////////////////////////////////////////////////
        public ViewModel_Interlock(SeqMachineControl mSeqMachineCtrl)
        {
            m_pSeqMachineCtrl = mSeqMachineCtrl;
            m_pMonitorInput = mSeqMachineCtrl.m_MonitorInput;

            m_pModuleHotBlower = m_pSeqMachineCtrl.m_Module_OP110_HotBlower;
        }


        //Error Message: Set Error Messages and ID////////////////////////////////
        public void Set_Error(int iErrorID, String strErrorMsgAdd, String strErrorMsgFront)
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
            Set_Error(0, "", "");
            //return full error message string
            return strError;
        }



        /**************************************************************************************************************
         * Output bit Interlock For Manual page
         **************************************************************************************************************/
        public bool OutputBit_Interlock(String strIOLabel, int iOutput)
        {
            //Check machine status
            if (m_pMonitorInput.Idle() == false) { m_iErrorID = m_pMonitorInput.Get_Error(); return false; }

            return true;
        }


        /**************************************************************************************************************
         * Cylinder Interlock For Manual page
         **************************************************************************************************************/
        public bool Cylinder_Interlock(String strIOLabel, int iOutput)
        {
            bool bRet = true;   //for robotstation use only
            double dbZPos = 0;
            double dbYPos = 0;

            //Check machine status
            if (m_pMonitorInput.Idle() == false) { m_iErrorID = m_pMonitorInput.Get_Error(); return false; }


            //Interlock: OP110 cylinder
            if (strIOLabel == m_pModuleHotBlower.m_cylReflector_ExtRet.Get_Label() && iOutput == 1)
            {
                if (m_pModuleHotBlower.m_cylReflector_UpDown.Get_Work_Sensor().Read() == 1) { Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_REFLECTOROUT, "", m_pModuleHotBlower.m_cylReflector_UpDown.Get_Label()); return false; }
            }

            if (strIOLabel == m_pModuleHotBlower.m_cylReflector_ExtRet.Get_Label() && iOutput == 0)
            {
                if (m_pModuleHotBlower.m_cylReflector_UpDown.Get_Work_Sensor().Read() == 1) { Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_REFLECTOROUT, "", m_pModuleHotBlower.m_cylReflector_UpDown.Get_Label()); return false; }

            }

            if (m_pSeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
            {
                if (strIOLabel == m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Label()) //axis X extend retract
                {
                    if (m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Work_Sensor().Read() == 1 || m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Work_Output().Read() == 1)
                    {
                        Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CYLINDER_COLLISION, m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Label() + " not allowed to move when " + m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label() + " is down.", m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Label());
                        return false;
                    }
                }

                if (strIOLabel == m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label()) //axis Z up down
                {
                    if (m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Home_Sensor().Read() == 1 || m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Home_Output().Read() == 1)
                    {
                        Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CYLINDER_COLLISION, m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label() + " not allowed to move when " + m_pModuleHotBlower.m_cylCombPNP_AxisX_ExtRet.Get_Label() + " is retracted.", m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label());
                        return false;
                    }

                    if (m_pModuleHotBlower.m_cylCombGripper_GripUngrip.Get_Work_Sensor().Read() == 1 || m_pModuleHotBlower.m_cylCombGripper_GripUngrip.Get_Work_Output().Read() == 1)
                    {
                        Set_Error((int)ErrorTable.en_ErrorCode.INTERLOCK_CYLINDER_COLLISION, m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label() + " not allowed to move when " + m_pModuleHotBlower.m_cylCombGripper_GripUngrip.Get_Label() + " is gripped.", m_pModuleHotBlower.m_cylCombPNP_AxisZ_DownUp.Get_Label());
                        return false;
                    }
                }
            }

            return true;
        }
    }
}