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
    public class FilterConfigurationModel : PageModel
    {
        private readonly ILogger<FilterConfigurationModel> _logger;

        public static int collectionRuleIndex;

        public static int filterIndex = -1; // If -1, then creating new one; if has a value, then accessing an existing filter

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public FilterConfigurationModel(ILogger<FilterConfigurationModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
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

            CRFilter currFilter = General._collectionRules[collectionRuleIndex]._filters[filterIndex];

            object propertyValue = propertyInfo.GetValue(currFilter);

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
                }
            }

            var ctors = typeof(CRFilter).GetConstructors();

            CRFilter filter = (CRFilter)ctors[0].Invoke(constructorArgs);

            General._collectionRules[collectionRuleIndex]._filters.Add(filter);

            FilterCreationModel.collectionRuleIndex = collectionRuleIndex;

            return RedirectToPage("./FilterCreation");
        }
    }
}
