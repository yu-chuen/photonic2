/****************************************************************************************************************************************
 * Monitoring class
 * Desc:    This class is to monitor certain input bits (IO Bits) such as Emg, curtain sensor, main pressure valves and etc.
 *               Thus this class will signal all working module threads to stop if there is an error or pause if the curtain sensor detects.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

using System.Windows.Forms;
using System.Windows.Threading;

//For ServiceMessages
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace StaticCoreLib
{
    public class MonitorInput
    {
        //Variables
        SeqMachineControl		m_pSeqMachineCtrl = null;
        Module_ControlPanel		m_pModuleCtrlPanel = null;
       
        int m_iErrorID = (int) ErrorTable.en_ErrorCode.NO_ERR;

		//Lock
		private readonly object m_lock_GetErrorID = new object();
        private readonly object m_lock_Flag_Monitor = new object();
		private readonly object m_lock_Flag_ErrorMonitor = new object();

		bool m_bFlag_ErrorMonitor = false;

		public bool Flag_ErrorMonitor_b
		{
			set { lock ( m_lock_Flag_ErrorMonitor ) { m_bFlag_ErrorMonitor = value; } }
			get { lock ( m_lock_Flag_ErrorMonitor ) { return m_bFlag_ErrorMonitor; } }
		}

		//Constructor////////////////////////////////////////////////////////////////////////////////////////////////////
        public MonitorInput(SeqMachineControl mSeqMachineCtrl)
        {
            m_pSeqMachineCtrl		= mSeqMachineCtrl;
            m_pModuleCtrlPanel		= m_pSeqMachineCtrl.m_Module_ControlPanel;
        }
		
		//Monitor sensors and related input bits///////////////////////////////////////////////////////////
        public bool Idle()
        {
            Flag_ErrorMonitor_b = false;

            lock (m_lock_Flag_Monitor)
            {
                //EStop
                if (
                    m_pModuleCtrlPanel.m_pInEStopButton.Read() == 0         ||
                    m_pModuleCtrlPanel.m_pInEStopButton_OP110.Read() == 0   
                   )
                { 
                    Set_Error((int)ErrorTable.en_ErrorCode.ESTOP_PRESS_ERR); 
                    return false; 
                }

                //Safety relay
                if (m_pModuleCtrlPanel.m_pInSafetyRelay.Read() == 0)
                { 
                    Set_Error((int)ErrorTable.en_ErrorCode.SAFETYRELAY_OPEN); 
                    return false; 
                }

                //Air pressure
                if (m_pModuleCtrlPanel.m_pInAirPressure_UL.Read() == 0)
                { 
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.AIR_PRESSURE_UL_ERR; 
                    Flag_ErrorMonitor_b = true; 
                    return false; 
                }

                if (m_pModuleCtrlPanel.m_pInAirPressure_LL.Read() == 0)
                {
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.AIR_PRESSURE_LL_ERR; 
                    Flag_ErrorMonitor_b = true; 
                    return false; 
                }

                //door lock 
                if (m_pSeqMachineCtrl.Flag_RunningInit == true || m_pSeqMachineCtrl.Flag_Run == true)
                {
                    if (
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation1_Front.Read() == 0 || 
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation2_Front.Read() == 0 ||
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation3_Front.Read() == 0 || 
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation1_Back.Read()  == 0 ||
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation2_Back.Read()  == 0 || 
                        m_pModuleCtrlPanel.m_inpDoorLock_SolderStation3_Back.Read()  == 0 ||
                        m_pModuleCtrlPanel.m_inpDoorLock_VisionStation.Read()        == 0 || 
                        m_pModuleCtrlPanel.m_inpDoorLock_HotBlower_Front.Read()      == 0 ||
                        m_pModuleCtrlPanel.m_inpDoorLock_HotBlower_Back.Read()       == 0
                       )
                    { 
                        Set_Error((int)ErrorTable.en_ErrorCode.DOOKLOCK_OPEN); 
                        return false; 
                    }
                }
            }

            return true;
        }


		//Set Error///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public void Set_Error( int iErrorID )
		{
			m_iErrorID = iErrorID;
			Flag_ErrorMonitor_b = true; 
		}


		//Get Error///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_Error()
		{
			lock ( m_lock_GetErrorID )
			{
				return m_iErrorID;
			}
		}
    }
}
