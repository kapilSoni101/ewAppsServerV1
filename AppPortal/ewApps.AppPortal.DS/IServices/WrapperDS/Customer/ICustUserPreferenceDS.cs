using System;
using System.Threading;
using System.Threading.Tasks;

namespace ewApps.AppPortal.DS
{
    public interface ICustUserPreferenceDS
    {
        /// <summary>
        /// Adds the payment preference value.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="tenantUserId">The tenant user identifier.</param>
        /// <param name="createdBy">The created by.</param>
        /// <param name="userSessionID">The user session identifier.</param>
        /// <param name="emailPreference">The email preference.</param>
        /// <param name="smsPreference">The SMS preference.</param>
        /// <param name="asPreference">As preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> AddPaymentPrefValue(Guid tenantId, Guid tenantUserId, Guid createdBy, Guid appId, string userSessionID, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the be preference value.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="tenantUserId">The tenant user identifier.</param>
        /// <param name="createdBy">The created by.</param>
        /// <param name="userSessionID">The user session identifier.</param>
        /// <param name="emailPreference">The email preference.</param>
        /// <param name="smsPreference">The SMS preference.</param>
        /// <param name="asPreference">As preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> AddBEPrefValue(Guid tenantId, Guid tenantUserId, Guid createdBy, Guid appId, string userSessionID, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken);

        /// <summary>
        /// Adds the ap preference value.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="tenantUserId">The tenant user identifier.</param>
        /// <param name="appId">The application identifier.</param>
        /// <param name="createdBy">The created by.</param>
        /// <param name="emailPreference">The email preference.</param>
        /// <param name="smsPreference">The SMS preference.</param>
        /// <param name="asPreference">As preference.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<bool> AddAPPrefValue(Guid tenantId, Guid tenantUserId,  Guid createdBy,Guid appId, long emailPreference, long smsPreference, long asPreference, CancellationToken cancellationToken = default(CancellationToken));
    }
}
