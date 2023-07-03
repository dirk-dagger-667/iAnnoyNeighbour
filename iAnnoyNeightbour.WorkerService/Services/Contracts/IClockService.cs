using iAnnoyNeightbour.WorkerService.Models;
using System;
using System.Collections.Generic;

namespace iAnnoyNeightbour.WorkerService.Services.Contracts
{
    public interface IClockService
    {
        int GenerateRandMins(DateTime fromTime, DateTime toTime);

        IEnumerable<DateTimeRange> SetupTimeRanges(DateTime startingTime);
    }
}
