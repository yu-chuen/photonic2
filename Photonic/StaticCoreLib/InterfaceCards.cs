/****************************************************************************************************************************************
 * Interface cards class
 * Desc:    This class is used to set the corresponding hardware interface.
 ****************************************************************************************************************************************/
#define PCI7432_Card
#define PCI7853_Card								//used for PCI7853 and PCI7854
#define PCI7856_Card								//used for PCI7856 card utilizaing APS library for IO and motion control MOTIONNET MNET

#define PCI833X_ETHERCAT_Card

#define BETA_TESTING

////////////////////////////////////////////////////////////////////////////////////////
//for offline and beta naming
#undef PCI7432_Card
//#undef PCI7853_Card								//=>enabled: running using 7853
//#undef PCI833X_ETHERCAT_Card		//=>enabled: running using ECAT PCI833X
#undef PCI7856_Card									//=>enabled: running using PC7856 APS library IO and/OR Motion NET

//#undef Offline_Test										//Note: Removed to machine info class. Set at machine settings m_bOfflineTest
#undef BETA_TESTING
////////////////////////////////////////////////////////////////////////////////////////

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;

namespace StaticCoreLib
{
    public class InterfaceCards
    {
        //Variables
        public PCI7432 m_pPCI7432_Module1 = null;												//PCI Dask (Adlink)				: no slaves, Standalone type IO pci card
		public PCI7853 m_pPCI7853_Module1 = null;												//PCI 7853 IO card (Adlink)	: 1 pci card(master) connect to multiple IO cards (slaves)
		public PCI7856 m_pPCI7856_Module1 = null;												//PCI 7856 IO Card (adlink)

		//HSL Slave Cards
		List<HSL32DI32DO_7853> m_ll_pHSL_32DI32DO_Module = new List<HSL32DI32DO_7853>();

		//APS IO Slave Cards
		List<HSL16DI16DO> m_ll_pHSL16DI16D0_Module = new List<HSL16DI16DO>();

		//ECAT PCI Card
		public APS168_Ethercat_PCIcard m_APS168_Ethercat_PCIcard = null;

		//Edit Slave card amount here
		int m_iTotalSlaveCard = 10;

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Constructors
        public InterfaceCards()
        {
            
        }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        //Initialize all motion and IO cards
        public bool Init_InterfaceCards()
        {
			int iSlaveNum = 0;

//Note: IO Card module using PCI Dask Library---------------------------------------------------------------------------------------------------------
#if PCI7432_Card
            m_pPCI7432_Module1 = new PCI7432(0);   if (m_pPCI7432_Module1 == null) return false;

            if (Global_Var.m_bOfflineTest == false)
            {
                if (!m_pPCI7432_Module1.Initialize()) { MessageBox.Show("Error: Unable to initialize PCI7432 Card"); return false; }
            }
#endif

//Note: IO Card module using HSL Library-----------------------------------------------------------------------------------------------------------------
//Note: Please set Get input and output functions as well
#if PCI7853_Card
			m_pPCI7853_Module1 = new PCI7853(); if (m_pPCI7853_Module1 == null) return false;

			//Create new cards  object: IO Card
			for ( iSlaveNum = 1; iSlaveNum <= m_iTotalSlaveCard; iSlaveNum++ )
				m_ll_pHSL_32DI32DO_Module.Add(new HSL32DI32DO_7853(m_pPCI7853_Module1, iSlaveNum));
			
			//initialize the cards
            if (Global_Var.m_bOfflineTest == false)
            {
                if (m_pPCI7853_Module1.Initialize() == false) { MessageBox.Show(new Form() { TopMost = true }, "Error: Unable to initialize PCI7853 Card"); return false; }
				
				foreach ( HSL32DI32DO_7853 pHSL_32DI32DO_Module in m_ll_pHSL_32DI32DO_Module )
				{
                    //skip card
                    //if (pHSL_32DI32DO_Module.m_iSlaveNum > 3 && pHSL_32DI32DO_Module.m_iSlaveNum < 5 && pHSL_32DI32DO_Module.m_iSlaveNum > 10&&) continue;   //PC3
                    //if (pHSL_32DI32DO_Module.m_iSlaveNum > 12) continue;    //PC2
                    //if (pHSL_32DI32DO_Module.m_iSlaveNum == 4 || pHSL_32DI32DO_Module.m_iSlaveNum == 11) continue;

					//init cards
					if ( pHSL_32DI32DO_Module.Initialize() == false )
                    { MessageBox.Show(new Form() { TopMost = true }, "Error: Unable to initialize IO Card " + pHSL_32DI32DO_Module.m_iSlaveNum.ToString()); return false; }
				}
			}
#endif

//Note: IO Card module using APS Library (Adlink)-----------------------------------------------------------------------------------------------------------------
#if PCI7856_Card
			m_pPCI7856_Module1 = new PCI7856(); if ( m_pPCI7856_Module1 == null ) { MessageBox.Show("PCI7856 Module class is null"); return false; };

			//Create new cards  object: IO Card
			for ( iSlaveNum = 1; iSlaveNum <= m_iTotalSlaveCard; iSlaveNum++ )
				m_ll_pHSL16DI16D0_Module.Add(new HSL16DI16DO(m_pPCI7856_Module1, iSlaveNum));
			
			//initialize the cards
			if ( Global_Var.m_bOfflineTest == false )
			{
				//init pci card
				if ( m_pPCI7856_Module1.Initialize() == false ) { MessageBox.Show("Error: Unable to initialize PCI7856 Card"); return false; }

				//init slaves
				int iCount = 0;
				foreach ( HSL16DI16DO pHSL16DI16D0_Module in m_ll_pHSL16DI16D0_Module )
				{
					iCount++;

					//bypass cards: temporary
					if ( iCount != 1 && iCount != 2 && iCount != 3 && iCount != 7 && iCount != 8 ) continue;

					//init cards
					if ( pHSL16DI16D0_Module.Initialize() == false )
					{ MessageBox.Show("Error: Unable to initialize IO Card " + pHSL16DI16D0_Module.m_iSlaveNum.ToString()); return false; }
				}
			}
#endif


//Note: Motion Card using Ethercat communication-----------------------------------------------------------------------------------------------------
#if PCI833X_ETHERCAT_Card
			m_APS168_Ethercat_PCIcard = new APS168_Ethercat_PCIcard();
			if ( Global_Var.m_bOfflineTest == false )
			{
                if (m_APS168_Ethercat_PCIcard.Initialize_PCI() == false) { MessageBox.Show(new Form() { TopMost = true }, m_APS168_Ethercat_PCIcard.Get_ErrorMessage(m_APS168_Ethercat_PCIcard.Get_ErrorID())); }
			}
#endif

			return true;
        }



