using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using DotnetMonitorConfiguration.Models;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TriggerSelectionModel : PageModel
    {
        private readonly ILogger<TriggerSelectionModel> _logger;

        [BindProperty]
        public string triggerType { get; set; }

        public static int collectionRuleIndex;

        public static string GetExistingTriggerTypeName()
        {
            CRTrigger trigger = General._collectionRules[collectionRuleIndex]._trigger;
            return (null != trigger) ? trigger._triggerType.Name : "";
        }

        public TriggerSelectionModel(ILogger<TriggerSelectionModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostTriggerType(string data)
        {
            Type t = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types." + data);

            TriggerConfigurationModel.triggerType = t;

            TriggerConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./TriggerConfiguration"); 
        }
    }
}
