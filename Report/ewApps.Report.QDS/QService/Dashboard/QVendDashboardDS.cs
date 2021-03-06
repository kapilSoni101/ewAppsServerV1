﻿/* Copyright © 2018 eWorkplace Apps (https://www.eworkplaceapps.com/). All Rights Reserved.
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
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ewApps.Core.BaseService;
using ewApps.Core.UserSessionService;
using ewApps.Report.DTO;
using ewApps.Report.QData;

namespace ewApps.Report.QDS {

    // This class Contain Business Logic Of Vendor Portal Vendor App 
    public class QVendDashboardDS:BaseDS<BaseDTO>, IQVendDashboardDS {

        #region Local Varialbe 
        IQVendDashboardRepository _dashboardRepository;
        //IAppDataService _appDataService;
        private IUserSessionManager _sessionManager;
        #endregion

        public QVendDashboardDS(IQVendDashboardRepository dashboardRepository, IUserSessionManager sessionManager) : base(dashboardRepository) {
            _dashboardRepository = dashboardRepository;
            _sessionManager = sessionManager;
        }

        ///<inheritdoc/>
        public async Task<VendApInvoiceStatusDTO> GetVendInvoicesStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendInvoicesStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<VendOrdersLineStatusDTO> GetVendOrderLineStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendOrderLineStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<VendOrderStatusDTO> GetVendOrderStatusCountForDashBoardByTenantAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetVendOrderStatusCountForDashBoardByTenantAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<VendOpenLinesListDTO>> GetAllVendOpenLineListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllVendOpenLineListByTenantLAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<VendRecentAPInvoices>> GetAllVendRecentAPInvoicesListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllVendRecentAPInvoicesListByTenantLAsync(us.TenantId, token);
        }

        ///<inheritdoc/>
        public async Task<List<VendRecentPurchaseOrder>> GetAllVendRecentPurchaseOrderListByTenantLAsync(Guid tenantId, CancellationToken token = default(CancellationToken)) {
            UserSession us = _sessionManager.GetSession();
            return await _dashboardRepository.GetAllVendRecentPurchaseOrderListByTenantLAsync(us.TenantId, token);
        }
    }
}
