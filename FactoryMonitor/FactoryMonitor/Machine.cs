using System;
using System.Collections.Generic;
using System.Text;

namespace FactoryMonitor.Models
{
    public class Machine
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public double Temperature { get; set; }
        public double OperationRate { get; set; }
    }
}
