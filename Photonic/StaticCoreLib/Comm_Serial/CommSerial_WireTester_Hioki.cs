using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading;
using System.Diagnostics;
using System.Windows.Forms;

namespace StaticCoreLib
{
	public class CommSerial_WireTester_Hioki
	{
		//variables
		public Comm_Serial m_ComPort;
		public CommSerial_WireTester_Hioki_Settings m_Settings = new CommSerial_WireTester_Hioki_Settings();
        SeqMachineControl m_SeqMachineCtrl;
        Data_UnitTempStorage m_DataUnit;

		//IO
		public InputBit m_inpEndOfMeasurement;			//indicates that the measurement is end / zero adjustment end, and the results has been finalized
		public InputBit m_inpINDEX;								//indicates that the A/D conversion is completed
		public InputBit m_inpError;
		public InputBit m_inpResult_High;
		public InputBit m_inpResult_Pass;
		public InputBit m_inpResult_Low;

		public OutputBit m_outTrigger;
		public OutputBit m_outZeroAdjustment;
		public OutputBit m_outKeyLock;

		//vars
		public String m_strOhmReading = "0";
		public String m_strTemperatureReading = "0";
		public String m_strFilePath = "";
		public String m_strLabel = "Wire_Tester";
		public String m_strDebug = "";
		String m_strErrorMsg = "";
		int m_iErrorID = 0;


		//Constructor///////////////////////////////////////////////////////////////////////////////////////////////////
		public CommSerial_WireTester_Hioki(String strLabel, String strFilePath, SeqMachineControl mSeqMachineCtrl)
		{
            m_SeqMachineCtrl = mSeqMachineCtrl;
            m_DataUnit = m_SeqMachineCtrl.m_DataUnit;

			m_strLabel		= strLabel;
			m_strFilePath	= strFilePath;

			m_ComPort = new Comm_Serial(m_Settings.m_Settings);
			From_Registry();
		}


		//To Register///////////////////////////////////////////////////////////////////////////////////////////////////
		public void To_Registry()
		{
			String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
			XMLSerializer.Serialize_Func<CommSerial_WireTester_Hioki_Settings>(m_Settings, strFulFilePath);
		}

		//From Register/////////////////////////////////////////////////////////////////////////////////////////////
		public void From_Registry()
		{
			String strFulFilePath = m_strFilePath + m_strLabel + ".xml";
			m_Settings = XMLSerializer.Deserialize_Func<CommSerial_WireTester_Hioki_Settings>(strFulFilePath);

			//set to com port settings
			m_ComPort.Set_CommSettings(m_Settings.m_Settings);
		}

        public void Disconnect()
        {
            //Disconnect from Serial Port
            m_ComPort.Close_Port();
        }


		//Supporting function////////////////////////////////////////////////////////////////////////////////////
        private bool Wait_DeviceEndOfMeasurement()
        {
            bool bRet = true;
            Stopwatch mSt = new Stopwatch();

            //wait measurement end
            mSt.Start();
            while (true)
            {
                //measurement done
                if (m_inpEndOfMeasurement.Read() == 1) { break; }

                //error occur
                if (m_inpError.Read() == 1)
                {
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.WIRETESTER_ERROR;
                    bRet = false;
                    break;
                }

                //time out
                if (mSt.ElapsedMilliseconds > 8000)
                {
                    m_iErrorID = (int)ErrorTable.en_ErrorCode.WIRETESTER_TIMEOUT;
                    bRet = false;
                    break;
                }

                Thread.Sleep(10);
            }
            mSt.Stop();

            //error occur: just in case
            if (m_inpError.Read() == 1)
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.WIRETESTER_ERROR;
                bRet = false;
            }

            return bRet;
        }




		//Read////////////////////////////////////////////////////////////////////////////////////////////////////////////
		//Note: Issue command (ex: "READ?") to reader will disable the external trigger. The 2nd time calling to this command will result in WIRETESTER_TIMEOUT error.
		//			Thus, change back to IO control which is better. The Hioki however have to be set Data Out .
        public bool Read_Resistance(ref String strReadData)
        {
            bool bRet = true;
            Stopwatch mSt = new Stopwatch();
            String strData = "";
            int iErrorCounterMax = 5;               //max times error input bit is high
            int iErrorCounter = 0;                  //count the times the error input bit is high

            //reset return data to empty
            strReadData = "";

            //Disconnect from Serial Port first to close existing connection
            m_ComPort.Close_Port();

            //Connect to serial port
            if (m_ComPort.Open_Port() == true)
            {
                //delay to wait for device to down all signals
                Thread.Sleep(1000);

                //wait measurement end
                if (Wait_DeviceEndOfMeasurement() == false) { bRet = false; }

                //Read from ohm meter
                if (bRet == true)
                {
                    //reset
                    m_strOhmReading = "0";

                    //trigger to set COM port output
                    m_outTrigger.Write(1);

                    //error occur
                    if (m_inpError.Read() == 1)
                    {
                        //counter
                        if (iErrorCounter < iErrorCounterMax)
                        {
                            iErrorCounter++;
                            Thread.Sleep(100);      //delay in case the error bit is just a flicker
                        }
                        else
                        {
                            //set error and return false
                            m_iErrorID = (int)ErrorTable.en_ErrorCode.WIRETESTER_ERROR;
                            bRet = false;
                            goto End;
                        }
                    }

                    if (m_SeqMachineCtrl.m_DataUnit.MachineTypeNum_i == 1)
                    {
                        //Send read resistance command
                        if (m_ComPort.Write("") == false)
                        {
                            m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
                            bRet = false;
                        }
                    }

                    //read from com port
                    if (m_ComPort.Read(ref strData) == false)
                    {
                        m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
                        bRet = false;
                        goto End;
                    }

                    //turn off trigger
                    m_outTrigger.Write(0);
                }
            }
            else
            {
                bRet = false;
                m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
                goto End;
            }

            //Disconnect from Serial Port
            m_ComPort.Close_Port();

            //set data
            strReadData = strData.Replace("\r", "").Replace("\n", "").Trim();

            //Decode results
            if (bRet == true)
            {
                String strOhm = "";

                //get ohm reading
                strOhm = DecodeResponse(strReadData);

                //return error
                if (strOhm.ToLower() == "error") { bRet = false; }
                else { m_strOhmReading = strOhm; }
            }

        End:
            //turn off trigger
            m_outTrigger.Write(0);

            return bRet;
        }

