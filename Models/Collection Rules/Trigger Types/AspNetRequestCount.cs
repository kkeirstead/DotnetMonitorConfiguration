using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetRequestCount : CRTrigger
    {
        public AspNetRequestCount(int requestCount, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            RequestCount = requestCount;
            SlidingWindowDuration = slidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        public int RequestCount { get; set; }

        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }

        Type CRTrigger._triggerType { get; set; }
    }
}
