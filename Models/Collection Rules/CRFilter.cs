using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CRFilter
    {
        public CRFilter(string key, string value, string matchType)
        {
            Key = key;
            Value = value;
            MatchType = matchType;
        }

        public string Key { get; set; }
        public string Value { get; set; }

        public string MatchType { get; set; }
    }
}
