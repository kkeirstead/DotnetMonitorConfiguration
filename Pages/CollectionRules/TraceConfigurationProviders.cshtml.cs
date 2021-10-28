// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TraceConfigurationProvidersModel : PageModel
    {
        private readonly ILogger<TraceConfigurationProvidersModel> _logger;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public TraceConfigurationProvidersModel(ILogger<TraceConfigurationProvidersModel> logger)
        {
            _logger = logger;
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            return General.GetCurrValueAction(propertyInfo, ActionCreationModel.actionIndex, CollectionRuleCreationModel.crIndex);
        }

        public static PropertyInfo[] GetConfigurationSettings(bool includeEPP)
        {
            var props = typeof(CollectTrace).GetProperties();

            List<PropertyInfo> providersProps = new List<PropertyInfo> { };

            foreach (var prop in props)
            {
                bool isProviders = !Attribute.IsDefined(prop, typeof(ProfileAttribute));

                // Special case since we handle EventPipeProvider on a separate screen
                if (isProviders && (includeEPP || prop.PropertyType != typeof(List<EventPipeProvider>)))
                {
                    providersProps.Add(prop);
                }
            }

            return providersProps.ToArray();
        }

        public static List<EventPipeProvider> GetEventPipeProviders(PropertyInfo propertyInfo)
        {
            List<EventPipeProvider> epProviders = new();

            if (ActionCreationModel.actionIndex != General._collectionRules[CollectionRuleCreationModel.crIndex]._actions.Count)
            {
                CRAction action = General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex];

                epProviders = (null != propertyInfo.GetValue(action)) ? (List<EventPipeProvider>)propertyInfo.GetValue(action) : epProviders;
            }

            return epProviders;
        }

        public IActionResult OnPostAddEPP()
        {
            SaveCurrAction();

            ProviderConfigurationModel.providerIndex = -1;

            return RedirectToPage("./ProviderConfiguration");
        }

        public IActionResult OnPostAccessEPP(string data)
        {
            SaveCurrAction();

            ProviderConfigurationModel.providerIndex = int.Parse(data);

            return RedirectToPage("./ProviderConfiguration");
        }

        public IActionResult OnPostDeleteEPP(string data)
        {
            ((CollectTrace)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).Providers.RemoveAt(int.Parse(data));

            return null;
        }

        private bool SaveCurrAction()
        {
            var typeProperties = GetConfigurationSettings(includeEPP: false);

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CollectTrace).GetConstructors().ToList();

                ctors.RemoveAll(ctor => ctor.GetParameters().Length != constructorArgs.Length);

                CollectTrace action = (CollectTrace)ctors[0].Invoke(constructorArgs);
                action.IsProviders = true; // Only used internally to simplify checks

                action.Providers = ((CollectTrace)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).Providers;

                General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex] = action;
                General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]._actionType = typeof(CollectTrace);

                return true;
            }

            return false;
        }

        public IActionResult OnPostSubmit()
        {
            if (!SaveCurrAction())
            {
                return null;
            }

            return RedirectToPage("./ActionCreation");
        }
    }
}
