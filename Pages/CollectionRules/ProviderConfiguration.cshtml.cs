using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class ProviderConfigurationModel : PageModel
    {
        private readonly ILogger<ProviderConfigurationModel> _logger;

        public static int collectionRuleIndex;

        public static int actionIndex;

        public static int providerIndex;

        public static List<(string, string)> keyValuePairs = new();

        public bool initialLaunch = true;

        [BindProperty]
        public Dictionary<string, string> properties { get; set; }

        public ProviderConfigurationModel(ILogger<ProviderConfigurationModel> logger)
        {
            Console.WriteLine("Hit constructor");
            _logger = logger;

            GenerateKVPairs();

            initialLaunch = false;
        }

        private static void GenerateKVPairs()
        {
            Console.WriteLine("GenKVP");
            Dictionary<string, string> kvPairs = new();
            if (providerIndex != -1)
            {
                kvPairs = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments ?? kvPairs;
            }

            keyValuePairs = new();

            foreach (var kvPair in kvPairs)
            {
                keyValuePairs.Add((kvPair.Key, kvPair.Value));
            }
        }

        public void OnGet()
        { 
        }

        public static string GetCurrValue(PropertyInfo propertyInfo)
        {
            if (providerIndex == -1)
            {
                return "";
            }

            List<EventPipeProvider> providers = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers;

            EventPipeProvider currEPP = providers[providerIndex];

            return General.GetStringRepresentation(currEPP, propertyInfo);
        }

        public static Dictionary<string, string> GetKVPairs(PropertyInfo propertyInfo)
        {
            Console.WriteLine("GetKVP");

            if (providerIndex != -1)
            {
                return ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments ?? new Dictionary<string, string>();
            }

            return new Dictionary<string, string>();
        }

        public IActionResult OnPostAddKVP(string data)
        {
            SaveCurrProvider();

            KVConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            KVConfigurationModel.actionIndex = actionIndex;

            KVConfigurationModel.providerIndex = providerIndex;

            KVConfigurationModel._key = "";

            return RedirectToPage("./KVConfiguration");
        }

        public IActionResult OnPostAccessKVP(string data)
        {
            SaveCurrProvider();

            KVConfigurationModel.collectionRuleIndex = collectionRuleIndex;

            KVConfigurationModel.actionIndex = actionIndex;

            KVConfigurationModel.providerIndex = providerIndex;

            KVConfigurationModel._key = keyValuePairs[int.Parse(data)].Item1;

            return RedirectToPage("./KVConfiguration");
        }

        public void SaveCurrProvider()
        {
            EventPipeProvider epp = new EventPipeProvider();

            epp.Name = properties["Name"];
            epp.Keywords = properties["Keywords"];
            epp.EventLevel = (properties["EventLevel"] != null) ? (EventLevel)Enum.Parse(typeof(EventLevel), properties["EventLevel"]) : epp.EventLevel;
            epp.Arguments = keyValuePairs.ToDictionary(tuple => tuple.Item1, tuple => tuple.Item2);

            if (null == ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers)
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers = new List<EventPipeProvider>();
            }

            if (providerIndex == -1)
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers.Add(epp);
                providerIndex = ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers.Count - 1;
            }
            else
            {
                ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex] = epp;
            }
        }

        public IActionResult OnPostFinished(string data)
        {
            SaveCurrProvider();

            // Let's make this dynamic (should add generic check that makes sure all Required properties are filled)
            if (string.IsNullOrEmpty(properties["Name"]))
            {
                return null;
            }

            return RedirectToPage("./TraceConfigurationProviders");
        }
    }
}
