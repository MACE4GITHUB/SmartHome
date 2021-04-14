using SmartHome.EventBus.Events;

namespace SmartHome.EventBus.Abstractions
{
    public interface IEventBus
    {
        void Publish(IntegrationEvent evt);

        void Subscribe<T, TH, TP>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
            where TP : BusParams<T>;


        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
