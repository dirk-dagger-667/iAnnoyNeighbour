using iAnnoyNeightbour.WorkerService.Configuration;
using iAnnoyNeightbour.WorkerService.Services.Contracts;
using iAnnoyNeightbour.WorkerService.Services.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace iAnnoyNeightbour.WorkerService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<IClockService, ClockService>();
                    services.AddTransient<IConfigurationManager, ConfigurationManager>();
                    services.AddTransient<IPlayerService, PlayerService>();
                    services.AddScoped<IWaveOutService, WaveOutService>();
                    services.AddHostedService<Worker>();
                });
    }
}
