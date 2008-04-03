using System;
using System.Collections.Generic;
using System.Text;
using System.Net;

namespace AMTManageability.Entities
{
    /// <summary>
    /// @Author: karim wadie
    /// @Date: 29/3/08
    /// @Update:
    /// </summary>
    public class Machine 
    {

        protected int machineId;
        protected string hostName;
        protected IPAddress ip;

        #region "Setters & Getters"


        public int MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }

        public string HostName
        {
            get { return hostName; }
            set { hostName = value; }
        }

        public IPAddress Ip
        {
            get { return ip; }
            set { ip = value; }
        }
        #endregion
    }
}
