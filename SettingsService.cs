using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryMonitor
{
    internal static class SettingsService
    {
        private const ushort MAX_CHARGE = 95;
        private const ushort MIN_CHARGE = 15;
        private const bool RUN_AT_STARTUP = false;
        private const string DB_STRING = "{0};{1};{2}";
        private const string DB_NAME = "settings.dat";


        public static Settings LoadSettings()
        {
            Settings settings;
            string settingsString = "";
            if (AppContext.BaseDirectory != null)
            {

                if (File.Exists(AppContext.BaseDirectory + DB_NAME))
                {
                    settingsString = File.ReadAllText(AppContext.BaseDirectory + DB_NAME);
                }
                else
                {
                    settingsString = String.Format(DB_STRING, MIN_CHARGE, MAX_CHARGE, RUN_AT_STARTUP);
                    File.WriteAllText(AppContext.BaseDirectory + DB_NAME, settingsString);

                }
            }
            else
            {
                settingsString = String.Format(DB_STRING, MIN_CHARGE, MAX_CHARGE, RUN_AT_STARTUP);

            }
            var _settings = settingsString.Split('\u003B');
            settings = new Settings()
            {
                MinCharge = Convert.ToUInt16(_settings[0]),
                MaxCharge = Convert.ToUInt16(_settings[1]),
                RunAtStartup = Convert.ToBoolean(_settings[2])
            };



            return settings;
        }

        public static bool SaveSettings(ushort MinCharge, ushort MaxCharge, bool RunAtStartup)
        {
            try
            {
                string settingsString = String.Format(DB_STRING, MinCharge, MaxCharge, RunAtStartup);
                File.WriteAllText(AppContext.BaseDirectory + DB_NAME, settingsString);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
    }
}
