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

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TraceConfigurationProfileModel : PageModel
    {
        private readonly ILogger<TraceConfigurationProfileModel> _logger;

        public static Type actionType;

        public static int collectionRuleIndex;

        public static int actionIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public TraceConfigurationProfileModel(ILogger<TraceConfigurationProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
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

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = typeof(CollectTrace).GetProperties();

            List<PropertyInfo> profileProps = new List<PropertyInfo> { };

            foreach (var prop in props)
            {
                bool isProfile = !Attribute.IsDefined(prop, typeof(ProvidersAttribute));

                if (isProfile)
                {
                    profileProps.Add(prop);
                }
            }

            return profileProps.ToArray();
        }

        public IActionResult OnPostSubmit(string data)
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CollectTrace).GetConstructors().ToList();

                ctors.RemoveAll(ctor => ctor.GetParameters().Length != constructorArgs.Length);

                CollectTrace action = (CollectTrace)ctors[0].Invoke(constructorArgs);
                action.IsProviders = false; // Only used internally to simplify checks

                General._collectionRules[collectionRuleIndex]._actions[actionIndex] = action;
                General._collectionRules[collectionRuleIndex]._actions[actionIndex]._actionType = typeof(CollectTrace);
            }

            return RedirectToPage("./ActionCreation");
        }
    }
}