        //Get Input Port/////////////////////////////////////////////////////////////////////////////////////////////////
        public MyIPort Get_InputPort(int iSlaveNum = 1)
		{
			MyIPort mIP = null;

			//Blocker
			if ( iSlaveNum < 1 ) { MessageBox.Show("Input Port BitID is less than 1"); return mIP; }

			//PCI7431: PCI_DASK LIB------------------------------------------------------------------------------------------------------------
			#if PCI7432_Card			
			mIP = m_pPCI7432_Module1.m_InputPort; break;
			#endif


			//PCI7853: ---------------------------------------------------------------------------------------------------------------------------------
			#if PCI7853_Card
			if ( iSlaveNum <= m_ll_pHSL_32DI32DO_Module.Count )
				mIP = m_ll_pHSL_32DI32DO_Module[iSlaveNum - 1].m_InputPort;
			#endif 


			//PCI7856: -------------------------------------------------------------------------------------------------------------------------------
			#if PCI7856_Card
			if ( iSlaveNum <= m_ll_pHSL16DI16D0_Module.Count )
				mIP = m_ll_pHSL16DI16D0_Module[iSlaveNum - 1].m_InputPort;
			#endif


			if ( mIP == null ) MessageBox.Show("Input Port is not assigned a library: Slave ID: " + iSlaveNum.ToString());
			return mIP; 
        }

        //Get Output Port////////////////////////////////////////////////////////////////////////////////////////////////
        public MyOPort Get_OutputPort(int iSlaveNum = 1)
		{
			MyOPort mOP = null;

			//Blocker
			if ( iSlaveNum < 1 ) { MessageBox.Show("Output Port BitID is less than 1"); return mOP; }

			//PCI7431: PCI_DASK LIB------------------------------------------------------------------------------------------------------------
			#if PCI7432_Card			
			mOP = m_pPCI7432_Module1.m_OutputPort;
			#endif
						

			//PCI7853: ---------------------------------------------------------------------------------------------------------------------------------
			#if PCI7853_Card
			if ( iSlaveNum <= m_ll_pHSL_32DI32DO_Module.Count )
				mOP = m_ll_pHSL_32DI32DO_Module[iSlaveNum - 1].m_OutputPort;
			#endif
			

			//PCI7856: -------------------------------------------------------------------------------------------------------------------------------
			#if PCI7856_Card
			if ( iSlaveNum <= m_ll_pHSL16DI16D0_Module.Count )
				mOP = m_ll_pHSL16DI16D0_Module[iSlaveNum - 1].m_OutputPort;
			#endif


			if ( mOP == null ) MessageBox.Show("Output Port is not assigned a library: Slave ID: " + iSlaveNum.ToString()); 
			return mOP;
        }
    }

    /*********************************************************************************************************************
	//Global Variables Class
	 *********************************************************************************************************************/
    public static class Global_Var
    {
        public static bool m_bOfflineTest = false;
    }
}