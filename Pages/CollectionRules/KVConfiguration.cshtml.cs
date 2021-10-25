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
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages.CollectionRules
{
    public class KVConfigurationModel : PageModel
    {
        private readonly ILogger<KVConfigurationModel> _logger;

        public static int collectionRuleIndex;

        public static int actionIndex;

        public static int providerIndex;

        public static string _key;

        [BindProperty]
        public string Key { get; set; }

        [BindProperty]
        public string Value { get; set; }

        public KVConfigurationModel(ILogger<KVConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static string GetValue()
        {
            if (string.IsNullOrEmpty(_key))
            {
                return "";
            }

            return ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments[_key];
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostWay2(string data)
        {
            if (!(string.IsNullOrEmpty(Key) || string.IsNullOrEmpty(Value)))
            {
                if (null != ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments)
                {
                    ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments[Key] = Value;
                }
                else
                {
                    ((CollectTrace)General._collectionRules[collectionRuleIndex]._actions[actionIndex]).Providers[providerIndex].Arguments = new Dictionary<string, string>() { { Key, Value } };
                }
            }

            return RedirectToPage("./ProviderConfiguration");
        }

    }
}
