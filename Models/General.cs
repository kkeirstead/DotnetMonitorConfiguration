using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

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

        public static string GetStringRepresentation(object propertyValue, Type t)
        {
            if (t == typeof(string[]))
            {
                return string.Join(',', (string[])propertyValue);
            }
            else
            {
                return propertyValue.ToString(); // Will need to add alternative representations for other types most likely
            }

        }

        public static object GetConstructorArgs(PropertyInfo propertyInfo, string propertyValue)
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
                else if (propsType.IsEnum)
                {
                    return Enum.Parse(propsType, propertyValue);
                }
            }

            return null;
        }

        public static object GetConstructorArgsLoaded(PropertyInfo propertyInfo, object propertyValue)
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
    }
}
