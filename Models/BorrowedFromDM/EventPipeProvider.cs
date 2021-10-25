// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Text.Json.Serialization;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    public class EventPipeProvider
    {
        [Required]
        public string Name { get; set; }

        public string Keywords { get; set; }

        public EventLevel? EventLevel { get; set; }

        public Dictionary<string, string> Arguments { get; set; }
    }
}
