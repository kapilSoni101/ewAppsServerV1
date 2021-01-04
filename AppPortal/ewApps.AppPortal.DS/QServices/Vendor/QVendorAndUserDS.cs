/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:  Anil Nigam <anigam@eworkplaceapps.com>
 * Date: 10 February 2020
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;
using ewApps.AppPortal.QData;

namespace ewApps.AppPortal.DS {
    public class QVendorAndUserDS :IQVendorAndUserDS {

        IQVendorAndUserRepository _qVendorAndUserRepository;

        public QVendorAndUserDS(IQVendorAndUserRepository qVendorAndUserRepository) {
            _qVendorAndUserRepository = qVendorAndUserRepository;
        }

        public async Task<List<TenantUserSetupListDTO>> GetAllVendSetupVendorUsersAsync(int userType, Guid tenantId,Guid businessPartnerTenantId,  bool deleted) {
            return await _qVendorAndUserRepository.GetAllVendorSetupUsersAsync(userType, tenantId, businessPartnerTenantId,  deleted);
        }

    //    public async Task<List<TenantUserDetailsDTO>> GetPaymentAppVendorUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
    //        return await _qVendorAndUserRepository.GetPaymentAppVendorUser(userType, tenantId, businessPartnerTenantId, appId, deleted);
    //    }

    //    public async Task<List<TenantUserDetailsDTO>> GetVendortAppVendorUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted) {
    //        return await _qVendorAndUserRepository.GetVendorAppVendorUsers(userType, tenantId, businessPartnerTenantId, appId, deleted);
    //    }

    //    ///<inheritdoc/>
    //    public async Task<List<AppShortInfoDTO>> GetApplicationForVendorUserAsync(Guid tenantUserId, Guid tenantId) {
    //        return await _qVendorAndUserRepository.GetApplicationForVendorUserAsync(tenantUserId, tenantId);
    //    }

    ///<inheritdoc/>
    public async Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey) {
      return await _qVendorAndUserRepository.GetTenantUserDetails(tenantUserId, tenantId, businessPartnerTenantId, appKey);
    }

    //    public async Task<VendorOnBoardNotificationDTO> GetVendorOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {
    //        return await _qVendorAndUserRepository.GetVendorOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, businessPartnerTenantId, userId);
    //    }

    //    ///<inheritdoc/>
    //    public async Task<VendorOnBoardNotificationDTO> GetVendorSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId) {
    //        return await _qVendorAndUserRepository.GetVendorSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(appKey, businessTenantId, businessPartnerTenantId, userId);
    //    }

    //    public async Task<VendorUserNotificationGeneralDTO> GetVendorUserNotificationGeneralDataAsync(Guid tenantUserId,Guid businessPartnerTenantId, Guid tenantId, string appKey) {
    //        return await _qVendorAndUserRepository.GetVendorUserNotificationGeneralDataAsync(tenantUserId, businessPartnerTenantId, tenantId, appKey);
    //    }

    //    public async Task<VendorUserPermissionChangeNotificationGeneralDTO> GetVendorUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey) {
    //        return await _qVendorAndUserRepository.GetVendorUserPermissionChangeNotificationGeneralDataAsync(tenantUserId, invitedTenantUserId, tenantId, appKey);
    //    }

    ///// <inheritdoc/>
    //public async Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId) {

    //  bool result = false;

    //  List<RoleKeyCountDTO> roleKeyCountDTO = await _qVendorAndUserRepository.CheckUserIsLastVendorAdminUserAsync(tenantId, userType, appId);
    //  if(roleKeyCountDTO != null) {
    //    if(roleKeyCountDTO.Count == 1) {
    //      result = true;
    //      Tuple<bool, Guid> t = new Tuple<bool, Guid>(result, roleKeyCountDTO[0].TenantUserId);
    //      return t;
    //    }
    //  }
    //  Tuple<bool, Guid> t1 = new Tuple<bool, Guid>(result, Guid.Empty);
    //  return t1;
    //}
  }
}
