// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ActionConfigurationModel : PageModel
    {
        private readonly ILogger<ActionConfigurationModel> _logger;

        public static Type actionType;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public ActionConfigurationModel(ILogger<ActionConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            return actionType.GetProperties();
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            return General.GetCurrValueAction(propertyInfo, ActionCreationModel.actionIndex, CollectionRuleCreationModel.crIndex);
        }

        public static string GetDocumentationLink()
        {
            return actionType.GetField("_documentationLink")?.GetValue(null).ToString() ?? "";
        }

        public IActionResult OnPostSubmit()
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = actionType.GetConstructors();

                CRAction action = (CRAction)ctors[0].Invoke(constructorArgs);

                action._actionType = actionType;

                if (ActionCreationModel.actionIndex == General._collectionRules[CollectionRuleCreationModel.crIndex]._actions.Count)
                {
                    General._collectionRules[CollectionRuleCreationModel.crIndex]._actions.Add(action);
                }
                else
                {
                    General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex] = action;
                }

                return RedirectToPage("./ActionCreation");
            }

            return null;
        }
    }
}
