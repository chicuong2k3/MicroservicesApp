using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Ordering.Domain.Abstractions;

namespace Ordering.Infrastructure.Data.Interceptors
{
    public class DispatchDomainEventsInterceptor(IMediator mediator)
        : SaveChangesInterceptor
    {
        public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
        {

            DispatchDomainEventsAsync(eventData.Context).GetAwaiter().GetResult();
            return base.SavingChanges(eventData, result);
        }

        public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
        {

            await DispatchDomainEventsAsync(eventData.Context);
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }

        private async Task DispatchDomainEventsAsync(DbContext? context)
        {
            if (context != null)
            {
                var aggregates = context.ChangeTracker
                    .Entries<IAggregate>()
                    .Where(x => x.Entity.DomainEvents.Any())
                    .Select(x => x.Entity);

                var domainEvents = aggregates.SelectMany(x => x.DomainEvents).ToList();

                foreach (var aggregate in aggregates)
                {
                    aggregate.ClearDomainEvents();
                }

                foreach (var domainEvent in domainEvents)
                {
                    await mediator.Publish(domainEvent);
                }
            }
        }
    }
}
