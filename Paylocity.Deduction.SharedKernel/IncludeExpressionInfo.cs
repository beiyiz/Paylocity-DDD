using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.SharedKernel
{
    public class IncludeExpressionInfo
    {
        public LambdaExpression LambdaExpression { get; }

        public Type EntityType { get; }

        public Type PropertyType { get; }

        public Type? PreviousPropertyType { get; }

        public Enum.IncludeTypeEnum Type { get; }

        private IncludeExpressionInfo(LambdaExpression expression, Type entityType, Type propertyType, Type? previousPropertyType, Enum.IncludeTypeEnum includeType)
        {
            if (expression == null)
            {
                throw new ArgumentNullException("expression");
            }

            if ((object)entityType == null)
            {
                throw new ArgumentNullException("entityType");
            }

            if ((object)propertyType == null)
            {
                throw new ArgumentNullException("propertyType");
            }

            if (includeType == Enum.IncludeTypeEnum.ThenInclude && (object)previousPropertyType == null)
            {
                throw new ArgumentNullException("previousPropertyType");
            }

            LambdaExpression = expression;
            EntityType = entityType;
            PropertyType = propertyType;
            PreviousPropertyType = previousPropertyType;
            Type = includeType;
        }

        public IncludeExpressionInfo(LambdaExpression expression, Type entityType, Type propertyType)
            : this(expression, entityType, propertyType, null, Enum.IncludeTypeEnum.Include)
        {
        }

        public IncludeExpressionInfo(LambdaExpression expression, Type entityType, Type propertyType, Type previousPropertyType)
            : this(expression, entityType, propertyType, previousPropertyType, Enum.IncludeTypeEnum.ThenInclude)
        {
        }
    }
}
