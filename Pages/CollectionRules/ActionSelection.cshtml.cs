// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ActionSelectionModel : PageModel
    {
        private readonly ILogger<ActionSelectionModel> _logger;

        [BindProperty]
        public string actionType { get; set; }

        public ActionSelectionModel(ILogger<ActionSelectionModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPostSubmit(string data)
        {
            Type t = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types." + data);

            if (typeof(CollectTrace) == t)
            {
                return RedirectToPage("./TraceConfiguration1");
            }
            else 
            {
                ActionConfigurationModel.actionType = t;

                return RedirectToPage("./ActionConfiguration");
            }
        }
    }
}
