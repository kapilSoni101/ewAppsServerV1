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
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Logic of OpenPurchaseLine Report 
    /// </summary>
    public class QOpenPurchaseLineReportDS:BaseDS<BaseDTO>, IQOpenPurchaseLineReportDS {

        #region Local Member
        IQOpenPurchaseLineReportRepository _openPurchaseLineReportRepository;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="openPurchaseLineReportRepository"></param>
        public QOpenPurchaseLineReportDS(IQOpenPurchaseLineReportRepository openPurchaseLineReportRepository, IUserSessionManager userSessionManager) : base(openPurchaseLineReportRepository) {
            _openPurchaseLineReportRepository = openPurchaseLineReportRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Business
        ///<inheritdoc/>
        public async Task<List<VendOpenPurchaseLineReportDTO>> GetBizVendOpenPurchaseLineListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _openPurchaseLineReportRepository.GetBizVendOpenPurchaseLineListByTenantIdAsync(filter, us.TenantId, token);
    }
    #endregion Business


}
}
