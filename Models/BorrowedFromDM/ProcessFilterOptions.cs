// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    public enum ProcessFilterKey
    {
        ProcessId,
        ProcessName,
        CommandLine,
    }

    public enum ProcessFilterType
    {
        Exact,
        Contains,
    }

    public sealed class ProcessFilterOptions
    {
        public List<ProcessFilterDescriptor> Filters { get; set; } = new List<ProcessFilterDescriptor>(0);
    }

    public sealed class ProcessFilterDescriptor
    {
        [Required]
        public ProcessFilterKey Key { get;set; }

        [Required]
        public string Value { get; set; }

        [DefaultValue(ProcessFilterType.Exact)]
        public ProcessFilterType MatchType { get; set; } = ProcessFilterType.Exact;
    }
}
