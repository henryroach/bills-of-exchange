using System;
using System.Collections.Generic;
using System.Linq;

namespace BillsOfExchange.DataProvider.Extensions
{
    internal static class EnumerableExtensions
    {
        /// <summary>
        /// Orders IEnumerable (entities) to match IEnumerable that represents the order (ids used to resolve)
        /// </summary>
        /// <typeparam name="TElement"></typeparam>
        /// <typeparam name="TSequence"></typeparam>
        /// <param name="source"></param>
        /// <param name="sequence"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static IEnumerable<TElement> SortBySequence<TElement, TSequence>(
            this IEnumerable<TElement> source,
            IEnumerable<TSequence> sequence,
            Func<TElement, TSequence> selector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (sequence == null) throw new ArgumentNullException(nameof(sequence));
            if (selector == null) throw new ArgumentNullException(nameof(selector));

            TSequence SelectKey(TElement element)
            {
                if (element == null)
                    return default;

                TSequence resolvedKey = selector(element);
                if (resolvedKey == null)
                    throw new InvalidOperationException("Key of element is null.");

                return resolvedKey;
            }

            IDictionary<TSequence, TElement> dict = source.ToDictionary(SelectKey);

            foreach (TSequence orderItem in sequence)
            {
                if (dict.TryGetValue(orderItem, out TElement valueItem))
                {
                    yield return valueItem;
                    continue;
                }

                yield return default;
            }
        }
    }
}