using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;
using AMTManageability.Remote;

namespace AMTManageability.Business_Logic_Layer
{
    public class AMTPowerStateManager
    {

        private static AMTPowerStateManager instance = null;

        private AMTPowerStateManager()
        {
        }

        public static AMTPowerStateManager Instance
        {
            get
            {
                if (instance == null)
                    instance = new AMTPowerStateManager();
                return instance;
            }
        }

        public PowerState GetAMTMachinePowerState(AMTMachine machine)
        {

            return PowerState.POWER_UP;
        }

        public bool PoweUpAMTMachine(AMTMachine machine)
        {
           // AMTMachineRemoteControl rc = new AMTMachineRemoteControl(machine);
            return true;
            
        }

        public bool PowerDownAMTMachine(AMTMachine machine)
        {
            return false;
        }

        public bool RestartAMTMachine(AMTMachine machine)
        {

            return false;
        }
        
        
        public enum PowerState
        {
            POWER_UP,POWER_DOWN
        }

    }
}
