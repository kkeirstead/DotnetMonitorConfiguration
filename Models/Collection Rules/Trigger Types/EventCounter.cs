using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class EventCounter : CRTrigger
    {
        public EventCounter(string providerName, string counterName, double? greaterThan, double? lessThan, TimeSpan? slidingWindowDuration)
        {
            ProviderName = providerName;
            CounterName = counterName;
            GreaterThan = greaterThan;
            LessThan = lessThan;
            SlidingWindowDuration = (null != slidingWindowDuration) ? slidingWindowDuration : SlidingWindowDuration;
        }

        [Required]
        public string ProviderName { get; set; }

        [Required]
        public string CounterName { get; set; }

        public double? GreaterThan { get; set; }

        public double? LessThan { get; set; }

        [DefaultValue(EventCounterOptionsDefaults.SlidingWindowDuration)]
        public TimeSpan? SlidingWindowDuration { get; set; }

        Type CRTrigger._triggerType { get; set; }
    }
}
