/****************************************************************************************************************************************
 * Adlink IO Wrapper class: 
 * Desc:    This class is to send command to the Adlink layer or library. This class is also used to initialize the card and interfaces.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

using APS_Define_W32;
using APS168_W32;

namespace StaticCoreLib
{
    /**********************************************************************
     * Wrapper class for HSL IO (Input & Output) Card Controller
     **********************************************************************/
    public class EtherCat_8338
    {
        //Variables
        int m_iCardNum = 0;
        int m_iSlaveNum = 0;
        int m_iBusNum = 0;
        int m_iSetID = 0;

        public _EtherCat_8338_InputPort m_InputPort;
        public _EtherCat_8338_OutputPort m_OutputPort;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public EtherCat_8338(PCI7856 pMasterCard, int iSlaveNum)
        {
            m_iCardNum  = pMasterCard.GetCardNum();
            m_iSlaveNum = iSlaveNum;
            m_iBusNum   = 0;
            m_iSetID = 0;

            m_InputPort = new _EtherCat_8338_InputPort(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
            m_OutputPort = new _EtherCat_8338_OutputPort(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Destructor / Finalizer
		~EtherCat_8338()
        {
            m_InputPort = null;
            m_OutputPort = null;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Init
        public bool Initialize()
        {
            if (m_iCardNum < 0)
                return false;

            if (m_InputPort.Initialize() == true && m_OutputPort.Initialize() == true)
                return true;

            return false;
        }
    }

    /**********************************************************************
     * Interface class for Input Port
     **********************************************************************/
    public class _EtherCat_8338_InputPort : MyIPort
    {
        private int m_iSlaveNumber;
        private int m_iCardNumber;
        private int m_iSetID;
        private int m_iBusNo;

        public _EtherCat_8338_InputPort(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
        {
            m_iCardNumber   = pCardNumber;
            m_iSetID        = pSetID;
            m_iSlaveNumber  = pSlaveNumber;
            m_iBusNo        = pBusNo;

            m_iNumberOfBits = 16;
        }

        public override bool Initialize()
        { 
            int iLive = 0;
            int ret = APS168.APS_get_slave_online_status(m_iCardNumber, m_iBusNo, m_iSlaveNumber, ref iLive);
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS InputPort Failed to Init.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);
                return false;
            }

            m_bInit = true;

            return true;
        }

        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            int iTemplogic = 0;
            int ret = APS168.APS_field_bus_d_get_input(m_iCardNumber, m_iBusNo, m_iSlaveNumber, ref iTemplogic); 
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS InputPort card failed to Read.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);

                return false;
            }

            iReadLogic = (((iTemplogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;
            
            return true;
        }
    }

    /**********************************************************************
     * Interface class for Output port
     **********************************************************************/
    public class _EtherCat_8338_OutputPort : MyOPort
    {
        private int m_iSlaveNumber;
        private int m_iCardNumber;
        private int m_iSetID;
        private int m_iBusNo;

        public _EtherCat_8338_OutputPort(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
        {
            m_iCardNumber   = pCardNumber;
            m_iSetID        = pSetID;
            m_iSlaveNumber  = pSlaveNumber;
            m_iBusNo        = pBusNo;

            m_iNumberOfBits = 16;
        }

        public override bool Initialize()
        {
            int iLive = 0;
            int ret = APS168.APS_get_slave_online_status(m_iCardNumber, m_iBusNo, m_iSlaveNumber, ref iLive); 
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS OutputPort Failed to Init.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);
                return false;
            }

            m_bInit = true;

            return true;
        }

        public override bool Write(int iBitIndex, int iLogic)
        {
            //read from output port to determine if the bit is set
            int iTempLogic = 0;
            int ret = APS168.APS_field_bus_d_get_output(m_iCardNumber, m_iBusNo, m_iSlaveNumber, ref iTempLogic); 
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS OutputPort Failed to get bit state.\r\n" + "Card: " + m_iCardNumber.ToString() 
                    + "Slave Num: " + m_iSlaveNumber.ToString()  + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);
                return false;
            }
            int iReadBit = (((iTempLogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;
            if (iReadBit == iLogic) return true;

            //if bit is different, set the bit
            int iBitmask = 1 << iBitIndex;
            if (iReadBit == 0 && iLogic == 1)
                iTempLogic = iTempLogic | iBitmask;
            if (iReadBit == 1 && iLogic == 0)
                iTempLogic = iTempLogic & (~iLogic);

            ret = APS168.APS_field_bus_d_set_output(m_iCardNumber, m_iBusNo, m_iSlaveNumber, iTempLogic); 
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS OutputPort Failed to Write.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Bit: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);
                return false;
            }
            
            return true;
        }

        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            int iTempLogic = 0;
            int ret = APS168.APS_field_bus_d_get_output(m_iCardNumber, m_iBusNo, m_iSlaveNumber, ref iTempLogic);
            if (ret != (int)APS_Define.ERR_NoError)
            {
                String strMsg = "Error: APS OutputPort Failed to get bit state.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Slave Num: " + m_iSlaveNumber.ToString() + "Error Code: " + ret.ToString();
                MessageBox.Show(strMsg);
                return false;
            }

            iReadLogic = (((iTempLogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;

            return true;
        }
    }
}
