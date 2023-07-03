using System;

namespace iAnnoyNeightbour.WorkerService.Models
{
    public class DateTimeRange
    {
        public DateTimeRange(DateTime from, DateTime to)
        {
            this.From = from;
            this.To = to;
        }

        public DateTime From { get; }
        public DateTime To { get; }

        public bool IsInTimeRange(DateTime current)
            => current >= this.From && current <= this.To;
    }
}
