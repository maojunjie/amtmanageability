using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;

namespace AMTManageability.Business_Logic_Layer
{
    public class User_Machine_MM_Manager
    {
        private static User_Machine_MM_Manager instance = null;

        private User_Machine_MM_Manager()
        {
         
        }

        public static User_Machine_MM_Manager Instance
        {
            get
            {
                if (instance == null)
                    instance = new User_Machine_MM_Manager();

                return instance;
            }
          
        }

        public bool Create(User_Machine_MM user_machine)
        {
            /// Presist the user-machine in the DB table usin the DAL Class
            ///  

            return true;
        }

        public User_Machine_MM[] GetUser_Machines_ByUser(User user)
        {
            /// Use the DAL to retrive 
            /// 
            return new User_Machine_MM[1];
        }
    }
}
