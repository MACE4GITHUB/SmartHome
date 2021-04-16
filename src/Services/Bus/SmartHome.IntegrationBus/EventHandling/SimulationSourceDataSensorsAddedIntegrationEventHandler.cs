using Microsoft.Extensions.Logging;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus.EventHandling
{
    public class SimulationSourceDataSensorsAddedIntegrationEventHandler<TContent> :
        IIntegrationEventHandler<IntegrationEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;
        private readonly ILogger<SimulationSourceDataSensorsAddedIntegrationEventHandler<TContent>> _logger;

        public SimulationSourceDataSensorsAddedIntegrationEventHandler(IIntegrationEventService integrationEventService,
            ILogger<SimulationSourceDataSensorsAddedIntegrationEventHandler<TContent>> logger)
        {
            _integrationEventService = integrationEventService;
            _logger = logger;
        }

        public async Task Handle(IntegrationEvent @event)
        {
            #region Simulation error
            if (char.IsDigit(@event.EventId.ToString()[0]))
            {
                throw new InvalidOperationException("Failed to send event.");
            }
            #endregion

            _logger.LogDebug("Handling integration event from source: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.EventId, @event.ApplicationName, @event);

            await _integrationEventService.PublishThroughEventBusAsync(@event);
        }
    }
}
