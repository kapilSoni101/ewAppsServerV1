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
using System.Data.SqlClient;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {
    /// <summary>
    /// This class Contain Business Logic of Customer Portal Customer APp Dashboard 
    /// </summary>
    public class QBizCustDashboardDS:BaseDS<BaseDTO>, IQBizCustDashboardDS {

        #region Local Varialbe 
        IQBizCustDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        #region Constructor
        public QBizCustDashboardDS(IQBizCustDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }
        #endregion

        ///<inheritdoc/>
        public async Task<SalesQuotationsAndOrdersStatusCountDTO> GetSalesQuotationsAndOrdersStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetSalesQuotationsAndOrdersStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<RecentSalesQuotationsDTO>> GetAllRecentSalesQuotationsByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllRecentSalesQuotationsByTenantListAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<RecentSalesOrdersDTO>> GetAllRecentSalesOrdersByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllRecentSalesOrdersByTenantListAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<RecentDeliveriesDTO>> GetAllRecentDeliveriesByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllRecentDeliveriesByTenantListAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<UpcomingDeliveriesDTO>> GetAllUpcomingDeliveriesByTenantListAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllUpcomingDeliveriesByTenantListAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<DeliveriesStatusCountDTO> GetDeliveriesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetDeliveriesStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }
    }
}
