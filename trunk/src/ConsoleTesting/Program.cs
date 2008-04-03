using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Remote;
using AMTManageability.Entities;
using System.Net;

namespace ConsoleTesting
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                AMTMachine machine = new AMTMachine();
                machine.UserName = "admin";
                machine.Password = "Intel@12";

                machine.HostName = "ncgp2";
               // machine.Ip = IPAddress.Parse("10.0.0.3");

                AMTMachineRemoteControl rc = new AMTMachineRemoteControl(machine);

               // Console.WriteLine("before send");
                rc.PowerDown_Async();
               // Console.WriteLine("after send");
               // AmtPowerStates state = rc.GetMachinePowerState();
                

               // Console.WriteLine("state :"+ state);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
