using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;


namespace AMTManageability.Business_Logic_Layer
{
    public class User_Machine_RegistrationManager
    {
        #region IUser_Machine_RegistrationManager Members

        public bool Register_Machine_To_User(Machine machine, User user)
        {

            if (machine == null)
                throw new ArgumentNullException("machine cannot be null");

            if (user == null)
                throw new ArgumentNullException("user cannot be null");

            try
            {
                User_Machine_MM userMachine = new User_Machine_MM();
                userMachine.MachineId = machine.MachineId;
                userMachine.UserName = user.UserName;

                return User_Machine_MM_Manager.Instance.Create(userMachine);
            }
            catch
            {
                throw;
            }
        }

        public Machine[] GetUser_RegisterdMachines(User user)
        {
            if (user == null)
                throw new ArgumentNullException("user cannot be null");

            User_Machine_MM[] userMachines = null;
            List<Machine> machines = new List<Machine>();
           

            try
            {
              userMachines =  User_Machine_MM_Manager.Instance.GetUser_Machines_ByUser(user);

              Machine tempMachine;
              foreach (User_Machine_MM mm in userMachines)
              {
                  try
                  {
                      tempMachine = MachineManager.Instance.GetMachine_ById(mm.MachineId);
                      machines.Add(tempMachine);                  
                  }
                  catch
                  {
                      continue;
                  }
              }
            }
            catch
            {
                throw;
            }

            return machines.ToArray();
        }

        #endregion
    }
}
