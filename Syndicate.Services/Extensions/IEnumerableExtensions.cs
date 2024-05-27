namespace Syndicate.Services.Extensions;

public static class IEnumerableExtensions
{
    public static bool Empty<T>(this IEnumerable<T> collection)
    {
        return !collection.Any();
    }

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

        value = default(T);
        return false;
    }
}
