using CompanyName.ProjectName.Infrastructure.Dtos.ScheduledJobLog;

namespace CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateNewScheduledJobLog;

public record CreateNewScheduledJobLogCommand(string JobName) : IRequest<ScheduledJobLogDto> { }


