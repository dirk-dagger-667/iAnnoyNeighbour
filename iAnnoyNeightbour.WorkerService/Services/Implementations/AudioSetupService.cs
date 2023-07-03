using iAnnoyNeightbour.WorkerService.Extensions;
using iAnnoyNeightbour.WorkerService.Models;
using iAnnoyNeightbour.WorkerService.Services.Contracts;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;

namespace iAnnoyNeightbour.WorkerService.Services.Implementations
{
    public class AudioSetupService : IAudioSetupService
    {
        private readonly IClockService clockService;

        public AudioSetupService(IClockService clockService)
        {
            this.clockService = clockService;
        }

        public async Task RunAudioFilesBasedOnRange(
            DateTime currentTime,
            string longFilePath,
            string shortFilePath,
            int deviceIndex,
            IEnumerable<DateTimeRange> timeRanges,
            CancellationToken cancellationToken)
        {
            var lastRange = timeRanges.LastOrDefault();

            if (lastRange.IsInTimeRange(currentTime))
            {
                await this.RunAudioFileWithRandomDelay(currentTime, longFilePath, deviceIndex, timeRanges, cancellationToken);
            }
            else
            {
                await this.RunAudioFileWithRandomDelay(currentTime, shortFilePath, deviceIndex, timeRanges, cancellationToken);
            }
        }

        private async Task RunAudioFileWithRandomDelay(
            DateTime currentTime,
            string filePath,
            int deviceIndex,
            IEnumerable<DateTimeRange> timeRanges,
            CancellationToken cancellationToken)
        {
            var rangeTimeIsPartOf = timeRanges.GetTimeRange(currentTime);

            if (rangeTimeIsPartOf == null)
            {
                return;
            }

            var minutesToWait = this.clockService.GenerateRandMins(rangeTimeIsPartOf.From, rangeTimeIsPartOf.To);
            await Task.Delay(TimeSpan.FromMinutes(minutesToWait), cancellationToken);
            this.RunAudioFile(filePath, deviceIndex);
        }

        private void RunAudioFile(string audioFilePath, int indexOfDevice)
        {
            using (var audioFile = new AudioFileReader(audioFilePath))
            using (var outputDevice = new WaveOutEvent() { DeviceNumber = indexOfDevice })
            {
                outputDevice.Init(audioFile);
                outputDevice.Play();
                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(1000);
                }
            }
        }
    }
}
