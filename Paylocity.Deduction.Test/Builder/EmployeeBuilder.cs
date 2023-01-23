using Paylocity.Deduction.Core.Aggregates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Test.Builder
{
    public class EmployeeBuilder
    {
        private Employee _employee;

        public EmployeeBuilder()
        {
            WithDefaultValues();
        }

        

        public EmployeeBuilder WithDefaultValues()
        {
            _employee = new Employee("Ftest", "Ltest");

            return this;
        }

        public Employee Build() => new Employee("Ftest", "LTest");
    }
}