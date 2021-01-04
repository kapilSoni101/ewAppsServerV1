/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
 * Unauthorized copying of this file, via any medium is strictly prohibited
 * Proprietary and confidential
 * 
 * Author: Atul Badgujar <abadgujar@batchmaster.com>
 * Date: 4 February 2020
 * 
 * Contributor/s: Nitin Jain
 * Last Updated On: 4 February 2020
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

    // This class Contain Business Logic Of Business Portal Vendor App 
    public class QBizVendDashboardDS:BaseDS<BaseDTO>, IQBizVendDashboardDS {

        #region Local Varialbe 
        IQBizVendDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        public QBizVendDashboardDS(IQBizVendDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }        

        ///<inheritdoc/>
        public async Task<VendApInvoiceStatusDTO> GetVendInvoicesStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendInvoicesStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<VendOrdersLineStatusDTO> GetVendOrderLineStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendOrderLineStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<VendOrderStatusDTO> GetVendOrderStatusCountForDashBoardByTenantAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendOrderStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<VendOpenLinesListDTO>> GetAllVendOpenLineListByTenantLAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();            
            return await _dashboardRepository.GetAllVendOpenLineListByTenantLAsync(us.TenantId, token);            
        }

        ///<inheritdoc/>
        public async Task<List<VendRecentAPInvoices>> GetAllVendRecentAPInvoicesListByTenantLAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllVendRecentAPInvoicesListByTenantLAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<VendRecentPurchaseOrder>> GetAllVendRecentPurchaseOrderListByTenantLAsync(CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllVendRecentPurchaseOrderListByTenantLAsync(us.TenantId, token);
        }
    }
}
