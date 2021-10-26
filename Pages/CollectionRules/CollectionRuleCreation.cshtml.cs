// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class CollectionRuleCreationModel : PageModel
    {
        private readonly ILogger<CollectionRuleCreationModel> _logger;

        [BindProperty]
        public string Name { get; set; }

        public static int collectionRuleIndex;

        public CollectionRuleCreationModel(ILogger<CollectionRuleCreationModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPostSubmit()
        {
            if (!string.IsNullOrEmpty(Name))
            {
                if (collectionRuleIndex != -1)
                {
                    General._collectionRules[collectionRuleIndex].Name = Name;

                    TriggerSelectionModel.collectionRuleIndex = collectionRuleIndex;
                }
                else
                {
                    CollectionRule collectionRule = new CollectionRule(Name);

                    General._collectionRules.Add(collectionRule);

                    TriggerSelectionModel.collectionRuleIndex = General._collectionRules.Count() - 1;
                }

                return RedirectToPage("./TriggerSelection");
            }

            return null;
        }
    }
}
