using DotnetMonitorConfiguration.Models.BorrowedFromDM;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DotnetMonitorConfiguration.Models.Collection_Rules
{
    public class CRFilter
    {
        public CRFilter(ProcessFilterKey key, string value, ProcessFilterType matchType)
        {
            Key = key;
            Value = value;
            MatchType = matchType;
        }

        [Required]
        public ProcessFilterKey Key { get; set; }

        [Required]
        public string Value { get; set; }

        [DefaultValue(ProcessFilterType.Exact)]
        public ProcessFilterType MatchType { get; set; }
    }
}
