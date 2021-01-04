/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit Mundra <amundra@eworkplaceapps.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Amit
 * Last Updated On: 26 December 2018
 */


using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.BusinessEntity.DTO;
using ewApps.BusinessEntity.Entity;
using ewApps.Core.BaseService;
using Microsoft.AspNetCore.Http;

namespace ewApps.BusinessEntity.DS {

    /// <summary>
    /// This interface provides the CRUD operations related methods for invoice item entity.
    /// </summary>
    public interface IBAARInvoiceDS:IBaseDS<BAARInvoice> {

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
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Invoice id.</param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IEnumerable<BusBAARInvoiceAttachmentDTO>> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken cancellationToken = default(CancellationToken));

        /// <summary>
        /// Get invoice list.
        /// Invoice list will be filter by Tenant and from-todate.
        /// </summary>
        /// <param name="filter">Contains filter clause to get filter invoice list.</param>
        /// <param name="tenatId">Tenant id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDQ>> GetInvoiceByTenant(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region Add

        /// <summary>
        /// add invoice list.
        /// </summary>
        /// <param name="invoiceList"></param>
        /// <param name="tenantId"></param>
        /// <param name="tenantUserId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task AddInvoiceListAsync(List<BAARInvoiceSyncDTO> invoiceList, Guid tenantId, Guid tenantUserId, bool isBulkInsert, CancellationToken token = default(CancellationToken));

        #endregion Add

        #region Add Invoice Standalone

        /// <summary>
        /// Add invoice when application run in standalone.
        /// </summary>
        /// <param name="invoiceDTO">Invoice add object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InvoiceResponseDTO> AddInvoiceAsync(AddBAARInvoiceDTO invoiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="httpRequest">httpRequest object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<InvoiceResponseDTO> AddInvoiceAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update invoice when make payment in standalone.
        /// </summary>
        /// <param name="listinvoiceDTO">Invoice object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<object> UpdateInvoiceAmountAsync(List<BAARInvoiceEntityDTO> listinvoiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Invoice exist for customer.
        /// </summary>
        /// <param name="customerId">CustomerId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> IsInvoiceExistsAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        #endregion Add invoice Standalone

        #region Delete

        /// <summary>
        /// Deletes document with file.
        /// </summary>
        /// <param name="documentId"></param>
        Task DeleteInvoiceAttachment(Guid documentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Cancel invoice by invoice id.
        /// </summary>
        /// <param name="invoiceId">Invoiceid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<object> CancelInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Delete
    }
}