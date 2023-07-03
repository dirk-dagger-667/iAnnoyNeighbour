using iAnnoyNeightbour.WorkerService.Models;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace iAnnoyNeightbour.WorkerService.Services.Contracts
{
    public interface IAudioSetupService
    {
        Task RunAudioFilesBasedOnRange(
            DateTime currentTime,
            string longFilePath,
            string shortFilePath,
            int deviceIndex,
            IEnumerable<DateTimeRange> timeRanges,
            CancellationToken cancellationToken);
    }
}
