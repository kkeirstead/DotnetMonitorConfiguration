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
    public class FilterConfigurationModel : PageModel
    {
        private readonly ILogger<FilterConfigurationModel> _logger;

        public static int filterIndex;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public FilterConfigurationModel(ILogger<FilterConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static PropertyInfo[] GetConfigurationSettings()
        {
            var props = typeof(CRFilter).GetProperties();

            return props;
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (filterIndex == -1)
            {
                return "";
            }

            CRFilter currFilter = General._collectionRules[CollectionRuleCreationModel.crIndex]._filters[filterIndex];

            return General.GetStringRepresentation(currFilter, propertyInfo);
        }

        public IActionResult OnPostSubmit()
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CRFilter).GetConstructors();

                CRFilter filter = (CRFilter)ctors[0].Invoke(constructorArgs);

                General._collectionRules[CollectionRuleCreationModel.crIndex]._filters.Add(filter);

                return RedirectToPage("./FilterCreation");
            }

            return null;
        }
    }
}
