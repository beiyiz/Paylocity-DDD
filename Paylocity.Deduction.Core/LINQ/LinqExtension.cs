using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Paylocity.Deduction.Core.LINQ
{
    public static class LinqExtension
    {
        private static bool IsNullableType(Type t)
        {
            return t.IsGenericType && t.GetGenericTypeDefinition() == typeof(Nullable<>);
        }
        private static Expression GetPropertyExpression(ParameterExpression parameter, string propertyName)
        {
            return Expression.Property(parameter, propertyName);
        }
        private static Expression<Func<T, bool>> GetExpressionForNull<T>(ParameterExpression parameter, FilterOperator filterOperator, Expression property)
        {
            switch (filterOperator)
            {
                case FilterOperator.Equals:
                case FilterOperator.EqualTo:
                    var nullOrEmptyCheck = Expression.Equal(property, Expression.Constant(string.Empty));
                    nullOrEmptyCheck = Expression.Or(nullOrEmptyCheck, Expression.Equal(property, Expression.Constant(null)));
                    return Expression.Lambda<Func<T, bool>>(nullOrEmptyCheck, parameter);
                default:
                    var notNullOrEmptyCheck = Expression.NotEqual(property, Expression.Constant(string.Empty));
                    notNullOrEmptyCheck = Expression.AndAlso(notNullOrEmptyCheck, Expression.NotEqual(property, Expression.Constant(null)));
                    return Expression.Lambda<Func<T, bool>>(notNullOrEmptyCheck, parameter);
            }
        }
        private static Expression<Func<T, bool>> GetExpressionForNotEqual<T>(ParameterExpression parameter, Expression property, string filter)
        {
            bool nullableProp = IsNullableType(property.Type);
            Expression notEqual = nullableProp ?
                Expression.Equal(Expression.Property(property, "HasValue"), Expression.Constant(true)) :
                Expression.NotEqual(parameter, Expression.Constant(null));

            Type nonnullableType = typeof(string);
            notEqual = Expression.AndAlso(notEqual, Expression.NotEqual(nullableProp ?
                 Expression.Convert(property, nonnullableType) :
                 property, Expression.Constant(filter)));

            return Expression.Lambda<Func<T, bool>>(notEqual, parameter);

        }
        private static Expression<Func<T, bool>> GetExpressionForEqualAndContains<T>(ParameterExpression parameter, Expression property, FilterOperator filterOperator, string filter)
        {
            MethodInfo miOperator;

            switch (filterOperator)
            {
                case FilterOperator.Equals:
                case FilterOperator.EqualTo:
                    miOperator = typeof(string).GetMethod("Equals", new Type[] { typeof(string) });
                    break;
                case FilterOperator.Contains:
                    miOperator = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                    break;
                case FilterOperator.StartsWith:
                    miOperator = typeof(string).GetMethod("StartsWith", new Type[] { typeof(string) });
                    break;
                case FilterOperator.EndsWith:
                    miOperator = typeof(string).GetMethod("EndsWith", new Type[] { typeof(string) });
                    break;
                case FilterOperator.FreeText:
                    var stringMethod = typeof(string).GetMethod("IndexOf", new Type[] { typeof(string) });

                    var stringTarget = Expression.Constant(filter);
                    var thisMethod = Expression.Call(property, stringMethod, stringTarget);
                    var equals = Expression.Equal(thisMethod, Expression.Constant(0));

                    return Expression.Lambda<Func<T, bool>>(equals, parameter);
                default:
                    miOperator = typeof(string).GetMethod("Contains", new Type[] { typeof(string) });
                    break;
            }

            // Add trim and lowercase
            MethodInfo miTrim = typeof(string).GetMethod("Trim", Type.EmptyTypes);
            MethodInfo miLower = typeof(string).GetMethod("ToLower", Type.EmptyTypes);

            // Trim (x.FirstName.Trim)
            Expression trimMethod = Expression.Call(property, miTrim);

            //// LowerCase (x.FirstName.Trim.ToLower)
            Expression toLowerMethod = Expression.Call(trimMethod, miLower);

            // The target value ( == "a")
            Expression target = Expression.Constant(filter.ToLower(), typeof(string));


            // Continue building the expression tree (x.FirstName.Trim().ToLower(), "StartsWith", "a")                
            Expression method = Expression.Call(toLowerMethod, miOperator, target);

            // Build the final expression ( x => x.Number.Trim.ToLower(), "StartsWith", "301" )
            return Expression.Lambda<Func<T, bool>>(method, parameter);

        }
        public static Expression<Func<T, bool>> GetStringExpression<T>(SearchFilter searchFilter)
        {
            ParameterExpression parameter = Expression.Parameter(typeof(T), "item");
            Expression property = GetPropertyExpression(parameter, searchFilter.Criteria);


            var filterOperator = searchFilter.Operator;

            if (string.IsNullOrWhiteSpace(searchFilter.Filter))
            {
                return GetExpressionForNull<T>(parameter, filterOperator, property);
            }
            else
            {
                if (filterOperator == FilterOperator.NotEquals || filterOperator == FilterOperator.NotEqualTo)
                {
                    return GetExpressionForNotEqual<T>(parameter, property, searchFilter.Filter);
                }
                else
                {
                    return GetExpressionForEqualAndContains<T>(parameter, property, filterOperator, searchFilter.Filter);
                }
            }
        }
    }
}
