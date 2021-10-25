// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace DotnetMonitorConfiguration.Pages
{
    public class CollectionRulesModel : PageModel
    {
        private readonly ILogger<CollectionRulesModel> _logger;

        public CollectionRulesModel(ILogger<CollectionRulesModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostLoadJSON(string data)
        {
            LoadJsonModel.failedState = false;

            return RedirectToPage("./LoadJSON");
        }

        public IActionResult OnPostStart(string data)
        {
            return RedirectToPage("./CollectionRules/Start");
        }
    }
}
