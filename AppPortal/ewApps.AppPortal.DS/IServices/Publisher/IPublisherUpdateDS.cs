using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IPublisherUpdateDS {
        Task UpdatePublisherAsync(PublisherUpdateReqDTO publisherUpdateReqDTO, CancellationToken cancellationToken = default(CancellationToken));
    }
}