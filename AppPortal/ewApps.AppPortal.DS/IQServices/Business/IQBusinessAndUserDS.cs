using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.QData;
using ewApps.AppPortal.DTO;
using ewApps.Core.UserSessionService;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.DS {
  public interface IQBusinessAndUserDS {

    #region Get

    /// <summary>
    /// Gets the business name list by publisher id and business active state filter.
    /// </summary>
    /// <param name="publisherId">The publisher identifier.</param>
    /// <param name="businessActiveState">Business active state to filter business records.</param>
    /// <returns>Returns list of business names that matches provided publisher id and business active state filter.</returns>
    List<string> GetBusinessNameListByPublisherId(Guid publisherId, BooleanFilterEnum businessActiveState);

    /// <summary>
    /// Get filteredbusiness  tenant list.
    /// </summary>
    /// <param name="filter">Filter object</param>
    /// <returns>return list of business of login tenant.</returns>
    Task<List<BusinessViewModelDQ>> GetBusinessListAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get filter business list.
    /// </summary>
    /// <param name="filter">Filter object contains filter properties.</param>
    /// <param name="homeAppId">Home application id.</param>
    /// <param name="tenantId">Filter by tenantid.</param>
    /// <param name="token"></param>
    /// <returns>return filter business list.</returns>
    Task<List<BusinessViewModelDQ>> GetFilterBusinessListAsync(ListDateFilterDTO filter, Guid tenantId, Guid homeAppId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get subscribed application list by tenantid.
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="publisherTenantId"></param>
    /// <param name="isdeleted"></param>
    /// <param name="token"></param>
    /// <returns>return application list.</returns>
    Task<List<PubBusinessAppSubscriptionInfoDTO>> GetBusinessAppSubscriptionInfoDTOAsync(Guid tenantId, Guid publisherTenantId, bool isdeleted, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get Business Configuration Detail 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken));

    #endregion Get

    Task<List<TenantUserSetupListDTO>> GetAllBusinessUsersAsync(int userType, Guid tenantId, bool deleted);

    /// <summary>
    /// Get Platform branding model
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get list of user for a particular customer along with its application details.
    /// </summary>
    /// <param name="businessPartnerTenantId">customer tenantId identifier of the customer.</param>
    /// <returns></returns>
    Task<List<BusCustomerUserDTO>> GetAllCustomerUserByBusinessPartnerId(Guid tenantId, Guid businessPartnerTenantId);

    Task<List<BusVendorUserDTO>> GetAllVendorUserByBusinessPartnerId(Guid businessPartnerTenantId);

    Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey);



    Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId, Guid tenantId);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="userType"></param>
    /// <param name="appId"></param>
    /// <returns></returns>
    Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId);

    #region Users

    #region Business

    Task<List<TenantUserDetailsDTO>> GetPaymentAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetShipmenttAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetCustomerAppAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted);
    Task<List<TenantUserDetailsDTO>> GetVendorAppAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted);

    #endregion Business

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

    #endregion Users
  }
}
