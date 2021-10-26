// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace DotnetMonitorConfiguration.Models
{
    public class General
    {
        public static IEnumerable<string> _triggerTypes = new List<string>() { nameof(AspNetRequestCount), nameof(AspNetRequestDuration), nameof(AspNetResponseStatus), nameof(EventCounter) };

        public static IEnumerable<string> _actionTypes = new List<string>() { nameof(CollectDump), nameof(CollectGCDump), nameof(CollectLogs), nameof(CollectTrace), nameof(Execute) };

        public static IList<CollectionRule> _collectionRules = new List<CollectionRule>() { };

        public static string GetFormatDescription(Type type)
        {
            if (type == typeof(Int32))
            {
                return "Provide a non-negative integer (e.g. 5)";
            }
            if (type == typeof(string))
            {
                return "Provide a string; spaces are permitted (e.g. MyString)";
            }
            if (type == typeof(string[]))
            {
                return "Provide a set of comma separated strings (e.g. First string,Second string,Third string)";
            }
            if ((null != type && type.IsEnum) || typeof(bool) == type)
            {
                return "Select an option from the dropdown.";
            }
            if (type == typeof(double))
            {
                return "Provide a non-negative floating-point number (e.g. 3.3)";
            }
            if (type == typeof(TimeSpan))
            {
                return "Use the TimeSpan format of hh:mm:ss (e.g. 01:30:00)";
            }

            return "";
        }

        public static Type GetType(Type t)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(t);
            return (underlyingType != null) ? underlyingType : t;
        }

        public static string GetStringRepresentation(object currValue, PropertyInfo propertyInfo)
        {
            object propertyValue = propertyInfo.GetValue(currValue);

            Type t = propertyInfo.PropertyType;

            if (propertyValue == null)
            {
                return "";
            }

            if (t == typeof(string[]))
            {
                return string.Join(',', (string[])propertyValue);
            }
            else
            {
                return propertyValue.ToString(); // May need to add alternative representations for other types
            }
        }

        public static object[] GetConstructorArgs(PropertyInfo[] typeProperties, Dictionary<string, string> userAssignedProperties)
        {
            object[] constructorArgs = new object[typeProperties.Length];

            foreach (var key in userAssignedProperties.Keys)
            {
                int index = int.Parse(key);

                if (typeProperties[index].PropertyType == typeof(List<EventPipeProvider>))
                {
                    // Special case since we handle EventPipeProviders on a separate screen
                    continue;
                }

                bool isDefined = Attribute.IsDefined(typeProperties[index], typeof(RequiredAttribute));

                if (isDefined && string.IsNullOrEmpty(userAssignedProperties[key]))
                {
                    return null;
                }

                try
                {
                    constructorArgs[index] = GetConstructorArgument(typeProperties[index], userAssignedProperties[key]);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }

            return constructorArgs;
        }

        public static object GetConstructorArgument(PropertyInfo propertyInfo, string propertyValue)
        {
            Type propsType = GetType(propertyInfo.PropertyType);

            if (null == propertyValue)
            {
                return null;
            }
            else
            {
                if (propsType == typeof(Int32))
                {
                    return int.Parse(propertyValue);
                }
                else if (propsType == typeof(string))
                {
                    return propertyValue;
                }
                else if (propsType == typeof(TimeSpan))
                {
                    return TimeSpan.Parse(propertyValue);
                }
                else if (propsType == typeof(string[]))
                {
                    return propertyValue.Split(',');
                }
                else if (propsType == typeof(double))
                {
                    return Convert.ToDouble(propertyValue);
                }
                else if (propsType == typeof(bool))
                {
                    return bool.Parse(propertyValue);
                }
                else if (propsType.IsEnum)
                {
                    return Enum.Parse(propsType, propertyValue);
                }
            }

            return null;
        }

        public static object GetConstructorArgumentJSONLoading(PropertyInfo propertyInfo, object propertyValue)
        {
            Type propsType = GetType(propertyInfo.PropertyType);

            if (propsType == typeof(Int32))
            {
                return Convert.ToInt32(propertyValue);
            }
            else if (propsType == typeof(string))
            {
                return propertyValue;
            }
            else if (propsType == typeof(TimeSpan))
            {
                return TimeSpan.Parse((string)propertyValue);
            }
            else if (propsType == typeof(string[]))
            {
                return ((Newtonsoft.Json.Linq.JArray)propertyValue).ToObject<string[]>();
            }
            else if (propsType == typeof(double))
            {
                return Convert.ToDouble(propertyValue);
            }
            else if (propsType == typeof(bool))
            {
                return propertyValue.ToString();
            }
            else if (propsType.IsEnum)
            {
                return Enum.Parse(propsType, (string)propertyValue);
            }
            else
            {
                return propertyValue;
            }
        }

        public static CollectionRuleOptions SerializeCollectionRule(CollectionRule rule)
        {
            // Actions

            List<CollectionRuleActionOptions> actionOptionsList = new List<CollectionRuleActionOptions>();

            foreach (CRAction action in rule._actions)
            {
                CollectionRuleActionOptions actionOptions = new CollectionRuleActionOptions();

                actionOptions.Name = action.Name;
                actionOptions.Type = action._actionType.Name;
                actionOptions.WaitForCompletion = action.WaitForCompletion;

                PropertyInfo[] actionPropertyInfos = action._actionType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

                Dictionary<string, object> actionSettings = new Dictionary<string, object>(); 

                foreach (var propertyInfo in actionPropertyInfos)
                {
                    // Don't love this, but the bindings above aren't working.
                    if (!(propertyInfo.Name.Equals("Name") || propertyInfo.Name.Equals("WaitForCompletion")) && null != propertyInfo.GetValue(action))
                    {
                        actionSettings[propertyInfo.Name] = propertyInfo.GetValue(action); // Make sure this piece works
                    }
                }

                actionOptions.Settings = actionSettings;

                actionOptionsList.Add(actionOptions);
            }

            // Trigger

            CollectionRuleTriggerOptions triggerOptions = new CollectionRuleTriggerOptions();

            triggerOptions.Type = rule._trigger._triggerType.Name;

            PropertyInfo[] triggerPropertyInfos = rule._trigger._triggerType.GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance);

            Dictionary<string, object> triggerSettings = new Dictionary<string, object>();

            foreach (var propertyInfo in triggerPropertyInfos)
            {
                if (null != propertyInfo.GetValue(rule._trigger))
                {
                    triggerSettings[propertyInfo.Name] = propertyInfo.GetValue(rule._trigger); // Make sure this piece works
                }
            }

            triggerOptions.Settings = triggerSettings;

            // CollectionRuleOptions

            CollectionRuleOptions collectionRuleOptions = new CollectionRuleOptions();

            collectionRuleOptions.Actions = actionOptionsList;
            collectionRuleOptions.Trigger = triggerOptions;
            collectionRuleOptions.Limits = rule._limit;
            collectionRuleOptions.Filters = rule._filters;

            return collectionRuleOptions;
        }

        public static void ConvertJsonToCollectionRules(string JsonRules)
        {
            Dictionary<string, object> ruleNamesAndBodies = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonRules);

            List<CollectionRuleOptions> rules = new List<CollectionRuleOptions>();
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
                    string currKey = prop.Name;

                    triggerConstructorArgs[triggerSettingIndex] = null; // May be unnecessary

                    foreach (string triggerKey in triggerSettings.Keys)
                    {
                        if (triggerKey.Equals(currKey))
                        {
                            triggerConstructorArgs[triggerSettingIndex] = GetConstructorArgumentJSONLoading(prop, triggerSettings[triggerKey]);

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
                        string currKey = prop.Name;

                        actionConstructorArgs[actionSettingIndex] = null; // May be unnecessary

                        foreach (string actionKey in actionSettings.Keys)
                        {
                            if (actionKey.Equals(currKey))
                            {
                                actionConstructorArgs[actionSettingIndex] = GetConstructorArgumentJSONLoading(prop, actionSettings[actionKey]);

                                break;
                            }
                        }

                        actionSettingIndex++;
                    }

                    var actionCtors = actionType.GetConstructors();

                    CRAction crAction = (CRAction)actionCtors[0].Invoke(actionConstructorArgs);
                    crAction._actionType = actionType;
                    crAction.Name = action.Name;
                    crAction.WaitForCompletion = action.WaitForCompletion;

                    tempRule._actions.Add(crAction);
                }

                _collectionRules.Add(tempRule);

                ++collectionRuleIndex;
            }
        }

        public static List<string> GetEnumNames(Type currType, bool isRequired)
        {
            List<string> names = (currType.IsEnum) ? Enum.GetNames(currType).ToList() : new List<string>() { "true", "false" };

            if (!isRequired)
            {
                names.Insert(0, ""); // Default option should be blank
            }

            return names;
        }

        public static string GetCurrValueAction(PropertyInfo propertyInfo, int actionIndex, int collectionRuleIndex)
        {
            if (actionIndex == -1)
            {
                return "";
            }

            CRAction currAction = _collectionRules[collectionRuleIndex]._actions[actionIndex];

            return GetStringRepresentation(currAction, propertyInfo);
        }

        public static string GenerateNameValue(PropertyInfo propertyInfo)
        {
            bool isRequired = Attribute.IsDefined(propertyInfo, typeof(RequiredAttribute));

            return propertyInfo.Name + (isRequired ? "*" : "");
        }
    }
}
