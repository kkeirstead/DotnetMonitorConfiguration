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
    public class TraceConfigurationProfileModel : PageModel
    {
        private readonly ILogger<TraceConfigurationProfileModel> _logger;

        public static Type actionType;

        public static int collectionRuleIndex;

        public static int actionIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public TraceConfigurationProfileModel(ILogger<TraceConfigurationProfileModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        // Moving this to be shared for ActionConfig and the TraceConfigs would be useful (would need to parameterize the actionIndex).
        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (actionIndex == -1)
            {
                return "";
            }

            CRAction currAction = General._collectionRules[collectionRuleIndex]._actions[actionIndex];

            return General.GetStringRepresentation(currAction, propertyInfo);
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = typeof(CollectTrace).GetProperties();

            List<PropertyInfo> profileProps = new List<PropertyInfo> { };

            foreach (var prop in props)
            {
                bool isProfile = !Attribute.IsDefined(prop, typeof(ProvidersAttribute));

                if (isProfile)
                {
                    profileProps.Add(prop);
                }
            }

            return profileProps.ToArray();
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
