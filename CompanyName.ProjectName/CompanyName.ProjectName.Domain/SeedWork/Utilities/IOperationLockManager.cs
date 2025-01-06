using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Domain.SeedWork.Utilities;

public interface IOperationLockManager
{
    Task<bool> IsLockedAsync(string key, TimeSpan? duration = null);
    Task<bool> ReleaseLockAsync(string key);
}


