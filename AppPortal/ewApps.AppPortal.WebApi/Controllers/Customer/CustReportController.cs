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
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;
using ewApps.Report.QDS;
using Microsoft.AspNetCore.Mvc;

namespace ewApps.AppPortal.WebApi {
    [Route("api/[controller]")]
    [ApiController]
    /// <summary>
    ///Cust Report controller expose all Bus Report related APIs, It allow add/update/delete operation on Cust Report.
    /// </summary>
    public class CustReportController : ControllerBase {

        #region Local Variable
        IQAppUserReportDS _appUserRptDS;
        IQSupportTicketReportDS _ticketRptDS;
        IQInvoiceReportDS _invoiceRptDS;
        IQPaymentReportDS _paymentRptDS;
        IQDeliveriesReportDS _deliveriesReportDS;
        IQSalesOrdersReportDS _salesOrdersReportDS;
        IQSalesQuotationsReportDS _salesQuotationsReportDS;
        IQItemMasterReportDS _itemMasterReportDS;
        #endregion

        #region Constructor
        public CustReportController(
       IQAppUserReportDS appUserRptDS,
       IQSupportTicketReportDS ticketRptDS,
       IQInvoiceReportDS invoiceRptDS,
       IQPaymentReportDS paymentRptDS,
        IQDeliveriesReportDS deliveriesReportDS,
        IQSalesOrdersReportDS salesOrdersReportDS,
        IQSalesQuotationsReportDS salesQuotationsReportDS,
        IQItemMasterReportDS itemMasterReportDS
       ) {
            _appUserRptDS = appUserRptDS;
            _ticketRptDS = ticketRptDS;
            _invoiceRptDS = invoiceRptDS;
            _paymentRptDS = paymentRptDS;
            _deliveriesReportDS = deliveriesReportDS;
            _salesOrdersReportDS = salesOrdersReportDS;
            _salesQuotationsReportDS = salesQuotationsReportDS;
            _itemMasterReportDS = itemMasterReportDS;
        }
        #endregion

        #region Partner

        [Route("part/appusers/cust/{businesspartnertenantid}/{appId}")]
        [HttpPut]
        public async Task<List<PartAppUserReportDTO>> GetPartCustAppUserListAsync(ReportFilterDTO filter, [FromRoute] Guid businesspartnertenantid, [FromRoute] Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetPartCustAppUserListAsync(filter, appId, businesspartnertenantid, token);
        }


        [Route("part/appusers/pay/{businesspartnertenantid}/{appId}")]
        [HttpPut]
        public async Task<List<PartAppUserReportDTO>> GetPartPayAppUserListAsync(ReportFilterDTO filter, [FromRoute] Guid businesspartnertenantid, [FromRoute] Guid appId, CancellationToken token = default(CancellationToken)) {
            return await _appUserRptDS.GetPartPayAppUserListAsync(filter, appId, businesspartnertenantid, token);
        }

        [Route("part/tickets")]
        [HttpPut]
        public async Task<List<PartSupportTicketReportDTO>> GetPartPaySupportTicketListByCustomerIdAsync([FromBody] PartReportSupportTicketParamDTO filter, Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _ticketRptDS.GetPartPaySupportTicketListByCustomerIdAsync(filter, token);
        }

        [HttpPut]
        [Route("part/invoices")]
        public Task<List<PartInvoiceReportDTO>> GetPartPayInvoiceListByCustomerAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return _invoiceRptDS.GetPartPayInvoiceListByCustomerAsync(filter, token);
        }

        [Route("part/payments")]
        [HttpPut]
        public async Task<List<PartPaymentReportDTO>> GetPartPaymentListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _paymentRptDS.GetPartPaymentListAsyncByCustomerAsync(filter, token);
        }

        [Route("part/itemmaster/{tenantId}")]
        [HttpPut]
        public async Task<List<PartItemMasterReportDTO>> GetPartItemmasterListAsync(ReportFilterDTO filter, [FromRoute] Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _itemMasterReportDS.GetCustItemMasterListByTenantIdAsync(filter, tenantId, token);
        }

        [Route("part/cust/deliveries")]
        [HttpPut]
        public async Task<List<PartCustDeliveriesReportDTO>> GetPartCustSalesDeliveriesListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _deliveriesReportDS.GetPartCustSalesDeliveriesListByCustomerIdAsync(filter, token);
        }


        [Route("part/cust/salesorders")]
        [HttpPut]
        public async Task<List<PartCustOrdersReportDTO>> GetPartCustSalesOrdersListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _salesOrdersReportDS.GetPartCustSalesOrdersListByCustomerIdAsync(filter, token);
        }


        [Route("part/cust/salesquotations")]
        [HttpPut]
        public async Task<List<PartCustQuotationsReportDTO>> GetPartCustSalesQuotationsListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _salesQuotationsReportDS.GetPartCustSalesQuotationsListByCustomerIdAsync(filter, token);
        }
        #endregion Partner

    }
}