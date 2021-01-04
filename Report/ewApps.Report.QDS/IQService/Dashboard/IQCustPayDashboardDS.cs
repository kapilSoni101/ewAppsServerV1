/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 07 October 2019
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 07 October 2019
 */

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS {
    public interface IQCustPayDashboardDS {


        /// <summary>
        /// Get All Upcoming Payment list for Customer
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Recent Payment list for Customer upto 1 years
        /// </summary>
        /// <returns></returns>
        Task<List<RecentPaymentDTO>> GetAllRecentPaymentListForCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Gets the invoice status count for dashboard by given customer.
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business name and sum of invoice by customer id
        /// </summary>
        /// <returns></returns>
        Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Recent Invoices By Customer TenantId 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));
    }
}
