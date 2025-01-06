using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Domain.SeedWork.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Persistent.Repositories;

public class ScheduledJobLogRepository : BaseRepository<ScheduledJobLog, Guid>, IScheduledJobLogRepository
{
    public ScheduledJobLogRepository(ProjectNameContext dbContext) : base(dbContext)
    {
    }
}


