﻿using DotnetMonitorConfiguration.Models;
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

            object propertyValue = propertyInfo.GetValue(currLimit);

            Type t = propertyInfo.PropertyType;

            return (propertyValue != null) ? General.GetStringRepresentation(propertyValue, t) : "";
        }

        public IActionResult OnPostWay2(string data)
        {
            var props = GetConfigurationSettings();

            object[] constructorArgs = new object[props.Length];

            // This code is being reused in multiple places -> consider a shared function that knows how to parse everything
            foreach (var key in properties.Keys)
            {
                int index = int.Parse(key);

                constructorArgs[index] = General.GetConstructorArgs(props[index], properties[key]);
            }

            var ctors = typeof(CRLimit).GetConstructors();

            CRLimit limit = (CRLimit)ctors[0].Invoke(constructorArgs);

            General._collectionRules[collectionRuleIndex]._limit = limit;

            return RedirectToPage("./Start");
        }
    }
}
