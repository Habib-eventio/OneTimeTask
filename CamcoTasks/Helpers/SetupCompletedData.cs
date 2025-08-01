using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CamcoTasks.Data.Other
{
    public class SetupCompletedData
    {
        public string Employee { get; set; }
        public string PartNumber { get; set; }
        public int CountOfSetupsCompleted { get; set; }
        public double AvgSetupTimePerPart { get; set; }
        public string OpNumber { get; set; }
    }
}
