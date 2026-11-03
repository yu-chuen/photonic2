/****************************************************************************************************************************************
 * Module Control Panel class: 
 * Desc:    This class is to control outputs, cylinders and read inputs, sensors which are not related to any other modules.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class Module_ControlPanel
    {
        //Variables
        SeqMachineControl m_SeqMachineCtrl;
        String m_strFilePath = "";
        MonitorInput m_pMonitorInput;
        public String m_strLabel = "Module Control Panel";

		public InputBit m_pInSafetyRelay;
		public InputBit m_pInAirPressure_UL;
		public InputBit m_pInAirPressure_LL;
        public InputBit m_pInEStopButton;
		public InputBit m_pInEStopButton_OP110;
		public InputBit m_inpKeySwitch;
        public InputBit m_inpKeySwitch1;


        public InputBit m_inpDoorLock_SolderStation1_Front;
		public InputBit m_inpDoorLock_SolderStation2_Front;
		public InputBit m_inpDoorLock_SolderStation3_Front;
		public InputBit m_inpDoorLock_SolderStation1_Back;
		public InputBit m_inpDoorLock_SolderStation2_Back;
		public InputBit m_inpDoorLock_SolderStation3_Back;
		public InputBit m_inpDoorLock_VisionStation;
		public InputBit m_inpDoorLock_HotBlower_Back;
		public InputBit m_inpDoorLock_HotBlower_Front;
        public InputBit m_inpDoorLock_HotBlower_Front1;
        public InputBit m_inpDoorLock_HotBlower_Front2;
        public InputBit m_inpDoorLock_HotBlower_Front3;
        public InputBit m_inpDoorLock_HotBlower_Front4;
        public InputBit m_inpDoorLock_HotBlower_Front5;
        public InputBit m_inpDoorLock_HotBlower_Front6;
        public InputBit m_inpDoorLock_HotBlower_Front7;
        public InputBit m_inpDoorLock_HotBlower_Front8;
        public InputBit m_inpDoorLock_HotBlower_Front9;
        public InputBit m_inpDoorLock_HotBlower_Front10;

        public OutputBit m_pOutTowerLightRed_Static;
        public OutputBit m_pOutTowerLightGreen_Static;
        public OutputBit m_pOutTowerLightYellow_Static;
        public OutputBit m_pOutTowerLightRed_Blink;
        public OutputBit m_pOutTowerLightYellow_Blink;
        public OutputBit m_pOutTowerLightGreen_Blink;
        public OutputBit m_pOutTowerLightBuzzer;

		public OutputBit m_pOutMaintenanceLight;
        public OutputBit m_pOutMaintenanceLight_VisionStat;

		public OutputBit m_pOutDoorLock_SolderStation;
		public OutputBit m_pOutDoorLock_VisionStation;
		public OutputBit m_pOutDoorLock_HotBlowerStation;


        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public Module_ControlPanel(SeqMachineControl mSeqMachineCtrl)
        {
            m_SeqMachineCtrl = mSeqMachineCtrl;
            m_strFilePath = m_SeqMachineCtrl.m_MachineInfo.m_strFilePath;

            m_pMonitorInput = m_SeqMachineCtrl.m_MonitorInput;
        }

        //////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Init or Create cylinders and IO ports
        public bool Create_InputsOutputs(InterfaceCards mInterface)
        {
            int iBitID = 0;

            //input--------------------------------------------------------------------------------------------------------------------
            iBitID = 0;		m_pInAirPressure_UL						= new InputBit(mInterface.Get_InputPort(1), iBitID, "Air Pressure Upper Limit (NC)");
            iBitID = 1;		m_pInAirPressure_LL						= new InputBit(mInterface.Get_InputPort(1), iBitID, "Air Pressure Lower Limit (NO)");
            iBitID = 2;		m_pInSafetyRelay						= new InputBit(mInterface.Get_InputPort(4), iBitID, "Safety Relay");
            iBitID = 3;		m_pInEStopButton						= new InputBit(mInterface.Get_InputPort(4), iBitID, "EStop - OP90");
            iBitID = 4;	m_pInEStopButton_OP110					= new InputBit(mInterface.Get_InputPort(5), iBitID, "EStop - OP110");
            iBitID = 5;		m_inpKeySwitch							= new InputBit(mInterface.Get_InputPort(4), iBitID, "Key Switch OP90");
            iBitID = 13; m_inpKeySwitch1 = new InputBit(mInterface.Get_InputPort(4), iBitID, "11111111");

            iBitID = 6;		m_inpDoorLock_SolderStation1_Front		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 1 Front");
			iBitID = 7;		m_inpDoorLock_SolderStation2_Front		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 2 Front");
			iBitID = 8;	m_inpDoorLock_SolderStation3_Front		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 3 Front");
			iBitID = 9;	m_inpDoorLock_SolderStation1_Back		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 1 Back");
			iBitID = 10;	m_inpDoorLock_SolderStation2_Back		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 2 Back");
			iBitID = 11;	m_inpDoorLock_SolderStation3_Back		= new InputBit(mInterface.Get_InputPort(1), iBitID, "Door Lock - SolderStat 3 Back");
            iBitID = 12;		m_inpDoorLock_VisionStation				= new InputBit(mInterface.Get_InputPort(4), iBitID, "Door Lock - Vision Station");
			iBitID = 8;		m_inpDoorLock_HotBlower_Front			= new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
			iBitID = 9;		m_inpDoorLock_HotBlower_Back			= new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Back");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front1 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front2 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front3 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front4 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front5 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front6 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front7 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front8 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front9 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");
            //iBitID = 8; m_inpDoorLock_HotBlower_Front10 = new InputBit(mInterface.Get_InputPort(5), iBitID, "Door Lock - Hot Blower Front");

            ////output------------------------------------------------------------------------------------------------------------------
            iBitID = 0;		m_pOutTowerLightRed_Static				= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Red Static");
            iBitID = 1;		m_pOutTowerLightYellow_Static			= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Yellow Static");
            iBitID = 2;		m_pOutTowerLightGreen_Static			= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Green Static");
            iBitID = 3;		m_pOutTowerLightRed_Blink				= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Red Blink");
            iBitID = 5;		m_pOutTowerLightYellow_Blink			= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Yellow Blink");
            iBitID = 6;		m_pOutTowerLightGreen_Blink				= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Green Blink");
            iBitID = 4;		m_pOutTowerLightBuzzer					= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Tower Light Buzzer");

            iBitID = 7;		m_pOutMaintenanceLight					= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Maintenance Light");
            iBitID = 7;		m_pOutMaintenanceLight_VisionStat		= new OutputBit(mInterface.Get_OutputPort(4), iBitID, "Maintenance Light Vision Stat");

			iBitID = 8;		m_pOutDoorLock_SolderStation			= new OutputBit(mInterface.Get_OutputPort(1), iBitID, "Door Lock - Solder Station");
			iBitID = 8;		m_pOutDoorLock_VisionStation			= new OutputBit(mInterface.Get_OutputPort(4), iBitID, "Door Lock - Vision Station");
			iBitID = 8;		m_pOutDoorLock_HotBlowerStation			= new OutputBit(mInterface.Get_OutputPort(5), iBitID, "Door Lock - Hot Blower");


            return true;
        }

        //Init Module//////////////////////////////////////////////////////////////////////////////////////////////////////
        public void Init_Module()
        {
            m_pOutTowerLightRed_Blink.Write(0);
            m_pOutTowerLightRed_Static.Write(0);
            m_pOutTowerLightGreen_Static.Write(0);
            m_pOutTowerLightYellow_Static.Write(1);
            m_pOutTowerLightBuzzer.Write(0);
        }

        //Tower Light - Error///////////////////////////////////////////////////////////////////////////////////
        public void TowerLight_Error()
        {
            m_pOutTowerLightRed_Blink.Write(1);
            m_pOutTowerLightGreen_Static.Write(0);
            m_pOutTowerLightYellow_Static.Write(0);
            m_pOutTowerLightBuzzer.Write(1);
        }
        //Tower Light - Run///////////////////////////////////////////////////////////////////////////////////
        public void TowerLight_Run()
        {
            m_pOutTowerLightRed_Blink.Write(0);
            m_pOutTowerLightGreen_Static.Write(1);
            m_pOutTowerLightYellow_Static.Write(0);
            m_pOutTowerLightBuzzer.Write(0);
        }
        //Tower Light - Standby/////////////////////////////////////////////////////////////////////////////
        public void TowerLight_Standby()
        {
            m_pOutTowerLightRed_Blink.Write(0);
            m_pOutTowerLightGreen_Static.Write(0);
            m_pOutTowerLightYellow_Static.Write(1);
            m_pOutTowerLightBuzzer.Write(0);
        }

        //Turn off buzzer only///////////////////////////////////////////////////////////////////////////////////
        public void TurnOff_Buzzer()
        {
            m_pOutTowerLightBuzzer.Write(0);
        }

        //Turn on buzzer only///////////////////////////////////////////////////////////////////////////////////
        public void TurnOn_Buzzer()
        {
            m_pOutTowerLightBuzzer.Write(1);
        }

        //Safety Door Lock ON///////////////////////////////////////////////////////////////////////////////////
        public void TurnOn_DoorLock()
        {
            m_pOutDoorLock_SolderStation.Write(1);
            m_pOutDoorLock_VisionStation.Write(1);
            m_pOutDoorLock_HotBlowerStation.Write(1);
        }

        //Safety Door Lock OFF///////////////////////////////////////////////////////////////////////////////////
        public void TurnOff_DoorLock()
        {
            m_pOutDoorLock_SolderStation.Write(0);
            m_pOutDoorLock_VisionStation.Write(0);
            m_pOutDoorLock_HotBlowerStation.Write(0);
        }

    }
}
