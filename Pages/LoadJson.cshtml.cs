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
                Dictionary<string, object> ruleNamesAndBodies = JsonConvert.DeserializeObject<Dictionary<string,object>>(JsonRules);

                List<CollectionRuleOptions> rules = new List<CollectionRuleOptions>() ;
                List<string> ruleNames = new List<string>();

                foreach (var entryKey in ruleNamesAndBodies.Keys)
                {
                    rules.Add(JsonConvert.DeserializeObject<CollectionRuleOptions>(ruleNamesAndBodies[entryKey].ToString()));
                    ruleNames.Add(entryKey);
                }

                int collectionRuleIndex = 0;
                foreach (CollectionRuleOptions rule in rules)
                {
                    CollectionRule tempRule = new CollectionRule(ruleNames[collectionRuleIndex]);

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
                                triggerConstructorArgs[triggerSettingIndex] = General.GetConstructorArgsLoaded(prop, triggerSettings[triggerKey]);

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
                                    actionConstructorArgs[actionSettingIndex] = General.GetConstructorArgsLoaded(prop, actionSettings[actionKey]);

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
