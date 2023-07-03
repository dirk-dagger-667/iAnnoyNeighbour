using System.Threading;
using System.Threading.Tasks;

namespace iAnnoyNeightbour.WorkerService.Services.Contracts
{
    public interface IPlayerService
    {
        Task PlayAudioAsync(CancellationToken cancellationToken);
    }
}
