using Paylocity.Deduction.Core.Aggregates.Constants;
using Paylocity.Deduction.Core.Interfaces;
using Paylocity.Deduction.Core.LINQ;
using Paylocity.Deduction.SharedKernel.Interface;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.Aggregates
{
    public class DeductionCompute : IAggregateRoot
    {
        private readonly IDecuctionRule _rule;
        private readonly Employee _employee;
        public DeductionCompute(
            IDecuctionRule rule,
            Employee employee
            )
        {
            _employee = employee;
            _rule = rule;
        }


        public decimal Calculate()
        {
            decimal deductable =_rule.ApplyBaseRule( EmployeeConstants.DEFAULT_EMPLOYEE_DEDUCTION, 1);

            var predefinedDiscountRuleForDependent = LinqExtension.GetStringExpression<Dependent>(EmployeeConstants.PREDEFINED_RULE);

            var predefinedDiscountRuleForEmployee = LinqExtension.GetStringExpression<Employee>(EmployeeConstants.PREDEFINED_RULE);

            var qualifiedForDiscount = (new List<Employee>() { _employee }).AsQueryable().Any(predefinedDiscountRuleForEmployee);

            if (qualifiedForDiscount)
            {
                deductable = _rule.ApplyDiscountRule(EmployeeConstants.DEFAULT_EMPLOYEE_DEDUCTION, EmployeeConstants.DISCOUNT_PERCENT, 1);
            }

            if (_employee.Dependents?.Any() ?? false)
            {
                var count1 = _employee.Dependents.AsQueryable().Count(predefinedDiscountRuleForDependent);
                var count2 = _employee.Dependents.Count - count1;

                deductable += _rule.ApplyBaseRule(EmployeeConstants.DEFAULT_EMPLOYEE_DEDUCTION, count2);
                deductable += _rule.ApplyDiscountRule(EmployeeConstants.DEFAULT_EMPLOYEE_DEDUCTION, EmployeeConstants.DISCOUNT_PERCENT, count1);

            }
            return deductable;

        }
    }
}
