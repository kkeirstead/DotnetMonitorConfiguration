using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ActionSelectionModel : PageModel
    {
        private readonly ILogger<ActionSelectionModel> _logger;

        [BindProperty]
        public string actionType { get; set; }

        public static int collectionRuleIndex;

        public ActionSelectionModel(ILogger<ActionSelectionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostWay2(string data)
        {
            Type t = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types." + data);

            if (typeof(CollectTrace) == t)
            {
                TraceConfiguration1Model.actionType = t;

                TraceConfiguration1Model.actionIndex = -1;

                TraceConfiguration1Model.collectionRuleIndex = collectionRuleIndex;

                return RedirectToPage("./TraceConfiguration1");
            }
            else 
            {
                ActionConfigurationModel.actionType = t;

                ActionConfigurationModel.collectionRuleIndex = collectionRuleIndex;

                return RedirectToPage("./ActionConfiguration");
            }
        }
    }
}
