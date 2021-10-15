using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectDump : CRAction
    {
        public CollectDump(string egress, DumpType? type)
        {
            Egress = egress;
            Type = type;
        }


        // Using the CollectDumpOptions. For a real version of this, it may be better to actually create some linkage between projects to reduce duplication.

        public string Egress { get; set; }

        public DumpType? Type { get; set; } = DumpType.WithHeap;

        Type CRAction._actionType { get; set; }
    }
}
