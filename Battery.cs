
namespace BatteryMonitor
{
    public class Battery
    {
        public UInt16 Availability { get; set; }
        public UInt32 BatteryRechargeTime { get; set; }
        public UInt16 BatteryStatus { get; set; }
        public string? BatteryStatusString { get; set; }
        public string? Caption { get; set; }
        public UInt16 BatteryType { get; set; }
        public string? BatteryTypeString { get; set; }
        public UInt32 ConfigManagerErrorCode { get; set; }
        public Boolean ConfigManagerUserConfig { get; set; }
        public string? CreationClassName { get; set; }
        public string? Description { get; set; }
        public UInt32 DesignCapacity { get; set; }
        public UInt64 DesignVoltage { get; set; }
        public string? DeviceID { get; set; }
        public Boolean ErrorCleared { get; set; }
        public string? ErrorDescription { get; set; }
        public UInt16 EstimatedChargeRemaining { get; set; }
        public UInt32 EstimatedRunTime { get; set; }
        public UInt32 ExpectedBatteryLife { get; set; }
        public UInt32 ExpectedLife { get; set; }
        public UInt32 FullChargeCapacity { get; set; }
        public DateTime InstallDate { get; set; }
        public UInt32 LastErrorCode { get; set; }
        public UInt32 MaxRechargeTime { get; set; }
        public string? Name { get; set; }
        public string? PNPDeviceID { get; set; }
        public Boolean PowerManagementSupported { get; set; }
        public string? SmartBatteryVersion { get; set; }
        public string? Status { get; set; }
        public UInt16 StatusInfo { get; set; }
        public string? SystemCreationClassName { get; set; }
        public string? SystemName { get; set; }
        public UInt32 TimeOnBattery { get; set; }
        public UInt32 TimeToFullCharge { get; set; }

    }


    public class BatteryStat
    {
        public UInt32 BatteryRechargeTime { get; set; }
        public UInt16 BatteryStatus { get; set; }
        public string? BatteryStatusString { get; set; }
        public UInt16 EstimatedChargeRemaining { get; set; }
        public UInt32 EstimatedRunTime { get; set; }
        public string? Status { get; set; }
        public UInt16 StatusInfo { get; set; }
        public UInt32 TimeOnBattery { get; set; }
        public UInt32 TimeToFullCharge { get; set; }
        public string? Name { get; set; }
    }
}


