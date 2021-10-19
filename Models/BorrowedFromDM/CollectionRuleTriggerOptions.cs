// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    /// <summary>
    /// Options for describing the type of trigger and the settings to pass to that trigger.
    /// </summary>
    [DebuggerDisplay("Trigger: Type = {Type}")]
    internal sealed partial class CollectionRuleTriggerOptions
    {
        [Required]
        public string Type { get; set; }

        public object Settings { get; set; }
    }
}
