using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;
using ewApps.Core.BaseService;
using ewApps.Core.DMService;
using ewApps.Core.UserSessionService;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// A wrapper queryied class to get business and its supported entity data. 
    /// </summary>
    public class QBusinessAndUserDS:IQBusinessAndUserDS {

        #region Local Variables

        IUserSessionManager _userSessionManager;
        IEntityThumbnailDS _entityThumbnailDS;
        IQBusinessAndUserRepository _qBusinessAndUserRepository;

        #endregion Local Variables

        #region Constructor

        /// <summary>
        /// Contructor to initilize local variables.
        /// </summary>
        public QBusinessAndUserDS(IQBusinessAndUserRepository qBusinessRepository, IEntityThumbnailDS entityThumbnailDS, IUserSessionManager userSessionManager) {
            _qBusinessAndUserRepository = qBusinessRepository;
            _entityThumbnailDS = entityThumbnailDS;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Get

        public List<string> GetBusinessNameListByPublisherId(Guid publisherId, BooleanFilterEnum businessActiveState) {
            return _qBusinessAndUserRepository.GetBusinessNameListByPublisherId(publisherId, businessActiveState);
        }

        /// <summary>
        /// Get filteredbusiness  tenant list.
        /// </summary>
        /// <param name="filter">Filter object</param>
        /// <returns>return list of business of login tenant.</returns>
        public async Task<List<BusinessViewModelDQ>> GetBusinessListAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            Guid tenantId = _userSessionManager.GetSession().TenantId;
            //TO DO Amit 
            Guid homeAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            return await GetFilterBusinessListAsync(filter, tenantId, homeAppId, token);
        }

        /// <summary>
        /// Get filter business list.
        /// </summary>
        /// <param name="filter">Filter object contains filter properties.</param>
        /// <param name="homeAppId">Home application id.</param>
        /// <param name="tenantId">Filter by tenantid.</param>
        /// <param name="token"></param>
        /// <returns>return filter business list.</returns>
        public async Task<List<BusinessViewModelDQ>> GetFilterBusinessListAsync(ListDateFilterDTO filter, Guid tenantId, Guid homeAppId, CancellationToken token = default(CancellationToken)) {
            return await _qBusinessAndUserRepository.GetFilterBusinessListByTenantIdAsync(filter, tenantId, homeAppId, token);
        }


        /// <summary>
        /// Get subscribed application list by tenantid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="publisherTenantId"></param>
        /// <param name="isdeleted"></param>
        /// <param name="token"></param>
        /// <returns>return application list.</returns>
        public async Task<List<PubBusinessAppSubscriptionInfoDTO>> GetBusinessAppSubscriptionInfoDTOAsync(Guid tenantId, Guid publisherTenantId, bool isdeleted, CancellationToken token = default(CancellationToken)) {
            Guid homeAppId = new Guid(Core.BaseService.Constants.BusinessApplicationId);
            List<PubBusinessAppSubscriptionInfoDTO> list = await _qBusinessAndUserRepository.GetBusinessAppSubscriptionInfoDTOByBusinessIdAsync(tenantId, homeAppId, publisherTenantId, isdeleted, token);
            return list;
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserSetupListDTO>> GetAllBusinessUsersAsync(int userType, Guid tenantId, bool deleted) {
            return await _qBusinessAndUserRepository.GetAllUsersByUserTypeAsync(userType, tenantId, deleted);
        }

        ///<inheritdoc/>
        public async Task<List<AppShortInfoDTO>> GetApplicationForUserAsync(Guid tenantUserId, Guid tenantId) {
            return await _qBusinessAndUserRepository.GetApplicationForUserAsync(tenantUserId, tenantId);
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetPaymentAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qBusinessAndUserRepository.GetAllPaymentUsersByUserTypeAsync(userType, tenantId, appId, deleted);
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetShipmenttAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qBusinessAndUserRepository.GetAllShipmentUsersByUserTypeAsync(userType, tenantId, appId, deleted);
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetCustomerAppAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qBusinessAndUserRepository.GetAllCustomerAppUsersByUserTypeAsync(userType, tenantId, appId, deleted);
        }

        ///<inheritdoc/>
        public async Task<List<TenantUserDetailsDTO>> GetVendorAppAllBusinessUsersAsync(int userType, Guid tenantId, Guid appId, bool deleted) {
            return await _qBusinessAndUserRepository.GetAllVendorAppUsersByUserTypeAsync(userType, tenantId, appId, deleted);
        }

        ///<inheritdoc/>
        public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, string appKey) {
            return await _qBusinessAndUserRepository.GetTenantUserDetails(tenantUserId, tenantId, appKey);
        }

        ///<inheritdoc/>
        public async Task<List<BusCustomerUserDTO>> GetAllCustomerUserByBusinessPartnerId(Guid tenantId,Guid businessPartnerTenantId) {
            List<BusCustomerUserDTO> busCustomerUserDTOs = await _qBusinessAndUserRepository.GetAllCustomerUserByBusinessPartnerId(businessPartnerTenantId);
            if(busCustomerUserDTOs != null && busCustomerUserDTOs.Count > 0) {
                foreach(BusCustomerUserDTO item in busCustomerUserDTOs) {
                    item.AssignedAppInfo = await _qBusinessAndUserRepository.GetCustomerUserAsignedAppInfo(tenantId,businessPartnerTenantId, item.TenantUserId);
                }
            }
            return busCustomerUserDTOs;
        }
    ///<inheritdoc/>
    public async Task<List<BusVendorUserDTO>> GetAllVendorUserByBusinessPartnerId(Guid businessPartnerTenantId)
    {
      List<BusVendorUserDTO> busVendorUserDTOs = await _qBusinessAndUserRepository.GetAllVendorUserByBusinessPartnerId(businessPartnerTenantId);
      if (busVendorUserDTOs != null && busVendorUserDTOs.Count > 0)
      {
        foreach (BusVendorUserDTO item in busVendorUserDTOs)
        {
          item.AssignedAppInfo = await _qBusinessAndUserRepository.GetCustomerUserAsignedAppInfo(businessPartnerTenantId,businessPartnerTenantId, item.TenantUserId);
        }
      }
      return busVendorUserDTOs;
    }

    #region Business Configuration
    /// <summary>
    /// get Business Configuration Detail 
    /// </summary>
    /// <param name="tenantId"></param>
    /// <param name="appId"></param>
    /// <param name="cancellationToken"></param>
    /// <returns>Business Configuration Model</returns>
    public async Task<BusConfigurationDTO> GetBusinessConfigurationDetailAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusConfigurationDTO configurationDQ = await _qBusinessAndUserRepository.GetBusinessConfigurationDetailAsync(tenantId, appId, cancellationToken);
            return configurationDQ;
        }
        #endregion

        #endregion Get

        #region GET Business Branding

        /// <summary>
        /// get platform branding details
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="appId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>PublisherBranding Model</returns>
        public async Task<BusinessBrandingDQ> GetBusinessBrandingAsync(Guid tenantId, Guid appId, CancellationToken cancellationToken = default(CancellationToken)) {
            BusinessBrandingDQ businessBrandingDQ = await _qBusinessAndUserRepository.GetBusinessBrandingAsync(tenantId, appId, cancellationToken);
            // Get platform thumbnail Model
            businessBrandingDQ.ThumbnailAddUpdateModel = _entityThumbnailDS.GetThumbnailInfoByOwnerEntityId(tenantId);
            return businessBrandingDQ;
        }

        #endregion

        #region Notification Predata

        /// <inheritdoc/>
        public async Task<BusinessOnBoardNotificationDTO> GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qBusinessAndUserRepository.GetBusinessOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, userId, cancellationToken);
        }

        /// <inheritdoc/>
        public async Task<BusinessOnBoardNotificationDTO> GetBusinessSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken)) {
            return await _qBusinessAndUserRepository.GetBusinessSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, userId, cancellationToken);

        }

        public async Task<BusinessUserNotificationGeneralDTO> GetBusinessUserNotificationGeneralDataAsync(Guid tenantUserId, Guid tenantId, string appKey) {
            return await _qBusinessAndUserRepository.GetBusinessUserNotificationGeneralDataAsync(tenantUserId, tenantId, appKey);
        }

        ///<inheritdoc/>
        public async Task<BusinessUserNotificationAppAccessUpdateDTO> GetBusinessUserNotificationGenDataAsync(Guid tenantUserId, Guid tenantId, Guid userId) {
            return await _qBusinessAndUserRepository.GetBusinessUserNotificationGenDataAsync(tenantUserId, tenantId, userId);
        }

        public async Task<BusinessUserPermissionNotificationGeneralDTO> GetBusinessUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId,Guid invitedTenantUserId, Guid tenantId, string appKey) {
            return await _qBusinessAndUserRepository.GetBusinessUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId, appKey);
        }


    /// <inheritdoc/>
    public async Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId) {

      bool result = false;

      List<RoleKeyCountDTO> roleKeyCountDTO = await _qBusinessAndUserRepository.CheckUserIsLastBusinessAdminUserAsync(tenantId, userType, appId);
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

    #endregion

  }
}
