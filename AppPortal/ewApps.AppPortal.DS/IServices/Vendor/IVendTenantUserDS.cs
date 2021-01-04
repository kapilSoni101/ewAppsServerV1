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

  /// <summary>
  /// This class implements standard business logic and operations for Vendor TenantUser entity.
  /// </summary>  
  public interface IVendTenantUserDS {

    /// <summary>
    /// this method is used to user profile detail
    /// </summary>
    /// <param name="tenantUserId"></param>
    /// <returns></returns>
    Task<TenantUserProfileDTO> GetUserInfoByIdAsync(Guid tenantUserId);

    /// <summary>
    /// Update Tenant User Profile 
    /// </summary>
    /// <param name="tenantUserProfileDTO"></param>
    /// <returns></returns>
    Task<ResponseModelDTO> UpdateTenantUserProfile(TenantUserProfileDTO tenantUserProfileDTO);

    Task ForgotPasswordAsync(ForgotPasswordDTO forgotPasswordDTO);
    /// <summary>
    /// 
    /// </summary>
    /// <param name="businesspartnertenantid"></param>
    /// <param name="deleted"></param>
    /// <returns></returns>
    Task<List<TenantUserSetupListDTO>> GetAllVendSetupVendorUsersAsync(Guid businesspartnertenantid, bool deleted);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="tenantUserId"></param>
    /// <param name="businessPartnerTenantId"></param>
    /// <param name="deleted"></param>
    /// <returns></returns>
    Task<TenantUserAndAppViewDTO> GetTenantUserAndAppDetails(Guid tenantUserId, Guid businessPartnerTenantId, bool deleted);



  }
}
