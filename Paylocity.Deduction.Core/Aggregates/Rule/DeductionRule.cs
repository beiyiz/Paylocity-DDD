using Paylocity.Deduction.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.Aggregates.Rule
{
    public class DeductionRule : IDecuctionRule
    {
        public decimal ApplyBaseRule(decimal amount, int count)
        {
            return amount * count;
        }

        public decimal ApplyDiscountRule(decimal amount, decimal discountPercent, int count)
        {
            return count * amount * (1- discountPercent/100);
        }
    }
}
