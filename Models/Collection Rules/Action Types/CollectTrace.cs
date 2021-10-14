using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
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
            RequestRundown = requestRundown;
            BufferSizeMegabytes = bufferSizeMegabytes;
            Duration = duration;
            Egress = egress;
        }
        public TraceProfile? Profile { get; set; }

        public List<EventPipeProvider> Providers { get; set; }

        public bool? RequestRundown { get; set; }

        public int? BufferSizeMegabytes { get; set; }

        public TimeSpan? Duration { get; set; }

        public string Egress { get; set; }
    }
}
