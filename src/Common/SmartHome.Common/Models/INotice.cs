using System;
using System.Threading.Tasks;

namespace SmartHome.Common.Models
{
    public interface INotice
    {
        Task NotifyAsync(Func<Task> actionAsync);

        void Notify(Action action);
    }
}