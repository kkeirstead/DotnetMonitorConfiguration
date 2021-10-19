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

            /*
            string json = "[{\"Filters\": [{\"Key\": \"ProcessName\",\"Value\": \"dotnet\",\"MatchType\": \"Exact\"}],\"Trigger\": {\"Type\": \"EventCounter\",\"Settings\": {\"ProviderName\": \"System.Runtime\",\"CounterName\": \"cpu-usage\",\"GreaterThan\": 70,\"SlidingWindowDuration\": \"00:00:10\"}},\"Actions\": [{\"Type\": \"CollectTrace\",\"Settings\": {\"Profile\": \"Cpu\",\"Duration\": \"00:01:00\"}}],\"Limits\": {\"ActionCount\": 2,\"ActionCountSlidingWindowDuration\": \"1:00:00\"}},{\"Filters\": [{\"Key\": \"ProcessName\",\"Value\": \"dotnet\",\"MatchType\": \"Exact\"}],\"Trigger\": {\"Type\": \"EventCounter\",\"Settings\": {\"ProviderName\": \"System.Runtime\",\"CounterName\": \"cpu-usage\",\"GreaterThan\": 80,\"SlidingWindowDuration\": \"00:00:10\"}},\"Actions\": [{\"Type\": \"CollectTrace\",\"Settings\": {\"Profile\": \"Cpu\",\"Duration\": \"00:11:00\"}}],\"Limits\": {\"ActionCount\": 4,\"ActionCountSlidingWindowDuration\": \"4:00:00\"}}]";

            // [{"Filters": [{"Key": "ProcessName","Value": "dotnet","MatchType": "Exact"}],"Trigger": {"Type": "EventCounter","Settings": {"ProviderName": "System.Runtime","CounterName": "cpu-usage","GreaterThan": 70,"SlidingWindowDuration": "00:00:10"}},"Actions": [{"Type": "CollectTrace","Settings": {"Profile": "Cpu","Duration": "00:01:00"}}],"Limits": {"ActionCount": 2,"ActionCountSlidingWindowDuration": "1:00:00"}},{"Filters": [{"Key": "ProcessName","Value": "dotnet","MatchType": "Exact"}],"Trigger": {"Type": "EventCounter","Settings": {"ProviderName": "System.Runtime","CounterName": "cpu-usage","GreaterThan": 80,"SlidingWindowDuration": "00:00:10"}},"Actions": [{"Type": "CollectTrace","Settings": {"Profile": "Cpu","Duration": "00:04:00"}}],"Limits": {"ActionCount": 3,"ActionCountSlidingWindowDuration": "2:00:00"}}]

            // {"HighCpuRule":{"Filters": [{"Key": "ProcessName","Value": "dotnet","MatchType": "Exact"}],"Trigger": {"Type": "EventCounter","Settings": {"ProviderName": "System.Runtime","CounterName": "cpu-usage","GreaterThan": 70,"SlidingWindowDuration": "00:00:10"}},"Actions": [{"Type": "CollectTrace","Settings": {"Profile": "Cpu","Duration": "00:01:00"}}],"Limits": {"ActionCount": 2,"ActionCountSlidingWindowDuration": "1:00:00"}}}



            CollectionRuleOptions[] rules = JsonConvert.DeserializeObject<CollectionRuleOptions[]>(json);

            int collectionRuleIndex = 0;
            foreach (CollectionRuleOptions rule in rules)
            {
                CollectionRule tempRule = new CollectionRule("temp");

                Type triggerType = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types." + rule.Trigger.Type);

                Dictionary<string, object> triggerSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(rule.Trigger.Settings.ToString());

                var triggerProps = triggerType.GetProperties();

                object[] triggerConstructorArgs = new object[triggerProps.Length];

                int triggerSettingIndex = 0;

                foreach (var prop in triggerProps)
                {
                    Type propsType = General.GetType(prop.PropertyType);

                    string currKey = prop.Name;

                    triggerConstructorArgs[triggerSettingIndex] = null; // May be unnecessary

                    foreach (string triggerKey in triggerSettings.Keys)
                    {
                        if (triggerKey.Equals(currKey))
                        {
                            if (propsType == typeof(Int32))
                            {
                                triggerConstructorArgs[triggerSettingIndex] = (int)triggerSettings[triggerKey];
                            }
                            else if (propsType == typeof(string))
                            {
                                triggerConstructorArgs[triggerSettingIndex] = triggerSettings[triggerKey];
                            }
                            else if (propsType == typeof(TimeSpan))
                            {
                                triggerConstructorArgs[triggerSettingIndex] = TimeSpan.Parse((string)triggerSettings[triggerKey]);
                            }
                            else if (propsType == typeof(string[]))
                            {
                                triggerConstructorArgs[triggerSettingIndex] = (string[])triggerSettings[triggerKey];
                            }
                            else if (propsType == typeof(double))
                            {
                                triggerConstructorArgs[triggerSettingIndex] = Convert.ToDouble(triggerSettings[triggerKey]);
                            }
                            else if (propsType.IsEnum)
                            {
                                triggerConstructorArgs[triggerSettingIndex] = Enum.Parse(propsType, (string)triggerSettings[triggerKey]);
                            }
                            else
                            {
                                triggerConstructorArgs[triggerSettingIndex] = triggerSettings[triggerKey];
                            }

                            break;
                        }
                    }

                    triggerSettingIndex++;
                }

                var triggerCtors = triggerType.GetConstructors();

                CRTrigger trigger = (CRTrigger)triggerCtors[0].Invoke(triggerConstructorArgs);
                trigger._triggerType = triggerType;

                tempRule._trigger = trigger;

                tempRule._limit = rule.Limits;

                tempRule._filters = rule.Filters;

                foreach (var action in rule.Actions)
                {
                    Type actionType = Type.GetType("DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types." + action.Type);

                    Dictionary<string, object> actionSettings = JsonConvert.DeserializeObject<Dictionary<string, object>>(action.Settings.ToString());

                    var actionProps = actionType.GetProperties();

                    object[] actionConstructorArgs = new object[actionProps.Length];

                    int actionSettingIndex = 0;

                    foreach (var prop in actionProps)
                    {
                        Type propsType = General.GetType(prop.PropertyType);

                        string currKey = prop.Name;

                        actionConstructorArgs[actionSettingIndex] = null; // May be unnecessary

                        foreach (string actionKey in actionSettings.Keys)
                        {
                            if (actionKey.Equals(currKey))
                            {
                                if (propsType == typeof(Int32))
                                {
                                    actionConstructorArgs[actionSettingIndex] = (int)actionSettings[actionKey];
                                }
                                else if (propsType == typeof(string))
                                {
                                    actionConstructorArgs[actionSettingIndex] = actionSettings[actionKey];
                                }
                                else if (propsType == typeof(TimeSpan))
                                {
                                    actionConstructorArgs[actionSettingIndex] = TimeSpan.Parse((string)actionSettings[actionKey]);
                                }
                                else if (propsType == typeof(string[]))
                                {
                                    actionConstructorArgs[actionSettingIndex] = (string[])actionSettings[actionKey];
                                }
                                else if (propsType == typeof(double))
                                {
                                    actionConstructorArgs[actionSettingIndex] = Convert.ToDouble(actionSettings[actionKey]);
                                }
                                else if (propsType.IsEnum)
                                {
                                    actionConstructorArgs[actionSettingIndex] = Enum.Parse(propsType, (string)actionSettings[actionKey]);
                                }
                                else
                                {
                                    actionConstructorArgs[actionSettingIndex] = actionSettings[actionKey];
                                }

                                break;
                            }
                        }

                        actionSettingIndex++;
                    }

                    var actionCtors = actionType.GetConstructors();

                    CRAction crAction = (CRAction)actionCtors[0].Invoke(actionConstructorArgs);

                    tempRule._actions.Add(crAction);
                }

                General._collectionRules.Add(tempRule);

                ++collectionRuleIndex;
            }
            */
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostWay2(string data)
        {
            return RedirectToPage("./CollectionRules");
        }
    }
}
