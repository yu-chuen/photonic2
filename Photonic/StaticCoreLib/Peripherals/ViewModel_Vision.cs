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
	public class ViewModel_Vision
	{
		//variables
        int m_iErrorID = (int)ErrorTable.en_ErrorCode.NO_ERR;
        String m_strErrorMsgAdd = "";
        String m_strErrorMsgFront = "";

		//list of commands based on camera
		public List<Vision_DebugString> m_ll_stVisionDebugString = new List<Vision_DebugString>();

        SeqMachineControl m_pSeqMachineCtrl = null;

		//vision label names
		List<string> m_ll_strVisionLabel = new List<string>();

        //lock: prevent double trigger.
        private readonly object m_lock_Access = new object();

		//constructor
		public ViewModel_Vision(SeqMachineControl pSeqMachineCtrl)
		{
            m_pSeqMachineCtrl = pSeqMachineCtrl;

            //add command, token and strData / command parameters
            Add_String(pSeqMachineCtrl.m_Camera1.Label, Vision_Command.LOAD_RECIPE, "CAM1", "");
            Add_String(pSeqMachineCtrl.m_Camera1.Label, Vision_Command.INSPECT_PART_FRONT, "CAM1", "Dummy_Test123");
            Add_String(pSeqMachineCtrl.m_Camera1.Label, Vision_Command.SAVE_RECIPE, "CAM1", "");

            Add_String(pSeqMachineCtrl.m_Camera2.Label, Vision_Command.LOAD_RECIPE, "CAM2", "");
            Add_String(pSeqMachineCtrl.m_Camera2.Label, Vision_Command.INSPECT_PART_REAR, "CAM2", "Dummy_Test123");
            Add_String(pSeqMachineCtrl.m_Camera2.Label, Vision_Command.SAVE_RECIPE, "CAM2", "");
		}

		//Add String//////////////////////////////////////////////////////////////////////
		public void Add_String( String strCamLabel, String strCommand, String strToken, String strData )
		{
			Vision_DebugString mVisionDebug = new Vision_DebugString();
			mVisionDebug.m_strCamLabel = strCamLabel;
			mVisionDebug.m_strCommand = strCommand;
			mVisionDebug.m_strToken = strToken;
			mVisionDebug.m_strData = strData;
			
			m_ll_stVisionDebugString.Add(mVisionDebug);
		}

		//Get String via Command String//////////////////////////////////////////////////////////////////////
		public Vision_DebugString Get_VisionDebugString( String strCommand )
		{
			Vision_DebugString mVisionDebug = new Vision_DebugString();
			mVisionDebug.m_strCommand = "unknown";

			foreach ( Vision_DebugString st in m_ll_stVisionDebugString )
			{
				if ( st.m_strCommand == strCommand )
				{
					mVisionDebug = st;
					break;
				}
			}

			return mVisionDebug;
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

        private void Set_ModuleVisionInspection_Error()
        {
        }

        
     //vision test sub sequence
        public bool VisionTest(int iCount) //iCount = repeat trigger vision how many times
        {
            bool bRet = false;

            //lock
            lock (m_lock_Access)
            {
                //set RFID here to be used for trigger camera
                m_pSeqMachineCtrl.m_DataUnit.RFID_OP90_SN_str = "OP90_VisionTest";

                // clamp up
                //if (m_pModule_OP90_VisionInspection.m_cylClamp_Wire.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_ModuleVisionInspection_Error(); goto End; }

                for (int i = 1; i <= iCount; i++)
                {
                    // clamp up
                    //if (m_pModule_OP90_VisionInspection.m_cylClamp_Wire.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_ModuleVisionInspection_Error(); goto End; }

                    //force reset vision results here
                    m_pSeqMachineCtrl.m_DataUnit.Reset_OP90_WT_Vision_Results();

                    //clamp wire for wire tester
                    //if (!m_pModule_OP90_VisionInspection.m_cylClamp_Wire.Cyl_Trigger_N_WaitTaskComplete(true)) { Set_ModuleVisionInspection_Error(); goto End; }

                    //read wire tester
                    //if (m_pModule_OP90_VisionInspection.WireTester_Read(WT_LN1_str) == false) { Set_ModuleVisionInspection_Error(); goto End; }
                    //if (m_pModule_OP90_VisionInspection.WireTester_Read(WT_LN2_str) == false) { Set_ModuleVisionInspection_Error(); goto End; }
                    //if (m_pModule_OP90_VisionInspection.WireTester_Read(WT_LN3_str) == false) { Set_ModuleVisionInspection_Error(); goto End; }
                    //if (m_pModule_OP90_VisionInspection.WireTester_Read(WT_LR0_str) == false) { Set_ModuleVisionInspection_Error(); goto End; }
                    //if (m_pModule_OP90_VisionInspection.WireTester_Read(WT_R0R1_str) == false) { Set_ModuleVisionInspection_Error(); goto End; }
                    
                    //read temperature
                    //if (m_pModule_OP90_VisionInspection.WireTester_Temperature() == false) { Set_ModuleVisionInspection_Error(); goto End; }

                    //check grading
                    //int iRet_HTR = m_pModule_OP90_VisionInspection.HTR_Grading();
                    //int iRet_RTD = m_pModule_OP90_VisionInspection.RTD_Grading();
                    //int iRet = iRet_HTR;

                    //if (iRet_RTD > iRet_HTR)
                    //    iRet = iRet_RTD;

                    //Set String According to Grade
                    //string Add_Grade = m_pModule_OP90_VisionInspection.Laser_Char(iRet);
                    //m_pSeqMachineCtrl.m_DataUnit.OP90_WT_Result_Overall_Grade_str = Add_Grade;
                    //m_pSeqMachineCtrl.m_DataUnit.OP90_WT_Result_HTR_Grade_str = m_pModule_OP90_VisionInspection.ConvertIntToGrade(iRet_HTR);
                    //m_pSeqMachineCtrl.m_DataUnit.OP90_WT_Result_RTD_Grade_str = m_pModule_OP90_VisionInspection.ConvertIntToGrade(iRet_RTD);

                    //write Data log
                    m_pSeqMachineCtrl.Write_ToDataLog_OP90();

                }


                //docking down
                //if (m_pModule_OP90_VisionInspection_Conv.m_cylDocking_VisionStat.Cyl_Trigger_N_WaitTaskComplete(false) == false) { Set_ModuleVisionInspection_Error(); goto End; }
            }

            bRet = true;

        End:
            return bRet;
        }

        //Wire Tester Position////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public const String WT_LN1_str = "LN1";
        public const String WT_LN2_str = "LN2";
        public const String WT_LN3_str = "LN3";
        public const String WT_LR0_str = "LR0";
        public const String WT_R0R1_str = "R0R1";
        public const String WT_CLOSE_str = "close";
	}
}

	//Vision Command struct
	public struct Vision_DebugString
	{
		public String m_strCamLabel;
		public String m_strCommand;
		public String m_strToken;
		public String m_strData;
	}