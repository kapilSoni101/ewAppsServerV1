/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Sanjeev Khanna <skhanna@eworkplaceapps.com>
 * Date: 24 September 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 10 October 2018
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

  public interface IBACustomerRepository:IBaseRepository<BACustomer> {

        #region Get

        /// <summary>
        /// Get Customer list.
        /// </summary>
        /// <param name="tenatId"></param>
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

        /// <summary>
        /// Get Customer list.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BACustomerDTO> GetCustomerDetailByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BusCustomerSetUpAppDTO>> GetCustomerListForBizSetupApp(Guid tenantId, bool isDeleted, CancellationToken token = default(CancellationToken));

        Task<BusCustomerSetUpAppViewDTO> GetCustomerDetailForBizSetupApp(Guid customerId, CancellationToken token = default(CancellationToken));
        #endregion Get
    }
}
