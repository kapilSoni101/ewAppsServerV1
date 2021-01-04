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
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.Money;
using ewApps.Payment.Data;
using ewApps.Payment.DTO;
using ewApps.Payment.QData;

namespace ewApps.Payment.DS {
    /// <summary>
    /// A wrapper class contains method to get payment data w.r.t Invoice.
    /// </summary>
    public class QAPInvoiceDS: IQAPInvoiceDS {

        #region Local variables

        IQAPInvoiceRepository _paymentRep;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentRep"></param>
        public QAPInvoiceDS(IQAPInvoiceRepository paymentRep) {
            _paymentRep = paymentRep;
        }

        #endregion Constructor

        #region Get

        /// <summary>
        /// Get invoice detail by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAAPInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoiceDetailByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice entity by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAAPInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoiceByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get business info by tenantid.
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<TenantInfo> GetBusinessInfoByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetBusinessInfoByTenantAsync(tenantId, token);
        }

        /// <summary>
        /// Get invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAAPInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoiceByTenantAsync(filter, tenatId, token);
        }

        /// <summary>
        /// Get invoice list by invoiceIds.
        /// </summary>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAAPInvoiceDTO>> GetBAAPInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetBAAPInvoiceDTOListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>return list of cancel invoice</returns>
        public async Task<List<BAAPInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetCancelInvoiceByTenantAsync(filter, tenatId, token);
        }

        /// <summary>
        /// Get Vendor invoices by Vendor tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAAPInvoiceDTO>> GetInvoiceByVendorAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            List<BAAPInvoiceDTO> listDTO = await _paymentRep.GetInvoiceByVendorAsync(filter, token);
            return listDTO;
        }

        ///<inheritdoc/>
        public async Task<List<BAAPInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {           
            List<BAAPInvoiceDTO> listDTO = await _paymentRep.GetQuickPayInvoiceByTenantAsync(filter, tenantId, token);
            return listDTO;
        }

        /// <summary>
        /// Get Vendor payble invoices.
        /// </summary>
        /// <param name="vendorId">VendorId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAAPInvoiceDTO>> GetVendorPaybleinvoicesByVendorIdAsync(Guid vendorId, CancellationToken token = default(CancellationToken)) {
            List<BAAPInvoiceDTO> listDTO = await _paymentRep.GetVendorPaybleinvoicesByVendorIdAsync(vendorId, token);
            return listDTO;
        }

        /// <summary>
        /// Get primary user of a customer.
        /// </summary>
        /// <param name="businessPartnerTenantId">Business Partner TenantId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfVendorAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetPrimaryUserOfVendorAsync(businessPartnerTenantId, token);
        }

        #endregion Get

    }
}
