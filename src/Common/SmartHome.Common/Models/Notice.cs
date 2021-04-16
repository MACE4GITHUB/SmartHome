using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHome.Common.Models
{
    public class Notice : INotice
    {
        private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);
        private readonly NoticeTimeStamp _noticeTimeStamp;

        public Notice(Action<NoticeOptions> options)
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            var notifyOptions = new NoticeOptions();
            options(notifyOptions);

            _noticeTimeStamp = new NoticeTimeStamp
            {
                StartDateTime = notifyOptions.StartDateTime,
                Frequency = notifyOptions.Frequency * 1000
            };
        }

        public async Task NotifyAsync(Func<Task> actionAsync)
        {
            if (actionAsync == null)
            {
                throw new ArgumentException("The action async is not defined.");
            }

            if (_noticeTimeStamp.IsWaiting)
            {
                return;
            }

            await _semaphoreSlim.WaitAsync();

            try
            {
                if (_noticeTimeStamp.IsWaiting)
                {
                    _semaphoreSlim.Release();
                    return;
                }
                await actionAsync();
                _noticeTimeStamp.ShiftStartDateTime();
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }

        /// <summary>
        /// Determines notice options.
        /// </summary>
        public sealed class NoticeOptions
        {
            public uint Frequency { get; set; }

            public DateTime StartDateTime { get; set; }
        }

        private class NoticeTimeStamp
        {
            private int _isStamped;

            public DateTime StartDateTime { get; set; }

            public uint Frequency { get; init; }

            public bool IsWaiting => StartDateTime > CurrentDateTime || !IsElapsed || _isStamped == 1;

            public void ShiftStartDateTime()
            {
                Interlocked.Exchange(ref _isStamped, 1);
                StartDateTime = CurrentDateTime;
            }

            private double ElapsedTime => (CurrentDateTime - StartDateTime).TotalMilliseconds;

            private bool IsElapsed
            {
                get
                {
                    if (ElapsedTime > Frequency)
                    {
                        Interlocked.Exchange(ref _isStamped, 0);
                        return true;
                    }

                    return false;
                }
            }

            private static DateTime CurrentDateTime => DateTime.UtcNow;
        }
    }
}
