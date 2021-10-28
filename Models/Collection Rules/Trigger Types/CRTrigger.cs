// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types
{
    public interface CRTrigger
    {
        internal Type _triggerType { get; set; }
    }
}
