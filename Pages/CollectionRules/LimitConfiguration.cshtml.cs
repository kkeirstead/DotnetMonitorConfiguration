using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
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
    public class LimitConfigurationModel : PageModel
    {
        private readonly ILogger<LimitConfigurationModel> _logger;

        public static int collectionRuleIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public LimitConfigurationModel(ILogger<LimitConfigurationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = typeof(CRLimit).GetProperties();

            return props;
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            CRLimit currLimit = General._collectionRules[collectionRuleIndex]._limit;

            if (currLimit == null)
            {
                return "";
            }

            return General.GetStringRepresentation(currLimit, propertyInfo);
        }

        public IActionResult OnPostWay2(string data)
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CRLimit).GetConstructors();

                CRLimit limit = (CRLimit)ctors[0].Invoke(constructorArgs);

                General._collectionRules[collectionRuleIndex]._limit = limit;

                return RedirectToPage("./Start");
            }

            return null;
        }
    }
}
