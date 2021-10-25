// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TraceConfiguration1Model : PageModel
    {
        private readonly ILogger<TraceConfiguration1Model> _logger;

        public static Type actionType;

        public static int collectionRuleIndex;

        public static int actionIndex;

        public TraceConfiguration1Model(ILogger<TraceConfiguration1Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostProfile(string data)
        {
            CreateDummyAction();

            TraceConfigurationProfileModel.collectionRuleIndex = collectionRuleIndex;

            TraceConfigurationProfileModel.actionIndex = actionIndex;

            return RedirectToPage("./TraceConfigurationProfile");
        }

        public IActionResult OnPostProviders(string data)
        {
            CreateDummyAction();

            TraceConfigurationProvidersModel.collectionRuleIndex = collectionRuleIndex;

            TraceConfigurationProvidersModel.actionIndex = actionIndex;

            return RedirectToPage("./TraceConfigurationProviders");
        }

        private void CreateDummyAction()
        {
            var typeProperties = typeof(CollectTrace).GetProperties();

            object[] constructorArgs = new object[typeProperties.Length]; // Just do all null args

            var ctors = actionType.GetConstructors();

            CRAction action = (CRAction)ctors[0].Invoke(constructorArgs);

            action._actionType = actionType;

            // Need to create an action before allowing trace configuration...a bit hacky.
            if (actionIndex == -1)
            {
                General._collectionRules[collectionRuleIndex]._actions.Add(action);
                actionIndex = General._collectionRules[collectionRuleIndex]._actions.Count - 1;
            }
        }
    }
}
