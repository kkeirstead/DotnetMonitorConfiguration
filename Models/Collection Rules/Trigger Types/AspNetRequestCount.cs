// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public class AspNetRequestCount : CRTrigger
    {
        public AspNetRequestCount(int requestCount, TimeSpan? slidingWindowDuration, string[] includePaths, string[] excludePaths)
        {
            RequestCount = requestCount;
            SlidingWindowDuration = (null != slidingWindowDuration) ? slidingWindowDuration : SlidingWindowDuration;
            IncludePaths = includePaths;
            ExcludePaths = excludePaths;
        }

        [Required]
        public int RequestCount { get; set; }

        [DefaultValue(AspNetRequestCountOptionsDefaults.SlidingWindowDuration)]
        public TimeSpan? SlidingWindowDuration { get; set; }

        public string[] IncludePaths { get; set; }

        public string[] ExcludePaths { get; set; }

        Type CRTrigger._triggerType { get; set; }

        public const string _documentationLink = "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#aspnetrequestcount-trigger";
    }
}
