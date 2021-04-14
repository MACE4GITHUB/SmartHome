namespace SmartHome.IntegrationBus.IntegrationEvents
{
    using EventBus.Events;

    public record DataSensorsAddedIntegrationEvent<T>
        (T Content, string ApplicationName, BusParams BusParams)
        : IntegrationEvent(ApplicationName, BusParams);
}
