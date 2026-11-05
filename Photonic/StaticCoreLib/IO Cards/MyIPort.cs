/****************************************************************************************************************************************
 * MyIPort class: 
 * Desc:    Interface to the Hardware wrapper API class. This class calls the read function from the IO card.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class MyIPort
    {
        //Variables
        public int m_iNumberOfBits = 0;
        public bool m_bInit = false;    //

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public MyIPort()
        {
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //to input class
        public bool ReadBit(int iBitIndex, ref int Value)
        {
            if (iBitIndex >= m_iNumberOfBits || iBitIndex < 0 || m_bInit == false)
                return false;
            
            if (this.Read(iBitIndex, ref Value))
            {
                return true;
            }
            return false;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Init the hardware wrapper API class
        public virtual bool Initialize()
        {
            return true;
        }

        ///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Virtual function to link to hardware wrapper class
        public virtual bool Read(int iBitIndex, ref int iReadLogic)
        {
            return true;
        }

        //Get Bit ID for display////////////////////////////////////////////////////////////////////////////////////
        public virtual int Get_SlaveID() { return 0; }

    }
}
