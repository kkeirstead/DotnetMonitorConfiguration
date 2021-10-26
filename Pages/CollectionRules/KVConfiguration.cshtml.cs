// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

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

        public IActionResult OnPostSubmit(string data)
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
