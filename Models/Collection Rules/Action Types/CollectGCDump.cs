// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectGCDump : CRAction
    {
        public CollectGCDump(string name, bool? waitForCompletion, string egress)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            Egress = egress;
        }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }

        [Required]
        public string Egress { get; set; }

        Type CRAction._actionType { get; set; }

        public const string _documentationLink = "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#collectgcdump-action";
    }
}
