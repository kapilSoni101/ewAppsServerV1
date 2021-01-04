/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 25 February 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 25 February 2019
 */

using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.CommonService;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Entity;
using ewApps.Core.DbConProvider;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.Data {

  /// <summary>
  /// Provide support method for add/update recurring schedule.
  /// </summary>
  public interface IRecurringPaymentRepository : IBaseRepository<RecurringPayment> {

        /// <summary>
        /// Get recurring payment detail list.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get recurring payment detail list by customerid.
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentByCustomerAsync(Guid customerid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get recurring payment schedule by customerid and order.
        /// </summary>
        /// <param name="customerid"></param>
        /// <param name="orderId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecurringPaymentViewDTO>> GetRecurrtingPaymentByCustomerOrderAsync(Guid customerid, string orderId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get recurring detail model by id.
        /// </summary>
        /// <param name="recurringId"></param>
        /// <returns></returns>
        Task<RecurringDTO> GetRecurringModelById(Guid recurringId);

        /// <summary>
        /// Checking whether orderid exist.
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="tenantId"></param>
        /// <param name="recId">unique recurring id.</param>
        /// <returns></returns>
        bool IsOrderIdExist(string orderId, Guid tenantId, Guid recId);

    }
}
