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

        [BindProperty]
        public string Dropdown { get; set; }


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

        public IActionResult OnPostWay2(string data)
        {
            var props = GetConfigurationSettings();

            object[] constructorArgs = new object[props.Length];

            // This code is being reused in multiple places -> consider a shared function that knows how to parse everything
            foreach (var key in properties.Keys)
            {
                int index = int.Parse(key);
                Console.WriteLine(props[index].Name + " | " + properties[key]);

                Type propsType = General.GetType(props[index].PropertyType);

                if (null == properties[key])
                {
                    constructorArgs[index] = null;
                }
                else
                {
                    if (propsType == typeof(Int32))
                    {
                        constructorArgs[index] = int.Parse(properties[key]);
                    }
                    else if (propsType == typeof(string))
                    {
                        constructorArgs[index] = properties[key];
                    }
                    else if (propsType == typeof(TimeSpan))
                    {
                        constructorArgs[index] = TimeSpan.Parse(properties[key]);
                    }
                    else if (propsType == typeof(string[]))
                    {
                        constructorArgs[index] = properties[key].Split(',');
                    }
                    else if (propsType.IsEnum)
                    {
                        constructorArgs[index] = Enum.Parse(propsType, properties[key]);
                    }
                }
            }
             
            var ctors = actionType.GetConstructors();

            CRAction action = (CRAction)ctors[0].Invoke(constructorArgs);

            action._actionType = actionType;

            General._collectionRules[collectionRuleIndex]._actions.Add(action);

            ActionCreationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./ActionCreation");
        }
    }
}
