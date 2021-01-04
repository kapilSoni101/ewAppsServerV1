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

namespace ewApps.AppMgmt.Data {
    public interface IUserTenantLinkingRepository:IBaseRepository<UserTenantLinking> {

        ///// <summary>
        ///// Get primary user of a customer.
        ///// </summary>
        ///// <param name="businesPartnerTenantId">Business partner tenant id.</param>
        ///// <param name="token"></param>
        ///// <returns>return UserTenantLinking of parimary user of a customer.</returns>
        //Task<UserTenantLinking> GetPrimaryUserOfCustomerAsync(Guid businesPartnerTenantId, CancellationToken token = default(CancellationToken));

    }
}
