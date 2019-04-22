using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using System.Collections.Immutable;

namespace Lykke.Bil2.Ethereum.Utils
{
    [PublicAPI]
    public static class EnumerableExtensions
    {
        public static IReadOnlyList<TResult> SelectItems<TSource, TResult>(
            this IEnumerable<TSource> sources,
            Func<TSource, TResult> selector)
        {
            return sources
                .Select(selector)
                .ToImmutableArray();
        }
        
        public static async Task<IReadOnlyList<TResult>> SelectItemsAsync<TSource, TResult>(
            this IEnumerable<TSource> sources,
            Func<TSource, Task<TResult>> selector)
        {
            var results = await Task.WhenAll(sources.Select(selector));

            return results.ToImmutableArray();
        }
    }
}
