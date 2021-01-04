/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 20 November 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 20 November 2018
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

namespace ewApps.AppPortal.WebApi.Controllers.Business
{

    /// <summary>
    ///dashboard Controller expose all Dashboard related APIs, It allow get operation on dashboard entity.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class BusDashboardController : ControllerBase
    {

        /// <summary>
        /// App controller expose all Application related APIs, It allow Get operation on Dasbhoard entity.
        /// </summary>

        #region Local Member

        IQBizPayDashboardDS _bizPayDashboardDS;
        IQBizCustDashboardDS _bizCustDashboardDS;
        IQBizVendDashboardDS _bizVendDashboardDS;

        #endregion

        #region  Constructor

        /// <summary>
        /// This is the constructor injecting dashboard dataservice
        /// </summary>
        /// <param name="bizPayDashboardDS"></param>
        public BusDashboardController(IQBizPayDashboardDS bizPayDashboardDS, IQBizCustDashboardDS bizCustDashboardDS, IQBizVendDashboardDS bizVendDashboardDS) {
            _bizPayDashboardDS = bizPayDashboardDS;
            _bizCustDashboardDS = bizCustDashboardDS;
            _bizVendDashboardDS = bizVendDashboardDS;
        }

        #endregion Constructor

        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("invoicesstatuscountfortenant")]
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizPayDashboardDS.GetInvoicesStatusCountForDashBoardByTenantAsync(token);
        }

        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getmonthnameandsumofinvoicefortenant")]
        public async Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizPayDashboardDS.GetMonthNameAndSumOfInvoiceByTenantListAsync(token);
        }

        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getupcomingpaymentfortenant")]
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizPayDashboardDS.GetAllUpcomingPaymentListAsync(token);
        }

        // <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getrecentpaymentfortenant")]
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizPayDashboardDS.GetAllRecentPaymentListAsync(token);
        }

        // <summary>
        /// Get Invoice List.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getrecentinvoicefortenant")]
        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoiceListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizPayDashboardDS.GetAllRecentInvoicesByTenantListAsync(token);
        }

        // <summary>
        /// Get Sales Quotation and Sales Orders Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getsalesquotationandsalesordersfortenant")]
        public async Task<SalesQuotationsAndOrdersStatusCountDTO> GetSalesQuotationsAndOrdersStatusCountForDashBoardAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetSalesQuotationsAndOrdersStatusCountForDashBoardByTenantAsync(token);
        }

        // <summary>
        /// Get Recent Sales QuoTations 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentsalesquotationsfortenant")]
        public async Task<List<RecentSalesQuotationsDTO>> GetAllRecentSalesQuotationsListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetAllRecentSalesQuotationsByTenantListAsync(token);
        }

        // <summary>
        /// Get Recent Sales Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentsalesordersfortenant")]
        public async Task<List<RecentSalesOrdersDTO>> GetAllRecentSalesOrdersListAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetAllRecentSalesOrdersByTenantListAsync(token);
        }

        // <summary>
        /// Get Recent Deliveries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentdelivieresfortenant")]
        public async Task<List<RecentDeliveriesDTO>> GetAllRecentDeliveriesByAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetAllRecentDeliveriesByTenantListAsync(token);
        }


        // <summary>
        /// Get Upcoming Deliveries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getupcomingdelivieresfortenant")]
        public async Task<List<UpcomingDeliveriesDTO>> GetAllUpcomingDeliveriesByAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetAllUpcomingDeliveriesByTenantListAsync(token);
        }

        // <summary>
        /// Get Deliveries Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getdeliveriescountfortenant")]
        public async Task<DeliveriesStatusCountDTO> GetDeliveriesStatusCountForDashBoardAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizCustDashboardDS.GetDeliveriesStatusCountForDashBoardByTenantAsync(token);
        }


        // <summary>
        /// Get Invoice Status Count for Vendor.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getinvoicestatuscountfortenant")]
           public async Task<VendApInvoiceStatusDTO> GetVendInvoicesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetVendInvoicesStatusCountForDashBoardByTenantAsync(token);
        }


        // <summary>
        /// Get Order line Status Count for Vendor.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getorderlinestatuscountfortenant")]      
        public async Task<VendOrdersLineStatusDTO> GetVendOrderLineStatusCountForDashBoardByTenantAsync( CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetVendOrderLineStatusCountForDashBoardByTenantAsync(token);
        }

        // <summary>
        /// Get Purchase Order Status Count for Vendor.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getpurchaseorderstatuscountfortenant")]        
        public async Task<VendOrderStatusDTO> GetVendOrderStatusCountForDashBoardByTenantAsync( CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetVendOrderStatusCountForDashBoardByTenantAsync(token);
        }

        // <summary>
        /// Get Open Line List.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getopenlinelistfortenant")]
        public async Task<List<VendOpenLinesListDTO>> GetAllVendOpenLineListByTenantLAsync( CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetAllVendOpenLineListByTenantLAsync(token);
        }

        // <summary>
        /// Get Recent AP Invoices.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getrencetapinvoiceslistfortenant")]
        public async Task<List<VendRecentAPInvoices>> GetAllVendRecentAPInvoicesListByTenantLAsync( CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetAllVendRecentAPInvoicesListByTenantLAsync(token);
        }

        // <summary>
        /// Get recent purchase order.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("vend/getrencetpurchaseorderlistfortenant")]
        public async Task<List<VendRecentPurchaseOrder>> GetAllVendRecentPurchaseOrderListByTenantLAsync( CancellationToken token = default(CancellationToken)) {
            return await _bizVendDashboardDS.GetAllVendRecentPurchaseOrderListByTenantLAsync(token);
        }

    }
}