using CompanyName.ProjectName.Contracts.ServiceBus.Events;
using Microsoft.EntityFrameworkCore.Storage;

namespace CompanyName.ProjectName.Application.IntegrationEvents;

public class IntegrationEventStore
{
    private List<IntegrationEventStoreEntry> _events;

    public IReadOnlyCollection<IntegrationEventStoreEntry> IntegrationEvents => _events ?? new List<IntegrationEventStoreEntry>();

    public void AddEvent(IntegrationEvent @event, IDbContextTransaction transaction)
    {
        if (_events == null)
            _events = new List<IntegrationEventStoreEntry>();

        var eventStoreEntry = new IntegrationEventStoreEntry(@event, transaction.TransactionId);
        _events.Add(eventStoreEntry);
    }

    public void ClearTransactionEvents(Guid transactionId)
    {
        if (_events != null)
            _events = _events.Where(e => e.TransactionId == transactionId).ToList();
    }


}


