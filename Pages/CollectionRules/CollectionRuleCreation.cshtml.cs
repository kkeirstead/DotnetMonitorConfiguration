using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
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
    public class CollectionRuleCreationModel : PageModel
    {
        private readonly ILogger<CollectionRuleCreationModel> _logger;

        [BindProperty]
        public string Name { get; set; }

        public CollectionRuleCreationModel(ILogger<CollectionRuleCreationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostWay2(string data)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                CollectionRule collectionRule = new CollectionRule(Name);

                General._collectionRules.Add(collectionRule);

                TriggerSelectionModel.collectionRuleIndex = General._collectionRules.Count() - 1;

                return RedirectToPage("./TriggerSelection");
            }
            return null;
        }
    }
}
