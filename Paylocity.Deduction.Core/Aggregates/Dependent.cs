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
        public Dependent(int employeeId, string dependent_FirstName, string dependent_LastName, int dependentTypeId)
        {
            EmployeeId = employeeId;
            Dependent_FirstName = dependent_FirstName;
            Dependent_LastName = dependent_LastName;
            DependentTypeId = dependentTypeId;
        }

        public int EmployeeId { get; private set; }
        public string Dependent_FirstName { get; private set; }
        public string Dependent_LastName { get; private set; }

        public int DependentTypeId { get; private set; }
        public DependentType DependentType { get; private set; }

        public void UpdateName(string firstName, string lastName)
        {
            Dependent_FirstName = firstName;
            Dependent_LastName = lastName;
        }

        
    }

}
