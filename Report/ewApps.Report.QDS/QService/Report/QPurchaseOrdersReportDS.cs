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
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Purchase Orders Report
    /// </summary>
    public class QPurchaseOrdersReportDS:BaseDS<BaseDTO>, IQPurchaseOrdersReportDS {

        #region Local Member
        IQPurchaseOrdersReportRepository _purchaseOrdersReportRepository;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="salesOrdersReportRepository"></param>
        /// <param name="cacheService"></param>
        public QPurchaseOrdersReportDS(IQPurchaseOrdersReportRepository purchaseOrdersReportRepository, IUserSessionManager userSessionManager) : base(purchaseOrdersReportRepository) {
            _purchaseOrdersReportRepository = purchaseOrdersReportRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<List<VendPurchaseOrdersReportDTO>> GetBizVendPurchaseOrdersListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _purchaseOrdersReportRepository.GetBizVendPurchaseOrdersListByTenantIdAsync(filter, us.TenantId, token);
        }
    }
}
