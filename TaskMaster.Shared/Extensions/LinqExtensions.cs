namespace TaskMaster.Shared.Extensions
{
    /// <summary>
    /// Provides extension methods for <see cref="IEnumerable{T}"/>.
    /// </summary>
    public static class LinqExtensions
    {
        /// <summary>
        /// Determines whether the specified <paramref name="items"/> is null or an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="items">The collection to check.</param>
        /// <returns><c>true</c> if the collection is null or empty; otherwise, <c>false</c>.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return items == null || !items.Any();
        }

        /// <summary>
        /// Determines whether the specified <paramref name="items"/> is not null and not an empty collection.
        /// </summary>
        /// <typeparam name="T">The type of elements in the collection.</typeparam>
        /// <param name="items">The collection to check.</param>
        /// <returns><c>true</c> if the collection is not null and not empty; otherwise, <c>false</c>.</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> items)
        {
            return !items.IsNullOrEmpty();
        }
    }
}
