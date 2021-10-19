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
            Console.WriteLine("Top of Generate.");
            // Fair warning, this is currently a mess. Will be straightened up later.

            string jsonString = "{\"CollectionRules\": {";

            int collectionRuleIndex = 0;
            foreach (var collectionRule in General._collectionRules)
            {
                string currCollectionRule = FormatKVPairNonTextValue(collectionRule.Name, "{");

                currCollectionRule += FormatKVPairNonTextValue("Filters", "[");

                int filterIndex = 0;
                foreach (var filter in collectionRule._filters)
                {
                    currCollectionRule += "{";

                    int filterSettingIndex = 0;
                    foreach (var setting in typeof(CRFilter).GetProperties())
                    {
                        currCollectionRule += FormatKVPair(setting, setting.GetValue(filter));

                        if (filterSettingIndex != typeof(CRFilter).GetProperties().Length - 1)
                        {
                            currCollectionRule += ",";
                        }

                        ++filterSettingIndex;
                    }

                    currCollectionRule += "}";

                    if (filterIndex != collectionRule._filters.Count - 1)
                    {
                        currCollectionRule += ",";
                    }

                    ++filterIndex;
                }

                currCollectionRule += "],";

                currCollectionRule += FormatKVPairNonTextValue("Trigger", "{");

                currCollectionRule += FormatKVPair("Type", collectionRule._trigger._triggerType.Name);

                currCollectionRule += ",";

                currCollectionRule += FormatKVPairNonTextValue("Settings", "{");

                int triggerSettingIndex = 0;
                foreach (var setting in collectionRule._trigger._triggerType.GetProperties())
                {
                    if (null != setting.GetValue(collectionRule._trigger))
                    {
                        if (triggerSettingIndex != 0)
                        {
                            currCollectionRule += ",";
                        }
                        currCollectionRule += FormatKVPair(setting, setting.GetValue(collectionRule._trigger));

                        ++triggerSettingIndex;
                    }
                }

                currCollectionRule += "}"; // end of settings
                currCollectionRule += "},"; // end of trigger

                currCollectionRule += FormatKVPairNonTextValue("Actions", "[");

                int actionIndex = 0;
                foreach (var action in collectionRule._actions)
                {
                    currCollectionRule += "{";

                    currCollectionRule += FormatKVPair("Type", (action._actionType).Name); // Pay special attention to whether this is correct

                    currCollectionRule += ",";

                    currCollectionRule += FormatKVPairNonTextValue("Settings", "{");

                    int actionSettingIndex = 0;
                    foreach (var setting in action._actionType.GetProperties())
                    {
                        if (null != setting.GetValue(action))
                        {
                            if (actionSettingIndex != 0)
                            {
                                currCollectionRule += ",";
                            }
                            currCollectionRule += FormatKVPair(setting, setting.GetValue(action));

                            ++actionSettingIndex;
                        }
                    }

                    currCollectionRule += "}"; // end of settings

                    currCollectionRule += "}"; // end of action

                    if (actionIndex != collectionRule._actions.Count - 1)
                    {
                        currCollectionRule += ",";
                    }

                    ++actionIndex;
                }

                currCollectionRule += "],";

                currCollectionRule += FormatKVPairNonTextValue("Limits", "{");

                int limitSettingIndex = 0;

                foreach (var setting in typeof(CRLimit).GetProperties())
                {
                    if (null != setting.GetValue(collectionRule._limit))
                    {
                        if (limitSettingIndex != 0)
                        {
                            currCollectionRule += ",";
                        }
                        currCollectionRule += FormatKVPair(setting, setting.GetValue(collectionRule._limit));

                        ++limitSettingIndex;
                    }
                }

                currCollectionRule += "}"; // end of limits

                currCollectionRule += "}"; // end of rule

                if (collectionRuleIndex != General._collectionRules.Count - 1)
                {
                    currCollectionRule += ",";
                }

                ++collectionRuleIndex;

                jsonString += currCollectionRule;
            }

            jsonString += "}"; // end of collection rules

            jsonString += "}"; // end of entire configuration

            Console.WriteLine("JsonString: " + jsonString);

            dynamic parsedJson = JsonConvert.DeserializeObject(jsonString);
            return JsonConvert.SerializeObject(parsedJson, Formatting.Indented);
        }

        private static string FormatKVPair(PropertyInfo setting, object settingValue)
        {
            string toReturn = "";

            Type t = General.GetType(setting.PropertyType);

            // Could really use a better solution than this...
            if (t == typeof(Int32))
            {
                toReturn += FormatKVPair(setting.Name, (int)settingValue);
            }
            else if (t == typeof(string) || t == typeof(bool))
            {
                toReturn += FormatKVPair(setting.Name, (string)settingValue);
            }
            else if (t == typeof(string[]))
            {
                toReturn += FormatKVPair(setting.Name, (string[])settingValue);
            }
            else if (t == typeof(TimeSpan))
            {
                TimeSpan? timespan = (TimeSpan?)settingValue;
                toReturn += FormatKVPair(setting.Name, timespan.ToString());
            }
            else if (t == typeof(double))
            {
                toReturn += FormatKVPair(setting.Name, (double?)settingValue);
            }
            else if (t.IsEnum)
            {
                toReturn += FormatKVPair(setting.Name, Enum.GetName(t, settingValue));
            }

            return toReturn;
        }

        private static string FormatKVPair(string key, string value)
        {
            return "\"" + key + "\": " + "\"" + value + "\"";
        }

        private static string FormatKVPairNonTextValue(string key, string value)
        {
            return "\"" + key + "\": " + value;
        }

        private static string FormatKVPair(string key, int value)
        {
            return "\"" + key + "\": " + value;
        }

        private static string FormatKVPair(string key, double? value)
        {
            return "\"" + key + "\": " + value;
        }

        private static string FormatKVPair(string key, string[] values)
        {
            string toReturn = "\"" + key + "\": [";

            int index = 0;
            foreach (var value in values)
            {
                toReturn += "\"" + value + "\"";

                if (index != values.Length - 1)
                {
                    toReturn += ", ";
                }

                ++index;
            }

            toReturn += "]";

            return toReturn;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostWay2(string data)
        {
            CollectionRuleCreationModel.collectionRuleIndex = -1;

            return RedirectToPage("./CollectionRuleCreation");
        }

        public IActionResult OnPostWay3(string data)
        {
            CollectionRuleCreationModel.collectionRuleIndex = int.Parse(data);

            return RedirectToPage("./CollectionRuleCreation");
        }
    }
}
