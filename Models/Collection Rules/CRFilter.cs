// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CRFilter
    {
        public CRFilter(ProcessFilterKey key, string value, ProcessFilterType? matchType)
        {
            Key = key;
            Value = value;
            MatchType = (null != matchType) ? matchType : MatchType;
        }

        [Required]
        public ProcessFilterKey Key { get; set; }

        [Required]
        public string Value { get; set; }

        [DefaultValue(ProcessFilterType.Exact)]
        public ProcessFilterType? MatchType { get; set; }

        internal string _documentationLink { get { return "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#filters"; } }
    }
}
