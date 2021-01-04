using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {
    public interface IQPlatformUserSessionRepository {

        Task<PlatfromUserSessionDTO> GetPlaformPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey);
        Task<UserSessionAppDTO> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, string appKey);
    }
}
