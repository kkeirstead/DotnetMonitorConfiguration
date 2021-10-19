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

        public IActionResult OnPostWay2(string data)
        {
            Console.WriteLine("JsonRules: " + JsonRules);

            if (!string.IsNullOrEmpty(JsonRules))
            {

                CollectionRuleOptions[] rules = JsonConvert.DeserializeObject<CollectionRuleOptions[]>(JsonRules);

                int collectionRuleIndex = 0;
                foreach (CollectionRuleOptions rule in rules)
                {
                    CollectionRule tempRule = new CollectionRule("temp" + collectionRuleIndex.ToString());

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
                        crAction._actionType = actionType;

                        tempRule._actions.Add(crAction);
                    }

                    General._collectionRules.Add(tempRule);

                    ++collectionRuleIndex;
                }

                return RedirectToPage("./CollectionRules/Start");
            }

            return null;
        }
    }
}
