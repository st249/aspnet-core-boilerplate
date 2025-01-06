

namespace CompanyName.ProjectName.Infrastructure.Extentions;

public static class MediatorExtentions
{
    public static async Task DispatchDomainEventsAsync(this IMediator mediator, ProjectNameContext ctx)
    {
        var domainEntities = ctx.ChangeTracker
            .Entries<IAggregateRoot>()
            .Where(x => x.Entity.DomainEvents != null && x.Entity.DomainEvents.Any(e => e is INotificationDomainEvent))
            .ToList();

        var domainEvents = domainEntities
            .SelectMany(x => x.Entity.DomainEvents)
            .ToList();

        domainEntities.ToList()
            .ForEach(entity => entity.Entity.ClearDomainEvents());

        foreach (var domainEvent in domainEvents)
            await mediator.Publish(domainEvent);
    }
}


