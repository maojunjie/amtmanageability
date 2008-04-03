using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;

namespace AMTManageability.Business_Logic_Layer
{
    public class MachineManager
    {
        private static MachineManager instance = null;

        private MachineManager()
        {
        }

        public static MachineManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new MachineManager();

                return instance;
            }
        }

        /// <summary>
        /// Takes a machine Id, search the DB, and return the 
        /// appropriate machine type with the available data
        //
        /// </summary>
        /// <param name="machineId">machine id to be fetched</param>
        /// <returns></returns>
        public Machine GetMachine_ById(int machineId)
        {
            /// use the DAL to retrive the machine
            /// use the builder to build the appropriate machine type
            /// 

            return new AMTMachine ();
        }

    
    }
}
