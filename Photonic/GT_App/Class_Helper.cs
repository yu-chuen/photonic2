using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace GT_App
{
    public class Class_Helper
    {
        //detect double numeric only////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Detect_DoubleOnly(String strData, ref int iErrorType)
        {
            bool bDot = false, bDash = false; 

            strData = strData.Trim();
            foreach (char c in strData)
            {
                if (c < '0' || c > '9')
                {
                    if (c != '.' && c != '-')
                    {
                        MessageBox.Show("Please enter a numeric value");
                        iErrorType = -1;
                        return false;
                    }
                    else if (c == '.' && bDot == true)
                    {
                        MessageBox.Show("Detected Multiple dots");
                        iErrorType = -2;
                        return false;
                    }
                    else if (c == '-' && bDash == true)
                    {
                        MessageBox.Show("Detected Multiple dashes");
                        iErrorType = -3;
                        return false;
                    }

                    if (c == '.') bDot = true;
                    if (c == '-') bDash = true;
                }
            }

            return true;
        }

        //detect integer only//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Detect_IntegerOnly(String strData)
        {
            foreach (char c in strData)
            {
                if (c < '0' || c > '9')
                {
                    if (c == '-') return true;

                    MessageBox.Show("Please enter numbers only");
                    return false;
                }
            }

            return true;
        }

        //Detect integer only with String output//////////////////////////////////////////////////////////////////////////////////////////////
        public static String Detect_IntegerOnly_OutString(String strData)
        {
            return (Regex.Replace(strData, "[^0-9]", ""));
        }

        //Detect IP Address/////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        public static bool Detect_IPAddress(String strData)
        {
            //Check if the input is numeric
            foreach (char c in strData)
            {
                if (c < '0' || c > '9')
                {
                    MessageBox.Show("Please enter numbers only");
                    return false;
                }
            }

            //check if the total string exceed maximum of 3 digits
            if (strData.Length > 3) return false;

            return true;
        }

        //IP Address Textbox Text Change//////////////////////////////////////////////////////////////////////////////////////////////////
        public static void IPAddress_TextBoxChanged(TextBox obj)
        {
            if (Detect_IPAddress(obj.Text) == false)
            {
                obj.Text = Regex.Replace(obj.Text, "[^0-9]", "");
                if (obj.Text.Length > 3) obj.Text = obj.Text.Remove(obj.Text.Length - 1);
            }
        }

		//Display error messagebox for maintenance control////////////////////////////////////////////////////////////////////
		public static void Display_ErrorMessageBox(String strError)
		{
			MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
			dlg.Set_Messages(strError, "");
            dlg.Set_BitMapLogo(1);
            dlg.Set_Buttons(1);
            dlg.ShowDialog();
            dlg.Dispose();
		}

		//Display Notice message box///////////////////////////////////////////////////////////////////////////////////////////////////////////
		public static void Display_NoticeMessageBox( String strNotice )
		{
			MessageBox_CustomDlg dlg = new MessageBox_CustomDlg();
			dlg.Set_Messages(strNotice, "");
			dlg.Set_BitMapLogo(2);
			dlg.Set_Buttons(1);
			dlg.ShowDialog();
			dlg.Dispose();
		}


    }
}
