/****************************************************************************************************************************************
 * Input Bit class: 
 * Desc:    Functional class for reading from a single input bit.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace StaticCoreLib
{
    public class InputBit
    {
        //Variables
        protected MyIPort m_IPort;
        public int m_iBitID;

        public String Label { get; set; }

        //////////////////////////////////////////////////////////////////////
        //constructor. Set MyIPort class
        public InputBit()
        {
            m_iBitID = 0;
            m_IPort = null;
        }

        public InputBit(MyIPort pIPort, int iBitID, String strLabel)
        {
            m_IPort = pIPort;
            m_iBitID = iBitID;
            Label = strLabel;
        }

        //////////////////////////////////////////////////////////////////////
        //Read
        public int Read()
        {
            int iTempValue = 0;

            if (m_IPort == null)
            {
                MessageBox.Show("Input Port is null");
                iTempValue = -1;
            }
            else if (!m_IPort.ReadBit(m_iBitID, ref iTempValue))
            {
                iTempValue = -1;
            }

            return iTempValue;
        }

        //////////////////////////////////////////////////////////////////////
        //Set MyIPort object
        public void Set_InputPort(MyIPort pIPort)
        {
            m_IPort = pIPort;
        }

        //Get Slave ID///////////////////////////////////////////
        public int Get_SlaveID() { return m_IPort.Get_SlaveID(); }

    }
}
