using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.DS {
    public interface IQCustomerAndUserDS {

        Task<List<TenantUserSetupListDTO>> GetAllCustSetupCustomerUsersAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetPaymentAppCustomerUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted);
        Task<List<TenantUserDetailsDTO>> GetCustomertAppCustomerUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted);
        Task<List<AppShortInfoDTO>> GetApplicationForCustomerUserAsync(Guid tenantUserId, Guid tenantId);
    Task<TenantUserDetailsDTO> GetAdminCustomerUsersDetailsAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted);
    Task<CustomerUserNotificationGeneralDTO> GetCustomerUserNotificationGeneralDataAsync(Guid tenantUserId, Guid businessPartnerTenantId, Guid tenantId, string appKey);
        Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey);
        Task<CustomerOnBoardNotificationDTO> GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId);

        /// <summary>
        /// Customer Setup User OnBoard 
        /// </summary>
        /// <param name="appKey"></param>
        /// <param name="businessTenantId"></param>
        /// <param name="businessPartnerTenantId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        Task<CustomerOnBoardNotificationDTO> GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId);
        

        Task<CustomerUserPermissionChangeNotificationGeneralDTO> GetCustomerUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey);

/// <summary>
/// 
/// </summary>
/// <param name="tenantId"></param>
/// <param name="userType"></param>
/// <param name="appId"></param>
/// <returns></returns>
    Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId);
    }
}
