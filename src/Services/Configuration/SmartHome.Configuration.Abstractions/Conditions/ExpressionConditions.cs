using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SmartHome.Configuration.Abstractions.Conditions
{
    /// <summary>
    /// Represents Expression Conditions. 
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    /// <exception cref="InvalidOperationException">Thrown when index is not exist.</exception>
    public class ExpressionConditions<TModel, TKey>
        where TModel : class
        where TKey : Enum
    {
        private readonly Dictionary<TKey, Expression<Func<TModel, object>>> _expressions = new();

        /// <summary>
        /// Gets or sets an expression by index. When index set it expression value override.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException">Thrown when index is not exist.</exception>
        public Expression<Func<TModel, object>> this[TKey index]
        {
            get
            {
                if (_expressions.TryGetValue(index, out var value))
                {
                    return value;
                }

                throw new InvalidOperationException($"The value by index {index} is not exist.");
            }
            set => _expressions[index] = value;
        }
    }
}
