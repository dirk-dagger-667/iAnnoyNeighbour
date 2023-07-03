using iAnnoyNeightbour.WorkerService.Models;
using iAnnoyNeightbour.WorkerService.Services.Contracts;
using System;
using System.Collections.Generic;

namespace iAnnoyNeightbour.WorkerService.Services.Implementations
{
    public class ClockService : IClockService
    {
        private readonly Random generator;

        public ClockService() => this.generator = new Random();

        public int GenerateRandMins(DateTime fromTime, DateTime toTime)
        {
            var fromTimeInMins = (int)fromTime.Ticks / (10000000 * 60);
            var toTimeInMins = (int)toTime.Ticks / (10000000 * 60);

            var randMinsToAdd = this.generator.Next(fromTimeInMins, toTimeInMins);

            return randMinsToAdd;
        }

        public IEnumerable<DateTimeRange> SetupTimeRanges(DateTime startingTime)
        {
            var midnight = this.IsStartedBefore7Am(startingTime)
                ? new DateTime(startingTime.Year, startingTime.Month, startingTime.Day, 0, 0, 0)
                : new DateTime(startingTime.Year, startingTime.Month, startingTime.Day + 1, 0, 0, 0);

            var oneAm = midnight.AddHours(1);
            var threeAm = midnight.AddHours(3);
            var fourAm = midnight.AddHours(4);
            var sixAm = midnight.AddHours(6);
            var sevenAm = midnight.AddHours(7);

            var ranges = new List<DateTimeRange>()
                {
                    new DateTimeRange(midnight, oneAm),
                    new DateTimeRange(threeAm, fourAm),
                    new DateTimeRange(sixAm, sevenAm)
                };

            return ranges;
        }

        private bool IsStartedBefore7Am(DateTime currentTime)
            => currentTime < new DateTime(currentTime.Year, currentTime.Month, currentTime.Day, 7, 0, 0);
    }
}
