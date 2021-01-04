using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Payment.DTO;

namespace ewApps.Payment.QData {
    public interface IQInvoiceRepository {

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice by invoiceId.
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// get invoice list by tenantId for quickpay.
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>List of invoice list dto</returns>
        Task<List<BAARInvoiceDTO>> GetQuickPayInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        Task<TenantInfo> GetBusinessInfoByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get primary user of a customer.
        /// </summary>
        /// <param name="businessPartnerTenantId">Business Partner TenantId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfCustomerAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken));

        #endregion Get        

    }
}
