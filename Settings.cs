﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatteryMonitor
{
    internal class Settings
    {
        public ushort MinCharge { get; set; }
        public ushort MaxCharge { get; set; } 
        public bool RunAtStartup { get;set; }

    }
}
