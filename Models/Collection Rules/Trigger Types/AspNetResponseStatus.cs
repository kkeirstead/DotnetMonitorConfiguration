using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetResponseStatus : CRTrigger
    {
        public AspNetResponseStatus(string[] statusCodes, int requestCount, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            StatusCodes = statusCodes;
            RequestCount = requestCount;
            SlidingWindowDuration = (null != slidingWindowDuration) ? slidingWindowDuration : SlidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        [Required]
        public string[] StatusCodes { get; set; }

        [Required]
        public int RequestCount { get; set; }

        [DefaultValue(AspNetResponseStatusOptionsDefaults.SlidingWindowDuration)]
        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }

        Type CRTrigger._triggerType { get; set; }
    }
}
