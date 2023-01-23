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
        public string TypeName { get; private set; }
        public string TypeDescription { get; private set; }
        public int Id {get;}
        
        public DependentType(string typeName, string typeDescription)
        {
            TypeName = typeName;
            TypeDescription = typeDescription;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return TypeName;
            yield return TypeDescription;
        }
    }
}
