/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 29 August 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 29 August 2019
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
    /// This is the repository responsible for filtering data realted to Business Payment Dashboard and services related to it
    /// </summary>
    public interface IQBizPayDashboardRepository:IBaseRepository<BaseDTO> {

        /// <summary>
        /// Get invoice status count by tenant id
        /// </summary>
        /// <returns></returns>
        Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business name and sum of invoice by tenant id
        /// </summary>
        /// <returns></returns>
        Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All upcoming payment for tenant 
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All upcoming recurring payment for business 
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingRecurringPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Recent payment for Tenant upto 1 years 
        /// </summary>
        /// <returns></returns>
        Task<List<RecentPaymentDTO>> GetAllRecentPaymentByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Recent Invoices For Tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByTenantListAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
