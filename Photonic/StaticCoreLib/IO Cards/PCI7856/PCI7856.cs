/****************************************************************************************************************************************
 * Adlink PCI Card wrapper class: 
 * Desc:    This class is to init the PCI card and establish the object for the motion and IO cards.
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
    public class PCI7856
    {
        //Variables
        public int m_iCardNum = 0;
        public int m_iMNETBusNo = 1;
        public int m_iHSLBusNo = 0;
        public bool m_bCardInitOK = false;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructor and destructor
        public PCI7856()
        {
        }

        public PCI7856(int iCardNum)
        {
            m_iCardNum = iCardNum;
        }

        ~PCI7856()
        {
            De_Init();
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Get Card Num
        public int GetCardNum()
        {
            return m_iCardNum;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Initialize PCI Card
        public bool Initialize()
        {
            if (m_bCardInitOK) return true;

            int DPAC_ID_Bits = 0;

            if (APS168.APS_initial(ref DPAC_ID_Bits, 0) == (int)APS_Define.ERR_NoError)
            {
                //Start HSL IO card
                if (APS168.APS_start_field_bus(m_iCardNum, m_iHSLBusNo, 0) != (int)APS_Define.ERR_NoError)
                {
                    MessageBox.Show("PCI7856 Error: Unable to initialize IO card.");
                    return false;
                }

                //Start MNET Motion card
				//if (APS168.APS_start_field_bus(m_iCardNum, m_iMNETBusNo, 0) != (int)APS_Define.ERR_NoError)
				//{
				//    MessageBox.Show("Error: Unable to initialize motion card.");
				//    return false;
				//}
            }
            else
            {
				MessageBox.Show("PCI7856 Error: Unable to Initialize PCI Card.");
                m_bCardInitOK = false;
                return false;
            }

            //Set Motion Net Parameter
            APS168.APS_set_field_bus_param(m_iCardNum, m_iMNETBusNo, (int)APS_Define.PRF_TRANSFER_RATE, 3); //Set PRF_TRANSFER_RATE: 3 (12M)
            
            m_bCardInitOK = true;

            return true;
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //De-init the card
        public void De_Init()
        {
            if (m_bCardInitOK)
            {
                APS168.APS_stop_field_bus(m_iHSLBusNo, 0);
                APS168.APS_stop_field_bus(m_iMNETBusNo, 0);
                APS168.APS_close();

                m_bCardInitOK = false;
            }
        }


        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Get Init state
        public bool IsInit()
        {
            return m_bCardInitOK;
        }
    }
}
