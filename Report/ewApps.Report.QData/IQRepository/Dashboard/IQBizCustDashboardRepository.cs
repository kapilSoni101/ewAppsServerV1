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
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QData {
    /// <summary>
    /// This is the repository responsible for filtering data realted to Business portal Customer App Dashboard and services related to it
    /// </summary>
    public interface IQBizCustDashboardRepository:IBaseRepository<BaseDTO> {


        /// <summary>
        /// Get Sales Quotations And Sales Orders Status By Business Tenant Id 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<SalesQuotationsAndOrdersStatusCountDTO> GetSalesQuotationsAndOrdersStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Recent Sales Quotation List By Business Tenant Id 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentSalesQuotationsDTO>> GetAllRecentSalesQuotationsByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Recent Sales Orders List By Business Tenant Id 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentSalesOrdersDTO>> GetAllRecentSalesOrdersByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Recent Deliveries List By Business Tenant Id 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentDeliveriesDTO>> GetAllRecentDeliveriesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Upcoming Deliveries List By Business Tenant Id 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<UpcomingDeliveriesDTO>> GetAllUpcomingDeliveriesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Deliveries Status By business TenantId 
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<DeliveriesStatusCountDTO> GetDeliveriesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    }
}
