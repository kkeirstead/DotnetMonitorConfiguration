// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    /// <summary>
    /// Options for describing an entire collection rule.
    /// </summary>
    internal sealed partial class CollectionRuleOptions
    {
        //public List<ProcessFilterDescriptor> Filters { get; } = new List<ProcessFilterDescriptor>(0);

        public List<CRFilter> Filters { get; } = new List<CRFilter>(0);

        [Required]
        public CollectionRuleTriggerOptions Trigger { get; set; }

        public List<CollectionRuleActionOptions> Actions { get; } = new List<CollectionRuleActionOptions>(0);

        //public CollectionRuleLimitsOptions Limits { get; set; }

        public CRLimit Limits { get; set; }

    }
}
