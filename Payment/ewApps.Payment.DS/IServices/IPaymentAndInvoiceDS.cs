using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using ewApps.Core.Common;
using ewApps.Core.CommonService;
using ewApps.Core.ExceptionService;
using ewApps.Core.Money;
using ewApps.Core.UserSessionService;
using ewApps.Payment.Common;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.Entity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ewApps.Payment.DS {

    /// <summary>
    /// A wrapper class conatins payment and invoice support methods.
    /// </summary>
    public interface IPaymentAndInvoiceDS {

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        ///<summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by invoiceId.
        /// </summary>
        /// <param name="invoiceId">Ids of invoice.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto with required field.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken));

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
        /// <param name="paymentId">PaymentId</param>
        /// <param name="token"></param>
        Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment transection status flow.
        /// </summary>
        /// <param name="paymentId">Unique paymentId</param>
        /// <param name="paymentMode">Mode of payment Vericheck or card</param>
        /// <param name="token"></param>
        /// <returns>retun payment status.</returns>
        Task<List<PaymentTransectionStatusDTO>> GetPaymentStatusFlowListAsync(Guid paymentId, string paymentMode, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region Get Payment History

        /// <summary>
        /// Get tenant payment list filter by tenant and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetVoidFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payment list filter by customer and from-to date.
        /// </summary>
        /// <param name="filter">Filter criter to filter data by.</param>
        /// <param name="token"></param>
        /// <returns>return paid invoice payment history.</returns>
        Task<IList<PaymentDetailDQ>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>   
        Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Get Payment History

        #region Make Payment

        /// <summary>
        /// Adds Payment for mulitple invoices.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        Task<PaymentDetailDQ> AddPaymentsAsync(AddPaymentDTO[] e, CancellationToken token = default(CancellationToken));

        #endregion Make Payment

        #region Payment Status

        /// <summary>
        /// Refund payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task RefundPaymentAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Void payment transaction.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <param name="serviceName">service used for paying invoice</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task VoidPaymentAsync(Guid paymentId, string serviceName, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Update payment status.
        /// </summary>
        /// <param name="vhTransactioStatusChangeDTO">Payment transection object.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task UpdatePaymentStatusAsync(WHTransactioStatusChangeDTO vhTransactioStatusChangeDTO, CancellationToken token = default(CancellationToken));

        #endregion Payment Status

        #region Advance Payment

        /// <summary>
        /// Add advance Payment for mulitple invoices.
        /// </summary>
        /// <param name="e" entity to be added</param>
        /// <returns>Added entity</returns>
        Task<PaymentDetailDQ> AddAdvancePaymentsAsync(AddAdvancedPaymentDTO e, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

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

    }
}
