using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;

namespace AMTManageability.Remote
{
    public abstract class MachineRemoteControl
    {
        protected Machine machine;
        protected RemoteControlService remoteControlService;
    }
}
