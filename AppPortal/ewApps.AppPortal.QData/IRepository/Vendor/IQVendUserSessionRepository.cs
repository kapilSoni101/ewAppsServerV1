
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {
   public interface IQVendUserSessionRepository {

        Task<VendorUserSessionDTO> GetVendorPortalUserSessionInfoAsync(Guid identityUserId, Guid tenantId, string portalKey, int userType);
        Task<List<UserSessionAppDTO>> GetSessionAppDetailsAsync(Guid identityUserId, Guid tenantId, int userType);
    }
}
