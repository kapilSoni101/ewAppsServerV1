/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS {
    public interface IQCustPortCustDashboardDS {


        /// <summary>
        /// Get Recent Sales Quotation List By Business Customer Id 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentPurchaseQuotationsFCDTO>> GetAllRecentPurchaseQuotationsByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Recent Sales Orders List By Business Customer Id 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentSalesOrdersFCDTO>> GetAllRecentSalesOrdersByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Recent Deliveries List By Business Customer Id 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentDeliveriesFCDTO>> GetAllRecentDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Upcoming Deliveries List By Business Customer Id 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<UpcomingDeliveriesFCDTO>> GetAllUpcomingDeliveriesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Deliveries Status By business Customer Id 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<DeliveriesStatusCountFCDTO> GetDeliveriesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Last Six month Purchase 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PurchaseAndMonthNameFCDTO>> GetMonthNameAndSumOfPurchaseByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Name and Quantity of Sales Orders Items
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<SalesOrdersItemsCountFCDTO>> GetItemNameAndSumOfItemsQuantityByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

    }
}
