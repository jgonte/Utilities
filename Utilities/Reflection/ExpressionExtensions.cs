using System;
using System.Linq.Expressions;

namespace Utilities
{
    public static class ExpressionExtensions
    {
        public static string GetPropertyName<T>(this Expression<Func<T, object>> expression)
        {
            if (expression == null)
            {
                throw new ArgumentNullException(nameof(expression));
            }

            if (expression.Body is MemberExpression) // Reference type property or field
            {
                var memberExpression = (MemberExpression)expression.Body;

                return memberExpression.Member.Name;
            }
            else if (expression.Body is UnaryExpression) // Property, field of method returning value type
            {
                var unaryExpression = (UnaryExpression)expression.Body;

                if (unaryExpression.Operand is MemberExpression)
                {
                    var memberExpression = (MemberExpression)unaryExpression.Operand;

                    return memberExpression.Member.Name;
                }
                else
                {
                    throw new InvalidOperationException("The operand of the unaty expression must be of type MemberExpression");
                }
            }
            else
            {
                throw new InvalidOperationException("The body of the expression must be of type MemberExpression or UnaryExpression");
            }
        }
    }
}
