using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Data.Infrastructure.Abstractions;
using SmartHome.Data.Infrastructure.Abstractions.Models;
using SmartHome.EventBus.Abstractions;
using SmartHome.IntegrationBus.Content;
using SmartHome.IntegrationBus.IntegrationEvents;
using System.Threading.Tasks;

namespace SmartHome.Data.Api.Controllers
{
    /// <summary>
    /// The controller performs operations on Data.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/data")]
    public class DataController : ControllerBase
    {
        private readonly IDataRepository _dataRepository;
        private readonly ILogger<DataController> _logger;
        private readonly IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> _dataHandler;

        /// <summary>
        /// Creates the Data Controller.
        /// </summary>
        /// <param name="dataRepository"></param>
        /// <param name="logger"></param>
        /// <param name="dataHandler"></param>
        public DataController(
            IDataRepository dataRepository,
            ILogger<DataController> logger,
            IIntegrationEventHandler<DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>> dataHandler)
        {
            _dataRepository = dataRepository;
            _logger = logger;
            _dataHandler = dataHandler;
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="sensorDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("save")]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SaveData([FromBody, CustomizeValidator(RuleSet = "SaveSensorData")] SensorDataRequest sensorDataRequest)
        {
            var isSuccess = await _dataRepository.SaveSensorDataAsync(sensorDataRequest);

            if (isSuccess)
            {
                var dataEvent = new DataSensorsAddedIntegrationEvent<DataSensorsAddedContent>
                                (new DataSensorsAddedContent("Data added."), "SmartHome.Data.Api", null);

                await _dataHandler.Handle(dataEvent);
            }

            return Ok(sensorDataRequest);
        }
    }
}
