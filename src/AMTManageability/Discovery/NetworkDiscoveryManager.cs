using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using AMTManageability.Entities;
using AMTManageability.Utilities;

namespace AMTManageability.Discovery
{
    public class NetworkDiscovery
    {
        private int discoveryThreadsNumber;

        private List<AMTMachine> amtMachinesList;
        private List<Machine> nonAMTMachineList;

        public int DiscoveryThreadsNumber
        {
            get { return discoveryThreadsNumber; }
           
        }

        public List<AMTMachine> AmtMachines
        {
            get
            {
                return amtMachinesList;
            }
        }

        public List<Machine> NonAmtMachines
        {
            get
            {
                return nonAMTMachineList;
            }
        }

        public NetworkDiscovery(int discoverThreadsCount)
        {
            this.discoveryThreadsNumber = discoverThreadsCount;
        }

        public void ScanNetwork(IPAddress startAddress, IPAddress endAddress)
        {
            amtMachinesList = new List<AMTMachine>();
            nonAMTMachineList = new List<Machine>();

            HttpWebRequest request= null;
            HttpWebResponse response = null;

            IPAddress currentAddress = startAddress;

            /**
            while (! AreEqual (currentAddress, endAddress))
            {
                request  = (HttpWebRequest)HttpWebRequest.Create("http://"+currentAddress+":"+ResourcesManager.AMTPort);
                response = (HttpWebResponse) request.GetResponse();

                //if (response.Server.StartsWith ("amt"))

            }
             * 
             * */

            request = (HttpWebRequest)HttpWebRequest.Create("http://ncgp2:" + ResourcesManager.AMTPort);
            response = (HttpWebResponse)request.GetResponse();

            if (IsAmtServer(response.Server))
            {
                AMTMachine temp = new AMTMachine();
                temp.HostName = "ncgp2";
                int[] version = ParseAmtVersion(response.Server);
                temp.AmtVersion = version[0];
                temp.AmtRevMin = version[1];
                amtMachinesList.Add(temp);
            }

        }

        private long GetAddressDiff(IPAddress lowerIp, IPAddress upperIp)
        {
            byte[] b1 = lowerIp.GetAddressBytes();
            byte[] b2 = upperIp.GetAddressBytes();
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

        public void TestThreads()
        {
            // calculate the numeber of ips for each threads
            int dThreads = 10;
            IPAddress ip1 = IPAddress.Parse("10.0.0.1");
            IPAddress ip2 = IPAddress.Parse("10.0.0.100");

            double ipLotta = Math.Ceiling  ((double)  (GetAddressDiff(ip1,ip2) / dThreads) );
        }

        public void Test()
        {
            amtMachinesList = new List<AMTMachine>();
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            request = (HttpWebRequest)HttpWebRequest.Create("http://ncgp2:" + ResourcesManager.AMTPort);
            response = (HttpWebResponse)request.GetResponse();

            if (IsAmtServer(response.Server))
            {
                AMTMachine temp = new AMTMachine();
                temp.HostName = "ncgp2";
                int[] version = ParseAmtVersion(response.Server);
                temp.AmtVersion = version[0];
                temp.AmtRevMin = version[1];
                amtMachinesList.Add(temp);
            }
        }

        private IPAddress NextIPAddress(IPAddress address)
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

        private bool AreEqual(IPAddress add1, IPAddress add2)
        {
            return add1.ToString().Equals(add2.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        private bool IsAmtServer(string serverHeader)
        {
            return serverHeader.StartsWith(ResourcesManager.AMTServerResponceHeader);         
        }

        private int [] ParseAmtVersion (string serverHeader)
        {
            string signatur = ResourcesManager.AMTServerResponceHeader;

            if (serverHeader.StartsWith(signatur))
            {
                string versionSubStr = serverHeader.Substring(signatur.Length);
                versionSubStr = versionSubStr.Trim();

                char[] separator = {'.' };
                string[] versions = versionSubStr.Split(separator);

                int[] ret = new int[versions.Length];

                for (int i = 0; i < versions.Length; i++)
                {
                    try
                    {
                        ret[i] = int.Parse(versions[i]);
                    }
                    catch (FormatException)
                    {
                        ret[i] = -1;
                    }
                }

                return ret;
            }
            else
            {
                return new int[2];

            }
        }
    }
}
