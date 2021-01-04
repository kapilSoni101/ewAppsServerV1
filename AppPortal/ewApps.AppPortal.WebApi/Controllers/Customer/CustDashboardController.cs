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
    public class CustDashboardController : ControllerBase
    {
        #region Local Variable 
        IQCustPayDashboardDS _dashboardDataService;
        IQCustPortCustDashboardDS _custPortCustDashboardDS;
        #endregion

        #region Constructor
        public CustDashboardController(IQCustPayDashboardDS dashboardDataService, IQCustPortCustDashboardDS custPortCustDashboardDS) {
            _dashboardDataService = dashboardDataService;
            _custPortCustDashboardDS = custPortCustDashboardDS;
        }
        #endregion

        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("invoicesstatuscountforcustomer/{customerId}")]
        public async Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardDataService.GetInvoicesStatusCountForDashBoardByCustomerAsync(customerId, token);
        }

        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getbusinessnameandsumofinvoiceforcustomer/{customerId}")]
        public async Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardDataService.GetBusinessNameAndSumOfInvoiceByCustomerListAsync(customerId, token);
        }


        /// <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getupcomingpaymentforcustomer/{customerId}")]
        public async Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardDataService.GetAllUpcomingPaymentListForCustomerAsync(customerId, token);
        }

        // <summary>
        /// Get Invoice Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getrecentpaymentforcustomer/{customerId}")]
        public async Task<List<RecentPaymentDTO>> GetAllRecentPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _dashboardDataService.GetAllRecentPaymentListForCustomerAsync(customerId, token);
        }

        // <summary>
        /// Get Invoice List.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("getrecentinvoiceforcustomer/{customerId}")]

        public async Task<List<RecentInvoicesDTO>> GetAllRecentInvoiceListAsync([FromRoute] Guid customerId,CancellationToken token = default(CancellationToken)) {
            return await _dashboardDataService.GetAllRecentInvoicesByCustomerListAsync(customerId,token);
        }

        #region Customer App
        // <summary>
        /// Get Recent Sales QuoTations 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentsalesquotationsforcustomer/{customerId}")]
        public async Task<List<RecentPurchaseQuotationsFCDTO>> GetAllRecentSalesQuotationsListAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetAllRecentPurchaseQuotationsByCustomerListAsync(customerId, token);
        }

        // <summary>
        /// Get Recent Sales Orders.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentsalesordersforcustomer/{customerId}")]
        public async Task<List<RecentSalesOrdersFCDTO>> GetAllRecentSalesOrdersListAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetAllRecentSalesOrdersByCustomerListAsync(customerId, token);
        }

        // <summary>
        /// Get Recent Deliveries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getrecentdelivieresforcustomer/{customerId}")]
        public async Task<List<RecentDeliveriesFCDTO>> GetAllRecentDeliveriesAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetAllRecentDeliveriesByCustomerListAsync(customerId, token);
        }


        // <summary>
        /// Get Upcoming Deliveries.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getupcomingdelivieresforcustomer/{customerId}")]
        public async Task<List<UpcomingDeliveriesFCDTO>> GetAllUpcomingDeliveriesAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetAllUpcomingDeliveriesByCustomerListAsync(customerId, token);

        }

        // <summary>
        /// Get Deliveries Status Count.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getdeliveriescountforcustomer/{customerId}")]
        public async Task<DeliveriesStatusCountFCDTO> GetDeliveriesStatusCountForDashBoardAsync([FromRoute] Guid customerId,CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetDeliveriesStatusCountForDashBoardByCustomerAsync(customerId,token);
        }

        // <summary>
        /// Get Sales Order Name and Quantity of it items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getsalesordernameandquantityofitemforcustomer/{customerId}")]
        public async Task<List<SalesOrdersItemsCountFCDTO>> GetItemNameAndSumOfItemsQuantityByCustomerListAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetItemNameAndSumOfItemsQuantityByCustomerListAsync(customerId, token);
        }

        // <summary>
        /// Get Sales Order Name and Quantity of it items.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("cust/getmonthnameandsumofpurchaseforcustomer/{customerId}")]
        public async Task<List<PurchaseAndMonthNameFCDTO>> GetMonthNameAndSumOfPurchaseByCustomerListAsync([FromRoute] Guid customerId, CancellationToken token = default(CancellationToken)) {
            return await _custPortCustDashboardDS.GetMonthNameAndSumOfPurchaseByCustomerListAsync(customerId, token);
        }
        #endregion
    }
}