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

namespace ewApps.AppPortal.DS {
  public interface IQVendorAndUserDS {

    Task<List<TenantUserSetupListDTO>> GetAllVendSetupVendorUsersAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, bool deleted);

    //        Task<List<TenantUserDetailsDTO>> GetPaymentAppVendorUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted);
    //        Task<List<TenantUserDetailsDTO>> GetVendortAppVendorUsers(int userType, Guid tenantId, Guid businessPartnerTenantId, Guid appId, bool deleted);
    //        Task<List<AppShortInfoDTO>> GetApplicationForVendorUserAsync(Guid tenantUserId, Guid tenantId);

    //        Task<VendorUserNotificationGeneralDTO> GetVendorUserNotificationGeneralDataAsync(Guid tenantUserId, Guid businessPartnerTenantId, Guid tenantId, string appKey);
            Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey);
    //        Task<VendorOnBoardNotificationDTO> GetVendorOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId);

    //        /// <summary>
    //        /// Vendor Setup User OnBoard 
    //        /// </summary>
    //        /// <param name="appKey"></param>
    //        /// <param name="businessTenantId"></param>
    //        /// <param name="businessPartnerTenantId"></param>
    //        /// <param name="userId"></param>
    //        /// <returns></returns>
    //        Task<VendorOnBoardNotificationDTO> GetVendorSetupOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid businessPartnerTenantId, Guid userId);


    //        Task<VendorUserPermissionChangeNotificationGeneralDTO> GetVendorUserPermissionChangeNotificationGeneralDataAsync(Guid tenantUserId, Guid invitedTenantUserId, Guid tenantId, string appKey);

    ///// <summary>
    ///// 
    ///// </summary>
    ///// <param name="tenantId"></param>
    ///// <param name="userType"></param>
    ///// <param name="appId"></param>
    ///// <returns></returns>
    //    Task<Tuple<bool, Guid>> CheckUserIsLastAdminUserAsync(Guid tenantId, int userType, Guid appId);
    //    }
  }
}