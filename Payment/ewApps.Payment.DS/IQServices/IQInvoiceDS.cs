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
    public interface IQInvoiceDS {

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice by invoiceId.
        /// </summary>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<BAARInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken));

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
        /// <param name="tenantId">Tenantid.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        Task<TenantInfo> GetBusinessInfoByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

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
