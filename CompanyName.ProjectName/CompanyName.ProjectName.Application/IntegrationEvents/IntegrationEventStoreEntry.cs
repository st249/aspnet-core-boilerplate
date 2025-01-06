using CompanyName.ProjectName.Contracts.ServiceBus.Events;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace CompanyName.ProjectName.Application.IntegrationEvents;

public class IntegrationEventStoreEntry
{
    private IntegrationEventStoreEntry() { }
    public IntegrationEventStoreEntry(IntegrationEvent @event, Guid transactionId)
    {
        EventId = @event.Id;
        CreationTime = @event.CreationDate;
        EventTypeName = @event.GetType().FullName;
        Content = JsonSerializer.Serialize(@event, @event.GetType(), new JsonSerializerOptions
        {
            WriteIndented = true
        });
        //State = EventStateEnum.NotPublished;
        TimesSent = 0;
        TransactionId = transactionId;
        IntegrationEvent = @event;
    }
    public Guid EventId { get; private set; }
    public string EventTypeName { get; private set; }
    [NotMapped]
    public string EventTypeShortName => EventTypeName.Split('.')?.Last();
    [NotMapped]
    public IntegrationEvent IntegrationEvent { get; private set; }
    //public EventStateEnum State { get; set; }
    public int TimesSent { get; set; }
    public DateTime CreationTime { get; private set; }
    public string Content { get; private set; }
    public Guid TransactionId { get; private set; }

    public IntegrationEventStoreEntry DeserializeJsonContent(Type type)
    {
        IntegrationEvent = JsonSerializer.Deserialize(Content, type, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true }) as IntegrationEvent;
        return this;
    }
}


