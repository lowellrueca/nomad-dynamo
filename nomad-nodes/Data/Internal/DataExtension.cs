using System;
using System.Collections.Generic;
using System.Linq;

namespace Data.Internal
{
    static class DataExtension
    {
        public static IEnumerable<TResult> Map<T1, T2, TResult>(this IEnumerable<T1> sourceA,
            IEnumerable<T2> sourceB, Func<T1, T2, TResult> func)
        {
            foreach (var (x, y) in sourceA.Zip(sourceB, (a, b) => (a, b)))
            {
                yield return func(x, y);
            }
        }

        public static IEnumerable<TResult> Map<T1, T2, T3, TResult>(this IEnumerable<T1> sourceA,
            IEnumerable<T2> sourceB, IEnumerable<T3> sourceC, Func<T1, T2, T3, TResult> func)
        {
            foreach (var ((x, y), z) in sourceA.Zip(sourceB, (a, b) => (a, b)).Zip(sourceC, (c, d) => (c, d)))
            {
                yield return func(x, y, z);
            }
        }
    }
}
