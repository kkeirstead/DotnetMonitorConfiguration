// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.Extensions.Logging;

namespace DotnetMonitorConfiguration.Models.BorrowedFromDM
{
    internal static class CollectLogsOptionsDefaults
    {
        public const LogLevel DefaultLevel = LogLevel.Warning;
        public const bool UseAppFilters = true;
        public const string Duration = "00:00:30";
        public const LogFormat Format = LogFormat.PlainText;
    }
}
