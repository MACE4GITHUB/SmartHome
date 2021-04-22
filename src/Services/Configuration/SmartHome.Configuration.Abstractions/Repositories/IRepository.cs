using SmartHome.Configuration.Abstractions.Conditions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SmartHome.Configuration.Abstractions.Repositories
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> ReadAllAsync(ExpressionConditions<T, Enum> condition);
    }
}
