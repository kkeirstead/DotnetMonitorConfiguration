using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
