using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class EventCounter : CRTrigger
    {
        public EventCounter(string name, string providerName, string counterName, double? greaterThan, double? lessThan, TimeSpan? slidingWindowDuration)
        {
            Name = name;
            ProviderName = providerName;
            CounterName = counterName;
            GreaterThan = greaterThan;
            LessThan = lessThan;
            SlidingWindowDuration = slidingWindowDuration;
        }

        public string Name { get; set; }
        public string ProviderName { get; set; }

        public string CounterName { get; set; }

        public double? GreaterThan { get; set; }

        public double? LessThan { get; set; }
        
        public TimeSpan? SlidingWindowDuration { get; set; }
    }
}
