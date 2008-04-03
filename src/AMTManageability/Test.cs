using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;
using System.Net;

namespace AMTManageability
{


    public class Test
    {



        public uint test()
        {
            RemoteControlService service = new RemoteControlService("10.0.0.3:16992");
            ServiceSetup(service);
            //uint powerState = new uint ();

           // service.GetSystemPowerState(out powerState);
           // return powerState;

          //  service.RemoteControl(RemoteControlCommandType.PowerDown,343,new byte (),false,new ushort (),false,new ushort (),false,new ushort (),false);

            uint state = new uint ();
            uint ret = new uint ();
            ret = service.GetSystemPowerState(out state);
            return ret;

        }


        private void ServiceSetup(System.Web.Services.Protocols.SoapHttpClientProtocol service)
        {
            service.Proxy = null;


            service.Url = service.Url.Replace("https://", "http://");
            service.Url = service.Url.Replace("16993", "16992");

            string Password = "Intel@12";
            string Username = "admin";
            string HostName = "10.0.0.3";

            service.ConnectionGroupName = "10.0.0.3";
            service.Timeout = 10000;

            if (Password != null && Password.Length > 0)
            {
                int i = Username.IndexOf("\\");
                if (i > 0)
                {
                    // Using Kerberos
                    string t_username = Username.Substring(i + 1);
                    string t_domain = Username.Substring(0, i);

                    // Setup the authentication manager
                    if (AuthenticationManager.CustomTargetNameDictionary.ContainsKey(service.Url) == false)
                    {
                        string SPN = "HTTP/" + HostName + ":16993";
                        if (service.Url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase) == true) SPN = "HTTP/" + HostName + ":16992";
                        AuthenticationManager.CustomTargetNameDictionary.Add(service.Url, SPN);
                    }

                    CredentialCache myCache = new CredentialCache();
                    myCache.Add(new System.Uri(service.Url), "Negotiate", new NetworkCredential(t_username, Password, t_domain));
                    service.Credentials = myCache;
                }
                else
                {
                    // Using HTTP Digest

                    NetworkCredential nc = new NetworkCredential(Username, Password);
                    service.Credentials = nc;
                }
            }

            if ((Password == null || Password.Length == 0) && (Username == null || Username.Length == 0))
            {
                // Use Kerberos with local credentials

                // Setup the authentication manager
                if (AuthenticationManager.CustomTargetNameDictionary.ContainsKey(service.Url) == false)
                {
                    string SPN = "HTTP/" + HostName + ":16993";
                    if (service.Url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase) == true) SPN = "HTTP/" + HostName + ":16992";
                    AuthenticationManager.CustomTargetNameDictionary.Add(service.Url, SPN);
                }

                CredentialCache myCache = new CredentialCache();
                myCache.Add(new System.Uri(service.Url), "Negotiate", System.Net.CredentialCache.DefaultNetworkCredentials);
                service.Credentials = myCache;
            }





        }
    }
}
