// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using DotnetMonitorConfiguration.Models;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TriggerSelectionModel : PageModel
    {
        private readonly ILogger<TriggerSelectionModel> _logger;

        [BindProperty]
        public string triggerType { get; set; }

        public static string GetExistingTriggerTypeName()
        {
            CRTrigger trigger = General._collectionRules[CollectionRuleCreationModel.crIndex]._trigger;
            return (null != trigger) ? trigger._triggerType.Name : "";
        }

        public TriggerSelectionModel(ILogger<TriggerSelectionModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPostTriggerType(string data)
        {
            Type t = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types." + data);

            TriggerConfigurationModel.triggerType = t;
            TriggerConfigurationModel.failedState = false;

            return RedirectToPage("./TriggerConfiguration"); 
        }
    }
}
