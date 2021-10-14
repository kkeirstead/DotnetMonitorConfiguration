using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CRLimit
    {
        public CRLimit(int actionCount, TimeSpan? actionCountSlidingWindowDuration, TimeSpan? ruleDuration)
        {
            ActionCount = actionCount;
            ActionCountSlidingWindowDuration = actionCountSlidingWindowDuration;
            RuleDuration = ruleDuration;
        }

        public int ActionCount { get; set; }
        public TimeSpan? ActionCountSlidingWindowDuration { get; set; }

        public TimeSpan? RuleDuration { get; set; }
    }
}
