using System;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;

namespace ewApps.AppMgmt.DS {
    public interface IIdentityServerDS {

        Task<IdentityServerAddUserResponseDTO> AddUserOnIdentityServerAsync(IdentityUserDTO identityUserDTO);

        Task DeleteUserByTenantIdOnIdentityServerAsync(Guid idnetityUserId, Guid tenantId);

        Task MarkActiveInActiveOnIdentityServerAsync(bool active, Guid identityUserId, Guid tenantId);

        Task DeAssignApplicationOnIdentityServerAsync(Guid identityUserId, Guid tenantId, string appKey);

        Task AssignApplicationOnIdentityServerAsync(Guid identityUserId, Guid tenantId, string clientAppType);
    }
}
