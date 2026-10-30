using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using HSL_W32;

namespace StaticCoreLib
{
    public class PCI7853
    {
        //variables
        short m_iCardNum = 0;
        bool m_bCardInitOK = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor
        public PCI7853()
        {
        }

        public PCI7853(short iCardNum)
        {
            m_iCardNum = iCardNum;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Destructor / Finalizer
        ~PCI7853()
        {
            De_Init();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Get Card Num
        public int GetCardNum()
        {
            return m_iCardNum;
        }

        //Initialize PCI card//////////////////////////////////////////////////////////////////////////////////////////////
        public bool Initialize()
        {
            if (m_bCardInitOK == true) return true;

            int result = 0;

            result = HSL.HSL_initial(m_iCardNum);
            if (result != 0) return (m_bCardInitOK = false);

            result = HSL.HSL_auto_start(m_iCardNum, 0);
            if (result != 0)
            {
                HSL.HSL_close(m_iCardNum);
                return (m_bCardInitOK = false);
            }

            return (m_bCardInitOK = true);
        }

        public void De_Init()
        {
            if (m_bCardInitOK)
            {
                HSL.HSL_stop(m_iCardNum, 0);
            }
        }
    }
}
