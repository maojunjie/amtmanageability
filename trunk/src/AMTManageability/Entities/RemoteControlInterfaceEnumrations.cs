

namespace AMTManageability.Entities
{
    public enum AmtPowerStates
    {
        S0_POWERED = 0,
        S1_SLEEPING_WITH_CONTEXT = 1,
        S2_SLEEPING_WITHOUT_CONTEXT = 2,
        S3_SLEEPING_WITH_MEMORY_CONTEXT_ONLY = 3,
        S4_SUSPENDED_TO_DISK = 4,
        S5_SOFT_OFF = 5,
        S4_OR_S5 = 6,
        MECHANICAL_OFF = 7,
        S1_OR_S2_OR_S3 = 8,
        S1_OR_S2_OR_S3_OR_S4 = 9,
        S5_OVERRIDE = 10,
        LEGACY_ON = 11,
        LEGACY_OFF = 12,
        UNKNOWN13 = 13,
        UNKNOWN14 = 14,
        UNKNOWN15 = 15,
        ERROR_GETTING_STATE = 16,
        UNKNOWN = 254,
        DISCONNECTED = 255,
    }

    public enum AmtCallStatus
    {
        SUCCESS = 0x0,
        INTERNAL_ERROR = 0x1,
        INVALID_PT_MODE = 0x3,
        INVALID_NAME = 0xC,
        INVALID_BYTE_COUNT = 0xF,
        NOT_PERMITTED = 0x10,
        MAX_LIMIT_REACHED = 0x17,
        INVALID_AUTH_TYPE = 0x18,
        INVALID_DHCP_MODE = 0x1A,
        INVALID_IP_ADDRESS = 0x1B,
        INVALID_DOMAIN_NAME = 0x1C,
        INVALID_PROVISIONING_STATE = 0x20,
        INVALID_TIME = 0x22,
        INVALID_INDEX = 0x23,
        INVALID_PARAMETER = 0x24,
        INVALID_NETMASK = 0x25,
        FLASH_WRITE_LIMIT_EXCEEDED = 0x26,
        NETWORK_IF_ERROR_BASE = 0x800,
        UNSUPPORTED_OEM_NUMBER = 0x801,
        UNSUPPORTED_BOOT_OPTION = 0x802,
        INVALID_COMMAND = 0x803,
        INVALID_SPECIAL_COMMAND = 0x804,
        INVALID_HANDLE = 0x805,
        INVALID_PASSWORD = 0x806,
        INVALID_REALM = 0x807,
        STORAGE_ACL_ENTRY_IN_USE = 0x808,
        DATA_MISSING = 0x809,
        DUPLICATE = 0x80A,
        EVENTLOG_FROZEN = 0x80B,
        PKI_MISSING_KEYS = 0x80C,
        PKI_GENERATING_KEYS = 0x80D,
        INVALID_KEY = 0x80E,
        INVALID_CERT = 0x80F,
        CERT_KEY_NOT_MATCH = 0x810,
        MAX_KERB_DOMAIN_REACHED = 0x811,
        DUPLICATE_PRECEDENCE = 0x813,
        NOP = 0xFFFD,
        CALL_SKIPPED = 0xFFFE,
        FAILED_WEB_CALL = 0xFFFF
    };

    public class RemotSpecialCommandType
    {
        /// <summary>
        ///No additional special command is included; the Special
        ///Command Parameter has no meaning.
        /// </summary>
       public static readonly byte NOP = 0x00;

        /// <summary>
        ///
        /// The Special Command Parameter is used to specify a
        ///PXE parameter. When the parameter value is 0, the
        ///system default PXE device is booted. All other values for
        ///the PXE parameter are reserved for future definition by
        ///this specification.
        /// 
        /// 
        /// </summary>
        public static readonly byte ForcePxeBoot = 0x01;

        /// <summary>
        /// The Special Command Parameter identifies the bootmedia
        ///index for the managed client. When the parameter
        ///value is 0, the default hard-drive is booted; when the
        ///parameter value is 1, the primary hard drive is booted;
        ///when the value is 2, the secondary hard drive is booted -
        ///and so on.
        /// </summary>
        public static readonly byte ForceHardDriveBoot =0x02;

        /// <summary>
        /// The Special Command Parameter identifies the bootmedia
        ///index for the managed client. When the parameter
        ///value is 0, the default hard drive is booted; when the
        ///parameter value is 1, the primary hard drive is booted;
        ///when the value is 2, the secondary hard drive is booted -
        ///and so on.
        /// </summary>
        public static readonly byte ForceHardDriveSafeModeBoot = 0x03;

        /// <summary>
        /// The Special Command Parameter can be used to specify
        ///a diagnostic parameter. When the parameter value is 0,
        ///the default diagnostic media is booted. All other values
        ///for the diagnostic parameter are reserved for future
        ///definition by this specification.
        /// </summary>
        public static readonly byte ForceDiagnosticsBoot = 0x04;

