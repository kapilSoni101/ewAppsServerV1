using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.AppPortal.Common;
using ewApps.AppPortal.DTO;
using ewApps.Core.ServiceProcessor;
using ewApps.Core.UserSessionService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ewApps.AppPortal.DS {

    /// <summary>
    /// A wrapper interface contains method to expose invoice/payment method.
    /// </summary>
    public interface IPaymentAndInvoiceDS {

        #region Get

        /// <summary>
        /// Get invoice list by tenant id.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get invoices customer and its payment info.
        /// </summary>
        /// <param name="reqDto">invoice list and customerid.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<InvoiceCustomerResponseDTO> GetInvoiceAndCustomerPayInfoByInvoiceIdAndSyncInvoiceAsync(InvoiceCustomerRequestDTO reqDto, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get cancel invoice list by tenant id. 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCancelInvoiceListByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer invoices by customer partner tenantid and filter by date.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns>retun list of invoices.</returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceListByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get quick pay invoice list.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payble invoices list.
        /// </summary>
        /// <param name="customerId">customer id</param>
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
        /// Get payment detail by paymentid.
        /// </summary>
        /// <param name="paymentId"></param>
        /// <returns></returns>
        Task<PaymentDetailModelDTO> GetPaymentDTOAsyncByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets advance Payments detail by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>    
        Task<AdvancePaymentDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>  
        /// <param name="paymentId">PaymentId</param>
        /// <param name="token"></param>
        Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<PaymentDetailDTO>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<PaymentDetailDTO>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by invoice.
        /// </summary>
        /// <param name="invoiceId">filter by invoice.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<PaymentDetailDTO>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<PaymentDetailDTO>> GetVoidPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment history by tenant and from/todate for a invoice by partnerid.
        /// </summary>
        /// <param name="filter">filter for getting tenant payment list.</param>
        /// <param name="token">For cancellation</param>
        /// <returns>IList of Payment entities</returns>
        Task<List<PaymentDetailDTO>> GetSattledFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice attachment list.
        /// </summary>
        /// <param name="invoiceId">Unique invoiceId id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        Task<object> GetInvoiceAttachmentListByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment transection status flow.
        /// </summary>
        /// <param name="paymentId">Unique paymentId</param>
        /// <param name="paymentMode">Mode of payment Vericheck or card</param>
        /// <param name="token"></param>
        /// <returns>retun payment status.</returns>
        Task<List<PaymentTransectionStatusDTO>> GetPaymentStatusFlowListAsync(Guid paymentId, string paymentMode, CancellationToken token = default(CancellationToken));

        #endregion Get

        #region Get PreAuth payment

        /// <summary>
        /// Get preauthorized payment history.
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<IList<PreAuthPaymentDetailDTO>> GetFilterTenantPreAuthPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Get PreAuth Payment

        #region Advance Payment

        /// <summary>
        /// Get advance payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDTO>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        Task<IList<PaymentDetailDTO>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Advance Payment

        #region Pre Auth Payment

        /// <summary>
        /// Authorize/Block Amount from Credit card for future use.
        /// </summary>
        /// <param name="payments">authorized payment object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<object> AuthorizeCardPaymentAsync(AddPreAuthPaymentDTO payments, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// UnAuthorize/UnBlock Amount from Credit card for future use.
        /// </summary>
        /// <param name="preAuthPaymentId"></param>        
        /// <param name="token"></param>
        /// <returns></returns>
        Task VoidPreAuthPaymentAsync(Guid preAuthPaymentId, CancellationToken token = default(CancellationToken));

        #endregion Pre Auth Payment

        #region Invoice

        /// <summary>
        /// Get invoice by id.
        /// </summary>
        /// <param name="invoiceId">Unique invoice id.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceViewDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Invoice

        #region Quick Payment

        /// <summary>
        /// Generate deeplink of a invoice.
        /// </summary>
        /// <param name="deepLinkRequestModel"></param>
        /// <param name="token">Cancellation token</param>
        /// <returns>return link url.</returns>
        Task<string> GenerateQuickPaylink(AddDeepLinkDTO deepLinkRequestModel, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice detail by deeplink shorturl.
        /// </summary>
        /// <param name="shortUrlKey">Shorturl key</param>
        /// <param name="machineIPAddress">Machine IP address.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<QuickPayInvoiceDetailDTO> GetInvoiceCustomerPaymentDetailAsyncByQuickPaylink(string shortUrlKey, string machineIPAddress, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="shorturl">a key generated for making payment.</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<object> QuickPaymentsAsync(AddPaymentDTO[] payments, string shorturl, CancellationToken token = default(CancellationToken));

        #endregion Quick Payment

        #region Add Payment

        /// <summary>
        /// Add muliple invoice payment.
        /// </summary>
        /// <param name="payments">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<object> AddPaymentsAsync(AddPaymentDTO[] payments, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Pay advance payment.
        /// </summary>
        /// <param name="payment">AddPaymentDTO object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<object> AddAdvancePaymentsAsync(AddPaymentDTO payment, CancellationToken token = default(CancellationToken));

        #endregion Add Payment

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
        /// <param name="serviceName">Name of service, used for pay invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task VoidPaymentAsync(Guid paymentId, string serviceName, CancellationToken token = default(CancellationToken));

        #endregion Payment Status

        #region  AddInvoice

        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="invoiceDTO">Add invoice object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<InvoiceResponseDTO> AddInvoiceAsync(AddBAARInvoiceDTO invoiceDTO, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Add invoice.
        /// </summary>
        /// <param name="httpRequest">httpRequest object</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<InvoiceResponseDTO> AddInvoiceAsync(HttpRequest httpRequest, string request, CancellationToken token = default(CancellationToken));

        #endregion  AddInvoice

        #region Delete

        /// <summary>
        /// delete invoice attachment.
        /// </summary>
        /// <param name="invoiceId">invoiceId</param>
        /// <param name="documentId">Unique document id.</param>
        /// <param name="token"></param>
        /// <returns>return attachment list.</returns>
        Task<object> DeleteInvoiceAttachmentByDocumentIdAsync(Guid invoiceId, Guid documentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Cancel invoice.
        /// </summary>
        /// <param name="invoiceId">invoice id</param>
        /// <param name="token">Cancellation token for async operations</param>   
        Task<object> CancelInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        #endregion Delete

    }
}
