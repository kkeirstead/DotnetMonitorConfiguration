using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectGCDump : CRAction
    {
        public CollectGCDump(string egress)
        {
            Egress = egress;
        }


        // Using the CollectGCDumpOptions. For a real version of this, it may be better to actually create some linkage between projects to reduce duplication.
        
        [Required]
        public string Egress { get; set; }

        Type CRAction._actionType { get; set; }
    }
}
