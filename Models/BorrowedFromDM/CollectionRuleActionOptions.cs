// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    /// <summary>
    /// Options for describing the type of action to execute and the settings to pass to that action.
    /// </summary>
    [DebuggerDisplay("Action: Type = {Type}")]
    internal sealed partial class CollectionRuleActionOptions
    {
        public string Name { get; set; }

        [Required]
        public string Type { get; set; }

        public object Settings { get; set; }

        [DefaultValue(CollectionRuleActionOptionsDefaults.WaitForCompletion)]
        public bool? WaitForCompletion { get; set; }
    }
}
