using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Remote;
using AMTManageability.Entities;
using System.Net;
using AMTManageability.Discovery;
using System.Threading;

namespace ConsoleTesting
{
    class Program
    {
        private static IPAddress NextIPAddress(IPAddress address)
        {
            // copied from DTK 

            byte[] ip = address.GetAddressBytes();
            ip[ip.Length - 1]++;
            bool carry = false;
            carry = (ip[ip.Length - 1] == 0);
            for (int i = ip.Length - 1; i > 0; i--)
            {
                if (carry)
                {
                    ip[i - 1]++;
                    carry = (ip[i - 1] == 0);
                }
            }
            return new IPAddress(ip);
        }

        public static long GetAddressDiff(IPAddress a1, IPAddress a2)
        {
            byte[] b1 = a1.GetAddressBytes();
            byte[] b2 = a2.GetAddressBytes();
            if (b1.Length != b2.Length) return -1;
            int carry = 0;
            long total = 0;
            int l = b1.Length - 1;
            for (int i = l; i >= 0; i--)
            {
                int d = b2[i] - carry - b1[i];
                if (d < 0)
                {
                    carry = 1;
                    total += (d << ((l - i) * 8));
                }
                else
                {
                    carry = 0;
                    total += (d << ((l - i) * 8));
                }
            }
            return total + 1;
        }

        public static void OnMachineDiscovery(AmtDiscoveryEvent e, AMTMachine machine)
        {
            if (e == AmtDiscoveryEvent.LaunchingDiscovery)
                Console.WriteLine("DISCOVERY STARTED");

            if (e == AmtDiscoveryEvent.DiscoveryFinished)
                Console.WriteLine("DISCOVERY FINISHED");

            if (e == AmtDiscoveryEvent.MachineFound)
            {
                Console.WriteLine("--------------");
                Console.WriteLine("MAchine "+ machine.HostName);
                Console.WriteLine("------------------");
            }

           
        }

        public static void IPRegression()
        {
            IPAddress s = IPAddress.Parse("9.255.255.100");

            for (int i = 0; i < 300; i++)
            {
                Console.WriteLine(s.ToString ());
                s = NextIPAddress(s);
            }
	{
		 
	}
        }

        public static void ThreadTest()
        {
            NetworkDiscovery d = new NetworkDiscovery(1);
            d.OnDiscoveryEvent += new NetworkDiscovery.AmtDiscoveryEventHandler(OnMachineDiscovery);

            string ip1 = "10.0.0.0";
            string ip2 = "9.255.255.100";

            IPAddress s = IPAddress.Parse(ip1);
            IPAddress e = IPAddress.Parse("10.0.0.10");

            d.ScanNetwork(s, e);
            //d.Test();

            Thread.Sleep(TimeSpan.FromSeconds(3));
            while (d.IsRunning)
            {
                Thread.Sleep(TimeSpan.FromSeconds(3));
                Console.WriteLine("discoverd count " + d.AmtMachines.Count);
            }

           

            
        }

        static void Main(string[] args)
        {
            try
            {
                //IPRegression();
                ThreadTest();

                /**
                AMTManageability.Discovery.NetworkDiscovery d = new AMTManageability.Discovery.NetworkDiscovery(1);
                d.Test();

                Console.WriteLine(d.AmtMachines.Count);
                foreach (AMTManageability.Entities.AMTMachine m in d.AmtMachines)
                    Console.WriteLine(m.HostName + "   "+ m.AmtVersion + "  "+ m.AmtRevMin );
                **/
            

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
