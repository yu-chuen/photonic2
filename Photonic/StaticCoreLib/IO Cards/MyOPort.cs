/****************************************************************************************************************************************
 * MyOPort class: 
 * Desc:    Interface to the Hardware wrapper API class. This class calls the write function from the IO card.
 *          This class also can be used to read the output bit of the IO Card.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class MyOPort
    {
        //Variables
        public int m_iNumberOfBits = 0;
        public bool m_bInit = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public MyOPort()
        {
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //to output class
        public bool WriteBit(int iBitIndex, int Value)
        {
            int iReadLogic = 0;

            if (iBitIndex < m_iNumberOfBits || iBitIndex >= 0)
            {
                if (m_bInit && Read(iBitIndex, ref iReadLogic))
                {
                    if (Write(iBitIndex, Value))
                        return true;
                }
            }
            
            return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Init the hardware wrapper API class
        public virtual bool Initialize()
        {
            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //virtual function linking to hardware class wrapper
        public virtual bool Write(int iBitIndex, int iLogic)
        {
            return true;
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //virtual function read from output port
        public virtual bool Read(int iBitIndex, ref int iReadLogic)
        {
            return true;
        }

        //Get Bit ID for display////////////////////////////////////////////////////////////////////////////////////
        public virtual int Get_SlaveID() { return 0; }

    }
}
