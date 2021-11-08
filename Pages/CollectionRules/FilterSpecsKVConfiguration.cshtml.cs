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
    public class FilterSpecsKVConfigurationModel : PageModel
    {
        private readonly ILogger<FilterSpecsKVConfigurationModel> _logger;

        public static string _key;

        [BindProperty]
        public string Key { get; set; }

        [BindProperty]
        public LogLevel? Value { get; set; }

        public FilterSpecsKVConfigurationModel(ILogger<FilterSpecsKVConfigurationModel> logger)
        {
            _logger = logger;
        }

        public static string GetValue()
        {
            if (string.IsNullOrEmpty(_key))
            {
                return "";
            }

            return ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs[_key]!.ToString();
        }

        public IActionResult OnPostSubmit()
        {
            if (!(string.IsNullOrEmpty(Key) && Value.HasValue))
            {
                if (null != ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs)
                {
                    ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs[Key] = Value;
                }
                else
                {
                    ((CollectLogs)General._collectionRules[CollectionRuleCreationModel.crIndex]._actions[ActionCreationModel.actionIndex]).FilterSpecs = new Dictionary<string, LogLevel?>() { { Key, Value } };
                }
            }

            return RedirectToPage("./ActionConfiguration");
        }
    }
}
