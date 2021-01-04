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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.DTO;

namespace ewApps.AppPortal.QData {
  public interface IQVendorAndUserRepository {

    Task<List<TenantUserSetupListDTO>> GetAllVendorSetupUsersAsync(int userType, Guid tenantId, Guid businessPartnerTenantId, bool deleted);
        Task<TenantUserAndAppViewDTO> GetTenantUserDetails(Guid tenantUserId, Guid tenantId, Guid businessPartnerTenantId, string appKey);
        
        
        Task<VendorOnBoardNotificationDTO> GetVendorOnBoardDetailByAppAndBusinessAndUserIdAsync(string appKey, Guid businessTenantId, Guid vendorTenantId, Guid userId, CancellationToken cancellationToken = default(CancellationToken));
   
    }
}
