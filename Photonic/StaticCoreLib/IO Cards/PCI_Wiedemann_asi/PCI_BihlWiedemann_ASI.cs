using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AsiDrvClass;

namespace StaticCoreLib
{
	/************************************************************************************************************************
	 * Wrapper class to ASI pci card by Bihl Wiedemann
	 ************************************************************************************************************************/
	public class PCI_BihlWiedemann_ASI
	{
		//variables
		int m_iCardNum = 0;
		int m_iSlaveNum = 0;
		int m_iBusNum = 0;
		int m_iSetID = 0;

		public _InputPort_WiedemannASI m_InputPort;
		public _OutputPort_WiedemannASI m_OutputPort;

		AsiDrvClass.MAASi MAASi1;

		//constructor///////////////////////////////////////////////////////////////////////////////////////////////////
		public PCI_BihlWiedemann_ASI()
		{

			//MAASi1 = new AsiDrvClass.MAASi();

			m_InputPort = new _InputPort_WiedemannASI(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
			m_OutputPort = new _OutputPort_WiedemannASI(m_iCardNum, m_iSetID, m_iBusNum, m_iSlaveNum);
		}

		//Destructor////////////////////////////////////////////////////////////////////////////////////////////////////
		~PCI_BihlWiedemann_ASI()
		{
			m_InputPort			= null;
			m_OutputPort		= null;
		}

		//Start Up
		public void start()
		{
			MAASi1 = new MAASi();
		}

		//Initialize PCI Card////////////////////////////////////////////////////////////////////////////////////////
		public bool Initialize()
		{
			//Init PCI Card here

			//Init input and output class
			if ( m_InputPort.Initialize() == false || m_OutputPort.Initialize() == false )
				return false;

			return true;
		}

	}




/**********************************************************************
 * 7853 Input Port
 **********************************************************************/
	public class _InputPort_WiedemannASI : MyIPort
	{
		//variables
		private short m_iSlaveNumber;
		private short m_iCardNumber;
		private short m_iSetID;
		private short m_iBusNo;

		//constructor///////////////////////////////////////////////////////////////////////////////////////////////////////
		public _InputPort_WiedemannASI(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
		{
			m_iCardNumber = (short)pCardNumber;
			m_iSetID = (short)pSetID;
			m_iSlaveNumber = (short)pSlaveNumber;
			m_iBusNo = (short)pBusNo;

			m_iNumberOfBits = 1000;
		}

		//Input Port: Init///////////////////////////////////////////////////////////////////////////////////////////////////
		public override bool Initialize()
		{

			m_bInit = true;
			return true;
		}

		//Input Bit: Read/////////////////////////////////////////////////////////////////////////////////////////////////
		public override bool Read( int iBitIndex, ref int iReadLogic )
		{
			return true;
		}

		public override int Get_SlaveID() { return m_iSlaveNumber; }
	}



/**********************************************************************
* 7853 Output Port
**********************************************************************/
	public class _OutputPort_WiedemannASI : MyOPort
	{
		//variables
		private short m_iSlaveNumber;
		private short m_iCardNumber;
		private short m_iSetID;
		private short m_iBusNo;

		//constructor///////////////////////////////////////////////////////////////////////////////////////////////////////
		public _OutputPort_WiedemannASI(int pCardNumber, int pSetID, int pBusNo, int pSlaveNumber)
		{
			m_iCardNumber		= (short)pCardNumber;
			m_iSetID					= (short)pSetID;
			m_iSlaveNumber		= (short)pSlaveNumber;
			m_iBusNo				= (short)pBusNo;

			m_iNumberOfBits = 1000;
		}

		//Output Port: Init///////////////////////////////////////////////////////////////////////////////////////////////////
		public override bool Initialize()
		{

			m_bInit = true;
			return true;
		}

		//Output Bit: Write////////////////////////////////////////////////////////////////////////////////////////////////
		public override bool Write( int iBitIndex, int iLogic )
		{
			return true;
		}

		//Read////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public override bool Read( int iBitIndex, ref int iReadLogic )
		{
			return true;
		}

		public override int Get_SlaveID() { return m_iSlaveNumber; }
	}


}
