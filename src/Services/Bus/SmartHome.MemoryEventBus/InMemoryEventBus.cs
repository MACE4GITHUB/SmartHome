using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SmartHome.EventBus;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using SmartHome.EventBus.Extensions;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SmartHome.MemoryEventBus
{
    public class InMemoryEventBus : IEventBus
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<InMemoryEventBus> _logger;
        private readonly IEventBusSubscriptionsManager _subsManager;

        public InMemoryEventBus(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _logger = _serviceProvider.GetRequiredService<ILogger<InMemoryEventBus>>();
            _subsManager = new InMemoryEventBusSubscriptionsManager();
        }

        public void Publish(IntegrationEvent evt)
        {
            // Can add the event manager.

            Task.Run(async () => await ProcessEvent(evt));
        }

        public void Subscribe<T, TH, TP>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
            where TP : BusParams<T>
        {
            var eventName = GetEventName<T>();

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).GetNormalizeTypeName());

            _subsManager.AddSubscription<T, TH, TP>();
        }

        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = GetEventName<T>();

            _logger.LogInformation("Unsubscribing from event {EventName}", eventName);

            _subsManager.RemoveSubscription<T, TH>();
        }

        private string GetEventName<T>() => _subsManager.GetEventKey<T>();

        private async Task ProcessEvent(IntegrationEvent iEvent)
        {
            var eventName = iEvent.GetNormalizeTypeName();

            _logger.LogDebug("Processing event: {EventName}", eventName);

            if (_subsManager.HasSubscriptionsForEvent(eventName))
            {
                var subscriptions = _subsManager.GetHandlersForEvent(eventName);
                foreach (var subscription in subscriptions)
                {
                    var handler = subscription.HandlerType;
                    if (handler == null)
                    {
                        continue;
                    }

                    var handlerConstructor = handler.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
                        .OrderByDescending(c => c.GetParameters().Length).First();

                    var parameters = handlerConstructor.GetParameters();
                    var arguments = parameters.Select(p => _serviceProvider.GetService(p.ParameterType)).ToArray();

                    dynamic objectHandler = handlerConstructor.Invoke(arguments);

                    var eventType = _subsManager.GetEventTypeByName(eventName);
                    var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);

                    await Task.Yield();
                    await (Task)concreteType.GetMethod("Handle").Invoke(objectHandler, new object[] { iEvent });
                }
            }
            else
            {
                _logger.LogWarning("No subscription for event: {EventName}.", eventName);
            }
        }
    }
}
