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

  public interface IBACustomerContactRepository:IBaseRepository<BACustomerContact> {
        Task<List<CustomerContactDTO>> GetCustomerContactListByIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get BACustomer contact by customerid.
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BACustomerContact>> GetCustomerContactListByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));
  }
}
