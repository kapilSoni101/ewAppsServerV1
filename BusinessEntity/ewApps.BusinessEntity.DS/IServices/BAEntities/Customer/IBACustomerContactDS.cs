/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sourabh Agarwal <sagrawal@eworkplaceapps.com>
 * Date: 08 August 2019
 * 
 * Contributor/s: Sourabh agrawal
 * Last Updated On: 22 August 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    ///  Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to BACustomerContact entity.
    /// </summary>
    public interface IBACustomerContactDS:IBaseDS<BACustomerContact> {

        /// <summary>
        /// Add customer contact detail list.
        /// </summary>
        /// <param name="customerContactList"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddCustomerContactListAsync(List<BACustomerContactSyncDTO> customerContactList, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Customer List.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<CustomerContactDTO>> GetCustomerContactListByIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Customer contact List by customerid.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BACustomerContact>> GetCustomerContactListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// To delete customer contact associated with Customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="commit"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteCustomerAsync(Guid customerId, bool commit, CancellationToken token = default(CancellationToken));
    }
}
