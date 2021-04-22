using Microsoft.EntityFrameworkCore;
using SmartHome.Configuration.Abstractions.Conditions;
using SmartHome.Configuration.Abstractions.Repositories;
using SmartHome.Configuration.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartHome.Configuration.Infrastructure.Repositories
{
    /// <summary>
    /// Determines the Sensors Repository.
    /// </summary>
    public class SensorsRepository : BaseRepository, IRepository<SensorDb>
    {
        /// <summary>
        /// Creates the Sensors Repository.
        /// </summary>
        /// <param name="context"></param>
        public SensorsRepository(ConfiguringContext context) : base(context) { }

        /// <summary>
        /// Gets all SensorDb ordered by condition["OrderBy"].
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<IEnumerable<SensorDb>> ReadAllAsync(ExpressionConditions<SensorDb, Enum> condition)
        {
            return await _context.Sensors
                .Include(_ => _.SensorType)
                .OrderBy(condition[ConditionsOption.OrderBy])
                .ToListAsync().ConfigureAwait(false);
        }

        public enum ConditionsOption
        {
            OrderBy
        }
    }
}
