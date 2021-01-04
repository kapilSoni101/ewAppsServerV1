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
    ///  Responsible for exposing all the methods that are intrecting with the DB for retriving the data related to BACustomer entity.
    /// </summary>
    public interface IBACustomerDS:IBaseDS<BACustomer> {

        /// <summary>
        /// Get Customer List.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BACustomerDTO>> GetCustomerListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer list.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="status">Status of customer</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BACustomerDTO>> GetCustomerListByStatusAndTenantIdAsync(Guid tenantId, int status, CancellationToken token = default(CancellationToken));

        Task<BusBACustomerDetailDTO> GetCustomerDetailByIdAsync(Guid CustomerId, CancellationToken token = default(CancellationToken));

        Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));

        Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid CustomerId, CancellationToken token = default(CancellationToken));

        Task<CustBACustomerDetailDTO> GetCustomerDetailByIdAsyncForCust(Guid CustomerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update BA Customer
        /// </summary>
        /// <param name="custConfigurationUpdateDTO"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<ResponseModelDTO> BACustomerUpdateAsync(CustConfigurationUpdateDTO custConfigurationUpdateDTO, CancellationToken token = default(CancellationToken));

        Task<bool> UpdateCustomerDetail(BACustomerDTO custDetailDTO, CancellationToken token = default(CancellationToken));

        Task<bool> UpdateCustomerDetailForBizSetupApp(BusCustomerUpdateDTO custDetailDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update customer and customercontact list.
        /// </summary>
        /// <param name="custContactDetailDTO">Detail object of customer and contact list.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> UpdateCustomerAndContactDetailAsync(BusBACustomerDetailDTO custContactDetailDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Method will delete customer and its associated data.
        /// </summary>
        /// <param name="baCustomerId">BACustomer Id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task DeleteCustomerAsync(Guid baCustomerId, CancellationToken token = default(CancellationToken));
    }
}
