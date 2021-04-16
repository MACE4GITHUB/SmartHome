using System;
using System.Threading.Tasks;

namespace SmartHome.Common.Models
{
    /// <summary>
    /// Represents a notice interface.
    /// </summary>
    public interface INotice
    {
        /// <summary>
        /// Notifies something. 
        /// </summary>
        /// <param name="actionAsync"></param>
        /// <returns></returns>
        Task NotifyAsync(Func<Task> actionAsync);
    }
}