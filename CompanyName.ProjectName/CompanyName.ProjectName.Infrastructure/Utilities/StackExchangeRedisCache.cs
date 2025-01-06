using CompanyName.ProjectName.Domain.SeedWork.Utilities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Utilities;

public class StackExchangeRedisCache : ICache
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;

    private readonly string Prefix;

    public StackExchangeRedisCache(IConnectionMultiplexer multiplexer, string instancePrefix)
    {
        if (string.IsNullOrWhiteSpace(instancePrefix))
        {
            throw new ArgumentException("Invalid instance prefix. Provide a valid string as prefix");
        }

        Prefix = instancePrefix.Trim().Replace(" ", "") ?? "";
        _connectionMultiplexer = multiplexer;
    }

    public async Task<T> GetAsync<T>(string key, bool hasAbsoluteKey = false)
    {
        string text = (hasAbsoluteKey ? key : (Prefix + "-" + key));
        RedisValue redisValue = await _connectionMultiplexer.GetDatabase().StringGetAsync(text);
        if (redisValue.IsNull)
        {
            return default(T);
        }

        return JsonSerializer.Deserialize<T>((string)redisValue);
    }

    public async Task<bool> SetAsync<T>(string key, T value)
    {
        string text = JsonSerializer.Serialize(value);
        return await _connectionMultiplexer.GetDatabase().StringSetAsync(Prefix + "-" + key, text);
    }

    public async Task<bool> SetAsync<T>(string key, T value, TimeSpan ttl)
    {
        string text = JsonSerializer.Serialize(value);
        return await _connectionMultiplexer.GetDatabase().StringSetAsync(Prefix + "-" + key, text, ttl);
    }

    public async Task<bool> RemoveAsync(string key)
    {
        return await _connectionMultiplexer.GetDatabase().KeyDeleteAsync(Prefix + "-" + key);
    }
}


