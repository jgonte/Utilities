using System;
using System.Collections.Generic;
using System.Linq;

namespace Utilities
{
    public static class LinqExtensions
    {
        /// <summary>
        /// Projects each element of a sequence into a new form with immediate execution by calling ToList()
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static List<TResult> SelectToList<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToList();
        }

        /// <summary>
        /// Projects each element of a sequence into a new form with immediate execution by calling ToArray()
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TResult[] SelectToArray<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            return source.Select(selector).ToArray();
        }

        /// <summary>
        /// Returns true whether the source contains any of the value in values
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool ContainsAny<TSource>(this IEnumerable<TSource> source, params TSource[] values)
        {
            return source.Intersect(values).Any();
        }
    }
}
