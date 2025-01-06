using CompanyName.ProjectName.Domain.Aggregates.ScheduledJobLogAggregate;
using CompanyName.ProjectName.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Domain.DomainEvents.ScheduledJobLogDomainEvents;

public record ScheduledJobFailedEvent(ScheduledJobLog ScheduledJob) : IMessageDomainEvent { }


