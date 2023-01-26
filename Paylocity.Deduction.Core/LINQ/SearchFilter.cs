using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.LINQ
{
    public class SearchFilter
    {
        public string Criteria { get; set; }
        public FilterOperator Operator { get; set; }
        public string Filter { get; set; }

        public SearchFilter(string criteria,FilterOperator filterOperator, string filter)
        {
            Criteria= criteria;
            Filter= filter;
            Operator = filterOperator;
        }
    }
}
