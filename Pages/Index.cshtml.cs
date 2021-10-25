using DotnetMonitorConfiguration.Models;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostCollectionRules(string data)
        {
            return RedirectToPage("./CollectionRules");
        }
    }
}
