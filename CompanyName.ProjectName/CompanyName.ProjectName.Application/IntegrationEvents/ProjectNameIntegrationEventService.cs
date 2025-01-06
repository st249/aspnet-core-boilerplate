using CompanyName.ProjectName.Contracts.ServiceBus.Events;
using MassTransit;

namespace CompanyName.ProjectName.Application.IntegrationEvents;

public class ProjectNameIntegrationEventService : IProjectNameIntegrationEventService
{
    private readonly IntegrationEventStore _integrationEventStore;
    private readonly ProjectNameContext _dbContext;
    private readonly IPublishEndpoint _publisher;
    private readonly ILogger _logger;
    public ProjectNameIntegrationEventService(
        IntegrationEventStore integrationEventStore, ProjectNameContext dbContext, IPublishEndpoint publisher, ILogger logger)
    {
        _integrationEventStore = integrationEventStore;
        _dbContext = dbContext;
        _publisher = publisher;
        _logger = logger;
    }

    public void AddAndSaveEventAsync(IntegrationEvent evt)
    {
        _logger.Information("Adding event to store {@eventName}", evt.GetGenericTypeName());
        _integrationEventStore.AddEvent(evt, _dbContext.GetCurrentTransaction());
    }

    public async Task PublishEventsThroughEventBusAsync(Guid transactionId)
    {
        var pendingEvents = _integrationEventStore.IntegrationEvents.Where(e => e.TransactionId == transactionId).ToList();

        foreach (var pendingEvent in pendingEvents)
        {
            _logger.Information("----- Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", pendingEvent.EventId, this.GetType().Name, pendingEvent.IntegrationEvent);

            try
            {
                await _publisher.Publish((object)pendingEvent.IntegrationEvent);

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "ERROR publishing integration event: {IntegrationEventId} from {AppName}", pendingEvent.EventId, this.GetType().Name);

                //Todo: WE SHOULD STORE THIS FAILED EVENT
                //await _eventLogService.MarkEventAsFailedAsync(logEvt.EventId);

            }
        }
        _integrationEventStore.ClearTransactionEvents(transactionId);

    }
}


