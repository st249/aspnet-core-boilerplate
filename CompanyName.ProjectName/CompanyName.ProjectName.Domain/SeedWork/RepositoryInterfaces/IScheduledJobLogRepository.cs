using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Domain.SeedWork.RepositoryInterfaces;

public interface IScheduledJobLogRepository : IRepository<ScheduledJobLog, Guid>
{
}


