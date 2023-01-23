using Paylocity.Deduction.Core.ValueObjects;
using Paylocity.Deduction.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.Aggregates
{
    public class Dependent : BaseEntity<int>
    {
        public Dependent(int employeeId, string firstName, string lastName)
        {
            EmployeeId = employeeId;
            FirstName = firstName;
            LastName = lastName;
        }

        public int EmployeeId { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }

        public DependentType DependentType { get; private set; }

        public void SetDependentType(DependentType dependentType)
        {
            this.DependentType = dependentType;
        }

        
    }

}
