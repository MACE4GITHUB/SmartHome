using SmartHome.EventBus.Abstractions;
using SmartHome.IntegrationBus.Content;
using SmartHome.IntegrationBus.IntegrationEvents;
using System.Threading.Tasks;

namespace SmartHome.IntegrationBus
{
    public class DataSensorsHandlerWrapper : IDataSensorsHandlerWrapper
    {
        private readonly IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> _dataHandler;

        public DataSensorsHandlerWrapper(IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> dataHandler)
        {
            _dataHandler = dataHandler;
        }

        public async Task Handle()
        {
            var dataEvent = new DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>
                (new DataSensorsAddedContent("Data added."), "SmartHome.Data.Api", null);

            await _dataHandler.Handle(dataEvent);
        }
    }
}
