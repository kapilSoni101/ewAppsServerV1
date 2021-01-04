/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Amit mundra <amundra@eworkplaceapps.com>
 * Date: 04 Septemeber 2019
 * 
 */
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.Common;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Options;
using System.Threading;
using ewApps.Payment.DTO;
using ewApps.Core.BaseService;


namespace ewApps.Payment.DS {
    /// <summary>
    /// Interface contains method to get payment data.
    /// </summary>
    public interface IQPaymentDS {

        #region Get

        /// <summary>
        /// Gets Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentInfoDTO> GetPaymentInfoDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        Task<PaymentDetailDQ> GetPaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get advace payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        Task<PaymentDetailDQ> GetAdvancePaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetVoidFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get tenant payment list filter by tenant and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid id, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>    
        /// <param name="paymentId">Payment id.</param>
        /// <param name="token"></param>
        Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region Advance Payment

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Advance Payment

        #region Vendor

        /// <summary>
        /// Get vendor payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<VendorPaymentDetailDQ>> GetFilterVendorTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get vendor payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<VendorPaymentDetailDQ>> GetVendorPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Vendor

    }
}
