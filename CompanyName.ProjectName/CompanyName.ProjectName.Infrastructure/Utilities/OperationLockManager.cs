using CompanyName.ProjectName.Domain.SeedWork.Utilities;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Utilities;

public class OperationLockManager : IOperationLockManager
{
    private IDatabase _database { get { return _connectionMultiplexer.GetDatabase(); } }
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    public OperationLockManager(IConnectionMultiplexer multiplexer)
    {
        _connectionMultiplexer = multiplexer;
    }

    public async Task<bool> IsLockedAsync(string key, TimeSpan? duration = null)
    {
        var redisKey = new RedisKey(key);

        duration ??= new TimeSpan(0, 1, 0);

        var value = await _database.StringGetAsync(redisKey);

        if (value.HasValue) return true;

        await _database.StringSetAsync(redisKey, new RedisValue($"{key}-operation-is-locked"));

        await _database.KeyExpireAsync(redisKey, duration);

        return false;
    }

    public async Task<bool> ReleaseLockAsync(string key)
    {
        var redisKey = new RedisKey(key);
        var result = await _database.KeyDeleteAsync(redisKey);
        return result;
    }

}

