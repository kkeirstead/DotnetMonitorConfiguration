using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public interface CRAction
    {
        internal Type _actionType { get; set; }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }
    }
}
