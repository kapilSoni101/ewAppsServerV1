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

    public interface IQApplicationReportDS:IBaseDS<BaseDTO> {
    /// <summary>
    /// Get All Application Report List For Publisher
    /// </summary>
    /// <returns></returns>
    Task<List<ApplicationReportDTO>> GetApplicationListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));

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
    /// Get All Application Name By Tenant for Platform  
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFApplicaitionNameListAsync(Guid publisherId, CancellationToken token = default(CancellationToken));


        /// <summary>
        /// Get All Application Name By Tenant for Platform  
        /// </summary>
        /// <returns></returns>
        Task<List<NameDTO>> GetBusinessAppSubscriptionInfoDTOAsync(Guid tenantId);
  }

}
