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
    /// This is the repository responsible for filtering data realted to Customer Payment Dashboard and services related to it
    /// </summary>
    public interface IQCustPayDashboardRepository:IBaseRepository<BaseDTO>  {

        /// <summary>
        /// Get invoice status count by customer id
        /// </summary>
        /// <returns></returns>
        Task<BACInvoiceStatusCountDTO> GetInvoicesStatusCountForDashBoardByCustomerAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get business name and sum of invoice by customer id
        /// </summary>
        /// <returns></returns>
        Task<List<InoviceAndMonthNameDTO>> GetBusinessNameAndSumOfInvoiceByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All upcoming payment for Customer 
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Recent payment for Customer upto 1 years 
        /// </summary>
        /// <returns></returns>
        Task<List<RecentPaymentDTO>> GetAllRecentPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All upcoming recurring payment for Customer 
        /// </summary>
        /// <returns></returns>
        Task<List<UpComingPaymentDTO>> GetAllUpcomingRecurringPaymentByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Recent Invoice By Customer 
        /// </summary>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<RecentInvoicesDTO>> GetAllRecentInvoicesByCustomerListAsync(Guid customerId, CancellationToken token = default(CancellationToken));

    }
}
