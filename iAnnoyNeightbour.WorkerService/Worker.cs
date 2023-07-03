using iAnnoyNeightbour.WorkerService.Services.Contracts;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace iAnnoyNeightbour.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> logger;
        private IServiceProvider serviceProvider;

        //private Timer? timer;

        public Worker(ILogger<Worker> logger,
            IServiceProvider serviceProvider)
        {
            this.logger = logger;
            this.serviceProvider = serviceProvider;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            this.logger.LogInformation("iAnnoyNeighbour worker service is starting.");

            //this.timer = new Timer()

            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                logger.LogInformation("iAnnoyNeighbour worker service is working: {time}", DateTimeOffset.Now);

                using (IServiceScope scope = this.serviceProvider.CreateScope())
                {
                    IPlayerService audioPlayerService = scope.ServiceProvider.GetRequiredService<IPlayerService>();

                    await audioPlayerService.PlayAudioAsync(stoppingToken);
                }
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation(
            "Consume Scoped Service Hosted Service is stopping.");

            await base.StopAsync(cancellationToken);
        }
    }
}
