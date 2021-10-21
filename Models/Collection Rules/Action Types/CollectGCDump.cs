using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

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
    }
}
