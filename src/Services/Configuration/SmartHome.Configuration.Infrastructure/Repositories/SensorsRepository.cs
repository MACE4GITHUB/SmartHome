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
            IQueryable<SensorDb> query = _context.Sensors.Include(_ => _.SensorType);
            AddWhereIfExists(ref query, condition);
            AddOrderByIfExists(ref query, condition);
            return await query
                .ToListAsync().ConfigureAwait(false);
        }

        /// <summary>
        /// Represents Conditions Option.
        /// </summary>
        public enum ConditionsOption
        {
            OrderBy,
            Where
        }

        private void AddWhereIfExists(ref IQueryable<SensorDb> query, ExpressionConditions<SensorDb, Enum> condition)
        {
            if (condition.KeyExists(ConditionsOption.Where))
            {
                query = query.Where(condition.GetBool(ConditionsOption.Where));
            }
        }

        private void AddOrderByIfExists(ref IQueryable<SensorDb> query, ExpressionConditions<SensorDb, Enum> condition)
        {
            if (condition.KeyExists(ConditionsOption.Where))
            {
                query = query.OrderBy(condition[ConditionsOption.OrderBy]);
            }
        }
    }
}
