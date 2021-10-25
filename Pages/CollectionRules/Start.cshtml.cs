using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.Json;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class StartModel : PageModel
    {
        private readonly ILogger<StartModel> _logger;

        public StartModel(ILogger<StartModel> logger)
        {
            _logger = logger;
        }

        public static string GenerateJSON()
        {
            RemoveBadCollectionRules();

            List<CollectionRuleOptions> collectionRuleOptions = new List<CollectionRuleOptions>();
            Dictionary<string, CollectionRuleOptions> collectionRuleOptionsDict = new();

            foreach (var rule in General._collectionRules)
            {
                collectionRuleOptions.Add(General.SerializeCollectionRule(rule));
                collectionRuleOptionsDict[rule.Name] = General.SerializeCollectionRule(rule);
            }

            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.NullValueHandling = NullValueHandling.Ignore;
            settings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            return JsonConvert.SerializeObject(collectionRuleOptionsDict, Formatting.Indented, settings);
        }

        private static void RemoveBadCollectionRules()
        {
            for (int index = General._collectionRules.Count - 1; index >= 0; --index)
            {
                if (null == General._collectionRules[index]._trigger || null == General._collectionRules[index]._limit)
                {
                    General._collectionRules.RemoveAt(index);
                }
            }
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostNewCollectionRule(string data)
        {
            CollectionRuleCreationModel.collectionRuleIndex = -1;

            return RedirectToPage("./CollectionRuleCreation");
        }

        public IActionResult OnPostSelectCollectionRule(string data)
        {
            CollectionRuleCreationModel.collectionRuleIndex = int.Parse(data);

            return RedirectToPage("./CollectionRuleCreation");
        }

        public IActionResult OnPostDelete(string data)
        {
            General._collectionRules.RemoveAt(int.Parse(data));

            return null;
        }
    }
}
