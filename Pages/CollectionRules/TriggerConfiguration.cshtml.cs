using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class TriggerConfigurationModel : PageModel
    {
        private readonly ILogger<TriggerConfigurationModel> _logger;

        public static Type triggerType;

        public static int collectionRuleIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public TriggerConfigurationModel(ILogger<TriggerConfigurationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = triggerType.GetProperties();

            return props;
        }

        public IActionResult OnPostWay2(string data)
        {
            var props = GetConfigurationSettings();

            object[] constructorArgs = new object[props.Length];

            foreach (var key in properties.Keys)
            {
                int index = int.Parse(key);
                Console.WriteLine(props[index].Name + " | " + properties[key]);

                if (null == properties[key])
                {
                    constructorArgs[index] = null;
                }
                else
                {
                    if (props[index].PropertyType == typeof(Int32))
                    {
                        constructorArgs[index] = int.Parse(properties[key]);
                    }
                    else if (props[index].PropertyType == typeof(string))
                    {
                        constructorArgs[index] = properties[key];
                    }
                    else if (props[index].PropertyType == typeof(TimeSpan?))
                    {
                        constructorArgs[index] = TimeSpan.Parse(properties[key]);
                    }
                    else if (props[index].PropertyType == typeof(string[]))
                    {
                        constructorArgs[index] = properties[key].Split(',');
                    }
                }
            }

            var ctors = triggerType.GetConstructors();

            CRTrigger trigger = (CRTrigger)ctors[0].Invoke(constructorArgs);
            trigger._triggerType = triggerType; 

            General._collectionRules[collectionRuleIndex]._trigger = trigger;

            ActionCreationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./ActionCreation");
        }
    }
}
