/****************************************************************************************************************************************
 * Hardware wrapper class: 
 * Desc:    wrapper for the hardware API.
 *          This is the motion card API wrapper.
 *          
 * Note:    This class seems to be a useless class. Communication is done via PCI and not to the motion card.
 ****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using APS168_W32;
using APS_Define_W32;

namespace StaticCoreLib
{
    public class MNET4XMO
    {
        //variables
        int m_iCardNumber = 0;
        int m_iSlaveNumber = 0;
        int m_iBusID = 0;
        int m_iSetID = 0;

        bool m_bInitOK = false;

        public MNET4XMO()
        {
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Initialize MNET Card
        public bool Initialize()
        {
            int iLive = 0;
            int retval = APS168.APS_get_slave_online_status(m_iCardNumber, m_iBusID, m_iSlaveNumber, ref iLive);

            if (iLive != 1)
            {
                m_bInitOK = false;
                return false;
            }
            else m_bInitOK = true;

            return true;
        }
    }
}
