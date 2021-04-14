using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using System;
using System.Collections.Generic;
using static SmartHome.EventBus.InMemoryEventBusSubscriptionsManager;

namespace SmartHome.EventBus
{
    public interface IEventBusSubscriptionsManager
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnEventRemoved;

        void AddSubscription<T, TH, TP>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
            where TP : BusParams<T>;

        void RemoveSubscription<T, TH>()
            where TH : IIntegrationEventHandler<T>
            where T : IntegrationEvent;

        bool HasSubscriptionsForEvent<T>() where T : IntegrationEvent;

        bool HasSubscriptionsForEvent(string eventName);

        Type GetEventTypeByName(string eventName);

        void Clear();

        IEnumerable<SubscriptionInfo> GetHandlersForEvent<T>() where T : IntegrationEvent;

        IEnumerable<SubscriptionInfo> GetHandlersForEvent(string eventName);

        string GetEventKey<T>();

        Type GetTypeParams(string eventName);
    }
}