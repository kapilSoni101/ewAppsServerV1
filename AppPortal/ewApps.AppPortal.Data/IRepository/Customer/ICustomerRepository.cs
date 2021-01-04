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
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Entity;
using ewApps.Core.BaseService;

namespace ewApps.AppPortal.Data {

  public interface ICustomerRepository:IBaseRepository<Customer> {

        /// <summary>
        /// Get customer by business partner id.
        /// </summary>
        /// <param name="busPartnerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<Customer> GetCustomerByBusinesPartnerIdAsync(Guid busPartnerId, CancellationToken token = default(CancellationToken));

  }
}
