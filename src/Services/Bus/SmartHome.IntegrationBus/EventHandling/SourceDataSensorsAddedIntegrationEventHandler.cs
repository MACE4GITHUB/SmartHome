using Microsoft.Extensions.Logging;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus.EventHandling
{
    public class SourceDataSensorsAddedIntegrationEventHandler<TContent> :
        IIntegrationEventHandler<IntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<SourceDataSensorsAddedIntegrationEventHandler<TContent>> _logger;

        public SourceDataSensorsAddedIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<SourceDataSensorsAddedIntegrationEventHandler<TContent>> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }

        public async Task Handle(IntegrationEvent @event)
        {
            _logger.LogDebug("Handling integration event from source: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, @event.ApplicationName, @event);

            await _integrationEventService.PublishThroughEventBusAsync(@event);
        }
    }
}
