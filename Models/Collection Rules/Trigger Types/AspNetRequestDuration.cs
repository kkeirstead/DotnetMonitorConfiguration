using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetRequestDuration : CRTrigger
    {
        public AspNetRequestDuration(int requestCount, TimeSpan? requestDuration, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            RequestCount = requestCount;
            RequestDuration = (null != requestDuration) ? requestDuration : RequestDuration;
            SlidingWindowDuration = (null != slidingWindowDuration) ? slidingWindowDuration : SlidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        [Required]
        public int RequestCount { get; set; }

        [DefaultValue(AspNetRequestDurationOptionsDefaults.RequestDuration)]
        public TimeSpan? RequestDuration { get; set; }

        [DefaultValue(AspNetRequestDurationOptionsDefaults.SlidingWindowDuration)]
        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }

        Type CRTrigger._triggerType { get; set; }
    }
}
