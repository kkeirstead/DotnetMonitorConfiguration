// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace DotnetMonitorConfiguration.Pages
{
    public class LoadJsonModel : PageModel
    {
        private readonly ILogger<LoadJsonModel> _logger;

        [BindProperty]
        public string JsonRules { get; set; }

        public static bool failedState = false;

        public LoadJsonModel(ILogger<LoadJsonModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnPostConvertJSON()
        {
            Console.WriteLine("JsonRules: " + JsonRules);

            if (!string.IsNullOrEmpty(JsonRules))
            {
                try
                {
                    List<CollectionRule> newCollectionRules = General.ConvertJsonToCollectionRules(JsonRules);

                    General._collectionRules.AddRange(newCollectionRules);
                }
                catch (Exception ex)
                {
                    failedState = true;

                    Console.WriteLine(ex.Message);

                    return null;
                }

                return RedirectToPage("./CollectionRules/Start");
            }

            return null;
        }
    }
}
