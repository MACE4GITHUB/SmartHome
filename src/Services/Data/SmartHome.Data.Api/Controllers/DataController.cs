using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace SmartHome.Data.Api.Controllers
{
    using Models;

    /// <summary>
    /// The controller performs operations on Data.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/data")]
    public class DataController : ControllerBase
    {
        private readonly ILogger<DataController> _logger;

        /// <summary>
        /// Creates the Data Controller.
        /// </summary>
        /// <param name="logger"></param>
        public DataController(ILogger<DataController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Updates the data.
        /// </summary>
        /// <param name="sensorDataRequest"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("update")]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(SensorDataRequest), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateData([FromBody, CustomizeValidator(RuleSet = "UpdateSensorData")] SensorDataRequest sensorDataRequest)
        {
            await Task.CompletedTask;

            return Ok(sensorDataRequest);
        }
    }
}
