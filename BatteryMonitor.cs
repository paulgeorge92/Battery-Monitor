using BatteryMonitor.Properties;
using System;
using Timer = System.Threading.Timer;
namespace BatteryMonitor
{
    public class BatteryMonitor : ApplicationContext
    {
        private NotifyIcon trayIcon;
        private string primaryBattery = "";
        private Settings settings;
        private Timer timer = null;
        public BatteryMonitor()
        {
            ContextMenuStrip trayMenu = new();
            
            trayMenu.Items.Add("Show Battery Information", null, ShowBatteryInformation);
            _ = trayMenu.Items.Add("Settings", null, ShowSettings);
            _ = trayMenu.Items.Add("Exit", null, Exit);

            settings = SettingsService.LoadSettings();

            trayIcon = new NotifyIcon()
            {
                Icon = Resources.BatteryOK,
                ContextMenuStrip = trayMenu,
                Visible = true,
                BalloonTipIcon = ToolTipIcon.Info,
                BalloonTipTitle = "Battery Monitor"
            };

            var batteries = BatteryService.GetBatteries();
            if (batteries.Count > 0)
            {
                primaryBattery = batteries[0];

            }

            if (string.IsNullOrEmpty(primaryBattery))
            {
                trayIcon.BalloonTipText = "Battery not found. Application will now exit";
                trayIcon.BalloonTipIcon = ToolTipIcon.Error;
                trayIcon.ShowBalloonTip(2000);
                System.Threading.Thread.Sleep(2000);
                Application.Exit();
            }
            else
            {
                updateTrayIcon(BatteryService.GetBatteryStat(primaryBattery), true);
                
            }

            SetTimerJob();

        }

        void SetTimerJob()
        {
            timer = new Timer(GetBatteryStat, null, 5000, 5000);
        }

        void GetBatteryStat(object sender)
        {
            settings = SettingsService.LoadSettings();
            updateTrayIcon(BatteryService.GetBatteryStat(primaryBattery));
        }

        void updateTrayIcon(BatteryStat batteryStats, bool initialRun = false)
        {
            bool isCharging = (new ushort[] { BatteryStatus.Charging, BatteryStatus.ACPower, BatteryStatus.ChargingWithHigh, BatteryStatus.ChargingWithLow }).Contains(batteryStats.BatteryStatus);

            if (isCharging)
            {
                trayIcon.Icon = Resources.BatteryCharging;
            }
            else
            {
                trayIcon.Icon = Resources.BatteryOK;
            }


            if (batteryStats.BatteryStatus == BatteryStatus.Low || batteryStats.BatteryStatus == BatteryStatus.CriticallyLow || (batteryStats.EstimatedChargeRemaining <= settings.MinCharge && !isCharging))
            {
                trayIcon.Icon = Resources.BatteryLow;
                trayIcon.BalloonTipText = String.Format("Battery is Low ({0}%). Please connect to a charger", batteryStats.EstimatedChargeRemaining);
                trayIcon.BalloonTipIcon = ToolTipIcon.Warning;
                trayIcon.ShowBalloonTip(2000);
            }
            else if (isCharging && batteryStats.EstimatedChargeRemaining >= settings.MaxCharge)
            {
                trayIcon.BalloonTipText = String.Format("Battery is sufficienty charged ({0}%). Disconnect charger to save battery", batteryStats.EstimatedChargeRemaining);
                trayIcon.BalloonTipIcon = ToolTipIcon.Info;
                trayIcon.ShowBalloonTip(2000);
            }
            else if(initialRun)
            {
                trayIcon.BalloonTipText = String.Format("Battery is {0}. Current Percentange: {1}%", batteryStats.BatteryStatusString, batteryStats.EstimatedChargeRemaining);
                trayIcon.ShowBalloonTip(2000);
            }
            
            
        }

        void ShowBatteryInformation(object sender, EventArgs e)
        {
            var batteryInfoForm = new BatteryInformation();
            batteryInfoForm.ShowDialog();
        }

        void ShowSettings(object sender, EventArgs e)
        {
            var settigsForm = new SettingsForm();
            DialogResult res = settigsForm.ShowDialog();
            if (res == DialogResult.OK)
            {
                GetBatteryStat(null);
            }
        }

        void Exit(object sender, EventArgs e)
        {
            trayIcon.Visible = false;
            Application.Exit();
        }

        
    }
}
