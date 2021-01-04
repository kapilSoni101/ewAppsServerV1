/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 06 February 2020
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 06 February 2020
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
    /// This is the repository responsible for filtering data realted to Purchase Orders Report and services related to it
    /// </summary>
    public interface IQPurchaseOrdersReportRepository:IBaseRepository<BaseDTO> {

        /// <summary>
        /// Get Purchase Orders For Vendor Application on Business Portal
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendPurchaseOrdersReportDTO>> GetBizVendPurchaseOrdersListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
