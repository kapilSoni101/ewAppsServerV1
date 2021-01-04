using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {
    public class QCustomerAndUserDS :IQCustomerAndUserDS {

        IQCustomerAndUserRepository _qCustomerAndUserRepository;

        public QCustomerAndUserDS(IQCustomerAndUserRepository qCustomerAndUserRepository) {
            _qCustomerAndUserRepository = qCustomerAndUserRepository;
        }

        public async Task<List<TenantUserSetupListDTO>> GetAllCustSetupCustomerUsersAsync(int userType, Guid tenantId,Guid businessPartnerTenantId,  bool deleted) {
            return await _qCustomerAndUserRepository.GetAllCustomerSetupUsersAsync(userType, tenantId, businessPartnerTenantId,  deleted);
        }

        public async Task<List<TenantUserDetailsDTO>> GetPaymentAppCustomerUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
            return await _qCustomerAndUserRepository.GetPaymentAppCustomerUser(userType, tenantId, businessPartnerTenantId, appId, deleted);
        }

        public async Task<List<TenantUserDetailsDTO>> GetCustomertAppCustomerUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
            return await _qCustomerAndUserRepository.GetCustomerAppCustomerUsers(userType, tenantId, businessPartnerTenantId, appId, deleted);
        }

        ///<inheritdoc/>
        public async Task<List<AppShortInfoDTO>> GetApplicationForCustomerUserAsync(Guid tenantUserId, Guid tenantId) {
            return await _qCustomerAndUserRepository.GetApplicationForCustomerUserAsync(tenantUserId, tenantId);
        }
    public async Task<TenantUserDetailsDTO> GetAdminCustomerUsersDetailsAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted)
    {
      return await _qCustomerAndUserRepository.GetAdminCustomerUsersDetailsAsync(userType, tenantId, businessPartnerTenantId, appId,deleted);
    }
    ///<inheritdoc/>
    public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId,Guid businessPartnerTenantId, string appKey) {
            return await _qCustomerAndUserRepository.GetTenantUserDetails(tenantUserId, tenantId, businessPartnerTenantId, appKey);
        }

        public async Task<CustomerOnBoardNotificationDTO> GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {
            return await _qCustomerAndUserRepository.GetCustomerOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, businessPartnerTenantId, userId);
        }

        ///<inheritdoc/>
        public async Task<CustomerOnBoardNotificationDTO> GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {
            return await _qCustomerAndUserRepository.GetCustomerSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, businessPartnerTenantId, userId);
        }

        public async Task<CustomerUserNotificationGeneralDTO> GetCustomerUserNotificationGeneralDataAsync(Guid tenantUserId,Guid businessPartnerTenantId, Guid tenantId, string appKey) {
            return await _qCustomerAndUserRepository.GetCustomerUserNotificationGeneralDataAsync(tenantUserId, businessPartnerTenantId, tenantId, appKey);
        }

        public async Task<CustomerUserPermissionChangeNotificationGeneralDTO> GetCustomerUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey) {
            return await _qCustomerAndUserRepository.GetCustomerUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId, appKey);
        }

    /// <inheritdoc/>
    public async Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId) {

      bool result = false;

      List<RoleKeyCountDTO> roleKeyCountDTO = await _qCustomerAndUserRepository.CheckUserIsLastCustomerAdminUserAsync(tenantId, userType, appId);
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
  }
}
