﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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

    public interface IQAppUserReportDS:IBaseDS<BaseDTO> {

    /// <summary>
    /// Get All AppUserList By User Type for Platform Portal
    /// </summary>
    /// <returns></returns>
    Task<List<PlatAppUserReportDTO>> GetAllPFAppUserListByUserTypeAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken));


    /// <summary>
    /// Get All AppUserList By User Type for Publisher Portal
    /// </summary>
    /// <returns></returns>
    Task<List<PubAppUserReportDTO>> GetAllPubAppUserListByUserTypeAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All AppUserList By User Type for Business Portal
    /// </summary>
    /// <returns></returns>
    Task<List<BizAppUserReportDTO>> GetBizPayAppUserListByUserTypeAsync(ReportFilterDTO filter, Guid appId,  CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All AppUserList By User Type for Business Portal
        /// </summary>
        /// <returns></returns>
        Task<List<BizAppUserReportDTO>> GetBizCustAppUserListByUserTypeAsync(ReportFilterDTO filter, Guid appId, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All AppUserList By User Type for Business Partner Portal 
        /// </summary>
        /// <returns></returns>
        Task<List<PartAppUserReportDTO>> GetPartCustAppUserListAsync(ReportFilterDTO filter, Guid appId, Guid businesspartnertenantid, CancellationToken token = default(CancellationToken));

        /// <summary>
        /// Get All AppUserList By User Type for Business Partner Portal 
        /// </summary>
        /// <returns></returns>
        Task<List<PartAppUserReportDTO>> GetPartPayAppUserListAsync(ReportFilterDTO filter, Guid appId, Guid businesspartnertenantid, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All User Name By Tenant 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));

    /// <summary>
    /// Get All User Name By Tenant For PlatForm 
    /// </summary>
    /// <returns></returns>
    Task<List<NameDTO>> GetPFUserNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken));
  }
}
