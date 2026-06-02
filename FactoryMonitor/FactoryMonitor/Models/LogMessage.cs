using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMonitor.Models
{
    public class LogMessage
    {
        public DateTime Time { get; set; }

        public string Message { get; set; } = string.Empty;

        public string TimeText => Time.ToString("HH:mm:ss");
    }
}
