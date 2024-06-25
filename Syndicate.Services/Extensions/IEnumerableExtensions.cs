namespace Syndicate.Services.Extensions;

public static class IEnumerableExtensions
{
    public static bool Empty<T>(this IEnumerable<T> collection)
    {
        return !collection.Any();
    }

    /// <summary>
    /// Gets the first value that satisfies <paramref name="predicate"/>.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="source"></param>
    /// <param name="predicate"></param>
    /// <param name="value"></param>
    /// <returns><see langword="true"/> if <paramref name="source"/> contains element that satisfies <paramref name="predicate"/>; otherwise, <see langword="false"/>.</returns>
    public static bool TryGetFirstValue<T>(this IEnumerable<T> source, Func<T, bool> predicate, out T? value)
    {
        foreach(var item in source)
        {
            if (predicate(item))
            {
                value = item;
                return true;
            }
        }

        value = default;
        return false;
    }
}
