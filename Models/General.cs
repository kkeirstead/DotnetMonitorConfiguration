using DotnetMonitorConfiguration.Models.Collection_Rules;
using DotnetMonitorConfiguration.Models.Collection_Rules.Action_Types;
using DotnetMonitorConfiguration.Models.Collection_Rules.Trigger_Types;
using System;
using System.Collections.Generic;

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
            if (null != type && type.IsEnum)
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
    }
}
