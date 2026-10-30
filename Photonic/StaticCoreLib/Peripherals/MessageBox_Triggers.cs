/**************************************************************************************************************************************************************************
 * Desc: This class is used to trigger the message box which is display at the GUI level.
 **************************************************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StaticCoreLib
{
    public class MessageBox_Triggers
    {
        //variables
        private readonly object m_lock_FlagMsgBoxWarning = new object();                               //Lock: Warning message box
        private readonly object m_lock_FlagMsgBoxError = new object();                                    //Lock: Error Message Box trigger
        private readonly object m_lock_FlagRequestMsgbox = new object();                                //Lock: Request message box lock
        private readonly object m_lock_Flag_RequestState = new object();                                  //Lock: Request MessageBox return input from user, OK or No
        private readonly object m_lock_FlagMsgBox_Modeless = new object();                           //Lock: Modeless Message Box
		private readonly object m_lock_Flag_RequestMsgBox3Input = new object();                  //Lock: Message Box with 3 input
		private readonly object m_lock_iRequestState_3input = new object();							   //Lock : Message box 3 input selection

		private readonly object m_lock_strErrorMessage = new object();
		private readonly object m_lock_strWarningMessage = new object();
		private readonly object m_lock_strRequestMessage = new object();
		private readonly object m_lock_strRequestMsgBox3Input = new object();
		private readonly object m_lock_strModelessMsg = new object();                                       //Lock: Modeless message

        String m_strWarningMessage = "";                                   //for GUI do display warning message. no stop process run
        String m_strErrorMessage = "";                                         //for GUI do display error message. Stop process run
        String m_strRequestMessage = "";                                   //for GUI do display request message. 
        String m_strModelessMsg = "";                                         //for GUI do display modeless message> Msg box that will appear until a flag trigger it down
		String m_strRequestMsgBox3Input = "";                           //for GUI do display request message with 3 inputs
		
		bool m_bFlag_MsgBoxWarning = false;
        bool m_bFlag_MsgBoxError = false;
        bool m_bFlag_RequestMsgbox = false;
		bool m_bFlag_MsgBox_Modeless = false;
		bool m_bFlag_RequestMsgBox3Input = false;
		
		int m_bFlag_RequestMsgBoxState = 0;
		int m_iFlag_RequestMsgBoxState_3Input = 0;

        public bool Flag_MsgBoxWarning
        {
            set { lock (m_lock_FlagMsgBoxWarning) { m_bFlag_MsgBoxWarning = value; } }
            get { lock (m_lock_FlagMsgBoxWarning) { return m_bFlag_MsgBoxWarning; } }
        }                                              //Flag: Display Warning messagebox due to Read Code fail
        public bool Flag_MsgBoxError
        {
            set { lock (m_lock_FlagMsgBoxError) { m_bFlag_MsgBoxError = value; } }
            get { lock (m_lock_FlagMsgBoxError) { return m_bFlag_MsgBoxError; } }
        }                                                   //Flag: DisplayError messagebox
        public bool Flag_RequestMsgbox
        {
            set { lock (m_lock_FlagRequestMsgbox) { m_bFlag_RequestMsgbox = value; } }
            get { lock (m_lock_FlagRequestMsgbox) { return m_bFlag_RequestMsgbox; } }
        }                                              //Flag: Request Message box. Asking for user input or command
        public bool Flag_MsgBoxModeless
        {
            set { lock (m_lock_FlagMsgBox_Modeless) { m_bFlag_MsgBox_Modeless = value; } }
            get { lock (m_lock_FlagMsgBox_Modeless) { return m_bFlag_MsgBox_Modeless; } }
        }                                           //Flag: Modeless Message box.
		public bool Flag_RequestMsgBox3Input
		{
			set { lock (m_lock_Flag_RequestMsgBox3Input) { m_bFlag_RequestMsgBox3Input = value; } }
			get { lock (m_lock_Flag_RequestMsgBox3Input) { return m_bFlag_RequestMsgBox3Input; } }
		}									  //Flag: Request message box with 3 inputs
        public int Flag_RequestState
        {
            set { lock (m_lock_Flag_RequestState) { m_bFlag_RequestMsgBoxState = value; } }
            get { lock (m_lock_Flag_RequestState) { return m_bFlag_RequestMsgBoxState; } }
        }                                                      //Selection: Request Message box return status. yes or no
		public int RequestState_3input_i
		{
			set{ lock(m_lock_iRequestState_3input){ m_iFlag_RequestMsgBoxState_3Input = value; } }
			get { lock (m_lock_iRequestState_3input) { return m_iFlag_RequestMsgBoxState_3Input; } }
		}													//Selection: 3 input msg box result
        public String ModelessMsg_str
        {
            set { lock (m_lock_strModelessMsg) { m_strModelessMsg = value; } }
            get { lock (m_lock_strModelessMsg) { return m_strModelessMsg; } }
        }                                                   //String: pass striing to the modeless message box

		public String MessageError_str { set { lock ( m_lock_strErrorMessage ) { m_strErrorMessage = value; } } get { lock ( m_lock_strErrorMessage ) { return m_strErrorMessage; } } }
		public String MessageWarning_str { set { lock ( m_lock_strWarningMessage ) { m_strWarningMessage = value; } } get { lock ( m_lock_strWarningMessage ) { return m_strWarningMessage; } } }
		public String MessageRequest_str { set { lock ( m_lock_strRequestMessage ) { m_strRequestMessage = value; } } get { lock ( m_lock_strRequestMessage ) { return m_strRequestMessage; } } }
		public String MessageRequest3Input_str { set { lock ( m_lock_strRequestMsgBox3Input ) { m_strRequestMsgBox3Input = value; } } get { lock ( m_lock_strRequestMsgBox3Input ) { return m_strRequestMsgBox3Input; } } }


        //Constructor////////////////////////////////////////////////////////////////////////////////////
        public MessageBox_Triggers()
        { 
        }

        //Reset//////////////////////////////////////////////////////////////////////////////////////////////
        public void Reset()
        {
            Flag_MsgBoxError = false;
            Flag_MsgBoxModeless = false;
            Flag_MsgBoxWarning = false;
            Flag_RequestMsgbox = false;
			Flag_RequestMsgBox3Input = false;

			Flag_RequestState = 0;
			RequestState_3input_i = 0;
        }

		//Triggers///////////////////////////////////////////////////////////////////////////////////////////
		public void Trigger_MessageBox_Error(String strError)
		{
			MessageError_str = strError;
			Flag_MsgBoxError = true;
		}
		public void Trigger_MessageBox_Warning(String strWarn)
		{
			MessageWarning_str = strWarn;
			Flag_MsgBoxWarning = true;
		}
		public void Trigger_MessageBox_Modeless(String strMsg, bool TriggerOn)
		{
			ModelessMsg_str = strMsg;
			Flag_MsgBoxModeless = TriggerOn;
		}
		public void Trigger_MessageBox_Request (String strRequest)
		{
			Flag_RequestState = 0;
			MessageRequest_str = strRequest;
			Flag_RequestMsgbox = true;
		}
		public void Trigger_MessageBox_Request3Input(String strRequest)
		{
			RequestState_3input_i = 0;
			MessageRequest3Input_str = strRequest;
			Flag_RequestMsgBox3Input = true;
		}
    }
}
