using Microsoft.Extensions.Configuration;

namespace iAnnoyNeightbour.WorkerService.Configuration
{
    public class ConfigurationManager : IConfigurationManager
    {
        private readonly IConfiguration configuration;

        public ConfigurationManager(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IConfigurationSection GetConfigurationSection(string key)
            => this.configuration.GetSection(key);

        public string PhilipsMonitorPartialName => this.GetConfigurationSection("PartialPhilipsMonitorDeviceName").Value;

        public string AudioFilesFolder => this.GetConfigurationSection("AudioFileFolder").Value;

        public string LongKnickingSoundEffect => this.GetConfigurationSection("LongKnockingSoundEffectFileName").Value;

        public string ShortKnockingSoundEffect => this.GetConfigurationSection("ShortKnockingSoundEffectFileName").Value;
    }
}
