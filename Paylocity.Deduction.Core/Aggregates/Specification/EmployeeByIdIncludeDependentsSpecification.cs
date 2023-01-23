using Ardalis.Specification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Paylocity.Deduction.Core.Aggregates.Specification
{
    public class EmployeeByIdIncludeDependentsSpecification : Specification<Employee>, ISingleResultSpecification
    {
        public EmployeeByIdIncludeDependentsSpecification(int id)
        {
            Query
              .Include(e => e.Dependents.Where(d=>d.EmployeeId==id))
              .ThenInclude(d=>d.DependentType)
              .Where(e => e.Id == id);
        }
    }
   
   
}
