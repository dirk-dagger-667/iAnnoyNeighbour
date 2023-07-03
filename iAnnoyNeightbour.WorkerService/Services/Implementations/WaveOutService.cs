using iAnnoyNeightbour.WorkerService.Services.Contracts;
using NAudio.Wave;
using System.Collections.Generic;
using System.Linq;

namespace iAnnoyNeightbour.WorkerService.Services.Implementations
{
    public class WaveOutService : IWaveOutService
    {
        public int GetIndexOfDeviceByPartialName(string partial)
        {
            var philipsMonitor = this.GetDeviceByPartialName(partial);
            var outputDeviceIndex = this.ListWaveOutDevices().IndexOf(philipsMonitor);

            return outputDeviceIndex;
        }

        private WaveOutCapabilities GetDeviceByPartialName(string partial)
            => this.ListWaveOutDevices().SingleOrDefault(d => d.ProductName.Contains(partial));

        private IList<WaveOutCapabilities> ListWaveOutDevices()
        {
            var listOfOutputDevices = new List<WaveOutCapabilities>();

            for (int n = 0; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                listOfOutputDevices.Add(caps);
            }

            return listOfOutputDevices;
        }
    }
}
