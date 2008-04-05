using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using AMTManageability.Entities;
using AMTManageability.Utilities;
using System.Threading;

namespace AMTManageability.Discovery
{
    public class NetworkDiscovery
    {

        private struct ScanNetworkParam
        {
            public IPAddress start;
            public IPAddress end;
        };

      
       


        #region "Properties"
        private int discoveryThreadsNumber;

        private List<AMTMachine> amtMachinesList;
        private List<Machine> nonAMTMachineList;
        private Thread[] discoveryThreads;
        private ScanNetworkParam[] ipGroups;
        private bool isRunning;

        private long ipCountToSearch;
        private long ipCurrentCount;
        #endregion

        public event AmtDiscoveryEventHandler OnDiscoveryEvent;
        public delegate void AmtDiscoveryEventHandler(AmtDiscoveryEvent e, AMTMachine machine);

        #region "Setters & Getters"
        public bool IsRunning
        {
            get { return isRunning; }
           
        }

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
        #endregion

        public NetworkDiscovery(int discoverThreadsCount)
        {
            this.discoveryThreadsNumber = discoverThreadsCount;
            amtMachinesList = new List<AMTMachine>();
            nonAMTMachineList = new List<Machine>();
            discoveryThreads = new Thread[discoveryThreadsNumber];
            ipGroups = new ScanNetworkParam[discoveryThreadsNumber];
        }

        public void ScanNetwork(IPAddress start, IPAddress end)
        {
            IPAddress currentIp = start;

            ipCountToSearch = GetAddressDiff(start, end);
            int ipLotta = (int)ipCountToSearch / discoveryThreadsNumber;
            
            int remainig = (int)ipCountToSearch % discoveryThreadsNumber;
            ScanNetworkParam threadParam;

            for (int i = 0; i < discoveryThreadsNumber; i++)
            {
               threadParam  = new ScanNetworkParam();
               threadParam.start = currentIp;

                // get the end ip by the loop
                for (int j = 0; j < ipLotta; j++)
                    currentIp = NextIPAddress(currentIp);

                threadParam.end = currentIp;

                if (i == discoveryThreadsNumber - 1 && remainig != 0)
                    for (int k = 0; k < remainig; k++)
                        currentIp = NextIPAddress(currentIp);

                threadParam.end = currentIp;

                ipGroups[i] = threadParam;
            }

            isRunning = true;
            if (OnDiscoveryEvent != null)
                OnDiscoveryEvent(AmtDiscoveryEvent.LaunchingDiscovery, null);

            for (int i = 0; i < discoveryThreadsNumber; i++)
            {
                discoveryThreads[i] = new Thread(ScanNetwork) ;
                discoveryThreads[i].Start(ipGroups[i]);
            }

        }

        private void ScanNetwork(object arg)
        {
            ScanNetworkParam param = (ScanNetworkParam)arg;

            HttpWebRequest request= null;


            IPAddress currentAddress = param.start;
            object[] onMachineFoundArgs = null;
          
            while (! AreEqual (currentAddress, param.end))
            {
                //Console.WriteLine("before");
                try
                {

                    onMachineFoundArgs = new object[2];
                    
                    request = (HttpWebRequest)HttpWebRequest.Create("http://" + currentAddress.ToString() + ":" + ResourcesManager.AMTPort);

                    onMachineFoundArgs[0] = request;
                    onMachineFoundArgs[1] = currentAddress;
                    request.BeginGetResponse(OnMachineResponse, onMachineFoundArgs);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error "+ ex.Message);
                }

            currentAddress = NextIPAddress(currentAddress);
           
            }
             
             

           

        }

        private void OnMachineResponse(IAsyncResult result)
        {
            try
            {
                object [] args = (object[]) result.AsyncState;

                ipCurrentCount++;

                HttpWebRequest request = (HttpWebRequest)args[0];
                IPAddress serverIp = (IPAddress) args[1];

               // Console.WriteLine("on machine response "+ request.RequestUri.ToString());

                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(result);

                if (IsAmtServer(response.Server))
                {
                    AMTMachine temp = new AMTMachine();

                    
                    IPHostEntry hostEntry = System.Net.Dns.GetHostEntry(serverIp);

                    temp.HostName = hostEntry.HostName;
                    int[] version = ParseAmtVersion(response.Server);
                    temp.AmtVersion = version[0];
                    temp.AmtRevMin = version[1];

                    lock (amtMachinesList)
                    {
                        amtMachinesList.Add(temp);
                    }

                    if (OnDiscoveryEvent!=null)
                        OnDiscoveryEvent(AmtDiscoveryEvent.MachineFound,temp);
                }

                response.Close();
            }
            catch(Exception ex)
            {
               // Console.WriteLine(ex.Message);
            }

            if (ipCurrentCount == ipCountToSearch)
            {
                isRunning = false;

                if (OnDiscoveryEvent != null)
                    OnDiscoveryEvent(AmtDiscoveryEvent.DiscoveryFinished, null);
            }
            
        }

        #region "Test methods"

       /**
        public string TestThreads()
        {

            Thread t = new Thread();
            t.Start(new ScanNetworkParam ());

            // calculate the numeber of ips for each threads
            int dThreads = 50;
            IPAddress ip1 = IPAddress.Parse("10.0.0.0");
            IPAddress ip2 = IPAddress.Parse("10.0.0.255");

            IPAddress currentIp = ip1;

            long ipsCount = GetAddressDiff(ip1, ip2);
            int ipLotta =(int) ipsCount / dThreads;
            string ret = "";
            int count = 1;
            int remainig = (int) ipsCount % dThreads;

           
                for (int i = 0; i < dThreads; i++)
                {
                    ret += "Group " + (i + 1) + "\r\n";

                    for (int j = 0; j < ipLotta; j++)
                    {
                        ret += "\t" + count + ": " + currentIp.ToString() + "\r\n";
                        currentIp = NextIPAddress(currentIp);
                        count++;
                    }

                     if (i == dThreads - 1 && remainig !=0 )
                    {
                        
                        for (int k = 0; k < remainig; k++)
                        {
                            ret += "\t" + count + ": " + currentIp.ToString() + "\r\n";
                            currentIp = NextIPAddress(currentIp);
                            count++;
                        }
                    }
                }

                return ret;
                   
                }
        * 
        * */
            
        public void Test()
        {
           
            HttpWebRequest request = null;
            HttpWebResponse response = null;

            request = (HttpWebRequest)HttpWebRequest.Create("http://10.0.0.3:" + ResourcesManager.AMTPort);
            response = (HttpWebResponse)request.GetResponse();

            

            if (IsAmtServer(response.Server))
            {
                AMTMachine temp = new AMTMachine();
                temp.HostName = "ncgp2";
                int[] version = ParseAmtVersion(response.Server);
                temp.AmtVersion = version[0];
                temp.AmtRevMin = version[1];
                
                lock (amtMachinesList)
                {
                    amtMachinesList.Add(temp);
                }
            }
        }
        #endregion


        #region "Helpers"
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
        #endregion
    }
}
