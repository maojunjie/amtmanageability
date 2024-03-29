﻿// ------------------------------------------------------------------------------
//<autogenerated>
//        This code was generated by Microsoft Visual Studio Team System 2005.
//
//        Changes to this file may cause incorrect behavior and will be lost if
//        the code is regenerated.
//</autogenerated>
//------------------------------------------------------------------------------
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DLLUnitTests
{
[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class BaseAccessor {
    
    protected Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject m_privateObject;
    
    protected BaseAccessor(object target, Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) {
        m_privateObject = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateObject(target, type);
    }
    
    protected BaseAccessor(Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType type) : 
            this(null, type) {
    }
    
    internal virtual object Target {
        get {
            return m_privateObject.Target;
        }
    }
    
    public override string ToString() {
        return this.Target.ToString();
    }
    
    public override bool Equals(object obj) {
        if (typeof(BaseAccessor).IsInstanceOfType(obj)) {
            obj = ((BaseAccessor)(obj)).Target;
        }
        return this.Target.Equals(obj);
    }
    
    public override int GetHashCode() {
        return this.Target.GetHashCode();
    }
}


[System.Diagnostics.DebuggerStepThrough()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.VisualStudio.TestTools.UnitTestGeneration", "1.0.0.0")]
internal class AMTManageability_Remote_AMTMachineRemoteControlAccessor : BaseAccessor {
    
    protected static Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType m_privateType = new Microsoft.VisualStudio.TestTools.UnitTesting.PrivateType(typeof(global::AMTManageability.Remote.AMTMachineRemoteControl));
    
    internal AMTManageability_Remote_AMTMachineRemoteControlAccessor(global::AMTManageability.Remote.AMTMachineRemoteControl target) : 
            base(target, m_privateType) {
    }
    
    internal global::AMTManageability.Entities.AMTMachine machine {
        get {
            global::AMTManageability.Entities.AMTMachine ret = ((global::AMTManageability.Entities.AMTMachine)(m_privateObject.GetField("machine")));
            return ret;
        }
        set {
            m_privateObject.SetField("machine", value);
        }
    }
    
    internal global::RemoteControlService remoteControlService {
        get {
            global::RemoteControlService ret = ((global::RemoteControlService)(m_privateObject.GetField("remoteControlService")));
            return ret;
        }
        set {
            m_privateObject.SetField("remoteControlService", value);
        }
    }
    
    internal static uint IanaOemNumber {
        get {
            uint ret = ((uint)(m_privateType.GetStaticField("IanaOemNumber")));
            return ret;
        }
        set {
            m_privateType.SetStaticField("IanaOemNumber", value);
        }
    }
    
    internal void ServiceSetup(global::System.Web.Services.Protocols.SoapHttpClientProtocol service) {
        object[] args = new object[] {
                service};
        m_privateObject.Invoke("ServiceSetup", new System.Type[] {
                    typeof(global::System.Web.Services.Protocols.SoapHttpClientProtocol)}, args);
    }
}
}
