/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author:Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 12 September 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 12 September 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQPayAppServiceRepository {


        /// <summary>
        /// Get Business AppService List 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<PayAppServiceDetailDTO>> GetBusinessAppServiceListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, CancellationToken cancellationToken = default(CancellationToken));


        /// <summary>
        /// Get BUsiness AppService and Attribute list along with account detail 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<PayAppServiceAttributeDetailDTO>> GetBusinessAppServiceAttributeAndAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceId, CancellationToken cancellationToken = default(CancellationToken));

        
        /// <summary>
        /// Get Business Appservice account list 
        /// </summary>
        /// <param name="appId"></param>
        /// <param name="tenantId"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<List<AppServiceAcctDetailDTO>> GetBusinessAppServiceAccountListByAppIdsAndTenantIdAsync(Guid appId, Guid tenantId, Guid serviceAttributeId, CancellationToken cancellationToken = default(CancellationToken));
    }
}
