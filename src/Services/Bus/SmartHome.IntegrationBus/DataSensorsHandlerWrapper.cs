using Microsoft.Extensions.Logging;
using SmartHome.Common.Models;
using SmartHome.EventBus.Abstractions;
using SmartHome.IntegrationBus.Content;
using SmartHome.IntegrationBus.IntegrationEvents;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus
{
    public class DataSensorsHandlerWrapper : IDataSensorsHandlerWrapper
    {
        private readonly IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> _dataHandler;
        private readonly INotice _notice;
        private readonly ILogger<DataSensorsHandlerWrapper> _logger;

        public DataSensorsHandlerWrapper(IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> dataHandler,
            INotice notice,
            ILogger<DataSensorsHandlerWrapper> logger)
        {
            _dataHandler = dataHandler;
            _notice = notice;
            _logger = logger;
        }

        public async Task Handle()
        {
            await _notice.NotifyAsync(async () =>
            {
                var dataEvent = new DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>
                                (new DataSensorsAddedContent("Data added."), "SmartHome.Data.Api", null);

                await _dataHandler.Handle(dataEvent);
            });
        }
    }
}
