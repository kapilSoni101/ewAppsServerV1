/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 11 Nov 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 11 Nov 2019
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
    /// This class Contain Business Logic of ItemMaster Report 
    /// </summary>
    public interface IQItemMasterReportDS:IBaseDS<BaseDTO> {

        /// <summary>
        /// Get Item Master Report For Vendor Application On Business Portal 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<VendItemMasterReportDTO>> GetBizVendItemMasterListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get Item Master Report For Customer Portal
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PartItemMasterReportDTO>> GetCustItemMasterListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));
    }
}
