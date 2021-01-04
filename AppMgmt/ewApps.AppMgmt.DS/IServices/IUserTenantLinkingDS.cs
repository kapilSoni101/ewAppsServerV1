/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore
 * Last Updated On: 30 January 2019
 */

using System;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppMgmt.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppMgmt.DS {

    public interface IUserTenantLinkingDS:IBaseDS<UserTenantLinking> {


        /// <summary>
        /// Get tanant primary user .
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="userType"></param>
        /// <returns></returns>
        Task<Guid> GetTenantPrimaryUserAsync(Guid tenantId, int userType, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="businessPartnerTenantId"></param>
        /// <returns></returns>
        Task<bool> CheckPrimaryUserExistsForCustomer(Guid businessPartnerTenantId);


    }
}
