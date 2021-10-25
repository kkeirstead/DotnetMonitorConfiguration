using DotnetMonitorConfiguration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace DotnetMonitorConfiguration.Pages
{
    public class LoadJsonModel : PageModel
    {
        private readonly ILogger<LoadJsonModel> _logger;

        [BindProperty]
        public string JsonRules { get; set; }

        public LoadJsonModel(ILogger<LoadJsonModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public IActionResult OnPostConvertJSON(string data)
        {
            Console.WriteLine("JsonRules: " + JsonRules);

            if (!string.IsNullOrEmpty(JsonRules))
            {
                General.ConvertJsonToCollectionRules(JsonRules);

                return RedirectToPage("./CollectionRules/Start");
            }

            return null;
        }
    }
}