        /// <summary>
        /// The Special Command Parameter identifies the bootmedia
        ///index for the managed client. When the parameter
        ///value is 0, the default CD/DVD is booted; when the
        /// value is 1, the primary CD/DVD is booted;
        ///when the value is 2, the secondary CD/DVD is booted;
        /// so on.
        /// </summary>
        public static readonly byte ForceCdOrDvdBoot = 0x05;

        /// <summary>
        /// The Special Command Parameter identifies the special
        ///Intel boot options (IDER, SOL etc). The Intel AMT boot
        ///options are defined in Intel AMT Proprietary Special
        ///Command parameters.
        /// </summary>
        public static readonly byte IntelOemCommand = 0x0c1;
    };


    /// <summary>
    /// @Author:Karim Wadie
    /// @Date:28\3\08
    /// @Update:
    /// 
    /// 
    /// @Description
    /// RemoteControlCommandType enumerates the remote boot and power state commands supported
    /// by the Intel AMT device via the Remote Control Interface.
    /// 
    /// @Refrence:
    /// - Network Interface Guide.pdf : Remote Control Interface
    /// 
    /// </summary>
    public class RemoteControlCommandType
    {
        /// <summary>
        /// The reset function causes a low latency reset of the system. This reset
        ///must, at a minimum, reset the host processor(s) and cause the PCI
        ///Reset# to be asserted so that all devices on the PCI bus are initialized.
        /// </summary>
       public static readonly byte Reset = 0x10;

        /// <summary>
        /// The power-up function brings a sleeping system into the S0/G0
        ///“working” state or into the Legacy ON state.
        /// </summary>
        public static readonly byte PowerUp = 0x11;

        /// <summary>
        /// The unconditional power-down function forces the system into an S5
        ///entered by override or a Legacy OFF powered-off state. This powerdown
        ///occurs without any blocking from software or the system. Because
        ///of this, ACPI and system state context is not guaranteed to be
        ///preserved.
        /// </summary>
        public static readonly byte PowerDown = 0x12;

        /// <summary>
        /// The power cycle reset function causes a hard reset of the system. This
        ///reset is functionally equivalent to an Unconditional Power-Down
        ///operation, followed by a Power Up operation
        /// </summary>
        public static readonly byte PowerCycleReset = 0x13;

        /// <summary>
        /// The set boot options function causes the system to use the given boot
        ///options after the next graceful shutdown or restart. This function does
        ///not change the power state of the system. This option can be used in
        ///conjunction with IDE-R session to favor a graceful boot during
        ///installation.
        /// </summary>
        public static readonly byte SetBootOptions = 0x21;

    }

    public class BootOptionsType
    {
        /// <summary>
        /// When set to 1b, the managed client’s firmware disables the
        ///power button operation for the system, normally until the next
        ///boot cycle. Client instrumentation might provide the capability
        ///to re-enable the button functionality without rebooting.
        /// </summary>
        public static readonly int LockPowerButton = 2;

        /// <summary>
        /// When set to 1b, the managed client’s firmware disables the
        ///reset button operation for the system, normally until the next
        ///boot cycle. Client instrumentation might provide the capability
        ///to re-enable the button functionality without rebooting
        /// </summary>
       public static readonly int LockResetButton = 4;

        /// <summary>
        /// When set to 1b, the managed client’s firmware disallows
        ///keyboard activity during its boot process. Client
        ///instrumentation or OS drivers might provide the capability to
        ///re-enable the keyboard functionality without rebooting.
        /// </summary>
       public static readonly int LockKeyboard = 32;

        /// <summary>
        /// When set to 1b, the managed client’s firmware disables the
        ///sleep button operation for the system, normally until the next
        ///boot cycle. Client instrumentation might provide the capability
        ///to re-enable the button functionality without rebooting.
        /// </summary>
      public static readonly int  LockSleepButton = 64;

        /// <summary>
        ///  When set to 1b, the managed client’s firmware boots the
        ///system and bypasses any user or boot password that might be
        ///set in the system. For example, this option allows a system
        ///administrator to force a system boot via PXE in an unattended
        //manner.
        /// </summary>
      public static readonly int  UserPasswordBypass = 2048;

        /// <summary>
        /// When set to 1b, the managed client’s firmware transmits all
        ///progress PET events to the alert-sending device. This option
        ///is usually used to aid in fail-to-boot problem determination
        /// </summary>
        public static readonly int ForceProgressEvents = 4096;
    }

    public class SpecialCommandParameterType
    {
       public static readonly int  UseIderFloppy = 1;  
       public static readonly int  ReflashBios = 4;   
       public static readonly int  BiosSetup = 8;  
       public static readonly int   BiosPause = 16;
        public static readonly int UseIderCD = 257;
    }
}