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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS{

    // This class Contain Business Logic Of Business Payment Dashboard 
    public interface IQBizPayDashboardDS:IBaseDS<BaseDTO> {

        /// <summary>
        /// Gets the invoice status count for dashboard by given Tenant.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business name and sum of invoice by customer id
        /// </summary>
        /// <returns></returns>
        Task<List<InoviceAndMonthNameDTO>> GetMonthNameAndSumOfInvoiceByTenantListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Upcoming Payment list for business
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Recent Payment list for business upto 1 years
        /// </summary>
        /// <returns></returns>
        Task<List<RecentPaymentDTO>> GetAllRecentPaymentListAsync(CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Recent Invoices For Tenant
        /// </summary>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByTenantListAsync(CancellationToken token = default(CancellationToken));
    }
}
