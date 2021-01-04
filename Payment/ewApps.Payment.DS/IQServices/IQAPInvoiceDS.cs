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
    public interface IQAPInvoiceDS {

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAAPInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice by invoiceId.
        /// </summary>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetBAAPInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAAPInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get vendor invoices by vendor tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetInvoiceByVendorAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto with required field.</param>
        /// <param name="tenantId">Tenantid.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get vendor payble invoices.
        /// </summary>
        /// <param name="vendorId">vendorId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAAPInvoiceDTO>> GetVendorPaybleinvoicesByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken));

        Task<TenantInfo> GetBusinessInfoByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get primary user of a customer.
        /// </summary>
        /// <param name="businessPartnerTenantId">Business Partner TenantId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfVendorAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken));

        #endregion Get

    }
}
