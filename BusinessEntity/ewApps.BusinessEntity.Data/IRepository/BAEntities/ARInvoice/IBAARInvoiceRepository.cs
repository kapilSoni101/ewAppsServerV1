// Hari sir comment
/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Souarbh Agrawal <sagrawal@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Souarbh Agrawal
 * Last Updated On: 26 December 2018
 */

 
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;

namespace ewApps.BusinessEntity.Data {

  // <summary>
  /// This interface provides methods to execute all database operations to get Invoice and
  /// related data transfer objects.
  // </summary>
  public interface IBAARInvoiceRepository:IBaseRepository<BAARInvoice> {

        #region Public methods

        /// <summary>
        /// Whether invoice exists.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return list of sales order entity.</returns>
        Task<bool> IsInvoiceExistAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken));

        #endregion Public Methods

        #region Get

        /// <summary>
        /// Get invoice.
        /// </summary>
        /// <param name="erpARInvoiceKey">ERP invoice unique key.</param>        
        /// <returns>return invoice.</returns>
        Task<BAARInvoice> GetInvoiceByERPInvoiceKeyAsync(string erpARInvoiceKey, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list.
        /// Invoice list will be filter by Tenant and from/todate.
        /// </summary>
        /// <param name="filter">Contains filter clause to get filter invoice list.</param>
        /// <param name="tenatId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDQ>> GetInvoiceByTenant(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Invoice exist for customer.
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> IsInvoiceExistsAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
