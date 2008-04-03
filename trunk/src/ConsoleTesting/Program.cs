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


        static void Main(string[] args)
        {
            try
            {

                IPAddress one = IPAddress.Parse("10.0.0.10");
                IPAddress two = IPAddress.Parse("10.0.0.1");

                long diff = GetAddressDiff(two, one);

                Console.WriteLine(diff);

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
