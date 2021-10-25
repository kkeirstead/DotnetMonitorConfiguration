using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
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

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            CRTrigger currTrigger = General._collectionRules[collectionRuleIndex]._trigger;

            if (currTrigger == null)
            {
                return "";
            }

            return General.GetStringRepresentation(currTrigger, propertyInfo);
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = triggerType.GetProperties();

            return props;
        }

        public IActionResult OnPostSubmit(string data)
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = triggerType.GetConstructors();

                CRTrigger trigger = (CRTrigger)ctors[0].Invoke(constructorArgs);
                trigger._triggerType = triggerType;

                General._collectionRules[collectionRuleIndex]._trigger = trigger;

                ActionCreationModel.collectionRuleIndex = collectionRuleIndex;

                return RedirectToPage("./ActionCreation");
            }

            return null;
        }
    }
}
