using System;
using System.Collections.Generic;
using System.Text;

namespace AMTManageability.Entities
{
    /// @Author: karim wadie
    /// @Date: 29/3/08
    /// @Update:
    /// </summary>
    public class User
    {
        #region "Properties"

        private string userName;
        private string password_Hashed;
        private string role;

        #endregion

        #region "Setters & Getters"

        public string Role
        {
            get { return role; }
            set { role = value; }
        }

        public string Password_Hashed
        {
            get { return password_Hashed; }
            set { password_Hashed = value; }
        }

        public string UserName
        {
            get { return userName; }
            set { userName = value; }
        }

        #endregion
    }
}
