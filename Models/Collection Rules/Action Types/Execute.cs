using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class Execute : CRAction
    {
        public Execute(string path, string arguments, bool? ignoreExitCode)
        {
            Path = path;
            Arguments = arguments;
            IgnoreExitCode = ignoreExitCode;
        }

        public string Path { get; set; }

        public string Arguments { get; set; }

        public bool? IgnoreExitCode { get; set; }
    }
}
