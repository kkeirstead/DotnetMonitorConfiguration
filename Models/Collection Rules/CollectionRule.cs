// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using System.Collections.Generic;

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

        //public Type _triggerType;

        public List<CRAction> _actions = new List<CRAction> {};

        public List<CRFilter> _filters = new List<CRFilter> { };

        public CRLimit _limit;
    }
}
