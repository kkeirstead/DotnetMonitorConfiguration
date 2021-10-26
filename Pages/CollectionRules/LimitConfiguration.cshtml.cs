// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Reflection;

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

        public IActionResult OnPostSubmit()
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
