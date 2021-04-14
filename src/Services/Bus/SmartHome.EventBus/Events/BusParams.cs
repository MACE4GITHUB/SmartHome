namespace SmartHome.EventBus.Events
{
    /// <summary>
    /// Represents options bus.
    /// </summary>
    public record BusParams(object Options);

    /// <summary>
    /// Represents options bus typed IntegrationEvent.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public record BusParams<T>(object Options) : BusParams(Options) where T : IntegrationEvent;
}