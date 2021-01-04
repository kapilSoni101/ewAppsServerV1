using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;

namespace ewApps.AppMgmt.DS {
    /// <summary>
    /// This interface provides method to manage tenant signup operations.
    /// </summary>
    public interface ITenantSignUpForPublisherDS {

        /// <summary>
        /// Add's publisher tenant, it's primary user information and other related data.
        /// </summary>
        /// <param name="publisherSignUpDTO">The publisher sign up dto.</param>
        /// <param name="cancellationToken">The async task token to notify about task cancellation.</param>
        /// <returns>Returns newly signup tenant and primary user information.</returns>
        Task<TenantSignUpResponseDTO> PublisherSignUpAsync(PublisherSignUpDTO publisherSignUpDTO, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Validates the publisher sign up request. Also if request is valid returns data required for sign-up otherwise returns validation result in form of <see cref="ErrorData"/>.
        /// </summary>
        /// <param name="publisherPreSignUpReqDTO">The publisher pre sign up req dto.</param>
        /// <returns>Returns either falied validation result or data required for publisher sign-up.</returns>
        Task<PublisherPreSignUpRespDTO> ValidatePublisherPreSignUpRequest(PublisherPreSignUpReqDTO publisherPreSignUpReqDTO);

        /// <summary>
        /// Gets the publisher pre update request data.
        /// </summary>
        /// <param name="publisherPreUpdateReqDTO">The publisher pre update req dto.</param>
        /// <returns>Returns application and subscription plan list with other requested data.</returns>
        Task<PublisherPreUpdateRespDTO> GetPublisherPreUpdateRequestDataAsync(PublisherPreUpdateReqDTO publisherPreUpdateReqDTO,CancellationToken cancellationToken);
    }
}
