/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 27 September 2019
 * 
 * Contributor/s: Sanjeev Khanna
 * Last Updated On: 27 September 2019
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi
{
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    ///Bus Report controller expose all Bus Report related APIs, It allow add/update/delete operation on Bus Report.
    /// </summary>
    public class BusReportController : ControllerBase
    {
        #region Local Variable
        IQAppUserReportDS _appUserRptDS;
        IQSupportTicketReportDS _ticketRptDS;
        IQInvoiceReportDS _invoiceRptDS;
        IQDeliveriesReportDS _deliveriesReportDS;
        IQSalesOrdersReportDS _salesOrdersReportDS;
        IQPurchaseOrdersReportDS _purchaseOrdersReportDS;
        IQSalesQuotationsReportDS _salesQuotationsReportDS;
        IQPaymentReportDS _paymentRptDS;
        IQItemMasterReportDS _itemMasterReportDS;
        IQOpenPurchaseLineReportDS _openPurchaseLineReportDS;
        IQVendorASNReportDS _vendorASNReportDS;
        #endregion

        #region Constructor
        public BusReportController(      
       IQAppUserReportDS appUserRptDS,
       IQSupportTicketReportDS ticketRptDS,
       IQInvoiceReportDS invoiceRptDS,
        IQDeliveriesReportDS deliveriesReportDS,
        IQSalesOrdersReportDS salesOrdersReportDS,
        IQItemMasterReportDS itemMasterReportDS,
        IQSalesQuotationsReportDS salesQuotationsReportDS,
        IQPurchaseOrdersReportDS purchaseOrdersReportDS,
         IQOpenPurchaseLineReportDS openPurchaseLineReportDS,
        IQPaymentReportDS paymentRptDS,
        IQVendorASNReportDS vendorASNReportDS
       ) {           
            _appUserRptDS = appUserRptDS;
            _ticketRptDS = ticketRptDS;
            _invoiceRptDS = invoiceRptDS;
            _deliveriesReportDS = deliveriesReportDS;
            _salesOrdersReportDS = salesOrdersReportDS;
            _salesQuotationsReportDS = salesQuotationsReportDS;
            _paymentRptDS = paymentRptDS;
            _itemMasterReportDS = itemMasterReportDS;
            _purchaseOrdersReportDS = purchaseOrdersReportDS;
            _openPurchaseLineReportDS = openPurchaseLineReportDS;
            _vendorASNReportDS = vendorASNReportDS;
        }
        #endregion

        [Route("biz/appusers/pay/{appId}")]
        [HttpPut]
        public async Task<List<BizAppUserReportDTO>> GetBizPayAppUserListAsync(ReportFilterDTO filter,[FromRoute]Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetBizPayAppUserListByUserTypeAsync(filter, appId, token);
        }

        [Route("biz/appusers/cust/{appId}")]
        [HttpPut]
        public async Task<List<BizAppUserReportDTO>> GetBizCustAppUserListAsync(ReportFilterDTO filter, [FromRoute]Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetBizCustAppUserListByUserTypeAsync(filter, appId, token);
        }

        [Route("biz/tickets")]
        [HttpPut]
        public async Task<List<BizSupportTicketReportDTO>> GetBizPaySupportTicketListByTenantAsync(PartReportSupportTicketParamDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _ticketRptDS.GetBizPaySupportTicketListByTenantAsync(filter, token);
        }

        [Route("biz/invoices")]
        [HttpPut]
        public async Task<List<BizInvoiceReportDTO>> GetBizPayInvoiceListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _invoiceRptDS.GetBizPayInvoiceListByTenantIdAsync(filter, token);
        }


        [Route("biz/cust/deliveries")]
        [HttpPut]
        public async Task<List<BizCustSalesDeliveriesReportDTO>> GetBizCustSalesDeliveriesListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _deliveriesReportDS.GetBizCustSalesDeliveriesListByTenantIdAsync(filter, token);
        }


        [Route("biz/cust/salesorders")]
        [HttpPut]
        public async Task<List<BizCustSalesOrdersReportDTO>> GetBizCustSalesOrdersListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _salesOrdersReportDS.GetBizCustSalesOrdersListByTenantIdAsync(filter, token);
        }


        [Route("biz/cust/salesquotations")]
        [HttpPut]
        public async Task<List<BizCustSalesQuotationsReportDTO>> GetBizCustSalesQuotationsListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _salesQuotationsReportDS.GetBizCustSalesQuotationsListByTenantIdAsync(filter, token);
        }

        [Route("biz/payments")]
        [HttpPut]
        public async Task<List<BizPaymentReceivedReportDTO>> GetBizPaymentListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentRptDS.GetBizPayRecPayListByTenantIdAsync(filter, token);
        }

        [Route("biz/vend/itemmaster")]
        [HttpPut]
        public async Task<List<VendItemMasterReportDTO>> GetBizVendItemMasterListByTenantIdAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken)) {
            return await _itemMasterReportDS.GetBizVendItemMasterListByTenantIdAsync(filter,  token);
        }

        [Route("biz/vend/purchaseorder")]
        [HttpPut]
        public async Task<List<VendPurchaseOrdersReportDTO>> GetBizVendPurchaseOrdersListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _purchaseOrdersReportDS.GetBizVendPurchaseOrdersListByTenantIdAsync(filter, token);
        }

        [Route("biz/vend/openpurchaselines")]
        [HttpPut]
        public async Task<List<VendOpenPurchaseLineReportDTO>> GetBizVendOpenPurchaseLineListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _openPurchaseLineReportDS.GetBizVendOpenPurchaseLineListByTenantIdAsync(filter, token);
        }

        [Route("biz/vend/invoices")]
        [HttpPut]
        public async Task<List<VendAPInvoicesReportDTO>> GetBizVendInvoiceListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _invoiceRptDS.GetBizVendInvoiceListByTenantIdAsync(filter, token);
        }

        [Route("biz/vend/asn")]
        [HttpPut]
        public async Task<List<VendorASNReportDTO>> GetBizVendorASNListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _vendorASNReportDS.GetBizVendorASNListByTenantIdAsync(filter, token);
        }

    }
}