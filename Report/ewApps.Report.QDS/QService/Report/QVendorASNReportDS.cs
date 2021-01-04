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
    /// This class Contain Business Logic of VendorASN Report 
    /// </summary>
    public class QVendorASNReportDS:BaseDS<BaseDTO>, IQVendorASNReportDS {

        #region Local Member
        IQVendorASNReportRepository _vendorASNReportRepository;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="vendorASNReportRepository"></param>
        public QVendorASNReportDS(IQVendorASNReportRepository vendorASNReportRepository, IUserSessionManager userSessionManager) : base(vendorASNReportRepository) {
            _vendorASNReportRepository = vendorASNReportRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        ///<inheritdoc/>
        public async Task<List<VendorASNReportDTO>> GetBizVendorASNListByTenantIdAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession us = _userSessionManager.GetSession();
            return await _vendorASNReportRepository.GetBizVendorASNListByTenantIdAsync(filter, us.TenantId, token);
        }
    }
}
