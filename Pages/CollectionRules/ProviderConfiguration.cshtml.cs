// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ProviderConfigurationModel : PageModel
    {
        private readonly ILogger<ProviderConfigurationModel> _logger;

        public static int collectionRuleIndex;

        public static int actionIndex;

        public static int providerIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public ProviderConfigurationModel(ILogger<ProviderConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static List<(string, string)> GenerateKVPairs()
        {
            Dictionary<string, string> kvPairs = new();
            if (providerIndex != -1)
            {
                kvPairs = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments ?? kvPairs;
            }

            List<(string, string)> keyValuePairs = new();

            foreach (var kvPair in kvPairs)
            {
                keyValuePairs.Add((kvPair.Key, kvPair.Value));
            }

            return keyValuePairs;
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (providerIndex == -1)
            {
                return "";
            }

            List<EventPipeProvider> providers = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers;

            EventPipeProvider currEPP = providers[providerIndex];

            return General.GetStringRepresentation(currEPP, propertyInfo);
        }

        public static Dictionary<string, string> GetKVPairs()
        {
            if (providerIndex != -1)
            {
                return ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments ?? new Dictionary<string, string>();
            }

            return new Dictionary<string, string>();
        }

        public IActionResult OnPostAddKVP()
        {
            KVConfigurationModel._key = "";

            return NavigateToKVConfiguration();
        }

        public IActionResult OnPostAccessKVP(string data)
        {
            KVConfigurationModel._key = GenerateKVPairs()[int.Parse(data)].Item1;

            return NavigateToKVConfiguration();
        }

        private IActionResult NavigateToKVConfiguration()
        {
            SaveCurrProvider();

            KVConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            KVConfigurationModel.actionIndex = actionIndex;

            KVConfigurationModel.providerIndex = providerIndex;

            return RedirectToPage("./KVConfiguration");
        }

        public void SaveCurrProvider()
        {
            EventPipeProvider epp = new EventPipeProvider();

            epp.Name = properties["Name"];
            epp.Keywords = properties["Keywords"];
            epp.EventLevel = (properties["EventLevel"] != null) ? (EventLevel)Enum.Parse(typeof(EventLevel), properties["EventLevel"]) : epp.EventLevel;
            epp.Arguments = GenerateKVPairs().ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            if (null == ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers)
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers = new List<EventPipeProvider>();
            }

            if (providerIndex == -1)
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers.Add(epp);
                providerIndex = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers.Count - 1;
            }
            else
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex] = epp;
            }
        }

        public IActionResult OnPostFinished()
        {
            SaveCurrProvider();

            foreach (var propertyInfo in typeof(EventPipeProvider).GetProperties())
            {
                bool isRequired = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

                if (isRequired)
                {
                    if (string.IsNullOrEmpty(properties[propertyInfo.Name]))
                    {
                        return null;
                    }
                }
            }

            return RedirectToPage("./TraceConfigurationProviders");
        }
    }
}
