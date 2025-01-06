using CompanyName.ProjectName.Domain.Aggregates;
using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Domain.SeedWork;
using CompanyName.ProjectName.Infrastructure.Dtos.ScheduledJobLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateNewScheduledJobLog;

public class CreateNewScheduledJobLogCommandHandler : IRequestHandler<CreateNewScheduledJobLogCommand, ScheduledJobLogDto>
{
    private readonly IUnitOfWork _uow;

    public CreateNewScheduledJobLogCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ScheduledJobLogDto> Handle(CreateNewScheduledJobLogCommand request, CancellationToken cancellationToken)
    {
        var scheduledJobLog = new ScheduledJobLog(request.JobName);
        var insertionResult = await _uow.ScheduledJobLogRepository.InsertAsync(scheduledJobLog);
        await _uow.SaveChangesAsync(cancellationToken);

        return new ScheduledJobLogDto(insertionResult);

    }
}

