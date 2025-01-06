using CompanyName.ProjectName.Contracts.ServiceBus.Events;

namespace CompanyName.ProjectName.Application.IntegrationEvents;
public interface IProjectNameIntegrationEventService
{
    Task PublishEventsThroughEventBusAsync(Guid transactionId);
    void AddAndSaveEventAsync(IntegrationEvent evt);
}


