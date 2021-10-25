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
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TraceConfiguration1Model : PageModel
    {
        private readonly ILogger<TraceConfiguration1Model> _logger;

        public static Type actionType;

        public static int collectionRuleIndex;

        public static int actionIndex;

        public TraceConfiguration1Model(ILogger<TraceConfiguration1Model> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostProfile(string data)
        {
            // This may need the same treatment as the method below

            TraceConfigurationProfileModel.collectionRuleIndex = collectionRuleIndex;

            TraceConfigurationProfileModel.actionIndex = actionIndex;

            return RedirectToPage("./TraceConfigurationProfile");
        }

        public IActionResult OnPostProviders(string data)
        {
            var typeProperties = typeof(CollectTrace).GetProperties();

            object[] constructorArgs = new object[typeProperties.Length]; // Just do all null args

            var ctors = actionType.GetConstructors();

            CRAction action = (CRAction)ctors[0].Invoke(constructorArgs);

            action._actionType = actionType;

            // Need to create an action before allowing trace configuration...a bit hacky.
            if (actionIndex == -1)
            {
                General._collectionRules[collectionRuleIndex]._actions.Add(action);
                actionIndex = General._collectionRules[collectionRuleIndex]._actions.Count - 1;
            }

            TraceConfigurationProvidersModel.collectionRuleIndex = collectionRuleIndex;

            TraceConfigurationProvidersModel.actionIndex = actionIndex;

            return RedirectToPage("./TraceConfigurationProviders");
        }
    }
}
