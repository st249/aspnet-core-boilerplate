using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Infrastructure.Dtos.ScheduledJobLog;

public class ScheduledJobLogDto
{
    public Guid Id { get; set; }
    public string JobName { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; }
    public string? ErrorMessage { get; set; }
    public ScheduledJobLogStatus Status { get; set; }
    public bool IsCompleted => Status != ScheduledJobLogStatus.InProgress;
    public ScheduledJobLogDto(Domain.Aggregates.ScheduledJobLogAggregate.ScheduledJobLog scheduledJob)
    {
        Id= scheduledJob.Id;    
        JobName = scheduledJob.JobName;
        StartTime = scheduledJob.StartTime;
        EndTime = scheduledJob.EndTime;
        ErrorMessage = scheduledJob.ErrorMessage;
        Status = scheduledJob.Status;
    }
}


