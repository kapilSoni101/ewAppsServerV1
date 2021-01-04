using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData
{
  public interface IQBusinessAndUserRepository
  {

    /// <summary>
    /// Get filtered business list.
    /// </summary>
    /// <param name="filter">Filter the business list by filter object.</param>
    /// <param name="homeAppId">Home application id.</param>
    /// <param name="tenantId">tenantid</param>
    /// <param name="token"></param>
    /// <returns>return filtered business list.</returns>
    Task<List<BusinessViewModelDQ>> GetFilterBusinessListByTenantIdAsync(ListDateFilterDTO filter, Guid tenantId, Guid homeAppId, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get all the Application list subscribed by a business tenant.
    /// </summary>
    /// <param name="businessId">Id of Business Tenant</param>
    /// <returns>return list of application.</returns>
    Task<List<PubBusinessAppSubscriptionInfoDTO>> GetBusinessAppSubscriptionInfoDTOByBusinessIdAsync(Guid businessId, Guid homeAppId, Guid publisherTenantId, bool isdeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get Configuration Detail For Business Async 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));





    /// <summary>
    /// Get Business branding details
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get all the customer with application count ignoring the customer setup app.
    /// </summary>
    /// <param name="businessPartnerTenantId">business partner tenant identifiers.</param>
    /// <returns></returns>
    Task<List<BusCustomerUserDTO>> GetAllCustomerUserByBusinessPartnerId(Guid businessPartnerTenantId);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="businessPartnerTenantId"></param>
    /// <returns></returns>
    Task<List<BusVendorUserDTO>> GetAllVendorUserByBusinessPartnerId(Guid businessPartnerTenantId);

    /// <summary>
    /// Get list of all the asiged application to the user and breif details.
    /// </summary>
    /// <param name="businessPartnerTenantId">tenantid of the customer</param>
    /// <param name="tenantUserId">user identifier for which we need to retrive the data</param>
    /// <returns></returns>
    Task<List<AppInfoDTO>> GetCustomerUserAsignedAppInfo(Guid tenantId,Guid businessPartnerTenantId, Guid tenantUserId);

    /// <summary>
    /// Get tenantUser details.
    /// </summary>
    /// <param name="tenantUserId"></param>
    /// <param name="tenantId"></param>
    /// <param name="appKey"></param>
    /// <returns></returns>
    Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey);

    /// <summary>
    /// Gets the business name list by publisher id and business active state filter.
    /// </summary>
    /// <param name="publisherId">The publisher identifier.</param>
    /// <param name="businessActiveState">Business active state to filter business records.</param>
    /// <returns>Returns list of business names that matches provided publisher id and business active state filter.</returns>
    List<string> GetBusinessNameListByPublisherId(Guid publisherId, BooleanFilterEnum businessActiveState);

    Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId, Guid tenantId);

    #region User

    #region Business

    Task<List<TenantUserSetupListDTO>> GetAllUsersByUserTypeAsync(int userType, Guid tenantId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetAllPaymentUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetAllShipmentUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetAllVendorAppUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetAllCustomerAppUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted);

    #endregion Business

    #endregion User


    Task<BusinessOnBoardNotificationDTO> GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken));
    Task<BusinessOnBoardNotificationDTO> GetBusinessSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken));

    Task<BusinessUserNotificationGeneralDTO> GetBusinessUserNotificationGeneralDataAsync(Guid tenantUserId, Guid tenantId, string appKey);

    /// <summary>
    /// Permission changes notification data 
    /// </summary>
    /// <param name="tenantUserId"></param>
    /// <param name="invitedTenantUserId"></param>
    /// <param name="tenantId"></param>
    /// <param name="appKey"></param>
    /// <returns></returns>
    Task<BusinessUserPermissionNotificationGeneralDTO> GetBusinessUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey);

    Task<BusinessUserNotificationAppAccessUpdateDTO> GetBusinessUserNotificationGenDataAsync(Guid tenantUserId, Guid tenantId, Guid userId);



    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userType"></param>
    /// <param name="appId"></param>
    /// <returns></returns>
    Task<List<RoleKeyCountDTO>> CheckUserIsLastBusinessAdminUserAsync(Guid tenantId, int userType, Guid appId);
  }
}
