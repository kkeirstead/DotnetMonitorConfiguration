using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public interface CRTrigger
    {
        internal Type _triggerType { get; set; }

    }
}
