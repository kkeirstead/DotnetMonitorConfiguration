// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public class CollectTrace : CRAction
    {
        // This should never end up being used (we shouldn't have a profile and providers
        public CollectTrace(string name, bool? waitForCompletion, TraceProfile? profile, List<EventPipeProvider> providers, bool? requestRundown, int? bufferSizeMegabytes, TimeSpan? duration, string egress)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            Profile = profile;
            Providers = providers;
            RequestRundown = (null != requestRundown) ? requestRundown : RequestRundown;
            BufferSizeMegabytes = (null != bufferSizeMegabytes) ? bufferSizeMegabytes : BufferSizeMegabytes;
            Duration = (null != duration) ? duration : Duration;
            Egress = egress;
        }

        public CollectTrace(string name, bool? waitForCompletion, bool? requestRundown, int? bufferSizeMegabytes, TimeSpan? duration, string egress)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            RequestRundown = (null != requestRundown) ? requestRundown : RequestRundown;
            BufferSizeMegabytes = (null != bufferSizeMegabytes) ? bufferSizeMegabytes : BufferSizeMegabytes;
            Duration = (null != duration) ? duration : Duration;
            Egress = egress;
        }

        public CollectTrace(string name, bool? waitForCompletion, TraceProfile? profile, TimeSpan? duration, string egress)
        {
            Name = name;
            WaitForCompletion = waitForCompletion;
            Profile = profile;
            Duration = (null != duration) ? duration : Duration;
            Egress = egress;
        }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }

        [Profile]
        [Required]
        public TraceProfile? Profile { get; set; }

        [Providers]
        [Required]
        public List<EventPipeProvider> Providers { get; set; }

        [Providers]
        [DefaultValue(CollectTraceOptionsDefaults.RequestRundown)]
        public bool? RequestRundown { get; set; }

        [Providers]
        [DefaultValue(CollectTraceOptionsDefaults.BufferSizeMegabytes)]
        public int? BufferSizeMegabytes { get; set; }

        [DefaultValue(CollectTraceOptionsDefaults.Duration)]
        public TimeSpan? Duration { get; set; }

        [Required]
        public string Egress { get; set; }

        Type CRAction._actionType { get; set; }

        internal bool IsProviders { get; set; }
    }




    [System.AttributeUsage(AttributeTargets.All,
                       AllowMultiple = true) 
    ]
    public class ProvidersAttribute : System.Attribute
    { }

    [System.AttributeUsage(AttributeTargets.All,
                   AllowMultiple = true)
]
    public class ProfileAttribute : System.Attribute
    { }
}
