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
    ///  Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to BACustomerPaymentDetail entity.
    /// </summary>
    public interface IBACustomerPaymentDetailDS:IBaseDS<BACustomerPaymentDetail> {

        /// <summary>
        /// Add Customer payment detail list .
        /// </summary>
        /// <param name="customerPaymentDetailList"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddCustomerPaymentDetailListAsync(List<BACustomerPaymentDetailSyncDTO> customerPaymentDetailList, Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
