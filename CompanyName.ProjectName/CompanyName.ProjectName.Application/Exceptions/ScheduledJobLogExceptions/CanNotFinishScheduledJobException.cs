using CompanyName.ProjectName.Application.Exceptions.BaseExceptions;
using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyName.ProjectName.Infrastructure.Exceptions.BaseExceptions;

namespace CompanyName.ProjectName.Application.Exceptions.ScheduledJobLogExceptions;

public class CanNotFinishScheduledJobException : OperationNotAllowedException
{
    public CanNotFinishScheduledJobException(string message) : base(message) { }

    public CanNotFinishScheduledJobException(string message, Exception innerException) : base(message, innerException) { }

    public CanNotFinishScheduledJobException(Guid id) : base($"Can not finish scheduledJob with id: {id}") { }

    public CanNotFinishScheduledJobException(Guid id, ScheduledJobLogStatus status) : base($"Can not finish scheduledJob with id: {id} because status is {status.Name}") { }
}


