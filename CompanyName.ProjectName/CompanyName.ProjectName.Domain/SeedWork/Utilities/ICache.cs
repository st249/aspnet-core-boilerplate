using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Domain.SeedWork.Utilities;

public interface ICache
{
    Task<T> GetAsync<T>(string key, bool hasAbsoluteKey = false);

    Task<bool> SetAsync<T>(string key, T value);

    Task<bool> SetAsync<T>(string key, T value, TimeSpan ttl);

    Task<bool> RemoveAsync(string key);
}


