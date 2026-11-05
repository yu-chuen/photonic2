/****************************************************************************************************************************************
 * Adlink 7853 IO Wrapper class: 
 * Used in APAM
 * Desc:    This class is to send command to the Adlink layer or library. This class is also used to initialize the card and interfaces.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using HSL_W32;
using HSL_Motion_W32;

namespace StaticCoreLib
{
    /**********************************************************************
    * Wrapper class for HSL 7853 IO (Input & Output) Card Controller
    **********************************************************************/    
    public class HSL32DI32DO_7853
    {
        //variables
        //Variables
        public int m_iCardNum = 0;
        public int m_iSlaveNum = 0;
        public int m_iBusNum = 0;
        public int m_iSetID = 0;

        public _InputPort_7853 m_InputPort;
        public _OutputPort_7853 m_OutputPort;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public HSL32DI32DO_7853(PCI7853 pMasterCard, int iSlaveNum)
        {
            m_iCardNum = pMasterCard.GetCardNum();
            m_iSlaveNum = iSlaveNum;
            m_iSetID = 0;
            m_iBusNum = 0;

            m_InputPort = new _InputPort_7853(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
            m_OutputPort = new _OutputPort_7853(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Destructor / Finalizer
        ~HSL32DI32DO_7853()
        {
            m_InputPort = null;
            m_OutputPort = null;
        }

        public bool Initialize()
        {
            if (m_iCardNum < 0) return false;
            
            if (m_InputPort.Initialize() == false || m_OutputPort.Initialize() == false)
                return false;
            
            return true;
        }
    }

    /**********************************************************************
     * 7853 Input Port
     **********************************************************************/
    public class _InputPort_7853 : MyIPort
    {
        private short m_iSlaveNumber;
        private short m_iCardNumber;
        private short m_iSetID;
        private short m_iBusNo;

        public _InputPort_7853(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
        {
            m_iCardNumber   = (short)pCardNumber;
            m_iSetID        = (short)pSetID;
            m_iSlaveNumber  = (short)pSlaveNumber;
            m_iBusNo        = (short)pBusNo;

            m_iNumberOfBits = 32;
        }

        public override bool Initialize()
        {
            short iLive = 0;
            short ret = HSL.HSL_slave_live(m_iCardNumber, m_iSetID, m_iSlaveNumber, ref iLive);
            if (iLive != 1)
            {
                String strMsg = "Error: HSL InputPort Failed to Init.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Error Code: " + ret.ToString();
                if (Global_Var.m_bOfflineTest == false) MessageBox.Show(strMsg);
                return false;
            }

            m_bInit = true;

            return true;
        }

        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            ushort iTempLogic = 0;

            short ret = HSL.HSL_D_read_channel_input(m_iCardNumber, m_iSetID, m_iSlaveNumber, (short)iBitIndex, ref iTempLogic);

            if (ret != 0)
            {
                String strMsg = "Error: HSL InputPort card failed to Read.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
				if ( Global_Var.m_bOfflineTest == false ) MessageBox.Show(strMsg);

                return false;
            }

            iReadLogic = (int)iTempLogic; //(((iTempLogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;
            
            return true;
        }

        public override int Get_SlaveID() { return m_iSlaveNumber; }
    }

    /**********************************************************************
     * 7853 Output port
     **********************************************************************/
    public class _OutputPort_7853 : MyOPort
    {
        private short m_iSlaveNumber;
        private short m_iCardNumber;
        private short m_iSetID;
        private short m_iBusNo;

        public _OutputPort_7853(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
        {
            m_iCardNumber		= (short) pCardNumber;
            m_iSetID					= (short) pSetID;
            m_iSlaveNumber		= (short) pSlaveNumber;
            m_iBusNo				= (short) pBusNo;

            m_iNumberOfBits = 32;
        }

        public override bool Initialize()
        {
            short iLive = 0;
            short ret = HSL.HSL_slave_live(m_iCardNumber, m_iSetID, m_iSlaveNumber, ref iLive);
            if (iLive != 1)
            {
                String strMsg = "Error: HSL OutputPort Failed to Init.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Error Code: " + ret.ToString();
				if ( Global_Var.m_bOfflineTest == false ) MessageBox.Show(strMsg);
                return false;
            }

            m_bInit = true;

            return true;
        }

        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            uint iTempLogic = 0;

            //bypass if offline version
            if (Global_Var.m_bOfflineTest == true) return true;

            //read from output port
            short ret = HSL.HSL_D_read_output(m_iCardNumber, m_iSetID, m_iSlaveNumber, ref iTempLogic);
            if (ret != 0)
            {
                String strMsg = "Error: HSL OutputPort card failed to Read.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
				if ( Global_Var.m_bOfflineTest == false ) MessageBox.Show(strMsg);

                return false;
            }

            iReadLogic = (((iTempLogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;

            return true;
        }

        public override bool Write(int iBitIndex, int iLogic)
        {
            short ret = HSL.HSL_D_write_channel_output(m_iCardNumber, m_iSetID, m_iSlaveNumber, (short)iBitIndex, (ushort)iLogic);
            if (ret != 0)
            {
                String strMsg = "Error: HSL OutputPort card failed to Write.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
				if ( Global_Var.m_bOfflineTest == false ) MessageBox.Show(strMsg);

                return false;
            }
            
            return true;
        }

        public override int Get_SlaveID() { return m_iSlaveNumber; }
    }
}
