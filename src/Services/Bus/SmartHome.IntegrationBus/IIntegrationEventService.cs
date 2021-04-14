using SmartHome.EventBus.Events;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus
{
    public interface IIntegrationEventService
    {
        Task PublishThroughEventBusAsync(IntegrationEvent evt);
    }
}
