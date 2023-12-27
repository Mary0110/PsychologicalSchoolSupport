using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using PsychologicalSupportPlatform.Common.Config;
using PsychologicalSupportPlatform.Edu.Application.Interfaces;

namespace PsychologicalSupportPlatform.Edu.Application.Services;

public class CacheService: ICacheService
{
    private readonly DistributedCacheEntryOptions _cacheOptions;
    private readonly IDistributedCache _cache;

    public CacheService(IDistributedCache cache, IOptions<RedisConfig> redisOption)
    {
        _cache = cache;
        _cacheOptions = new DistributedCacheEntryOptions()
            .SetSlidingExpiration(TimeSpan.FromMinutes(redisOption.Value.SlidingExpiration))
            .SetAbsoluteExpiration(TimeSpan.FromMinutes(redisOption.Value.AbsoluteExpiration));
    }
    
    public async Task<T?> GetAsync<T>(string key, CancellationToken cancellationToken = default) where T : class
    {
        var cachedValue = await _cache.GetStringAsync(key, cancellationToken);

        if (cachedValue is null)
        {
            return null;
        }

        var value = JsonConvert.DeserializeObject<T>(cachedValue) ;

        return value;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken cancellationToken = default) where T : class
    {
        var cacheValue = JsonConvert.SerializeObject(value);
        await _cache.SetStringAsync(key, cacheValue, _cacheOptions, cancellationToken);
    }

    public async Task DeleteAsync(string key, CancellationToken cancellationToken = default)
    {
        await _cache.RemoveAsync(key, cancellationToken);
    }
}
