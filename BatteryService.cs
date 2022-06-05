using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;

namespace BatteryMonitor
{
    internal static class BatteryStatus
    {
        public const ushort Discharging = 1;
        public const ushort ACPower = 2;
        public const ushort FullCharged = 3;
        public const ushort Low = 4;
        public const ushort CriticallyLow = 5;
        public const ushort Charging = 6;
        public const ushort ChargingWithHigh = 7;
        public const ushort ChargingWithLow = 8;
        public const ushort Undefined = 9;
        public const ushort PartiallyCharged = 10;
    }
    internal static class BatteryService
    {
        private const string GET_BATTERY_NAMES = "select Name from Win32_Battery";
        private const string GET_BATTERY_DETAILS = "select * from Win32_Battery where Name=\"{0}\"";
        private const ushort CHARGE_VARIATION = 0;

        public enum BatteryStatus
        {

        }

        private static Dictionary<ushort, string> StatusCodes = new Dictionary<ushort, string>()
        {
            {1, "Discharging" },
            {2, "Charging"},
            {3, "Fully Charged"},
            {4, "Low"},
            {5, "Critically Low"},
            {6, "Charging"},
            {7, "Charging and High"},
            {8, "Charging and Low"},
            {9, "Undefined"},
            {10,"Partially Charged"}
        };

        private static Dictionary<ushort, string> BatteryType = new Dictionary<ushort, string>()
        {
            {1, "Other" },
            {2, "Unknwon" },
            {3, "Lead Acid" },
            {4, "Nicket-Cadmium" },
            {5, "Nickel Metal Hydride" },
            {6, "Lithium ion" },
            {7, "Zinc air" },
            {8, "Lithium Polymer" }
        };

        public static List<string> GetBatteries()
        {
            List<string> batteries = new List<string>();

            ManagementObjectSearcher mos = new ManagementObjectSearcher(GET_BATTERY_NAMES);
            foreach (ManagementBaseObject mo in mos.Get())
            {
                if (mo["Name"] != null)
                {
                    batteries.Add(mo["Name"].ToString());
                }
            }


            return batteries;
        }

        public static Battery GetBatteryInfo(string batteryName)
        {
            Battery battery = new Battery();
            ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format(GET_BATTERY_DETAILS, batteryName));

