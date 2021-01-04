using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.QData {
    public interface IQPublisherAndUserRepository {

        Task<PublisherBrandingDQ> GetPublisherBrandingAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        Task<ConfigurationDTO> GetConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken token = default(CancellationToken));


        Task<AppDetailDQ> GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(Guid appId, Guid pubTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        ///  Get subscribed application list by a publisher tenant subdomain.
        /// </summary>
        /// <param name="subdomainName"></param>
        /// <param name="includeInactive">Include inactive application also.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<AppDQ>> GetPublisherSubscribedAppSubdomainAsync(string subdomainName, bool includeInactive, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="tenantId">Id of publisher Tenant</param>
        /// <returns>return application sub services and Application id.</returns>
        Task<List<TenantAppServiceDQ>> GetPublisherAppServiceByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get publisher created subscription plan or master subscription plan.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="publisherTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<TenantApplicationSubscriptionDQ>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the subscription plan list by application identifier and plan state.
        /// </summary>
        /// <param name="appId">The application identifier to get application specific subscription plan.</param>
        /// <param name="publisherTenantId">The publisher tenant id to get publisher's business app subscription plan.</param>
        /// <param name="planState">The plan state to filter subscription plan.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns subscription plan list that matches the input parameters.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppAndPubTenantIdAsync(Guid appId, Guid publisherTenantId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the application details publisher asynchronous.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<List<AppDetailDTO>> GetAppDetailsPublisherAsync(Guid publisherTenantId, CancellationToken token = default(CancellationToken));

        Task<AppDetailDTO> GetAppDetailsFromPubAppSettingIDAsync(Guid pubAppSettingID, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets all publishers.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns list of publishers detail.</returns>
        Task<List<PublisherDetailsDTO>> GetAllPublishersAsync(CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the pub business application list by pub tenant identifier.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token to cnacel async operation.</param>
        /// <returns>Returns all application assigned to publisher for business for given publisher tenant id.</returns>
        Task<List<AppDetailInfoDTO>> GetPubBusinessAppListByPubTenantIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the publisher detail by publisher identifier.
        /// </summary>
        /// <param name="publisherId">The publisher identifier to find publisher detail.</param>
        /// <param name="cancellationToken">The cancellation token to cnacel async operation.</param>
        /// <returns>Returns publisher details that matches provided publisher id.</returns>
        Task<PublisherViewDTO> GetPublisherDetailByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<AppServiceAttributeDetailDTO>> GetPubBusinessAppServiceAttributeListByServiceIdsAsync(List<Guid> serviceIdList, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<AppServiceDetailDTO>> GetPubBusinessAppServiceListByAppIdsAsync(List<Guid> appIdList, CancellationToken cancellationToken = default(CancellationToken));

        Task<List<AppServiceDetailDTO>> GetPubBusinessAppServiceListByAppIdsAsync(Guid appId, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Gets the publishers business subscription plan list by application ids and publisher tenant identifier asynchronous.
        /// </summary>
        /// <param name="appIdList">The application identifier list.</param>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns all saved publisher's business app subscirption plan information that matches provided app id and publisher tenant id.</returns>
        Task<List<SubscriptionPlanInfoDTO>> GetPublishersBusinessSubscriptionPlanListByAppIdsAndPublisherTenantIdAsync(List<Guid> appIdList, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the pub application setting list by publisher tenant identifier asynchronous.
        /// </summary>
        /// <param name="publisherTenantId">The publisher tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>Returns publisher application setting list that matches given publisher tenant id.</returns>
        Task<List<PubAppSettingDTO>> GetPubAppSettingListByPublisherTenantIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken));


        Task<List<TenantUserDetailsDTO>> GetAllUsersByUserTypeAsync(int userType, Guid tenantId, Guid appId, bool deleted);

        Task<List<RoleKeyCountDTO>> CheckUserIsLastPublisherAdminUserAsync(Guid tenantId, int userType, Guid appId);

        /// <summary>
        /// Gets the publisher name by business tenant identifier.
        /// </summary>
        /// <param name="businessTenantId">The business tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<StringDTO> GetPublisherNameByBusinessTenantId(Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the publisher name by partner tenant identifier.
        /// </summary>
        /// <param name="partnerTenantId">The partner tenant identifier.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns></returns>
        Task<StringDTO> GetPublisherNameByPartnerTenantId(Guid partnerTenantId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Gets the publisher name by tenant identifier.
        /// </summary>
        /// <param name="tenantId">The tenant identifier.</param>
        /// <param name="token">The token.</param>
        /// <returns></returns>
        Task<StringDTO> GetPublisherNameByTenantId(Guid tenantId, CancellationToken token = default(CancellationToken));


        Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted);

        Task<PublisherViewDTO> GetPublisherDetailByBusinessIdAsync(Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken));

        Task<PublisherPermissionNotificationDTO> GetPublisherUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId);

    }
}
