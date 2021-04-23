using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SmartHome.Configuration.Abstractions.Conditions;
using SmartHome.Configuration.Abstractions.Models;
using SmartHome.Configuration.Abstractions.Repositories;
using SmartHome.Configuration.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static SmartHome.Configuration.Infrastructure.Repositories.SensorsRepository.ConditionsOption;

namespace SmartHome.Configuration.Api.Controllers
{
    /// <summary>
    /// The controller performs operations on Sensors.
    /// </summary>
    [ApiVersion("1.0")]
    [ApiController]
    [Route("api/v{version:apiVersion}/sensors")]
    public class SensorController : ControllerBase
    {
        private readonly ILogger<SensorController> _logger;
        private readonly IRepository<SensorDb> _repository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates the Sensor Controller.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="repository"></param>
        /// <param name="mapper"></param>
        public SensorController(ILogger<SensorController> logger, IRepository<SensorDb> repository, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("getAll")]
        [ProducesResponseType(typeof(List<Sensor>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllSensorConfigurations()
        {
            var ex = new ExpressionConditions<SensorDb, Enum> { [OrderBy] = _ => _.SensorId };
            var result = await _repository.ReadAllAsync(ex);

            if (!result.Any())
            {
                return NotFound("Any configuration is not found.");
            }

            var sensor = _mapper.Map<List<Sensor>>(result);

            return Ok(sensor);
        }

        [HttpGet]
        [Route("get")]
        [ProducesResponseType(typeof(Sensor), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status403Forbidden)]
        [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(string), StatusCodes.Status405MethodNotAllowed)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetSensorConfigurationById(Guid id)
        {
            var sid = id.ToString();
            var ex = new ExpressionConditions<SensorDb, Enum> { [Where] = _ => _.SensorId == sid, [OrderBy] = _ => _.SensorId };
            var result = (await _repository.ReadAllAsync(ex)).ToList();

            if (!result.Any())
            {
                return NotFound("Any configuration is not found.");
            }

            var sensor = _mapper.Map<Sensor>(result.First());

            return Ok(sensor);
        }
    }
}