            foreach (ManagementBaseObject mo in mos.Get())
            {
                if (mo["Availability"] != null)
                    battery.Availability = (ushort)mo["Availability"];

                if (mo["BatteryRechargeTime"] != null)
                    battery.BatteryRechargeTime = (uint)mo["BatteryRechargeTime"];

                if (mo["BatteryStatus"] != null)
                {
                    battery.BatteryStatus = (ushort)mo["BatteryStatus"];
                    battery.BatteryStatusString = StatusCodes[battery.BatteryStatus];
                }

                if (mo["Caption"] != null)
                    battery.Caption = (string)mo["Caption"];

                if (mo["Chemistry"] != null)
                {
                    battery.BatteryType = (ushort)mo["Chemistry"];
                    battery.BatteryTypeString = BatteryType[battery.BatteryType];
                }

                if (mo["ConfigManagerErrorCode"] != null)
                    battery.ConfigManagerErrorCode = (uint)mo["ConfigManagerErrorCode"];

                if (mo["ConfigManagerUserConfig"] != null)
                    battery.ConfigManagerUserConfig = (bool)mo["ConfigManagerUserConfig"];

                if (mo["CreationClassName"] != null)
                    battery.CreationClassName = (string)mo["CreationClassName"];

                if (mo["CreationClassName"] != null)
                    battery.Description = (string)mo["CreationClassName"];

                if (mo["DesignCapacity"] != null)
                    battery.DesignCapacity = (uint)mo["DesignCapacity"];

                if (mo["DesignVoltage"] != null)
                    battery.DesignVoltage = (ulong)mo["DesignVoltage"];

                if (mo["DeviceID"] != null)
                    battery.DeviceID = (string)mo["DeviceID"];

                if (mo["ErrorCleared"] != null)
                    battery.ErrorCleared = (bool)mo["ErrorCleared"];

                if (mo["ErrorDescription"] != null)
                    battery.ErrorDescription = (string)mo["ErrorDescription"];

                if (mo["EstimatedChargeRemaining"] != null)
                {
                    battery.EstimatedChargeRemaining = (ushort)mo["EstimatedChargeRemaining"];
                    battery.EstimatedChargeRemaining += CHARGE_VARIATION;
                }

                if (mo["EstimatedRunTime"] != null)
                    battery.EstimatedRunTime = (uint)mo["EstimatedRunTime"];

                if (mo["ExpectedBatteryLife"] != null)
                    battery.ExpectedBatteryLife = (uint)mo["ExpectedBatteryLife"];

                if (mo["ExpectedLife"] != null)
                    battery.ExpectedLife = (uint)mo["ExpectedLife"];

                if (mo["FullChargeCapacity"] != null)
                    battery.FullChargeCapacity = (uint)mo["FullChargeCapacity"];

                if (mo["InstallDate"] != null)
                    battery.InstallDate = (DateTime)mo["InstallDate"];

                if (mo["LastErrorCode"] != null)
                    battery.LastErrorCode = (uint)mo["LastErrorCode"];

                if (mo["MaxRechargeTime"] != null)
                    battery.MaxRechargeTime = (uint)mo["MaxRechargeTime"];

                if (mo["Name"] != null)
                    battery.Name = (string)mo["Name"];

                if (mo["PNPDeviceID"] != null)
                    battery.PNPDeviceID = (string)mo["PNPDeviceID"];

                if (mo["PowerManagementSupported"] != null)
                    battery.PowerManagementSupported = (bool)mo["PowerManagementSupported"];

                if (mo["SmartBatteryVersion"] != null)
                    battery.SmartBatteryVersion = (string)mo["SmartBatteryVersion"];

                if (mo["StatusInfo"] != null)
                    battery.StatusInfo = (ushort)mo["StatusInfo"];

                if (mo["SystemCreationClassName"] != null)
                    battery.SystemCreationClassName = (string)mo["SystemCreationClassName"];

                if (mo["SystemName"] != null)
                    battery.SystemName = (string)mo["SystemName"];

                if (mo["TimeOnBattery"] != null)

                    battery.TimeOnBattery = (uint)mo["TimeOnBattery"];
                if (mo["TimeToFullCharge"] != null)
                    battery.TimeToFullCharge = (uint)mo["TimeToFullCharge"];

                if (mo["Status"] != null)
                    battery.Status = (string)mo["Status"];
            }

            return battery;
        }

        public static BatteryStat GetBatteryStat(string batteryName)
        {
            BatteryStat battery = new BatteryStat();
            ManagementObjectSearcher mos = new ManagementObjectSearcher(String.Format(GET_BATTERY_DETAILS, batteryName));

            foreach (ManagementBaseObject mo in mos.Get())
            {

                if (mo["BatteryRechargeTime"] != null)
                    battery.BatteryRechargeTime = (uint)mo["BatteryRechargeTime"];

                if (mo["BatteryStatus"] != null)
                {
                    battery.BatteryStatus = (ushort)mo["BatteryStatus"];
                    battery.BatteryStatusString = StatusCodes[battery.BatteryStatus];
                }

                if (mo["EstimatedChargeRemaining"] != null)
                {
                    battery.EstimatedChargeRemaining = (ushort)mo["EstimatedChargeRemaining"];
                    battery.EstimatedChargeRemaining += CHARGE_VARIATION;
                }

                if (mo["EstimatedRunTime"] != null)
                    battery.EstimatedRunTime = (uint)mo["EstimatedRunTime"];

                if (mo["Name"] != null)
                    battery.Name = (string)mo["Name"];

                if (mo["StatusInfo"] != null)
                    battery.StatusInfo = (ushort)mo["StatusInfo"];

                if (mo["TimeOnBattery"] != null)
                    battery.TimeOnBattery = (uint)mo["TimeOnBattery"];

                if (mo["TimeToFullCharge"] != null)
                    battery.TimeToFullCharge = (uint)mo["TimeToFullCharge"];

                if (mo["Status"] != null)
                    battery.Status = (string)mo["Status"];
            }

            return battery;
        }


    }
}
