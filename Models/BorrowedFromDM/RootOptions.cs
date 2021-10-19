// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules;
using System.Collections.Generic;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    internal partial class RootOptions
    {
        //public AuthenticationOptions Authentication { get; set; }

        public IDictionary<string, CollectionRule> CollectionRules { get; }
            = new Dictionary<string, CollectionRule>(0);

        //public GlobalCounterOptions GlobalCounter { get; set; }

        //public CorsConfigurationOptions CorsConfiguration { get; set; }

        //public DiagnosticPortOptions DiagnosticPort { get; set; }

        //public EgressOptions Egress { get; set; }

        //public MetricsOptions Metrics { get; set; }

        //public StorageOptions Storage { get; set; }

        //public ProcessFilterOptions DefaultProcess { get; set; }
    }
}
