// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class Execute : CRAction
    {
        public Execute(string name, bool? waitForCompletion, string path, string arguments, bool? ignoreExitCode)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            Path = path;
            Arguments = arguments;
            IgnoreExitCode = (null != ignoreExitCode) ? ignoreExitCode : IgnoreExitCode;
        }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }

        [Required]
        public string Path { get; set; }

        public string Arguments { get; set; }

        [DefaultValue(ExecuteOptionsDefaults.IgnoreExitCode)]
        public bool? IgnoreExitCode { get; set; }

        Type CRAction._actionType { get; set; }

        public const string _documentationLink = "https://github.com/dotnet/dotnet-monitor/blob/main/documentation/configuration.md#execute-action";
    }
}
