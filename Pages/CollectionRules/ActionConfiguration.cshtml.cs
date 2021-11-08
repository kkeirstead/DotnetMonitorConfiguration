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
using System.Linq;
using System.Reflection;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ActionConfigurationModel : PageModel
    {
        private readonly ILogger<ActionConfigurationModel> _logger;

        public static Type actionType;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public static bool failedState = false;

        public ActionConfigurationModel(ILogger<ActionConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static PropertyInfo[] GetConfigurationSettings(bool includeDictionary = true)
        {
            List<PropertyInfo> propertyInfo = actionType.GetProperties().ToList<PropertyInfo>();

            if (includeDictionary)
            {
                return propertyInfo.ToArray();
            }

            var toRemove = propertyInfo.Single(prop => prop.PropertyType == typeof(Dictionary<string, LogLevel?>));
            propertyInfo.Remove(toRemove);

            return propertyInfo.ToArray();
        }

        public static List<(string, LogLevel?)> GenerateKVPairs()
        {
            Dictionary<string, LogLevel?> kvPairs = new();
            if (ActionCreationModel.actionIndex != General._collectionRules[CollectionRuleCreationModel.crIndex]._actions.Count)
            {
                kvPairs = ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs ?? new();
            }

            List<(string, LogLevel?)> keyValuePairs = new();

            foreach (var kvPair in kvPairs)
            {
                keyValuePairs.Add((kvPair.Key, kvPair.Value));
            }

            return keyValuePairs;
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
            if (SaveCurrProvider())
            {
                failedState = false;
                return RedirectToPage("./ActionCreation");
            }
            else
            {
                failedState = true;
                return null;
            }
        }

        public bool SaveCurrProvider()
        {
            var typeProperties = GetConfigurationSettings(includeDictionary: false);

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
                    if (actionType == typeof(CollectLogs))
                    {
                        Dictionary<string, LogLevel?> filterSpecs = ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs;
                        General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex] = action;
                        ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs = filterSpecs;
                    }
                    else
                    {
                        General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex] = action;
                    }
                }

                return true;
            }

            return false;
        }

        public IActionResult OnPostAddKVP()
        {
            FilterSpecsKVConfigurationModel._key = "";

            return NavigateToFilterSpecsKVConfiguration();
        }

        public IActionResult OnPostAccessKVP(string data)
        {
            FilterSpecsKVConfigurationModel._key = GenerateKVPairs()[int.Parse(data)].Item1;

            return NavigateToFilterSpecsKVConfiguration();
        }

        private IActionResult NavigateToFilterSpecsKVConfiguration()
        {
            if (SaveCurrProvider())
            {
                failedState = false;
                return RedirectToPage("./FilterSpecsKVConfiguration");
            }
            else
            {
                failedState = true;
                return null;
            }
        }
    }
}
