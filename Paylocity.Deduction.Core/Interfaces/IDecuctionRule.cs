using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.Interfaces
{
    public interface IDecuctionRule
    {
        decimal ApplyBaseRule(decimal amount, int count);
        decimal ApplyDiscountRule(decimal amount, decimal discountPercent, int count);
    }
}
