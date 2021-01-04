using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IPublisherExtDS {

        /// <summary>
        /// Gets the publisher detail by publisher identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier to find publisher detail.</param>
        /// <param name="cancellationToken">The cancellation token to cnacel async operation.</param>
        /// <returns>Returns publisher details that matches provided publisher id.</returns>
        Task<PublisherViewDTO> GetPublisherDetailByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken));

    }
}