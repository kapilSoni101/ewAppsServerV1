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

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Purchase Orders Report
    /// </summary>
    public interface IQPurchaseOrdersReportDS:IBaseDS<BaseDTO> {

        /// <summary>
        /// Get Purchase Orders For Vendor Application on Business Portal
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendPurchaseOrdersReportDTO>> GetBizVendPurchaseOrdersListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));
    }
}
