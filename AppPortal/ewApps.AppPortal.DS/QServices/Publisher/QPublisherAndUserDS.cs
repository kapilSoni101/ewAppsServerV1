using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.DTO.DBQuery;
using ewApps.AppPortal.Entity;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// A wrapper class for publisher to provide  mulitple context data.
    /// </summary>
    public class QPublisherAndUserDS:IQPublisherAndUserDS {

        #region Local member
        IQPublisherAndUserRepository _qPublisherAndUserRepository;
        IEntityThumbnailDS _entityThumbnailDS;
        #endregion Local member

        #region Constructor

        /// <summary>
        /// constructor to initilize the local variables.
        /// </summary>
        public QPublisherAndUserDS(IQPublisherAndUserRepository qPublisherRepository, IEntityThumbnailDS entityThumbnailDS) {

            _qPublisherAndUserRepository = qPublisherRepository;
            _entityThumbnailDS = entityThumbnailDS;

        }

        #endregion Constructor

        #region GET Publisher Branding

        /// <summary>
        /// get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<PublisherBrandingDQ> GetPublisherBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            PublisherBrandingDQ publisherBrandingDQ = await _qPublisherAndUserRepository.GetPublisherBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            //publisherBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            publisherBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(publisherBrandingDQ.ThumbnailId);
            return publisherBrandingDQ;
        }

        #endregion

        #region GET Configuration

        /// <summary>
        /// get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<ConfigurationDTO> GetConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            ConfigurationDTO configurationDQ = await _qPublisherAndUserRepository.GetConfigurationDetailAsync(tenantId, appId, cancellationToken);
            return configurationDQ;
        }

        #endregion

        #region Get

        /// <summary>
        /// Get appdetail bu publisher tenant and appid.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="pubTenantId"></param>
        /// <returns></returns>
        public async Task<AppDetailDQ> GetAppDetailsAsyncFromPubAppSettingAppAndTenantIDAsync(Guid appId, Guid pubTenantId, CancellationToken token = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetAppDetailsAsyncFromPubAppSettingAppAndTenantID(appId, pubTenantId, token);
        }

        /// <summary>
        ///  Get subscribed application list by a publisher tenant subdomain.
        /// </summary>
        /// <param name="subdomainName">Publisher tenant subdomain name</param>
        /// <param name="includeInactive">Include inactive application also.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<AppDQ>> GetPublisherSubscribedAppSubdomainAsync(string subdomainName, bool includeInactive, CancellationToken token = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherSubscribedAppSubdomainAsync(subdomainName, includeInactive, token);
        }

        /// <summary>
        /// Get application sub services subscribed by a Tenant applications.
        /// </summary>
        /// <param name="tenantId">Id of publisher Tenant</param>
        /// <returns>return application sub services and Application id.</returns>
        public async Task<List<TenantAppServiceDQ>> GetPublisherAppServiceByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherAppServiceByTenantIdAsync(tenantId, token);
        }

        /// <summary>
        /// Get publisher created subscription plan or master subscription plan.
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="publisherTenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<TenantApplicationSubscriptionDQ>> GetAppSubscriptionAsync(Guid appId, Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetAppSubscriptionAsync(appId, publisherTenantId, token);
        }

        /// <inheritdoc/>
        public async Task<List<PublisherDetailsDTO>> GetAllPublishersAsync(CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetAllPublishersAsync(cancellationToken);
        }


        /// <inheritdoc/>
        public async Task<PublisherViewDTO> GetPublisherDetailByPublisherIdAsync(Guid publisherId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherDetailByPublisherIdAsync(publisherId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetPublishersBusinessSubscriptionPlanListByAppIdsAndPublisherTenantIdAsync(List<Guid> appIdList, Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublishersBusinessSubscriptionPlanListByAppIdsAndPublisherTenantIdAsync(appIdList, publisherTenantId, cancellationToken);

        }

        /// <inheritdoc/>
        public async Task<List<PubAppSettingDTO>> GetPubAppSettingListByPublisherTenantIdAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPubAppSettingListByPublisherTenantIdAsync(publisherTenantId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<PublisherViewDTO> GetPublisherDetailByBusinessIdAsync(Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherDetailByBusinessIdAsync(businessTenantId, cancellationToken);
        }

        public async Task<PublisherPermissionNotificationDTO> GetPublisherUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId) {
            return await _qPublisherAndUserRepository.GetPublisherUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId);
        }


        #endregion Get

        #region Publisher Business Subscription Plan


        /// <inheritdoc/>
        public async Task<List<SubscriptionPlanInfoDTO>> GetSubscriptionPlanListByAppAndPubTenantIdAsync(Guid appId, Guid publisherTenantId, BooleanFilterEnum planState, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetSubscriptionPlanListByAppAndPubTenantIdAsync(appId, publisherTenantId, planState, cancellationToken);
        }


        #endregion

        #region Get Application List

        ///<inheritdoc/>
        public async Task<List<AppDetailDTO>> GetAppDetailsPublisherAsync(Guid publisherTenantId, CancellationToken token = default(CancellationToken)) {
            List<AppDetailDTO> appList = await _qPublisherAndUserRepository.GetAppDetailsPublisherAsync(publisherTenantId, token);
            return appList;
        }

        ///<inheritdoc/>
        public async Task<AppAndServiceDTO> GetAppDetailsWithServicesPublisherAsync(Guid pubAppSettingID, AppAndServiceDTO appAndServiceDTO, CancellationToken token = default(CancellationToken)) {


            // Get publisher app setting  details.
            appAndServiceDTO.AppDetailDTO = await _qPublisherAndUserRepository.GetAppDetailsFromPubAppSettingIDAsync(pubAppSettingID, token);

            return appAndServiceDTO;
        }

        #endregion Get Application List

        #region Serivce And Attributes

        public async Task<List<AppDetailInfoDTO>> GetAppsWithServiceAndAttributeAsync(Guid publisherTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            List<AppDetailInfoDTO> appList = await _qPublisherAndUserRepository.GetPubBusinessAppListByPubTenantIdAsync(publisherTenantId);

            IEnumerable<Guid> appIdList = appList.Select(i => i.Id);

            // Get Service list by app id.
            List<AppServiceDetailDTO> appServiceDetailDTOs = await _qPublisherAndUserRepository.GetPubBusinessAppServiceListByAppIdsAsync(appIdList.ToList(), cancellationToken);

            IEnumerable<Guid> serviceIdList = appServiceDetailDTOs.Select(i => i.SerivceId);
            
            // Get Attribute List by serivce id.

            List<AppServiceAttributeDetailDTO> appServiceAttrDetailDTOs = new List<AppServiceAttributeDetailDTO>();

            if(serviceIdList.Count() > 0) {
                appServiceAttrDetailDTOs = await _qPublisherAndUserRepository.GetPubBusinessAppServiceAttributeListByServiceIdsAsync(serviceIdList.ToList(), cancellationToken);
            }
            
            foreach(AppDetailInfoDTO app in appList) {
                app.AppServiceList = appServiceDetailDTOs.FindAll(i => i.AppId == app.Id);

                foreach(AppServiceDetailDTO service in app.AppServiceList) {
                    service.AppServiceAttributeList = appServiceAttrDetailDTOs.FindAll(i => i.AppServiceId == service.SerivceId);
                }
            }
            return appList;
        }


        #endregion

        #region Users

        /// <inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetAllPublisherUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qPublisherAndUserRepository.GetAllUsersByUserTypeAsync(userType, tenantId, appId, deleted);
        }

        /// <inheritdoc/>
        public async Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId) {

            bool result = false;

            List<RoleKeyCountDTO> roleKeyCountDTO = await _qPublisherAndUserRepository.CheckUserIsLastPublisherAdminUserAsync(tenantId, userType, appId);
            if(roleKeyCountDTO != null) {
                if(roleKeyCountDTO.Count == 1) {
                    result = true;
                    Tuple<bool, Guid> t = new Tuple<bool, Guid>(result, roleKeyCountDTO[0].TenantUserId);
                    return t;
                }
            }
            Tuple<bool, Guid> t1 = new Tuple<bool, Guid>(result, Guid.Empty);
            return t1;
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndPermissionViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey, bool deleted) {
            return await _qPublisherAndUserRepository.GetTenantUserDetails(tenantUserId, tenantId, appKey, deleted);
        }

        #endregion Users

        #region GetPublisherName    

        public async Task<StringDTO> GetPublisherNameByBusinessTenantId(Guid businessTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherNameByBusinessTenantId(businessTenantId, cancellationToken);
        }

        public async Task<StringDTO> GetPublisherNameByPartnerTenantId(Guid partnerTenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherNameByPartnerTenantId(partnerTenantId, cancellationToken);
        }

        public async Task<StringDTO> GetPublisherNameByTenantId(Guid tenantId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qPublisherAndUserRepository.GetPublisherNameByTenantId(tenantId, cancellationToken);
        }

        #endregion GetPublisherName

    }

}

