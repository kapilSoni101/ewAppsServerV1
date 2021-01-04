﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 15 Oct 2019
 * 
 * Contributor/s: Sanjeev Khanna 
 * Last Updated On: 15 Oct 2019
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
    /// This is the repository responsible for filtering data realted to Sales Quotation Report and services related to it
    /// </summary>
    public interface IQSalesQuotationsReportRepository:IBaseRepository<BaseDTO> {

        #region Business 
        /// <summary>
        /// Get Sales Quotations For Business 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="tenantId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<BizCustSalesQuotationsReportDTO>> GetBizCustSalesQuotationsListByTenantIdAsync(ReportFilterDTO filter, Guid tenantId, CancellationToken token = default(CancellationToken));
        #endregion

        #region Customer

        /// <summary>
        /// Get Sales Quotations For Customer 
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="customerId"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<List<PartCustQuotationsReportDTO>> GetPartCustSalesQuotationsListByCustomerIdAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken));
        #endregion

    }
}
