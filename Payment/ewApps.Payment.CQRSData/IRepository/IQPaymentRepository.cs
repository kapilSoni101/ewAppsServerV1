using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQPaymentRepository {

        #region Get

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
        /// <returns>return PaymentDetailDQ.</returns>
        Task<PaymentDetailDQ> GetPaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by partnerid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <returns></returns>
        Task<IList<PaymentDetailDQ>> GetCustomerFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetFilterTenantPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentInfoDTO> GetPaymentInfoDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets Payment DTO given its  id and inventoryid.
        /// </summary>
        /// <param name="id">  Id, unique key</param>
        /// <param name="invoiceId">  invoiceId</param>
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentDetailModelDTO> GetPaymentDTOAsync(Guid id, Guid invoiceId, CancellationToken token = default(CancellationToken));       

        /// <summary>
        /// Get the list of invoices paid in a single transection(payment).
        /// </summary>        
        Task<List<InvoiceInfoDTO>> GetInvoicesByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<List<PaymentDetailModelDTO>> GetPaymentDTOListAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets list of Payments by given Payment id.
        /// </summary>
        /// <param name="paymentId">  Id, unique key</param>        
        /// <param name="token">Cancellation token</param>
        /// <returns>Found Payment DTO</returns>
        Task<PaymentAdvanceDetailModelDTO> GetAdvancePaymentDTOAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        #endregion Get        

        #region Payment Hostory

        /// <summary>
        /// Get payment list by invoice.
        /// </summary>
        /// <param name="invoiceId">Filter by invoice.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetPaymentHistoryByInvoiceAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

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

        #endregion Payment History

        #region Advance Payment History

        /// <summary>
        /// Get advance payment history by paymentid.
        /// </summary>
        /// <returns></returns>
        Task<PaymentDetailDQ> GetAdvancePaymentHistoryByPaymentIdAsync(Guid paymentId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer advance payment list by partnerid.
        /// </summary>
        /// <returns></returns>
        Task<IList<PaymentDetailDQ>> GetCustomerAdvanceFilterPaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get payment list by tenantid.
        /// </summary>
        /// <param name="filter">Filter object contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>        
        Task<IList<PaymentDetailDQ>> GetFilterTenantAdvancePaymentHistoryAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        #endregion Advance Payment History

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
