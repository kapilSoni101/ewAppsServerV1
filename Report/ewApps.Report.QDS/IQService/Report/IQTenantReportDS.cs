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

    public interface IQTenantReportDS :IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All Tenant List Report 
    /// </summary>
    /// <returns></returns> 
    Task<List<TenantReportDTO>> GetTenantListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Service Name  List By Tenant  
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetServiceNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Tenant Name  List By App  
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetTenantNameListByAppIdAsync(Guid appId, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Service Name  List By App  
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Service Name  List By App For platform 
    /// </summary>
    /// <returns></returns> 
    Task<List<NameDTO>> GetPFServiceNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Tenant Name  List By SubscriptionPlan  
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetTenantNameListBySubscriptionPlanIdAsync(Guid subscriptionPlanId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Tenant Name  List By App for platform 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFTenantNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Tenant Name  List By Publisher for platform 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Active Tenant Name  List By Tenant for platform 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFActiveTenantNameListByPublisherIdAsync(Guid publisherId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Tenant List for platform 
        /// </summary>
        /// <returns></returns>
        Task<List<PlatformTenantDTO>> GetTenantReportListOnPlatformAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get Service Name by plan id 
        /// </summary>
        /// <param name="planId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<NameDTO>> GetPFServiceNameListByPlanIdAsync(Guid planId, CancellationToken token = default(CancellationToken));

  }
}
