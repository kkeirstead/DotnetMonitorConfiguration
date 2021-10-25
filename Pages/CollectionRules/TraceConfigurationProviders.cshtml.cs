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

        public static int collectionRuleIndex;

        public static int actionIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public TraceConfigurationProvidersModel(ILogger<TraceConfigurationProvidersModel> logger)
        {
            _logger = logger;
        }

        // Moving this to be shared for ActionConfig and the TraceConfigs would be useful (would need to parameterize the actionIndex).
        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (actionIndex == -1)
            {
                return "";
            }

            CRAction currAction = General._collectionRules[collectionRuleIndex]._actions[actionIndex];

            return General.GetStringRepresentation(currAction, propertyInfo);
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
            List<EventPipeProvider> epProviders = new List<EventPipeProvider>();

            if (actionIndex != -1)
            {
                CRAction action = General._collectionRules[collectionRuleIndex]._actions[actionIndex];

                epProviders = (null != propertyInfo.GetValue(action)) ? (List<EventPipeProvider>)propertyInfo.GetValue(action) : epProviders;
            }

            return epProviders;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAddEPP(string data)
        {
            SaveCurrAction();

            ProviderConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            ProviderConfigurationModel.actionIndex = actionIndex;

            ProviderConfigurationModel.providerIndex = -1;

            return RedirectToPage("./ProviderConfiguration");
        }

        public IActionResult OnPostAccessEPP(string data)
        {
            SaveCurrAction();

            ProviderConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            ProviderConfigurationModel.actionIndex = actionIndex;

            ProviderConfigurationModel.providerIndex = int.Parse(data);

            return RedirectToPage("./ProviderConfiguration");
        }

        public IActionResult OnPostDeleteEPP(string data)
        {
            ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers.RemoveAt(int.Parse(data));

            return null;
        }

        private void SaveCurrAction()
        {
            var typeProperties = GetConfigurationSettings(includeEPP: false);

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CollectTrace).GetConstructors().ToList();

                ctors.RemoveAll(ctor => ctor.GetParameters().Length != constructorArgs.Length);

                CollectTrace action = (CollectTrace)ctors[0].Invoke(constructorArgs);
                action.IsProviders = true; // Only used internally to simplify checks

                action.Providers = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers; // Hacky, but should work

                General._collectionRules[collectionRuleIndex]._actions[actionIndex] = action;
                General._collectionRules[collectionRuleIndex]._actions[actionIndex]._actionType = typeof(CollectTrace);
            }
        }

        public IActionResult OnPostWay2(string data)
        {
            SaveCurrAction();

            return RedirectToPage("./ActionCreation");
        }
    }
}
