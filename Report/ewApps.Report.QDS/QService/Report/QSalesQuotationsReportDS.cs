/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {    
        /// <summary>
        /// This class Contain Business Login of Sales Orders Report
        /// </summary>
        public class QSalesQuotationsReportDS:BaseDS<BaseDTO>, IQSalesQuotationsReportDS {

        #region Local Member
        IQSalesQuotationsReportRepository _salesQuotationsReportRepository;
            IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="salesQuotationsReportRepository"></param>
        /// <param name="cacheService"></param>
        public QSalesQuotationsReportDS(IQSalesQuotationsReportRepository salesQuotationsReportRepository, IUserSessionManager userSessionManager) : base(salesQuotationsReportRepository) {
            _salesQuotationsReportRepository = salesQuotationsReportRepository;
                _userSessionManager = userSessionManager;
            }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<BizCustSalesQuotationsReportDTO>> GetBizCustSalesQuotationsListByTenantIdAsync(ReportFilterDTO filter,  CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _salesQuotationsReportRepository.GetBizCustSalesQuotationsListByTenantIdAsync(filter, us.TenantId, token);
        }
        #endregion

        #region Customer
        ///<inheritdoc/>
        public async Task<List<PartCustQuotationsReportDTO>> GetPartCustSalesQuotationsListByCustomerIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            return await _salesQuotationsReportRepository.GetPartCustSalesQuotationsListByCustomerIdAsync(filter, token);
        }
        #endregion
    }
}
