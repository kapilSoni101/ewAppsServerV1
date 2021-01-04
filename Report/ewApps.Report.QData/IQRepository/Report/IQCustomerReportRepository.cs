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
    /// This is the repository responsible for filtering data realted to Customer Report and services related to it
    /// </summary>
    public interface IQCustomerReportRepository :IBaseRepository<BaseDTO> {

    /// <summary>
    /// Get All Customers List By Tenant 
    /// </summary>
    /// <returns></returns>
    Task<List<CustomerReportDTO>> GetCustomerListByTenantAsync(ReportFilterDTO filter, Guid tenantId,Guid appId, CancellationToken token = default(CancellationToken));
  }
}
