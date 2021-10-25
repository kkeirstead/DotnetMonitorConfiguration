using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages
{
    public class CollectionRulesModel : PageModel
    {
        private readonly ILogger<CollectionRulesModel> _logger;

        public CollectionRulesModel(ILogger<CollectionRulesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLoadJSON(string data)
        {
            return RedirectToPage("./LoadJSON");
        }

        public IActionResult OnPostStart(string data)
        {
            return RedirectToPage("./CollectionRules/Start");
        }
    }
}
