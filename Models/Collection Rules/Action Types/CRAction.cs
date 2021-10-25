// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types
{
    public interface CRAction
    {
        internal Type _actionType { get; set; }

        public string Name { get; set; }

        public bool? WaitForCompletion { get; set; }
    }
}
