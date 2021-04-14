using Microsoft.Extensions.Logging;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus.EventHandling
{
    public class DestinationDataSensorsAddedIntegrationEventHandler<TContent> :
        IIntegrationEventHandler<IntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<DestinationDataSensorsAddedIntegrationEventHandler<TContent>> _logger;

        public DestinationDataSensorsAddedIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<DestinationDataSensorsAddedIntegrationEventHandler<TContent>> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }

        public async Task Handle(IntegrationEvent @event)
        {
            _logger.LogInformation("Handling integration event from destination (subscribers): {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, @event.ApplicationName, @event);

            // Something do

            await Task.CompletedTask;
        }
    }
}