// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class StartModel : PageModel
    {
        private readonly ILogger<StartModel> _logger;

        public StartModel(ILogger<StartModel> logger)
        {
            _logger = logger;
        }

        public static string GenerateJSON()
        {
            Dictionary<string, CollectionRuleOptions> collectionRuleOptionsDict = new();

            foreach (var rule in General._collectionRules)
            {
                collectionRuleOptionsDict[rule.Name] = General.SerializeCollectionRule(rule);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return "CollectionRules: " + JsonConvert.SerializeObject(collectionRuleOptionsDict, Formatting.Indented, settings);
        }

        public static void FilterBadCRs()
        {
            for (int index = General._collectionRules.Count - 1; index >= 0; --index)
            {
                if (null == General._collectionRules[index]._trigger)
                {
                    General._collectionRules.RemoveAt(index);
                }

                if (null == General._collectionRules[index]._limit)
                {
                    General._collectionRules[index]._limit = new Models.Collection_Rules.CRLimit(null, null, null);
                }
            }
        }

        public IActionResult OnPostNewCollectionRule()
        {
            CollectionRuleCreationModel.collectionRuleIndex = -1;

            return RedirectToPage("./CollectionRuleCreation");
        }

        public IActionResult OnPostSelectCollectionRule(string data)
        {
            CollectionRuleCreationModel.collectionRuleIndex = int.Parse(data);

            return RedirectToPage("./CollectionRuleCreation");
        }

        public IActionResult OnPostDelete(string data)
        {
            General._collectionRules.RemoveAt(int.Parse(data));

            return null;
        }
    }
}
