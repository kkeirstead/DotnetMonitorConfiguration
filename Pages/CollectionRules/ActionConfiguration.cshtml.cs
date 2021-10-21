using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ActionConfigurationModel : PageModel
    {
        private readonly ILogger<ActionConfigurationModel> _logger;

        public static Type actionType;

        public static int collectionRuleIndex;

        public static int actionIndex = -1; // If -1, then creating new one; if has a value, then accessing an existing action

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public ActionConfigurationModel(ILogger<ActionConfigurationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = actionType.GetProperties();

            return props;
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (actionIndex == -1)
            {
                return "";
            }

            CRAction currAction = General._collectionRules[collectionRuleIndex]._actions[actionIndex];

            return General.GetStringRepresentation(currAction, propertyInfo);
        }

        public IActionResult OnPostWay2(string data)
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = actionType.GetConstructors();

                CRAction action = (CRAction)ctors[0].Invoke(constructorArgs);

                action._actionType = actionType;

                if (actionIndex == -1)
                {
                    General._collectionRules[collectionRuleIndex]._actions.Add(action);
                }
                else
                {
                    General._collectionRules[collectionRuleIndex]._actions[actionIndex] = action;
                }

                ActionCreationModel.collectionRuleIndex = collectionRuleIndex;

                return RedirectToPage("./ActionCreation");
            }

            return null;
        }
    }
}
