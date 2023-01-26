using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.LINQ
{
    public enum FilterOperator
    {
        Equals,
        EqualTo,
        NotEquals,
        NotEqualTo,
        Contains,
        StartsWith,
        EndsWith,
        Before,
        PriorTo,
        LessThan,
        LessThanOrEqual,
        LessThanOrEqualTo,
        GreaterThan,
        GreaterThanOrEqual,
        GreaterThanOrEqualTo,
        DayOfWeek,
        WeekDay,
        FreeText,
        Default
    }
}
