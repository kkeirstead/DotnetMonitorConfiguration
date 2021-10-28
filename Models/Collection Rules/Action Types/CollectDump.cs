// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectDump : CRAction
    {
        public CollectDump(string name, bool? waitForCompletion, string egress, DumpType? type)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            Egress = egress;
            Type = (null != type) ? type : Type;
        }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }

        [Required]
        public string Egress { get; set; }

        [DefaultValue(CollectDumpOptionsDefaults.Type)]
        public DumpType? Type { get; set; }

        Type CRAction._actionType { get; set; }

        public const string _documentationLink = "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#collectdump-action";
    }
}
