using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.EventBus;
using SmartHome.EventBus.Abstractions;
using SmartHome.MemoryEventBus;

namespace SmartHome.IntegrationBus.Extentions
{
    public static class MemoryEventBusExtentions
    {
        public static IServiceCollection AddMemoryEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIntegrationEventService, IntegrationEventService>();
            services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
            services.AddSingleton<IEventBus, InMemoryEventBus>(sp => new InMemoryEventBus(sp));

            return services;
        }
    }
}
