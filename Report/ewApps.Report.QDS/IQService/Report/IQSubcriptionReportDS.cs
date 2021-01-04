/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 19 December 2018
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 19 December 2018
 */
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Report.DTO;

namespace ewApps.Report.QDS {
    public interface IQSubcriptionReportDS :IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All Subscription List 
    /// </summary>
    /// <returns></returns> 
    Task<List<SubscriptionReportDTO>> GetSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All SubscriptionName List By Tenant
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetSubscriptionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All SubscriptionName List By AppId
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetSubscriptionNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Subscription List On Platform 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PlatSubscriptionReportDTO>> GetPlatSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));
  }
}
