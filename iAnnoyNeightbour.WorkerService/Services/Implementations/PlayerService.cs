using iAnnoyNeightbour.WorkerService.Configuration;
using iAnnoyNeightbour.WorkerService.Services.Contracts;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace iAnnoyNeightbour.WorkerService.Services.Implementations
{
    public class PlayerService : IPlayerService
    {
        private readonly ILogger logger;
        private readonly IWaveOutService waveOutService;
        private readonly IConfigurationManager configurationManager;
        private readonly IClockService clockService;
        private readonly IAudioSetupService audioSetupService;

        public PlayerService(ILogger<PlayerService> logger,
            IWaveOutService waveOutService,
            IConfigurationManager configurationManager,
            IClockService clockService,
            IAudioSetupService audioSetupService)
        {
            this.logger = logger;
            this.waveOutService = waveOutService;
            this.configurationManager = configurationManager;
            this.clockService = clockService;
            this.audioSetupService = audioSetupService;
        }

        public async Task PlayAudioAsync(CancellationToken cancellationToken)
        {
            var startingTime = DateTime.Now;
            var timeRanges = this.clockService.SetupTimeRanges(startingTime);

            while (!cancellationToken.IsCancellationRequested)
            {
                var longKnickingSoundEffect = Path.Combine(Environment.CurrentDirectory,
                    this.configurationManager.AudioFilesFolder,
                    this.configurationManager.LongKnickingSoundEffect);
                var shortKnockingSoundEffect = Path.Combine(Environment.CurrentDirectory,
                    this.configurationManager.AudioFilesFolder,
                    this.configurationManager.ShortKnockingSoundEffect);
                var indexOfPhilipsDevice = this.waveOutService.GetIndexOfDeviceByPartialName(this.configurationManager.PhilipsMonitorPartialName);
                var currentTime = DateTime.Now;

                await this.audioSetupService.RunAudioFilesBasedOnRange(
                    currentTime,
                    longKnickingSoundEffect,
                    shortKnockingSoundEffect,
                    indexOfPhilipsDevice,
                    timeRanges,
                    cancellationToken);

                this.logger.LogInformation($"AudioRunner service is running.");

                await Task.Delay(TimeSpan.FromMinutes(5), cancellationToken);
            }
        }
    }
}
