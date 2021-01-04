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
    public class QInvoiceDS:IQInvoiceDS {

        #region Local variables

        IQInvoiceRepository _paymentRep;

        #endregion Local variables

        #region Constructor

        /// <summary>
        /// Initilize local variables.
        /// </summary>
        /// <param name="paymentRep"></param>
        public QInvoiceDS(IQInvoiceRepository paymentRep) {
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
        public async Task<BAARInvoiceViewDTO> GetInvoiceDetailByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoiceDetailByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get invoice entity by invoice id.
        /// </summary>
        /// <param name="invoiceId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<BAARInvoiceEntityDTO> GetInvoiceByInvoiceIdAsync(Guid invoiceId, CancellationToken token = default(CancellationToken)) {
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
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetInvoiceByTenantAsync(filter, tenatId, token);
        }

        /// <summary>
        /// Get invoice list by invoiceIds.
        /// </summary>
        /// <param name="invoiceId">Id of invoiceId.</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetBAARInvoiceDTOListByInvoiceIdAsync(List<Guid> invoiceId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetBAARInvoiceDTOListByInvoiceIdAsync(invoiceId, token);
        }

        /// <summary>
        /// Get cancel invoice list by tenantId
        /// </summary>
        /// <param name="filter">Date filter dto</param>
        /// <param name="tenatId">Id of tenant in which invoice belong</param>
        /// <param name="token">A token that can be use to propogate async operation cancel notification. Using this token async operation duration can be control.</param>
        /// <returns>return list of cancel invoice</returns>
        public async Task<List<BAARInvoiceDTO>> GetCancelInvoiceByTenantAsync(ListDateFilterDTO filter, Guid tenatId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetCancelInvoiceByTenantAsync(filter, tenatId, token);
        }

        /// <summary>
        /// Get customer invoices by customer tenantid and filter by date.
        /// </summary>
        /// <param name="filter">Contains filter criteria.</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetInvoiceByCustomerAsync(ListDateFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            List<BAARInvoiceDTO> listDTO = await _paymentRep.GetInvoiceByCustomerAsync(filter, token);
            return listDTO;
        }

        ///<inheritdoc/>
        public async Task<List<BAARInvoiceDTO>> GetQuickPayInvoicesByTenantAsync(ListDateFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken)) {           
            List<BAARInvoiceDTO> listDTO = await _paymentRep.GetQuickPayInvoiceByTenantAsync(filter, tenantId, token);
            return listDTO;
        }

        /// <summary>
        /// Get customer payble invoices.
        /// </summary>
        /// <param name="customerId">customerid</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<List<BAARInvoiceDTO>> GetCustomerPaybleinvoicesByCustomerIdAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            List<BAARInvoiceDTO> listDTO = await _paymentRep.GetCustomerPaybleinvoicesByCustomerIdAsync(customerId, token);
            return listDTO;
        }

        /// <summary>
        /// Get primary user of a customer.
        /// </summary>
        /// <param name="businessPartnerTenantId">Business Partner TenantId</param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<UserTenantLinkingEntityDTO> GetPrimaryUserOfCustomerAsync(Guid businessPartnerTenantId, CancellationToken token = default(CancellationToken)) {
            return await _paymentRep.GetPrimaryUserOfCustomerAsync(businessPartnerTenantId, token);
        }

        //private List<BAARInvoiceDTO> MapCurrencyCodeToString(List<BAARInvoiceDTO> list) {
        //    CurrencyCultureInfoTable currencyCultureInfoTable = new CurrencyCultureInfoTable(null);
        //    for(int i = 0; i < list.Count; i++) {
        //        int currencyCode = Convert.ToInt32(list[i].CustomerCurrencyCode);
        //        CurrencyCultureInfo culInfo = currencyCultureInfoTable.GetCultureInfo((CurrencyISOCode)currencyCode);
        //        if(culInfo != null) {

        //        }
        //    }

        //    return list;
        //}

        #endregion Get

    }
}
