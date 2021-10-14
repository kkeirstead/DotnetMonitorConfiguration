using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using System.Collections.Generic;

namespace DotnetMonitorConfiguration.Models
{
    public class General
    {
        public static IEnumerable<string> _triggerTypes = new List<string>() { nameof(AspNetRequestCount), nameof(AspNetRequestDuration), nameof(AspNetResponseStatus), nameof(EventCounter) };

        public static IEnumerable<string> _actionTypes = new List<string>() { nameof(CollectDump), nameof(CollectGCDump), nameof(CollectLogs), nameof(CollectTrace), nameof(Execute) };

        public static IList<CollectionRule> _collectionRules = new List<CollectionRule>() { };
    }
}
