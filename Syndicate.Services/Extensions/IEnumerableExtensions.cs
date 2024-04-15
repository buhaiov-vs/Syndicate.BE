namespace Syndicate.Services.Extensions;

public static class IEnumerableExtensions
{
    public static bool Empty<T>(this IEnumerable<T> collection)
    {
        return !collection.Any();
    }
}
