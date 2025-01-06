using CompanyName.ProjectName.Application.Exceptions.ScheduledJobLogExceptions;
using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Domain.SeedWork;
using CompanyName.ProjectName.Infrastructure.Dtos.ScheduledJobLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateFinishJobLog;

public class CreateFinishJobLogCommandHandler : IRequestHandler<CreateFinishJobLogCommand, ScheduledJobLogDto>
{
    public IUnitOfWork _uow;

    public CreateFinishJobLogCommandHandler(IUnitOfWork uow)
    {
        _uow = uow;
    }

    public async Task<ScheduledJobLogDto> Handle(CreateFinishJobLogCommand request, CancellationToken cancellationToken)
    {
        var scheduledJobLog = await _uow.ScheduledJobLogRepository.GetAsync(e => e.Id == request.Id) ?? throw new ScheduledJobLogNotFoundException(request.Id);

        if (request.Status.IsSucceeded)
            scheduledJobLog.SetAsSucceeded();
        else
            scheduledJobLog.SetAsFailed(request.ErrorMessage);
        await _uow.SaveChangesAsync(cancellationToken);
        return new ScheduledJobLogDto(scheduledJobLog);
    }
}


