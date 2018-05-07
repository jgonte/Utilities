using System;
using System.Linq.Expressions;

namespace Utilities
{
    public static class ExpressionHelper
    {
        /// <summary>
        /// Casts the expression from to the type to
        /// </summary>
        /// <param name="from">Expression to cast from</param>
        /// <param name="to">Target to cast to</param>
        /// <returns></returns>
        public static UnaryExpression Cast(Expression from, Type to)
        {
            return to.IsValueType ? Expression.Convert(from, to) : Expression.TypeAs(from, to);
        }


    }
}
