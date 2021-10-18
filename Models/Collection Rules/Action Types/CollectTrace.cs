using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectTrace : CRAction
    {
        public CollectTrace(TraceProfile? profile, List<EventPipeProvider> providers, bool? requestRundown, int? bufferSizeMegabytes, TimeSpan? duration, string egress)
        {
            Profile = profile;
            Providers = providers;
            RequestRundown = (null != requestRundown) ? requestRundown : RequestRundown;
            BufferSizeMegabytes = (null != bufferSizeMegabytes) ? bufferSizeMegabytes : BufferSizeMegabytes;
            Duration = (null != duration) ? duration : Duration;
            Egress = egress;
        }
        public TraceProfile? Profile { get; set; }

        public List<EventPipeProvider> Providers { get; set; }

        [DefaultValue(CollectTraceOptionsDefaults.RequestRundown)]
        public bool? RequestRundown { get; set; }

        [DefaultValue(CollectTraceOptionsDefaults.BufferSizeMegabytes)]
        public int? BufferSizeMegabytes { get; set; }

        [DefaultValue(CollectTraceOptionsDefaults.Duration)]
        public TimeSpan? Duration { get; set; }

        [Required]
        public string Egress { get; set; }

        Type CRAction._actionType { get; set; }
    }
}
