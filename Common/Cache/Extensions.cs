using Microsoft.Extensions.Caching.Memory;

namespace MeowBot.Common.Cache;

public static class Extensions
{
    private static readonly TimeSpan DefaultExpiration = TimeSpan.FromMinutes(30);

    public static T SetWithDefault<T>(this IMemoryCache cache, object key, T value)
    {
        var options = new MemoryCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = DefaultExpiration
        };
        return cache.Set(key, value, options);
    }
}