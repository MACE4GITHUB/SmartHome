using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Data.Infrastructure.Abstractions;
using SmartHome.Data.Infrastructure.Abstractions.Models;
using SmartHome.IntegrationBus;
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
        private readonly IDataSensorsHandlerWrapper _handler;

        /// <summary>
        /// Creates the Data Controller.
        /// </summary>
        /// <param name="dataRepository"></param>
        /// <param name="logger"></param>
        /// <param name="handler"></param>
        public DataController(
            IDataRepository dataRepository,
            ILogger<DataController> logger,
            IDataSensorsHandlerWrapper handler
            )
        {
            _dataRepository = dataRepository;
            _logger = logger;
            _handler = handler;
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
                await _handler.Handle();
            }

            return Ok(sensorDataRequest);
        }
    }
}