		//Read Temperature///////////////////////////////////////////////////////////////////////////////////////
        public bool Read_Temperature(ref String strReadData)
        {
            bool bRet = true;
            String strData = "";

            //Connect to serial port
            if (m_ComPort.Open_Port() == true)
            {
                //Send read temperature command
                if (m_ComPort.Write("FETCh:TEMPerature?") == false)
                {
                    m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
                    bRet = false;
                }

                //read from com port
                if (m_ComPort.Read(ref strData) == false)
                {
                    m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
                    bRet = false;
                }
            }
            else
            {
                bRet = false;
                m_ComPort.Get_Error(ref m_iErrorID, ref m_strErrorMsg);
            }

            //Disconnect from Serial Port
            m_ComPort.Close_Port();

            //set data
            strReadData = strData.Replace("\r", "").Replace("\n", "").Trim();

            //reset variable
            m_strTemperatureReading = "0";

            //Decode results
            if (bRet == true)
                m_strTemperatureReading = DecodeResponse(strReadData);

            return bRet;
        }



		//Zero  Adjustment / Reset////////////////////////////////////////////////////////////////////////////////////////
		public bool ZeroAdjustment()
		{
			bool bRet = true;
			Stopwatch mSt = new Stopwatch();

			//trigger zero adjustment bit
			m_outZeroAdjustment.Write(1);
			Thread.Sleep(10);
			m_outZeroAdjustment.Write(0);

			//wait adjustment end
			if ( Wait_DeviceEndOfMeasurement() == false ) bRet = false;

			return bRet;
		}

		//Decode Response///////////////////////////////////////////////////////////////////////////////////////////////////
        public String DecodeResponse(String strResponse)
        {
            String strOhmResult = "";
            int iMetricPrefix = 0;
            int i_MidPos_E = 0;

            //get mid position
            i_MidPos_E = strResponse.ToUpper().IndexOf("E");

            try
            {
                //get metric prefix
                iMetricPrefix = int.Parse(strResponse.Substring(i_MidPos_E + 1, (strResponse.Length - i_MidPos_E - 1)));

                //get OhmResults
                //strOhmResult = strResponse.Substring(0, i_MidPos_E);
                strOhmResult = double.Parse(strResponse.Substring(0, i_MidPos_E)).ToString();
            }
            catch
            {
                m_iErrorID = (int)ErrorTable.en_ErrorCode.WIRETESTER_ERROR;
                m_strErrorMsg = "Decoding string: " + strResponse;
                return "error";
            }

            //Get Metric prefix
            switch (iMetricPrefix)
            {
                case 0: break;
                case 1: strOhmResult += "da"; break;
                case 2: strOhmResult += "h"; break;
                case 3: strOhmResult += "k"; break;
                case 6: strOhmResult += "M"; break;
                case 9: strOhmResult += "G"; break;
                case 12: strOhmResult += "T"; break;
                //case 18: strOhmResult = "Open"; break;

                default: strOhmResult = "Open"; break;
            }

            return strOhmResult;
        }



        //Get Settings///////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public bool Enable { set { m_Settings.m_bEnable = value; } get { return m_Settings.m_bEnable; } }



		//Get Error////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
		public int Get_ErrorID()
		{
			int iErrorID = m_iErrorID;
			m_iErrorID = 0;
			return iErrorID;
		}

		public String Get_ErrorMessage()
		{
			String strErrorMsg = m_strErrorMsg;
			m_strErrorMsg = "";
			return strErrorMsg;
		}
	}


	//Note: Settings for the wire tester Hioki class. This class is created incase of additional settings that this ready is required
	[Serializable]
	public class CommSerial_WireTester_Hioki_Settings
	{
        public bool m_bEnable = true;
		public Comm_Serial_Settings m_Settings = new Comm_Serial_Settings();
	}

}
