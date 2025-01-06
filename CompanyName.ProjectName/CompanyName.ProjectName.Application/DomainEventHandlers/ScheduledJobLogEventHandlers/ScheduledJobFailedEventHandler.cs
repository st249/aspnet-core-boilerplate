using CompanyName.ProjectName.Domain.DomainEvents.ScheduledJobLogDomainEvents;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyName.ProjectName.Application.DomainEventHandlers.ScheduledJobLogEventHandlers
{
    public class ScheduledJobFailedEventHandler : IConsumer<ScheduledJobFailedEvent>
    {
        public async Task Consume(ConsumeContext<ScheduledJobFailedEvent> context)
        {
            //SEND EMAIL TO ADMIN 
            throw new NotImplementedException();
        }
    }
}


