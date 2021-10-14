﻿using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public IActionResult OnPostWay2(string data)
        {
            TriggerConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./FilterConfiguration");
        }

        public IActionResult OnPostWay3(string data)
        {
            FilterConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./LimitConfiguration");
        }
    }
}