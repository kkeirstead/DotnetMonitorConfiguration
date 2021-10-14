// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Text.Json.Serialization;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    public class EventPipeProvider
    {
        public string Name { get; set; }

        public string Keywords { get; set; } = "0x" + EventKeywords.All.ToString("X");

        public EventLevel EventLevel { get; set; } = EventLevel.Verbose;

        public IDictionary<string, string> Arguments { get; set; }
    }
}
