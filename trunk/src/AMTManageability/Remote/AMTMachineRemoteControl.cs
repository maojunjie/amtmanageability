using System;
using System.Collections.Generic;
using System.Text;
using AMTManageability.Entities;
using System.Net;
using AMTManageability.Utilities;
using System.Web.Services.Protocols;

namespace AMTManageability.Remote
{
    /// <summary>
    /// @author: karim wadie
    /// @date: 2/4/08
    /// @update:
    /// 
    /// 
    /// This class provides remote control methods
    /// for managing an amt machine.
    /// 
    /// the core method is RemoteControlService.RemoteControl that is warpped
    /// by the implemented methods given the right paramaters to perform
    /// a specific task.
    /// 
    /// NOTE:
    /// - to add a new remote control function (ex:restart and set redirect flag)
    /// you should wrap the RemoteControlService.RemoteControl function to abstract 
    /// the RemoteControlService network interface.
    /// - see the Network interface.pdf guide for the documentation 
    /// of RemoteControlService Interface 
    /// </summary>
    public class AMTMachineRemoteControl
    {
        private AMTMachine machine;
        private RemoteControlService remoteControlService;
        private const uint IanaOemNumber = 343; 

        public AMTMachineRemoteControl(AMTMachine machine)
        {
            this.machine = machine;
            remoteControlService = new RemoteControlService( machine.HostName + ":" + ResourcesManager.AMTPort);
            ServiceSetup(remoteControlService);

        }

        private void ServiceSetup(SoapHttpClientProtocol service)
        {
            service.Proxy = null;

            service.Url = service.Url.Replace("https://", "http://");
            service.Url = service.Url.Replace("16993", "16992");

            service.ConnectionGroupName = this.machine.NetworkName;
            service.Timeout = 10000;

            if (this.machine.Password != null && this.machine.Password.Length > 0)
            {
                int i = ((AMTMachine)this.machine).UserName.IndexOf("\\");
                if (i > 0)
                {
                    // Using Kerberos
                    string t_username =  this.machine.UserName.Substring(i + 1);
                    string t_domain = this.machine.UserName.Substring(0, i);

                    // Setup the authentication manager
                    if (AuthenticationManager.CustomTargetNameDictionary.ContainsKey(service.Url) == false)
                    {
                        string SPN = "HTTP/" + this.machine.NetworkName + ":" + ResourcesManager.AMTPort;

                        if (service.Url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase) == true) SPN = "HTTP/" + this.machine.NetworkName+ ":" + ResourcesManager.AMTPort;
                        AuthenticationManager.CustomTargetNameDictionary.Add(service.Url, SPN);
                    }

                    CredentialCache myCache = new CredentialCache();
                    myCache.Add(new System.Uri(service.Url), "Negotiate", new NetworkCredential(t_username, this.machine.Password, t_domain));
                    service.Credentials = myCache;
                }
                else
                {
                    // Using HTTP Digest

                    NetworkCredential nc = new NetworkCredential(this.machine.UserName, this.machine.Password);
                    service.Credentials = nc;
                }
            }

            if ((this.machine.Password == null || this.machine.Password.Length == 0) && (this.machine.UserName == null || this.machine.UserName.Length == 0))
            {
                // Use Kerberos with local credentials

                // Setup the authentication manager
                if (AuthenticationManager.CustomTargetNameDictionary.ContainsKey(service.Url) == false)
                {
                    string SPN = "HTTP/" + this.machine.NetworkName + ":" + ResourcesManager.AMTPort;

                    if (service.Url.StartsWith("https", StringComparison.CurrentCultureIgnoreCase) == true) SPN = "HTTP/" + this.machine.NetworkName + ":" + ResourcesManager.AMTPort;
                    AuthenticationManager.CustomTargetNameDictionary.Add(service.Url, SPN);

                }

                CredentialCache myCache = new CredentialCache();
                myCache.Add(new System.Uri(service.Url), "Negotiate", System.Net.CredentialCache.DefaultNetworkCredentials);
                service.Credentials = myCache;
            }





        }

        public AmtCallStatus PowerUp()
        {
           return (AmtCallStatus) remoteControlService.RemoteControl(RemoteControlCommandType.PowerUp,
                                                IanaOemNumber,
                                                 RemotSpecialCommandType.NOP, false,
                                                0, false,
                                                0, false,
                                                0, false);

            
                                                
        }

        public AmtCallStatus PowerDown()
        {
                return (AmtCallStatus)remoteControlService.RemoteControl(RemoteControlCommandType.PowerDown,
                                                    IanaOemNumber,
                                                    RemotSpecialCommandType.NOP, false,
                                                    0, false,
                                                    0, false,
                                                    0, false);
          
        }

        public AmtCallStatus Restart()
        {
            return (AmtCallStatus) remoteControlService.RemoteControl(RemoteControlCommandType.Reset,
                                                IanaOemNumber,
                                                 RemotSpecialCommandType.NOP, false,
                                                0, false,
                                                0, false,
                                                0, false);
        }

        public AmtPowerStates GetMachinePowerState()
        {
            AmtCallStatus r;
            uint systemPowerState;
            try
            {
                r = (AmtCallStatus) remoteControlService.GetSystemPowerState(out systemPowerState);
            }
            catch 
            {
                return AmtPowerStates.ERROR_GETTING_STATE;
            }
            if (r != AmtCallStatus.SUCCESS)
                return AmtPowerStates.ERROR_GETTING_STATE;

            return (AmtPowerStates)(systemPowerState);// & (uint)0x000F);
        }

        public void PowerUp_Async()
        {
            remoteControlService.RemoteControlAsync(RemoteControlCommandType.PowerUp,
                                                IanaOemNumber,
                                                 RemotSpecialCommandType.NOP, false,
                                                0, false,
                                                0, false,
                                                0, false);

        }

        public void PowerDown_Async()
        {
            //////dsadasd
            remoteControlService.RemoteControlAsync(RemoteControlCommandType.PowerDown,
                                                    IanaOemNumber,
                                                    RemotSpecialCommandType.NOP, false,
                                                    0, false,
                                                    0, false,
                                                    0, false);
        }

        public void Restart_Async()
        {
            remoteControlService.RemoteControlAsync(RemoteControlCommandType.Reset,
                                                IanaOemNumber,
                                                 RemotSpecialCommandType.NOP, false,
                                                0, false,
                                                0, false,
                                                0, false);
        }

        public void BeginGetMachiePowerState(AsyncCallback onCallBackDelegate, object asyncState)
        {
            remoteControlService.BeginGetSystemPowerState(onCallBackDelegate, asyncState);
        }
    }
}
