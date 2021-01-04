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
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    /// <summary>
    /// This class Contain Business Login of Subscription Report 
    /// </summary>
    public class QSubcriptionReportDS:BaseDS<BaseDTO>, IQSubcriptionReportDS {

        #region Local Member
        IQSubcriptionReportRepository _subcriptionReportRepository;
        IUserSessionManager _userSessionManager;
        #endregion

        #region Constructor
        /// <summary>
        ///  Constructor Initialize the Base Variable
        /// </summary>
        /// <param name="subcriptionReportRepository"></param>
        /// <param name="cacheService"></param>
        public QSubcriptionReportDS(IQSubcriptionReportRepository subcriptionReportRepository, IUserSessionManager userSessionManager) : base(subcriptionReportRepository) {
            _subcriptionReportRepository = subcriptionReportRepository;
            _userSessionManager = userSessionManager;
        }

        #endregion Constructor

        #region Get

        ///<inheritdoc/>
        public async Task<List<SubscriptionReportDTO>> GetSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            return await _subcriptionReportRepository.GetSubscriptionListAsync(filter, session.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetSubscriptionNameListByTenantIdAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            return await _subcriptionReportRepository.GetSubscriptionNameListByTenantIdAsync(tenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<NameDTO>> GetSubscriptionNameListByAppIdAsync(Guid appId, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            return await _subcriptionReportRepository.GetSubscriptionNameListByAppIdAsync(appId, session.TenantId, token);
        }

        #region Platform Subscription Report 
        ///<inheritdoc/>
        public async Task<List<PlatSubscriptionReportDTO>> GetPlatSubscriptionListAsync(ReportFilterDTO filter, CancellationToken token = default(CancellationToken)) {
            UserSession session = _userSessionManager.GetSession();
            return await _subcriptionReportRepository.GetPlatSubscriptionListAsync(filter, session.TenantId, token);
        }

        #endregion
        #endregion
    }
}
