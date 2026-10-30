using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StaticCoreLib
{
    public class PCI7432
    {
        //variables
        short m_iCardNum = 0;
        bool m_bCardInitOK = false;
        public short m_dev = 0;

        public _InputPort_7432 m_InputPort;
        public _OutputPort_7432 m_OutputPort;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public PCI7432()
        {
        }

        public PCI7432(short iCardNum)
        {
            m_iCardNum = iCardNum;

            m_InputPort = new _InputPort_7432(m_iCardNum);
            m_OutputPort = new _OutputPort_7432(m_iCardNum);
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Destructor / Finalizer
        ~PCI7432()
        {
            De_Init();
        }

        //Initialize PCI card//////////////////////////////////////////////////////////////////////////////////////////////
        public bool Initialize()
        {
            if (m_bCardInitOK == true) return true;

            m_dev = DASK.Register_Card(DASK.PCI_7432, (ushort)m_iCardNum);
            if (m_dev < 0)
            {

                return (m_bCardInitOK = false);
            }
            
            return (m_bCardInitOK = true);
        }

        public void De_Init()
        {
            short ret;
            if (m_bCardInitOK)
            {
                ret = DASK.Release_Card((ushort)m_dev);
            }
        }


    }

    /**********************************************************************
     * 7432 Input Port
     **********************************************************************/
    public class _InputPort_7432 : MyIPort
    {
        private short m_iCardNumber;

        public _InputPort_7432(int pCardNumber)
        {
            m_iCardNumber = (short)pCardNumber;

            m_iNumberOfBits = 32;
            m_bInit = true;
        }



        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            ushort iTempLogic = 0;
            short ret = DASK.DI_ReadLine((ushort)m_iCardNumber, 0, (ushort)iBitIndex, out iTempLogic);
            if (ret < 0)
            {
                String strMsg = "Error: PCI 7432 Input failed to Read.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
                //MessageBox.Show(strMsg);

                return false;
            }

            iReadLogic = (int)iTempLogic; //(((iTempLogic >> iBitIndex) & 0x01) == 1) ? 1 : 0;

            return true;
        }
    }

    /**********************************************************************
     * 7432 Output port
     **********************************************************************/
    public class _OutputPort_7432 : MyOPort
    {
        private short m_iCardNumber;

        public _OutputPort_7432(int pCardNumber)
        {
            m_iCardNumber = (short)pCardNumber;

            m_iNumberOfBits = 32;
            m_bInit = true;
        }



        public override bool Read(int iBitIndex, ref int iReadLogic)
        {
            ushort iTempLogic;

            short ret = DASK.DO_ReadLine((ushort)m_iCardNumber, 0, (ushort)iBitIndex, out iTempLogic);
            if (ret < 0)
            {
                String strMsg = "Error: PCi 7432 Output failed to Read.\r\n" + "Card: " + m_iCardNumber.ToString()
                    + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
                //MessageBox.Show(strMsg);

                return false;
            }            


            iReadLogic = (iTempLogic == 1) ? 1 : 0;

            return true;
        }

        public override bool Write(int iBitIndex, int iLogic)
        {

            short ret = DASK.DO_WriteLine((ushort)m_iCardNumber, 0, (ushort)iBitIndex, (ushort)iLogic);
            if (ret < 0)
            {
                String strMsg = "Error: PCI 7432 Output failed to Write.\r\n" + "Card: " + m_iCardNumber.ToString()
                     + "Bit Num: " + iBitIndex.ToString() + "Error Code: " + ret.ToString();
                //MessageBox.Show(strMsg);

                return false;
            }

            return true;
        }
    }
   
}
