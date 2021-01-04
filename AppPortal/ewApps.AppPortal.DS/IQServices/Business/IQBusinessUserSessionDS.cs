using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQBusinessUserSessionDS {

        /// <summary>
        /// Get the login user session by identityuserId, tenantId and appkey for the platform user.
        /// </summary>
        /// <param name="identityUserId">Users identity server linking identifier.</param>
        /// <param name="tenantId">tenant identifier of the user to get session for the user for a particular tenant.</param>
        /// <param name="appKey">This parameter indicates application key for the application for which we are geting the user session.</param>
        /// <returns>Returns platform user session data trasfer object containing user and its tenant and branding related information.</returns>
        Task<BusinessUserSessionDTO> GetUserSessionAsync(Guid identityUserId, Guid tenantId, string appKey);

        Task LogOut(string identityServerId);
    }
}
