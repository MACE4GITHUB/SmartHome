using Microsoft.Extensions.Logging;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using System;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus
{
    public class IntegrationEventService : IIntegrationEventService
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<IntegrationEventService> _logger;

        public IntegrationEventService(ILogger<IntegrationEventService> logger, IEventBus eventBus)
        {
            _logger = logger;
            _eventBus = eventBus;
        }

        public async Task PublishThroughEventBusAsync(IntegrationEvent evt)
        {
            try
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId_published} from {AppName} - ({@IntegrationEvent})", evt.EventId, evt.ApplicationName, evt);

                _eventBus.Publish(evt);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ERROR Publishing integration event: {IntegrationEventId} from {AppName} - ({@IntegrationEvent})", evt.EventId, evt.ApplicationName, evt);
            }

            await Task.CompletedTask;
        }
    }
}
