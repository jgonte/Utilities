using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Utilities
{
    public static class CollectionExtensions
    {
        /// <summary>
        /// Loops through a list and executes the action on every item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action">The delegate with the item of the list as well as the index</param>
        public static void For<T>(this IList<T> list, Action<T, int> action)
        {
            if (list == null)
            {
                return;
            }

            int count = list.Count;

            for (int i = 0; i < count; ++i)
            {
                T item = list[i];
                action(item, i);
            }
        }

        /// <summary>
        /// Loops through a list and executes the action on every item
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="action">The delegate with the item of the list as well as the index and the total count</param>
        public static void For<T>(this IList<T> list, Action<T, int, int> action)
        {
            if (list == null)
            {
                return;
            }

            int count = list.Count;

            for (int i = 0; i < count; ++i)
            {
                T item = list[i];
                action(item, i, count);
            }
        }

        public static void For<T>(this IEnumerable<T> enumerable, Action<T, int> action)
        {
            enumerable.ToList().For<T>(action);
        }

        /// <summary>
        /// Join a string inside the builder delimiting it by the separator
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="builder"></param>
        /// <param name="separator"></param>
        /// <param name="action"></param>
        /// <param name="removeLastSeparator"></param>
        public static void Join<T>(this IList<T> list, StringBuilder builder, string separator, Action<StringBuilder, T, int> action, bool removeLastSeparator = true)
        {
            int lastSeparator = -1;

            list.For((item, index) =>
            {
                action(builder, item, index);

                lastSeparator = builder.Length;
                builder.Append(separator);
            });

            if (removeLastSeparator
                && lastSeparator > -1)
            {
                builder.Remove(lastSeparator, separator.Length);
            }
        }

        public static IEnumerable<T> SkipLast<T>(this IEnumerable<T> source, uint count)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            var toTake = source.Count() - count;

            if (toTake < 1)
            {
                return Enumerable.Empty<T>();
            }

            Queue<T> saveList = new Queue<T>();

            int i = 0;

            foreach (T item in source)
            {
                if (i < toTake)
                {
                    saveList.Enqueue(item);

                    ++i;
                }
            }

            return saveList.AsEnumerable();
        }
    }
}
