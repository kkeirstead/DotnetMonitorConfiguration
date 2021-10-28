// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.ComponentModel;

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

        public const string _documentationLink = "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#limits";
    }
}
