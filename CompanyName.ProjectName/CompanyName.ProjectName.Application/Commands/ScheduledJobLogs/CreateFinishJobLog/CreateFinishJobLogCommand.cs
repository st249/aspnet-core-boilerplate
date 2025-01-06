using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Infrastructure.Dtos.ScheduledJobLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Application.Commands.ScheduledJobLogs.CreateFinishJobLog;

public record CreateFinishJobLogCommand(Guid Id, ScheduledJobLogStatus Status, string? ErrorMessage = null) : IRequest<ScheduledJobLogDto>
{
}

public class CreateFinishJobLogCommandValidator : AbstractValidator<CreateFinishJobLogCommand>
{
    public CreateFinishJobLogCommandValidator()
    {
        RuleFor(e => e.Status).NotEqual(ScheduledJobLogStatus.InProgress);
        RuleFor(e => e.ErrorMessage)
            .MinimumLength(1)
            .When(e => e.Status == ScheduledJobLogStatus.Failed);
    }
}


