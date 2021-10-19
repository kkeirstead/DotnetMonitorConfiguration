// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    /// <summary>
    /// Options for limiting the execution of a collection rule.
    /// </summary>
    internal sealed class CollectionRuleLimitsOptions
    {
        [DefaultValue(CollectionRuleLimitsOptionsDefaults.ActionCount)]
        public int? ActionCount { get; set; }

        [Range(typeof(TimeSpan), CollectionRuleOptionsConstants.ActionCountSlidingWindowDuration_MinValue, CollectionRuleOptionsConstants.ActionCountSlidingWindowDuration_MaxValue)]
        public TimeSpan? ActionCountSlidingWindowDuration { get; set; }

        [Range(typeof(TimeSpan), CollectionRuleOptionsConstants.RuleDuration_MinValue, CollectionRuleOptionsConstants.RuleDuration_MaxValue)]
        public TimeSpan? RuleDuration { get; set; }
    }
}
