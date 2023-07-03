using iAnnoyNeightbour.WorkerService.Models;
using System;
using System.Collections.Generic;

namespace iAnnoyNeightbour.WorkerService.Extensions
{
    public static class EnumerableExtensions
    {
        public static DateTimeRange GetTimeRange(this IEnumerable<DateTimeRange> dateTimeRanges, DateTime dateTime)
        {
            foreach (var range in dateTimeRanges)
            {
                if (range.IsInTimeRange(dateTime))
                {
                    return range;
                }
            }

            return null;
        }
    }
}
