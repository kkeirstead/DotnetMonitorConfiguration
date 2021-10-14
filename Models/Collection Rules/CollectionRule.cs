using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CollectionRule : ConfigurationItem
    {
        public CollectionRule(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public CRTrigger _trigger;

        public Type _triggerType;

        public List<CRAction> _actions = new List<CRAction> {};

        public List<CRFilter> _filters = new List<CRFilter> { };

        public CRLimit _limit;
    }
}
