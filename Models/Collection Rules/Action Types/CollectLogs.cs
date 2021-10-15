using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectLogs : CRAction
    {
        public CollectLogs(LogLevel? defaultLevel, Dictionary<string, LogLevel?> filterSpecs, bool? useAppFilters, TimeSpan? duration, string egress, LogFormat? format)
        {
            DefaultLevel = defaultLevel;
            FilterSpecs = filterSpecs;
            UseAppFilters = useAppFilters;
            Duration = duration;
            Egress = egress;
            Format = format;
        }
        public LogLevel? DefaultLevel { get; set; }

        public Dictionary<string, LogLevel?> FilterSpecs { get; set; }

        public bool? UseAppFilters { get; set; }

        public TimeSpan? Duration { get; set; }

        public string Egress { get; set; }

        public LogFormat? Format { get; set; }

        Type CRAction._actionType { get; set; }
    }
}
