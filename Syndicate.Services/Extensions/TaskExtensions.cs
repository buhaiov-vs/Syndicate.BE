namespace Syndicate.Services.Extensions;

public static class TaskExtensions
{
    public static Task<D> OnResult<T, D>(this Task<T> task, Func<T, D> action)
    {
        return task.ContinueWith(x => action(x.Result));
    }
}
