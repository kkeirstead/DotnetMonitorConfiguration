// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class FilterCreationModel : PageModel
    {
        private readonly ILogger<FilterCreationModel> _logger;

        public static List<CRAction> actionList = new List<CRAction>();

        public static int collectionRuleIndex;

        public FilterCreationModel(ILogger<FilterCreationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostNewFilter()
        {
            FilterConfigurationModel.collectionRuleIndex = collectionRuleIndex;
            FilterConfigurationModel.filterIndex = -1;

            return RedirectToPage("./FilterConfiguration");
        }

        public IActionResult OnPostDone()
        {
            LimitConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./LimitConfiguration");
        }

        public IActionResult OnPostFilterSelect(string data)
        {
            FilterConfigurationModel.collectionRuleIndex = collectionRuleIndex;
            FilterConfigurationModel.filterIndex = int.Parse(data);

            return RedirectToPage("./FilterConfiguration");
        }

        public IActionResult OnPostDelete(string data)
        {
            int indexToDelete = int.Parse(data);

            General._collectionRules[collectionRuleIndex]._filters.RemoveAt(indexToDelete);

            return null;
        }
    }
}
