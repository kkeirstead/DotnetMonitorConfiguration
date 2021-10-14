using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetResponseStatus : CRTrigger
    {
        public AspNetResponseStatus(string name, string[] statusCodes, int requestCount, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            Name = name;
            StatusCodes = statusCodes;
            RequestCount = requestCount;
            SlidingWindowDuration = slidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        public string Name { get; set; }
        public string[] StatusCodes { get; set; }

        public int RequestCount { get; set; }

        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }
    }
}
