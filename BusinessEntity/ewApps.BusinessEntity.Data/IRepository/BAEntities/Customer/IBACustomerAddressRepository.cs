/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 30 January 2019
 * 
 * Contributor/s: Ishwar Rathore/Amit Mundra.
 * Last Updated On: 31 January 2019
 */

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;


namespace ewApps.BusinessEntity.Data {

  /// <summary>
  /// Contains supportive method for Address entity.
  /// </summary>
  public interface IBACustomerAddressRepository:IBaseRepository<BACustomerAddress> {
        Task<List<CustomerAddressDTO>> GetCustomerAddressListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer address entity list.
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BACustomerAddress>> GetCustomerAddressEntityListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));
  }
}
