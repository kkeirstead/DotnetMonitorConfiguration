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
    public class ActionCreationModel : PageModel
    {
        private readonly ILogger<ActionCreationModel> _logger;

        public static List<CRAction> actionList = new List<CRAction>();

        public static int collectionRuleIndex;

        public ActionCreationModel(ILogger<ActionCreationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostNewAction(string data)
        {
            ActionSelectionModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./ActionSelection");
        }

        public IActionResult OnPostDone(string data)
        {
            FilterCreationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./FilterCreation");
        }

        public IActionResult OnPostActionSelect(string data)
        {
            int actionIndex = int.Parse(data);

            CRAction currAction = General._collectionRules[collectionRuleIndex]._actions[actionIndex];
            
            Type actionType = currAction._actionType;

            if (actionType == typeof(CollectTrace))
            {
                if (((CollectTrace)currAction).IsProviders)
                {
                    TraceConfigurationProvidersModel.collectionRuleIndex = collectionRuleIndex;
                    TraceConfigurationProvidersModel.actionIndex = actionIndex;

                    return RedirectToPage("./TraceConfigurationProviders");
                }
                else
                {
                    TraceConfigurationProfileModel.collectionRuleIndex = collectionRuleIndex;
                    TraceConfigurationProfileModel.actionIndex = actionIndex;

                    return RedirectToPage("./TraceConfigurationProfile");
                }
            }
            else
            {
                ActionConfigurationModel.collectionRuleIndex = collectionRuleIndex;
                ActionConfigurationModel.actionIndex = actionIndex;
                ActionConfigurationModel.actionType = actionType;

                return RedirectToPage("./ActionConfiguration");
            }
        }

        public IActionResult OnPostDelete(string data)
        {
            int indexToDelete = int.Parse(data);

            General._collectionRules[collectionRuleIndex]._actions.RemoveAt(indexToDelete);

            return null;
        }

        public IActionResult OnPostDown(string data)
        {
            int indexToDecrease = int.Parse(data);

            if (indexToDecrease == General._collectionRules[collectionRuleIndex]._actions.Count - 1)
            {
                return null;
            }

            CRAction tempAction = General._collectionRules[collectionRuleIndex]._actions[indexToDecrease];

            General._collectionRules[collectionRuleIndex]._actions[indexToDecrease] = General._collectionRules[collectionRuleIndex]._actions[indexToDecrease + 1];

            General._collectionRules[collectionRuleIndex]._actions[indexToDecrease + 1] = tempAction;

            return null;
        }

        public IActionResult OnPostUp(string data)
        {
            int indexToIncrease = int.Parse(data);

            if (indexToIncrease == 0)
            {
                return null;
            }

            CRAction tempAction = General._collectionRules[collectionRuleIndex]._actions[indexToIncrease];

            General._collectionRules[collectionRuleIndex]._actions[indexToIncrease] = General._collectionRules[collectionRuleIndex]._actions[indexToIncrease - 1];

            General._collectionRules[collectionRuleIndex]._actions[indexToIncrease - 1] = tempAction;

            return null;
        }
    }
}
