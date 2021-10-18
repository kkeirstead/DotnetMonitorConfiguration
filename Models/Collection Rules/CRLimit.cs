using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CRLimit
    {
        public CRLimit(int? actionCount, TimeSpan? actionCountSlidingWindowDuration, TimeSpan? ruleDuration)
        {
            ActionCount = (null != actionCount) ? actionCount.Value : ActionCount;
            ActionCountSlidingWindowDuration = actionCountSlidingWindowDuration;
            RuleDuration = ruleDuration;
        }

        [DefaultValue(CollectionRuleLimitsOptionsDefaults.ActionCount)]
        public int? ActionCount { get; set; }

        public TimeSpan? ActionCountSlidingWindowDuration { get; set; }

        public TimeSpan? RuleDuration { get; set; }
    }
}
