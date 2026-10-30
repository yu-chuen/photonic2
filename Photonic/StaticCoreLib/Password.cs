/****************************************************************************************************************************************
 * Password class:
 * Desc: This class stores the login passwords and usernames.Write to file and etc.
****************************************************************************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

namespace StaticCoreLib
{
    //Data class*******************************************************************************************************************************
    [Serializable]
    public class Password_Data
    {
        //variables
        public String m_strCurrentUser = "";

        [XmlArray("PD_n_ID")]
        public List<User_Data> m_llUserData;

        //constructor///////////////////////////////////////////////////////////////////////////////////////////////////
        public Password_Data()
        {
            m_llUserData = new List<User_Data>();
        }
    }
    //*******************************************************************************************************************************************

    //Working Class
    public class Password
    {
        //variables
        Password_Data m_PDData = new Password_Data();

        String m_strPasswordFilePath = "";

        List<string> m_ll_LevelPd;

        public string Level_User { get { return "User"; } }
        public string Level_Supervisor { get { return "Supervisor"; } }
        public string Level_Engineer { get { return "Engineer"; } }
        public string Level_Admin { get { return "Admin"; } }

        //constructor/////////////////////////////////////////////////////////////////////////////////
        public Password(String strFilePath)
        {
            if (strFilePath.EndsWith("\\") == false) strFilePath += "\\";

            m_strPasswordFilePath = strFilePath + "Access.xml";

            From_Registry();

            //add user levels
            m_ll_LevelPd = new List<string>();
            //m_ll_LevelPd.Add(Level_User);
            //m_ll_LevelPd.Add(Level_Supervisor);
            //m_ll_LevelPd.Add(Level_Engineer);
            m_ll_LevelPd.Add(Level_Admin);
        }

        //Get Current username/////////////////////////////////////////////////////////////////////////
        public String Get_CurrentUser() { return m_PDData.m_strCurrentUser; }
        //Set Current username////////////////////////////////////////////////////////////////////////
        public void Set_CurrentUser(String strUserName) { m_PDData.m_strCurrentUser = strUserName; To_Registry(); }

        //Add user///////////////////////////////////////////////////////////////////////////////////////////////////
        public void Add_User(String strUsername, String strPD, String strLevel)
        {
            User_Data mUserData = new User_Data();
            bool bExist = false;
            int iPos = 0;

            //populate local data
            mUserData.m_strUserName = strUsername;
            mUserData.m_strPassword = strPD;
            mUserData.m_strLevel = strLevel;

            //Check User exist
            bExist = User_Exist(strUsername, ref iPos);

            //add local data to list
            if (bExist)
            {
                m_PDData.m_llUserData[iPos] = mUserData;
            }
            else
                m_PDData.m_llUserData.Add(mUserData);

            //Update xml setting file
            To_Registry();
        }

        //Check if User exist///////////////////////////////////////////////////////////////////////////////////
        public bool User_Exist(String strUserName, ref int iPos)
        {
            iPos = 0;

            //struct list is empty
            if (m_PDData.m_llUserData.Count <= 0)
                return false;

            //Check
            foreach (User_Data mUserData in m_PDData.m_llUserData)
            {
                if (mUserData.m_strUserName == strUserName)
                {
                    return true;
                }

                iPos++;
            }

            return false;
        }

        //To registry/////////////////////////////////////////////////////////////////////////////////////////////////
        public void To_Registry()
        {
            XMLSerializer.Serialize_Func<Password_Data>(m_PDData, m_strPasswordFilePath);
        }

        //From registry///////////////////////////////////////////////////////////////////////////////////////////
        public void From_Registry()
        {
            m_PDData = XMLSerializer.Deserialize_Func<Password_Data>(m_strPasswordFilePath);
        }

        //Check if the username and password matches//////////////////////////////////
        public bool IsValid(String strUserName, String strPassword)
        {
            if (strUserName.ToLower().Trim() == "root" && strPassword == "genetec") { return true; } //genetec used only

            //struct list is empty
            if (m_PDData.m_llUserData.Count <= 0)
            {
                if (strUserName.ToLower().Trim() == "admin" && strPassword == "1234") return true;
                else return false;
            }

            ////loop through the list to find the user name
            foreach (User_Data mUserData in m_PDData.m_llUserData)
            {
                if (mUserData.m_strUserName == strUserName && mUserData.m_strPassword == strPassword)
                {
                    Set_CurrentUser(strUserName);
                    return true;
                }
            }

            return false;
        }

        //Get User level/////////////////////////////////////////////////////////////////////////////////////////
        public String Get_UserLevel(String strUserName)
        {
            String strLevel = "";

            //struct list is empty
            if (m_PDData.m_llUserData.Count <= 0)
            {
                return "admin";
            }

            //loop to get the user
            foreach (User_Data mUserData in m_PDData.m_llUserData)
            {
                if (mUserData.m_strUserName == strUserName)
                {
                    strLevel = mUserData.m_strLevel.ToLower();
                }
            }

            return strLevel;
        }

        //Get user level - Clearance////////////////////////////////////////////////////////////////////
        //Note: Set this to instantly get pass or fail
        public bool Get_UserLevel_Clearance(int iLevel)
        {
            String strLevel = Get_UserLevel(Get_CurrentUser()).ToLower();

            switch (iLevel)
            {
                case 2: if (strLevel == Level_Supervisor.ToLower() || strLevel == Level_Engineer.ToLower() || strLevel == Level_Admin.ToLower()) return true; break;
                case 3: if (strLevel == Level_Engineer.ToLower() || strLevel == Level_Admin.ToLower()) return true; break;
                case 4: if (strLevel == Level_Admin.ToLower()) return true; break;
            }

            return false;
        }

        //Delete user/////////////////////////////////////////////////////////////////////////////////////////////
        public void Delete_User(String strUsername)
        {
            int iPos = 0;

            //struct list is empty
            if (m_PDData.m_llUserData.Count <= 0) return;

            //Remove user from list
            if (User_Exist(strUsername, ref iPos) == true)
            {
                m_PDData.m_llUserData.RemoveAt(iPos);
            }
        }

        //Get Level list////////////////////////////////////////////////////////////////////////////////////////// for ui display only
        /// <summary>
        /// Get the index from the list of user levels. For Ui display only
        /// </summary>
        /// <param name="iIndex"></param>
        /// <returns></returns>
        public String Get_Level(int iIndex)
        {
            String strLevel = "Error";

            //blocking and checking
            if (iIndex > m_ll_LevelPd.Count - 1) { return "Error: Get level exceeded maximum number in the list"; }

            //get level
            strLevel = m_ll_LevelPd[iIndex];

            return strLevel;
        }

    }

    //data structur
    public struct User_Data
    {
        public String m_strUserName;
        public String m_strPassword;
        public String m_strLevel;
    }
}
