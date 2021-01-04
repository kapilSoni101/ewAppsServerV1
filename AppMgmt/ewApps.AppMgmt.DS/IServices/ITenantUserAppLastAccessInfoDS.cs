/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Ishwar Rathore <irathore@eworkplaceapps.com>
 * Date: 14 Aug 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 14 Aug 2019
 */

using System;
using System.Threading.Tasks;
using ewApps.AppMgmt.DTO;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    /// <summary>
    /// Provide functionality to write bussiness logic on AppLastAccessInfo entity by creating an object to this class. 
    /// </summary>
    public interface ITenantUserAppLastAccessInfoDS:IBaseDS<TenantUserAppLastAccessInfo> {

        /// <summary>
        /// Method user add update the user last access info for a particular tenant and a specific appliation.
        /// </summary>
        /// <param name="tenantUserAppLastAccessInfoRequestDTO">Data Transfer object used to create/update last acces info of the user</param>
        /// <returns>Returns entity added flag</returns>
        Task<bool> AddUpdateTenantUserLastAccessInfoAsync(TenantUserAppLastAccessInfoRequestDTO tenantUserAppLastAccessInfoRequestDTO, Guid appId);
    }
}
