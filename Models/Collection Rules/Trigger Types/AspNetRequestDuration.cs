using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetRequestDuration : CRTrigger
    {
        public AspNetRequestDuration(string name, int requestCount, TimeSpan? requestDuration, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            Name = name;
            RequestCount = requestCount;
            RequestDuration = requestDuration;
            SlidingWindowDuration = slidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        public string Name { get; set; }
        public int RequestCount { get; set; }

        public TimeSpan? RequestDuration { get; set; }

        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }

    }
}
