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

namespace ewApps.Report.QData {

    /// <summary>
    /// This is the repository responsible for filtering data realted to Application Report and services related to it
    /// </summary>
    public interface IQApplicationReportRepository:IBaseRepository<BaseDTO> {

    /// <summary>
    /// Get All Application Report List For Publisher 
    /// </summary>
    /// <returns></returns>
    Task<List<ApplicationReportDTO>> GetApplicationListAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Application Report List For PlatForm
    /// </summary>
    /// <returns></returns>
    Task<List<PlatApplicationReportDTO>> GetPFApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get All Application Name By Tenant 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetApplicaitionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All Application Name By Tenant For Platform
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFApplicaitionNameListAsync(Guid publisherId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All Application Name By Tenant For Platform
        /// </summary>
        /// <returns></returns>
        Task<List<NameDTO>> GetBusinessAppSubscriptionInfoDTOByBusinessId(Guid tenantId, Guid homeAppId);
  }  
}
