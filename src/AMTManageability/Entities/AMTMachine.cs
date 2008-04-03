using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AMTManageability.Entities
{
    /// @Author: karim wadie
    /// @Date: 29/3/08
    /// @Update:
    /// </summary>
    public class AMTMachine:Machine
    {
        #region "Properties"
        private string amtVersion;
        private string userName;
        private string password;
        #endregion

        #region "Setters & Getters"

        public string AmtVersion
        {
            get { return amtVersion; }
            set { amtVersion = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public string NetworkName
        {
            get
            {
                if (ip != null)
                {
                    return ip.ToString();
                }
                else
                {
                    return HostName;
                }
            }
        }

        #endregion
    }
}
