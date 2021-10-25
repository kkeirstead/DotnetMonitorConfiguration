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

        public static int collectionRuleIndex;

        public static int filterIndex;

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

            return General.GetStringRepresentation(currFilter, propertyInfo);
        }
        public IActionResult OnPostSubmit(string data)
        {
            var typeProperties = GetConfigurationSettings();

            object[] constructorArgs = General.GetConstructorArgs(typeProperties, properties);

            if (null != constructorArgs)
            {
                var ctors = typeof(CRFilter).GetConstructors();

                CRFilter filter = (CRFilter)ctors[0].Invoke(constructorArgs);

                General._collectionRules[collectionRuleIndex]._filters.Add(filter);

                FilterCreationModel.collectionRuleIndex = collectionRuleIndex;

                return RedirectToPage("./FilterCreation");
            }

            return null;
        }
    }
}
