using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bot.Extensions
{
    /// <summary>
    /// Extension methods for the <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        /// Executes the for each loop over an enumerable in an async fashion.
        /// </summary>
        /// <param name="enumerable">The enumerable to loop over</param>
        /// <param name="action">The async action to execute</param>
        /// <typeparam name="T">The type contained in the enumerable</typeparam>
        /// <returns>The task representing the looping over.</returns>
        public static async Task ForEachAsync<T>(this IEnumerable<T> enumerable, Func<T, Task> action)
        {
            foreach (var v in enumerable)
            {
                await action(v);
            }
        }
    }
}