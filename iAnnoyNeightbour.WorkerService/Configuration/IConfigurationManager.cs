using Microsoft.Extensions.Configuration;

namespace iAnnoyNeightbour.WorkerService.Configuration
{
    public interface IConfigurationManager
    {
        string PhilipsMonitorPartialName { get; }

        string AudioFilesFolder { get; }

        string LongKnickingSoundEffect { get; }

        string ShortKnockingSoundEffect { get; }

        IConfigurationSection GetConfigurationSection(string key);
    }
}
