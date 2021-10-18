using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectLogs : CRAction
    {
        public CollectLogs(LogLevel? defaultLevel, Dictionary<string, LogLevel?> filterSpecs, bool? useAppFilters, TimeSpan? duration, string egress, LogFormat? format)
        {
            DefaultLevel = (null != defaultLevel) ? defaultLevel : DefaultLevel;
            FilterSpecs = filterSpecs;
            UseAppFilters = (null != useAppFilters) ? useAppFilters : UseAppFilters;
            Duration = (null != duration) ? duration : Duration;
            Egress = egress;
            Format = (null != format) ? format : Format;
        }

        [DefaultValue(CollectLogsOptionsDefaults.DefaultLevel)]
        public LogLevel? DefaultLevel { get; set; }

        public Dictionary<string, LogLevel?> FilterSpecs { get; set; }

        [DefaultValue(CollectLogsOptionsDefaults.UseAppFilters)]
        public bool? UseAppFilters { get; set; }

        [DefaultValue(CollectLogsOptionsDefaults.Duration)]
        public TimeSpan? Duration { get; set; }

        [Required]
        public string Egress { get; set; }

        [DefaultValue(CollectLogsOptionsDefaults.Format)]
        public LogFormat? Format { get; set; }

        Type CRAction._actionType { get; set; }
    }
}
