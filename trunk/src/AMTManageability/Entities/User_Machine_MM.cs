using System;
using System.Collections.Generic;
using System.Text;

namespace AMTManageability.Entities
{
    /// <summary>
    /// @Author: karim wadie
    /// @Date: 29/3/08
    /// @Update:
    /// 
    /// 
    /// 
    ///@Description:
    /// 
    /// This class acts as the many-to-many entity calss for
    /// the relation between Machine and User tables in the DB
    /// </summary>
    public class User_Machine_MM
    {
       

        #region "Properties"
        private int machineId;
        private string userName;
        #endregion


        #region "Setters & Getters"

        public int MachineId
        {
            get { return machineId; }
            set { machineId = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }
        #endregion


    }
}
