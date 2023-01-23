using Paylocity.Deduction.Core.Aggregates.Constants;
using Paylocity.Deduction.Core.Interfaces;
using Paylocity.Deduction.SharedKernel;
using Paylocity.Deduction.SharedKernel.Interface;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Paylocity.Deduction.Core.Aggregates
{
    public class Employee : BaseEntity<int>, ICompute, IAggregateRoot
    {
        public Employee(int id, string firstName, string lastName, decimal? annualSalary)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            AnnualSalary = annualSalary;
            Dependents = new List<Dependent>();
        }


        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public decimal? AnnualSalary { get; private set; }
        public IList<Dependent> Dependents { get; private set; } = new List<Dependent>();

        public decimal Calculate()
        {
            var deductable = EmployeeConstants.DEFAULT_EMPLOYEE_DEDUCTION;
            if (this.FirstName.StartsWith("A")) { deductable = deductable * (100-EmployeeConstants.DISCOUNT_PERCENT) / 100; }

            if (this.Dependents != null && this.Dependents.Any())
            {
                var cnt1 = this.Dependents.Count(x => x.Dependent_FirstName.StartsWith("A"));
                var cnt2 = this.Dependents.Count - cnt1;

                
                deductable += EmployeeConstants.DEFAULT_DEPENDENT_DEDUCTION * (cnt2 + (100-EmployeeConstants.DISCOUNT_PERCENT)/100 * cnt1);
                
            }
            return deductable;
        }

        
    }
}
