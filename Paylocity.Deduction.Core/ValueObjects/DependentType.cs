using Paylocity.Deduction.SharedKernel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.ValueObjects
{
    public class DependentType : ValueObject
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        
        public DependentType(string name, string description)
        {
            Name = name;
            Description = description;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Name;
            yield return Description;
        }
    }
}
