using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartHome.EventBus.Abstractions;
using SmartHome.EventBus.Events;
using SmartHome.IntegrationBus.Content;
using SmartHome.IntegrationBus.EventHandling;
using SmartHome.IntegrationBus.IntegrationEvents;

namespace SmartHome.Data.Api.Extentions
{
    public static class EventBusExtentions
    {
        public static IServiceCollection AddEventBusHandlers(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>>, SourceDataSensorsAddedIntegrationEventHandler<DataSensorsAddedContent>>();

            return services;
        }

        public static IApplicationBuilder UseEventBus(this IApplicationBuilder app)
        {
            var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
            eventBus.Subscribe<
                DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>,
                DestinationDataSensorsAddedIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>>,
                BusParams<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>>>();

            return app;
        }
    }
}
